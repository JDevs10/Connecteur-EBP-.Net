using System;
using System.Collections.Generic;

namespace ConnecteurEBP.Classes
{
  /// <summary>
  /// Classe représentant une commande
  /// </summary>
  public class FactureVente
  {
    #region Constructeurs
    /// <summary>
    /// Création d'une instance de Order
    /// </summary>

      public FactureVente(string id,string customerName, decimal? a, decimal? b, decimal? c, decimal? d, decimal? e, decimal? f)
        {
            Id = id;
            CustomerName = customerName;
            AmountVatExcludedWithDiscountAndShipping = a;
            VatAmount = b;
            AmountVatIncluded = c;
            DepositAmount = d;
            TotalDueAmount = e;
            CommitmentsBalanceDue = f;
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

    /// <summary>
    /// Retourne et modifie l'identifiant du tiers rattaché à la commande
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// Retourne et modifie le nom du tiers rattaché à la commande
    /// </summary>
    public decimal? AmountVatExcludedWithDiscountAndShipping { get; set; }
    /// <summary>
    /// Retourne et modifie l'adresse du tiers rattaché à la commande
    /// </summary>
    public decimal? VatAmount { get; set; }
    /// <summary>
    /// Retourne et modifie le code postal du tiers rattaché à la commande
    /// </summary>
    public decimal? AmountVatIncluded { get; set; }
    /// <summary>
    /// Retourne et modifie la ville du tiers rattaché à la commande
    /// </summary>
    public decimal? DepositAmount { get; set; }
    /// <summary>
    /// Retourne et modifie le pays du tiers rattaché à la commande
    /// </summary>
    public decimal? TotalDueAmount { get; set; }
    /// <summary>
    /// Retourne et modifie les lignes de vente de la commande
    /// </summary>
    public decimal? CommitmentsBalanceDue { get; set; }
    #endregion
  }


}
