using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Laborator2
{
    public partial class Form1 : Form
    {
        string connectionString;
        string parentTable;
        string childTable;
        string parentKey;
        string childKey;
        string foreignKey;
        List<String> parentColumns = new List<String>();
        List<String> childColumns = new List<String>();
        SqlConnection sqlConnection;

        public Form1()
        {
            InitializeComponent();
            DataConfiguration();
            inputs();
            InputReset();
            CONNECT.Enabled = true;
        }

        //iau configurarile din app.config si creez si restul de configurari
        private void DataConfiguration()
        {
            //configurarile din app.config
            connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            parentTable = ConfigurationManager.AppSettings["parentTable"];
            childTable = ConfigurationManager.AppSettings["childTable"];

            sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand;

            //configurari parent
            string getParentKey = "SELECT COLUMN_NAME " +
                "FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                "WHERE TABLE_NAME LIKE '" + parentTable + "' " +
                "AND CONSTRAINT_NAME LIKE 'PK%'";


            string getColumsParent = "SELECT COLUMN_NAME " +
                                      "FROM INFORMATION_SCHEMA.COLUMNS " +
                                      "WHERE TABLE_NAME = N'" + parentTable + "'";

            //configurari copil
            string getChildKey = "SELECT COLUMN_NAME " +
                                 "FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE " +
                                 "WHERE TABLE_NAME LIKE '" + childTable + "' " +
                                 "AND CONSTRAINT_NAME LIKE 'PK%'";


            string getColumsChild = "SELECT COLUMN_NAME " +
                                      "FROM INFORMATION_SCHEMA.COLUMNS " +
                                      "WHERE TABLE_NAME = N'" + childTable + "'";

            //FK imi da coloeana pe care face fk
            string fk = "SELECT COL_NAME(parent_object_id, parent_column_id) " +
                        "FROM sys.foreign_key_columns " +
                        "WHERE parent_object_id = OBJECT_ID( '" + childTable + "') " +
                        "AND OBJECT_NAME(referenced_object_id) = '" + parentTable +"'";
                      

            try
            {
                sqlConnection.Open();

                //ce este executat scalare, e pt. a lua doar primul rezultat si restul sa fie ignorat

                //pk-ul tabelului parinte
                sqlCommand = new SqlCommand(getParentKey, sqlConnection);
                parentKey = (string)sqlCommand.ExecuteScalar();

                //pk-ul tabelului copil
                sqlCommand = new SqlCommand(getChildKey, sqlConnection);
                childKey = (string)sqlCommand.ExecuteScalar();

                //fk-ul
                sqlCommand = new SqlCommand(fk, sqlConnection);
                foreignKey = (string)sqlCommand.ExecuteScalar();

                //se selecteaza coloanele tabelului parinte
                //rezultatul este transformat intr-o lista de string-uri
                sqlCommand = new SqlCommand(getColumsParent, sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    parentColumns.Add(sqlDataReader.GetString(0));
                }
                sqlDataReader.Close();

                //se selecteaza coloanele tabelului copil
                //rezultatul este transformat intr-o lista de string-uri
                sqlCommand = new SqlCommand(getColumsChild, sqlConnection);
                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    childColumns.Add(sqlDataReader.GetString(0));
                }
                sqlDataReader.Close();

                //conexiunea inchisa
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sqlConnection.Close();
            }


        }

        //sunt create textbox-urile si label-urile pt insert, delete, update
        private void inputs()
        {
            int i;

            for(i = 0; i < childColumns.Count; i++)
            {
                //este creat label-ul dupa numele din db
                Label label = new Label() { Text = childColumns[i] + " = "};
                Inputs.Controls.Add(label);

                //avem textbox dupa label
                //Inputs e fereastra de forms panel
                TextBox textBox = new TextBox() { Name = childColumns[i].ToString()};
                Inputs.Controls.Add(textBox);

                //cum sunt pozitionate in Inputs datele
                label.Location = new System.Drawing.Point(0, 30 * i);
                textBox.Location = new System.Drawing.Point(100, 30 * i);
            }
        }

        private void InputReset()
        {
            foreach (Control control in Inputs.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Clear();
                    textBox.Enabled = false;
                }
            }
            INSERT.Enabled = false;
            UPDATE.Enabled = false;
            DELETE.Enabled = false;
        }

        private void INSERT_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                //avem nevoie de un SqlDataAdapter pentru inserare
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                //initial setul de parametrii este creat ca si o lista de stringuri
                List<string> parametrii = new List<string>();

                foreach (string childColumn in childColumns)
                {
                    parametrii.Add("@" + childColumn);
                }

                //string-ul comenzii de insert este facut cu parametrii necesari
                string insert = "INSERT INTO " + childTable + " VALUES(" + string.Join(", ", parametrii) + ")";
                dataAdapter.InsertCommand = new SqlCommand(insert, sqlConnection);

                //valorile parametrilor sunt obtinute din textboxuri
                foreach (Control control in Inputs.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;
                        if (!String.IsNullOrWhiteSpace(textBox.Text))
                        {
                            dataAdapter.InsertCommand.Parameters
                                .AddWithValue("@" + control.Name, textBox.Text);
                        }
                        //textboxurile goale --> NULL
                        else
                        {
                            dataAdapter.InsertCommand.Parameters
                                .AddWithValue("@" + control.Name, DBNull.Value);
                        }
                    }
                }

                try
                {
                    //se stabileste o conexiune la baza de date si se executa comanda
                    sqlConnection.Open();
                    dataAdapter.InsertCommand.ExecuteNonQuery();
                    MessageBox.Show("Inserted data successfully.");
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sie haben falsche Daten gegeben.");
                    sqlConnection.Close();
                }
                
                //pentru a vedea datele nou inserate, trebuie dat refresh la baza de date
                InputReset();
                CONNECT_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sqlConnection.Close();
            }
        }

        private void DELETE_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                //avem nevoie de un SqlDataAdapter pentru delete
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                //se declara stringul comenzii de delete, contine 1 singur parametru
                dataAdapter.DeleteCommand = new SqlCommand("DELETE FROM " + childTable +
                    " WHERE " + childKey + "= @" + childKey, sqlConnection);

                //valoarea cheii este cautata in textboxuri
                foreach (Control control in Inputs.Controls)
                {
                    if (control is TextBox)
                    {
                        if (control.Name.Equals(childKey))
                        {
                            dataAdapter.DeleteCommand.Parameters
                                .AddWithValue("@" + childKey, ((TextBox)control).Text);
                        }
                    }
                }

                try
                {
                    //se stabileste o conexiune la baza de date si se executa comanda
                    sqlConnection.Open();
                    dataAdapter.DeleteCommand.ExecuteNonQuery();
                    MessageBox.Show("Deleted data successfully.");
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sie haben falsche Daten gegeben.");
                    sqlConnection.Close();
                }


                //pentru a vedea datele sterse, trebuie dat refresh la baza de date
                InputReset();
                CONNECT_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sqlConnection.Close();
            }
        }

        private void UPDATE_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                //avem nevoie de un SqlDataAdapter pentru update
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                //initial setul de parametrii este creat ca si o lista de stringuri
                List<string> parametrii = new List<string>();
                foreach (string childColumn in childColumns)
                {
                    if (childColumn != childKey)
                        parametrii.Add(childColumn + " = @" + childColumn);
                }

                //string-ul comenzii de update este facut cu parametrii necesari
                string update = "UPDATE " + childTable + " SET " + string.Join(", ", parametrii) + " WHERE " + childKey + "= @" + childKey;
                dataAdapter.UpdateCommand = new SqlCommand(update, sqlConnection);

                //valorile parametrilor sunt obtinute din textboxuri
                foreach (Control control in Inputs.Controls)
                {
                    if (control is TextBox)
                    {
                        TextBox textBox = (TextBox)control;

                        if (!String.IsNullOrWhiteSpace(textBox.Text))
                        {
                            dataAdapter.UpdateCommand.Parameters
                                .AddWithValue("@" + control.Name, textBox.Text);
                        }
                        //textboxurile goale --> NULL
                        else
                        {
                            dataAdapter.UpdateCommand.Parameters
                                .AddWithValue("@" + control.Name, DBNull.Value);
                        }

                    }
                }

                try
                {
                    //se stabileste o conexiune la baza de date si se executa comanda
                    sqlConnection.Open();
                    dataAdapter.UpdateCommand.ExecuteNonQuery();
                    MessageBox.Show("Updated data successfully.");
                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Sie haben falsche Daten gegeben.");
                    sqlConnection.Close();
                }


                //pentru a vedea datele updatate, trebuie dat refresh la baza de date
                InputReset();
                CONNECT_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                sqlConnection.Close();
            }
        }

        private void CONNECT_Click(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            
            SqlDataAdapter parentAdapter = new SqlDataAdapter("SELECT * FROM " + parentTable, connectionString);
            SqlDataAdapter childAdapter = new SqlDataAdapter("SELECT * FROM " + childTable, connectionString);

            //un DataTable numit "parent" este adaugat la DataSet si este populat folosind parentAdapter
            parentAdapter.Fill(dataSet, "parent");
            //un DataTable numit "child" este adaugat la DataSet si este populat folosind childAdapter
            childAdapter.Fill(dataSet, "child");

            //se creeaza legatuta 1:M
            DataRelation dataRelation = new DataRelation("FK_parent_child",
                dataSet.Tables["parent"].Columns[parentKey],
                dataSet.Tables["child"].Columns[foreignKey]);

            try
            {
                dataSet.Relations.Add(dataRelation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            BindingSource bindingSource1 = new BindingSource() { DataSource =  dataSet};
            bindingSource1.DataMember = "parent";

            BindingSource bindingSource2 = new BindingSource() { DataSource = bindingSource1};
            bindingSource2.DataMember = "FK_parent_child";

            Parent.DataSource = bindingSource1;
            Child.DataSource = bindingSource2;
        }

        private void Parent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                InputReset();
                int positionPK = -1;
                int i;

                //nu stim exact pe ce poz e pk-ul parintelui, pt. ca nu e mereu primul atribut din tabel
                for (i = 0; i < parentColumns.Count && positionPK == -1; i++)
                {
                    if (parentColumns[i].Equals(parentKey))
                        positionPK = i;
                }
                
                //valoarea pk-ului parinte este selectata din randul pe care am facut click
                string parentKeyValue = Parent[positionPK, Parent.CurrentCell.RowIndex].Value.ToString();

                //daca se face click pe un rand nevalid, nu se activeze interactiunea sau fill-ul
                if (!String.IsNullOrWhiteSpace(parentKeyValue))
                {
                    foreach (Control control in Inputs.Controls)
                    {
                        //textbox-urile trebuie modificate
                        if (control is TextBox)
                        {
                            TextBox textBox = (TextBox)control;
                            //textBox-ul cu fk este filluit cu valoarea pk-ului parinte
                            //si ramane dezactivat
                            if (control.Name.Equals(foreignKey))
                            {
                                textBox.Text = parentKeyValue;
                                textBox.Enabled = false;
                            }
                            //celelalte textbox-uri sunt golite si activate
                            else
                            {
                                textBox.Clear();
                                textBox.Enabled = true;
                            }
                        }
                    }
                    //se poate face inserarea
                    INSERT.Enabled = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void Child_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                InputReset();
                int rowNumber = Child.CurrentCell.RowIndex;
                int i;

                //sa aflu unde e fk-ul si pk-ul
                int fkPosition = -1;
                int pkPosition = -1;

                //caut pozitia la pk
                for (i = 0; i < childColumns.Count && pkPosition == -1; i++)
                {
                    if (childColumns[i].Equals(childKey))
                        pkPosition = i;
                }

                //caut pozitia la fk
                for (i = 0; i < childColumns.Count && fkPosition == -1; i++)
                {
                    if (childColumns[i].Equals(foreignKey))
                        fkPosition = i;
                }

                //daca se face click pe un rand nevalid, nu trevuie sa se activeze interactiunea sau fill-ul
                if (!String.IsNullOrWhiteSpace(Child[pkPosition, rowNumber].Value.ToString()))
                {
                    int controlNumber = 0;
                    foreach (Control control in Inputs.Controls)
                    {
                        //textboxurile trebuie modificate
                        if (control is TextBox)
                        {
                            TextBox textBox = (TextBox)control;

                            //se completeaza datele randului pe care am facut click
                            textBox.Text = Child[controlNumber, rowNumber].Value.ToString();

                            if (control.Name.Equals(childKey))
                            {
                                textBox.Enabled = false;
                            }
                            else
                            {
                                 textBox.Enabled = true;
                            }
                            
                            controlNumber++;

                        }
                    }

                    //update-uri si delete-uri sunt permise
                    UPDATE.Enabled = true;
                    DELETE.Enabled = true;
                }
                else
                {
                    foreach (Control control in Inputs.Controls)
                    {
                        if (control is TextBox)
                        {
                            if (control.Name.Equals(foreignKey))
                            {
                                ((TextBox)control).Text = Child[fkPosition, rowNumber].Value.ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }
    }
}
