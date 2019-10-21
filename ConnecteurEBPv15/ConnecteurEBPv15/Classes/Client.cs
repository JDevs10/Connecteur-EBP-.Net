using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurSage.Classes
{
    public class Client
    {
        #region Propriétés

        public string CT_Num { get; set; }

        public string CG_NumPrinc { get; set; }

        public string CT_NumPayeur { get; set; }

        public string N_Condition { get; set; }

        public string N_Devise { get; set; }

        public string N_Expedition { get; set; }

        public string CT_Langue { get; set; }

        public string CT_Facture { get; set; }

        public string N_Period { get; set; }

        public string N_CatTarif { get; set; }

        public string CT_Taux02 { get; set; }

        public string N_CatCompta { get; set; }

        public string CT_EDI1 { get; set; }

        #endregion


        public Client(string CT_Num, string CG_NumPrinc, string CT_NumPayeur, string N_Condition, string N_Devise, string N_Expedition, string CT_Langue, string CT_Facture, string N_Period, string N_CatTarif, string CT_Taux02, string N_CatCompta)
        {
            this.CT_Num = CT_Num;
            this.CG_NumPrinc = CG_NumPrinc;
            this.CT_NumPayeur = CT_NumPayeur;
            this.N_Condition = N_Condition;
            this.N_Devise = N_Devise;
            this.N_Expedition = N_Expedition;
            this.CT_Langue = CT_Langue;
            this.CT_Facture = CT_Facture;
            this.N_Period = N_Period;
            this.N_CatTarif = N_CatTarif;
            this.CT_Taux02 = CT_Taux02;
            this.N_CatCompta = N_CatCompta;
        }


        public Client(string CT_Num, string CT_EDI1, string CG_NumPrinc, string CT_NumPayeur, string N_Condition, string N_Devise, string N_Expedition, string CT_Langue, string CT_Facture, string N_Period, string N_CatTarif, string CT_Taux02, string N_CatCompta)
        {
            this.CT_Num = CT_Num;
            this.CG_NumPrinc = CG_NumPrinc;
            this.CT_NumPayeur = CT_NumPayeur;
            this.N_Condition = N_Condition;
            this.N_Devise = N_Devise;
            this.N_Expedition = N_Expedition;
            this.CT_Langue = CT_Langue;
            this.CT_Facture = CT_Facture;
            this.N_Period = N_Period;
            this.N_CatTarif = N_CatTarif;
            this.CT_Taux02 = CT_Taux02;
            this.N_CatCompta = N_CatCompta;
            this.CT_EDI1 = CT_EDI1;
        }

        public char charTest { get; set; }
        public string sCliCode { get; set; }
        public string sCliRaisonSoc { get; set; }
        public string sCliAdresse1Ligne { get; set; }
        public string sCliAdresse1CodePos { get; set; }
        public string sCliAdresse1Ville { get; set; }
        public string sCliAdresse1CodePay { get; set; }
        public string sCli_Adresse1_NPAI { get; set; }
        public string sCliAdresse2Ligne { get; set; }
        public string sCliAdresse2CodePos { get; set; }
        public string sCliAdresse2Ville { get; set; }
        public string sCliAdresse2CodePay { get; set; }
        public string sCli_Adresse2_NPAI { get; set; }
        public string sCliType { get; set; }
        public string sCliNII { get; set; }
        public string sCliFraisPort { get; set; }
        public string sCliModeReglement { get; set; }
        public string sCliEscompte { get; set; }
        public string sCliRemise { get; set; }
        public string bRemiseTTC { get; set; }
        public string sCliDevise { get; set; }
        public string sCliCivilite { get; set; }
        public string sCliExemplairesPiec { get; set; }
        public string sCli_NoCompte { get; set; }
        public string sCli_CodeRepr { get; set; }
        public string sCli_GrilleTarifs { get; set; }
        public string sCli_ModeleDevis0 { get; set; }
        public string sCli_ModeleDevis1 { get; set; }
        public string sCli_ModeleCommande0 { get; set; }
        public string sCli_ModeleCommande1 { get; set; }
        public string sCli_ModeleBL0 { get; set; }
        public string sCli_ModeleBL1 { get; set; }
        public string sCli_ModeleFacture0 { get; set; }
        public string sCli_ModeleFacture1 { get; set; }
        public string sCli_DepotDefaut { get; set; }
        public string sCli_LienExterne { get; set; }
        public string sCli_Escompte { get; set; }
        public string sCli_RemiseProduits { get; set; }
        public string sCli_RemiseServices { get; set; }
        public string sCli_RemiseForfaits { get; set; }
        public string sCli_Siret { get; set; }
        public string codebarcf { get; set; }
        public string sCondRegl_NbJours { get; set; }
        public string sCondReglEchNbJour1 { get; set; }
        public string sCondReglEchNbJour2 { get; set; }
        public string sCondReglEchNbJour3 { get; set; }
        public string sCondReglEchNbJour4 { get; set; }
        public string sCondReglEchNbJour5 { get; set; }
        public string sCondReglEchNbJour6 { get; set; }
        public string sCondReglEchNbJour7 { get; set; }
        public string sCondReglEchNbJour8 { get; set; }
        public string sCondReglEchNbJour9 { get; set; }
        public string sCondRegl_Net { get; set; }
        public string sCondReglEchTypeEc1 { get; set; }
        public string sCondReglEchTypeEc2 { get; set; }
        public string sCondReglEchTypeEc3 { get; set; }
        public string sCondReglEchTypeEc4 { get; set; }
        public string sCondReglEchTypeEc5 { get; set; }
        public string sCondReglEchTypeEc6 { get; set; }
        public string sCondReglEchTypeEc7 { get; set; }
        public string sCondReglEchTypeEc8 { get; set; }
        public string sCondReglEchTypeEc9 { get; set; }
        public string sCondRegl_JourLe { get; set; }
        public string sCondRegl_JourLe1 { get; set; }
        public string sCondRegl_JourLe2 { get; set; }
        public string sCondRegl_JourLe3 { get; set; }
        public string sCondRegl_JourLe4 { get; set; }
        public string sCondRegl_JourLe5 { get; set; }
        public string sCondRegl_JourLe6 { get; set; }
        public string sCondRegl_JourLe7 { get; set; }
        public string sCondRegl_JourLe8 { get; set; }
        public string sCondRegl_JourLe9 { get; set; }
        public string sCondReglEchPource0 { get; set; }
        public string sCondReglEchPource1 { get; set; }
        public string sCondReglEchPource2 { get; set; }
        public string sCondReglEchPource3 { get; set; }
        public string sCondReglEchPource4 { get; set; }
        public string sCondReglEchPource5 { get; set; }
        public string sCondReglEchPource6 { get; set; }
        public string sCondReglEchPource7 { get; set; }
        public string sCondReglEchPource8 { get; set; }
        public string sCondReglEchPource9 { get; set; }
        public string b30jEquivalentMois { get; set; }
        public string sCondReglEchb30jEq1 { get; set; }
        public string sCondReglEchb30jEq2 { get; set; }
        public string sCondReglEchb30jEq3 { get; set; }
        public string sCondReglEchb30jEq4 { get; set; }
        public string sCondReglEchb30jEq5 { get; set; }
        public string sCondReglEchb30jEq6 { get; set; }
        public string sCondReglEchb30jEq7 { get; set; }
        public string sCondReglEchb30jEq8 { get; set; }
        public string sCondReglEchb30jEq9 { get; set; }

        public Client(
        string sCliCode
            , string sCliRaisonSoc
            , string sCliAdresse1Ligne
            , string sCliAdresse1CodePos
            , string sCliAdresse1Ville
            , string sCliAdresse1CodePay
            , string sCli_Adresse1_NPAI
            , string sCliAdresse2Ligne
            , string sCliAdresse2CodePos
            , string sCliAdresse2Ville
            , string sCliAdresse2CodePay
            , string sCli_Adresse2_NPAI
            , string sCliType
            , string sCliNII
         , string sCliFraisPort
             , string sCliModeReglement
               , string sCliEscompte
         , string sCliRemise
            , string bRemiseTTC,
            string sCliDevise
           , string sCliCivilite
            , string sCliExemplairesPiec
            , string sCli_NoCompte
           , string sCli_CodeRepr
            , string sCli_GrilleTarifs
            , string sCli_ModeleDevis0
             , string sCli_ModeleDevis1
              , string sCli_ModeleCommande0
             , string sCli_ModeleCommande1,
            string sCli_ModeleBL0
               , string sCli_ModeleBL1
                , string sCli_ModeleFacture0
                 , string sCli_ModeleFacture1
               , string sCli_DepotDefaut
                , string sCli_LienExterne
                , string sCli_Escompte
                , string sCli_RemiseProduits
                , string sCli_RemiseServices
                , string sCli_RemiseForfaits
            , string sCli_Siret, 
            string codebarcf, 
            string sCondRegl_NbJours,
             string sCondReglEchNbJour1
            , string sCondReglEchNbJour2
            , string sCondReglEchNbJour3
            , string sCondReglEchNbJour4
            , string sCondReglEchNbJour5
            , string sCondReglEchNbJour6
            , string sCondReglEchNbJour7
            , string sCondReglEchNbJour8
            , string sCondReglEchNbJour9
             , string sCondRegl_Net
            , string sCondReglEchTypeEc1
            , string sCondReglEchTypeEc2
            , string sCondReglEchTypeEc3
            , string sCondReglEchTypeEc4
            , string sCondReglEchTypeEc5
            , string sCondReglEchTypeEc6
            , string sCondReglEchTypeEc7
            , string sCondReglEchTypeEc8
            , string sCondReglEchTypeEc9
            , string sCondRegl_JourLe
               , string  sCondRegl_JourLe1
            , string sCondRegl_JourLe2
           , string  sCondRegl_JourLe3
            , string sCondRegl_JourLe4
            , string sCondRegl_JourLe5
            , string sCondRegl_JourLe6
            , string sCondRegl_JourLe7
            , string sCondRegl_JourLe8
            , string sCondRegl_JourLe9
           , string  sCondReglEchPource0
            , string sCondReglEchPource1
            , string sCondReglEchPource2
            , string sCondReglEchPource3
            , string sCondReglEchPource4
            , string sCondReglEchPource5
            , string sCondReglEchPource6
            , string sCondReglEchPource7
            , string sCondReglEchPource8
            , string sCondReglEchPource9
            , string b30jEquivalentMois
            , string sCondReglEchb30jEq1
            , string sCondReglEchb30jEq2
            , string sCondReglEchb30jEq3
            , string sCondReglEchb30jEq4
           , string  sCondReglEchb30jEq5
            , string sCondReglEchb30jEq6
            , string sCondReglEchb30jEq7
            , string sCondReglEchb30jEq8
            , string sCondReglEchb30jEq9)
        {
            this.sCliCode = sCliCode;
            this.sCliRaisonSoc = sCliRaisonSoc;
            this.sCliAdresse1Ligne = sCliAdresse1Ligne;
            this.sCliAdresse1CodePos = sCliAdresse1CodePos;
            this.sCliAdresse1Ville = sCliAdresse1Ville;
            this.sCliAdresse1CodePay = sCliAdresse1CodePay;
            this.sCli_Adresse1_NPAI = sCli_Adresse1_NPAI;
            this.sCliAdresse2Ligne = sCliAdresse2Ligne;
            this.sCliAdresse2CodePos = sCliAdresse2CodePos;
            this.sCliAdresse2Ville = sCliAdresse2Ville;
            this.sCliAdresse2CodePay = sCliAdresse2CodePay;
            this.sCli_Adresse2_NPAI = sCli_Adresse2_NPAI;
            this.sCliType = sCliType;
            this.sCliNII = sCliNII;
            this.sCliFraisPort = sCliFraisPort;
            this.sCliModeReglement = sCliModeReglement;
            this.sCliEscompte = sCliEscompte;
            this.sCliRemise = sCliRemise;
            this.bRemiseTTC = bRemiseTTC;
            this.sCliDevise = sCliDevise;
            this.sCliCivilite = sCliCivilite;
            this.sCliExemplairesPiec = sCliExemplairesPiec;
            this.sCli_NoCompte = sCli_NoCompte;
            this.sCli_CodeRepr = sCli_CodeRepr;
            this.sCli_GrilleTarifs = sCli_GrilleTarifs;
            this.sCli_ModeleDevis0 = sCli_ModeleDevis0;
            this.sCli_ModeleDevis1 = sCli_ModeleDevis1;
            this.sCli_ModeleCommande0 = sCli_ModeleCommande0;
            this.sCli_ModeleCommande1 = sCli_ModeleCommande1;
            this.sCli_ModeleBL0 = sCli_ModeleBL0;
            this.sCli_ModeleBL1 = sCli_ModeleBL1;
            this.sCli_ModeleFacture0 = sCli_ModeleFacture0;
            this.sCli_ModeleFacture1 = sCli_ModeleFacture1;
            this.sCli_DepotDefaut = sCli_DepotDefaut;
            this.sCli_LienExterne = sCli_LienExterne;
            this.sCli_Escompte = sCli_Escompte;
            this.sCli_RemiseProduits = sCli_RemiseProduits;
            this.sCli_RemiseServices = sCli_RemiseServices;
            this.sCli_RemiseForfaits = sCli_RemiseForfaits;
            this.sCli_Siret = sCli_Siret;
            this.codebarcf = codebarcf;
            this.sCondRegl_NbJours = sCondRegl_NbJours;
            this.sCondReglEchNbJour1 = sCondReglEchNbJour1;
            this.sCondReglEchNbJour2 = sCondReglEchNbJour2;
            this.sCondReglEchNbJour3 = sCondReglEchNbJour3;
            this.sCondReglEchNbJour4 = sCondReglEchNbJour4;
            this.sCondReglEchNbJour5 = sCondReglEchNbJour5;
            this.sCondReglEchNbJour6 = sCondReglEchNbJour6;
            this.sCondReglEchNbJour7 = sCondReglEchNbJour7;
            this.sCondReglEchNbJour8 = sCondReglEchNbJour8;
            this.sCondReglEchNbJour9 = sCondReglEchNbJour9;
            this.sCondRegl_Net = sCondRegl_Net;
            this.sCondReglEchTypeEc1 = sCondReglEchTypeEc1;
            this.sCondReglEchTypeEc2 = sCondReglEchTypeEc2;
            this.sCondReglEchTypeEc3 = sCondReglEchTypeEc3;
            this.sCondReglEchTypeEc4 = sCondReglEchTypeEc4;
            this.sCondReglEchTypeEc5 = sCondReglEchTypeEc5;
            this.sCondReglEchTypeEc6 = sCondReglEchTypeEc6;
            this.sCondReglEchTypeEc7 = sCondReglEchTypeEc7;
            this.sCondReglEchTypeEc8 = sCondReglEchTypeEc8;
            this.sCondReglEchTypeEc9 = sCondReglEchTypeEc9;
            this.sCondRegl_JourLe = sCondRegl_JourLe;
            this.sCondRegl_JourLe1 = sCondRegl_JourLe1;
            this.sCondRegl_JourLe2 =sCondRegl_JourLe2;
            this.sCondRegl_JourLe3 =sCondRegl_JourLe3;
            this.sCondRegl_JourLe4 =sCondRegl_JourLe4;
            this.sCondRegl_JourLe5 =sCondRegl_JourLe5;
            this.sCondRegl_JourLe6 =sCondRegl_JourLe6;
            this.sCondRegl_JourLe7 =sCondRegl_JourLe7;
            this.sCondRegl_JourLe8 =sCondRegl_JourLe8;
            this.sCondRegl_JourLe9 =sCondRegl_JourLe9;
            this.sCondReglEchPource0 = sCondReglEchPource0;
            this.sCondReglEchPource1 = sCondReglEchPource1;
            this.sCondReglEchPource2 = sCondReglEchPource2;
            this.sCondReglEchPource3 = sCondReglEchPource3;
            this.sCondReglEchPource4 = sCondReglEchPource4;
            this.sCondReglEchPource5 = sCondReglEchPource5;
            this.sCondReglEchPource6 = sCondReglEchPource6;
            this.sCondReglEchPource7 = sCondReglEchPource7;
            this.sCondReglEchPource8 = sCondReglEchPource8;
            this.sCondReglEchPource9 = sCondReglEchPource9;
            this.b30jEquivalentMois = b30jEquivalentMois;
            this.sCondReglEchb30jEq1 = sCondReglEchb30jEq1;
            this.sCondReglEchb30jEq2 = sCondReglEchb30jEq2;
            this.sCondReglEchb30jEq3 = sCondReglEchb30jEq3;
            this.sCondReglEchb30jEq4 = sCondReglEchb30jEq4;
            this.sCondReglEchb30jEq5 =sCondReglEchb30jEq5;
            this.sCondReglEchb30jEq6 = sCondReglEchb30jEq6;
            this.sCondReglEchb30jEq7 = sCondReglEchb30jEq7;
            this.sCondReglEchb30jEq8 =sCondReglEchb30jEq8;
            this.sCondReglEchb30jEq9 =sCondReglEchb30jEq9;

        }
    }
}
