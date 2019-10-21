
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurSage.Classes
{
    public class _Client
    {
        public string sCliCode { get; set; }
        public string sCliRaisonSoc { get; set; }
        public string sCliAdresse1Ligne { get; set; }
        public string sCliAdresse1CodePos { get; set; }
        public string sCliAdresse1Ville { get; set; }
        public string sCliAdresse1CodePay { get; set; }
        public string sCliAdresse2Ligne { get; set; }
        public string sCliAdresse2CodePos { get; set; }
        public string sCliAdresse2Ville { get; set; }
        public string sCliAdresse2CodePay { get; set; }
        public string sCliFraisPort { get; set; }
        public string sCliModeReglement { get; set; }
        public string sCliEscompte { get; set; }
        public string sCliRemise { get; set; }
        public string sCliDevise { get; set; }
        public string sCli_Escompte { get; set; }
        public string sCli_RemiseProduits { get; set; }
        public string sCli_RemiseServices { get; set; }
        public string sCli_RemiseForfaits { get; set; }
        public string sCli_Siret { get; set; }
        public string codebarcf { get; set; }
        public string IBAN0_Contenu { get; set; }
        public string Bq0NomBanque { get; set; }

        public _Client(string sCliCode,
        string sCliRaisonSoc,
        string sCliAdresse1Ligne,
        string sCliAdresse1CodePos,
        string sCliAdresse1Ville,
        string sCliAdresse1CodePay,
        string sCliAdresse2Ligne,
        string sCliAdresse2CodePos,
        string sCliAdresse2Ville,
        string sCliAdresse2CodePay,
        string sCliFraisPort,
        string sCliModeReglement,
        string sCliEscompte,
        string sCliRemise,
        string sCliDevise,
        string sCli_Escompte,
        string sCli_RemiseProduits,
        string sCli_RemiseServices,
        string sCli_RemiseForfaits,
        string sCli_Siret,
        string codebarcf,
        string IBAN0_Contenu,
        string Bq0NomBanque)
        {
            this.sCliCode = sCliCode;
            this.sCliRaisonSoc = sCliRaisonSoc;
            this.sCliAdresse1Ligne = sCliAdresse1Ligne;
            this.sCliAdresse1CodePos = sCliAdresse1CodePos;
            this.sCliAdresse1Ville = sCliAdresse1Ville;
            this.sCliAdresse1CodePay = sCliAdresse1CodePay;
            this.sCliAdresse2Ligne = sCliAdresse2Ligne;
            this.sCliAdresse2CodePos = sCliAdresse2CodePos;
            this.sCliAdresse2Ville = sCliAdresse2Ville;
            this.sCliAdresse2CodePay = sCliAdresse2CodePay;
            this.sCliFraisPort = sCliFraisPort;
            this.sCliModeReglement = sCliModeReglement;
            this.sCliEscompte = sCliEscompte;
            this.sCliRemise = sCliRemise;
            this.sCliDevise = sCliDevise;
            this.sCli_Escompte = sCli_Escompte;
            this.sCli_RemiseProduits = sCli_RemiseProduits;
            this.sCli_RemiseServices = sCli_RemiseServices;
            this.sCli_RemiseForfaits = sCli_RemiseForfaits;
            this.sCli_Siret = sCli_Siret;
            this.codebarcf = codebarcf;
            this.IBAN0_Contenu = IBAN0_Contenu;
            this.Bq0NomBanque = Bq0NomBanque;

        }
    }
}
