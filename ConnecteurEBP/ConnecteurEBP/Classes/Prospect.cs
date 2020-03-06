
namespace ConnecteurEBP.Classes
{
  /// <summary>
  /// Classe représentant un prospect
  /// </summary>
  public class Prospect
  {
    #region Constructeurs
    /// <summary>
    /// Création d'une instance de Prospect
    /// </summary>
    /// <param name="name">le nom du prospect</param>
    /// <param name="firstname">le prénom du prospect</param>
    /// <param name="email">l'adresse mail du prospect</param>
    public Prospect(string name, string firstname, string email)
    {
      Name = name;
      Firstname = firstname;
      Email = email;
    }
    #endregion

    #region Propriétés
    /// <summary>
    /// Retourne et modifie le nom du prospect
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Retourne et modifie le prénom du prospect
    /// </summary>
    public string Firstname { get; private set; }
    /// <summary>
    /// Retourne et modifie l'adresse mail du prospect
    /// </summary>
    public string Email { get; private set; }
    #endregion
  }
}
