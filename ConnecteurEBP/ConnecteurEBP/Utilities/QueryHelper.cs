using System;
using System.Collections.Generic;
using System.Text;

namespace ConnecteurEBP.Utilities
{
  /// <summary>
  /// Classe statique permettant de stocker toutes les requêtes SQL utilisées dans l'application
  /// </summary>
  public static class QueryHelper
  {
    #region SQLServer Queries
    /// <summary>
    /// Récupère tous les clients actifs avec leurs adresses
    /// </summary>
    public const string Customers_SQLServer = "SELECT Id, Name, MainInvoicingAddress_Address1 as Address, " +
            "MainInvoicingAddress_ZipCode as ZipCode, MainInvoicingAddress_City as City, " +
          "MainInvoicingAddress_CountryIsoCode as Country FROM Customer WHERE ActiveState = 0";
    /// <summary>
    /// Récupère le prochain identifiant des commandes
    /// </summary>
    public const string NextOrderId_SQLServer = "SELECT incValue FROM EbpSysAutoIncrement WHERE incName = 'SaleOrder¤NextId'";

    public const string NextDeliveryId_SQLServer = "SELECT incValue FROM EbpSysAutoIncrement WHERE incName = 'DeliveryOrder¤NextId'";
    /// <summary>
    /// Récupère les 20 meilleurs clients classés par chiffre d'affaire
    /// </summary>
    public const string BestTop20CustomersBySales_SQLServer = "select top 20 c.Id, c.Name, sum(sd.AmountVatExcludedWithDiscount) Sales,  " +
                         "c.MainInvoicingAddress_Address1 as Address, c.MainInvoicingAddress_ZipCode as ZipCode, " +
                         "c.MainInvoicingAddress_City as City, c.MainInvoicingAddress_CountryIsoCode as Country" +
                         " from customer c left outer join SaleDocument sd on c.Id = sd.CustomerId" +
                         " group by c.Id, c.Name, c.MainInvoicingAddress_Address1, c.MainInvoicingAddress_ZipCode, " +
                         "c.MainInvoicingAddress_City, c.MainInvoicingAddress_CountryIsoCode " +
                         "order by Sales desc";
    /// <summary>
    /// Récupère tous les articles classés par par ordre alphabétique
    /// </summary>
    public const string Items_SQLServer = "SELECT Id, Caption, SalePriceVatIncluded, VatAmount, PurchasePrice FROM Item ORDER BY Caption asc";
    /// <summary>
    /// Récupère tous les modèles d'impression correspondant aux articles classés par ordre alphabétique
    /// </summary>
    public const string ItemReports_SQLServer = "SELECT Id, Label FROM EbpSysReport WHERE CategoryId LIKE 'EB27722D-470B-41B8-A97C-27A9EF332EAA' " +
            "AND LevelId IN ('92E1BD21-4140-453D-892F-56B3896EAC41', 'dfd4abf9-2963-4f53-9032-72f543b6d343', '58b33e99-768c-41d8-be8e-081aa5655dc3', '8824ac9e-bd07-499b-8b9e-46cc6365b9b3', 'E5A45148-24BA-47f5-97DB-E5502E1DB492') ORDER BY Label asc";
   
    
   public const string Factures_Ventes_SQLServer = " SELECT DocumentNumber,CustomerName,AmountVatExcludedWithDiscountAndShipping,VatAmount,AmountVatIncluded,DepositAmount,TotalDueAmount,CommitmentsBalanceDue FROM SaleDocument where DocumentNumber like 'FA%' order by DocumentNumber DESC";

   public const string Client_BASE_X_EBP = "select ID_BASE_X,ID_EBP,Name from Customer,Client_BASE_X_EBP where Client_BASE_X_EBP.ID_EBP=Customer.Id";

   public const string createTable = "IF NOT EXISTS"+
  " (  SELECT [name]"+
      "FROM sys.tables"+
     " WHERE [name] = 'Client_BASE_X_EBP'"+
   ")"+
   
  " CREATE TABLE Client_BASE_X_EBP (  "+
  "  ID INT PRIMARY KEY IDENTITY, " +
   " ID_BASE_X VARCHAR(128), "+
  " ID_EBP VARCHAR(128), " + 
")";

   public const string createTableCommande = "IF NOT EXISTS" +
" (  SELECT [name]" +
"FROM sys.tables" +
" WHERE [name] = 'commande_enregistrer'" +
")" +

" CREATE TABLE commande_enregistrer (  " +
"  ID INT PRIMARY KEY IDENTITY, " +
" ID_Import VARCHAR(128), " +
" ID_EBP VARCHAR(128), " +
")";

   public const string createPath = "IF NOT EXISTS" +
" (  SELECT [name]" +
"FROM sys.tables" +
" WHERE [name] = 'Path_ImportPlanifier'" +
")" +

" CREATE TABLE Path_ImportPlanifier (  " +
" path VARCHAR(128), " +
")";

   public const string createTable_TachePlanifier = "IF NOT EXISTS" +
" (  SELECT [name]" +
"FROM sys.tables" +
" WHERE [name] = 'TachePlanifier'" +
")" +

" CREATE TABLE TachePlanifier (  " +
" import_commande BIT, " +
" import_bonlivraison BIT, " +
" import_facture BIT, " +
" export_commande BIT, " +
" export_bonlivraison BIT, " +
" export_facture BIT, " +
")";

   public const string getTachePlanifier = "select * from TachePlanifier";

   public const string deleteImportPlanifier = "delete from Path_ImportPlanifier";

   public const string deleteTachePlanifier = "delete from TachePlanifier";

   public const string selectPath = "select path from Path_ImportPlanifier";

   public const string VerifierDeletedCommande = "delete from commande_enregistrer";

   public const string createTriger1 = "Create trigger tr1 on SaleDocument "+
"after insert "+
"as "+
"update SaleDocument set Reference = REPLACE ( reference , 'Document importé n° ' , '' ) "+
"where DocumentNumber in (select DocumentNumber from inserted) and (select reference from inserted)is not null "+
"INSERT INTO commande_enregistrer(ID_Import,ID_EBP)  select reference, DocumentNumber from inserted where (select reference from inserted)is not null ";

      public const string createTriger2 = "Create trigger tr2 on SaleDocument "+
"after delete "+
"as "+
"DELETE FROM commande_enregistrer "+
"WHERE ID_EBP in (Select DocumentNumber from deleted )";


   public const string EbpSysGenericImportSettings = "IF NOT EXISTS"+
  " (  SELECT name"+
   "   FROM EbpSysGenericImportSettings"+
   "   WHERE name = 'Commandes2'"+
   ")"+
"insert into EbpSysGenericImportSettings"+
"(sysCreatedDate,sysCreatedUser,sysModifiedDate,sysModifiedUser,name,categoryId,export,formatId,serializedEntity)"+
"values('20121223 23:59:59.99','ADM','20121223 23:59:59.99','ADM','Commandes2','D22C51B6-5E65-45D7-A0E8-3B0A8348F16D','false','04C5697F-B974-4170-8A03-20D295C9487C','null')";

   public const string EbpSysGenericImportSettings2 = "IF NOT EXISTS" +
" (  SELECT name" +
"   FROM EbpSysGenericImportSettings" +
"   WHERE name = 'import_bonLivraison'" +
")" +
"insert into EbpSysGenericImportSettings" +
"(sysCreatedDate,sysCreatedUser,sysModifiedDate,sysModifiedUser,name,categoryId,export,formatId,serializedEntity)" +
"values('20121223 23:59:59.99','ADM','20121223 23:59:59.99','ADM','import_bonLivraison','5a1aa132-7002-4cd7-8ec9-71c03c0343ae','false','04C5697F-B974-4170-8A03-20D295C9487C','null')";

   public const string EbpSysGenericImportSettings_facture = "IF NOT EXISTS" +
" (  SELECT name" +
"   FROM EbpSysGenericImportSettings" +
"   WHERE name = 'import_facture'" +
")" +
"insert into EbpSysGenericImportSettings" +
"(sysCreatedDate,sysCreatedUser,sysModifiedDate,sysModifiedUser,name,categoryId,export,formatId,serializedEntity)" +
"values('20150312 23:59:59.99','ADM','20150312 23:59:59.99','ADM','import_facture','ed7fcd30-7018-49c4-a1fc-622f0dee675c','false','04c5697f-b974-4170-8a03-20d295c9487c','null')";

   //public const string UpdateEbpSysGenericImport = @"update EbpSysGenericImportSettings set serializedEntity=(SELECT * FROM   OPENROWSET(BULK '" + Environment.CurrentDirectory+ @"\..\Resources\Monfichier.xml', SINGLE_CLOB) as T) where name='Commandes' and serializedEntity='null'";

   public static string insertClient(string id_X, string id_EBP)
   {
       return "INSERT INTO Client_BASE_X_EBP (ID_BASE_X,ID_EBP) VALUES ('" + id_X + "','" + id_EBP + "')";
   }
            
   public static string insertCommande(string ID_Import,string ID_EBP)
   {
       return "INSERT INTO commande_enregistrer (ID_Import,ID_EBP) VALUES ('" + ID_Import + "','" + ID_EBP + "')";
   }

   public static string ExistCustomer(string id)
   {
       return "SELECT Id FROM Customer where Id='" + id + "'";
   }
   
      public static string testExisteClient(string id)
   {
       return "SELECT ID_BASE_X FROM Client_BASE_X_EBP where ID_EBP='" + id + "'";
   }
      
   public static string returnClient(string id)
   {
       return "SELECT ID_EBP FROM Client_BASE_X_EBP where ID_BASE_X='" + id + "'";
   }

   public static string deleteClient(string idEBP,string id)
   {
       return "delete FROM Client_BASE_X_EBP where ID_EBP='"+idEBP+"' and ID_BASE_X='" + id + "'";
   }

   public static string ModifierClient(string oldIdEBP, string oldIdGNL, string newIdEBP, string newIdGNL)
   {
       return "update Client_BASE_X_EBP set ID_EBP='" + newIdEBP + "' , ID_BASE_X='" + newIdGNL + "'  where ID_EBP='" + oldIdEBP + "' and ID_BASE_X='" + oldIdGNL + "'";
   }

   public static string updateClient(string IdEBP, string IdGNL)
   {
       return "update Client_BASE_X_EBP set ID_BASE_X='" + IdGNL + "'  where ID_EBP='" + IdEBP + "'";
   }

   public static string returnArticle(string id)
   {
       return "SELECT ID_EBP FROM Article_BASE_X_EBP where ID_BASE_X='" + id + "'";
   }

   public static string returnArticleCodeBarre(string codeBarre)
   {
       return "select Id from Item where barcode='" + codeBarre + "'";
   }

   public static string returnNomClient(string id)
   {
       return "select Name from Customer where Id='" + id + "'";
   }

   public static string TestClientAdressFacturation(string id)
   {
       return "select MainDeliveryAddress_ZipCode,MainDeliveryAddress_City,MainDeliveryAddress_CountryIsoCode from Customer where Id='" + id + "'";
   }

   public static string returnCommande(string id)
   {
       return "SELECT ID_EBP FROM commande_enregistrer where ID_Import='" + id + "'";
   }

   public static string VerifierCommande(string id)
   {
       return "SELECT reference FROM SaleDocument where REPLACE ( REPLACE ( reference , 'Document importé n° ' , '' ),'(prix article différent)','')='" + id + "'";
   }

   public static string VerifierPrixArticle(string id)
   {
       return "select SalePriceVatExcluded from item where id='"+id+"'";
   }

   // *******************************************************************************************************

   public static string getFactureClient(string clientId)
   {
       return "select * from saledocument where DocumentType=2 and customerId='" + clientId + "'";
   }

   public static string getListCommandes()
   {
       return "select * from saledocument where DocumentType=8";
   }

   public static string getBonLivraisonClient(string clientId)
   {
       return "select * from saledocument where DocumentType=6 and customerId='" + clientId + "'";
   }

   public static string getDocumentLine(string factureid)
   {
       return "select * from saledocumentline where DocumentId='" + factureid + "' and linetype!=9";
   }

   public static string returnEANClient(string id)
   {
       return "SELECT ID_BASE_X FROM Client_BASE_X_EBP where ID_EBP='" + id + "'";
   }

   public static string returnEANArticle(string id)
   {
       return "select barcode from Item where Id='" + id + "'";
   }

   public static string getCommandeFacture(string id)
   {
       return "SELECT documentnumber,REPLACE ( REPLACE ( reference , 'Document importé n° ' , '' ),'(prix article différent)',''),documentdate from saledocument  where transfereddocumentid='" + id + "' and documentType=8";
   }

   public static string getBonLivraisonFacture(string id)
   {
       return "SELECT documentdate from saledocument  where transfereddocumentid='" + id + "' and documentType=6";
   }

   public static string getTransport(string id)
   {
       return "SELECT caption from IntrastatTransportMode where id='" + id + "'";
   }


    #endregion
  }
}
