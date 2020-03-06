using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ConnecteurEBP.Classes;
using ConnecteurEBP.Utilities;
using System.Configuration;
using System.Threading;
using System.Reflection;
using System.Data.SqlClient;
using ProgressBarExample;

namespace ConnecteurEBP.Forms
{
    public partial class New : Form
    {
        // Flag that indcates if a process is running
        private bool isProcessRunning = false;

        public New()
        {
           if (isProcessRunning)
            {
                MessageBox.Show("A process is already running.");
                return;
            }

                  // Initialize the dialog that will contain the progress bar
            ProgressDialog progressDialog = new ProgressDialog();

            // Initialize the thread that will handle the background process
            Thread backgroundThread = new Thread(
                new ThreadStart(() =>
                {
                    // Set the flag that indicates if a process is currently running
                    isProcessRunning = true;
                    for (int n = 0; n < 29; n++)
                    {
                        Thread.Sleep(2);
                        progressDialog.UpdateProgress(n);
                    }

            InitializeComponent();

            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    for (int n = 30; n < 49; n++)
                    {
                        Thread.Sleep(1);
                        progressDialog.UpdateProgress(n);
                    }
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(QueryHelper.createPath + "; " + QueryHelper.createTable + "; " + QueryHelper.EbpSysGenericImportSettings + "; " + QueryHelper.EbpSysGenericImportSettings2 + "; " + QueryHelper.EbpSysGenericImportSettings_facture + "; " + QueryHelper.createTable_TachePlanifier, sqlConnection);
                    cmd.ExecuteNonQuery();
                    for (int n = 50; n < 79; n++)
                    {
                        Thread.Sleep(1);
                        progressDialog.UpdateProgress(n);
                    }
                    string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    string fileName = "serializedEntity.xml";
                    string fileName2 = "import_BL_serializedEntity.xml";
                    string fileName3 = "import_INV_serializedEntity.xml";
                    string sourceFile = System.IO.Path.Combine(pathModule, fileName);
                    string dataXML = System.IO.File.ReadAllText(sourceFile);
                    string sourceFile2 = System.IO.Path.Combine(pathModule, fileName2);
                    string dataXML2 = System.IO.File.ReadAllText(sourceFile2);
                    string sourceFile3 = System.IO.Path.Combine(pathModule, fileName3);
                    string dataXML3 = System.IO.File.ReadAllText(sourceFile3);

                    cmd = new SqlCommand(@"update EbpSysGenericImportSettings set serializedEntity='" + dataXML + "' where name='Commandes2' and serializedEntity='null'; " +
                    "update EbpSysGenericImportSettings set serializedEntity='" + dataXML2 + "' where name='import_bonLivraison' and serializedEntity='null'; " +
                    "update EbpSysGenericImportSettings set serializedEntity='" + dataXML3 + "' where name='import_facture' and serializedEntity='null'", sqlConnection);
                    cmd.ExecuteNonQuery();

                    for (int n = 80; n < 89; n++)
                    {
                        Thread.Sleep(1);
                        progressDialog.UpdateProgress(n);
                    }
                    //cmd = new SqlCommand(QueryHelper.createTriger, sqlConnection);
                    //cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }

            for (int n = 90; n < 100; n++)
            {
                Thread.Sleep(5);
                progressDialog.UpdateProgress(n);
            }

                      // Close the dialog if it hasn't been already
            if (progressDialog.InvokeRequired)
                progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

            // Reset the flag that indicates if a process is currently running
            isProcessRunning = false;
                }
            ));

            // Start the background process thread
            backgroundThread.Start();

            // Open the dialog
            progressDialog.ShowDialog();

        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;

            if (!Utils.CheckAppConfigIntegrity())
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = false;
                import_facture.Enabled = false;
                button6.Enabled = false;
                button7.Enabled = false;
                button8.Enabled = false;
                MessageBox.Show("Le fichier de configuration est introuvable ou mal formaté. Veuillez vérifier l'intégrité de ce fichier.", "Fichier de configuration introuvable", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //DateTime value = new DateTime(2013, 3, 1);
            //if (value <= DateTime.Today)
            //{
            //    button1.Enabled = false;
            //    button2.Enabled = false;
            //    button3.Enabled = false;
            //    button5.Enabled = false;
            //    MessageBox.Show("Le fichier de configuration est mal formaté.", "Fichier de configuration !!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    //label2.Text = "La version d'essaie est terminé !!";
            //    return;
            //}
        }

        public void SplashScreen()
        {
            System.Windows.Forms.Application.Run(new SplashScreen());
        }

        private void New_Load(object sender, EventArgs e)
        {

        }

        private void New_Load_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {
                if (!Settings.CheckSettings())
                    return;
                using (importManuel form = new importManuel())
                {
                    form.ShowDialog();
                }
            }
            // Récupération d'une possible SDKException
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Settings.CheckSettings())
                    return;
                using (Clients form = new Clients())
                {
                    form.ShowDialog();
                }
            }
            // Récupération d'une possible SDKException
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                //recherche de produits installés compatibles avec la DEMO SDK
                if (!Utils.IsCompatibleProductsExists())
                {
                    MessageBox.Show("Aucune application EBP compatible avec cette version du SDK n'a été trouvée", "Application compatible non trouvée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Ouverture de la fenêtre des paramètres
                using (SettingsForm form = new SettingsForm())
                    if (form.ShowDialog() == DialogResult.OK)
                        FormatMainFormAccordingCurrentApplication();
            }
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Fonction permettant d'activer/désactiver des boutons selon la version de l'application sélectionnée
        /// </summary>
        public void FormatMainFormAccordingCurrentApplication()
        {

            if (Settings.Instance.Application != null)
            {
                //activation des fonctionnalités uniquement sur PRO et PME autonomes et intégrées
                button2.Enabled = Settings.Instance.Application.Id == new Guid(ConfigurationManager.AppSettings["PMEId"].ToString()) ||
                    Settings.Instance.Application.Id == new Guid(ConfigurationManager.AppSettings["PROId"].ToString()) ||
                    Settings.Instance.Application.Id == new Guid(ConfigurationManager.AppSettings["ProAIId"].ToString()) ||
                    Settings.Instance.Application.Id == new Guid(ConfigurationManager.AppSettings["PMEAIId"].ToString());
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Settings.CheckSettings())
                    return;
                using (Planifier form = new Planifier())
                {
                    form.ShowDialog();
                }
            }
            // Récupération d'une possible SDKException
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //private void button6_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!Settings.CheckSettings())
        //            return;
        //        using (AboutBox form = new AboutBox())
        //        {
        //            form.ShowDialog();
        //        }
        //    }
        //    // Récupération d'une possible SDKException
        //    catch (SDKException ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }

        //}

        private void button1_MouseHover(object sender, EventArgs e)
        {

            label2.Text = "Vous pouvez réaliser l'import de \ncommandes d'une façon manuelle.";

        }

        private void button5_MouseHover(object sender, EventArgs e)
        {

            label2.Text = "Vous pouvez planifier l'import \nde commandes pour une heure \nqui vous convient.";

        }

        private void button2_MouseHover(object sender, EventArgs e)
        {

            label2.Text = "Avant de réaliser l’importation \nde commandes, il faut ajouter le lien \nentre l’identifiant de l’acheteur dans \nle fichier CSV et l’identifiant dans \nla base EBP.";

        }

        private void button_MouseLeave(object sender, EventArgs e)
        {

            label2.Text = "Vous pouvez réaliser l'import de\n" +
                    "commandes et l'export de factures \n" +
                    "et bons de livraisons .\n" +
                    "Vous pouvez aussi planifier l'import \n" +
                    "de commandes pour une heure qui \n" +
                    "vous convient.";

        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion



        private void button4_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            try
            {
                //recherche de produits installés compatibles avec la DEMO SDK
                if (!Utils.IsCompatibleProductsExists())
                {
                    MessageBox.Show("Aucune application EBP compatible avec cette version du SDK n'a été trouvée", "Application compatible non trouvée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Ouverture de la fenêtre des paramètres
                using (ExportFactures form = new ExportFactures())
                    if (form.ShowDialog() == DialogResult.OK)
                        FormatMainFormAccordingCurrentApplication();
            }
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                //recherche de produits installés compatibles avec la DEMO SDK
                if (!Utils.IsCompatibleProductsExists())
                {
                    MessageBox.Show("Aucune application EBP compatible avec cette version du SDK n'a été trouvée", "Application compatible non trouvée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Ouverture de la fenêtre des paramètres
                using (BonLivraison form = new BonLivraison())
                    if (form.ShowDialog() == DialogResult.OK)
                        FormatMainFormAccordingCurrentApplication();
            }
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                //recherche de produits installés compatibles avec la DEMO SDK
                if (!Utils.IsCompatibleProductsExists())
                {
                    MessageBox.Show("Aucune application EBP compatible avec cette version du SDK n'a été trouvée", "Application compatible non trouvée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Ouverture de la fenêtre des paramètres
                using (ExportCommande form = new ExportCommande())
                    if (form.ShowDialog() == DialogResult.OK)
                        FormatMainFormAccordingCurrentApplication();
            }
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Settings.CheckSettings())
                    return;
                using (Clients form = new Clients())
                {
                    form.ShowDialog();
                }
            }
            // Récupération d'une possible SDKException
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {


            try
            {
                if (!Settings.CheckSettings())
                    return;
                using (Import_BonLivraison form = new Import_BonLivraison())
                {
                    form.ShowDialog();
                }
            }
            // Récupération d'une possible SDKException
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void labelProductName_Click(object sender, EventArgs e)
        {

        }

        private void import_facture_Click(object sender, EventArgs e)
        {

            try
            {
                if (!Settings.CheckSettings())
                    return;
                using (Import_facture form = new Import_facture())
                {
                    form.ShowDialog();
                }
            }
            // Récupération d'une possible SDKException
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            try
            {
                using (ConfMail form = new ConfMail())
                {
                    form.ShowDialog();
                }
            }
            // Récupération d'une possible SDKException
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
