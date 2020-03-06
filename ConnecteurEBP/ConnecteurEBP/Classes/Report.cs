using System;

namespace ConnecteurEBP.Classes
{
  /// <summary>
  /// Classe représentant un modèle d'impression
  /// </summary>
  public class Report
  {
    #region Constructeurs
    /// <summary>
    /// Création d'une instance de Report
    /// </summary>
    /// <param name="id">l'identifiant du modèle</param>
    /// <param name="label">le libellé du modèle</param>
    public Report(Guid id, string label)
    {
      Id = id;
      Label = label;
    }
    #endregion

    #region Propriétés
    /// <summary>
    /// Retourne et modifie l'identifiant du modèle
    /// </summary>
    public Guid Id { get; private set; }
    /// <summary>
    /// Retourne et modifie le libellé du modèle
    /// </summary>
    public string Label { get; private set; }
    #endregion
  }
}
