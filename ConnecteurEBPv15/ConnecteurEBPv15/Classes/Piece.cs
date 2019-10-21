using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurSage.Classes
{
    public class Piece
    {
        public string NumeroPrefixe{get; set;}
        public string NumeroNumero{get; set;}
        public string cdate{get; set;}
        public string TiersCode{get; set;}
        public string CodebarCT { get; set; }
        public string TiersAdresse2Ligne{get; set;}
        public string TiersAdresse2CodePo{get; set;}
        public string TiersAdresse2Ville{get; set;}
        public string TiersAdresse2CodePa{get; set;}
        public string TiersDevise{get; set;}
        public string NetAPayer{get; set;}
        public string DateLivraison{get; set;}
        public string SPiece_CodeTable { get; set; }
        public string ContactLiv_Nom { get; set; }
        public List<Ligne> Lines  { get; set; }

        public Piece( string NumeroPrefixe,
        string NumeroNumero,
        string cdate,
        string TiersCode,
        string CodebarCT,
        string TiersAdresse2Ligne,
        string TiersAdresse2CodePo,
        string TiersAdresse2Ville,
        string TiersAdresse2CodePa,
        string TiersDevise,
        string NetAPayer,
        string DateLivraison,
        string SPiece_CodeTable,
        string ContactLiv_Nom)
        {
        this.NumeroPrefixe =  NumeroPrefixe  ;
        this.NumeroNumero = NumeroNumero   ;
        this.cdate =cdate;
        this.TiersCode =TiersCode;
        this.CodebarCT = CodebarCT;
        this.TiersAdresse2Ligne = TiersAdresse2Ligne   ;
        this.TiersAdresse2CodePo = TiersAdresse2CodePo  ;
        this.TiersAdresse2Ville = TiersAdresse2Ville   ;
        this.TiersAdresse2CodePa =  TiersAdresse2CodePa  ;
        this.TiersDevise =  TiersDevise  ;
        this.NetAPayer =  NetAPayer  ;
        this.DateLivraison = DateLivraison   ;
        this.SPiece_CodeTable = SPiece_CodeTable;
        this.ContactLiv_Nom = ContactLiv_Nom;

        }
    }

    public class Ligne
    {
        public string ip { get; set; }
        public string barCode { get; set; }
        public string libelle { get; set; }
        public string quantite { get; set; }
        public string PxUnitBrut { get; set; }
        public string MontantNetHT { get; set; }
        public string DateLiv { get; set; }
        public string commentaire { get; set; }

        public Ligne(string ip,
        string barCode,
        string libelle,
        string quantite,
        string PxUnitBrut,
        string MontantNetHT,
        string DateLiv,
        string commentaire)
        {
            this.ip= ip ;
        this.barCode= barCode ;
        this.libelle= libelle ;
        this.quantite=quantite  ;
        this.PxUnitBrut= PxUnitBrut ;
        this.MontantNetHT=MontantNetHT  ;
        this.DateLiv = DateLiv;
        this.commentaire = commentaire;
        }

    }
}
