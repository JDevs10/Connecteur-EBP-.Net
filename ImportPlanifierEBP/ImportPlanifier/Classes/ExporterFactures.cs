using ConsoleApplication1.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApplication1.Classes
{
    public class ExporterFactures
    {
        private List<DocumentVente> GetFactureFromDataBase()
        {
            //DocumentVente Facture = new DocumentVente();
            List<DocumentVente> listFactures = new List<DocumentVente>();
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.getFactureExport(), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DocumentVente Facture = new DocumentVente(reader["Id"].ToString(), reader["DocumentNumber"].ToString(), reader["NumberPrefix"].ToString(), reader["NumberSuffix"].ToString(), reader["DocumentDate"].ToString().Substring(0, 10),
                                    reader["DeliveryDate"].ToString(), reader["InvoicingAddress_Address1"].ToString(), reader["InvoicingAddress_Address2"].ToString(), reader["InvoicingAddress_Address3"].ToString(),
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
                                    reader["DetailTaxAmount0_TaxCalculationBase"].ToString(), reader["DetailTaxAmount0_BaseAmount"].ToString(), reader["DetailTaxAmount0_TaxAmount"].ToString(), reader["DetailTaxAmount0_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount1_TaxCalculationBase"].ToString(), reader["DetailTaxAmount1_BaseAmount"].ToString(), reader["DetailTaxAmount1_TaxAmount"].ToString(), reader["DetailTaxAmount1_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount2_TaxCalculationBase"].ToString(), reader["DetailTaxAmount2_BaseAmount"].ToString(), reader["DetailTaxAmount2_TaxAmount"].ToString(), reader["DetailTaxAmount2_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount3_TaxCalculationBase"].ToString(), reader["DetailTaxAmount3_BaseAmount"].ToString(), reader["DetailTaxAmount3_TaxAmount"].ToString(), reader["DetailTaxAmount3_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount4_TaxCalculationBase"].ToString(), reader["DetailTaxAmount4_BaseAmount"].ToString(), reader["DetailTaxAmount4_TaxAmount"].ToString(), reader["DetailTaxAmount4_TaxCaption"].ToString(),
                                     reader["DetailTaxAmount5_TaxCalculationBase"].ToString(), reader["DetailTaxAmount5_BaseAmount"].ToString(), reader["DetailTaxAmount5_TaxAmount"].ToString(), reader["DetailTaxAmount5_TaxCaption"].ToString()


                                    );
                                listFactures.Add(Facture);
                            }
                        }
                    }
                    return listFactures;
                }
                catch (Exception e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }

        public static string addZero(string date)
        {
            if (date.Length == 1)
            {
                return "0" + date;
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
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
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
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    Console.WriteLine(e.Message);
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
                        return null;
                    }
                }
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
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
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
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

        public void ExportFacture(string pathExport)
        {
            try
            {
                var dirName = string.Format(@"\Fichier Exporter\LOG\LOG{0:MM-yyyy}\", DateTime.Now);
                var logName = string.Format("Log.{0:ddMMyyyy}.log", DateTime.Now);
                string outputFile = pathExport + @"\Fichier Exporter\Factures\";

                if (!System.IO.Directory.Exists(pathExport + dirName))
                {
                    System.IO.Directory.CreateDirectory(pathExport + dirName);
                }

                if (!System.IO.Directory.Exists(outputFile))
                {
                    System.IO.Directory.CreateDirectory(outputFile);
                }
                using (StreamWriter writerLog = new StreamWriter(pathExport + dirName + logName, true, Encoding.Default))
                {
                    try
                    {
                        Console.WriteLine(DateTime.Now + " : ***** Debut de l'export de factures ***** ");
                        writerLog.WriteLine("-------------------------------------------------------------------------");
                        writerLog.WriteLine(DateTime.Now + " : ***** Debut de l'export de factures ***** ");


                        List<DocumentVente> FacturesAExporter = GetFactureFromDataBase();

                        if (FacturesAExporter.Count != 0)
                        {
                            Console.WriteLine(DateTime.Now + " : (" + FacturesAExporter.Count + ") trouvé(s).");
                            writerLog.WriteLine(DateTime.Now + " : (" + FacturesAExporter.Count + ") trouvé(s).");
                        }

                        for (int i = 0; i < FacturesAExporter.Count; i++)
                        {
                            string EANClient = GetEANClient(FacturesAExporter[i].CustomerId);

                            //var fileName = string.Format("Facture {0:dd-MM-yyyy hh.mm.ss}.csv", DateTime.Now);
                            var fileName = string.Format("EDI_INVOICE." + FacturesAExporter[i].CustomerId + "." + EANClient + ".{0:yyyyMMdd}.{0:hhmmss}.csv", DateTime.Now);


                            using (StreamWriter writer = new StreamWriter(outputFile + @"\" + fileName, false, Encoding.Default))
                            {
                                writer.WriteLine("DEMAT-AAA;v01.0;;;" + DateTime.Today.Year + addZero(DateTime.Today.Month.ToString()) + addZero(DateTime.Today.Day.ToString()) + ";;");
                                writer.WriteLine("");
                                writer.WriteLine("");




                                string[] tab = new string[] { "", "", "" };


                                // TYPE du document original exemple : facture  --origin--> bon de commande 
                                if (FacturesAExporter[i].OriginDocumentType == "8")
                                {
                                    tab = GetCommandeFacture(FacturesAExporter[i].Id).Split(';');
                                }


                                writer.WriteLine("DEMAT-HD1;v01.0;;" + FacturesAExporter[i].DocumentNumber.Replace(FacturesAExporter[i].NumberPrefix, "") + ";380;9;" + ConvertDate(FacturesAExporter[i].DocumentDate) + ";" + ConvertDate(FacturesAExporter[i].DeliveryDate) + ";;;;;" + FacturesAExporter[i].PaymentTypeId + ";;;;0;;" + tab[1].Trim() + ";" + ConvertDate(tab[2]) + ";;;;;;;;;;;;;;;;;;;;;;;;;" + FacturesAExporter[i].CurrencyId + ";" + FacturesAExporter[i].CurrencyId + ";;;" + FacturesAExporter[i].FinancialDiscountRate.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].FinancialDiscountAmount.Replace(",", ".").Replace("00000", "") + ";;;;;;;;;;;;;");
                                writer.WriteLine("");

                                writer.WriteLine("DEMAT-HD2;" + FacturesAExporter[i].DocumentNumber + ";;" + FacturesAExporter[i].Adresse1_facturation + ";" + FacturesAExporter[i].Codepostal_facturation + ";" + FacturesAExporter[i].Ville_facturation + ";" + FacturesAExporter[i].codePays_facturation + ";;;" + FacturesAExporter[i].Adresse1_facturation + ";" + FacturesAExporter[i].Codepostal_facturation + ";" + FacturesAExporter[i].Ville_facturation + ";" + FacturesAExporter[i].codePays_facturation + ";;;" + FacturesAExporter[i].Adresse1_facturation + ";" + FacturesAExporter[i].Codepostal_facturation + ";" + FacturesAExporter[i].Ville_facturation + ";" + FacturesAExporter[i].codePays_facturation + ";;;;;;;;;;;;;;;" + FacturesAExporter[i].Adresse1_livraison + ";" + FacturesAExporter[i].Codepostal_livraison + ";" + FacturesAExporter[i].Ville_livraison + ";" + FacturesAExporter[i].codePays_livraison + ";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
                                writer.WriteLine("");

                                writer.WriteLine("DEMAT-CTA;" + FacturesAExporter[i].InvoicingContact_Function + ";;" + FacturesAExporter[i].InvoicingContact_Name + " " + FacturesAExporter[i].InvoicingContact_FirstName + ";" + FacturesAExporter[i].InvoicingContact_Email + ";" + FacturesAExporter[i].InvoicingContact_Fax + ";" + FacturesAExporter[i].InvoicingContact_Phone + ";" + FacturesAExporter[i].InvoicingContact_Function + ";;" + FacturesAExporter[i].InvoicingContact_Name + " " + FacturesAExporter[i].InvoicingContact_FirstName + ";" + FacturesAExporter[i].InvoicingContact_Email + ";" + FacturesAExporter[i].InvoicingContact_Fax + ";" + FacturesAExporter[i].InvoicingContact_Phone + ";;;;;;;;;;;;;;;;;;;" + FacturesAExporter[i].DeliveryContact_Function + ";;" + FacturesAExporter[i].DeliveryContact_Name + " " + FacturesAExporter[i].DeliveryContact_FirstName + ";" + FacturesAExporter[i].DeliveryContact_Email + ";" + FacturesAExporter[i].DeliveryContact_Fax + ";" + FacturesAExporter[i].DeliveryContact_Phone + ";;;;;;;");
                                writer.WriteLine("");

                                if (FacturesAExporter[i].DiscountAmount != "0,00000000" || FacturesAExporter[i].DiscountAmount != "0,00000000")
                                {
                                    writer.WriteLine("DEMAT-REM;;A;;;;;;;;" + FacturesAExporter[i].DiscountAmount.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].DiscountRate.Replace(",", ".").Replace("00000", "") + ";;");
                                    writer.WriteLine("");
                                }

                                FacturesAExporter[i].lines = getDocumentLine(FacturesAExporter[i].Id);

                                for (int j = 0; j < FacturesAExporter[i].lines.Count; j++)
                                {
                                    string EANArticle = GetEANArticle(FacturesAExporter[i].lines[j].ItemId);

                                    writer.WriteLine("DEMAT-LIN;" + FacturesAExporter[i].lines[j].LineOrder + ";" + EANArticle + ";EAN;;;" + EANClient + ";;;" + FacturesAExporter[i].lines[j].TrackingNumber + ";" + FacturesAExporter[i].lines[j].DescriptionClear + ";;" + FacturesAExporter[i].lines[j].TotalNetWeight.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].TotalVolume.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Quantity.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].RealQuantity.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].OrderedQuantity.Replace(",", ".").Replace("00000", "") + ";;;;;" + FacturesAExporter[i].lines[j].RealNetAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].SalePriceVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].SalePriceVatExcluded.Replace(",", ".").Replace("00000", "") + ";;1;;;" + ConvertDate(FacturesAExporter[i].lines[j].DeliveryDate) + ";;;;;" + FacturesAExporter[i].lines[j].LineOrder + ";" + ConvertDate(tab[2]) + ";;;;;;;;" + FacturesAExporter[i].lines[j].RealNetAmountVatIncluded.Replace(",", ".").Replace("00000", "") + ";;;;;;;;");
                                    writer.WriteLine("");

                                    if (FacturesAExporter[i].lines[j].OtherTaxes0_TaxAmount != "0,00000000")
                                    {
                                        writer.WriteLine("DEMAT-TAX;1;;;" + FacturesAExporter[i].lines[j].OtherTaxes0_TaxValue.Replace(",", ".").Replace("00000", "") + ";;" + FacturesAExporter[i].lines[j].OtherTaxes0_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;;");
                                        writer.WriteLine("");
                                    }
                                    if (FacturesAExporter[i].lines[j].OtherTaxes1_TaxAmount != "0,00000000")
                                    {
                                        writer.WriteLine("DEMAT-TAX;2;;;" + FacturesAExporter[i].lines[j].OtherTaxes1_TaxValue.Replace(",", ".").Replace("00000", "") + ";;" + FacturesAExporter[i].lines[j].OtherTaxes1_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;;");
                                        writer.WriteLine("");
                                    }

                                    if (FacturesAExporter[i].lines[j].OtherTaxes2_TaxAmount != "0,00000000")
                                    {
                                        writer.WriteLine("DEMAT-TAX;3;;;" + FacturesAExporter[i].lines[j].OtherTaxes2_TaxValue.Replace(",", ".").Replace("00000", "") + ";;" + FacturesAExporter[i].lines[j].OtherTaxes2_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;;");
                                        writer.WriteLine("");
                                    }


                                    if (FacturesAExporter[i].lines[j].Discounts0_UnitDiscountRate != "0,00000000")
                                    {
                                        writer.WriteLine("DEMAT-DED;;A;;;;;;;" + FacturesAExporter[i].lines[j].Discounts0_DiscountType.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts0_UnitDiscountAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts0_UnitDiscountRate.Replace(",", ".").Replace("00000", "") + ";;");
                                        writer.WriteLine("");
                                    }


                                    if (FacturesAExporter[i].lines[j].Discounts1_UnitDiscountRate != "0,00000000")
                                    {
                                        writer.WriteLine("DEMAT-DED;;A;;;;;;;" + FacturesAExporter[i].lines[j].Discounts1_DiscountType.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts1_UnitDiscountAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts1_UnitDiscountRate.Replace(",", ".").Replace("00000", "") + ";;");
                                        writer.WriteLine("");
                                    }


                                    if (FacturesAExporter[i].lines[j].Discounts2_UnitDiscountRate != "0,00000000")
                                    {
                                        writer.WriteLine("DEMAT-DED;;A;;;;;;;" + FacturesAExporter[i].lines[j].Discounts2_DiscountType.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts2_UnitDiscountAmountVatExcluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].lines[j].Discounts2_UnitDiscountRate.Replace(",", ".").Replace("00000", "") + ";;");
                                        writer.WriteLine("");
                                    }
                                }

                                //  Les lignes des taxes
                                if (FacturesAExporter[i].DetailTaxAmount0_TaxAmount != "0,00000000")
                                {
                                    writer.WriteLine("DEMAT-TTX;1;;;" + FacturesAExporter[i].DetailTaxAmount0_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount0_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                                    writer.WriteLine("");
                                }
                                if (FacturesAExporter[i].DetailTaxAmount1_TaxAmount != "0,00000000")
                                {
                                    writer.WriteLine("DEMAT-TTX;2;;;" + FacturesAExporter[i].DetailTaxAmount1_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount1_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                                    writer.WriteLine("");
                                }
                                if (FacturesAExporter[i].DetailTaxAmount2_TaxAmount != "0,00000000")
                                {
                                    writer.WriteLine("DEMAT-TTX;3;;;" + FacturesAExporter[i].DetailTaxAmount2_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount2_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                                    writer.WriteLine("");
                                }
                                if (FacturesAExporter[i].DetailTaxAmount3_TaxAmount != "0,00000000")
                                {
                                    writer.WriteLine("DEMAT-TTX;4;;;" + FacturesAExporter[i].DetailTaxAmount3_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount3_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                                    writer.WriteLine("");
                                }
                                if (FacturesAExporter[i].DetailTaxAmount4_TaxAmount != "0,00000000")
                                {
                                    writer.WriteLine("DEMAT-TTX;5;;;" + FacturesAExporter[i].DetailTaxAmount4_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount4_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                                    writer.WriteLine("");
                                }
                                if (FacturesAExporter[i].DetailTaxAmount5_TaxAmount != "0,00000000")
                                {
                                    writer.WriteLine("DEMAT-TTX;6;;;" + FacturesAExporter[i].DetailTaxAmount5_TaxCaption.Replace(",", ".").Replace("00000", "") + ";;;" + FacturesAExporter[i].DetailTaxAmount5_TaxAmount.Replace(",", ".").Replace("00000", "") + ";;");
                                    writer.WriteLine("");
                                }

                                writer.WriteLine("DEMAT-END;;;" + FacturesAExporter[i].DocumentNumber.Replace(FacturesAExporter[i].NumberPrefix, "") + ";" + FacturesAExporter[i].AmountVatExcludedWithDiscountAndShipping.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].AmountVatIncluded.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].VatAmount.Replace(",", ".").Replace("00000", "") + ";;;;" + FacturesAExporter[i].FinancialDiscountAmount.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].DepositAmount.Replace(",", ".").Replace("00000", "") + ";" + FacturesAExporter[i].TotalDueAmount.Replace(",", ".").Replace("00000", "") + ";;;;");
                                writer.WriteLine("");
                                writer.WriteLine("");


                                writer.WriteLine("DEMAT-ZZZ;v01.0;;;;");


                            }

                            updateDocumentdeVente(FacturesAExporter[i].DocumentNumber);

                        }

                        Console.WriteLine(DateTime.Now + " : Nombre de facture exportée : " + FacturesAExporter.Count);
                        writerLog.WriteLine(DateTime.Now + " : Nombre de facture exportée : " + FacturesAExporter.Count);

                    }
                    catch (Exception ex)
                    {
                        //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                        Console.WriteLine(DateTime.Now + " : Exception : " + ex.Message);
                        writerLog.WriteLine(DateTime.Now + " : Exception : " + ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                Console.WriteLine(ex.Message);
            }
        }
    }
}
