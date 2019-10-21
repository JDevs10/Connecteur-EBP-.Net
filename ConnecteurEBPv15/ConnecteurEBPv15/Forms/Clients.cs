using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Data.Odbc;
using ConnecteurSage.Classes;
using ConnecteurSage.Helpers;
using System.IO;

namespace ConnecteurSage.Forms
{
    public partial class Clients : Form
    {

        string pathExport = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

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
        private List<ClientMini> GetClientsFromDatabase()
        {
            List<ClientMini> clients = new List<ClientMini>();
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.get_Clients_codebarre(), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clients.Add(new ClientMini(reader[0].ToString(), reader[1].ToString()));
                            }
                        }
                    }
                    return clients;
                }
                catch (Exception e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[C1] : " + e.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                                       MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }

        //public void SplashScreen()
        //{
        //    System.Windows.Forms.Application.Run(new Loading());
        //}

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                //Affichage des articles du dossier
                customersDataGridView.DataSource = GetClientsFromDatabase();

                //Formatage de la grille des articles
                if (customersDataGridView.Columns["Cli_Code"] != null)
                    customersDataGridView.Columns["Cli_Code"].HeaderText = "Code client";
                if (customersDataGridView.Columns["Cli_GLN"] != null)
                    customersDataGridView.Columns["Cli_GLN"].HeaderText = "GLN client";
            }
            catch(Exception ex)
            {
                MessageBox.Show(" ERREUR[C5] : " + ex.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

        }

        private void AnnulerButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EnregistrerClientButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(IdClientXBaseTextBox.Text) || string.IsNullOrEmpty(IdClientEBPTextBox.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs", "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (IdClientXBaseTextBox.Text.Length != 13)
            {
                MessageBox.Show("La longueur Id Client doit être =13 chiffres", "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            //if (ExistCustomer(IdClientEBPTextBox.Text) == null)
            //{
            //    MessageBox.Show("L'Id Client " + IdClientEBPTextBox.Text + " n'existe pas dans la base EBP. ");
            //    return;
            //}

            ClientMini cli = ExistGLN(IdClientXBaseTextBox.Text, IdClientEBPTextBox.Text);

            if (cli != null && cli.Cli_Code != IdClientEBPTextBox.Text)
            {
                MessageBox.Show("Code GLN Client deja enregistré : " + cli.Cli_Code+"-"+cli.Cli_GLN, "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (cli != null && cli.Cli_Code == IdClientEBPTextBox.Text && cli.Cli_GLN == IdClientXBaseTextBox.Text)
            {
                return;
            }

            //cli = testClient(IdClientEBPTextBox.Text);

            //if (cli != null)
            //{
            //    MessageBox.Show("L'Id Client EBP est deja enregistré : " + IdClientEBPTextBox.Text);
            //    return;
            //}
            else
            {
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {
                    try
                    {
                        connection.Open();
                        OdbcCommand command = new OdbcCommand(QueryHelper.insert_client_GLN(new ClientMini(IdClientEBPTextBox.Text, IdClientXBaseTextBox.Text)), connection);
                        command.ExecuteNonQuery();
                        customersDataGridView.DataSource = GetClientsFromDatabase();
                        //IdClientXBaseTextBox.Text = string.Empty;
                        //IdClientEBPTextBox.Text = string.Empty;
                            MessageBox.Show("Succés: Code GLN enregistrer pour le client.", "Info!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        
                        
                        // Fermeture connection
                        //connection.Close();
                    }
                    catch (Exception ex)
                    {
                        //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                        MessageBox.Show(" ERREUR[C4] : " + ex.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
        }

        private ClientMini testClient(string id_EBP)
        {
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    OdbcCommand command = new OdbcCommand(QueryHelper.Client_codeEBp(id_EBP), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                return (new ClientMini(reader[0].ToString(), reader[1].ToString()));
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return null;
                    }
                }
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(" ERREUR[C3] : " + e.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }



        private ClientMini ExistGLN(string NUM,string code)
        {
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    OdbcCommand command = new OdbcCommand(QueryHelper.Client_codeGLN(NUM,code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                return (new ClientMini(reader[0].ToString(), reader[1].ToString()));
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return null;
                    }
                }
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(" ERREUR[C2] : " + e.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }

        private void SupprimerClient(string idEBP,string id)
        {
            //using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            //{
            //    try
            //    {
            //        connection.Open();
            //        //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
            //        SqlCommand cmd = new SqlCommand(QueryHelper.get_Clients_codebarre(), sqlConnection);
            //        command.ExecuteReader();
                        
                   
            //    }
            //    catch (InvalidOperationException e)
            //    {
            //        //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
            //        MessageBox.Show(e.Message);
            //        return;
            //    }
            //    catch (IndexOutOfRangeException e)
            //    {
            //        //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
            //        MessageBox.Show(e.Message);
            //        return;
            //    }
            //    catch (SqlException e)
            //    {
            //        //Exceptions pouvant survenir durant l'exécution de la requête SQL
            //        MessageBox.Show(e.Message);
            //        return;
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClientMini customer = customersDataGridView.SelectedRows[0].DataBoundItem as ClientMini;
            DialogResult dialogResult = MessageBox.Show("Voullez-vous supprimer le client " + customer.Cli_GLN + " ?", "Message de confirmation", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                //SupprimerClient(customer.Id_EBP,customer.ID_BASE_X);
                customersDataGridView.DataSource = GetClientsFromDatabase();
            }
            else if (dialogResult == DialogResult.No)
            {
                return;
            }
        
        }

        private void customersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (customersDataGridView.SelectedRows.Count != 0)
            {
                ClientMini customer = customersDataGridView.SelectedRows[0].DataBoundItem as ClientMini;
                IdClientEBPTextBox.Text = customer.Cli_Code;
                IdClientXBaseTextBox.Text = customer.Cli_GLN;
            }
        }

        private ClientMini GetClient(string NUM, string code)
        {
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    OdbcCommand command = new OdbcCommand(QueryHelper.Client_codeGLN(NUM, code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                return (new ClientMini(reader[0].ToString(), reader[1].ToString()));
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return null;
                    }
                }
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(" ERREUR[C2] : " + e.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }

        private ClientExport GetClientExport(string code)
        {
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    OdbcCommand command = new OdbcCommand(QueryHelper.get_client_export(code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                return (new ClientExport(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(),
                                    reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(), reader[19].ToString(),
                                    reader[20].ToString(), reader[21].ToString(), reader[22].ToString(), reader[23].ToString(), reader[24].ToString(), reader[25].ToString(), reader[26].ToString(), reader[27].ToString(), reader[28].ToString(), reader[29].ToString(),
                                    reader[30].ToString(), reader[31].ToString(), reader[32].ToString(), reader[33].ToString(), reader[34].ToString(), reader[35].ToString(), reader[36].ToString(), reader[37].ToString(), reader[38].ToString(), reader[39].ToString(),
                                    reader[40].ToString(), reader[41].ToString(), reader[42].ToString(), reader[43].ToString(), reader[44].ToString(), reader[45].ToString(), reader[46].ToString(), reader[47].ToString(), reader[48].ToString(), reader[49].ToString(),
                                    reader[50].ToString(), reader[51].ToString(), reader[52].ToString(), reader[53].ToString(), reader[54].ToString(), reader[55].ToString(), reader[56].ToString(), reader[57].ToString(), reader[58].ToString(), reader[59].ToString(),
                                    reader[60].ToString(), reader[61].ToString(), reader[62].ToString()));
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return null;
                    }
                }
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(" ERREUR[C2] : " + e.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }

        private List<Adresse> GetClientAdresses(string code)
        {
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                List<Adresse> adresses = new List<Adresse>();
                try
                {
                    connection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    OdbcCommand command = new OdbcCommand(QueryHelper.get_client_adresse_export(code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                adresses.Add(new Adresse(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString()));
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return adresses;
                    }
                }
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(" ERREUR[C2] : " + e.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }

        private List<Contact> GetClientContact(string code)
        {
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                List<Contact> contacts = new List<Contact>();
                try
                {
                    connection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    OdbcCommand command = new OdbcCommand(QueryHelper.get_client_contact_export(code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                                contacts.Add(new Contact(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(),
                                    reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString()));
                        }
                        //Si aucun prochain identifiant n'est récupéré, retourner la valeur 00000001
                        return contacts;
                    }
                }
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(" ERREUR[C2] : " + e.Message.Replace("[HY000]", "").Replace("ERROR", "").Replace("[Pervasive]", "").Replace("[ODBC Client Interface]", "").Replace("[LNA]", "").Replace("[ODBC Engine Interface]", "").Replace("[Data Record Manager]", "").Replace("(Btrieve ", " ("), "Erreur!!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ExportClient(IdClientEBPTextBox.Text);
        }

        private void ExportClient(string code)
        {
            try
            {

                //if (string.IsNullOrEmpty(textBox1.Text))
                //{
                //    MessageBox.Show("Le chemin du fichier d'import de commande doit être renseigné");
                //    return;
                //}

                ClientExport client = GetClientExport(code);
                List<Adresse> adresses = GetClientAdresses(code) ;
                List<Contact> contacts = GetClientContact(code);


                var fileName = string.Format(code + ".{0:yyyyMMddhhmmss}.csv", DateTime.Now);

                using (StreamWriter writer = new StreamWriter(pathExport + @"\" + fileName.Replace("..", "."), false, Encoding.Default))
                {
                    writer.WriteLine("CLI-DEBUT;v1.0;{0:yyyyMMdd};", DateTime.Now);
                    writer.WriteLine("CLI-CODE;" + client.sCliCode + ";" + client.CodeBarCF + ";" + return_civilite(client.sCliCivilite) + ";"+client.sCliRaisonSoc+";;"+client.sCli_siret+";");
                    for (int i = 0; i<adresses.Count;i++)
                    {
                        writer.WriteLine("CLI-ADRESSE;" + i + ";" + (adresses[i].bFacturation == "True" ? "F" : "L") + ";" + (adresses[i].bPrincipal == "True" ? "1" : "0") + ";" + (adresses[i].Adresse_NPAI == "True" ? "1" : "0") + ";" + adresses[i].Adresse_Ligne.Replace("\r\n"," ") + ";" + adresses[i].Adresse_CodePostal + ";" + adresses[i].Adresse_Ville + ";" + adresses[i].Adresse_CodePays + ";");
                    }
                    for (int i = 0; i < contacts.Count; i++)
                    {
                        writer.WriteLine("CLI-CONTACT;" + i + ";" + (contacts[i].bPrincipal == "True" ? "1" : "0") + ";" + (contacts[i].bPrincipalLiv == "True" ? "1" : "0") + ";" + return_civilite(contacts[i].sContact_Civilite) + ";" + contacts[i].sContact_Interloc + ";" + contacts[i].sContact_Nom + ";" + contacts[i].sContact_Prenom + ";" + contacts[i].sContact_Fonction + ";" + contacts[i].sContact_Tel + ";" + contacts[i].sContact_Fax + ";" + contacts[i].sContact_Portable + ";" + contacts[i].sContact_EMail + ";" + contacts[i].sContact_Url + ";" + contacts[i].sContact_Password + ";");
                    }
                    writer.WriteLine("CLI-VENTE;" + return_type_client(client.sCliType) + ";" + client.sCliDevise + ";" + client.sCli_CodeRepr + ";" + client.sCliFraisPort + ";" + client.sCli_GrilleTarifs + ";");
                    //writer.WriteLine("CLI-INFO;commentaire;lien externe;");
                    writer.WriteLine("CLI-BANQUE;1;" + client.Bq0NomBanque + ";" + client.Bq0AdrBanque + ";" + client.IBAN0_Pays + client.IBAN0_Controle + client.IBAN0_Contenu + ";" + client.BIC0_Banque + client.BIC0_Pays + client.BIC0_Localication + client.BIC0_Branche + ";");
                    if (!string.IsNullOrEmpty(client.Bq1NomBanque) || !string.IsNullOrEmpty(client.Bq1AdrBanque) || !string.IsNullOrEmpty(client.IBAN1_Pays) || !string.IsNullOrEmpty(client.IBAN1_Controle)  || !string.IsNullOrEmpty(client.BIC1_Banque))
                    {
                        writer.WriteLine("CLI-BANQUE;2;" + client.Bq1NomBanque + ";" + client.Bq1AdrBanque + ";" + client.IBAN1_Pays + client.IBAN1_Controle + client.IBAN1_Contenu + ";" + client.BIC1_Banque + client.BIC1_Pays + client.BIC1_Localication + client.BIC1_Branche + ";");
                    }
                    if (!string.IsNullOrEmpty(client.Bq2NomBanque) || !string.IsNullOrEmpty(client.Bq2AdrBanque) || !string.IsNullOrEmpty(client.IBAN2_Pays) || !string.IsNullOrEmpty(client.IBAN2_Controle) || !string.IsNullOrEmpty(client.BIC2_Banque))
                    {
                        writer.WriteLine("CLI-BANQUE;3;" + client.Bq2NomBanque + ";" + client.Bq2AdrBanque + ";" + client.IBAN2_Pays + client.IBAN2_Controle + client.IBAN2_Contenu + ";" + client.BIC2_Banque + client.BIC2_Pays + client.BIC2_Localication + client.BIC2_Branche + ";");
                    }
                    if (!string.IsNullOrEmpty(client.Bq3NomBanque) || !string.IsNullOrEmpty(client.Bq3AdrBanque) || !string.IsNullOrEmpty(client.IBAN3_Pays) || !string.IsNullOrEmpty(client.IBAN3_Controle) || !string.IsNullOrEmpty(client.BIC3_Banque))
                    {
                        writer.WriteLine("CLI-BANQUE;4;" + client.Bq3NomBanque + ";" + client.Bq3AdrBanque + ";" + client.IBAN3_Pays + client.IBAN3_Controle + client.IBAN3_Contenu + ";" + client.BIC3_Banque + client.BIC3_Pays + client.BIC3_Localication + client.BIC3_Branche + ";");
                    }
                        
                    writer.WriteLine("CLI-REGL;" + client.sCliModeReglement + ";");
                    writer.WriteLine("CLI-FIN;");
                }

                MessageBox.Show("Client exportée avec succés", "Information !!",
                                             MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                //Close();



            }
            catch (Exception ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show(ex.Message);
            }
        }

        public string return_civilite(string code)
        {
            switch (code)
            {
                case "SA": return "SA"; 
                case "SARL": return "SARL"; 
                case "EURL": return "EURL"; 
                case "Association": return "Assoc";
                case "Monsieur": return "M"; 
                case "Madame": return "Mme"; 
                case "Mademoiselle": return "Mlle";
                default: return null;
            }
        }

        public string return_type_client(string code)
        {
            if (code == "")
            {
                return "1";
            }
            else if(code == "")
            {
                return "2";
            }
            else if (code == "")
            {
                 return "3";
            }
            else
            {
                return "0";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (ImportClient form = new ImportClient())
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

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    Client customer = customersDataGridView.SelectedRows[0].DataBoundItem as Client;

        //    using (ModifierClient form = new ModifierClient(customer.Id_EBP,customer.ID_BASE_X))
        //    {
        //        form.ShowDialog();
        //    }
        //}


        
    }
}
