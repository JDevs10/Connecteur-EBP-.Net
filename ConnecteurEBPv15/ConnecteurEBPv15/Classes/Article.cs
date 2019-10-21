using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurSage.Classes
{
    public class Article
    {
        public string AR_REF { get; set; }
        public string gamme1 { get; set; }
        public string gamme2 { get; set; }
        public string AR_SuiviStock { get; set; }
        public string AR_Nomencl { get; set; }
        public string AR_StockId { get; set; }
        public string AR_REFCompose { get; set; }
        public string RP_CODEDEFAUT { get; set; }
        public string AR_PRIXVEN { get; set; }

        public string code { get; set; }
        public string description { get; set; }
        public string prixVente { get; set; }
        public string Colisage { get; set; }
        public string typebarcode { get; set; }
        // nbrRouleaux = poidsUnite dans la base
        public string nbrRouleaux { get; set; }

        public Article(string code, string description, string prixVente, string Colisage, string typebarcode,
            string nbrRouleaux)
        {
            this.code = code;
            this.description = description;
            this.prixVente = prixVente;
            this.Colisage = Colisage;
            this.typebarcode = typebarcode;
            this.nbrRouleaux = nbrRouleaux;
        }

        public Article()
        {
        }

        public Article(string AR_REF, string AR_SuiviStock, string gamme1, string gamme2, string AR_Nomencl, string RP_CODEDEFAUT, string AR_PRIXVEN)
        {
            this.AR_REF = AR_REF;
            this.gamme1 = gamme1;
            this.gamme2 = gamme2;
            this.AR_SuiviStock = AR_SuiviStock;
            this.AR_Nomencl = AR_Nomencl;
            this.AR_REFCompose = "";
            this.RP_CODEDEFAUT = RP_CODEDEFAUT;
            this.AR_PRIXVEN = AR_PRIXVEN;
  
        }

    }
}
