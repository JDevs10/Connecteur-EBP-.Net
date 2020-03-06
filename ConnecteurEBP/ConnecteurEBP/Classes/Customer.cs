
namespace ConnecteurEBP.Classes
{
  /// <summary>
  /// Classe représentant un client
  /// </summary>
  public class Customer
  {
    #region Constructeurs
    /// <summary>
    /// Création d'une instance de Customer
    /// </summary>
    public Customer()
    {
    }

    /// <summary>
    /// Création d'une instance de Customer
    /// </summary>
    /// <param name="id">l'identifiant du client</param>
    /// <param name="name">le nom du client</param>
    /// <param name="sales">le chiffre d'affaires du client</param>
    public Customer(string id, string name, decimal? sales)
    {
      Id = id;
      Name = name;
      Sales = sales;
    }
    #endregion

    #region Propriétés
    /// <summary>
    /// Retourne et modifie l'identifiant du client
    /// </summary>
    public string Id { get; set; }
    /// <summary>
    /// Retourne et modifie le nom du client
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Retourne et modifie l'adresse du client
    /// </summary>
    public string Address { get; set; }
    /// <summary>
    /// Retourne et modifie le code postal du client
    /// </summary>
    public string ZipCode { get; set; }
    /// <summary>
    /// Retourne et modifie la ville du client
    /// </summary>
    public string City { get; set; }
    /// <summary>
    /// Retourne et modifie le pays du client
    /// </summary>
    public string Country { get; set; }
    /// <summary>
    /// Retourne et modifie le chiffre d'affaires du client
    /// </summary>
    public decimal? Sales { get; set; }
    #endregion
  }
}
