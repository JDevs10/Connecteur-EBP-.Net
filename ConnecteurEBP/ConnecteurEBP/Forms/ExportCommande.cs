using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ConnecteurEBP.Utilities;
using System.Data.SqlClient;
using ConnecteurEBP.Classes;
using ProgressBarExample;
using System.Threading;
using System.IO;

namespace ConnecteurEBP.Forms
{
    public partial class ExportCommande : Form
    {
        public ExportCommande()
        {
            InitializeComponent();
        }

        private DocumentVente documentvente;

        private List<DocumentVente> GetCommandeFromDataBase()
        {
            //DocumentVente Commande = new DocumentVente();
            List<DocumentVente> listCommandes = new List<DocumentVente>();
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.getListCommandes(), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DocumentVente Commande = new DocumentVente(reader["Id"].ToString(), reader["DocumentNumber"].ToString(), reader["NumberPrefix"].ToString(), reader["NumberSuffix"].ToString(), reader["DocumentDate"].ToString().Substring(0, 10),
                                    reader["DeliveryDate"].ToString(), reader["InvoicingAddress_Address1"].ToString(), reader["InvoicingAddress_Address2"].ToString(), reader["InvoicingAddress_Address3"].ToString(),
                                    reader["InvoicingAddress_Address4"].ToString(), reader["InvoicingAddress_ZipCode"].ToString(), reader["InvoicingAddress_City"].ToString(), reader["InvoicingAddress_CountryIsoCode"].ToString(),
                                    reader["DeliveryAddress_Address1"].ToString(), reader["DeliveryAddress_Address2"].ToString(), reader["DeliveryAddress_Address3"].ToString(), reader["DeliveryAddress_Address4"].ToString(),
                                    reader["DeliveryAddress_ZipCode"].ToString(), reader["DeliveryAddress_City"].ToString(), reader["DeliveryAddress_CountryIsoCode"].ToString(), reader["CommitmentsBalanceDue"].ToString(),
                                    reader["AmountVatExcluded"].ToString(), reader["DiscountRate"].ToString(), reader["DiscountAmount"].ToString(), reader["AmountVatExcludedWithDiscount"].ToString(),
                                    reader["AmountVatExcludedWithDiscountAndShipping"].ToString().Replace(",",".").Replace("000000",""), reader["AmountVatExcludedWithDiscountAndShippingWithoutEcotax"].ToString(), reader["VatAmount"].ToString(), reader["AmountVatIncluded"].ToString().Replace(",",".").Replace("000000",""),
                                    reader["DepositAmount"].ToString(), reader["DepositCurrencyAmount"].ToString(), reader["TotalDueAmount"].ToString(), reader["DetailVatAmount0_DetailVatRate"].ToString(),
                                    reader["DetailVatAmount0_DetailAmountVatExcluded"].ToString(), reader["PaymentTypeId"].ToString(), reader["BankId"].ToString(), reader["FinancialDiscountType"].ToString(),
                                    reader["FinancialDiscountRate"].ToString(), reader["FinancialDiscountAmount"].ToString(), reader["CurrencyId"].ToString(), reader["IntrastatTransportMode"].ToString(), reader["CustomerId"].ToString(),
                                    reader["CustomerName"].ToString(), reader["DocumentType"].ToString(), reader["OriginDocumentType"].ToString(), reader["TransferedDocumentId"].ToString(),
                                    reader["InvoicingContact_Name"].ToString(), reader["InvoicingContact_FirstName"].ToString(),
                                    reader["InvoicingContact_Phone"].ToString(), reader["InvoicingContact_CellPhone"].ToString(), reader["InvoicingContact_Fax"].ToString(), reader["InvoicingContact_Email"].ToString(),
                                    reader["InvoicingContact_Function"].ToString(), reader["DeliveryContact_Name"].ToString(), reader["DeliveryContact_FirstName"].ToString(), reader["DeliveryContact_Phone"].ToString(),
                                    reader["DeliveryContact_CellPhone"].ToString(), reader["DeliveryContact_Fax"].ToString(), reader["DeliveryContact_Email"].ToString(), reader["DeliveryContact_Function"].ToString(),
                                    reader["DetailTaxAmount0_TaxCalculationBase"].ToString(), reader["DetailTaxAmount0_BaseAmount"].ToString(), reader["DetailTaxAmount0_TaxAmount"].ToString(), reader["DetailTaxAmount0_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount1_TaxCalculationBase"].ToString(), reader["DetailTaxAmount1_BaseAmount"].ToString(), reader["DetailTaxAmount1_TaxAmount"].ToString(), reader["DetailTaxAmount1_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount2_TaxCalculationBase"].ToString(), reader["DetailTaxAmount2_BaseAmount"].ToString(), reader["DetailTaxAmount2_TaxAmount"].ToString(), reader["DetailTaxAmount2_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount3_TaxCalculationBase"].ToString(), reader["DetailTaxAmount3_BaseAmount"].ToString(), reader["DetailTaxAmount3_TaxAmount"].ToString(), reader["DetailTaxAmount3_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount4_TaxCalculationBase"].ToString(), reader["DetailTaxAmount4_BaseAmount"].ToString(), reader["DetailTaxAmount4_TaxAmount"].ToString(), reader["DetailTaxAmount4_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount5_TaxCalculationBase"].ToString(), reader["DetailTaxAmount5_BaseAmount"].ToString(), reader["DetailTaxAmount5_TaxAmount"].ToString(), reader["DetailTaxAmount5_TaxCaption"].ToString(),
                                     reader["reference"].ToString()


                                    );
                                listCommandes.Add(Commande);
                            }
                        }
                    }
                    return listCommandes;
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {

                textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                // Initialize the dialog that will contain the progress bar
                ProgressDialog progressDialog = new ProgressDialog();

                // Initialize the thread that will handle the background process
                Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        // Set the flag that indicates if a process is currently running
                        //isProcessRunning = true;
                        for (int n = 0; n < 25; n++)
                        {
                            Thread.Sleep(1);
                            progressDialog.UpdateProgress(n);
                        }


                       // Customer customer = ListCommandesDataGridView.SelectedRows[0].DataBoundItem as Customer;
                        if (ListCommandesDataGridView.InvokeRequired)
                        {
                            ListCommandesDataGridView.Invoke(new MethodInvoker(delegate
                            {
                                ListCommandesDataGridView.DataSource = GetCommandeFromDataBase();

                                for (int n = 46; n < 75; n++)
                                {
                                    Thread.Sleep(1);
                                    progressDialog.UpdateProgress(n);
                                }

                                importButton.Enabled = ListCommandesDataGridView.Rows.Count > 0;
                                ListCommandesDataGridView.Columns["DocumentDate"].HeaderText = "Date de Commande";
                                ListCommandesDataGridView.Columns["DocumentNumber"].HeaderText = "N° document";
                                ListCommandesDataGridView.Columns["CustomerId"].HeaderText = "Code client";
                                ListCommandesDataGridView.Columns["DeliveryDate"].Visible = false;
                                ListCommandesDataGridView.Columns["PaymentTypeId"].Visible = false;
                                ListCommandesDataGridView.Columns["CustomerName"].Visible = false;
                                ListCommandesDataGridView.Columns["BankId"].Visible = false;
                                ListCommandesDataGridView.Columns["FinancialDiscountType"].Visible = false;
                                ListCommandesDataGridView.Columns["FinancialDiscountRate"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailVatAmount0_DetailAmountVatExcluded"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailVatAmount0_DetailAmountVatExcluded"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailVatAmount0_DetailVatRate"].Visible = false;
                                ListCommandesDataGridView.Columns["TotalDueAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DepositCurrencyAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DepositAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["AmountVatIncluded"].HeaderText = "Montant TTC";
                                ListCommandesDataGridView.Columns["VatAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["AmountVatExcludedWithDiscountAndShippingWithoutEcotax"].Visible = false;
                                ListCommandesDataGridView.Columns["AmountVatExcludedWithDiscountAndShipping"].HeaderText = "Montant Total HT";
                                ListCommandesDataGridView.Columns["AmountVatExcludedWithDiscount"].Visible = false;
                                ListCommandesDataGridView.Columns["DiscountAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DiscountRate"].Visible = false;
                                ListCommandesDataGridView.Columns["AmountVatExcluded"].Visible = false;
                                ListCommandesDataGridView.Columns["CommitmentsBalanceDue"].Visible = false;
                                ListCommandesDataGridView.Columns["codePays_livraison"].Visible = false;
                                ListCommandesDataGridView.Columns["Ville_livraison"].Visible = false;
                                ListCommandesDataGridView.Columns["Codepostal_livraison"].Visible = false;
                                ListCommandesDataGridView.Columns["Adresse4_livraison"].Visible = false;

                                for (int n = 76; n < 90; n++)
                                {
                                    Thread.Sleep(1);
                                    progressDialog.UpdateProgress(n);
                                }

                                ListCommandesDataGridView.Columns["Adresse3_livraison"].Visible = false;
                                ListCommandesDataGridView.Columns["Adresse2_livraison"].Visible = false;
                                ListCommandesDataGridView.Columns["Adresse1_livraison"].Visible = false;
                                ListCommandesDataGridView.Columns["codePays_facturation"].Visible = false;
                                ListCommandesDataGridView.Columns["Ville_facturation"].Visible = false;
                                ListCommandesDataGridView.Columns["Codepostal_facturation"].Visible = false;
                                ListCommandesDataGridView.Columns["Adresse4_facturation"].Visible = false;
                                ListCommandesDataGridView.Columns["Adresse3_facturation"].Visible = false;
                                ListCommandesDataGridView.Columns["Adresse2_facturation"].Visible = false;
                                ListCommandesDataGridView.Columns["Adresse1_facturation"].Visible = false;
                                ListCommandesDataGridView.Columns["Id"].Visible = false;
                                ListCommandesDataGridView.Columns["NumberSuffix"].Visible = false;
                                ListCommandesDataGridView.Columns["NumberPrefix"].Visible = false;
                                ListCommandesDataGridView.Columns["FinancialDiscountAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["CurrencyId"].Visible = false;
                                ListCommandesDataGridView.Columns["IntrastatTransportMode"].Visible = false;
                                ListCommandesDataGridView.Columns["DocumentType"].Visible = false;
                                ListCommandesDataGridView.Columns["OriginDocumentType"].Visible = false;
                                ListCommandesDataGridView.Columns["TransferedDocumentId"].Visible = false;
                                ListCommandesDataGridView.Columns["InvoicingContact_Name"].Visible = false;
                                ListCommandesDataGridView.Columns["InvoicingContact_FirstName"].Visible = false;
                                ListCommandesDataGridView.Columns["InvoicingContact_Phone"].Visible = false;
                                ListCommandesDataGridView.Columns["InvoicingContact_CellPhone"].Visible = false;
                                ListCommandesDataGridView.Columns["InvoicingContact_Fax"].Visible = false;
                                ListCommandesDataGridView.Columns["InvoicingContact_Email"].Visible = false;
                                ListCommandesDataGridView.Columns["InvoicingContact_Function"].Visible = false;
                                ListCommandesDataGridView.Columns["DeliveryContact_Name"].Visible = false;
                                ListCommandesDataGridView.Columns["DeliveryContact_FirstName"].Visible = false;
                                ListCommandesDataGridView.Columns["DeliveryContact_Phone"].Visible = false;
                                ListCommandesDataGridView.Columns["DeliveryContact_CellPhone"].Visible = false;
                                ListCommandesDataGridView.Columns["DeliveryContact_Fax"].Visible = false;
                                ListCommandesDataGridView.Columns["DeliveryContact_Email"].Visible = false;
                                ListCommandesDataGridView.Columns["DeliveryContact_Function"].Visible = false;

                                ListCommandesDataGridView.Columns["DetailTaxAmount0_TaxCalculationBase"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount0_BaseAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount0_TaxAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount0_TaxCaption"].Visible = false;

                                ListCommandesDataGridView.Columns["DetailTaxAmount1_TaxCalculationBase"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount1_BaseAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount1_TaxAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount1_TaxCaption"].Visible = false;

                                ListCommandesDataGridView.Columns["DetailTaxAmount2_TaxCalculationBase"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount2_BaseAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount2_TaxAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount2_TaxCaption"].Visible = false;

                                ListCommandesDataGridView.Columns["DetailTaxAmount3_TaxCalculationBase"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount3_BaseAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount3_TaxAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount3_TaxCaption"].Visible = false;

                                ListCommandesDataGridView.Columns["DetailTaxAmount4_TaxCalculationBase"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount4_BaseAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount4_TaxAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount4_TaxCaption"].Visible = false;

                                ListCommandesDataGridView.Columns["DetailTaxAmount5_TaxCalculationBase"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount5_BaseAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount5_TaxAmount"].Visible = false;
                                ListCommandesDataGridView.Columns["DetailTaxAmount5_TaxCaption"].Visible = false;
                            }));
                        }

                        for (int n = 91; n < 100; n++)
                        {
                            Thread.Sleep(1);
                            progressDialog.UpdateProgress(n);
                        }

                        // Close the dialog if it hasn't been already
                        if (progressDialog.InvokeRequired)
                            progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

                        // Reset the flag that indicates if a process is currently running
                        //isProcessRunning = false;
                    }
                ));

                // Start the background process thread
                backgroundThread.Start();

                // Open the dialog
                progressDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ListCommandesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (ListCommandesDataGridView.SelectedRows.Count == 0)
            {
                importButton.Enabled = false;
                return;
            }
            
            documentvente = ListCommandesDataGridView.SelectedRows[0].DataBoundItem as DocumentVente;
            
            //if (order == null)
            //    throw new NullReferenceException("order");

            //CommandeAExporter = order;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            folderDlg.ShowNewFolderButton = true;

            // Show the FolderBrowserDialog.

            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {

                textBox1.Text = folderDlg.SelectedPath;

                //Environment.SpecialFolder root = folderDlg.RootFolder;

            }
        }

        private List<DocumentVenteLine> getDocumentLine(string id)
        {
            List<DocumentVenteLine> listLines = new List<DocumentVenteLine>();

            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.getDocumentLine(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())

                                listLines.Add(new DocumentVenteLine(reader["LineOrder"].ToString(), reader["DescriptionClear"].ToString(),
                                    reader["ItemId"].ToString(), reader["Quantity"].ToString(), reader["RealQuantity"].ToString().Substring(0, 10),
                                    reader["PurchasePrice"].ToString(),
                                    reader["CostPrice"].ToString(), reader["TotalDiscountRate"].ToString(), reader["NetPriceVatExcluded"].ToString(),
                                    reader["NetPriceVatIncluded"].ToString(), reader["NetPriceVatExcludedWithDiscount"].ToString(),
                                    reader["NetPriceVatIncludedWithDiscount"].ToString(), reader["NetAmountVatExcluded"].ToString(),
                                    reader["NetAmountVatExcludedWithDiscount"].ToString(), reader["NetAmountVatIncluded"].ToString(),
                                    reader["NetAmountVatIncludedWithDiscount"].ToString(), reader["VatId"].ToString(),
                                    reader["OrderedQuantity"].ToString(), reader["VatAmount"].ToString(), reader["DeliveryDate"].ToString(), reader["DeliveredQuantity"].ToString(),
                                    reader["NetWeight"].ToString(), reader["TotalNetWeight"].ToString(), reader["RealNetAmountVatExcluded"].ToString(),
                                    reader["RealNetAmountVatExcludedWithDiscount"].ToString(), reader["RealNetAmountVatIncluded"].ToString(),
                                    reader["RealNetAmountVatIncludedWithDiscount"].ToString(),
                                    reader["RealNetAmountVatExcludedWithDiscountAndFinancialDiscount"].ToString(),
                                    reader["RealNetAmountVatIncludedWithDiscountAndFinancialDiscount"].ToString(), reader["SalePriceVatExcluded"].ToString(),
                                    reader["SalePriceVatIncluded"].ToString(), reader["TrackingNumber"].ToString(), reader["Volume"].ToString(), reader["TotalVolume"].ToString(),

                                    reader["Discounts0_UnitDiscountRate"].ToString(), reader["Discounts0_UnitDiscountAmountVatExcluded"].ToString(), reader["Discounts0_DiscountType"].ToString(),
                                    reader["Discounts1_UnitDiscountRate"].ToString(), reader["Discounts1_UnitDiscountAmountVatExcluded"].ToString(), reader["Discounts1_DiscountType"].ToString(),
                                    reader["Discounts2_UnitDiscountRate"].ToString(), reader["Discounts2_UnitDiscountAmountVatExcluded"].ToString(), reader["Discounts2_DiscountType"].ToString(),
                                    reader["OtherTaxes0_TaxValue"].ToString(), reader["OtherTaxes0_TaxAmount"].ToString(),
                                    reader["OtherTaxes1_TaxValue"].ToString(), reader["OtherTaxes1_TaxAmount"].ToString(),
                                    reader["OtherTaxes2_TaxValue"].ToString(), reader["OtherTaxes2_TaxAmount"].ToString(),
                                    reader["NumberOfItemByPackage"].ToString()
                                    ));



                            return new List<DocumentVenteLine>(listLines);
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return new List<DocumentVenteLine>();
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                    return new List<DocumentVenteLine>();
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    MessageBox.Show(e.Message);
                    return new List<DocumentVenteLine>();
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return new List<DocumentVenteLine>();
                }
            }
        }

        private string GetEANClient(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.returnEANClient(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                return reader[0] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return null;
                    }
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

        public static string ConvertDate(string date)
        {
            if (date.Length == 10 || date.Length == 19)
            {
                return date.Substring(6, 4) + date.Substring(3, 2) + date.Substring(0, 2);
            }
            return date;
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Le chemin du fichier d'import de commande doit être renseigné");
                    return;
                }

                string EANClient = GetEANClient(documentvente.CustomerId);


                var fileName = string.Format("EDI_ORDERS." + EANClient + "." + documentvente.DocumentNumber + (documentvente.reference == "" ? "" : ".") + documentvente.reference.Replace("Document importé n° ", "").Replace(" (prix article différent)", "").Replace("/", "") + "." + ConvertDate(documentvente.DocumentDate.Replace("00:00:00", "")) + "." + documentvente.Ville_livraison + ".{0:yyyyMMddhhmmss}.csv", DateTime.Now);

                fileName = fileName.Replace("...", ".");

                using (StreamWriter writer = new StreamWriter(textBox1.Text + @"\" + fileName.Replace("..", "."), false, Encoding.UTF8))
                {
                    //if (CommandeAExporter.deviseCommande == "0")
                    //{
                    //    CommandeAExporter.deviseCommande = "";
                    //}

                    //if (CommandeAExporter.deviseCommande != "")
                    //{
                    //    CommandeAExporter.deviseCommande = getDeviseIso(CommandeAExporter.deviseCommande);
                    //}

                    string adresseDeLivraison = documentvente.Adresse1_livraison + "*" + documentvente.Adresse2_livraison + "*" + documentvente.Adresse3_livraison + "*" + documentvente.Adresse4_livraison + "*" + documentvente.Codepostal_livraison + "*" + documentvente.Ville_livraison + "*" + documentvente.codePays_livraison;

                    //adresseDeLivraison = adresseDeLivraison.Replace("...",".");

                    writer.WriteLine("ORDERS;" + documentvente.DocumentNumber + (documentvente.reference == "" ? "":" - ") + documentvente.reference.Replace("Document importé n° ", "").Replace(" (prix article différent)", "") + ";" + EANClient + ";" + EANClient + ";;;;" + adresseDeLivraison + ";" + documentvente.CurrencyId + ";;");

                    //if (CommandeAExporter.DateCommande != "")
                    //{
                    //    CommandeAExporter.DateCommande = ConvertDate(CommandeAExporter.DateCommande);
                    //}

                    //if (CommandeAExporter.DateCommande != " ")
                    //{
                    //    CommandeAExporter.conditionLivraison = "";
                    //}

                    writer.WriteLine("ORDHD1;" + ConvertDate(documentvente.DocumentDate.Replace("00:00:00", "")) + ";;" + documentvente.PaymentTypeId + ";");

                    documentvente.lines = getDocumentLine(documentvente.Id);

                    for (int i = 0; i < documentvente.lines.Count; i++)
                    {
                        string EANArticle = GetEANArticle(documentvente.lines[i].ItemId);

                        writer.WriteLine("ORDLIN;" + documentvente.lines[i].LineOrder + ";" + EANArticle + ";GS1;;;;;" + documentvente.lines[i].DescriptionClear + ";" + documentvente.lines[i].Quantity.Replace(",", ".").Replace("00000", "") + ";LM;" + documentvente.lines[i].RealNetAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";;;" + documentvente.lines[i].NetPriceVatExcluded.Replace(",", ".").Replace("00000", "") + ";;;LM;;;;" + ConvertDate(documentvente.lines[i].DeliveryDate.Replace(" 00:00:00", "")) + ";");
                    }
                    writer.WriteLine("ORDEND;" + documentvente.AmountVatExcludedWithDiscountAndShipping.Replace(",", ".").Replace("00000", "") + ";");



                }

                MessageBox.Show("Commande exportée avec succés", "Information !!",
                                             MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Close();



            }
            catch (Exception ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show(ex.Message);
            }

        }

        private string GetEANArticle(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.returnEANArticle(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                return reader[0] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return null;
                    }
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

    }
}
