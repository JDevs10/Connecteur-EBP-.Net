using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnecteurEBP.Classes
{
    public class Order
    {
        #region Propriétés
        public string Id { get; set; }
        public string NumCommande { get; set; }
        public string codeClient { get; set; }
        public string NomClient { get; set; }
        public string codeFournisseur { get; set; }
        public string villeReference { get; set; }
        public string adresseLivraison { get; set; }
        public string deviseCommande { get; set; }
        public string DateCommande { get; set; }
        public string MontantTotal { get; set; }
        public string StockId { get; set; }
        public string DateLivraison { get; set; }
        public string conditionLivraison { get; set; }
        public string Reference { get; set; }
        public string commentaires { get; set; }
        public string ChampPerso { get; set; }
        public List<OrderLine> Lines { get; set; }

        // Numero de commande enregistrer dans do_motif
        public string DO_MOTIF { get; set; }
        public string nom_contact { get; set; }
        public string adresse { get; set; }
        public string codepostale { get; set; }
        public string ville { get; set; }
        public string pays { get; set; }

        #endregion


        #region Constructeurs
        /// <summary>
        /// Création d'une instance de Order
        /// </summary>
        public Order()
        {
            StockId = "0";
            deviseCommande = "0";
        }

        public Order(string NumCommande, string codeClient, string adresseLivraison, string deviseCommande, string DateCommande, string DateLivraison, string conditionLivraison, string MontantTotal, string NomClient)
        {
            this.NumCommande = NumCommande;
            this.codeClient = codeClient;
            this.adresseLivraison = adresseLivraison;
            this.deviseCommande = deviseCommande;
            this.DateCommande = DateCommande;
            this.DateLivraison = DateLivraison;
            this.conditionLivraison = conditionLivraison;
            this.MontantTotal = MontantTotal;
            this.NomClient = NomClient;
        }

        public Order(string NumCommande, string codeClient, string adresseLivraison, string deviseCommande, string DateCommande, string DateLivraison, string conditionLivraison, string MontantTotal, string NomClient, string DO_MOTIF)
        {
            this.NumCommande = NumCommande;
            this.codeClient = codeClient;
            this.adresseLivraison = adresseLivraison;
            this.deviseCommande = deviseCommande;
            this.DateCommande = DateCommande;
            this.DateLivraison = DateLivraison;
            this.conditionLivraison = conditionLivraison;
            this.MontantTotal = MontantTotal;
            this.NomClient = NomClient;
            this.DO_MOTIF = DO_MOTIF;
        }

        #endregion

    }

    public class OrderLine
    {
        #region Propriétés
        public string NumLigne { get; set; }
        public string codeArticle { get; set; }
        public string descriptionArticle { get; set; }
        public string Quantite { get; set; }
        public string PrixNetHT { get; set; }
        public string Prix { get; set; }
        public string MontantLigne { get; set; }
        public string DateLivraison { get; set; }
        public string codeAcheteur { get; set; }
        public string codeFournis { get; set; }
        public Article article { get; set; }
        public string TypeQuantite { get; set; }

        #endregion

        #region Constructeurs
        public OrderLine()
        {
            Quantite = "1";
        }

        public OrderLine(string NumLigne,
        string codeArticle,
        string descriptionArticle,
        string Quantite,
        string PrixNetHT,
        string MontantLigne,
        string DateLivraison,
        string codeFournis,
        string codeAcheteur)
        {
            this.NumLigne = NumLigne;
            this.codeArticle = codeArticle;
            this.descriptionArticle = descriptionArticle;
            this.Quantite = Quantite;
            this.PrixNetHT = PrixNetHT;
            this.MontantLigne = MontantLigne;
            this.DateLivraison = DateLivraison;
            this.codeFournis = codeFournis;
            this.codeAcheteur = codeAcheteur;
        }
        #endregion

    }
}
