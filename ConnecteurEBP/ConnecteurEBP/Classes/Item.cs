
namespace ConnecteurEBP.Classes
{
    /// <summary>
    /// Classe représentant un article
    /// </summary>
    public class Item
    {
        #region Constructeurs
        /// <summary>
        /// Création d'une instance de Item
        /// </summary>
        /// <param name="id">l'identifiant de l'article</param>
        /// <param name="caption">le libellé de l'article</param>
        /// <param name="purchasePrice">le prix d'achat de l'article</param>
        /// <param name="vatAmount">le montant de TVA de l'article</param>
        /// <param name="salePriceVatIncluded">le prix de vente TTC de l'article</param>
        public Item(string id, string caption, decimal? purchasePrice, decimal? vatAmount, decimal? salePriceVatIncluded)
        {
            Id = id;
            Caption = caption;
            PurchasePrice = purchasePrice;
            VatAmount = vatAmount;
            SalePriceVatIncluded = salePriceVatIncluded;
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// Retourne et modifie l'identifiant de l'article
        /// </summary>
        public string Id { get; private set; }
        /// <summary>
        /// Retourne et modifie le libellé de l'article
        /// </summary>
        public string Caption { get; private set; }
        /// <summary>
        /// Retourne et modifie le prix d'achat de l'article
        /// </summary>
        public decimal? PurchasePrice { get; private set; }
        /// <summary>
        /// Retourne et modifie le montant de TVA de l'article
        /// </summary>
        public decimal? VatAmount { get; private set; }
        /// <summary>
        /// Retourne et modifie le prix de vente TTC de l'article
        /// </summary>
        public decimal? SalePriceVatIncluded { get; private set; }
        #endregion
    }
}
