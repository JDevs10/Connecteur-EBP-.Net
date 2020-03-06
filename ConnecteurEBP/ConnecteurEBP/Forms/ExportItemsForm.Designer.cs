namespace ConnecteurEBP.Forms
{
    partial class ExportItemsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportItemsForm));
            this.closeButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.exportCustomersButton = new System.Windows.Forms.Button();
            this.exportCustomersCommandTextBox = new System.Windows.Forms.TextBox();
            this.exportCustomersFileBrowseButton = new System.Windows.Forms.Button();
            this.exportCustomersFilenameTextBox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.customersDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(755, 545);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 40);
            this.closeButton.TabIndex = 19;
            this.closeButton.Text = "Fermer";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.exportCustomersButton);
            this.groupBox2.Controls.Add(this.exportCustomersCommandTextBox);
            this.groupBox2.Controls.Add(this.exportCustomersFileBrowseButton);
            this.groupBox2.Controls.Add(this.exportCustomersFilenameTextBox);
            this.groupBox2.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(15, 368);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(812, 170);
            this.groupBox2.TabIndex = 27;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Exporter les factures";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Sans Unicode", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(331, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "Chemin du fichier d\'export des factures au format CSV :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "Commande exécutée :";
            // 
            // exportCustomersButton
            // 
            this.exportCustomersButton.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportCustomersButton.Location = new System.Drawing.Point(729, 66);
            this.exportCustomersButton.Name = "exportCustomersButton";
            this.exportCustomersButton.Size = new System.Drawing.Size(77, 28);
            this.exportCustomersButton.TabIndex = 7;
            this.exportCustomersButton.Text = "Exporter";
            this.exportCustomersButton.UseVisualStyleBackColor = true;
            this.exportCustomersButton.Click += new System.EventHandler(this.exportCustomersButton_Click);
            // 
            // exportCustomersCommandTextBox
            // 
            this.exportCustomersCommandTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportCustomersCommandTextBox.Location = new System.Drawing.Point(166, 100);
            this.exportCustomersCommandTextBox.Multiline = true;
            this.exportCustomersCommandTextBox.Name = "exportCustomersCommandTextBox";
            this.exportCustomersCommandTextBox.ReadOnly = true;
            this.exportCustomersCommandTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.exportCustomersCommandTextBox.Size = new System.Drawing.Size(640, 50);
            this.exportCustomersCommandTextBox.TabIndex = 6;
            // 
            // exportCustomersFileBrowseButton
            // 
            this.exportCustomersFileBrowseButton.Font = new System.Drawing.Font("Lucida Sans Unicode", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportCustomersFileBrowseButton.Location = new System.Drawing.Point(634, 66);
            this.exportCustomersFileBrowseButton.Name = "exportCustomersFileBrowseButton";
            this.exportCustomersFileBrowseButton.Size = new System.Drawing.Size(89, 28);
            this.exportCustomersFileBrowseButton.TabIndex = 5;
            this.exportCustomersFileBrowseButton.Text = "Parcourir";
            this.exportCustomersFileBrowseButton.UseVisualStyleBackColor = true;
            this.exportCustomersFileBrowseButton.Click += new System.EventHandler(this.exportCustomersFileBrowseButton_Click);
            // 
            // exportCustomersFilenameTextBox
            // 
            this.exportCustomersFilenameTextBox.Font = new System.Drawing.Font("Lucida Sans Unicode", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exportCustomersFilenameTextBox.Location = new System.Drawing.Point(15, 66);
            this.exportCustomersFilenameTextBox.Name = "exportCustomersFilenameTextBox";
            this.exportCustomersFilenameTextBox.Size = new System.Drawing.Size(614, 28);
            this.exportCustomersFilenameTextBox.TabIndex = 4;
            this.exportCustomersFilenameTextBox.TextChanged += new System.EventHandler(this.exportCustomersFilenameTextBox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.customersDataGridView);
            this.groupBox1.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(818, 330);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Les Factures des ventes";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // customersDataGridView
            // 
            this.customersDataGridView.AllowUserToAddRows = false;
            this.customersDataGridView.AllowUserToDeleteRows = false;
            this.customersDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.customersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customersDataGridView.Location = new System.Drawing.Point(3, 28);
            this.customersDataGridView.MultiSelect = false;
            this.customersDataGridView.Name = "customersDataGridView";
            this.customersDataGridView.ReadOnly = true;
            this.customersDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.customersDataGridView.Size = new System.Drawing.Size(812, 299);
            this.customersDataGridView.TabIndex = 0;
            this.customersDataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customersDataGridView_CellContentClick);
            // 
            // ExportItemsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 591);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.closeButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(850, 630);
            this.MinimumSize = new System.Drawing.Size(850, 630);
            this.Name = "ExportItemsForm";
            this.Text = "Export des factures";
            this.Load += new System.EventHandler(this.ExportItemsForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customersDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button exportCustomersButton;
        private System.Windows.Forms.TextBox exportCustomersCommandTextBox;
        private System.Windows.Forms.Button exportCustomersFileBrowseButton;
        private System.Windows.Forms.TextBox exportCustomersFilenameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView customersDataGridView;
    }
}