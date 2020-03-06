using System;
using System.IO;
using System.Security;
using System.Xml.Serialization;
using ConnecteurEBP.Utilities;

namespace ConnecteurEBP.Classes
{
    /// <summary>
    /// Classe représentant les paramètres de l'utilisateur
    /// </summary>
    [Serializable]
    public class Settings
    {
        #region Champs privés
        /// <summary>
        /// Instance privée du singleton
        /// </summary>
        private static Settings instance;
        /// <summary>
        /// Objet pour garantir l'accès exclusif à des opérations
        /// </summary>
        private static object syncRoot = new object();
        /// <summary>
        /// Retourne et modifie le chemin du fichier de paramètre de l'utilisateur
        /// </summary>
        private static string SDKSettingsFile = Path.Combine(
          Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), @"EBP\SDKSettings_Invoicing.xml");
        #endregion

        #region Constructeurs
        /// <summary>
        /// Création d'une instance de Settings
        /// </summary>
        private Settings()
        {
            ShortcutPath = string.Empty;
            ApplicationPassword = string.Empty;
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// Retourne l'instance du singleton
        /// </summary>
        public static Settings Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = Load();
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Indique si les paramètres ont été renseignés
        /// </summary>
        public static bool IsSettingsDefined
        {
            get
            {
                try
                {
                    return (!string.IsNullOrEmpty(Instance.Server) && !string.IsNullOrEmpty(Instance.Database) &&
                                ((!Instance.UseWindowsAuthentication && !string.IsNullOrEmpty(Instance.SQLServerUsername)
                                && !string.IsNullOrEmpty(Instance.SQLServerPassword)) || Instance.UseWindowsAuthentication));
                }
                catch (SDKException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Indique si l'exécutable a été renseigné
        /// </summary>
        public static bool IsExecutableDefined
        {
            get
            {
                try
                {
                    return (Instance.Application != null && !string.IsNullOrEmpty(Instance.Application.ExecutablePath));
                }
                catch (SDKException)
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Retourne et modifie l'adresse du serveur
        /// </summary>
        public string Server { get; set; }
        /// <summary>
        /// Retourne et modifie le nom de la base de données
        /// </summary>
        public string Database { get; set; }
        /// <summary>
        /// Retourne et modifie le nom d'utilisateur SQL Server
        /// </summary>
        public string SQLServerUsername { get; set; }
        /// <summary>
        /// Retourne et modifie le mot de passe SQL Server
        /// </summary>
        [XmlIgnore]
        public string SQLServerPassword
        {
            get
            {
                return Utils.Decrypt(SQLServerCryptedPassword);
            }
            set
            {
                SQLServerCryptedPassword = Utils.Encrypt(value);
            }
        }
        /// <summary>
        /// Retourne et modifie le mot de passe encrypté SQL Server
        /// </summary>
        public string SQLServerCryptedPassword { get; set; }
        /// <summary>
        /// Retourne et modifie si l'authentification Windows est utilisée
        /// </summary>
        public bool UseWindowsAuthentication { get; set; }
        /// <summary>
        /// Retourne et modifie le chemin du shortcut utilisé
        /// </summary>
        public string ShortcutPath { get; set; }
        /// <summary>
        /// Retourne et modifie le login de l'utilisateur de l'application de Gestion Commerciale
        /// </summary>
        public string ApplicationLogin { get; set; }
        /// <summary>
        /// Retourne et modifie le mot de passe de l'utilisateur de l'application de Gestion Commerciale
        /// </summary>
        [XmlIgnore]
        public string ApplicationPassword
        {
            get
            {
                return Utils.Decrypt(ApplicationCryptedPassword);
            }
            set
            {
                ApplicationCryptedPassword = Utils.Encrypt(value);
            }
        }
        /// <summary>
        /// Retourne et modifie le mot de passe encrypté de l'utilisateur de l'application de Gestion Commerciale
        /// </summary>
        public string ApplicationCryptedPassword { get; set; }
        /// <summary>
        /// Retourne et modifie le produit EBP utilisé
        /// </summary>
        public Application Application { get; set; }
        #endregion

        #region Méthodes
        /// <summary>
        /// Sauvegarde des paramètres utilisateurs dans le fichier de paramètres
        /// </summary>
        public void Save()
        {
            try
            {
                //Création du répertoire EBP s'il n'existe pas
                if (!Directory.Exists(Path.GetDirectoryName(SDKSettingsFile)))
                    Directory.CreateDirectory(Path.GetDirectoryName(SDKSettingsFile));
                XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                //Sérialisation de la classe Settings dans le fichier de paramètres
                using (StreamWriter streamWriter = new StreamWriter(SDKSettingsFile))
                {
                    serializer.Serialize(streamWriter, this);
                }
            }
            catch (IOException e)
            {
                //Exceptions pouvant survenir si le fichier n'existe pas, si le nom est trop long ou
                //si le nom contient des caractères non permis
                throw new SDKException(e.Message, e);
            }
            catch (UnauthorizedAccessException e)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                throw new SDKException(e.Message, e);
            }
            catch (SecurityException e)
            {
                //Exception pouvant survenir lorsque l'utilisateur n'a pas les permissions requises
                throw new SDKException(e.Message, e);
            }
            catch (NotSupportedException e)
            {
                //Exception pouvant survenir lorsque le chemin du répertoire à créer contient le caractère ":"
                throw new SDKException(e.Message, e);
            }
        }

        /// <summary>
        /// Chargement des paramètres utilisateurs depuis le fichier de paramètres
        /// </summary>
        private static Settings Load()
        {
            try
            {
                //Création du fichier de paramètres s'il n'existe pas
                if (!File.Exists(SDKSettingsFile))
                    return new Settings();
                else
                {
                    //Désérialisation du fichier de paramètres dans l'objet Settings
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    using (StreamReader streamReader = new StreamReader(SDKSettingsFile))
                    {
                        return serializer.Deserialize(streamReader) as Settings;
                    }
                }
            }
            catch (IOException e)
            {
                //Exceptions pouvant survenir si le fichier n'existe pas, si le nom est trop long ou
                //si le nom contient des caractères non permis
                throw new SDKException(e.Message, e);
            }
            catch (UnauthorizedAccessException e)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                throw new SDKException(e.Message, e);
            }
            catch (NotSupportedException e)
            {
                //Exception pouvant survenir si le caractère ":" est présent dans le nom du fichier
                throw new SDKException(e.Message, e);
            }
            catch (SecurityException e)
            {
                //Exception pouvant survenir lorsqu'une erreur de sécurité est déclenchée
                throw new SDKException(e.Message, e);
            }
            catch (InvalidOperationException e)
            {
                //Exception déclenchée durant la désérialisation
                throw new SDKException(e.Message, e);
            }
        }

        /// <summary>
        /// Vérification de la validité des paramètres utilisateurs
        /// </summary>
        /// <returns>bool</returns>
        public static bool CheckSettings()
        {

            if (!Settings.IsExecutableDefined)
            {
                throw new SDKException("L'application par défaut doit être définie dans les paramètres");
            }
            if (!Settings.IsSettingsDefined)
            {
                throw new SDKException("Les paramètres de connexion à la source de données doivent être définis.");
            }
            return true;
        }
        #endregion
    }
}