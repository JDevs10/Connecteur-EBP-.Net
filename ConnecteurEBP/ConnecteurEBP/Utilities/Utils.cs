using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using ConnecteurEBP.Classes;
using Microsoft.Win32;
using System.Configuration;

namespace ConnecteurEBP.Utilities
{
    /// <summary>
    /// Classe statique utilitaire
    /// </summary>
    public static class Utils
    {
        #region Champs privés
        /// <summary>
        /// Mot-clé de chiffrement utilisé pour le cryptage
        /// </summary>
        private static string CryptKey = "@SDK$";
        #endregion

        #region Méthodes de cryptage
        /// <summary>
        /// Crypte une chaine de caractère en utilisant une clé de cryptage
        /// </summary>
        /// <param name="original">la chaine à crypter</param>
        /// <returns>La chaine cryptée</returns>
        public static string Encrypt(string original)
        {
            try
            {
                MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();
                byte[] passwordHash = hashMd5.ComputeHash(
                Encoding.Default.GetBytes(CryptKey));
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = passwordHash;
                des.Mode = CipherMode.ECB;
                byte[] buffer = Encoding.Default.GetBytes(original);
                return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (CryptographicException e)
            {
                //Survient lorsque le fournisseur de cryptographie n'est pas disponible
                MessageBox.Show(e.Message);
                return string.Empty;
            }
            catch (EncoderFallbackException e)
            {
                MessageBox.Show(e.Message);
                return string.Empty;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message);
                return string.Empty;
            }
        }

        /// <summary>
        /// Décrypte une chaine en utilisant une clé de cryptage
        /// </summary>
        /// <param name="encrypted">la chaîne à décrypter</param>
        /// <returns>la chaine décryptée</returns>
        public static string Decrypt(string encrypted)
        {
            if (string.IsNullOrEmpty(encrypted))
                return string.Empty;
            try
            {
                encrypted = Encoding.Default.GetString(Convert.FromBase64String(encrypted));
                MD5CryptoServiceProvider hashMd5 = new MD5CryptoServiceProvider();
                byte[] passwordHash = hashMd5.ComputeHash(Encoding.Default.GetBytes(CryptKey));
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = passwordHash;
                des.Mode = CipherMode.ECB;
                byte[] buffer = Encoding.Default.GetBytes(encrypted);
                return Encoding.Default.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (CryptographicException e)
            {
                //Survient lorsque le fournisseur de cryptographie n'est pas disponible
                MessageBox.Show(e.Message);
                return string.Empty;
            }
            catch (DecoderFallbackException e)
            {
                MessageBox.Show(e.Message);
                return string.Empty;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show(e.Message);
                return string.Empty;
            }
        }
        #endregion

        #region Méthodes diverses
        /// <summary>
        /// Récupération des produits compatibles avec l'exécution des lignes de commandes à partir du fichier de configuration
        /// </summary>
        /// <returns>La liste des produits compatibles et installés sur la poste utilisateur</returns>
        public static List<ConnecteurEBP.Classes.Application> GetCompatibleProducts()
        {
            string productType = ConfigurationManager.AppSettings["ProductType"].ToLower();
            double productVersion;
            if (!double.TryParse(ConfigurationManager.AppSettings["Version"].Replace('.', ','), out productVersion))
                throw new FormatException("Impossible de convertir la version du produit en type Double");
            List<string> authorizedIds = new List<string>();
            authorizedIds.Add(ConfigurationManager.AppSettings["PMEId"]);
            authorizedIds.Add(ConfigurationManager.AppSettings["ProId"]);
            authorizedIds.Add(ConfigurationManager.AppSettings["ClassicId"]);
            authorizedIds.Add(ConfigurationManager.AppSettings["DevisId"]);
            authorizedIds.Add(ConfigurationManager.AppSettings["PMEAIId"]);
            authorizedIds.Add(ConfigurationManager.AppSettings["ProAIId"]);
            authorizedIds.Add(ConfigurationManager.AppSettings["ClassicAIId"]);
            authorizedIds.Add(ConfigurationManager.AppSettings["DevisAIId"]);
            //Récupération des applications EBP installées, dans la base de registre
            try
            {
                List<ConnecteurEBP.Classes.Application> applications = new List<ConnecteurEBP.Classes.Application>();
                RegistryKey key = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Ebp").OpenSubKey("Applications");
                //Parcours des Guids
                foreach (string keyName in key.GetSubKeyNames())
                {
                    if (!authorizedIds.Contains(keyName))
                        continue;
                    RegistryKey subKey = key.OpenSubKey(keyName);
                    //Parcours des numéros de versions
                    foreach (string subKeyName in key.OpenSubKey(keyName).GetSubKeyNames())
                    {
                        double version;
                        if (!double.TryParse(subKeyName.Replace('.', ','), out version))
                            continue;
                        if (version < productVersion)
                            continue;
                        RegistryKey subSubKey = subKey.OpenSubKey(subKeyName);
                        if (subSubKey.GetValue("Path") != null && subSubKey.GetValue("Path").ToString().ToLower().Contains(productType))
                        {
                            if (!File.Exists(subSubKey.GetValue("Path").ToString()))
                                continue;
                            ConnecteurEBP.Classes.Application application = new ConnecteurEBP.Classes.Application();
                            application.Id = new Guid(keyName);
                            application.ExecutablePath = subSubKey.GetValue("Path").ToString();
                            application.ProductId = new Guid(subSubKey.GetValue("ProductId").ToString());
                            application.Version = subKeyName;
                            application.ProductName = GetProductName(application.ProductId.ToString(), subSubKey.GetValue("ProductVersion").ToString());
                            applications.Add(application);
                        }
                    }
                }
                return applications;
            }
            catch (SecurityException e)
            {
                //Exception pouvant survenir lorsque l'utilisateur n'a pas les permissions requises pour lire dans la base de registre
                MessageBox.Show(e.Message, "Security Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new SDKException(e.Message, e);
                return new List<ConnecteurEBP.Classes.Application>();
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.Message, "Null Reference Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new SDKException("Aucun produit EBP compatible n'a pu être trouvé", e);
                return new List<ConnecteurEBP.Classes.Application>();
            }
            catch (TypeInitializationException e)
            {
                MessageBox.Show(e.Message, "Type Initialization Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //throw new SDKException(e.Message, e);
                return new List<ConnecteurEBP.Classes.Application>();
            }
        }

        /// <summary>
        /// Indique si des applications sont compatibles avec le SDK
        /// </summary>
        /// <returns>Retourne True si au moins une application compatible est trouvée, sinon False</returns>
        public static bool IsCompatibleProductsExists()
        {
            return GetCompatibleProducts().Count > 0;
        }

        /// <summary>
        /// Compare deux numéros de version
        /// </summary>
        /// <param name="mostRecentVersion">le numéro de version censé être le plus récent</param>
        /// <param name="oldestVersion">le numéro de version censé être le plus vieux</param>
        /// <returns>Retourne True si </returns>
        public static bool IsMostRecentVersion(string mostRecentVersion, string oldestVersion)
        {
            double mostRecentVersionNum;
            double oldestVersionNum;
            if (!double.TryParse(mostRecentVersion.Replace('.', ','), out mostRecentVersionNum) || !double.TryParse(oldestVersion.Replace('.', ','), out oldestVersionNum))
                throw new FormatException("Impossible de convertir un numéro de version en format décimal");

            return mostRecentVersionNum > oldestVersionNum;
        }   

        /// <summary>
        /// Récupère le nom de produit depuis la base de registre
        /// </summary>
        /// <param name="guid">le guid du produit</param>
        /// <param name="version">la version du produit</param>
        /// <returns>Retourne le nom du produit</returns>
        private static string GetProductName(string guid, string version)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey("Software").OpenSubKey("Ebp").OpenSubKey("Products").OpenSubKey(guid).OpenSubKey(version);
                return key.GetValue("ProductCaption") as string;
            }
            catch (SecurityException e)
            {
                //Exception pouvant survenir lorsque l'utilisateur n'a pas les permissions requises pour lire dans la base de registre
                throw new SDKException(e.Message, e);
            }
            catch (NullReferenceException e)
            {
                throw new SDKException("Aucun produit EBP compatible n'a pu être trouvé", e);
            }
            catch (TypeInitializationException e)
            {
                throw new SDKException(e.Message, e);
            }
        }



        /// <summary>
        /// Lancement d'un processus
        /// </summary>
        /// <param name="args">tableau d'arguments de la ligne de commande à exécuter</param>
        public static void LaunchCommandLineProcess(params string[] args)
        {
            try
            {
                if (string.IsNullOrEmpty(Settings.Instance.Application.ExecutablePath))
                    throw new SDKException("Impossible de trouver l'application de Gestion Commerciale");
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.FileName = Settings.Instance.Application.ExecutablePath;
                processInfo.Arguments = string.Join(" ", args);
                using (Process process = new Process())
                {
                    process.StartInfo = processInfo;
                    process.Start();
                }
            }
            catch (InvalidOperationException e)
            {
                //Exceptions pouvant survenir si le chemin de l'exécutable est vide
                MessageBox.Show(e.Message);
            }
            catch (Win32Exception e)
            {
                //Exceptions durant le lancement de l'exécutable

                //Le code erreur 2 est retourné si l'exécutable est introuvable
                if (e.NativeErrorCode == 2)
                    MessageBox.Show(string.Format("Impossible de lancer l'application, le fichier suivant est introuvable :\n{0}", Settings.Instance.Application.ExecutablePath));
                else
                    MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Créer une connexion SQL après formatage d'une chaine de connexion
        /// </summary>
        /// <returns>Retourne la connexion SQL créée</returns>
        public static SqlConnection CreateSqlConnection()
        {
            DbConnectionStringBuilder connectionString = new DbConnectionStringBuilder();
            connectionString.Add("Data Source", Settings.Instance.Server);
            connectionString.Add("Initial Catalog", Settings.Instance.Database);
            if (!Settings.Instance.UseWindowsAuthentication)
            {
                connectionString.Add("User Id", Settings.Instance.SQLServerUsername);
                connectionString.Add("Password", Settings.Instance.SQLServerPassword);
            }
            else
            {
                connectionString.Add("Integrated Security", "SSPI");
            }
            return new SqlConnection(connectionString.ConnectionString);
        }

        /// <summary>
        /// Vérifie l'intégrité du fichier App.Config
        /// </summary>
        /// <returns></returns>
        public static bool CheckAppConfigIntegrity()
        {
            try
            {
                return !(ConfigurationManager.AppSettings["Version"] == null ||
                    ConfigurationManager.AppSettings["ProductType"] == null ||
                    ConfigurationManager.AppSettings["ProductName"] == null ||
                    ConfigurationManager.AppSettings["PMEId"] == null ||
                    ConfigurationManager.AppSettings["ProId"] == null ||
                    ConfigurationManager.AppSettings["ClassicId"] == null ||
                    ConfigurationManager.AppSettings["DevisId"] == null ||
                    ConfigurationManager.AppSettings["PMEAIId"] == null ||
                    ConfigurationManager.AppSettings["ProAIId"] == null ||
                    ConfigurationManager.AppSettings["ClassicAIId"] == null ||
                    ConfigurationManager.AppSettings["DevisAIId"] == null);
            }
            catch (ConfigurationErrorsException)
            {
                return false;
            }
            catch (NullReferenceException)
            {
                return false;
            }
        }
        #endregion
    }
}
