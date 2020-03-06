using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ConnecteurEBP.Utilities;
using System.IO;
using ConnecteurEBP.Classes;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Xml;
using ProgressBarExample;
using System.Globalization;

namespace ConnecteurEBP.Forms
{
    public partial class importManuel : Form
    {
        private static string ordersFilename = string.Format(@"{0}\importCommande.txt", Path.GetTempPath());// string.Format(@"C:\Documents and Settings\OTHMAN\Bureau\resultat11.txt");
        private static string filename = "";
        // Flag that indcates if a process is running
        //private bool isProcessRunning = false;

        public importManuel()
        {
            InitializeComponent();
        }

        public void SplashScreen()
        {
            System.Windows.Forms.Application.Run(new Loading());
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // If a process is already running, warn the user and cancel the operation
            //if (isProcessRunning)
            //{
            //    MessageBox.Show("A process is already running.");
            //    return;
            //}

            //// Initialize the dialog that will contain the progress bar
            //ProgressDialog progressDialog = new ProgressDialog();

            //// Initialize the thread that will handle the background process
            //Thread backgroundThread = new Thread(
            //    new ThreadStart(() =>
            //    {
            //        // Set the flag that indicates if a process is currently running
            //        isProcessRunning = true;
            //        for (int n = 0; n < 100; n++)
            //        {
            //            Thread.Sleep(5);
            //            progressDialog.UpdateProgress(n);
            //        }
            //        // Show a dialog box that confirms the process has completed
            //        //MessageBox.Show("Thread completed!");

            //        // Close the dialog if it hasn't been already
            //        if (progressDialog.InvokeRequired)
            //            progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

            //        // Reset the flag that indicates if a process is currently running
            //        isProcessRunning = false;
            //    }
            //));

            //// Start the background process thread
            //backgroundThread.Start();

            //// Open the dialog
            //progressDialog.ShowDialog();
        }

        private void exportCustomersFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "CSV|*.csv|TEXT|*.txt";
                //dialog.AddExtension = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(dialog.FileName).ToLower() == ".csv" || Path.GetExtension(dialog.FileName).ToLower() == ".txt")
                    {
                        exportCustomersFilenameTextBox.Text = dialog.FileName;
                        filename = dialog.FileName;
                        //exportCustomersCommandTextBox.Text = string.Format(CommandLinesHelper.ImportFactures, exportCustomersFilenameTextBox.Text);
                    }
                    else
                    {
                        exportCustomersFilenameTextBox.Text = string.Empty;
                        //exportCustomersCommandTextBox.Text = string.Empty;
                        MessageBox.Show("Le format de ce fichier doit être de type CSV.");
                    }
                }
            }
        }

        //-------------------------------- Import Enregistrer -----------------------------------------------------

        private void exportCustomersButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(exportCustomersFilenameTextBox.Text))
            {
                MessageBox.Show("Le chemin du fichier d'import de commande doit être renseigné");
                return;
            }

            try
            {
                Boolean Motref = false;
                string NumCommande = "";
                Order order = new Order();
                order.Id = GetNextOrderId();
                //order.Date = //DateTime.Now;
                //order.ThirdId = thirdIdTextBox.Text;
                order.ThirdName = "";
                order.Address = "";
                order.ZipCode = "";
                order.City = "";
                order.Country = "";
                long pos = 1;
                //order.Lines = orderLinesDataGridView.DataSource as List<OrderLine>;
                order.Lines = new List<OrderLine>();

                string[] lines = System.IO.File.ReadAllLines(filename);


                if (lines.Length < 4)
                {
                    MessageBox.Show("Le fichier est invalide", "Erreur !!",
                                                                                               MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if (lines[0].Split(';')[0] == "ORDERS" && lines[0].Split(';').Length == 11)
                {
                    NumCommande = lines[0].Split(';')[1];
                    if (NumCommande.Length > 9)
                    {
                        MessageBox.Show("Numéro de commande doit être < 10", "Erreur de lecture !!",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (NumCommande == "")
                    {
                        MessageBox.Show("Le champ numéro de commande est vide.", "Erreur !!",
                                                                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    if (!IsNumeric(NumCommande))
                    {
                        MessageBox.Show("Numéro de commande n'est pas valide.", "Erreur !!",
                                                                                                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    //Test Numero de commande

                    if (!TestNumeroCommande(NumCommande))
                    {
                        MessageBox.Show("Numero de commande déja enregistrer : " + NumCommande, "Erreur !!",
                              MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    //order.codeClient = lines[0].Split(';')[2];
                    if (lines[0].Split(';')[2] == "")
                    {
                        MessageBox.Show("Le champ GLN émetteur est vide.", "Erreur !!",
                                                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!IsNumeric(lines[0].Split(';')[2]))
                    {
                        MessageBox.Show("Numéro GLN émetteur n'est pas valide.", "Erreur !!",
                                                                                                  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    order.ThirdId = GetClientId(lines[0].Split(';')[2]);

                    if (order.ThirdId != null)
                    {
                        if (!TestAdresseFacturationClient(order.ThirdId))
                        {
                            MessageBox.Show("Veuillez completer les informations du client " + order.ThirdId + " dans la base EBP.!\nAdresse de facturation : \"Code Postale\", \"ville\", \"pays\" doient etre renseigné", "Erreur",
                             MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        //return nom du client
                        order.ThirdName = GetNomClient(order.ThirdId);
                    }

                    //order.Address = lines[0].Split(';')[7];

                    // Ajouter ville dans la réference
                    //string[] part = order.adresseLivraison.Split('.');
                    //if (part.Length >= 2)
                    //{
                    //    order.Reference = part[part.Length - 2];
                    //}

                    //string Ref = order.Reference + "/" + order.NumCommande;

                    //if(Ref.Length <= 17)
                    //{
                    //    order.Reference = Ref;
                    //}

                    //order.deviseCommande = lines[0].Split(';')[8];

                    //if (order.deviseCommande != "")
                    //{
                    //    order.deviseCommande = getDevise(order.deviseCommande);
                    //}

                    //if (order.deviseCommande == "erreur")
                    //{
                    //    return;
                    //}


                    if (lines[1].Split(';')[0] == "ORDHD1" && lines[1].Split(';').Length == 5)
                    {

                        if (lines[1].Split(';')[1].Length == 8)
                        {
                            string date = lines[1].Split(';')[1];

                            if (!IsNumeric(date))
                            {
                                MessageBox.Show("Date de la commande n'est pas valide.", "Erreur !!",
                                                                                                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            date = date.Substring(6, 2) + "/" + date.Substring(4, 2) + "/" + date.Substring(0, 4);
                            order.Date = Convert.ToDateTime(date);

                            if (lines[2].Split(';')[0] == "ORDLIN" && lines[2].Split(';').Length == 23)
                            {
                                decimal total = 0m;
                                foreach (string ligneDuFichier in lines)
                                {

                                    string[] tab = ligneDuFichier.Split(';');

                                    switch (tab[0])
                                    {
                                        case "ORDLIN":
                                            if (tab.Length == 23)
                                            {
                                                OrderLine line = new OrderLine();
                                                //line.NumLigne = tab[1];
                                                //line.article = getArticle(tab[2]);
                                                line.ItemId = GetArticleId(tab[2]);
                                                decimal quant = Convert.ToDecimal(tab[9].Replace(".", ","));

                                                line.Quantity = Convert.ToInt32(quant);

                                                if (line.ItemId == null)
                                                {
                                                    return;
                                                }

                                                //line.Quantite = tab[9].Replace(",", ".");
                                                //decimal d = Decimal.Parse(line.Quantity, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
                                                //if (d == 0)
                                                //{

                                                //    line.Quantity = "1";

                                                //}
                                                line.ItemPrice = Decimal.Parse(tab[14].Replace(",", "."), NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
                                                //line.MontantLigne = tab[11];
                                                //line.DateLivraison = "'{d " + ConvertDate(tab[21]) + "}'";
                                                //if (line.DateLivraison.Length == 6)
                                                //{
                                                //    line.DateLivraison = "Null";
                                                //}

                                                //line.codeAcheteur = tab[4];
                                                //line.descriptionArticle = tab[8];
                                                total = total + Decimal.Parse(tab[11], NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

                                                decimal prix = Convert.ToDecimal(line.ItemPrice);
                                                decimal prixEbp = Convert.ToDecimal(VerifierPrixVente(line.ItemId));

                                                if (prix != prixEbp)
                                                {
                                                    DialogResult resultDialog = MessageBox.Show("Prix de l'article " + line.ItemId + "(" + tab[2] + ") dans la base est : " + prixEbp + "\nIl est différent du prix envoyer par le client : " + prix + ".",
                                                            "Worning Message !!",
                                                            MessageBoxButtons.OKCancel,
                                                            MessageBoxIcon.Warning,
                                                            MessageBoxDefaultButton.Button2);
                                                    if (resultDialog == DialogResult.Cancel)
                                                    {
                                                        return;
                                                    }
                                                    if (resultDialog == DialogResult.OK)
                                                    {
                                                        Motref = true;
                                                    }
                                                }

                                                order.Lines.Add(line);
                                            }
                                            else
                                            {
                                                MessageBox.Show("Erreur dans la ligne " + pos + " du fichier.", "Erreur de lecture !!",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                return;
                                            }
                                            break;

                                    }

                                    pos++;

                                }


                                if (order.ThirdId != "")
                                {

                                    if (order.Lines.Count == 0)
                                    {
                                        MessageBox.Show("Aucun ligne de commande enregistré.", "Erreur de lecture !!",
                                                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }                                   

                                }
                                else
                                {
                                    MessageBox.Show("Il faut mentionner le code client.", "Erreur de lecture !!",
                                                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Erreur dans la troisième ligne du fichier.", "Erreur de lecture !!",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Date de la commande est incorrecte", "Erreur de lecture !!",
                                                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Erreur dans la deuxième ligne du fichier.", "Erreur de lecture !!",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Erreur dans la première ligne du fichier.", "Erreur de lecture !!",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


                if (order.ThirdId != null)
                {
                    using (StreamWriter writer = new StreamWriter(ordersFilename, false, Encoding.UTF8))
                    {
                        writer.WriteLine("OrderId;Date;ThirdId;ThirdName;Address;ZipCode;City;Country;ItemId;Quantity");

                        foreach (OrderLine line in order.Lines)
                        {
                            if (line.ItemId != null)
                            {
                                if (Motref)
                                {
                                    writer.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                                        NumCommande.Trim() + " (prix article différent)", order.Date, order.ThirdId, order.ThirdName, order.Address, order.ZipCode,
                                    order.City, order.Country, line.ItemId, line.Quantity));
                                }
                                else
                                {
                                    writer.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                                        NumCommande, order.Date, order.ThirdId, order.ThirdName, order.Address, order.ZipCode,
                                    order.City, order.Country, line.ItemId, line.Quantity));
                                }
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    //Création des arguments de la ligne de commande
                    string importArguments = string.Format(CommandLinesHelper.ImportOrder, ordersFilename);
                    //Exécution de la ligne de commande
                    Utils.LaunchCommandLineProcess(importArguments);
                    // Creer dossier sortie "LOG Directory" --------------------------
                    var dirName = string.Format("LogEBP(manuelle) {0:dd-MM-yyyy hh.mm.ss}", DateTime.Now);
                    string outputFile = Path.GetDirectoryName(filename) + @"\" + dirName;
                    System.IO.Directory.CreateDirectory(outputFile);
                    //deplacer les fichiers csv
                    System.IO.File.Move(filename, outputFile + @"\" + Path.GetFileName(filename));
                    Motref = false;
                    Close();
                }

            }
            catch (UnauthorizedAccessException ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show(ex.Message);
            }
            catch (IOException ex)
            {
                //Exception pouvant survenir si le chemin du fichier est trop long ou s'il est introuvable
                MessageBox.Show(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                //Exception pouvant survenir si le format du fichier est incorrect
                MessageBox.Show(ex.Message);
            }
            catch (SDKException ex)
            {
                //Exceptions issues de la méthode LaunchProcess
                MessageBox.Show(ex.Message);
            }

        }



        /// <summary>
        /// Récupération du prochain identifiant de commande
        /// </summary>
        /// <returns>Retourne le prochain identifiant</returns>
        private static string GetNextOrderId()
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.NextOrderId_SQLServer, sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader["incValue"] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return "00000001";
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
        }

        private static string GetClientId(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    //QueryHelper.returnClient(id);
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.returnClient(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader["ID_EBP"] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        MessageBox.Show("Le client " + id + " n'est pas enregistré dans la base.!", "Erreur !!",
                                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return null;
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
        }

        private static string GetArticleId(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    //QueryHelper.returnClient(id);
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.returnArticleCodeBarre(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader["Id"] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        MessageBox.Show("L'article " + id + " n'est pas enregistré dans la base.!", "Erreur !!",
                                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return null;
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
        }

        private static string GetNomClient(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    //QueryHelper.returnClient(id);
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.returnNomClient(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader["Name"] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        //MessageBox.Show("L'identifiant " + id + " n'est pas enregistré dans la base.");

                        MessageBox.Show("L'identifiant " + id + " n'est pas enregistré dans la base.!", "Erreur d'importation",
                                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return null;
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static Boolean TestAdresseFacturationClient(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    //QueryHelper.returnClient(id);
                    string codepostale = "";
                    string pays = "";
                    string ville = "";
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.TestClientAdressFacturation(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                codepostale = reader[0].ToString();
                                ville = reader[1].ToString();
                                pays = reader[2].ToString();
                            }
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        //MessageBox.Show("L'identifiant " + id + " n'est pas enregistré dans la base.");

                        if (codepostale != "" && pays != "" && ville != "")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                catch (Exception e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return false;
                }
            }
        }

        private static Boolean TestNumeroCommande(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    //QueryHelper.returnClient(id);
                    string NumCommande = "";

                    using (SqlCommand cmd = new SqlCommand(QueryHelper.VerifierCommande(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                NumCommande = reader[0].ToString();
                            }
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        //MessageBox.Show("L'identifiant " + id + " n'est pas enregistré dans la base.");
                        //MessageBox.Show(NumCommande);
                        if (NumCommande != "")
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
                catch (Exception e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return false;
                }
            }
        }

        private static string VerifierPrixVente(string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    //QueryHelper.returnClient(id);
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.VerifierPrixArticle(id), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader[0].ToString();
                            }
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        //MessageBox.Show("L'identifiant " + id + " n'est pas enregistré dans la base.");

                        MessageBox.Show("Aucun prix enregistrer dans la base", "Erreur !!",
                                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return null;
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return null;
                }
            }
        }

        public static Boolean IsNumeric(string Nombre)
        {
            try
            {
                long.Parse(Nombre);
                return true;
            }
            catch
            {
                return false;
            }
        }
       
    }
}
