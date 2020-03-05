using ImportPlanifier.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ImportPlanifier.Classes
{
    public class ExporterCommandes
    {
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
                                    reader["AmountVatExcludedWithDiscountAndShipping"].ToString().Replace(",", ".").Replace("000000", ""), reader["AmountVatExcludedWithDiscountAndShippingWithoutEcotax"].ToString(), reader["VatAmount"].ToString(), reader["AmountVatIncluded"].ToString().Replace(",", ".").Replace("000000", ""),
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
                                     reader["DetailTaxAmount5_TaxCalculationBase"].ToString(), reader["DetailTaxAmount5_BaseAmount"].ToString(), reader["DetailTaxAmount5_TaxAmount"].ToString(), reader["DetailTaxAmount5_TaxCaption"].ToString()


                                    );
                                listCommandes.Add(Commande);
                            }
                        }
                    }
                    return listCommandes;
                }
                catch (Exception e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    Console.WriteLine(e.Message);
                    return null;
                }
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
                catch (Exception e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    Console.WriteLine(e.Message);
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
                catch (Exception e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    Console.WriteLine(e.Message);
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

        public static void updateDocumentdeVente(string DocNumber)
        {
            // Insertion dans la base sage : cbase
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.updateExportDocument(DocNumber), sqlConnection))

                        cmd.ExecuteNonQuery();

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    Console.WriteLine(ex.Message);
                }
            }

        }

        public void LancerExportCommande(string pathExport)
        {
            try
            {
                var dirName = string.Format(@"\Fichier Exporter\LOG\LOG{0:MM-yyyy}\", DateTime.Now);
                var logName = string.Format("Log.{0:ddMMyyyy}.log", DateTime.Now);


                string outputFile = pathExport + @"\Fichier Exporter\Bons de commandes\";

                if (!System.IO.Directory.Exists(outputFile))
                {
                    System.IO.Directory.CreateDirectory(outputFile);
                }

                if (!System.IO.Directory.Exists(pathExport + dirName))
                {
                    System.IO.Directory.CreateDirectory(pathExport + dirName);
                }
                //DocumentVente documentvente = new DocumentVente();
                List<DocumentVente> ListDocumentVente = GetCommandeFromDataBase();

                using (StreamWriter writerLog = new StreamWriter(pathExport + dirName + logName, true, Encoding.Default))
                {
                    try
                    {
                        Console.WriteLine(DateTime.Now + " : ***** Debut de l'export de commandes ***** ");
                        writerLog.WriteLine("-------------------------------------------------------------------------");
                        writerLog.WriteLine(DateTime.Now + " : ***** Debut de l'export de commandes ***** ");

                        if (ListDocumentVente.Count != 0)
                        {
                            Console.WriteLine(DateTime.Now + " : (" + ListDocumentVente.Count + ") trouvé(s).");
                            writerLog.WriteLine(DateTime.Now + " : (" + ListDocumentVente.Count + ") trouvé(s).");
                        }

                        for (int j = 0; j < ListDocumentVente.Count; j++)
                        {
                            DocumentVente documentvente = ListDocumentVente[j];

                            string EANClient = GetEANClient(documentvente.CustomerId);


                            var fileName = string.Format("EDI_ORDERS." + EANClient + "." + documentvente.DocumentNumber.Replace(documentvente.NumberPrefix, "") + "." + ConvertDate(documentvente.DocumentDate.Replace("00:00:00", "")) + "." + documentvente.Ville_livraison + ".{0:yyyyMMddhhmmss}.csv", DateTime.Now);

                            fileName = fileName.Replace("...", ".");

                            using (StreamWriter writer = new StreamWriter(outputFile + @"\" + fileName.Replace("..", "."), false, Encoding.Default))
                            {
                                //if (CommandeAExporter.deviseCommande == "0")
                                //{
                                //    CommandeAExporter.deviseCommande = "";
                                //}

                                //if (CommandeAExporter.deviseCommande != "")
                                //{
                                //    CommandeAExporter.deviseCommande = getDeviseIso(CommandeAExporter.deviseCommande);
                                //}

                                string adresseDeLivraison = documentvente.Adresse1_livraison + "." + documentvente.Adresse2_livraison + "." + documentvente.Adresse3_livraison + "." + documentvente.Adresse4_livraison + "." + documentvente.Codepostal_livraison + "." + documentvente.Ville_livraison + "." + documentvente.codePays_livraison;

                                adresseDeLivraison = adresseDeLivraison.Replace("...", ".");

                                writer.WriteLine("ORDERS;" + documentvente.DocumentNumber.Replace(documentvente.NumberPrefix, "") + ";" + EANClient + ";" + EANClient + ";;;;" + adresseDeLivraison.Replace("..", ".") + ";" + documentvente.CurrencyId + ";;");

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

                            updateDocumentdeVente(documentvente.DocumentNumber);
                        }

                        Console.WriteLine(DateTime.Now + " : Nombre de commande exportée : " + ListDocumentVente.Count);
                        writerLog.WriteLine(DateTime.Now + " : Nombre de commande exportée : " + ListDocumentVente.Count);

                    }
                    catch (Exception ex)
                    {
                        //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                        Console.WriteLine(DateTime.Now + " : Exception : " + ex.Message);
                        writerLog.WriteLine(DateTime.Now + " : Exception : " + ex.Message);
                    }
                }


            }
            catch (Exception e)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                Console.WriteLine(e.Message);
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
                catch (Exception e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

    }
}
