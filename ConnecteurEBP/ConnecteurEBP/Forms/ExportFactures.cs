using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ConnecteurEBP.Classes;
using ConnecteurEBP.Utilities;
using System.Text;
using System.Threading;
using ProgressBarExample;

namespace ConnecteurEBP.Forms
{
    public partial class ExportFactures : Form
    {
      #region Champs privés
        /// <summary>
        /// Chemin du fichier d'import des commandes
        /// </summary>
        private string ordersFilename = string.Format(@"{0}\Orders.txt", Path.GetTempPath());
        private List<DocumentVente> FacturesAExporter;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Création d'une instance de ImportOrdersForm
        /// </summary>
        public ExportFactures()
        {
            InitializeComponent();
        }
        #endregion

        #region Intéractions avec l'application
        /// <summary>
        /// Récupération des articles du dossier
        /// </summary>
        /// <returns>Retourne la liste des articles</returns>
        private List<Item> GetItemsFromDatabase()
        {
            List<Item> items = new List<Item>();
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.Items_SQLServer, sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader["Id"] as string;
                                string label = reader["Caption"] as string;
                                decimal? price = reader["SalePriceVatIncluded"] as decimal?;
                                if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(label) || !price.HasValue)
                                    throw new SDKException("Une erreur est survenue lors du chargement des articles");
                                items.Add(new Item(id, label, null, null, price));
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

        private List<DocumentVente> GetFactureFromDataBase(string codeClient)
        {
            //DocumentVente Facture = new DocumentVente();
            List<DocumentVente> listFactures = new List<DocumentVente>();
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.getFactureClient(codeClient), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                           while(reader.Read())
                            {
                                DocumentVente Facture = new DocumentVente(reader["Id"].ToString(), reader["DocumentNumber"].ToString(), reader["NumberPrefix"].ToString(), reader["NumberSuffix"].ToString(), reader["DocumentDate"].ToString().Substring(0,10),
                                    reader["DeliveryDate"].ToString(),reader["InvoicingAddress_Address1"].ToString(), reader["InvoicingAddress_Address2"].ToString(), reader["InvoicingAddress_Address3"].ToString(),
                                    reader["InvoicingAddress_Address4"].ToString(), reader["InvoicingAddress_ZipCode"].ToString(), reader["InvoicingAddress_City"].ToString(), reader["InvoicingAddress_CountryIsoCode"].ToString(),
                                    reader["DeliveryAddress_Address1"].ToString(), reader["DeliveryAddress_Address2"].ToString(), reader["DeliveryAddress_Address3"].ToString(), reader["DeliveryAddress_Address4"].ToString(),
                                    reader["DeliveryAddress_ZipCode"].ToString(), reader["DeliveryAddress_City"].ToString(), reader["DeliveryAddress_CountryIsoCode"].ToString(), reader["CommitmentsBalanceDue"].ToString(),
                                    reader["AmountVatExcluded"].ToString(), reader["DiscountRate"].ToString(), reader["DiscountAmount"].ToString(), reader["AmountVatExcludedWithDiscount"].ToString(),
                                    reader["AmountVatExcludedWithDiscountAndShipping"].ToString(), reader["AmountVatExcludedWithDiscountAndShippingWithoutEcotax"].ToString(), reader["VatAmount"].ToString(), reader["AmountVatIncluded"].ToString(),
                                    reader["DepositAmount"].ToString(), reader["DepositCurrencyAmount"].ToString(), reader["TotalDueAmount"].ToString(), reader["DetailVatAmount0_DetailVatRate"].ToString(),
                                    reader["DetailVatAmount0_DetailAmountVatExcluded"].ToString(), reader["PaymentTypeId"].ToString(), reader["BankId"].ToString(), reader["FinancialDiscountType"].ToString(),
                                    reader["FinancialDiscountRate"].ToString(), reader["FinancialDiscountAmount"].ToString(), reader["CurrencyId"].ToString(), reader["IntrastatTransportMode"].ToString(), reader["CustomerId"].ToString(),
                                    reader["CustomerName"].ToString(), reader["DocumentType"].ToString(), reader["OriginDocumentType"].ToString(), reader["TransferedDocumentId"].ToString(),
                                    reader["InvoicingContact_Name"].ToString(), reader["InvoicingContact_FirstName"].ToString(),
                                    reader["InvoicingContact_Phone"].ToString(), reader["InvoicingContact_CellPhone"].ToString(), reader["InvoicingContact_Fax"].ToString(), reader["InvoicingContact_Email"].ToString(),
                                    reader["InvoicingContact_Function"].ToString(), reader["DeliveryContact_Name"].ToString(), reader["DeliveryContact_FirstName"].ToString(), reader["DeliveryContact_Phone"].ToString(),
                                    reader["DeliveryContact_CellPhone"].ToString(), reader["DeliveryContact_Fax"].ToString(), reader["DeliveryContact_Email"].ToString(), reader["DeliveryContact_Function"].ToString(),
                                    reader["DetailTaxAmount0_TaxCalculationBase"].ToString(), reader["DetailTaxAmount0_BaseAmount"].ToString(),reader["DetailTaxAmount0_TaxAmount"].ToString(), reader["DetailTaxAmount0_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount1_TaxCalculationBase"].ToString(), reader["DetailTaxAmount1_BaseAmount"].ToString(),reader["DetailTaxAmount1_TaxAmount"].ToString(), reader["DetailTaxAmount1_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount2_TaxCalculationBase"].ToString(), reader["DetailTaxAmount2_BaseAmount"].ToString(),reader["DetailTaxAmount2_TaxAmount"].ToString(), reader["DetailTaxAmount2_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount3_TaxCalculationBase"].ToString(), reader["DetailTaxAmount3_BaseAmount"].ToString(),reader["DetailTaxAmount3_TaxAmount"].ToString(), reader["DetailTaxAmount3_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount4_TaxCalculationBase"].ToString(), reader["DetailTaxAmount4_BaseAmount"].ToString(),reader["DetailTaxAmount4_TaxAmount"].ToString(), reader["DetailTaxAmount4_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount5_TaxCalculationBase"].ToString(), reader["DetailTaxAmount5_BaseAmount"].ToString(),reader["DetailTaxAmount5_TaxAmount"].ToString(), reader["DetailTaxAmount5_TaxCaption"].ToString(),
                                     reader["reference"].ToString()
                                     
                                    
                                    );  
                                    listFactures.Add(Facture);
                            }
                        }
                    }
                    return listFactures;
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
        /// Récupération des clients du dossier
        /// </summary>
        /// <returns>Retourne la liste des clients</returns>
        private List<Customer> GetCustomersFromDatabase()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer les clients du dossier
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.Customers_SQLServer, sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader["Id"] as string;
                                string name = reader["Name"] as string;
                                string address = reader["Address"] as string;
                                string zipCode = reader["ZipCode"] as string;
                                string city = reader["City"] as string;
                                string country = reader["Country"] as string;
                                customers.Add(new Customer()
                                {
                                    Id = id,
                                    Name = name,
                                    Address = address,
                                    ZipCode = zipCode,
                                    City = city,
                                    Country = country
                                });
                            }
                        }
                        return customers;
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

        /// <summary>
        /// Récupération du prochain identifiant de commande
        /// </summary>
        /// <returns>Retourne le prochain identifiant</returns>
        private string GetNextOrderId()
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.NextOrderId_SQLServer, sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader["incValue"] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return "00000001";
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
                        return null ;
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

        private string GetCommandeFacture(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.getCommandeFacture(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                                return reader[0].ToString() + ";" + reader[1].ToString() + ";" + reader[2].ToString();
                            else
                                return null;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
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

        public static string addZero(string date)
        {
            if (date.Length == 1)
            {
                return "0"+date;
            }
            return date;
        }

        public static string ConvertDate(string date)
        {
            if (date.Length == 10 || date.Length == 19)
            {
                return date.Substring(6, 4) + date.Substring(3, 2) + date.Substring(0, 2);
            }
            return date;
        }

        /// <summary>
        /// Génération du fichier d'export, lancement de l'application et exporter les factures
        /// /// </summary>
        private void ExportFacture()
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Le chemin du fichier d'import de commande doit être renseigné");
                    return;
                }

                var fileName = string.Format("FACTURE_{0:ddMMyyyy_HH.mm.ss}.csv", DateTime.Now);

                using (StreamWriter writer = new StreamWriter(textBox1.Text+@"\"+fileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("DEMAT-AAA;v01.0;;;" + DateTime.Today.Year + addZero(DateTime.Today.Month.ToString()) + addZero(DateTime.Today.Day.ToString()) + ";;");
                    ////writer.WriteLine("");
                    ////writer.WriteLine("");

                    for (int i = 0; i < FacturesAExporter.Count; i++)
                    {
                        string EANClient = GetEANClient(FacturesAExporter[i].CustomerId);

                        string[] tab = new string[]{"","",""};

                      
                        // TYPE du document original exemple : facture  --origin--> bon de commande 
                        if(FacturesAExporter[i].OriginDocumentType == "8")
                        {                             
                          tab = GetCommandeFacture(FacturesAExporter[i].Id).Split(';');
                        }


                        writer.WriteLine("DEMAT-HD1;v01.0;;" + FacturesAExporter[i].DocumentNumber.Replace(FacturesAExporter[i].NumberPrefix, "") + ";380;9;" + ConvertDate(FacturesAExporter[i].DocumentDate) + ";" + ConvertDate(FacturesAExporter[i].DeliveryDate) + ";;;;;" + FacturesAExporter[i].PaymentTypeId + ";;;;0;;" + tab[1].Trim() + ";" + ConvertDate(tab[2]) + ";;;;;;;;;;;;;;;;;;;;;;;;;" + FacturesAExporter[i].CurrencyId + ";" + FacturesAExporter[i].CurrencyId + ";;;" + FacturesAExporter[i].FinancialDiscountRate.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].FinancialDiscountAmount.Replace(",", ".").Replace("00000", "") + ";;;;;;;;;;;;;");
                        ////writer.WriteLine("");

                        writer.WriteLine("DEMAT-HD2;" + EANClient + ";;" + FacturesAExporter[i].Adresse1_facturation + ";" + FacturesAExporter[i].Codepostal_facturation + ";" + FacturesAExporter[i].Ville_facturation + ";" + FacturesAExporter[i].codePays_facturation + ";" + EANClient + ";;" + FacturesAExporter[i].Adresse1_facturation + ";" + FacturesAExporter[i].Codepostal_facturation + ";" + FacturesAExporter[i].Ville_facturation + ";" + FacturesAExporter[i].codePays_facturation + ";;;" + FacturesAExporter[i].Adresse1_facturation + ";" + FacturesAExporter[i].Codepostal_facturation + ";" + FacturesAExporter[i].Ville_facturation + ";" + FacturesAExporter[i].codePays_facturation + ";;;;;;;;;;;;;;;" + FacturesAExporter[i].Adresse1_livraison + ";" + FacturesAExporter[i].Codepostal_livraison + ";" + FacturesAExporter[i].Ville_livraison + ";" + FacturesAExporter[i].codePays_livraison + ";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
                        ////writer.WriteLine("");

                        writer.WriteLine("DEMAT-CTA;" + FacturesAExporter[i].InvoicingContact_Function + ";;" + FacturesAExporter[i].InvoicingContact_Name + " " + FacturesAExporter[i].InvoicingContact_FirstName + ";" + FacturesAExporter[i].InvoicingContact_Email + ";" + FacturesAExporter[i].InvoicingContact_Fax + ";" + FacturesAExporter[i].InvoicingContact_Phone + ";" + FacturesAExporter[i].InvoicingContact_Function + ";;" + FacturesAExporter[i].InvoicingContact_Name + " " + FacturesAExporter[i].InvoicingContact_FirstName + ";" + FacturesAExporter[i].InvoicingContact_Email + ";" + FacturesAExporter[i].InvoicingContact_Fax + ";" + FacturesAExporter[i].InvoicingContact_Phone + ";;;;;;;;;;;;;;;;;;;" + FacturesAExporter[i].DeliveryContact_Function + ";;" + FacturesAExporter[i].DeliveryContact_Name + " " + FacturesAExporter[i].DeliveryContact_FirstName + ";" + FacturesAExporter[i].DeliveryContact_Email + ";" + FacturesAExporter[i].DeliveryContact_Fax + ";" + FacturesAExporter[i].DeliveryContact_Phone + ";;;;;;;");
                        ////writer.WriteLine("");

                        writer.WriteLine("DEMAT-REF;;" + FacturesAExporter[i].reference.Replace("Document importé n° " , "" ).Replace(" (prix article différent)","") + ";");

                        if (FacturesAExporter[i].DiscountAmount != "0,00000000" || FacturesAExporter[i].DiscountAmount != "0,00000000")
                        {
                            writer.WriteLine("DEMAT-REM;;A;;;;;;;;" + FacturesAExporter[i].DiscountAmount.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].DiscountRate.Replace(",", ".").Replace("00000", "") + ";;");
                            ////writer.WriteLine("");
                        }

                        FacturesAExporter[i].lines = getDocumentLine(FacturesAExporter[i].Id);

                        for (int j = 0; j < FacturesAExporter[i].lines.Count; j++)
                        {
                            string EANArticle = GetEANArticle(FacturesAExporter[i].lines[j].ItemId);

                            writer.WriteLine("DEMAT-LIN;" + FacturesAExporter[i].lines[j].LineOrder + ";" + EANArticle + ";EAN;;;" + EANClient + ";;;" + FacturesAExporter[i].lines[j].TrackingNumber + ";" + FacturesAExporter[i].lines[j].DescriptionClear + ";;" + FacturesAExporter[i].lines[j].TotalNetWeight.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].TotalVolume.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Quantity.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].RealQuantity.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].OrderedQuantity.Replace(",", ".").Replace("00000", "") + ";;;;;" + FacturesAExporter[i].lines[j].RealNetAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].SalePriceVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].SalePriceVatExcluded.Replace(",", ".").Replace("00000", "") + ";;1;;;" + ConvertDate(FacturesAExporter[i].lines[j].DeliveryDate) + ";;;;;" + FacturesAExporter[i].lines[j].LineOrder + ";" + ConvertDate(tab[2]) + ";;;;;;;;" + FacturesAExporter[i].lines[j].RealNetAmountVatIncluded.Replace(",", ".").Replace("00000", "") + ";;;;;;;;");
                            ////writer.WriteLine("");

                            if (FacturesAExporter[i].lines[j].OtherTaxes0_TaxAmount != "0,00000000")
                            {
                                writer.WriteLine("DEMAT-TAX;1;;;" + FacturesAExporter[i].lines[j].OtherTaxes0_TaxValue.Replace(",", ".").Replace("00000", "") + ";;" + FacturesAExporter[i].lines[j].OtherTaxes0_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;;");
                                ////writer.WriteLine("");
                            }
                            if (FacturesAExporter[i].lines[j].OtherTaxes1_TaxAmount != "0,00000000")
                            {
                                writer.WriteLine("DEMAT-TAX;2;;;" + FacturesAExporter[i].lines[j].OtherTaxes1_TaxValue.Replace(",", ".").Replace("00000", "") + ";;" + FacturesAExporter[i].lines[j].OtherTaxes1_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;;");
                                ////writer.WriteLine("");
                            }

                            if (FacturesAExporter[i].lines[j].OtherTaxes2_TaxAmount != "0,00000000")
                            {
                                writer.WriteLine("DEMAT-TAX;3;;;" + FacturesAExporter[i].lines[j].OtherTaxes2_TaxValue.Replace(",", ".").Replace("00000", "") + ";;" + FacturesAExporter[i].lines[j].OtherTaxes2_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;;");
                                ////writer.WriteLine("");
                            }


                            if(FacturesAExporter[i].lines[j].Discounts0_UnitDiscountRate != "0,00000000")
                            {
                                writer.WriteLine("DEMAT-DED;;A;;;;;;;" + FacturesAExporter[i].lines[j].Discounts0_DiscountType.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts0_UnitDiscountAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts0_UnitDiscountRate.Replace(",", ".").Replace("00000", "") + ";;");
                                ////writer.WriteLine("");
                            }

                            
                            if(FacturesAExporter[i].lines[j].Discounts1_UnitDiscountRate != "0,00000000")
                            {
                                writer.WriteLine("DEMAT-DED;;A;;;;;;;" + FacturesAExporter[i].lines[j].Discounts1_DiscountType.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts1_UnitDiscountAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts1_UnitDiscountRate.Replace(",", ".").Replace("00000", "") + ";;");
                                ////writer.WriteLine("");
                            }


                            if (FacturesAExporter[i].lines[j].Discounts2_UnitDiscountRate != "0,00000000")
                            {
                                writer.WriteLine("DEMAT-DED;;A;;;;;;;" + FacturesAExporter[i].lines[j].Discounts2_DiscountType.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts2_UnitDiscountAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts2_UnitDiscountRate.Replace(",", ".").Replace("00000", "") + ";;");
                                ////writer.WriteLine("");
                            }
                        }

                        //  Les lignes des taxes
                        if (FacturesAExporter[i].DetailTaxAmount0_TaxAmount != "0,00000000")
                        {
                            writer.WriteLine("DEMAT-TTX;1;;;" + FacturesAExporter[i].DetailTaxAmount0_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount0_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                            ////writer.WriteLine("");
                        }
                        if (FacturesAExporter[i].DetailTaxAmount1_TaxAmount != "0,00000000")
                        {
                            writer.WriteLine("DEMAT-TTX;2;;;" + FacturesAExporter[i].DetailTaxAmount1_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount1_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                            ////writer.WriteLine("");
                        }
                        if (FacturesAExporter[i].DetailTaxAmount2_TaxAmount != "0,00000000")
                        {
                            writer.WriteLine("DEMAT-TTX;3;;;" + FacturesAExporter[i].DetailTaxAmount2_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount2_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                            ////writer.WriteLine("");
                        }
                        if (FacturesAExporter[i].DetailTaxAmount3_TaxAmount != "0,00000000")
                        {
                            writer.WriteLine("DEMAT-TTX;4;;;" + FacturesAExporter[i].DetailTaxAmount3_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount3_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                            ////writer.WriteLine("");
                        }
                        if (FacturesAExporter[i].DetailTaxAmount4_TaxAmount != "0,00000000")
                        {
                            writer.WriteLine("DEMAT-TTX;5;;;" + FacturesAExporter[i].DetailTaxAmount4_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount4_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                            ////writer.WriteLine("");
                        }
                        if (FacturesAExporter[i].DetailTaxAmount5_TaxAmount != "0,00000000")
                        {
                            writer.WriteLine("DEMAT-TTX;6;;;" + FacturesAExporter[i].DetailTaxAmount5_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount5_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                            ////writer.WriteLine("");
                        }

                        writer.WriteLine("DEMAT-END;;;" + FacturesAExporter[i].DocumentNumber.Replace(FacturesAExporter[i].NumberPrefix, "") + ";" + FacturesAExporter[i].AmountVatExcludedWithDiscountAndShipping.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].AmountVatIncluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].VatAmount.Replace(",", ".").Replace("00000", "") + ";;;;" + FacturesAExporter[i].FinancialDiscountAmount.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].DepositAmount.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].TotalDueAmount.Replace(",", ".").Replace("00000", "") + ";;;;");
                        ////writer.WriteLine("");
                        ////writer.WriteLine("");
                    }
                    
                    writer.WriteLine("DEMAT-ZZZ;v01.0;;;;");
                    
                    
                }

                MessageBox.Show("Nombre de facture : " + FacturesAExporter.Count , "Information !!",
                                             MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Close();

                
                
            }
            catch (Exception ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Méthodes diverses
        /// <summary>
        /// Chargement de la fenêtre
        /// </summary>
        /// <param name="e">paramètres de l'évènement</param>
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

                        //Affichage des clients du dossier
                        if (customersDataGridView.InvokeRequired)
                        {
                            customersDataGridView.Invoke(new MethodInvoker(delegate
                            {
                                customersDataGridView.DataSource = GetCustomersFromDatabase();
                                for (int n = 26; n < 45; n++)
                                {
                                    Thread.Sleep(1);
                                    progressDialog.UpdateProgress(n);
                                }
                                importButton.Enabled = customersDataGridView.Rows.Count > 0;
                                //Formatage de la grille des clients
                                if (customersDataGridView.Columns["Id"] != null)
                                    customersDataGridView.Columns["Id"].HeaderText = "Identifiant";
                                if (customersDataGridView.Columns["Name"] != null)
                                    customersDataGridView.Columns["Name"].HeaderText = "Nom";
                                if (customersDataGridView.Columns["Address"] != null)
                                    customersDataGridView.Columns["Address"].Visible = false;
                                if (customersDataGridView.Columns["ZipCode"] != null)
                                    customersDataGridView.Columns["ZipCode"].Visible = false;
                                if (customersDataGridView.Columns["City"] != null)
                                    customersDataGridView.Columns["City"].Visible = false;
                                if (customersDataGridView.Columns["Country"] != null)
                                    customersDataGridView.Columns["Country"].Visible = false;
                                if (customersDataGridView.Columns["Sales"] != null)
                                    customersDataGridView.Columns["Sales"].Visible = false;
                                //Récupération du prochain identifiant de commande à utiliser
                                //string nextOrderId = GetNextOrderId();
                            }));
                        }

                        for (int n = 46; n < 75; n++)
                        {
                            Thread.Sleep(1);
                            progressDialog.UpdateProgress(n);
                        }

                        Customer customer = customersDataGridView.SelectedRows[0].DataBoundItem as Customer;
                        if (itemsDataGridView.InvokeRequired)
                        {
                            itemsDataGridView.Invoke(new MethodInvoker(delegate
                            {
                                itemsDataGridView.DataSource = GetFactureFromDataBase(customer.Id);

                                for (int n = 76; n < 90; n++)
                                {
                                    Thread.Sleep(1);
                                    progressDialog.UpdateProgress(n);
                                }

                                importButton.Enabled = itemsDataGridView.Rows.Count > 0;
                                itemsDataGridView.Columns["DocumentDate"].HeaderText = "Date Facture";
                                itemsDataGridView.Columns["DocumentNumber"].HeaderText = "Numero de document";
                                itemsDataGridView.Columns["CustomerId"].HeaderText = "Code client";
                                itemsDataGridView.Columns["DeliveryDate"].Visible = false;
                                itemsDataGridView.Columns["PaymentTypeId"].Visible = false;
                                itemsDataGridView.Columns["CustomerName"].Visible = false;
                                itemsDataGridView.Columns["BankId"].Visible = false;
                                itemsDataGridView.Columns["FinancialDiscountType"].Visible = false;
                                itemsDataGridView.Columns["FinancialDiscountRate"].Visible = false;
                                itemsDataGridView.Columns["DetailVatAmount0_DetailAmountVatExcluded"].Visible = false;
                                itemsDataGridView.Columns["DetailVatAmount0_DetailAmountVatExcluded"].Visible = false;
                                itemsDataGridView.Columns["DetailVatAmount0_DetailVatRate"].Visible = false;
                                itemsDataGridView.Columns["TotalDueAmount"].Visible = false;
                                itemsDataGridView.Columns["DepositCurrencyAmount"].Visible = false;
                                itemsDataGridView.Columns["DepositAmount"].Visible = false;
                                itemsDataGridView.Columns["AmountVatIncluded"].Visible = false;
                                itemsDataGridView.Columns["VatAmount"].Visible = false;
                                itemsDataGridView.Columns["AmountVatExcludedWithDiscountAndShippingWithoutEcotax"].Visible = false;
                                itemsDataGridView.Columns["AmountVatExcludedWithDiscountAndShipping"].Visible = false;
                                itemsDataGridView.Columns["AmountVatExcludedWithDiscount"].Visible = false;
                                itemsDataGridView.Columns["DiscountAmount"].Visible = false;
                                itemsDataGridView.Columns["DiscountRate"].Visible = false;
                                itemsDataGridView.Columns["AmountVatExcluded"].Visible = false;
                                itemsDataGridView.Columns["CommitmentsBalanceDue"].Visible = false;
                                itemsDataGridView.Columns["codePays_livraison"].Visible = false;
                                itemsDataGridView.Columns["Ville_livraison"].Visible = false;
                                itemsDataGridView.Columns["Codepostal_livraison"].Visible = false;
                                itemsDataGridView.Columns["Adresse4_livraison"].Visible = false;
                                itemsDataGridView.Columns["Adresse3_livraison"].Visible = false;
                                itemsDataGridView.Columns["Adresse2_livraison"].Visible = false;
                                itemsDataGridView.Columns["Adresse1_livraison"].Visible = false;
                                itemsDataGridView.Columns["codePays_facturation"].Visible = false;
                                itemsDataGridView.Columns["Ville_facturation"].Visible = false;
                                itemsDataGridView.Columns["Codepostal_facturation"].Visible = false;
                                itemsDataGridView.Columns["Adresse4_facturation"].Visible = false;
                                itemsDataGridView.Columns["Adresse3_facturation"].Visible = false;
                                itemsDataGridView.Columns["Adresse2_facturation"].Visible = false;
                                itemsDataGridView.Columns["Adresse1_facturation"].Visible = false;
                                itemsDataGridView.Columns["Id"].Visible = false;
                                itemsDataGridView.Columns["NumberSuffix"].Visible = false;
                                itemsDataGridView.Columns["NumberPrefix"].Visible = false;
                                itemsDataGridView.Columns["FinancialDiscountAmount"].Visible = false;
                                itemsDataGridView.Columns["CurrencyId"].Visible = false;
                                itemsDataGridView.Columns["IntrastatTransportMode"].Visible = false;
                                itemsDataGridView.Columns["DocumentType"].Visible = false;
                                itemsDataGridView.Columns["OriginDocumentType"].Visible = false;
                                itemsDataGridView.Columns["TransferedDocumentId"].Visible = false;
                                itemsDataGridView.Columns["InvoicingContact_Name"].Visible = false;
                                itemsDataGridView.Columns["InvoicingContact_FirstName"].Visible = false;
                                itemsDataGridView.Columns["InvoicingContact_Phone"].Visible = false;
                                itemsDataGridView.Columns["InvoicingContact_CellPhone"].Visible = false;
                                itemsDataGridView.Columns["InvoicingContact_Fax"].Visible = false;
                                itemsDataGridView.Columns["InvoicingContact_Email"].Visible = false;
                                itemsDataGridView.Columns["InvoicingContact_Function"].Visible = false;
                                itemsDataGridView.Columns["DeliveryContact_Name"].Visible = false;
                                itemsDataGridView.Columns["DeliveryContact_FirstName"].Visible = false;
                                itemsDataGridView.Columns["DeliveryContact_Phone"].Visible = false;
                                itemsDataGridView.Columns["DeliveryContact_CellPhone"].Visible = false;
                                itemsDataGridView.Columns["DeliveryContact_Fax"].Visible = false;
                                itemsDataGridView.Columns["DeliveryContact_Email"].Visible = false;
                                itemsDataGridView.Columns["DeliveryContact_Function"].Visible = false;

                                itemsDataGridView.Columns["DetailTaxAmount0_TaxCalculationBase"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount0_BaseAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount0_TaxAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount0_TaxCaption"].Visible = false;

                                itemsDataGridView.Columns["DetailTaxAmount1_TaxCalculationBase"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount1_BaseAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount1_TaxAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount1_TaxCaption"].Visible = false;

                                itemsDataGridView.Columns["DetailTaxAmount2_TaxCalculationBase"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount2_BaseAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount2_TaxAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount2_TaxCaption"].Visible = false;

                                itemsDataGridView.Columns["DetailTaxAmount3_TaxCalculationBase"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount3_BaseAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount3_TaxAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount3_TaxCaption"].Visible = false;

                                itemsDataGridView.Columns["DetailTaxAmount4_TaxCalculationBase"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount4_BaseAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount4_TaxAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount4_TaxCaption"].Visible = false;

                                itemsDataGridView.Columns["DetailTaxAmount5_TaxCalculationBase"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount5_BaseAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount5_TaxAmount"].Visible = false;
                                itemsDataGridView.Columns["DetailTaxAmount5_TaxCaption"].Visible = false;
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

        /// <summary>
        /// Fermeture de la fenêtre
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Remplissage des infos d'adresse du client sélectionné
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void customersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (customersDataGridView.SelectedRows.Count == 0)
            {
                importButton.Enabled = false;
                return;
            }
            Customer customer = customersDataGridView.SelectedRows[0].DataBoundItem as Customer;
            if (customer == null)
                throw new NullReferenceException("customer");
            thirdIdTextBox.Text = customer.Id;
            thirdNameTextBox.Text = customer.Name;
            addressTextBox.Text = customer.Address;
            zipCodeTextBox.Text = customer.ZipCode;
            cityTextBox.Text = customer.City;
            countryTextBox.Text = customer.Country;
            itemsDataGridView.DataSource = GetFactureFromDataBase(customer.Id);
            importButton.Enabled = itemsDataGridView.Rows.Count > 0;
        }

        /// <summary>
        /// Remplissage des infos de lignes d'après les articles sélectionnés
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void itemsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            orderLinesDataGridView.DataSource = null;
            FacturesAExporter = new List<DocumentVente>();
            foreach (DataGridViewRow row in itemsDataGridView.SelectedRows)
            {
                DocumentVente item = row.DataBoundItem as DocumentVente;
                if (item == null)
                    throw new NullReferenceException("item");
                FacturesAExporter.Add(item);
            } 
            orderLinesDataGridView.DataSource = FacturesAExporter;
            importButton.Enabled = orderLinesDataGridView.Rows.Count > 0;
            orderLinesDataGridView.Columns["DocumentDate"].HeaderText = "Date Facture";
            orderLinesDataGridView.Columns["DocumentNumber"].HeaderText = "Numero de document";
            orderLinesDataGridView.Columns["CustomerName"].Visible = false;
            orderLinesDataGridView.Columns["AmountVatExcludedWithDiscountAndShippingWithoutEcotax"].HeaderText = "Montant total HT";
            orderLinesDataGridView.Columns["VatAmount"].HeaderText = "Montant TVA";
            orderLinesDataGridView.Columns["AmountVatIncluded"].HeaderText = "Montant TTC";


            orderLinesDataGridView.Columns["DeliveryDate"].Visible = false;
            orderLinesDataGridView.Columns["PaymentTypeId"].Visible = false;
            orderLinesDataGridView.Columns["CustomerId"].Visible = false;
            orderLinesDataGridView.Columns["BankId"].Visible = false;
            orderLinesDataGridView.Columns["FinancialDiscountType"].Visible = false;
            orderLinesDataGridView.Columns["FinancialDiscountRate"].Visible = false;
            orderLinesDataGridView.Columns["DetailVatAmount0_DetailAmountVatExcluded"].Visible = false;
            orderLinesDataGridView.Columns["DetailVatAmount0_DetailAmountVatExcluded"].Visible = false;
            orderLinesDataGridView.Columns["DetailVatAmount0_DetailVatRate"].Visible = false;
            orderLinesDataGridView.Columns["TotalDueAmount"].Visible = false;
            orderLinesDataGridView.Columns["DepositCurrencyAmount"].Visible = false;
            orderLinesDataGridView.Columns["DepositAmount"].Visible = false;       
            orderLinesDataGridView.Columns["AmountVatExcludedWithDiscountAndShipping"].Visible = false;
            orderLinesDataGridView.Columns["AmountVatExcludedWithDiscount"].Visible = false;
            orderLinesDataGridView.Columns["DiscountAmount"].Visible = false;
            orderLinesDataGridView.Columns["DiscountRate"].Visible = false;
            orderLinesDataGridView.Columns["AmountVatExcluded"].Visible = false;
            orderLinesDataGridView.Columns["CommitmentsBalanceDue"].Visible = false;
            orderLinesDataGridView.Columns["codePays_livraison"].Visible = false;
            orderLinesDataGridView.Columns["Ville_livraison"].Visible = false;
            orderLinesDataGridView.Columns["Codepostal_livraison"].Visible = false;
            orderLinesDataGridView.Columns["Adresse4_livraison"].Visible = false;
            orderLinesDataGridView.Columns["Adresse3_livraison"].Visible = false;
            orderLinesDataGridView.Columns["Adresse2_livraison"].Visible = false;
            orderLinesDataGridView.Columns["Adresse1_livraison"].Visible = false;
            orderLinesDataGridView.Columns["codePays_facturation"].Visible = false;
            orderLinesDataGridView.Columns["Ville_facturation"].Visible = false;
            orderLinesDataGridView.Columns["Codepostal_facturation"].Visible = false;
            orderLinesDataGridView.Columns["Adresse4_facturation"].Visible = false;
            orderLinesDataGridView.Columns["Adresse3_facturation"].Visible = false;
            orderLinesDataGridView.Columns["Adresse2_facturation"].Visible = false;
            orderLinesDataGridView.Columns["Adresse1_facturation"].Visible = false;
            orderLinesDataGridView.Columns["Id"].Visible = false;
            orderLinesDataGridView.Columns["NumberSuffix"].Visible = false;
            orderLinesDataGridView.Columns["NumberPrefix"].Visible = false;
            orderLinesDataGridView.Columns["FinancialDiscountAmount"].Visible = false;
            orderLinesDataGridView.Columns["CurrencyId"].Visible = false;
            orderLinesDataGridView.Columns["IntrastatTransportMode"].Visible = false;

            orderLinesDataGridView.Columns["DocumentType"].Visible = false;
            orderLinesDataGridView.Columns["OriginDocumentType"].Visible = false;
            orderLinesDataGridView.Columns["TransferedDocumentId"].Visible = false;

            orderLinesDataGridView.Columns["InvoicingContact_Name"].Visible = false;
            orderLinesDataGridView.Columns["InvoicingContact_FirstName"].Visible = false;
            orderLinesDataGridView.Columns["InvoicingContact_Phone"].Visible = false;
            orderLinesDataGridView.Columns["InvoicingContact_CellPhone"].Visible = false;
            orderLinesDataGridView.Columns["InvoicingContact_Fax"].Visible = false;
            orderLinesDataGridView.Columns["InvoicingContact_Email"].Visible = false;
            orderLinesDataGridView.Columns["InvoicingContact_Function"].Visible = false;
            orderLinesDataGridView.Columns["DeliveryContact_Name"].Visible = false;
            orderLinesDataGridView.Columns["DeliveryContact_FirstName"].Visible = false;
            orderLinesDataGridView.Columns["DeliveryContact_Phone"].Visible = false;
            orderLinesDataGridView.Columns["DeliveryContact_CellPhone"].Visible = false;
            orderLinesDataGridView.Columns["DeliveryContact_Fax"].Visible = false;
            orderLinesDataGridView.Columns["DeliveryContact_Email"].Visible = false;
            orderLinesDataGridView.Columns["DeliveryContact_Function"].Visible = false;

            orderLinesDataGridView.Columns["DetailTaxAmount0_TaxCalculationBase"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount0_BaseAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount0_TaxAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount0_TaxCaption"].Visible = false;

            orderLinesDataGridView.Columns["DetailTaxAmount1_TaxCalculationBase"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount1_BaseAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount1_TaxAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount1_TaxCaption"].Visible = false;

            orderLinesDataGridView.Columns["DetailTaxAmount2_TaxCalculationBase"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount2_BaseAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount2_TaxAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount2_TaxCaption"].Visible = false;

            orderLinesDataGridView.Columns["DetailTaxAmount3_TaxCalculationBase"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount3_BaseAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount3_TaxAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount3_TaxCaption"].Visible = false;

            orderLinesDataGridView.Columns["DetailTaxAmount4_TaxCalculationBase"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount4_BaseAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount4_TaxAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount4_TaxCaption"].Visible = false;

            orderLinesDataGridView.Columns["DetailTaxAmount5_TaxCalculationBase"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount5_BaseAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount5_TaxAmount"].Visible = false;
            orderLinesDataGridView.Columns["DetailTaxAmount5_TaxCaption"].Visible = false;
        }

        /// <summary>
        /// Formatage de la grille des lignes de documents lorsque les articles sélectionnés changent
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void orderLinesDataGridView_DataSourceChanged(object sender, EventArgs e)
        {
            if (orderLinesDataGridView.Columns["ItemId"] != null)
            {
                orderLinesDataGridView.Columns["ItemId"].HeaderText = "Identifiant article";
                orderLinesDataGridView.Columns["ItemId"].ReadOnly = true;
            }
            if (orderLinesDataGridView.Columns["ItemCaption"] != null)
            {
                orderLinesDataGridView.Columns["ItemCaption"].HeaderText = "Libellé";
                orderLinesDataGridView.Columns["ItemCaption"].ReadOnly = true;
            }
            if (orderLinesDataGridView.Columns["ItemPrice"] != null)
            {
                orderLinesDataGridView.Columns["ItemPrice"].HeaderText = "Prix";
                orderLinesDataGridView.Columns["ItemPrice"].DefaultCellStyle.Format = "f2";
                orderLinesDataGridView.Columns["ItemPrice"].ReadOnly = true;
            }
            if (orderLinesDataGridView.Columns["Quantity"] != null)
            {
                orderLinesDataGridView.Columns["Quantity"].HeaderText = "Quantité";
                orderLinesDataGridView.Columns["Quantity"].ReadOnly = false;
            }
        }

        /// <summary>
        /// Vérifie le format de la celleule des quantités
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void orderLinesDataGridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (orderLinesDataGridView.Columns[e.ColumnIndex].Name == "Quantity" && e.RowIndex != -1)
            {
                double quantity;
                if (!double.TryParse(e.FormattedValue.ToString(), out quantity))
                {
                    MessageBox.Show("La valeur indiquée pour la quantité ne respecte pas le format attendu (Ex: 7 ou 4,25).");
                    e.Cancel = true;
                    orderLinesDataGridView.CancelEdit();
                }
                else if (quantity < 0 || quantity > Int32.MaxValue)
                {
                    MessageBox.Show(string.Format("La quantité doit être comprise entre 0 et {0}", Int32.MaxValue));
                    e.Cancel = true;
                    orderLinesDataGridView.CancelEdit();
                }
            }
        }

        /// <summary>
        /// Lancement de l'import
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void importButton_Click(object sender, EventArgs e)
        {
            ExportFacture();
        }
        #endregion

        private void itemsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void customersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ImportOrdersForm_Load(object sender, EventArgs e)
        {

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
                                    reader["ItemId"].ToString(), reader["Quantity"].ToString(), reader["RealQuantity"].ToString().Substring(0,10),
                                    reader["PurchasePrice"].ToString(),
                                    reader["CostPrice"].ToString(),reader["TotalDiscountRate"].ToString(), reader["NetPriceVatExcluded"].ToString(),
                                    reader["NetPriceVatIncluded"].ToString(), reader["NetPriceVatExcludedWithDiscount"].ToString(), 
                                    reader["NetPriceVatIncludedWithDiscount"].ToString(), reader["NetAmountVatExcluded"].ToString(),
                                    reader["NetAmountVatExcludedWithDiscount"].ToString(), reader["NetAmountVatIncluded"].ToString(),
                                    reader["NetAmountVatIncludedWithDiscount"].ToString(), reader["VatId"].ToString(),
                                    reader["OrderedQuantity"].ToString(), reader["VatAmount"].ToString(), reader["DeliveryDate"].ToString(), reader["DeliveredQuantity"].ToString(),
                                    reader["NetWeight"].ToString(),reader["TotalNetWeight"].ToString(), reader["RealNetAmountVatExcluded"].ToString(),
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
      
    }
}
