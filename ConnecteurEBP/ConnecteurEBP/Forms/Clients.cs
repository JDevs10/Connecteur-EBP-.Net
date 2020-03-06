using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ConnecteurEBP.Classes;
using ConnecteurEBP.Utilities;
using System.Threading;
using ProgressBarExample;
using System.IO;
using System.Runtime.InteropServices;

namespace ConnecteurEBP.Forms
{
    public partial class Clients : Form
    {
        public Clients()
        {
            InitializeComponent();
        }

        private void customersDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        // Flag that indcates if a process is running
        //private bool isProcessRunning = false;

        /// <summary>
        /// Récupération des articles du dossier
        /// </summary>
        /// <returns>Retourne la liste des articles</returns>
        private List<Client> GetClientsFromDatabase()
        {
            List<Client> clients = new List<Client>();
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.Client_BASE_X_EBP, sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string id = reader["ID_BASE_X"] as string;
                                string id2 = reader["ID_EBP"] as string;
                                string name = reader["Name"] as string;

                                clients.Add(new Client(id,id2, name));
                            }
                        }
                    }
                    return clients;
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

        public void SplashScreen()
        {
            System.Windows.Forms.Application.Run(new Loading());
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //Affichage des articles du dossier
            customersDataGridView.DataSource = GetClientsFromDatabase();

            //Formatage de la grille des articles
            if (customersDataGridView.Columns["ID_BASE_X"] != null)
                customersDataGridView.Columns["ID_BASE_X"].HeaderText = "identification de l'Acheteur";
            if (customersDataGridView.Columns["Name"] != null)
                customersDataGridView.Columns["Name"].HeaderText = "Nom du client";
            if (customersDataGridView.Columns["ID_EBP"] != null)
                customersDataGridView.Columns["ID_EBP"].HeaderText = "ID dans EBP";

        }

        

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void AnnulerButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EnregistrerClientButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdClientXBaseTextBox.Text) || string.IsNullOrEmpty(IdClientEBPTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs");
                return;
            }

            if (IdClientXBaseTextBox.Text.Length != 13)
            {
                MessageBox.Show("La longueur Id Client doit être :13 chiffres");
                return;
            }

            if (ExistCustomer(IdClientEBPTextBox.Text) == null)
            {
                MessageBox.Show("L'Id Client " + IdClientEBPTextBox.Text + " n'existe pas dans la base EBP. ");
                return;
            }

            if (ExistIdX(IdClientXBaseTextBox.Text) != null)
            {
                MessageBox.Show("L'Id Client est deja enregistré : " + ExistIdX(IdClientXBaseTextBox.Text) + "=" + IdClientXBaseTextBox.Text);
                return;
            }

            
            if (testClient(IdClientEBPTextBox.Text) != null)
            {
                MessageBox.Show("L'Id Client EBP est deja enregistré : " + testClient(IdClientEBPTextBox.Text) + "=" + IdClientEBPTextBox.Text);
                return;
            }
            else
            {
                using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
                {
                    try
                    {
                        sqlConnection.Open();
                        SqlCommand cmd = new SqlCommand(QueryHelper.insertClient(IdClientXBaseTextBox.Text,IdClientEBPTextBox.Text), sqlConnection);
                        // Execution
                        cmd.ExecuteNonQuery();
                        customersDataGridView.DataSource = GetClientsFromDatabase();
                        IdClientXBaseTextBox.Text = string.Empty;
                        IdClientEBPTextBox.Text = string.Empty;
                        MessageBox.Show("Succés: Nombre de lignes affectées :1");
                        
                        // Fermeture connection
                        //connection.Close();
                    }
                    catch (InvalidOperationException ex)
                    {
                        //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                        MessageBox.Show(ex.Message);
                        return;
                    }
                }
            }
        }

        private string testClient(string id_EBP)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.testExisteClient(id_EBP), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader["ID_BASE_X"] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
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

        private string ExistCustomer(string id_EBP)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.ExistCustomer(id_EBP), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader["Id"] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
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

        private string ExistIdX(string id_X)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.returnClient(id_X), sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                                return reader["ID_EBP"] as string;
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
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

        private void SupprimerClient(string idEBP,string id)
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    SqlCommand cmd = new SqlCommand(QueryHelper.deleteClient(idEBP, id), sqlConnection);
                    cmd.ExecuteReader();
                        
                   
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                    return;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    MessageBox.Show(e.Message);
                    return;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(e.Message);
                    return;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Client customer = customersDataGridView.SelectedRows[0].DataBoundItem as Client;
            DialogResult dialogResult = MessageBox.Show("Voullez-vous supprimer le client " + customer.Id_EBP + " ?", "Message de confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                SupprimerClient(customer.Id_EBP,customer.ID_BASE_X);
                customersDataGridView.DataSource = GetClientsFromDatabase();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        
        }

        private void importer_liste_Click(object sender, EventArgs e)
        {
            string fichierAexporter = "";
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "CSV|*.csv|TEXT|*.txt";
                //dialog.AddExtension = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(dialog.FileName).ToLower() == ".csv" || Path.GetExtension(dialog.FileName).ToLower() == ".txt")
                    {
                        fichierAexporter = dialog.FileName;
                        //filename = dialog.FileName;
                        //exportCustomersCommandTextBox.Text = string.Format(CommandLinesHelper.ImportFactures, exportCustomersFilenameTextBox.Text);
                    }
                }
            }

            if (!string.IsNullOrEmpty(fichierAexporter))
            {

                AllocConsole();
            
            string[] lines = System.IO.File.ReadAllLines(fichierAexporter, Encoding.Default);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != ";;" || lines[i] != "")
                {
                    string[] tab = lines[i].Split(';');

                    if (tab.Length == 2)
                    {
                        if (tab[1] == "")
                        {
                            Console.WriteLine("ERREUR(Ligne:" + (i + 1) + ") : Code GLN Null");
                            goto goOut;
                        }

                        if (tab[1].Length != 13)
                        {
                            Console.WriteLine("ERREUR(Ligne:" + (i + 1) + ") : Code GLN != 13");
                            goto goOut;
                        }

                        if (!IsNumeric(tab[1]))
                        {
                            Console.WriteLine("ERREUR(Ligne:" + (i + 1) + ") : Code GLN n'est pas numerique");
                            goto goOut;
                        }

                        //return id client from Customer
                        if (ExistCustomer(tab[0]) == null)
                        {
                            Console.WriteLine("INFO(Ligne:" + (i + 1) + ") : Id Client " + tab[0] + " n'existe pas dans la base EBP. ");
                            goto goOut; 
                        }

                        //return id client from Client_BASE_X_EBP
                        string getidClient = ExistIdX(tab[1]);
                        
                        if (getidClient != null)
                        {
                            if (getidClient == tab[0])
                            {
                                Console.WriteLine("INFO(Ligne:" + (i + 1) + ") : Code GLN deja enregistré : " + getidClient + "=" + tab[1]);
                                goto goOut;
                            }
                            else
                            {
                                Console.WriteLine("INFO(Ligne:" + (i + 1) + ") : Code GLN appartient à un autre client : " + getidClient + "=" + tab[1]);
                                goto goOut;
                            }
                        }

                        //return GLN from Client_BASE_X_EBP
                        string getGLNClient = testClient(tab[0]);
                        if (getGLNClient != null)
                        {
                            if (getGLNClient != tab[1])
                            {
                                int res = update(tab[0], tab[1]);
                                if (res == 1)
                                {
                                    Console.WriteLine("INFO(Ligne:" + (i + 1) + ") : Mise à jour de code GLN : " + tab[0] + "=" + tab[1]);
                                }
                                goto goOut;
                            }
                        }


                        int result = Inserer(tab[0], tab[1]);

                        if (result == 0)
                        {
                            Console.WriteLine("ERREUR(Ligne:" + (i + 1) + ") : Insertion NON");
                        }
                        if (result == 1)
                        {
                            Console.WriteLine("INFO(Ligne:" + (i + 1) + ") : Insertion OK");
                        }
                        



                        //if (idGLN != null)
                        //{
                        //    Console.WriteLine("INFO(Ligne:" + (i + 1) + ") : L'Id Client est deja enregistré : " + idGLN + "=" + tab[1]);
                        //    goto goOut;
                        //}

                        //return GLN from Client_BASE_X_EBP
                       


                    }
                    else
                    {
                        Console.WriteLine("ERREUR(Ligne:" + (i + 1) + ") : <> de 2");
                        goto goOut;
                    }


                goOut: ;
                }
            }

            customersDataGridView.DataSource = GetClientsFromDatabase();
            IdClientXBaseTextBox.Text = string.Empty;
            IdClientEBPTextBox.Text = string.Empty;
            //Console.ReadLine();
            //MessageBox.Show("ok");
            
                
            }
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    Client customer = customersDataGridView.SelectedRows[0].DataBoundItem as Client;

        //    using (ModifierClient form = new ModifierClient(customer.Id_EBP,customer.ID_BASE_X))
        //    {
        //        form.ShowDialog();
        //    }
        //}

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

        public int Inserer(string client,string GLN)
        {
            // Insertion dans la base sage : cbase
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {                                       //SELECT sCliCode,codebarcf from client
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(QueryHelper.insertClient(GLN,client), sqlConnection);
                    // Execution
                    int i = cmd.ExecuteNonQuery();
                        

                    return i;


                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //Console.WriteLine(DateTime.Now + " : Erreur[42] - " + ex.Message.Replace("[CBase]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""));
                    Console.WriteLine(DateTime.Now + " : Erreur[2] - " + ex.Message.Replace("[CBase]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""));

                    return 2;
                }
            }
        }

        public int update(string client, string GLN)
        {
            // Insertion dans la base sage : cbase
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {                                       //SELECT sCliCode,codebarcf from client
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand(QueryHelper.updateClient(client,GLN), sqlConnection);
                    // Execution
                    int i = cmd.ExecuteNonQuery();
                    return i;


                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //Console.WriteLine(DateTime.Now + " : Erreur[42] - " + ex.Message.Replace("[CBase]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""));
                    Console.WriteLine(DateTime.Now + " : Erreur[2] - " + ex.Message.Replace("[CBase]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""));

                    return 2;
                }
            }
        }
        
    }
}
