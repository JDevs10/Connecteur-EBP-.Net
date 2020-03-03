using ConnecteurEBP.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnecteurEBP.Utilities
{
    public static class QueryHelper
    {
        #region SQL Queries

        public static string ListClients()
        {
            return "select sCliCode,sCliRaisonSoc,sCliAdresse1Ligne,sCliAdresse1CodePos,sCliAdresse1Ville,sCliAdresse1CodePay,sCliAdresse2Ligne," +
  "sCliAdresse2CodePos,sCliAdresse2Ville,sCliAdresse2CodePay,sCliFraisPort,sCliModeReglement,sCliEscompte,sCliRemise,sCliDevise," +
  "sCli_Escompte,sCli_RemiseProduits,sCli_RemiseServices,sCli_RemiseForfaits,sCli_Siret,codebarcf,IBAN0_Contenu,Bq0NomBanque from client";
        }

        public static string insert_client_GLN(ClientMini client)
        {
            return "update client set codebarcf='" + client.Cli_GLN + "' where sclicode='" + client.Cli_Code + "'";
        }

        public static string Client_codeEBp(string code)
        {
            return "SELECT sCliCode,codebarcf from client where sclicode='" + code + "'";
        }

        public static string Client_codeGLN(string barcode, string code)
        {
            return "SELECT sCliCode,codebarcf from client where codebarcf='" + barcode + "'";
        }

        public static string get_client_export(string code)
        {
            return "SELECT sCliCode,CodeBarCF,sCliRaisonSoc,sCliAdresse1Ligne,sCliAdresse1CodePos,sCliAdresse1Ville,sCliAdresse1CodePay,sCli_Adresse1_NPAI,sCliAdresse2Ligne,sCliAdresse2CodePos,sCliAdresse2Ville,sCliAdresse2CodePay,sCli_Adresse2_NPAI,sCliType,sCliFraisPort, sCliModeReglement,sCliDevise,sCliCivilite,sCli_NoCompte,sCli_CodeRepr, sCli_GrilleTarifs,sCli_LienExterne,sCli_siret,BIC0_Banque,BIC0_Pays,BIC0_Localisation,BIC0_Branche,IBAN0_Pays,IBAN0_Controle,IBAN0_Contenu,Bq0NomBanque,Bq0AdrBanque,CB0_AdresseSuite,BIC1_Banque,BIC1_Pays,BIC1_Localisation,BIC1_Branche,IBAN1_Pays,IBAN1_Controle, IBAN1_Contenu,Bq1NomBanque,Bq1AdrBanque,CB1_AdresseSuite,BIC2_Banque,BIC2_Pays,BIC2_Localisation,BIC2_Branche,IBAN2_Pays,IBAN2_Controle,IBAN2_Contenu,Bq2NomBanque,Bq2AdrBanque,CB2_AdresseSuite,BIC3_Banque,BIC3_Pays,BIC3_Localisation,BIC3_Branche,IBAN3_Pays,IBAN3_Controle,IBAN3_Contenu,Bq3NomBanque,Bq3AdrBanque,CB3_AdresseSuite " +
            "from client where sCliCode='" + code + "'";
        }

        public static string get_client_adresse_export(string code)
        {
            return "SELECT Adresse_Ligne,Adresse_CodePostal,Adresse_Ville,CodeDeb,Adresse_NPAI,bPrincipal,bFacturation,CodeInterne" +
            " from adresses,pays where CodeTiers='" + code + "' and Adresse_CodePays=code";
        }

        public static string get_client_contact_export(string code)
        {
            return "SELECT sContact_Civilite,sContact_Interloc,sContact_Fonction  ,sContact_Nom ,sContact_Prenom ,sContact_Tel ,sContact_Fax  ,sContact_Portable ,sContact_EMail ,sContact_Url  , sContact_Password ,bPrincipal , bPrincipalLiv ,AdresseFacturation ,AdresseLivraison" +
            " from contacts where CodeTiers='" + code + "'";
        }

        public static string get_Clients_codebarre()
        {
            return "SELECT sCliCode,codebarcf from client";
        }

        public static string getClient(string id)
        {
            return "SELECT sCliCode,sCliRaisonSoc,sCliAdresse1Ligne,sCliAdresse1CodePos,sCliAdresse1Ville,sCliAdresse1CodePay,sCli_Adresse1_NPAI,sCliAdresse2Ligne,sCliAdresse2CodePos,sCliAdresse2Ville,sCliAdresse2CodePay,sCli_Adresse2_NPAI,sCliType,sCliNII,sCliFraisPort,sCliModeReglement,sCliEscompte,sCliRemise,bRemiseTTC,sCliDevise,sCliCivilite,sCliExemplairesPiec,sCli_NoCompte,sCli_CodeRepr,sCli_GrilleTarifs,sCli_ModeleDevis0,sCli_ModeleDevis1,sCli_ModeleCommande0,sCli_ModeleCommande1,sCli_ModeleBL0,sCli_ModeleBL1,sCli_ModeleFacture0,sCli_ModeleFacture1,sCli_DepotDefaut,sCli_LienExterne,sCli_Escompte,sCli_RemiseProduits,sCli_RemiseServices,sCli_RemiseForfaits,sCli_Siret,codebarcf,sCondRegl_NbJours," +
                "sCondReglEchNbJour1,sCondReglEchNbJour2,sCondReglEchNbJour3,sCondReglEchNbJour4,sCondReglEchNbJour5,sCondReglEchNbJour6,sCondReglEchNbJour7,sCondReglEchNbJour8,sCondReglEchNbJour9," +
                "sCondRegl_Net,sCondReglEchTypeEc1,sCondReglEchTypeEc2,sCondReglEchTypeEc3,sCondReglEchTypeEc4,sCondReglEchTypeEc5,sCondReglEchTypeEc6,sCondReglEchTypeEc7,sCondReglEchTypeEc8,sCondReglEchTypeEc9,sCondRegl_JourLe," +
                "sCondReglEchJourLe1,sCondReglEchJourLe2,sCondReglEchJourLe3,sCondReglEchJourLe4,sCondReglEchJourLe5,sCondReglEchJourLe6,sCondReglEchJourLe7,sCondReglEchJourLe8,sCondReglEchJourLe9,sCondReglEchPource0,sCondReglEchPource1,sCondReglEchPource2,sCondReglEchPource3,sCondReglEchPource4,sCondReglEchPource5,sCondReglEchPource6,sCondReglEchPource7,sCondReglEchPource8,sCondReglEchPource9,b30jEquivalentMois,sCondReglEchb30jEq1,sCondReglEchb30jEq2,sCondReglEchb30jEq3,sCondReglEchb30jEq4,sCondReglEchb30jEq5,sCondReglEchb30jEq6,sCondReglEchb30jEq7,sCondReglEchb30jEq8,sCondReglEchb30jEq9" +
                " from client where codebarcf='" + id + "'";
        }

        public static string getArticle(string id)
        {
            return "SELECT Code,designation,PxVenteHT0,Colisage,typebarcode,poidsUnite from ARTICLE where barcode='" + id + "'";
        }

        public static string getStockId()
        {
            return "select DE_NO FROM F_Depot where DE_Principal = 1";
        }

        public static string getNumLivraison(string ct_num)
        {
            return "select LI_NO FROM F_LIVRAISON where CT_NUM = '" + ct_num + "' and LI_PRINCIPAL=1";
        }

        public static string deleteCommande(string numCommande)
        {
            return "delete from piece where numeroprefixe='CC' and numeronumero=" + numCommande + "";
            //return "delete from piece where numeroprefixe='DV' and numeronumero=" + numCommande + "";
        }

        public static string getGAMME(int type, string REF_Article)
        {
            return "select AG_NO from F_artgamme where AG_TYPE=" + type + " and AR_REF='" + REF_Article + "'";
        }

        public static string UpdateCommande(Client client, Order order)
        {
            return "update piece set " +
     "CDate = '" + order.DateCommande + "'," +
     "TiersRaisonSoc = '" + client.sCliRaisonSoc + "'," +
     "TiersAdresse1Ligne = '" + client.sCliAdresse1Ligne + "'," +
     "TiersAdresse1CodePo = '" + client.sCliAdresse1CodePos + "',TiersAdresse1Ville = '" + client.sCliAdresse1Ville + "'," +
     "TiersAdresse1CodePa = " + client.sCliAdresse1CodePay + "," +
     "Tiers_Adresse1_NPAI = " + client.sCli_Adresse1_NPAI + "," +
     "TiersAdresse2Ligne = '" + order.adresse + "'," +
     "TiersAdresse2CodePo = '" + order.codepostale + "'," +
     "TiersAdresse2Ville = '" + order.ville + "'," +
     "TiersAdresse2CodePa = " + order.pays + "," +
     "TiersNII = '" + client.sCliNII + "'," +
     "TiersFraisPort = '" + client.sCliFraisPort + "'," +
     "TiersModeReglement = '" + client.sCliModeReglement + "'," +
     "SPieceSCondReglCode = '" + client.sCliModeReglement + "'," +
     "sPieceSCondReglNbj0 = '" + client.sCondRegl_NbJours + "'," +
     "TiersDevise = '" + client.sCliDevise + "'," +
     "TiersCivilite = '" + client.sCliCivilite + "'," +
     "TiersExemplairesPie = '" + client.sCliExemplairesPiec + "'," +
     "Tiers_NoCompte = '" + client.sCli_NoCompte + "'," +
     "Tiers_CodeRepr = '" + client.sCli_CodeRepr + "'," +
      "Tiers_GrilleTarifs = '" + client.sCli_GrilleTarifs + "'," +
      "Tiers_ModeleDevis0 = " + client.sCli_ModeleDevis0 + "," +
      "Tiers_ModeleDevis1 = " + client.sCli_ModeleDevis1 + "," +
      "TiersModeleCommande0 = " + client.sCli_ModeleCommande0 + "," +
      "TiersModeleCommande1 = " + client.sCli_ModeleCommande1 + "," +
      "Tiers_ModeleBL0 = " + client.sCli_ModeleBL0 + "," +
      "Tiers_ModeleBL1 = " + client.sCli_ModeleBL1 + "," +
      "Tiers_ModeleFacture0 = " + client.sCli_ModeleFacture0 + "," +
      "Tiers_ModeleFacture1=  " + client.sCli_ModeleFacture1 + "   ," +
     "Tiers_DepotDefaut=   '" + client.sCli_DepotDefaut + "'  ," +
     "Tiers_LienExterne=  '" + client.sCli_LienExterne + "'   ," +
     "Tiers_Escompte=  " + client.sCli_Escompte + "   ," +
     "TiersRemsieProduits=  " + client.sCli_RemiseProduits + "   ," +
     "TiersRemiseServices=  " + client.sCli_RemiseServices + "   ," +
     "TiersRemiseForfaits=  " + client.sCli_RemiseForfaits + "   ," +
     "Tiers_Siret=  '" + client.sCli_Siret + "'   ," +
     "TauxTVA1=    '20' ," +
     "TauxTVA2=   '5,5'  ," +
    " NetAPayer=  '" + order.MontantTotal + "'   ," +
     "reference=   '" + order.Reference + "'   ," +
     "SPiece_codetable=  '" + order.NumCommande + "'   ," +
    " dateLivraison=  '" + order.DateLivraison + "'   ," +
     "CodeUtilisateur=   'ADM'  ," +
     "SPieceDicoVersionCr=  6419   ," +
     "SPieceDicoVersionCh=  6419   ," +
     "SPiece_bOldArrondi=   1  ," +
    " SPiecebOlCalculDeee=  1   ," +
    " parite=   1  ," +
    " ContactLiv_Nom='" + order.nom_contact + "'" +

        " where NumeroPrefixe='DV' and numeronumero=" + order.Id + "";
        }

        public static string insertCommandeVide(Order order)
        {
            return "insert into piece(CType, NumeroPrefixe, NumeroNumero, CDate   ,   TiersCode ,SPieceSCondReglNbJ0 ,SPieceSCondReglNbJ1, SPieceSCondReglNbJ2 ,SPieceSCondReglNbJ3" +
",SPieceSCondReglNbJ4 ,SPieceSCondReglNbJ5, SPieceSCondReglNbJ6 ,SPieceSCondReglNbJ7 ,SPieceSCondReglNbJ8 ,SPieceSCondReglNbJ9, SPieceSCondReglTyp0, " +
"SPieceSCondReglTyp1 ,SPieceSCondReglTyp2 ,SPieceSCondReglTyp3 ,SPieceSCondReglTyp4, SPieceSCondReglTyp5, SPieceSCondReglTyp6, SPieceSCondReglTyp7, " +
"SPieceSCondReglTyp8 ,SPieceSCondReglTyp9 ,SPieceSCondReglJou0 ,SPieceSCondReglJou1 ,SPieceSCondReglJou2, SPieceSCondReglJou3 ,SPieceSCondReglJou4 " +
",SPieceSCondReglJou5, SPieceSCondReglJou6 ,SPieceSCondReglJou7, SPieceSCondReglJou8, SPieceSCondReglJou9 ,SPieceSCondReglPou0, SPieceSCondReglPou1, " +
"SPieceSCondReglPou2 ,SPieceSCondReglPou3, SPieceSCondReglPou4 ,SPieceSCondReglPou5 ,SPieceSCondReglPou6 ,SPieceSCondReglPou7, SPieceSCondReglPou8 " +
",SPieceSCondReglPou9 ,SPieceSCondReglb300, SPieceSCondReglb301, SPieceSCondReglb302 ,SPieceSCondReglb303 ,SPieceSCondReglb304 " +
",SPieceSCondReglb305,SPieceSCondReglb306, SPieceSCondReglb307 ,SPieceSCondReglb308,SPieceSCondReglb309," +
"SPieceSCondReglCode) " +
"SELECT CType, NumeroPrefixe, " + order.Id + ", CDate   ,   TiersCode , SPieceSCondReglNbJ0 ,SPieceSCondReglNbJ1, SPieceSCondReglNbJ2 ,SPieceSCondReglNbJ3 " +
",SPieceSCondReglNbJ4 ,SPieceSCondReglNbJ5, SPieceSCondReglNbJ6 ,SPieceSCondReglNbJ7 ,SPieceSCondReglNbJ8 ,SPieceSCondReglNbJ9, SPieceSCondReglTyp0, " +
"SPieceSCondReglTyp1 ,SPieceSCondReglTyp2 ,SPieceSCondReglTyp3 ,SPieceSCondReglTyp4, SPieceSCondReglTyp5, SPieceSCondReglTyp6, SPieceSCondReglTyp7, " +
"SPieceSCondReglTyp8 ,SPieceSCondReglTyp9 ,SPieceSCondReglJou0 ,SPieceSCondReglJou1 ,SPieceSCondReglJou2, SPieceSCondReglJou3 ,SPieceSCondReglJou4 " +
",SPieceSCondReglJou5, SPieceSCondReglJou6 ,SPieceSCondReglJou7, SPieceSCondReglJou8, SPieceSCondReglJou9 ,SPieceSCondReglPou0, SPieceSCondReglPou1, " +
"SPieceSCondReglPou2 ,SPieceSCondReglPou3, SPieceSCondReglPou4 ,SPieceSCondReglPou5 ,SPieceSCondReglPou6 ,SPieceSCondReglPou7, SPieceSCondReglPou8 " +
",SPieceSCondReglPou9 ,SPieceSCondReglb300, SPieceSCondReglb301, SPieceSCondReglb302 ,SPieceSCondReglb303 ,SPieceSCondReglb304 " +
",SPieceSCondReglb305,SPieceSCondReglb306, SPieceSCondReglb307 ,SPieceSCondReglb308,SPieceSCondReglb309," +
"SPieceSCondReglCode   FROM PIECE where numeroprefixe='DV' and numeronumero=10";
        }


        public static string insertCommande(Client client, Order order)
        {
            return "SET TRUENULLCREATE=OFF; Insert into piece " +
     "(CType,NumeroPrefixe,NumeroNumero,CDate,TiersCode,TiersRaisonSoc,TiersAdresse1Ligne,TiersAdresse1CodePo,TiersAdresse1Ville," +
     "TiersAdresse1CodePa,Tiers_Adresse1_NPAI,TiersAdresse2Ligne,TiersAdresse2CodePo,TiersAdresse2Ville," +
     "TiersAdresse2CodePa,TiersNII,TiersFraisPort,TiersModeReglement,SPieceSCondReglCode,sPieceSCondReglNbj0," +
     "SPieceSCondReglNbJ1, SPieceSCondReglNbJ2, SPieceSCondReglNbJ3 ,SPieceSCondReglNbJ4 ,SPieceSCondReglNbJ5, SPieceSCondReglNbJ6, SPieceSCondReglNbJ7, SPieceSCondReglNbJ8, SPieceSCondReglNbJ9, " +
     "TiersDevise,TiersCivilite,TiersExemplairesPie,Tiers_NoCompte,Tiers_CodeRepr," +
     " Tiers_GrilleTarifs,Tiers_ModeleDevis0,Tiers_ModeleDevis1,TiersModeleCommande0,TiersModeleCommande1,Tiers_ModeleBL0,Tiers_ModeleBL1,Tiers_ModeleFacture0,Tiers_ModeleFacture1," +
     "Tiers_DepotDefaut,Tiers_LienExterne,Tiers_Escompte,TiersRemsieProduits,TiersRemiseServices,TiersRemiseForfaits,Tiers_Siret,TauxTVA1,TauxTVA2,NetAPayer,reference,SPiece_codetable,dateLivraison," +
     "CodeUtilisateur,SPieceDicoVersionCr,SPieceDicoVersionCh,SPiece_bOldArrondi,SPiecebOlCalculDeee,parite,ContactLiv_Nom," +
     "SPieceSCondReglTyp0,SPieceSCondReglTyp1,SPieceSCondReglTyp2,SPieceSCondReglTyp3,SPieceSCondReglTyp4,SPieceSCondReglTyp5,SPieceSCondReglTyp6,SPieceSCondReglTyp7,SPieceSCondReglTyp8,SPieceSCondReglTyp9,SPieceSCondReglJou0," +
     " SPieceSCondReglJou1, SPieceSCondReglJou2 ,SPieceSCondReglJou3, SPieceSCondReglJou4, SPieceSCondReglJou5, SPieceSCondReglJou6, SPieceSCondReglJou7, SPieceSCondReglJou8 ,SPieceSCondReglJou9, " +
        "SPieceSCondReglPou0 ,SPieceSCondReglPou1, SPieceSCondReglPou2, SPieceSCondReglPou3, SPieceSCondReglPou4, SPieceSCondReglPou5 ,SPieceSCondReglPou6 ,SPieceSCondReglPou7, SPieceSCondReglPou8, SPieceSCondReglPou9 ," +
        "SPieceSCondReglb300, SPieceSCondReglb301 ,SPieceSCondReglb302, SPieceSCondReglb303 ,SPieceSCondReglb304, SPieceSCondReglb305, SPieceSCondReglb306, SPieceSCondReglb307, SPieceSCondReglb308, SPieceSCondReglb309,flag" +
     ") " +
     "values" +
     //Inserer une bon de commande : '','CC' 
     //Inserer une bon de commande : '','DV'
     "('','CC'," + order.Id + ",'" + order.DateCommande + "','" + client.sCliCode + "','" + client.sCliRaisonSoc + "','" + client.sCliAdresse1Ligne + "','" + client.sCliAdresse1CodePos + "','" + client.sCliAdresse1Ville + "'," +
     "" + client.sCliAdresse1CodePay + "," + client.sCli_Adresse1_NPAI + ",'" + order.adresse + "','" + order.codepostale + "','" + order.ville + "'," +
     "" + order.pays + ",'" + client.sCliNII + "','" + client.sCliFraisPort + "','" + client.sCliModeReglement + "','" + client.sCliModeReglement + "','" + client.sCondRegl_NbJours + "'," +
     "'" + client.sCondReglEchNbJour1 + "','" + client.sCondReglEchNbJour2 + "','" + client.sCondReglEchNbJour3 + "','" + client.sCondReglEchNbJour4 + "','" + client.sCondReglEchNbJour5 + "','" + client.sCondReglEchNbJour6 + "','" + client.sCondReglEchNbJour7 + "','" + client.sCondReglEchNbJour8 + "','" + client.sCondReglEchNbJour9 + "'," +
     "'" + client.sCliDevise + "','" + client.sCliCivilite + "','" + client.sCliExemplairesPiec + "','" + client.sCli_NoCompte + "','" + client.sCli_CodeRepr + "'," +
     " '" + client.sCli_GrilleTarifs + "'," + client.sCli_ModeleDevis0 + "," + client.sCli_ModeleDevis1 + "," + client.sCli_ModeleCommande0 + "," + client.sCli_ModeleCommande1 + "," + client.sCli_ModeleBL0 + "," + client.sCli_ModeleBL1 + "," + client.sCli_ModeleFacture0 + "," + client.sCli_ModeleFacture1 + "," +
     "'" + client.sCli_DepotDefaut + "','" + client.sCli_LienExterne + "'," + client.sCli_Escompte + "," + client.sCli_RemiseProduits + "," + client.sCli_RemiseServices + "," + client.sCli_RemiseForfaits + ",'" + client.sCli_Siret + "','20','5,5','" + order.MontantTotal + "','" + order.Reference + "' ,'" + order.NumCommande + "','" + order.DateLivraison + "'," +
     "'ADM',6419,6419,1,1,1,'" + order.nom_contact + "'," +
     "" + client.sCondRegl_Net + "," + client.sCondReglEchTypeEc1 + "," + client.sCondReglEchTypeEc2 + "," + client.sCondReglEchTypeEc3 + "," + client.sCondReglEchTypeEc4 + "," + client.sCondReglEchTypeEc5 + "," + client.sCondReglEchTypeEc6 + "," + client.sCondReglEchTypeEc7 + "," + client.sCondReglEchTypeEc8 + "," + client.sCondReglEchTypeEc9 + "," + client.sCondRegl_JourLe + "," +
     "" + client.sCondRegl_JourLe1 + "," + client.sCondRegl_JourLe2 + "," + client.sCondRegl_JourLe3 + "," + client.sCondRegl_JourLe4 + "," + client.sCondRegl_JourLe5 + "," + client.sCondRegl_JourLe6 + "," + client.sCondRegl_JourLe7 + "," + client.sCondRegl_JourLe8 + "," + client.sCondRegl_JourLe9 + "," + client.sCondReglEchPource0 + "," + client.sCondReglEchPource1 + "," +
      "" + client.sCondReglEchPource2 + "," + client.sCondReglEchPource3 + "," + client.sCondReglEchPource4 + "," + client.sCondReglEchPource5 + "," + client.sCondReglEchPource6 + "," + client.sCondReglEchPource7 + "," + client.sCondReglEchPource8 + "," + client.sCondReglEchPource9 + ",'" + client.b30jEquivalentMois + "','" + client.sCondReglEchb30jEq1 + "','" + client.sCondReglEchb30jEq2 + "'," +
       "'" + client.sCondReglEchb30jEq3 + "','" + client.sCondReglEchb30jEq4 + "','" + client.sCondReglEchb30jEq5 + "','" + client.sCondReglEchb30jEq6 + "','" + client.sCondReglEchb30jEq7 + "','" + client.sCondReglEchb30jEq8 + "','" + client.sCondReglEchb30jEq9 + "',char(0)" +

     ") ";
        }

        public static string insertLigneCommande(Client client, Order order, OrderLine line, int a, int b)
        {
            //string p = "#";
            string[] tab = line.descriptionArticle.Split('#'); ;
            string info_palette_objet = "";

            if (tab.Length == 2)
            {
                line.descriptionArticle = tab[0];
                info_palette_objet = tab[1];
            }

            if (a == 1)
            {
                return "Insert Into ligne " +
         "(TypeMouvement,PiecePrefixe,PieceNumero,TypeLigne,CodeArt,Libelle,CDate,Quantite,PxUnitBrut,CodeTiers,CodeRepres,DateLiv,QteCommandee,IP,Commentaire,CodeTVA,Colis,Objet,Nombre,FormuleCalcul,NombreColis)" +
         " values " +
         // "('','DV'," + order.Id + ",''
         "('','CC'," + order.Id + ",'','" + line.article.code + "','" + line.descriptionArticle + "' ,'" + order.DateCommande + "'," + line.Quantite + ",'" + line.PrixNetHT + "','" + client.sCliCode + "','" + client.sCli_CodeRepr + "','" + line.DateLivraison + "'," + line.Quantite + "," + line.NumLigne + ",'" + line.codeAcheteur + "#" + line.codeFournis + "','','" + line.article.Colisage + "','" + info_palette_objet + "','" + (float.Parse(line.Quantite, CultureInfo.InvariantCulture.NumberFormat) / int.Parse(line.article.Colisage)).ToString() + "','" + line.article.Colisage + "','" + (float.Parse(line.Quantite, CultureInfo.InvariantCulture.NumberFormat) / int.Parse(line.article.Colisage)).ToString() + "')";
            }
            else
            {
                return "Insert Into ligne " +
        "(TypeMouvement,PiecePrefixe,PieceNumero,TypeLigne,CodeArt,Libelle,CDate,Quantite,PxUnitBrut,CodeTiers,CodeRepres,DateLiv,QteCommandee,IP,Commentaire,CodeTVA,Colis,Objet,Nombre,FormuleCalcul,NombreColis)" +
        " values " +
        "('','CC'," + order.Id + ",'','" + line.article.code + "','" + line.descriptionArticle + "','" + order.DateCommande + "'," + line.Quantite + ",'" + line.PrixNetHT + "','" + client.sCliCode + "','" + client.sCli_CodeRepr + "','" + line.DateLivraison + "'," + line.Quantite + "," + line.NumLigne + ",'" + line.codeAcheteur + "#" + line.codeFournis + "','','" + line.article.Colisage + "','" + info_palette_objet + "','" + (float.Parse(line.Quantite, CultureInfo.InvariantCulture.NumberFormat) / int.Parse(line.article.Colisage)).ToString() + "','" + line.article.Colisage + "','" + (float.Parse(line.Quantite, CultureInfo.InvariantCulture.NumberFormat) / int.Parse(line.article.Colisage)).ToString() + "')";

            }
        }
        public static string insertLignePalette(Order order, int count, string info)
        {

            return "Insert Into ligne " +
     "(TypeMouvement,PiecePrefixe,PieceNumero,IP,TypeLigne,Libelle)" +
     " values " +
     "('','CC'," + order.Id + "," + count + ",'','" + info + "' )";


        }


        public static string Lists_Commandes()
        {
            return "select piece.NumeroPrefixe,piece.NumeroNumero,piece.cdate,piece.TiersCode,client.codebarcf,piece.TiersAdresse2Ligne,piece.TiersAdresse2CodePo,piece.TiersAdresse2Ville,piece.TiersAdresse2CodePa,piece.TiersDevise,piece.NetAPayer,piece.DateLivraison,piece.SPiece_CodeTable,piece.ContactLiv_Nom from piece as piece,client as client where piece.tierscode=client.sclicode and NumeroPrefixe='CC' order by numeronumero";
        }

        public static string LignesDesCommandes(string NumeroNumero)
        {
            return "SELECT ligne.ip,article.barCode,ligne.libelle,ligne.quantite,ligne.PxUnitBrut,ligne.MontantNetHT,ligne.DateLiv,ligne.commentaire FROM ligne as ligne,article as article where ligne.codeArt = article.code and ligne.PiecePrefixe='CC' and ligne.PieceNumero=" + NumeroNumero + "";
        }



        public static string SelectPays(string code)
        {
            return "SELECT code from pays where CodeDEB='" + code + "'";
        }

        public static string CodeDEB_Pays(string code)
        {
            return "SELECT CodeDEB from pays where code=" + code + "";
        }


        public static string MaxNumPiece()
        {
            return "SELECT Max(NumeroNumero) FROM PIECE where NumeroPrefixe='CC'";
            //return "SELECT Max(NumeroNumero) FROM PIECE where NumeroPrefixe='DV'";
        }

        public static string get_NumPiece_SPiece_codeTable(string num)
        {
            return "SELECT NumeroNumero FROM PIECE WHERE SPiece_codetable='" + num + "'";
        }

        public static string get_NumPiece_Motif(string num)
        {
            return "SELECT DO_PIECE FROM F_DOCENTETE WHERE DO_MOTIF='" + num + "'";
        }

        public static string get_Next_NumPiece_BonCommande()
        {
            return "SELECT DC_PIECE FROM F_DOCCURRENTPIECE WHERE DC_IDCOL=1 and DC_SOUCHE=0";
        }

        public static string ListFacturesClient(string client)
        {
            return "SELECT NumeroPrefixe,NumeroNumero,CDate,TiersCode,TiersRaisonSoc,TiersAdresse1Ligne,TiersAdresse1CodePo,TiersAdresse1Ville," +
                "TiersAdresse1CodePa,TiersAdresse2Ligne,TiersAdresse2CodePo,TiersAdresse2Ville,TiersAdresse2CodePa,TiersFraisPort,TiersModeReglement," +
     "TiersEscompte,TiersRemise,TiersDevise,TiersCivilite,Tiers_Escompte,TiersRemsieProduits,TiersRemiseServices,TiersRemiseForfaits,Tiers_Siret,TotalVolume," +
         "TotalPoids,TotalColis ,BaseTVA0,BaseTVA1,BaseTVA2,BaseTVA3,BaseTVA4,BaseTVA5,BaseTVA6,BaseTVA7,BaseTVA8,BaseTVA9,TauxTVA0,TauxTVA1,TauxTVA2,TauxTVA3," +
     "TauxTVA4,TauxTVA5,TauxTVA6,TauxTVA7,TauxTVA8,TauxTVA9,MntTVA0,MntTVA1,MntTVA2, MntTVA3,MntTVA4, MntTVA5, MntTVA6, MntTVA7,  MntTVA8,MntTVA9, Acompte, BrutHT ," +
     "TotalBrutTTC, NetAPayer, FraisPort , FraisSuppl,DateLivraison,MontantEscompte, MontantRemise , MontantRemiseTTC,DelaiLiv,TotalPoidsNet, UnitePoids ," +
     "EscompteGlobal, Contact_Civilite, Contact_Fonction , Contact_Nom, Contact_Prenom, Contact_Tel    , Contact_Fax , Contact_Portable,Contact_EMail ,     " +
     "Contact_Url  , ContactLiv_Civilite, ContactLiv_Fonction , ContactLiv_Nom  , ContactLiv_Prenom , ContactLiv_Tel    ,   ContactLiv_Fax    ,   " +
     "ContactLiv_Portable,  ContactLiv_EMail   , ContactLiv_Url, IDPaiement ,ModeTransport ,NbArticles   ,   SPieceCodeModeRegtP,SPiece_CodeTable, reference,g.dateech FROM PIECE p LEFT join gecheance g on p.NumeroNumero  = g.piecenumero where p.NumeroPrefixe LIKE 'F%' and p.ctype='' and p.TiersCode='" + client + "'";
        }

        public static string getLignes(string prefixe, string prefixenumero)
        {
            return "select line.PieceNumero,line.IP,line.CodeArt,line.Libelle,line.CDate,line.Quantite,line.PxUnitBrut,line.PxUnitBrutTTC,line.TauxRemise,line.CodeTiers,line.PrixAchat,line.Volume,line.Poids,line.Colis,line.MontantBrutHT,line.MontantBrutTTC,line.MontantNetHT,line.MontantNetTTC,line.VolumeTotal,line.PoidsTotal,line.NombreColis,line.DateLiv,line.PoidsNet,line.PoidsTotalNet,line.Devise,art.barCode from ligne line, article art where pieceprefixe = '" + prefixe + "' and Typemouvement = '' and CodeArt <> '' and piecenumero='" + prefixenumero + "' and line.CodeArt = art.Code";
        }

        public static string ListBonLivraison(string client)
        {
            return "SELECT NumeroPrefixe,NumeroNumero,CDate,TiersCode,TiersRaisonSoc,TiersAdresse1Ligne,TiersAdresse1CodePo,TiersAdresse1Ville," +
                "TiersAdresse1CodePa,TiersAdresse2Ligne,TiersAdresse2CodePo,TiersAdresse2Ville,TiersAdresse2CodePa,TiersFraisPort,TiersModeReglement," +
     "TiersEscompte,TiersRemise,TiersDevise,TiersCivilite,Tiers_Escompte,TiersRemsieProduits,TiersRemiseServices,TiersRemiseForfaits,Tiers_Siret,TotalVolume," +
         "TotalPoids,TotalColis ,BaseTVA0,BaseTVA1,BaseTVA2,BaseTVA3,BaseTVA4,BaseTVA5,BaseTVA6,BaseTVA7,BaseTVA8,BaseTVA9,TauxTVA0,TauxTVA1,TauxTVA2,TauxTVA3," +
     "TauxTVA4,TauxTVA5,TauxTVA6,TauxTVA7,TauxTVA8,TauxTVA9,MntTVA0,MntTVA1,MntTVA2, MntTVA3,MntTVA4, MntTVA5, MntTVA6, MntTVA7,  MntTVA8,MntTVA9, Acompte, BrutHT ," +
     "TotalBrutTTC, NetAPayer, FraisPort , FraisSuppl,DateLivraison,MontantEscompte, MontantRemise , MontantRemiseTTC,DelaiLiv,TotalPoidsNet, UnitePoids ," +
     "EscompteGlobal, Contact_Civilite, Contact_Fonction , Contact_Nom, Contact_Prenom, Contact_Tel    , Contact_Fax , Contact_Portable,Contact_EMail ,     " +
     "Contact_Url  , ContactLiv_Civilite, ContactLiv_Fonction , ContactLiv_Nom  , ContactLiv_Prenom , ContactLiv_Tel    ,   ContactLiv_Fax    ,   " +
     "ContactLiv_Portable,  ContactLiv_EMail   , ContactLiv_Url, IDPaiement ,ModeTransport ,NbArticles   ,   SPieceCodeModeRegtP,SPiece_CodeTable FROM PIECE where NumeroPrefixe = 'BL'  and ctype='' and TiersCode='" + client + "'";
        }

        public static string getModeTransport(string code)
        {
            return "select libelle from MODETRANSPORT where code=" + code + "";
        }

        // ******************************************************************************************

        public static string getDevise(string codeIso)
        {
            return "select CBINDICE from P_DEVISE where D_CODEISO='" + codeIso + "'";
        }

        public static string getDeviseIso(string code)
        {
            return "select D_CODEISO from P_DEVISE where CBINDICE=" + code + "";
        }

        public static string getListCommandes()
        {
            return "SELECT doc.DO_PIECE, cli.CT_EDI1, liv.LI_ADRESSE, liv.LI_CODEPOSTAL, liv.LI_CODEREGION, liv.LI_COMPLEMENT, liv.LI_VILLE, liv.LI_PAYS, doc.DO_DEVISE, doc.DO_DATE, doc.DO_DATELIVR, cond.C_MODE, doc.FNT_TOTALHTNET,doc.do_tiers,doc.do_motif " +
     "FROM F_comptet cli, P_condlivr cond, F_docentete doc, F_LIVRAISON liv " +
     "WHERE (doc.DO_DOMAINE=0) AND (doc.DO_TYPE=1) AND (doc.LI_NO=liv.LI_NO) AND (cond.CBINDICE=doc.do_condition) AND (cli.CT_NUM=doc.do_tiers)";
        }

        public static string getListLignesCommandes(string codeCommande)
        {
            return "SELECT doc.DL_LIGNE, art.AR_CODEBARRE, doc.DL_DESIGN, doc.DL_QTE, doc.DL_PRIXUNITAIRE, doc.DL_MONTANTHT, doc.DO_DATELIVR, doc.AF_REFFOURNISS, doc.AC_REFCLIENT " +
     "FROM F_ARTICLE art, F_DOCLIGNE doc " +
     "WHERE doc.AR_REF = art.AR_REF and doc.do_piece='" + codeCommande + "'";
        }


        // ******************************************************************************************

        public static string getListDocumentVente(string client, int type)
        {
            return "SELECT doc.DO_Piece,doc.DO_date,doc.DO_dateLivr,doc.DO_devise,doc.LI_No,doc.DO_Statut,doc.DO_taxe1,doc.DO_taxe2,doc.DO_taxe3,doc.DO_TypeTaxe1,doc.DO_TypeTaxe2,doc.DO_TypeTaxe3,doc.FNT_MontantEcheance,doc.FNT_MontantTotalTaxes,doc.FNT_NetAPayer,doc.FNT_PoidsBrut,doc.FNT_PoidsNet,doc.FNT_Escompte,doc.FNT_TotalHT,doc.FNT_TotalHTNet,doc.FNT_TotalTTC,liv.LI_ADRESSE, liv.LI_CODEPOSTAL, liv.LI_CODEREGION, liv.LI_COMPLEMENT, liv.LI_VILLE, liv.LI_PAYS, cond.C_MODE " +
     "FROM F_comptet cli, P_condlivr cond, F_docentete doc, F_LIVRAISON liv " +
     "WHERE (doc.DO_DOMAINE=0) AND (doc.DO_TYPE=" + type + ") AND (doc.DO_TIERS='" + client + "') AND (doc.LI_NO=liv.LI_NO) AND (cond.CBINDICE=doc.do_condition)  AND (cli.CT_NUM=doc.do_tiers)";
        }

        public static string getListDocumentVenteLine(string codeDocument)
        {
            return "SELECT doc.DO_Date,doc.DO_DateLivr,doc.DL_Ligne,doc.AR_Ref,doc.DL_Design,doc.DL_Qte,doc.DL_QteBC,doc.DL_QteBL,doc.EU_Qte,doc.DL_PoidsNet,doc.DL_PoidsBrut,doc.DL_Remise01REM_Valeur,doc.DL_Remise01REM_Type,doc.DL_Remise03REM_Valeur,doc.DL_Remise03REM_Type,doc.DL_PrixUnitaire,doc.DL_Taxe1,doc.DL_Taxe2,doc.DL_Taxe3,doc.DL_TypeTaxe1,doc.DL_TypeTaxe2,doc.DL_TypeTaxe3,doc.DL_MontantHT,doc.DL_MontantTTC,doc.DL_NoColis,doc.FNT_MontantHT,doc.FNT_MontantTaxes,doc.FNT_MontantTTC,doc.FNT_PrixUNet,doc.FNT_PrixUNetTTC,doc.FNT_RemiseGlobale,art.AR_CODEBARRE " +
     "FROM F_ARTICLE art, F_DOCLIGNE doc " +
     "WHERE doc.AR_REF = art.AR_REF and doc.do_piece='" + codeDocument + "'";
        }

        public static string getListClient()
        {
            return "SELECT CT_Num,CT_Intitule,CT_Adresse,CT_APE,CAPITAL_SOCIAL,CT_CodePostal,CT_CodeRegion,CT_Complement,CT_CONTACT,CT_EDI1,CT_email,CT_Identifiant, CT_Ville,CT_Pays,CT_Siret,CT_Telephone,N_DEVISE  FROM F_COMPTET";
        }


        #endregion
    }
}
