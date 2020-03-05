using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImportPlanifier.Classes
{
    /// <summary>
    /// Classe représentant une commande
    /// </summary>
    public class Order
    {
        #region Constructeurs
        /// <summary>
        /// Création d'une instance de Order
        /// </summary>
        public Order()
        {
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// Retourne et modifie l'identifiant de la commande
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Retourne et modifie de la date de la commande
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Retourne et modifie l'identifiant du tiers rattaché à la commande
        /// </summary>
        public string ThirdId { get; set; }
        /// <summary>
        /// Retourne et modifie le nom du tiers rattaché à la commande
        /// </summary>
        public string ThirdName { get; set; }
        /// <summary>
        /// Retourne et modifie l'adresse du tiers rattaché à la commande
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Retourne et modifie le code postal du tiers rattaché à la commande
        /// </summary>
        public string ZipCode { get; set; }
        /// <summary>
        /// Retourne et modifie la ville du tiers rattaché à la commande
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Retourne et modifie le pays du tiers rattaché à la commande
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Retourne et modifie numero de la commande
        /// </summary>
        public string NumCommande { get; set; }
        /// </summary>
        /// Retourne et modifie numero de la commande
        /// </summary>
        public Boolean Different { get; set; }
        /// <summary>
        /// Retourne et modifie les lignes de vente de la commande
        /// </summary>
        public List<OrderLine> Lines { get; set; }
        #endregion
    }

    /// <summary>
    /// Classe repésentant une ligne de vente de la commande
    /// </summary>
    public class OrderLine
    {
        #region Constructeurs
        /// <summary>
        /// Création d'une instance de OrderLine
        /// </summary>
        public OrderLine()
        {
            Quantity = 1;
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// Retourne et modifie l'identifiant de l'article
        /// </summary>
        public string ItemId { get; set; }
        /// <summary>
        /// Retourne et modifie le libellé de l'article
        /// </summary>
        public string ItemCaption { get; set; }
        /// <summary>
        /// Retourne et modifie le prix de l'article
        /// </summary>
        public decimal? ItemPrice { get; set; }
        /// <summary>
        /// Retourne et modifie la quantité
        /// </summary>
        public decimal Quantity { get; set; }
        #endregion
    }
}
