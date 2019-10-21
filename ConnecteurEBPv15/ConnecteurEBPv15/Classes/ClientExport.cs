using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurSage.Classes
{
    public class ClientExport
    {
        public string sCliCode { get; set; }
        public string CodeBarCF { get; set; }
        public string sCliRaisonSoc { get; set; }

        // adresse
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
        public string sCliFraisPort { get; set; }
        public string sCliModeReglement { get; set; }
        public string sCliDevise { get; set; }
        public string sCliCivilite { get; set; }
        public string sCli_NoCompte { get; set; }
        public string sCli_CodeRepr { get; set; }
        public string sCli_GrilleTarifs { get; set; }
        public string sCli_LienExterne { get; set; }
        public string sCli_siret { get; set; }

        // BANQUE 0
        public string BIC0_Banque { get; set; }
        public string BIC0_Pays { get; set; }
        public string BIC0_Localication { get; set; }
        public string BIC0_Branche { get; set; }
        public string IBAN0_Pays { get; set; }
        public string IBAN0_Controle { get; set; }
        public string IBAN0_Contenu { get; set; }
        public string Bq0NomBanque { get; set; }
        public string Bq0AdrBanque { get; set; }
        public string CB0_AdresseSuite { get; set; }

        // BANQUE 1
        public string BIC1_Banque { get; set; }
        public string BIC1_Pays { get; set; }
        public string BIC1_Localication { get; set; }
        public string BIC1_Branche { get; set; }
        public string IBAN1_Pays { get; set; }
        public string IBAN1_Controle { get; set; }
        public string IBAN1_Contenu { get; set; }
        public string Bq1NomBanque { get; set; }
        public string Bq1AdrBanque { get; set; }
        public string CB1_AdresseSuite { get; set; }

        // BANQUE 2
        public string BIC2_Banque { get; set; }
        public string BIC2_Pays { get; set; }
        public string BIC2_Localication { get; set; }
        public string BIC2_Branche { get; set; }
        public string IBAN2_Pays { get; set; }
        public string IBAN2_Controle { get; set; }
        public string IBAN2_Contenu { get; set; }
        public string Bq2NomBanque { get; set; }
        public string Bq2AdrBanque { get; set; }
        public string CB2_AdresseSuite { get; set; }

        // BANQUE 3
        public string BIC3_Banque { get; set; }
        public string BIC3_Pays { get; set; }
        public string BIC3_Localication { get; set; }
        public string BIC3_Branche { get; set; }
        public string IBAN3_Pays { get; set; }
        public string IBAN3_Controle { get; set; }
        public string IBAN3_Contenu { get; set; }
        public string Bq3NomBanque { get; set; }
        public string Bq3AdrBanque { get; set; }
        public string CB3_AdresseSuite { get; set; }

        public List<Adresse> ListAdresse { get; set; }
        public List<Contact> ListContact { get; set; }

        public ClientExport(string sCliCode,
        string CodeBarCF,
        string sCliRaisonSoc,
        string sCliAdresse1Ligne,
        string sCliAdresse1CodePos,
        string sCliAdresse1Ville,
        string sCliAdresse1CodePay,
        string sCli_Adresse1_NPAI,
        string sCliAdresse2Ligne,
        string sCliAdresse2CodePos,
        string sCliAdresse2Ville,
        string sCliAdresse2CodePay,
        string sCli_Adresse2_NPAI,
        string sCliType,
        string sCliFraisPort,
        string sCliModeReglement,
        string sCliDevise,
        string sCliCivilite,
        string sCli_NoCompte,
        string sCli_CodeRepr,
        string sCli_GrilleTarifs,
        string sCli_LienExterne,
        string sCli_siret,
        string BIC0_Banque,
        string BIC0_Pays,
        string BIC0_Localication,
        string BIC0_Branche,
        string IBAN0_Pays,
        string IBAN0_Controle,
        string IBAN0_Contenu,
        string Bq0NomBanque,
        string Bq0AdrBanque,
        string CB0_AdresseSuite,
        string BIC1_Banque,
        string BIC1_Pays,
        string BIC1_Localication,
        string BIC1_Branche,
        string IBAN1_Pays,
        string IBAN1_Controle,
        string IBAN1_Contenu,
        string Bq1NomBanque,
        string Bq1AdrBanque,
        string CB1_AdresseSuite,
        string BIC2_Banque,
        string BIC2_Pays,
        string BIC2_Localication,
        string BIC2_Branche,
        string IBAN2_Pays,
        string IBAN2_Controle,
        string IBAN2_Contenu,
        string Bq2NomBanque,
        string Bq2AdrBanque,
        string CB2_AdresseSuite,
        string BIC3_Banque,
        string BIC3_Pays,
        string BIC3_Localication,
        string BIC3_Branche,
        string IBAN3_Pays,
        string IBAN3_Controle,
        string IBAN3_Contenu,
        string Bq3NomBanque,
        string Bq3AdrBanque,
        string CB3_AdresseSuite)
        {
            this.sCliCode = sCliCode;
            this.CodeBarCF = CodeBarCF;
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
            this.sCliFraisPort = sCliFraisPort;
            this.sCliModeReglement = sCliModeReglement;
            this.sCliDevise = sCliDevise;
            this.sCliCivilite = sCliCivilite;
            this.sCli_NoCompte = sCli_NoCompte;
            this.sCli_CodeRepr = sCli_CodeRepr;
            this.sCli_GrilleTarifs = sCli_GrilleTarifs;
            this.sCli_LienExterne = sCli_LienExterne;
            this.sCli_siret = sCli_siret;
            this.BIC0_Banque = BIC0_Banque;
            this.BIC0_Pays = BIC0_Pays;
            this.BIC0_Localication = BIC0_Localication;
            this.BIC0_Branche = BIC0_Branche;
            this.IBAN0_Pays = IBAN0_Pays;
            this.IBAN0_Controle = IBAN0_Controle;
            this.IBAN0_Contenu = IBAN0_Contenu;
            this.Bq0NomBanque = Bq0NomBanque;
            this.Bq0AdrBanque = Bq0AdrBanque;
            this.CB0_AdresseSuite = CB0_AdresseSuite;
            this.BIC1_Banque = BIC1_Banque;
            this.BIC1_Pays = BIC1_Pays;
            this.BIC1_Localication = BIC1_Localication;
            this.BIC1_Branche = BIC1_Branche;
            this.IBAN1_Pays = IBAN1_Pays;
            this.IBAN1_Controle = IBAN1_Controle;
            this.IBAN1_Contenu = IBAN1_Contenu;
            this.Bq1NomBanque = Bq1NomBanque;
            this.Bq1AdrBanque = Bq1AdrBanque;
            this.CB1_AdresseSuite = CB1_AdresseSuite;
            this.BIC2_Banque = BIC2_Banque;
            this.BIC2_Pays = BIC2_Pays;
            this.BIC2_Localication = BIC2_Localication;
            this.BIC2_Branche = BIC2_Branche;
            this.IBAN2_Pays = IBAN2_Pays;
            this.IBAN2_Controle = IBAN2_Controle;
            this.IBAN2_Contenu = IBAN2_Contenu;
            this.Bq2NomBanque = Bq2NomBanque;
            this.Bq2AdrBanque = Bq2AdrBanque;
            this.CB2_AdresseSuite = CB2_AdresseSuite;
            this.BIC3_Banque = BIC3_Banque;
            this.BIC3_Pays = BIC3_Pays;
            this.BIC3_Localication = BIC3_Localication;
            this.BIC3_Branche = BIC3_Branche;
            this.IBAN3_Pays = IBAN3_Pays;
            this.IBAN3_Controle = IBAN3_Controle;
            this.IBAN3_Contenu = IBAN3_Contenu;
            this.Bq3NomBanque = Bq3NomBanque;
            this.Bq3AdrBanque = Bq3AdrBanque;
            this.CB3_AdresseSuite = CB3_AdresseSuite;
        }

        public ClientExport()
        {
            // TODO: Complete member initialization
        }


    }

    public class Adresse
    {
        public string Adresse_Ligne { get; set; }
        public string Adresse_CodePostal { get; set; } 
        public string Adresse_Ville  { get; set; }                   
        public string Adresse_CodePays  { get; set; }
        public string Adresse_NPAI { get; set; } 
        public string bPrincipal { get; set; } 
        public string bFacturation  { get; set; }
        public string CodeInterne { get; set; }

        public Adresse(string Adresse_Ligne,
        string Adresse_CodePostal, 
        string Adresse_Ville ,                   
        string Adresse_CodePays ,
        string Adresse_NPAI, 
        string bPrincipal, 
        string bFacturation ,
        string CodeInterne)
        {
        this.Adresse_Ligne=Adresse_Ligne;
        this.Adresse_CodePostal = Adresse_CodePostal;
        this.Adresse_Ville = Adresse_Ville;
        this.Adresse_CodePays = Adresse_CodePays;
        this.Adresse_NPAI = Adresse_NPAI;
        this.bPrincipal = bPrincipal;
        this.bFacturation = bFacturation;
        this.CodeInterne = CodeInterne;
        }

        public Adresse()
        {
            // TODO: Complete member initialization
        }
    }

    public class Contact
    {
        public string sContact_Civilite { get; set; }   
        public string sContact_Interloc { get; set; }                                                  
        public string sContact_Fonction   { get; set; }              
        public string sContact_Nom  { get; set; }                                      
        public string sContact_Prenom  { get; set; }               
        public string sContact_Tel  { get; set; }        
        public string sContact_Fax   { get; set; }      
        public string sContact_Portable  { get; set; }   
        public string sContact_EMail  { get; set; }                                                                 
        public string sContact_Url   { get; set; }                                                                  
        public string sContact_Password  { get; set; }
        public string bPrincipal  { get; set; }
        public string bPrincipalLiv  { get; set; }
        public string AdresseFacturation  { get; set; }
        public string AdresseLivraison { get; set; }

        public Contact( string sContact_Civilite,   
        string sContact_Interloc,                                                  
        string sContact_Fonction  ,              
        string sContact_Nom ,                                      
        string sContact_Prenom ,               
        string sContact_Tel ,        
        string sContact_Fax  ,      
        string sContact_Portable ,   
        string sContact_EMail ,                                                                 
        string sContact_Url  ,                                                                  
        string sContact_Password ,
        string bPrincipal ,
        string bPrincipalLiv ,
        string AdresseFacturation ,
        string AdresseLivraison)
        {
        this.sContact_Civilite=  sContact_Civilite ;   
        this.sContact_Interloc= sContact_Interloc  ;                                                  
        this.sContact_Fonction  = sContact_Fonction  ;              
        this.sContact_Nom = sContact_Nom  ;                                      
        this.sContact_Prenom = sContact_Prenom  ;               
        this.sContact_Tel =  sContact_Tel ;        
        this.sContact_Fax  = sContact_Fax  ;      
        this.sContact_Portable = sContact_Portable  ;   
        this.sContact_EMail =  sContact_EMail ;                                                                 
        this.sContact_Url  = sContact_Url  ;                                                                  
        this.sContact_Password = sContact_Password  ;
        this.bPrincipal =  bPrincipal ;
        this.bPrincipalLiv = bPrincipalLiv  ;
        this.AdresseFacturation = AdresseFacturation  ;
        this.AdresseLivraison = AdresseLivraison;
        }
    }
}
