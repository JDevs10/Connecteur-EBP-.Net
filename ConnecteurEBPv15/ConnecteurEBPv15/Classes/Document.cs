using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurSage.Classes
{
    public class Document
    {
        public string NumeroPrefixe { get; set; }
        public string NumeroNumero { get; set; }
        public string CDate { get; set; }
        public string TiersCode { get; set; }
        public string TiersRaisonSoc { get; set; }
        public string TiersAdresse1Ligne { get; set; }
        public string TiersAdresse1CodePo { get; set; }
        public string TiersAdresse1Ville { get; set; }
        public string TiersAdresse1CodePa { get; set; }
        public string TiersAdresse2Ligne { get; set; }
        public string TiersAdresse2CodePo { get; set; }
        public string TiersAdresse2Ville { get; set; }
        public string TiersAdresse2CodePa { get; set; }
        public string TiersFraisPort { get; set; }
        public string TiersModeReglement { get; set; }
        public string TiersEscompte { get; set; }
        public string TiersRemise { get; set; }
        public string TiersDevise { get; set; }
        public string TiersCivilite { get; set; }
        public string Tiers_Escompte { get; set; }
        public string TiersRemsieProduits { get; set; }
        public string TiersRemiseServices { get; set; }
        public string TiersRemiseForfaits { get; set; }
        public string Tiers_Siret { get; set; }
        public string TotalVolume { get; set; }
        public string TotalPoids { get; set; }
        public string TotalColis { get; set; }
        public string BaseTVA0 { get; set; }
        public string BaseTVA1 { get; set; }
        public string BaseTVA2 { get; set; }
        public string BaseTVA3 { get; set; }
        public string BaseTVA4 { get; set; }
        public string BaseTVA5 { get; set; }
        public string BaseTVA6 { get; set; }
        public string BaseTVA7 { get; set; }
        public string BaseTVA8 { get; set; }
        public string BaseTVA9 { get; set; }
        public string TauxTVA0 { get; set; }
        public string TauxTVA1 { get; set; }
        public string TauxTVA2 { get; set; }
        public string TauxTVA3 { get; set; }
        public string TauxTVA4 { get; set; }
        public string TauxTVA5 { get; set; }
        public string TauxTVA6 { get; set; }
        public string TauxTVA7 { get; set; }
        public string TauxTVA8 { get; set; }
        public string TauxTVA9 { get; set; }
        public string MntTVA0 { get; set; }
        public string MntTVA1 { get; set; }
        public string MntTVA2 { get; set; }
        public string MntTVA3 { get; set; }
        public string MntTVA4 { get; set; }
        public string MntTVA5 { get; set; }
        public string MntTVA6 { get; set; }
        public string MntTVA7 { get; set; }
        public string MntTVA8 { get; set; }
        public string MntTVA9 { get; set; }
        public string Acompte { get; set; }
        public string BrutHT { get; set; }
        public string TotalBrutTTC { get; set; }
        public string NetAPayer { get; set; }
        public string FraisPort { get; set; }
        public string FraisSuppl { get; set; }
        public string DateLivraison { get; set; }
        public string MontantEscompte { get; set; }
        public string MontantRemise { get; set; }
        public string MontantRemiseTTC { get; set; }
        public string DelaiLiv { get; set; }
        public string TotalPoidsNet { get; set; }
        public string UnitePoids { get; set; }
        public string EscompteGlobal { get; set; }
        public string Contact_Civilite { get; set; }
        public string Contact_Fonction { get; set; }
        public string Contact_Nom { get; set; }
        public string Contact_Prenom { get; set; }
        public string Contact_Tel { get; set; }
        public string Contact_Fax { get; set; }
        public string Contact_Portable { get; set; }
        public string Contact_EMail { get; set; }
        public string Contact_Url { get; set; }
        public string ContactLiv_Civilite { get; set; }
        public string ContactLiv_Fonction { get; set; }
        public string ContactLiv_Nom { get; set; }
        public string ContactLiv_Prenom { get; set; }
        public string ContactLiv_Tel { get; set; }
        public string ContactLiv_Fax { get; set; }
        public string ContactLiv_Portable { get; set; }
        public string ContactLiv_EMail { get; set; }
        public string ContactLiv_Url { get; set; }
        public string IDPaiement { get; set; }
        public string ModeTransport { get; set; }
        public string NbArticles { get; set; }
        public string SPieceCodeModeRegtP { get; set; }
        public string SPiece_CodeTable { get; set; }
        public List<Lignes> lines { get; set; }

        public string Reference { get; set; }

        public string DateEch { get; set; }
        public Document(
         string NumeroPrefixe,
         string NumeroNumero,
         string CDate,
         string TiersCode,
         string TiersRaisonSoc,
         string TiersAdresse1Ligne,
         string TiersAdresse1CodePo,
         string TiersAdresse1Ville,
         string TiersAdresse1CodePa,
         string TiersAdresse2Ligne,
         string TiersAdresse2CodePo,
         string TiersAdresse2Ville,
         string TiersAdresse2CodePa,
         string TiersFraisPort,
         string TiersModeReglement,
         string TiersEscompte,
         string TiersRemise,
         string TiersDevise,
         string TiersCivilite,
         string Tiers_Escompte,
         string TiersRemsieProduits,
         string TiersRemiseServices,
         string TiersRemiseForfaits,
         string Tiers_Siret,
         string TotalVolume,
         string TotalPoids,
         string TotalColis,
         string BaseTVA0,
         string BaseTVA1,
         string BaseTVA2,
         string BaseTVA3,
         string BaseTVA4,
         string BaseTVA5,
         string BaseTVA6,
         string BaseTVA7,
         string BaseTVA8,
         string BaseTVA9,
         string TauxTVA0,
         string TauxTVA1,
         string TauxTVA2,
         string TauxTVA3,
         string TauxTVA4,
         string TauxTVA5,
         string TauxTVA6,
         string TauxTVA7,
         string TauxTVA8,
         string TauxTVA9,
         string MntTVA0,
         string MntTVA1,
         string MntTVA2,
         string MntTVA3,
         string MntTVA4,
         string MntTVA5,
         string MntTVA6,
         string MntTVA7,
         string MntTVA8,
         string MntTVA9,
         string Acompte,
         string BrutHT,
         string TotalBrutTTC,
         string NetAPayer,
         string FraisPort,
         string FraisSuppl,
         string DateLivraison,
         string MontantEscompte,
         string MontantRemise,
         string MontantRemiseTTC,
         string DelaiLiv,
         string TotalPoidsNet,
         string UnitePoids,
         string EscompteGlobal,
         string Contact_Civilite,
         string Contact_Fonction,
         string Contact_Nom,
         string Contact_Prenom,
         string Contact_Tel,
         string Contact_Fax,
         string Contact_Portable,
         string Contact_EMail,
         string Contact_Url,
         string ContactLiv_Civilite,
         string ContactLiv_Fonction,
         string ContactLiv_Nom,
         string ContactLiv_Prenom,
         string ContactLiv_Tel,
         string ContactLiv_Fax,
         string ContactLiv_Portable,
         string ContactLiv_EMail,
         string ContactLiv_Url,
         string IDPaiement,
         string ModeTransport,
         string NbArticles,
         string SPieceCodeModeRegtP,
         string SPiece_CodeTable,
         string Reference,
         string DateEch)
        {
            this.NumeroPrefixe = NumeroPrefixe;
            this.NumeroNumero = NumeroNumero;
            this.CDate = CDate;
            this.TiersCode = TiersCode;
            this.TiersRaisonSoc = TiersRaisonSoc;
            this.TiersAdresse1Ligne = TiersAdresse1Ligne;
            this.TiersAdresse1CodePo = TiersAdresse1CodePo;
            this.TiersAdresse1Ville = TiersAdresse1Ville;
            this.TiersAdresse1CodePa = TiersAdresse1CodePa;
            this.TiersAdresse2Ligne = TiersAdresse2Ligne;
            this.TiersAdresse2CodePo = TiersAdresse2CodePo;
            this.TiersAdresse2Ville = TiersAdresse2Ville;
            this.TiersAdresse2CodePa = TiersAdresse2CodePa;
            this.TiersFraisPort = TiersFraisPort;
            this.TiersModeReglement = TiersModeReglement;
            this.TiersEscompte = TiersEscompte;
            this.TiersRemise = TiersRemise;
            this.TiersDevise = TiersDevise;
            this.TiersCivilite = TiersCivilite;
            this.Tiers_Escompte = Tiers_Escompte;
            this.TiersRemsieProduits = TiersRemsieProduits;
            this.TiersRemiseServices = TiersRemiseServices;
            this.TiersRemiseForfaits = TiersRemiseForfaits;
            this.Tiers_Siret = Tiers_Siret;
            this.TotalVolume = TotalVolume;
            this.TotalPoids = TotalPoids;
            this.TotalColis = TotalColis;
            this.BaseTVA0 = BaseTVA0;
            this.BaseTVA1 = BaseTVA1;
            this.BaseTVA2 = BaseTVA2;
            this.BaseTVA3 = BaseTVA3;
            this.BaseTVA4 = BaseTVA4;
            this.BaseTVA5 = BaseTVA5;
            this.BaseTVA6 = BaseTVA6;
            this.BaseTVA7 = BaseTVA7;
            this.BaseTVA8 = BaseTVA8;
            this.BaseTVA9 = BaseTVA9;
            this.TauxTVA0 = TauxTVA0;
            this.TauxTVA1 = TauxTVA1;
            this.TauxTVA2 = TauxTVA2;
            this.TauxTVA3 = TauxTVA3;
            this.TauxTVA4 = TauxTVA4;
            this.TauxTVA5 = TauxTVA5;
            this.TauxTVA6 = TauxTVA6;
            this.TauxTVA7 = TauxTVA7;
            this.TauxTVA8 = TauxTVA8;
            this.TauxTVA9 = TauxTVA9;
            this.MntTVA0 = MntTVA0;
            this.MntTVA1 = MntTVA1;
            this.MntTVA2 = MntTVA2;
            this.MntTVA3 = MntTVA3;
            this.MntTVA4 = MntTVA4;
            this.MntTVA5 = MntTVA5;
            this.MntTVA6 = MntTVA6;
            this.MntTVA7 = MntTVA7;
            this.MntTVA8 = MntTVA8;
            this.MntTVA9 = MntTVA9;
            this.Acompte = Acompte;
            this.BrutHT = BrutHT;
            this.TotalBrutTTC = TotalBrutTTC;
            this.NetAPayer = NetAPayer;
            this.FraisPort = FraisPort;
            this.FraisSuppl = FraisSuppl;
            this.DateLivraison = DateLivraison;
            this.MontantEscompte = MontantEscompte;
            this.MontantRemise = MontantRemise;
            this.MontantRemiseTTC = MontantRemiseTTC;
            this.DelaiLiv = DelaiLiv;
            this.TotalPoidsNet = TotalPoidsNet;
            this.UnitePoids = UnitePoids;
            this.EscompteGlobal = EscompteGlobal;
            this.Contact_Civilite = Contact_Civilite;
            this.Contact_Fonction = Contact_Fonction;
            this.Contact_Nom = Contact_Nom;
            this.Contact_Prenom = Contact_Prenom;
            this.Contact_Tel = Contact_Tel;
            this.Contact_Fax = Contact_Fax;
            this.Contact_Portable = Contact_Portable;
            this.Contact_EMail = Contact_EMail;
            this.Contact_Url = Contact_Url;
            this.ContactLiv_Civilite = ContactLiv_Civilite;
            this.ContactLiv_Fonction = ContactLiv_Fonction;
            this.ContactLiv_Nom = ContactLiv_Nom;
            this.ContactLiv_Prenom = ContactLiv_Prenom;
            this.ContactLiv_Tel = ContactLiv_Tel;
            this.ContactLiv_Fax = ContactLiv_Fax;
            this.ContactLiv_Portable = ContactLiv_Portable;
            this.ContactLiv_EMail = ContactLiv_EMail;
            this.ContactLiv_Url = ContactLiv_Url;
            this.IDPaiement = IDPaiement;
            this.ModeTransport = ModeTransport;
            this.NbArticles = NbArticles;
            this.SPieceCodeModeRegtP = SPieceCodeModeRegtP;
            this.SPiece_CodeTable = SPiece_CodeTable;
            this.Reference = Reference;
            this.DateEch = DateEch;
        }
    }
}
