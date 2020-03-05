using ConnecteurEBP.Classes;
using ConnecteurEBP.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnecteurEBP.Forms
{
    public partial class ExportCommande : Form
    {
        #region Champs privés
        /// <summary>
        /// commande à exporter
        /// </summary>
        private Piece CommandeAExporter;

        #endregion

        #region Constructeurs
        /// <summary>
        /// Création d'une instance de ImportOrdersForm
        /// </summary>
        public ExportCommande()
        {
            InitializeComponent();
        }
        #endregion

        #region Intéractions avec l'application

        private List<Piece> GetCommandesFromDataBase()
        {
            try
            {
                //DocumentVente Facture = new DocumentVente();
                List<Piece> listCommande = new List<Piece>();
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {

                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.Lists_Commandes(), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Piece order = new Piece(reader[0].ToString(), reader[1].ToString(),
                                    reader[2].ToString().Replace("00:00:00", ""), reader[3].ToString(),
                                    reader[4].ToString(),
                                    reader[5].ToString(), reader[6].ToString(), reader[7].ToString(),
                                    reader[8].ToString(), reader[9].ToString(),
                                    reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString()
                                    );
                                listCommande.Add(order);
                            }
                        }
                    }
                    return listCommande;

                }

            }

            catch (Exception e)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show(e.Message);
                return null;
            }
        }






        /// <summary>
        /// Génération du fichier d'import, lancement de l'application et import des commandes
        /// </summary>
        private void ExportFacture()
        {
            try
            {

                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Le chemin du fichier d'import de commande doit être renseigné");
                    return;
                }


                var fileName = string.Format("EDI_ORDERS." + CommandeAExporter.CodebarCT + "." + CommandeAExporter.SPiece_CodeTable + "." + ConvertDate(CommandeAExporter.cdate) + "." + CommandeAExporter.TiersAdresse2Ligne + "." + CommandeAExporter.TiersAdresse2CodePo + "." + CommandeAExporter.TiersAdresse2Ville + "." + CommandeAExporter.TiersAdresse2CodePa + ".{0:yyyyMMddhhmmss}.csv", DateTime.Now);

                fileName = fileName.Replace("?", "é");

                using (StreamWriter writer = new StreamWriter(textBox1.Text + @"\" + fileName.Replace("..", "."), false, Encoding.UTF8))
                {

                    //if (CommandeAExporter.DO_MOTIF == "")
                    //{
                    //    CommandeAExporter.DO_MOTIF = CommandeAExporter.NumCommande;
                    //}
                    if (CommandeAExporter.TiersAdresse2CodePa != "")
                    {
                        CommandeAExporter.TiersAdresse2CodePa = getPays(CommandeAExporter.TiersAdresse2CodePa);
                    }

                    writer.WriteLine("ORDERS;" + CommandeAExporter.SPiece_CodeTable + ";" + CommandeAExporter.CodebarCT + ";" + CommandeAExporter.CodebarCT + ";;;;" + CommandeAExporter.ContactLiv_Nom + "." + CommandeAExporter.TiersAdresse2Ligne.Replace("?", "é") + "." + CommandeAExporter.TiersAdresse2CodePo + "." + CommandeAExporter.TiersAdresse2Ville.Replace("?", "é") + "." + CommandeAExporter.TiersAdresse2CodePa + ";" + CommandeAExporter.TiersDevise + ";;");

                    if (CommandeAExporter.cdate != "")
                    {
                        CommandeAExporter.cdate = ConvertDate(CommandeAExporter.cdate);
                    }

                    //if (CommandeAExporter.DateCommande != " ")
                    //{
                    //    CommandeAExporter.conditionLivraison = "";
                    //}

                    writer.WriteLine("ORDHD1;" + CommandeAExporter.cdate + ";;;");

                    CommandeAExporter.Lines = getLigneCommande(CommandeAExporter.NumeroNumero);

                    for (int i = 0; i < CommandeAExporter.Lines.Count; i++)
                    {
                        string[] tab = CommandeAExporter.Lines[i].commentaire.Split(';');
                        string codeAchteur = "";
                        string codefourn = "";
                        if (tab.Length == 2)
                        {
                            codeAchteur = tab[0];
                            codefourn = tab[1];
                        }
                        writer.WriteLine("ORDLIN;" + CommandeAExporter.Lines[i].ip + ";" + CommandeAExporter.Lines[i].barCode + ";GS1;" + codeAchteur + ";" + codefourn + ";;A;" + CommandeAExporter.Lines[i].libelle + ";" + CommandeAExporter.Lines[i].quantite.Replace(",", ".") + ";LM;" + CommandeAExporter.Lines[i].MontantNetHT.Replace(",", ".") + ";;;" + CommandeAExporter.Lines[i].PxUnitBrut.Replace(",", ".") + ";;;LM;;;;" + ConvertDate(CommandeAExporter.Lines[i].DateLiv) + ";");
                    }
                    writer.WriteLine("ORDEND;" + CommandeAExporter.NetAPayer.Replace(",", ".") + ";");



                }

                MessageBox.Show("Commande exportée avec succés", "Information !!",
                                             MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Close();



            }
            catch (Exception ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Méthodes diverses
        /// <summary>
        /// Chargement de la fenêtre
        /// </summary>
        /// <param name="e">paramètres de l'évènement</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {

                textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                // Initialize the dialog that will contain the progress bar
                ProgressDialog progressDialog = new ProgressDialog();

                // Initialize the thread that will handle the background process
                Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        // Set the flag that indicates if a process is currently running
                        //isProcessRunning = true;
                        for (int n = 0; n < 25; n++)
                        {
                            Thread.Sleep(1);
                            progressDialog.UpdateProgress(n);
                        }

                        //Affichage des clients du dossier
                        if (customersDataGridView.InvokeRequired)
                        {
                            customersDataGridView.Invoke(new MethodInvoker(delegate
                            {
                                customersDataGridView.DataSource = GetCommandesFromDataBase();
                                for (int n = 26; n < 45; n++)
                                {
                                    Thread.Sleep(1);
                                    progressDialog.UpdateProgress(n);
                                }
                                importButton.Enabled = customersDataGridView.Rows.Count > 0;

                                if (customersDataGridView.Columns["NumeroPrefixe"] != null)
                                    customersDataGridView.Columns["NumeroPrefixe"].HeaderText = "Préfixe";
                                if (customersDataGridView.Columns["NumeroNumero"] != null)
                                    customersDataGridView.Columns["NumeroNumero"].HeaderText = "Numéro";
                                if (customersDataGridView.Columns["cdate"] != null)
                                    customersDataGridView.Columns["cdate"].HeaderText = "Date";
                                if (customersDataGridView.Columns["TiersCode"] != null)
                                    customersDataGridView.Columns["TiersCode"].HeaderText = "Client";
                                if (customersDataGridView.Columns["TiersAdresse2Ligne"] != null)
                                    customersDataGridView.Columns["TiersAdresse2Ligne"].Visible = false;
                                if (customersDataGridView.Columns["TiersAdresse2CodePo"] != null)
                                    customersDataGridView.Columns["TiersAdresse2CodePo"].Visible = false;
                                if (customersDataGridView.Columns["TiersAdresse2Ville"] != null)
                                    customersDataGridView.Columns["TiersAdresse2Ville"].Visible = false;
                                if (customersDataGridView.Columns["TiersAdresse2CodePa"] != null)
                                    customersDataGridView.Columns["TiersAdresse2CodePa"].Visible = false;
                                if (customersDataGridView.Columns["TiersDevise"] != null)
                                    customersDataGridView.Columns["TiersDevise"].Visible = false;
                                if (customersDataGridView.Columns["NetAPayer"] != null)
                                    customersDataGridView.Columns["NetAPayer"].HeaderText = "Net A Payer";
                                if (customersDataGridView.Columns["DateLivraison"] != null)
                                    customersDataGridView.Columns["DateLivraison"].Visible = false;
                                if (customersDataGridView.Columns["SPiece_CodeTable"] != null)
                                    customersDataGridView.Columns["SPiece_CodeTable"].Visible = false;
                                if (customersDataGridView.Columns["CodebarCT"] != null)
                                    customersDataGridView.Columns["CodebarCT"].Visible = false;
                                if (customersDataGridView.Columns["ContactLiv_Nom"] != null)
                                    customersDataGridView.Columns["ContactLiv_Nom"].Visible = false;

                                //Récupération du prochain identifiant de commande à utiliser
                                //string nextOrderId = GetNextOrderId();
                            }));
                        }

                        for (int n = 46; n < 100; n++)
                        {
                            Thread.Sleep(1);
                            progressDialog.UpdateProgress(n);
                        }

                        // Close the dialog if it hasn't been already
                        if (progressDialog.InvokeRequired)
                            progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

                        // Reset the flag that indicates if a process is currently running
                        //isProcessRunning = false;
                    }
                ));

                // Start the background process thread
                backgroundThread.Start();

                // Open the dialog
                progressDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public static string ConvertDate(string date)
        {
            if (date.Length == 11 || date.Length == 19)
            {
                return date.Substring(6, 4) + date.Substring(3, 2) + date.Substring(0, 2);
            }
            return date;
        }

        /// <summary>
        /// Fermeture de la fenêtre
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Remplissage des infos d'adresse du client sélectionné
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void customersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (customersDataGridView.SelectedRows.Count == 0)
            {
                importButton.Enabled = false;
                return;
            }
            Piece order = customersDataGridView.SelectedRows[0].DataBoundItem as Piece;
            if (order == null)
                throw new NullReferenceException("order");

            CommandeAExporter = order;

        }

        /// <summary>
        /// Lancement de l'import
        /// </summary>
        /// <param name="sender">objet déclenchant l'évènement</param>
        /// <param name="e">paramètres de l'évènement</param>
        private void importButton_Click(object sender, EventArgs e)
        {
            importButton.Enabled = false;

            ExportFacture();
        }
        #endregion

        private void itemsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void customersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ImportOrdersForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();

            folderDlg.ShowNewFolderButton = true;

            // Show the FolderBrowserDialog.

            DialogResult result = folderDlg.ShowDialog();

            if (result == DialogResult.OK)
            {

                textBox1.Text = folderDlg.SelectedPath;

                //Environment.SpecialFolder root = folderDlg.RootFolder;

            }
        }

        private string getPays(string code)
        {
            try
            {
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {

                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.CodeDEB_Pays(code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader[0].ToString();
                            }
                        }
                    }
                    return null;

                }

            }

            catch (Exception ex)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show("" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        private List<Ligne> getLigneCommande(string code)
        {
            try
            {
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {
                    List<Ligne> lines = new List<Ligne>();

                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.LignesDesCommandes(code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lines.Add(new Ligne(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString()));
                            }

                            return lines;
                        }
                    }
                    return null;

                }

            }

            catch (Exception ex)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show("" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

    }
}
