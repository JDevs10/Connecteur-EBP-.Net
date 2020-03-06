using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ConnecteurEBP.Classes;
using ConnecteurEBP.Utilities;

namespace ConnecteurEBP.Forms
{
    public partial class ExportItemsForm : Form
    {
        #region Constructeur
        /// <summary>
        /// Création d'une instance de ExportItemsForm
        /// </summary>
        public ExportItemsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Intéractions avec l'application
        /// <summary>
        /// Récupération des articles du dossier
        /// </summary>
        /// <returns>Retourne la liste des articles</returns>
        private List<FactureVente> GetFacturesFromDatabase()
        {
            List<FactureVente> items = new List<FactureVente>();
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.Factures_Ventes_SQLServer, sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader["DocumentNumber"] as string;
                                string name = reader["CustomerName"] as string;
                                decimal? a = reader["AmountVatExcludedWithDiscountAndShipping"] as decimal?;
                                decimal? b = reader["VatAmount"] as decimal?;
                                decimal? c = reader["AmountVatIncluded"] as decimal?;
                                decimal? d = reader["DepositAmount"] as decimal?;
                                decimal? e = reader["TotalDueAmount"] as decimal?;
                                decimal? f = reader["CommitmentsBalanceDue"] as decimal?;

                                items.Add(new FactureVente(id,name, a, b,c,d,e,f));
                            }
                        }
                    }
                    return items;
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// Lancement de l'application et export des articles
        /// </summary>
        
        /// <summary>
        /// Lancement de l'application et export des clients
        /// </summary>
        private void ExportCustomers()
        {
            try
            {
                //Création des arguments de la ligne de commande
                string exportArguments = string.Format(CommandLinesHelper.ExportFactures, exportCustomersFilenameTextBox.Text);
                //Exécution de la ligne de commande
                Utils.LaunchCommandLineProcess(exportArguments);
            }
            catch (UnauthorizedAccessException ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show(ex.Message);
            }
            catch (IOException ex)
            {
                //Exception pouvant survenir si le chemin du fichier est trop long ou s'il est introuvable
                MessageBox.Show(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                //Exception pouvant survenir si le format du fichier est incorrect
                MessageBox.Show(ex.Message);
            }
            catch (SDKException ex)
            {
                //Exceptions issues de la méthode LaunchProcess
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region Méthodes

        /// <summary>
        /// Choix du fichier d'export des clients
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void exportCustomersFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "CSV|*.csv";
                dialog.AddExtension = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(dialog.FileName).ToLower() == ".csv")
                    {
                        exportCustomersFilenameTextBox.Text = dialog.FileName;
                        exportCustomersCommandTextBox.Text = string.Format(CommandLinesHelper.ExportFactures, exportCustomersFilenameTextBox.Text);
                    }
                    else
                    {
                        exportCustomersFilenameTextBox.Text = string.Empty;
                        exportCustomersCommandTextBox.Text = string.Empty;
                        MessageBox.Show("Le format de ce fichier doit être de type CSV.");
                    }
                }
            }
        }

        /// <summary>
        /// Lancement de l'export des articles
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
      
        /// <summary>
        /// Lancement de l'export des clients
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void exportCustomersButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(exportCustomersFilenameTextBox.Text))
            {
                MessageBox.Show("Le chemin du fichier d'export des clients doit être renseigné");
                return;
            }
            ExportCustomers();
        }

        /// <summary>
        /// Fermeture de la fenêtre
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //Affichage des articles du dossier
            customersDataGridView.DataSource = GetFacturesFromDatabase();
            //Formatage de la grille des articles
            if (customersDataGridView.Columns["Id"] != null)
                customersDataGridView.Columns["Id"].HeaderText = "Numéro du document";
            if (customersDataGridView.Columns["CustomerName"] != null)
                customersDataGridView.Columns["CustomerName"].HeaderText = "Nom du Client";
            if (customersDataGridView.Columns["AmountVatExcludedWithDiscountAndShipping"] != null)
                customersDataGridView.Columns["AmountVatExcludedWithDiscountAndShipping"].HeaderText = "Montant total HT";
            if (customersDataGridView.Columns["VatAmount"] != null)
                customersDataGridView.Columns["VatAmount"].HeaderText = "Montant de TVA";
            if (customersDataGridView.Columns["AmountVatIncluded"] != null)
                customersDataGridView.Columns["AmountVatIncluded"].HeaderText = "Montant TTC";
            if (customersDataGridView.Columns["DepositAmount"] != null)
                customersDataGridView.Columns["DepositAmount"].HeaderText = "Montant de l'acompte";
            if (customersDataGridView.Columns["TotalDueAmount"] != null)
                customersDataGridView.Columns["TotalDueAmount"].HeaderText = "Net à payer";
            if (customersDataGridView.Columns["CommitmentsBalanceDue"] != null)
                customersDataGridView.Columns["CommitmentsBalanceDue"].HeaderText = "Solde du";


        }


        /// <summary>
        /// Modifie la commande en fonction du chemin 
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
      
        /// <summary>
        /// Modifie la commande en fonction du chemin 
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void exportCustomersFilenameTextBox_TextChanged(object sender, EventArgs e)
        {
            exportCustomersCommandTextBox.Text = string.Format(CommandLinesHelper.ExportFactures, exportCustomersFilenameTextBox.Text);
        }
        #endregion

        private void ExportItemsForm_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void customersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
