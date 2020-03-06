using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using ConnecteurEBP.Classes;
using ConnecteurEBP.Utilities;

namespace ConnecteurEBP.Forms
{
    /// <summary>
    /// Fenêtre des paramètres de l'utilisateur
    /// </summary>
    public partial class SettingsForm : Form
    {
        #region Champs privés
        /// <summary>
        /// Raccourci utilisé
        /// </summary>
        private ConnecteurEBP.Classes.Shortcut shortcut;
        #endregion

        #region Constructeurs
        /// <summary>
        /// Création d'une instance de SettingsForm
        /// </summary>
        public SettingsForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Méthodes
        /// <summary>
        /// Chargement de la fenêtre
        /// </summary>
        /// <param name="e">paramètres de l'évènement</param>
        protected override void OnLoad(EventArgs e)
        {

            base.OnLoad(e);
            try
            {
                //Récupération des applications compatibles et installées, depuis le fichier de configuration et la base de registre
                List<ConnecteurEBP.Classes.Application> compatibleProducts = Utils.GetCompatibleProducts();
                compatibleProductsComboBox.DataSource = compatibleProducts;
                if (compatibleProducts.Contains(Settings.Instance.Application))
                    compatibleProductsComboBox.SelectedItem = Settings.Instance.Application;
                else
                    compatibleProductsComboBox.SelectedItem = null;
                //Remplissage des champs d'après les paramètres précédemment sauvegardés
                shortcutPathTextBox.Text = Settings.Instance.ShortcutPath;
                serverTextBox.Text = Settings.Instance.Server;
                databaseTextBox.Text = Settings.Instance.Database;
                useWindowsAuthenticationCheckBox.Checked = Settings.Instance.UseWindowsAuthentication;
                usernameTextBox.Text = Settings.Instance.SQLServerUsername;
                if (!string.IsNullOrEmpty(Settings.Instance.SQLServerPassword))
                    passwordTextBox.Text = Settings.Instance.SQLServerPassword;
                appLoginTextBox.Text = Settings.Instance.ApplicationLogin;
                if (!string.IsNullOrEmpty(Settings.Instance.ApplicationPassword))
                    appPasswordTextBox.Text = Settings.Instance.ApplicationPassword;
            }
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }
        }

        /// <summary>
        /// Annulation et fermeture de la fenêtre
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// Validation des paramètres et fermeture de la fenêtre
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void okButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(shortcutPathTextBox.Text))
                {
                    MessageBox.Show(string.Format("Impossible de trouver le raccourci {0}", shortcutPathTextBox.Text));
                    return;
                }
                //Sauvegarde du produit compatible sélectionné
                Settings.Instance.Application = compatibleProductsComboBox.SelectedItem as ConnecteurEBP.Classes.Application;
                //Vérification des informations de connexion au dossier sélectionné
                if (!CheckDatabaseConnection())
                {
                    MessageBox.Show("La connexion avec la source de données est impossible. Veuillez modifier vos informations de connexion");
                    return;
                }
                //Sauvegarde des informations du dossier sélectionné
                Settings.Instance.Server = serverTextBox.Text;
                Settings.Instance.Database = databaseTextBox.Text;
                Settings.Instance.UseWindowsAuthentication = useWindowsAuthenticationCheckBox.Checked;
                Settings.Instance.SQLServerUsername = usernameTextBox.Text;
                Settings.Instance.SQLServerPassword = passwordTextBox.Text;
                Settings.Instance.ShortcutPath = shortcutPathTextBox.Text;
                Settings.Instance.ApplicationLogin = appLoginTextBox.Text;
                Settings.Instance.ApplicationPassword = appPasswordTextBox.Text;
                //Sauvegarde des paramètres à la fermeture de la fenêtre
                Settings.Instance.Save();
                // Creer la table s'il n'existe pas
                using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
                {
                    try
                    {
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(QueryHelper.createTable, sqlConnection);
                        cmd.ExecuteNonQuery();
                        cmd = new SqlCommand(QueryHelper.EbpSysGenericImportSettings, sqlConnection);
                        cmd.ExecuteNonQuery();

                    }
                    catch (InvalidOperationException ex)
                    {
                        //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
                Close();
                DialogResult = DialogResult.OK;
            }
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Sélection du raccourci du dossier EBP dans l'explorateur Windows
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void browseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Raccourcis EBP|*.ebp";
                dialog.Multiselect = false;
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    shortcutPathTextBox.Text = dialog.FileName;
                }
            }
        }

        /// <summary>
        /// Remplissage des controles de la fenêtre
        /// </summary>
        /// <param name="shortcutPath">le chemin du raccourci</param>
        private void FillControls(string shortcutPath)
        {
            try
            {
                if (!File.Exists(shortcutPath))
                {
                    serverTextBox.Text = string.Empty;
                    databaseTextBox.Text = string.Empty;
                    usernameTextBox.Text = string.Empty;
                    passwordTextBox.Text = string.Empty;
                }
                else
                {
                    shortcut = new ConnecteurEBP.Classes.Shortcut(shortcutPath);
                    serverTextBox.Text = shortcut.Server;
                    databaseTextBox.Text = shortcut.Database;
                    useWindowsAuthenticationCheckBox.Checked = shortcut.UseWindowsAuthentication;
                    usernameTextBox.Text = shortcut.Username;
                    passwordTextBox.Text = shortcut.Password;
                }
            }
            catch (SDKException e)
            {
                shortcut = null;
                serverTextBox.Text = string.Empty;
                databaseTextBox.Text = string.Empty;
                usernameTextBox.Text = string.Empty;
                passwordTextBox.Text = string.Empty;
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Traitement des informations du raccourci lorsaque son chemin est modifié
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void shortcutPathTextBox_TextChanged(object sender, EventArgs e)
        {
            FillControls(shortcutPathTextBox.Text);
        }

        /// <summary>
        /// Vérification de la connexion à la base de données d'après le raccourci sélectionné
        /// </summary>
        /// <returns>Retourne Vrai si la connexion peut s'effectuer, faux sinon</returns>
        private bool CheckDatabaseConnection()
        {
            Cursor originCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DbConnectionStringBuilder connectionString = new DbConnectionStringBuilder();
                connectionString.Add("Data Source", serverTextBox.Text);
                connectionString.Add("Initial Catalog", databaseTextBox.Text);
                if (!useWindowsAuthenticationCheckBox.Checked)
                {
                    connectionString.Add("User Id", usernameTextBox.Text);
                    connectionString.Add("Password", passwordTextBox.Text);
                }
                else
                {
                    connectionString.Add("Integrated Security", "SSPI");
                }
                using (SqlConnection sqlConnection = new SqlConnection(connectionString.ConnectionString))
                {
                    try
                    {
                        sqlConnection.Open();
                        return true;
                    }
                    catch (InvalidOperationException e)
                    {
                        //Survient lorsque la connexion est déjà ouverte ou si la source de données ou l'adresse du serveur ne sont pas spécifiés
                        throw new SDKException(e.Message);
                    }
                    catch (SqlException e)
                    {
                        throw new SDKException(e.Message);
                    }
                }
            }
            finally
            {
                Cursor.Current = originCursor;
            }
        }

        /// <summary>
        /// Modifier le mode des champs utilisateur
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void useWindowsAuthenticationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            usernameTextBox.ReadOnly = useWindowsAuthenticationCheckBox.Checked;
            passwordTextBox.ReadOnly = useWindowsAuthenticationCheckBox.Checked;
        }
        #endregion

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
