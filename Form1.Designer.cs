namespace Laborator2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Parent = new System.Windows.Forms.DataGridView();
            this.Child = new System.Windows.Forms.DataGridView();
            this.INSERT = new System.Windows.Forms.Button();
            this.DELETE = new System.Windows.Forms.Button();
            this.UPDATE = new System.Windows.Forms.Button();
            this.CONNECT = new System.Windows.Forms.Button();
            this.Inputs = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.Parent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Child)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(43, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Parent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 13.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(43, 279);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Child";
            // 
            // Parent
            // 
            this.Parent.BackgroundColor = System.Drawing.Color.LightCyan;
            this.Parent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Parent.Location = new System.Drawing.Point(43, 74);
            this.Parent.Margin = new System.Windows.Forms.Padding(2);
            this.Parent.Name = "Parent";
            this.Parent.RowHeadersWidth = 51;
            this.Parent.RowTemplate.Height = 24;
            this.Parent.Size = new System.Drawing.Size(594, 188);
            this.Parent.TabIndex = 2;
            this.Parent.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Parent_CellContentClick);
            // 
            // Child
            // 
            this.Child.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Child.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Child.BackgroundColor = System.Drawing.Color.LightCyan;
            this.Child.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Child.Location = new System.Drawing.Point(43, 301);
            this.Child.Margin = new System.Windows.Forms.Padding(2);
            this.Child.Name = "Child";
            this.Child.RowHeadersWidth = 51;
            this.Child.RowTemplate.Height = 24;
            this.Child.Size = new System.Drawing.Size(288, 236);
            this.Child.TabIndex = 3;
            this.Child.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Child_CellContentClick);
            // 
            // INSERT
            // 
            this.INSERT.BackColor = System.Drawing.Color.MediumTurquoise;
            this.INSERT.Enabled = false;
            this.INSERT.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.INSERT.ForeColor = System.Drawing.SystemColors.Desktop;
            this.INSERT.Location = new System.Drawing.Point(674, 301);
            this.INSERT.Margin = new System.Windows.Forms.Padding(2);
            this.INSERT.Name = "INSERT";
            this.INSERT.Size = new System.Drawing.Size(112, 25);
            this.INSERT.TabIndex = 4;
            this.INSERT.Text = "INSERT";
            this.INSERT.UseVisualStyleBackColor = false;
            this.INSERT.Click += new System.EventHandler(this.INSERT_Click);
            // 
            // DELETE
            // 
            this.DELETE.BackColor = System.Drawing.Color.MediumTurquoise;
            this.DELETE.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DELETE.Location = new System.Drawing.Point(674, 342);
            this.DELETE.Margin = new System.Windows.Forms.Padding(2);
            this.DELETE.Name = "DELETE";
            this.DELETE.Size = new System.Drawing.Size(112, 24);
            this.DELETE.TabIndex = 5;
            this.DELETE.Text = "DELETE";
            this.DELETE.UseVisualStyleBackColor = false;
            this.DELETE.Click += new System.EventHandler(this.DELETE_Click);
            // 
            // UPDATE
            // 
            this.UPDATE.BackColor = System.Drawing.Color.MediumTurquoise;
            this.UPDATE.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UPDATE.Location = new System.Drawing.Point(674, 379);
            this.UPDATE.Margin = new System.Windows.Forms.Padding(2);
            this.UPDATE.Name = "UPDATE";
            this.UPDATE.Size = new System.Drawing.Size(112, 24);
            this.UPDATE.TabIndex = 6;
            this.UPDATE.Text = "UPDATE";
            this.UPDATE.UseVisualStyleBackColor = false;
            this.UPDATE.Click += new System.EventHandler(this.UPDATE_Click);
            // 
            // CONNECT
            // 
            this.CONNECT.BackColor = System.Drawing.Color.MediumTurquoise;
            this.CONNECT.Enabled = false;
            this.CONNECT.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CONNECT.ForeColor = System.Drawing.SystemColors.Desktop;
            this.CONNECT.Location = new System.Drawing.Point(43, 24);
            this.CONNECT.Margin = new System.Windows.Forms.Padding(2);
            this.CONNECT.Name = "CONNECT";
            this.CONNECT.Size = new System.Drawing.Size(112, 25);
            this.CONNECT.TabIndex = 7;
            this.CONNECT.Text = "CONNECT";
            this.CONNECT.UseVisualStyleBackColor = false;
            this.CONNECT.Click += new System.EventHandler(this.CONNECT_Click);
            // 
            // Inputs
            // 
            this.Inputs.AutoScroll = true;
            this.Inputs.BackColor = System.Drawing.Color.LightCyan;
            this.Inputs.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Inputs.Location = new System.Drawing.Point(350, 301);
            this.Inputs.Margin = new System.Windows.Forms.Padding(2);
            this.Inputs.Name = "Inputs";
            this.Inputs.Size = new System.Drawing.Size(287, 236);
            this.Inputs.TabIndex = 16;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(811, 575);
            this.Controls.Add(this.Inputs);
            this.Controls.Add(this.CONNECT);
            this.Controls.Add(this.UPDATE);
            this.Controls.Add(this.DELETE);
            this.Controls.Add(this.INSERT);
            this.Controls.Add(this.Child);
            this.Controls.Add(this.Parent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Parent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Child)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView Parent;
        private System.Windows.Forms.DataGridView Child;
        private System.Windows.Forms.Button INSERT;
        private System.Windows.Forms.Button DELETE;
        private System.Windows.Forms.Button UPDATE;
        private System.Windows.Forms.Button CONNECT;
        private System.Windows.Forms.Panel Inputs;
    }
}

