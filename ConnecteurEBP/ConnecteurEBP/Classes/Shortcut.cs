using System;
using System.IO;
using System.Security;
using System.Xml;

namespace ConnecteurEBP.Classes
{
  /// <summary>
  /// Classe représentant un raccourci de connexion au dossier EBP de l'utilisateur
  /// </summary>
  public class Shortcut
  {
    #region Fields
    /// <summary>
    /// Guid correspondant à l'application de Gestion Commerciale
    /// </summary>
    public const string InvoicingGuid = "0895452f-b7c1-4c00-a316-c6a6d0ea4bf4";
    #endregion

    #region Constructors
    /// <summary>
    /// Création d'une instance de Shortcut
    /// </summary>
    /// <param name="filename">le chemin du raccourci</param>
    public Shortcut(string filepath)
    {
      FillInfos(filepath);
    }
    #endregion

    #region Properties
    /// <summary>
    /// Retourne et modifie le nom du raccourci
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Retourne et modifie l'adresse du serveur
    /// </summary>
    public string Server { get; set; }
    /// <summary>
    /// Retourne et modifie le nom de la base de données
    /// </summary>
    public string Database { get; set; }
    /// <summary>
    /// Retourne et modifie le nom d'utilisateur
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// Retourne et modifie le mot de passe
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Retourne et modifie si l'authentification Windows est utilisée
    /// </summary>
    public bool UseWindowsAuthentication { get; set; }
    #endregion

    #region Methods
    /// <summary>
    /// Remplit les informations du raccourci d'après le chemin du fichier
    /// </summary>
    /// <param name="filepath">Chemin du raccourci</param>
    private void FillInfos(string filepath)
    {
      try
      {
        try
        {
          //Vérification du raccourci
          if (string.IsNullOrEmpty(filepath) || Path.GetExtension(filepath) != ".ebp")
            throw new SDKException("Le fichier ne correspond pas à un raccourci EBP.");
        }
        catch (ArgumentException)
        {
        }
        //Traitement du raccourci et récupération des informations de connexion
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filepath);
        bool missingInvoicingGuid = false;
        XmlNodeList nodeList = xmlDoc.GetElementsByTagName("SchemaId");
        if (nodeList == null)
          throw new SDKException("Une erreur est survenue lors du traitement du raccourci.");
        foreach (XmlNode node in nodeList)
        {
          if (node.InnerText == InvoicingGuid)
          {
            missingInvoicingGuid = true;
            Name = Path.GetFileNameWithoutExtension(filepath);
            //Récupération de la chaine de connexion
            XmlNode connectionStringNode = node.ParentNode.LastChild;
            if (connectionStringNode == null || connectionStringNode.Name != "ConnectionString")
              throw new SDKException("Une erreur est survenue lors du traitement du raccourci.");
            string connectionString = connectionStringNode.InnerText;
            //Récupération du mode de connexion à la base de données
            if (connectionString.ToLower().Contains("sspi"))
              UseWindowsAuthentication = true;
            else
            {
              UseWindowsAuthentication = false;
              Username = "sa";
              Password = "@ebp78EBP";
            }
            string[] connectionStringSegments = connectionString.Split(';');
            //Récupération de l'adresse du serveyr
            string server = Array.Find(connectionStringSegments, s => s.Contains("source"));
            if (string.IsNullOrEmpty(server))
              throw new SDKException("Une erreur est survenue lors du traitement du raccourci.");
            string[] serverSegments = server.Split('=');
            if (string.IsNullOrEmpty(serverSegments[1]))
              throw new SDKException("Une erreur est survenue lors du traitement du raccourci.");
            Server = serverSegments[1];
            //Récupération du nom de la base de données
            string database = Array.Find(connectionStringSegments, s => s.Contains("database"));
            if (string.IsNullOrEmpty(database))
              throw new SDKException("Une erreur est survenue lors du traitement du raccourci.");
            string[] databaseSegments = database.Split('=');
            if (string.IsNullOrEmpty(databaseSegments[1]))
              throw new SDKException("Une erreur est survenue lors du traitement du raccourci.");
            Database = databaseSegments[1];
          }
        }
        if (!missingInvoicingGuid)
          throw new SDKException("Le fichier ne correspond pas à un raccourci d'un dossier Gestion Commerciale");
      }
      catch (XmlException e)
      {
        //Exceptions pouvant survenir lors du chargement du fichier XML
        throw new SDKException(e.Message);
      }
      catch (IOException e)
      {
        //Exceptions pouvant survenir si le nom du fichier est trop long, ou s'il est introuvable
        throw new SDKException(e.Message);
      }
      catch (UnauthorizedAccessException e)
      {
        //Exceptions pouvant survenir si le fichier est en lecture seule, ou si le fichier spécifié est un répertoire
        throw new SDKException(e.Message);
      }
      catch (NotSupportedException e)
      {
        //Exception pouvant survenir si le format du fichier est incorrect
        throw new SDKException(e.Message);
      }
      catch (SecurityException e)
      {
        //Exception pouvant survenir lorsque l'utilisateur n'a pas les permissions requises
        throw new SDKException(e.Message);
      }
    }
    #endregion
  }
}
