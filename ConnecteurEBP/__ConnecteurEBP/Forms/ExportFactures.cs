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
    public partial class ExportFactures : Form
    {
        private List<Document> FacturesAExporter;
        private Classes._Client customer;

        public ExportFactures()
        {
            InitializeComponent();
        }

        private List<Document> GetFacturesFromDataBase(string client)
        {
            try
            {
                //DocumentVente Facture = new DocumentVente();
                List<Document> listDocumentVente = new List<Document>();
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {

                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.ListFacturesClient(client), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Document documentVente = new Document(reader[0].ToString(), reader[1].ToString(), reader[2].ToString().Replace("00:00:00", ""), reader[3].ToString(),
                                    reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(),
                                    reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(),
                                    reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(),
                                    reader[16].ToString(), reader[17].ToString(), reader[18].ToString(), reader[19].ToString(),
                                    reader[20].ToString(), reader[21].ToString(), reader[22].ToString(), reader[23].ToString(),
                                     reader[24].ToString(), reader[25].ToString(), reader[26].ToString(), reader[27].ToString(),
                                      reader[28].ToString(), reader[29].ToString(), reader[30].ToString(), reader[31].ToString(),
                                    reader[32].ToString(), reader[33].ToString(), reader[34].ToString(), reader[35].ToString(),
                                    reader[36].ToString(), reader[37].ToString(), reader[38].ToString(), reader[39].ToString(),
                                    reader[40].ToString(), reader[41].ToString(), reader[42].ToString(), reader[43].ToString(),
                                    reader[44].ToString(), reader[45].ToString(), reader[46].ToString(), reader[47].ToString(),
                                     reader[48].ToString(), reader[49].ToString(), reader[50].ToString(), reader[51].ToString(),
                                      reader[52].ToString(), reader[53].ToString(), reader[54].ToString(), reader[55].ToString(),
                                    reader[56].ToString(), reader[57].ToString(), reader[58].ToString(), reader[59].ToString(),
                                    reader[60].ToString(), reader[61].ToString(), reader[62].ToString(), reader[63].ToString(),
                                    reader[64].ToString(), reader[65].ToString(), reader[66].ToString(), reader[67].ToString(),
                                    reader[68].ToString(), reader[69].ToString(), reader[70].ToString(), reader[71].ToString(),
                                     reader[72].ToString(), reader[73].ToString(), reader[74].ToString(), reader[75].ToString(),
                                      reader[76].ToString(), reader[77].ToString(), reader[78].ToString(), reader[79].ToString(),
                                    reader[80].ToString(), reader[81].ToString(), reader[82].ToString(), reader[83].ToString(),
                                    reader[84].ToString(), reader[85].ToString(), reader[86].ToString(), reader[87].ToString(),
                                    reader[88].ToString(), reader[89].ToString(), reader[90].ToString(), reader[91].ToString(),
                                    reader[92].ToString(), reader[93].ToString(), reader[94].ToString(), reader[95].ToString()
                                    );

                                listDocumentVente.Add(documentVente);
                            }
                        }
                    }
                    return listDocumentVente;

                }

            }

            catch (Exception e)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show("" + e.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        private List<Lignes> getDocumentLine(string prefixe, string codeDocument)
        {
            try
            {
                //DocumentVente Facture = new DocumentVente();
                List<Lignes> lignesDocumentVente = new List<Lignes>();
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {

                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.getLignes(prefixe, codeDocument), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Lignes ligne = new Lignes(reader[0].ToString(), reader[1].ToString(),
                                    reader[2].ToString(), reader[3].ToString(), reader[4].ToString().Replace("00:00:00", ""), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(),
                                    reader[8].ToString(), reader[9].ToString(),
                                    reader[10].ToString(), reader[11].ToString(),
                                    reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(),
                                    reader[16].ToString(), reader[17].ToString(), reader[18].ToString(), reader[19].ToString(),
                                    reader[20].ToString(), reader[21].ToString().Replace("00:00:00", ""), reader[22].ToString(), reader[23].ToString(),
                                     reader[24].ToString(), reader[25].ToString()
                                    );
                                lignesDocumentVente.Add(ligne);
                            }
                        }
                    }
                    return lignesDocumentVente;

                }

            }

            catch (Exception e)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show("" + e.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        private List<Classes._Client> GetListClients()
        {
            try
            {
                List<Classes._Client> listClient = new List<Classes._Client>();
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {

                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.ListClients(), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Classes._Client client = new Classes._Client(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(), reader[19].ToString(), reader[20].ToString(), reader[21].ToString(), reader[22].ToString());
                                listClient.Add(client);
                            }
                        }
                    }
                    return listClient;

                }

            }

            catch (Exception e)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show("" + e.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {

                textBox1.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                // Initialize the dialog that will contain the progress bar
                ProgressDialog ProgressDialog = new ProgressDialog();

                // Initialize the thread that will handle the background process
                Thread backgroundThread = new Thread(
                    new ThreadStart(() =>
                    {
                        // Set the flag that indicates if a process is currently running
                        //isProcessRunning = true;
                        for (int n = 0; n < 25; n++)
                        {
                            Thread.Sleep(1);
                            ProgressDialog.UpdateProgress(n);
                        }

                        //Affichage des clients du dossier
                        if (customersDataGridView.InvokeRequired)
                        {
                            customersDataGridView.Invoke(new MethodInvoker(delegate
                            {
                                customersDataGridView.DataSource = GetListClients();
                                for (int n = 26; n < 45; n++)
                                {
                                    Thread.Sleep(1);
                                    ProgressDialog.UpdateProgress(n);
                                }
                                importButton.Enabled = customersDataGridView.Rows.Count > 0;

                                if (customersDataGridView.Columns["sCliCode"] != null)
                                    customersDataGridView.Columns["sCliCode"].HeaderText = "Client";
                                if (customersDataGridView.Columns["sCliRaisonSoc"] != null)
                                    customersDataGridView.Columns["sCliRaisonSoc"].Visible = false;
                                if (customersDataGridView.Columns["sCliAdresse1Ligne"] != null)
                                    customersDataGridView.Columns["sCliAdresse1Ligne"].Visible = false;
                                if (customersDataGridView.Columns["sCliAdresse1CodePos"] != null)
                                    customersDataGridView.Columns["sCliAdresse1CodePos"].Visible = false;
                                if (customersDataGridView.Columns["sCliAdresse1Ville"] != null)
                                    customersDataGridView.Columns["sCliAdresse1Ville"].Visible = false;
                                if (customersDataGridView.Columns["sCliAdresse1CodePay"] != null)
                                    customersDataGridView.Columns["sCliAdresse1CodePay"].Visible = false;
                                if (customersDataGridView.Columns["sCliAdresse2Ligne"] != null)
                                    customersDataGridView.Columns["sCliAdresse2Ligne"].Visible = false;
                                if (customersDataGridView.Columns["sCliAdresse2CodePos"] != null)
                                    customersDataGridView.Columns["sCliAdresse2CodePos"].Visible = false;
                                if (customersDataGridView.Columns["sCliAdresse2Ville"] != null)
                                    customersDataGridView.Columns["sCliAdresse2Ville"].Visible = false;
                                if (customersDataGridView.Columns["sCliAdresse2CodePay"] != null)
                                    customersDataGridView.Columns["sCliAdresse2CodePay"].Visible = false;
                                if (customersDataGridView.Columns["sCliFraisPort"] != null)
                                    customersDataGridView.Columns["sCliFraisPort"].Visible = false;
                                if (customersDataGridView.Columns["sCliModeReglement"] != null)
                                    customersDataGridView.Columns["sCliModeReglement"].Visible = false;
                                if (customersDataGridView.Columns["sCliEscompte"] != null)
                                    customersDataGridView.Columns["sCliEscompte"].Visible = false;
                                if (customersDataGridView.Columns["sCliRemise"] != null)
                                    customersDataGridView.Columns["sCliRemise"].Visible = false;
                                if (customersDataGridView.Columns["sCliDevise"] != null)
                                    customersDataGridView.Columns["sCliDevise"].Visible = false;
                                if (customersDataGridView.Columns["sCli_Escompte"] != null)
                                    customersDataGridView.Columns["sCli_Escompte"].Visible = false;
                                if (customersDataGridView.Columns["sCli_RemiseProduits"] != null)
                                    customersDataGridView.Columns["sCli_RemiseProduits"].Visible = false;
                                if (customersDataGridView.Columns["sCli_RemiseServices"] != null)
                                    customersDataGridView.Columns["sCli_RemiseServices"].Visible = false;
                                if (customersDataGridView.Columns["sCli_RemiseForfaits"] != null)
                                    customersDataGridView.Columns["sCli_RemiseForfaits"].Visible = false;
                                if (customersDataGridView.Columns["sCli_Siret"] != null)
                                    customersDataGridView.Columns["sCli_Siret"].Visible = false;
                                if (customersDataGridView.Columns["codebarcf"] != null)
                                    customersDataGridView.Columns["codebarcf"].HeaderText = "GNL";
                                if (customersDataGridView.Columns["IBAN0_Contenu"] != null)
                                    customersDataGridView.Columns["IBAN0_Contenu"].Visible = false;
                                if (customersDataGridView.Columns["Bq0NomBanque"] != null)
                                    customersDataGridView.Columns["Bq0NomBanque"].Visible = false;



                                //test si un champ selectionner
                                if (customersDataGridView.SelectedRows.Count == 0)
                                {
                                    return;
                                }

                                _Client customer = customersDataGridView.SelectedRows[0].DataBoundItem as _Client;
                                if (FacturesDataGridView.InvokeRequired)
                                {
                                    FacturesDataGridView.Invoke(new MethodInvoker(delegate
                                    {
                                        FacturesDataGridView.DataSource = GetFacturesFromDataBase(customer.sCliCode);

                                        for (int n = 76; n < 90; n++)
                                        {
                                            Thread.Sleep(1);
                                            ProgressDialog.UpdateProgress(n);
                                        }

                                        importButton.Enabled = FacturesDataGridView.Rows.Count > 0;

                                        if (FacturesDataGridView.Columns["NumeroPrefixe"] != null)
                                            FacturesDataGridView.Columns["NumeroPrefixe"].HeaderText = "Préfixe";
                                        if (FacturesDataGridView.Columns["NumeroNumero"] != null)
                                            FacturesDataGridView.Columns["NumeroNumero"].HeaderText = "Numéro";
                                        if (FacturesDataGridView.Columns["CDate"] != null)
                                            FacturesDataGridView.Columns["CDate"].HeaderText = "Date";
                                        if (FacturesDataGridView.Columns["TiersCode"] != null)
                                            FacturesDataGridView.Columns["TiersCode"].HeaderText = "Client";
                                        if (FacturesDataGridView.Columns["TiersRaisonSoc"] != null)
                                            FacturesDataGridView.Columns["TiersRaisonSoc"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersAdresse1Ligne"] != null)
                                            FacturesDataGridView.Columns["TiersAdresse1Ligne"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersAdresse1CodePo"] != null)
                                            FacturesDataGridView.Columns["TiersAdresse1CodePo"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersAdresse1Ville"] != null)
                                            FacturesDataGridView.Columns["TiersAdresse1Ville"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersAdresse1CodePa"] != null)
                                            FacturesDataGridView.Columns["TiersAdresse1CodePa"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersAdresse2CodePo"] != null)
                                            FacturesDataGridView.Columns["TiersAdresse2CodePo"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersAdresse2Ville"] != null)
                                            FacturesDataGridView.Columns["TiersAdresse2Ville"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersAdresse2CodePa"] != null)
                                            FacturesDataGridView.Columns["TiersAdresse2CodePa"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersAdresse2Ligne"] != null)
                                            FacturesDataGridView.Columns["TiersAdresse2Ligne"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersFraisPort"] != null)
                                            FacturesDataGridView.Columns["TiersFraisPort"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersModeReglement"] != null)
                                            FacturesDataGridView.Columns["TiersModeReglement"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersRemise"] != null)
                                            FacturesDataGridView.Columns["TiersRemise"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersDevise"] != null)
                                            FacturesDataGridView.Columns["TiersDevise"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersCivilite"] != null)
                                            FacturesDataGridView.Columns["TiersCivilite"].Visible = false;
                                        if (FacturesDataGridView.Columns["Tiers_Escompte"] != null)
                                            FacturesDataGridView.Columns["Tiers_Escompte"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersRemsieProduits"] != null)
                                            FacturesDataGridView.Columns["TiersRemsieProduits"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersRemiseServices"] != null)
                                            FacturesDataGridView.Columns["TiersRemiseServices"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersRemiseForfaits"] != null)
                                            FacturesDataGridView.Columns["TiersRemiseForfaits"].Visible = false;
                                        if (FacturesDataGridView.Columns["Tiers_Siret"] != null)
                                            FacturesDataGridView.Columns["Tiers_Siret"].Visible = false;
                                        if (FacturesDataGridView.Columns["TotalVolume"] != null)
                                            FacturesDataGridView.Columns["TotalVolume"].Visible = false;
                                        if (FacturesDataGridView.Columns["TotalPoids"] != null)
                                            FacturesDataGridView.Columns["TotalPoids"].Visible = false;
                                        if (FacturesDataGridView.Columns["TotalColis"] != null)
                                            FacturesDataGridView.Columns["TotalColis"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA0"] != null)
                                            FacturesDataGridView.Columns["BaseTVA0"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA1"] != null)
                                            FacturesDataGridView.Columns["BaseTVA1"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA2"] != null)
                                            FacturesDataGridView.Columns["BaseTVA2"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA3"] != null)
                                            FacturesDataGridView.Columns["BaseTVA3"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA4"] != null)
                                            FacturesDataGridView.Columns["BaseTVA4"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA5"] != null)
                                            FacturesDataGridView.Columns["BaseTVA5"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA6"] != null)
                                            FacturesDataGridView.Columns["BaseTVA6"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA7"] != null)
                                            FacturesDataGridView.Columns["BaseTVA7"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA8"] != null)
                                            FacturesDataGridView.Columns["BaseTVA8"].Visible = false;
                                        if (FacturesDataGridView.Columns["BaseTVA9"] != null)
                                            FacturesDataGridView.Columns["BaseTVA9"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA0"] != null)
                                            FacturesDataGridView.Columns["TauxTVA0"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA1"] != null)
                                            FacturesDataGridView.Columns["TauxTVA1"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA2"] != null)
                                            FacturesDataGridView.Columns["TauxTVA2"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA3"] != null)
                                            FacturesDataGridView.Columns["TauxTVA3"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA4"] != null)
                                            FacturesDataGridView.Columns["TauxTVA4"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA5"] != null)
                                            FacturesDataGridView.Columns["TauxTVA5"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA6"] != null)
                                            FacturesDataGridView.Columns["TauxTVA6"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA7"] != null)
                                            FacturesDataGridView.Columns["TauxTVA7"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA8"] != null)
                                            FacturesDataGridView.Columns["TauxTVA8"].Visible = false;
                                        if (FacturesDataGridView.Columns["TauxTVA9"] != null)
                                            FacturesDataGridView.Columns["TauxTVA9"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA0"] != null)
                                            FacturesDataGridView.Columns["MntTVA0"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA1"] != null)
                                            FacturesDataGridView.Columns["MntTVA1"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA2"] != null)
                                            FacturesDataGridView.Columns["MntTVA2"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA3"] != null)
                                            FacturesDataGridView.Columns["MntTVA3"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA4"] != null)
                                            FacturesDataGridView.Columns["MntTVA4"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA5"] != null)
                                            FacturesDataGridView.Columns["MntTVA5"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA6"] != null)
                                            FacturesDataGridView.Columns["MntTVA6"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA7"] != null)
                                            FacturesDataGridView.Columns["MntTVA7"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA8"] != null)
                                            FacturesDataGridView.Columns["MntTVA8"].Visible = false;
                                        if (FacturesDataGridView.Columns["MntTVA9"] != null)
                                            FacturesDataGridView.Columns["MntTVA9"].Visible = false;
                                        if (FacturesDataGridView.Columns["Acompte"] != null)
                                            FacturesDataGridView.Columns["Acompte"].Visible = false;
                                        if (FacturesDataGridView.Columns["BrutHT"] != null)
                                            FacturesDataGridView.Columns["BrutHT"].Visible = false;
                                        if (FacturesDataGridView.Columns["TotalBrutTTC"] != null)
                                            FacturesDataGridView.Columns["TotalBrutTTC"].Visible = false;
                                        if (FacturesDataGridView.Columns["NetAPayer"] != null)
                                            FacturesDataGridView.Columns["NetAPayer"].HeaderText = "Net A Payer";
                                        if (FacturesDataGridView.Columns["FraisPort"] != null)
                                            FacturesDataGridView.Columns["FraisPort"].Visible = false;
                                        if (FacturesDataGridView.Columns["FraisSuppl"] != null)
                                            FacturesDataGridView.Columns["FraisSuppl"].Visible = false;
                                        if (FacturesDataGridView.Columns["DateLivraison"] != null)
                                            FacturesDataGridView.Columns["DateLivraison"].Visible = false;
                                        if (FacturesDataGridView.Columns["MontantEscompte"] != null)
                                            FacturesDataGridView.Columns["MontantEscompte"].Visible = false;
                                        if (FacturesDataGridView.Columns["MontantRemise"] != null)
                                            FacturesDataGridView.Columns["MontantRemise"].Visible = false;
                                        if (FacturesDataGridView.Columns["MontantRemiseTTC"] != null)
                                            FacturesDataGridView.Columns["MontantRemiseTTC"].Visible = false;
                                        if (FacturesDataGridView.Columns["DelaiLiv"] != null)
                                            FacturesDataGridView.Columns["DelaiLiv"].Visible = false;
                                        if (FacturesDataGridView.Columns["TotalPoidsNet"] != null)
                                            FacturesDataGridView.Columns["TotalPoidsNet"].Visible = false;
                                        if (FacturesDataGridView.Columns["UnitePoids"] != null)
                                            FacturesDataGridView.Columns["UnitePoids"].Visible = false;
                                        if (FacturesDataGridView.Columns["EscompteGlobal"] != null)
                                            FacturesDataGridView.Columns["EscompteGlobal"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_Civilite"] != null)
                                            FacturesDataGridView.Columns["Contact_Civilite"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_Fonction"] != null)
                                            FacturesDataGridView.Columns["Contact_Fonction"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_Nom"] != null)
                                            FacturesDataGridView.Columns["Contact_Nom"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_Prenom"] != null)
                                            FacturesDataGridView.Columns["Contact_Prenom"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_Tel"] != null)
                                            FacturesDataGridView.Columns["Contact_Tel"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_Fax"] != null)
                                            FacturesDataGridView.Columns["Contact_Fax"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_Portable"] != null)
                                            FacturesDataGridView.Columns["Contact_Portable"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_EMail"] != null)
                                            FacturesDataGridView.Columns["Contact_EMail"].Visible = false;
                                        if (FacturesDataGridView.Columns["Contact_Url"] != null)
                                            FacturesDataGridView.Columns["Contact_Url"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_Civilite"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_Civilite"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_Fonction"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_Fonction"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_Nom"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_Nom"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_Prenom"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_Prenom"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_Tel"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_Tel"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_Fax"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_Fax"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_Portable"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_Portable"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_EMail"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_EMail"].Visible = false;
                                        if (FacturesDataGridView.Columns["ContactLiv_Url"] != null)
                                            FacturesDataGridView.Columns["ContactLiv_Url"].Visible = false;
                                        if (FacturesDataGridView.Columns["IDPaiement"] != null)
                                            FacturesDataGridView.Columns["IDPaiement"].Visible = false;
                                        if (FacturesDataGridView.Columns["ModeTransport"] != null)
                                            FacturesDataGridView.Columns["ModeTransport"].Visible = false;
                                        if (FacturesDataGridView.Columns["NbArticles"] != null)
                                            FacturesDataGridView.Columns["NbArticles"].Visible = false;
                                        if (FacturesDataGridView.Columns["SPieceCodeModeRegtP"] != null)
                                            FacturesDataGridView.Columns["SPieceCodeModeRegtP"].Visible = false;
                                        if (FacturesDataGridView.Columns["SPiece_CodeTable"] != null)
                                            FacturesDataGridView.Columns["SPiece_CodeTable"].Visible = false;
                                        if (FacturesDataGridView.Columns["TiersEscompte"] != null)
                                            FacturesDataGridView.Columns["TiersEscompte"].Visible = false;
                                        if (FacturesDataGridView.Columns["Reference"] != null)
                                            FacturesDataGridView.Columns["Reference"].Visible = false;
                                    }));
                                }

                                //Récupération du prochain identifiant de commande à utiliser
                                //string nextOrderId = GetNextOrderId();
                            }));
                        }

                        for (int n = 46; n < 100; n++)
                        {
                            Thread.Sleep(1);
                            ProgressDialog.UpdateProgress(n);
                        }

                        // Close the dialog if it hasn't been already
                        if (ProgressDialog.InvokeRequired)
                            ProgressDialog.BeginInvoke(new Action(() => ProgressDialog.Close()));

                        // Reset the flag that indicates if a process is currently running
                        //isProcessRunning = false;
                    }
                ));

                // Start the background process thread
                backgroundThread.Start();

                // Open the dialog
                ProgressDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }


        public static string ConvertDate(string date)
        {
            if (date.Length == 11 || date.Length == 19)
            {
                return date.Substring(6, 4) + date.Substring(3, 2) + date.Substring(0, 2);
            }
            return date;
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

        private List<OrderLine> getLigneFactures(string code)
        {
            try
            {
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {
                    List<OrderLine> lines = new List<OrderLine>();

                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.getListLignesCommandes(code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lines.Add(new OrderLine(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString()));
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

        private void customersDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (customersDataGridView.SelectedRows.Count == 0)
                {
                    importButton.Enabled = false;
                    return;
                }


                customer = customersDataGridView.SelectedRows[0].DataBoundItem as Classes._Client;
                if (customer == null)
                    throw new NullReferenceException("customer");
                thirdIdTextBox.Text = customer.sCliCode;
                thirdNameTextBox.Text = customer.codebarcf;
                addressTextBox.Text = customer.sCliAdresse1Ligne;
                zipCodeTextBox.Text = customer.sCliAdresse1CodePos;
                cityTextBox.Text = customer.sCliAdresse1Ville;
                countryTextBox.Text = customer.sCliAdresse1CodePay;

                FacturesDataGridView.DataSource = GetFacturesFromDataBase(customer.sCliCode);
                importButton.Enabled = FacturesDataGridView.Rows.Count > 0;
                if (FacturesDataGridView.Columns["NumeroPrefixe"] != null)
                    FacturesDataGridView.Columns["NumeroPrefixe"].HeaderText = "Préfixe";
                if (FacturesDataGridView.Columns["NumeroNumero"] != null)
                    FacturesDataGridView.Columns["NumeroNumero"].HeaderText = "Numéro";
                if (FacturesDataGridView.Columns["CDate"] != null)
                    FacturesDataGridView.Columns["CDate"].HeaderText = "Date";
                if (FacturesDataGridView.Columns["TiersCode"] != null)
                    FacturesDataGridView.Columns["TiersCode"].HeaderText = "Client";
                if (FacturesDataGridView.Columns["TiersRaisonSoc"] != null)
                    FacturesDataGridView.Columns["TiersRaisonSoc"].Visible = false;
                if (FacturesDataGridView.Columns["TiersAdresse1Ligne"] != null)
                    FacturesDataGridView.Columns["TiersAdresse1Ligne"].Visible = false;
                if (FacturesDataGridView.Columns["TiersAdresse1CodePo"] != null)
                    FacturesDataGridView.Columns["TiersAdresse1CodePo"].Visible = false;
                if (FacturesDataGridView.Columns["TiersAdresse1Ville"] != null)
                    FacturesDataGridView.Columns["TiersAdresse1Ville"].Visible = false;
                if (FacturesDataGridView.Columns["TiersAdresse1CodePa"] != null)
                    FacturesDataGridView.Columns["TiersAdresse1CodePa"].Visible = false;
                if (FacturesDataGridView.Columns["TiersAdresse2CodePo"] != null)
                    FacturesDataGridView.Columns["TiersAdresse2CodePo"].Visible = false;
                if (FacturesDataGridView.Columns["TiersAdresse2Ville"] != null)
                    FacturesDataGridView.Columns["TiersAdresse2Ville"].Visible = false;
                if (FacturesDataGridView.Columns["TiersAdresse2CodePa"] != null)
                    FacturesDataGridView.Columns["TiersAdresse2CodePa"].Visible = false;
                if (FacturesDataGridView.Columns["TiersAdresse2Ligne"] != null)
                    FacturesDataGridView.Columns["TiersAdresse2Ligne"].Visible = false;
                if (FacturesDataGridView.Columns["TiersFraisPort"] != null)
                    FacturesDataGridView.Columns["TiersFraisPort"].Visible = false;
                if (FacturesDataGridView.Columns["TiersModeReglement"] != null)
                    FacturesDataGridView.Columns["TiersModeReglement"].Visible = false;
                if (FacturesDataGridView.Columns["TiersRemise"] != null)
                    FacturesDataGridView.Columns["TiersRemise"].Visible = false;
                if (FacturesDataGridView.Columns["TiersDevise"] != null)
                    FacturesDataGridView.Columns["TiersDevise"].Visible = false;
                if (FacturesDataGridView.Columns["TiersCivilite"] != null)
                    FacturesDataGridView.Columns["TiersCivilite"].Visible = false;
                if (FacturesDataGridView.Columns["Tiers_Escompte"] != null)
                    FacturesDataGridView.Columns["Tiers_Escompte"].Visible = false;
                if (FacturesDataGridView.Columns["TiersRemsieProduits"] != null)
                    FacturesDataGridView.Columns["TiersRemsieProduits"].Visible = false;
                if (FacturesDataGridView.Columns["TiersRemiseServices"] != null)
                    FacturesDataGridView.Columns["TiersRemiseServices"].Visible = false;
                if (FacturesDataGridView.Columns["TiersRemiseForfaits"] != null)
                    FacturesDataGridView.Columns["TiersRemiseForfaits"].Visible = false;
                if (FacturesDataGridView.Columns["Tiers_Siret"] != null)
                    FacturesDataGridView.Columns["Tiers_Siret"].Visible = false;
                if (FacturesDataGridView.Columns["TotalVolume"] != null)
                    FacturesDataGridView.Columns["TotalVolume"].Visible = false;
                if (FacturesDataGridView.Columns["TotalPoids"] != null)
                    FacturesDataGridView.Columns["TotalPoids"].Visible = false;
                if (FacturesDataGridView.Columns["TotalColis"] != null)
                    FacturesDataGridView.Columns["TotalColis"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA0"] != null)
                    FacturesDataGridView.Columns["BaseTVA0"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA1"] != null)
                    FacturesDataGridView.Columns["BaseTVA1"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA2"] != null)
                    FacturesDataGridView.Columns["BaseTVA2"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA3"] != null)
                    FacturesDataGridView.Columns["BaseTVA3"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA4"] != null)
                    FacturesDataGridView.Columns["BaseTVA4"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA5"] != null)
                    FacturesDataGridView.Columns["BaseTVA5"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA6"] != null)
                    FacturesDataGridView.Columns["BaseTVA6"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA7"] != null)
                    FacturesDataGridView.Columns["BaseTVA7"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA8"] != null)
                    FacturesDataGridView.Columns["BaseTVA8"].Visible = false;
                if (FacturesDataGridView.Columns["BaseTVA9"] != null)
                    FacturesDataGridView.Columns["BaseTVA9"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA0"] != null)
                    FacturesDataGridView.Columns["TauxTVA0"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA1"] != null)
                    FacturesDataGridView.Columns["TauxTVA1"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA2"] != null)
                    FacturesDataGridView.Columns["TauxTVA2"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA3"] != null)
                    FacturesDataGridView.Columns["TauxTVA3"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA4"] != null)
                    FacturesDataGridView.Columns["TauxTVA4"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA5"] != null)
                    FacturesDataGridView.Columns["TauxTVA5"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA6"] != null)
                    FacturesDataGridView.Columns["TauxTVA6"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA7"] != null)
                    FacturesDataGridView.Columns["TauxTVA7"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA8"] != null)
                    FacturesDataGridView.Columns["TauxTVA8"].Visible = false;
                if (FacturesDataGridView.Columns["TauxTVA9"] != null)
                    FacturesDataGridView.Columns["TauxTVA9"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA0"] != null)
                    FacturesDataGridView.Columns["MntTVA0"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA1"] != null)
                    FacturesDataGridView.Columns["MntTVA1"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA2"] != null)
                    FacturesDataGridView.Columns["MntTVA2"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA3"] != null)
                    FacturesDataGridView.Columns["MntTVA3"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA4"] != null)
                    FacturesDataGridView.Columns["MntTVA4"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA5"] != null)
                    FacturesDataGridView.Columns["MntTVA5"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA6"] != null)
                    FacturesDataGridView.Columns["MntTVA6"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA7"] != null)
                    FacturesDataGridView.Columns["MntTVA7"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA8"] != null)
                    FacturesDataGridView.Columns["MntTVA8"].Visible = false;
                if (FacturesDataGridView.Columns["MntTVA9"] != null)
                    FacturesDataGridView.Columns["MntTVA9"].Visible = false;
                if (FacturesDataGridView.Columns["Acompte"] != null)
                    FacturesDataGridView.Columns["Acompte"].Visible = false;
                if (FacturesDataGridView.Columns["BrutHT"] != null)
                    FacturesDataGridView.Columns["BrutHT"].Visible = false;
                if (FacturesDataGridView.Columns["TotalBrutTTC"] != null)
                    FacturesDataGridView.Columns["TotalBrutTTC"].Visible = false;
                if (FacturesDataGridView.Columns["NetAPayer"] != null)
                    FacturesDataGridView.Columns["NetAPayer"].HeaderText = "Net A Payer";
                if (FacturesDataGridView.Columns["FraisPort"] != null)
                    FacturesDataGridView.Columns["FraisPort"].Visible = false;
                if (FacturesDataGridView.Columns["FraisSuppl"] != null)
                    FacturesDataGridView.Columns["FraisSuppl"].Visible = false;
                if (FacturesDataGridView.Columns["DateLivraison"] != null)
                    FacturesDataGridView.Columns["DateLivraison"].Visible = false;
                if (FacturesDataGridView.Columns["MontantEscompte"] != null)
                    FacturesDataGridView.Columns["MontantEscompte"].Visible = false;
                if (FacturesDataGridView.Columns["MontantRemise"] != null)
                    FacturesDataGridView.Columns["MontantRemise"].Visible = false;
                if (FacturesDataGridView.Columns["MontantRemiseTTC"] != null)
                    FacturesDataGridView.Columns["MontantRemiseTTC"].Visible = false;
                if (FacturesDataGridView.Columns["DelaiLiv"] != null)
                    FacturesDataGridView.Columns["DelaiLiv"].Visible = false;
                if (FacturesDataGridView.Columns["TotalPoidsNet"] != null)
                    FacturesDataGridView.Columns["TotalPoidsNet"].Visible = false;
                if (FacturesDataGridView.Columns["UnitePoids"] != null)
                    FacturesDataGridView.Columns["UnitePoids"].Visible = false;
                if (FacturesDataGridView.Columns["EscompteGlobal"] != null)
                    FacturesDataGridView.Columns["EscompteGlobal"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_Civilite"] != null)
                    FacturesDataGridView.Columns["Contact_Civilite"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_Fonction"] != null)
                    FacturesDataGridView.Columns["Contact_Fonction"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_Nom"] != null)
                    FacturesDataGridView.Columns["Contact_Nom"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_Prenom"] != null)
                    FacturesDataGridView.Columns["Contact_Prenom"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_Tel"] != null)
                    FacturesDataGridView.Columns["Contact_Tel"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_Fax"] != null)
                    FacturesDataGridView.Columns["Contact_Fax"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_Portable"] != null)
                    FacturesDataGridView.Columns["Contact_Portable"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_EMail"] != null)
                    FacturesDataGridView.Columns["Contact_EMail"].Visible = false;
                if (FacturesDataGridView.Columns["Contact_Url"] != null)
                    FacturesDataGridView.Columns["Contact_Url"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_Civilite"] != null)
                    FacturesDataGridView.Columns["ContactLiv_Civilite"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_Fonction"] != null)
                    FacturesDataGridView.Columns["ContactLiv_Fonction"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_Nom"] != null)
                    FacturesDataGridView.Columns["ContactLiv_Nom"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_Prenom"] != null)
                    FacturesDataGridView.Columns["ContactLiv_Prenom"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_Tel"] != null)
                    FacturesDataGridView.Columns["ContactLiv_Tel"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_Fax"] != null)
                    FacturesDataGridView.Columns["ContactLiv_Fax"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_Portable"] != null)
                    FacturesDataGridView.Columns["ContactLiv_Portable"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_EMail"] != null)
                    FacturesDataGridView.Columns["ContactLiv_EMail"].Visible = false;
                if (FacturesDataGridView.Columns["ContactLiv_Url"] != null)
                    FacturesDataGridView.Columns["ContactLiv_Url"].Visible = false;
                if (FacturesDataGridView.Columns["IDPaiement"] != null)
                    FacturesDataGridView.Columns["IDPaiement"].Visible = false;
                if (FacturesDataGridView.Columns["ModeTransport"] != null)
                    FacturesDataGridView.Columns["ModeTransport"].Visible = false;
                if (FacturesDataGridView.Columns["NbArticles"] != null)
                    FacturesDataGridView.Columns["NbArticles"].Visible = false;
                if (FacturesDataGridView.Columns["SPieceCodeModeRegtP"] != null)
                    FacturesDataGridView.Columns["SPieceCodeModeRegtP"].Visible = false;
                if (FacturesDataGridView.Columns["SPiece_CodeTable"] != null)
                    FacturesDataGridView.Columns["SPiece_CodeTable"].Visible = false;
                if (FacturesDataGridView.Columns["TiersEscompte"] != null)
                    FacturesDataGridView.Columns["TiersEscompte"].Visible = false;
                if (FacturesDataGridView.Columns["Reference"] != null)
                    FacturesDataGridView.Columns["Reference"].Visible = false;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void FacturesDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            FacturesLinesDataGridView.DataSource = null;
            FacturesAExporter = new List<Document>();
            foreach (DataGridViewRow row in FacturesDataGridView.SelectedRows)
            {
                Document item = row.DataBoundItem as Document;
                if (item == null)
                    throw new NullReferenceException("item");
                FacturesAExporter.Add(item);
            }
            FacturesLinesDataGridView.DataSource = FacturesAExporter;
            importButton.Enabled = FacturesLinesDataGridView.Rows.Count > 0;
            if (FacturesLinesDataGridView.Columns["NumeroPrefixe"] != null)
                FacturesLinesDataGridView.Columns["NumeroPrefixe"].HeaderText = "Préfixe";
            if (FacturesLinesDataGridView.Columns["NumeroNumero"] != null)
                FacturesLinesDataGridView.Columns["NumeroNumero"].HeaderText = "Numéro";
            if (FacturesLinesDataGridView.Columns["CDate"] != null)
                FacturesLinesDataGridView.Columns["CDate"].HeaderText = "Date";
            if (FacturesLinesDataGridView.Columns["TiersCode"] != null)
                FacturesLinesDataGridView.Columns["TiersCode"].HeaderText = "Client";
            if (FacturesLinesDataGridView.Columns["TiersRaisonSoc"] != null)
                FacturesLinesDataGridView.Columns["TiersRaisonSoc"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersAdresse1Ligne"] != null)
                FacturesLinesDataGridView.Columns["TiersAdresse1Ligne"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersAdresse1CodePo"] != null)
                FacturesLinesDataGridView.Columns["TiersAdresse1CodePo"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersAdresse1Ville"] != null)
                FacturesLinesDataGridView.Columns["TiersAdresse1Ville"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersAdresse1CodePa"] != null)
                FacturesLinesDataGridView.Columns["TiersAdresse1CodePa"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersAdresse2CodePo"] != null)
                FacturesLinesDataGridView.Columns["TiersAdresse2CodePo"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersAdresse2Ville"] != null)
                FacturesLinesDataGridView.Columns["TiersAdresse2Ville"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersAdresse2CodePa"] != null)
                FacturesLinesDataGridView.Columns["TiersAdresse2CodePa"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersAdresse2Ligne"] != null)
                FacturesLinesDataGridView.Columns["TiersAdresse2Ligne"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersFraisPort"] != null)
                FacturesLinesDataGridView.Columns["TiersFraisPort"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersModeReglement"] != null)
                FacturesLinesDataGridView.Columns["TiersModeReglement"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersRemise"] != null)
                FacturesLinesDataGridView.Columns["TiersRemise"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersDevise"] != null)
                FacturesLinesDataGridView.Columns["TiersDevise"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersCivilite"] != null)
                FacturesLinesDataGridView.Columns["TiersCivilite"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Tiers_Escompte"] != null)
                FacturesLinesDataGridView.Columns["Tiers_Escompte"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersRemsieProduits"] != null)
                FacturesLinesDataGridView.Columns["TiersRemsieProduits"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersRemiseServices"] != null)
                FacturesLinesDataGridView.Columns["TiersRemiseServices"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersRemiseForfaits"] != null)
                FacturesLinesDataGridView.Columns["TiersRemiseForfaits"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Tiers_Siret"] != null)
                FacturesLinesDataGridView.Columns["Tiers_Siret"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TotalVolume"] != null)
                FacturesLinesDataGridView.Columns["TotalVolume"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TotalPoids"] != null)
                FacturesLinesDataGridView.Columns["TotalPoids"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TotalColis"] != null)
                FacturesLinesDataGridView.Columns["TotalColis"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA0"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA0"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA1"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA1"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA2"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA2"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA3"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA3"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA4"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA4"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA5"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA5"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA6"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA6"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA7"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA7"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA8"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA8"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BaseTVA9"] != null)
                FacturesLinesDataGridView.Columns["BaseTVA9"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA0"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA0"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA1"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA1"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA2"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA2"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA3"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA3"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA4"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA4"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA5"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA5"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA6"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA6"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA7"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA7"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA8"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA8"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TauxTVA9"] != null)
                FacturesLinesDataGridView.Columns["TauxTVA9"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA0"] != null)
                FacturesLinesDataGridView.Columns["MntTVA0"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA1"] != null)
                FacturesLinesDataGridView.Columns["MntTVA1"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA2"] != null)
                FacturesLinesDataGridView.Columns["MntTVA2"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA3"] != null)
                FacturesLinesDataGridView.Columns["MntTVA3"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA4"] != null)
                FacturesLinesDataGridView.Columns["MntTVA4"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA5"] != null)
                FacturesLinesDataGridView.Columns["MntTVA5"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA6"] != null)
                FacturesLinesDataGridView.Columns["MntTVA6"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA7"] != null)
                FacturesLinesDataGridView.Columns["MntTVA7"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA8"] != null)
                FacturesLinesDataGridView.Columns["MntTVA8"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MntTVA9"] != null)
                FacturesLinesDataGridView.Columns["MntTVA9"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Acompte"] != null)
                FacturesLinesDataGridView.Columns["Acompte"].Visible = false;
            if (FacturesLinesDataGridView.Columns["BrutHT"] != null)
                FacturesLinesDataGridView.Columns["BrutHT"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TotalBrutTTC"] != null)
                FacturesLinesDataGridView.Columns["TotalBrutTTC"].Visible = false;
            if (FacturesLinesDataGridView.Columns["NetAPayer"] != null)
                FacturesLinesDataGridView.Columns["NetAPayer"].HeaderText = "Net A Payer";
            if (FacturesLinesDataGridView.Columns["FraisPort"] != null)
                FacturesLinesDataGridView.Columns["FraisPort"].Visible = false;
            if (FacturesLinesDataGridView.Columns["FraisSuppl"] != null)
                FacturesLinesDataGridView.Columns["FraisSuppl"].Visible = false;
            if (FacturesLinesDataGridView.Columns["DateLivraison"] != null)
                FacturesLinesDataGridView.Columns["DateLivraison"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MontantEscompte"] != null)
                FacturesLinesDataGridView.Columns["MontantEscompte"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MontantRemise"] != null)
                FacturesLinesDataGridView.Columns["MontantRemise"].Visible = false;
            if (FacturesLinesDataGridView.Columns["MontantRemiseTTC"] != null)
                FacturesLinesDataGridView.Columns["MontantRemiseTTC"].Visible = false;
            if (FacturesLinesDataGridView.Columns["DelaiLiv"] != null)
                FacturesLinesDataGridView.Columns["DelaiLiv"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TotalPoidsNet"] != null)
                FacturesLinesDataGridView.Columns["TotalPoidsNet"].Visible = false;
            if (FacturesLinesDataGridView.Columns["UnitePoids"] != null)
                FacturesLinesDataGridView.Columns["UnitePoids"].Visible = false;
            if (FacturesLinesDataGridView.Columns["EscompteGlobal"] != null)
                FacturesLinesDataGridView.Columns["EscompteGlobal"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_Civilite"] != null)
                FacturesLinesDataGridView.Columns["Contact_Civilite"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_Fonction"] != null)
                FacturesLinesDataGridView.Columns["Contact_Fonction"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_Nom"] != null)
                FacturesLinesDataGridView.Columns["Contact_Nom"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_Prenom"] != null)
                FacturesLinesDataGridView.Columns["Contact_Prenom"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_Tel"] != null)
                FacturesLinesDataGridView.Columns["Contact_Tel"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_Fax"] != null)
                FacturesLinesDataGridView.Columns["Contact_Fax"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_Portable"] != null)
                FacturesLinesDataGridView.Columns["Contact_Portable"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_EMail"] != null)
                FacturesLinesDataGridView.Columns["Contact_EMail"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Contact_Url"] != null)
                FacturesLinesDataGridView.Columns["Contact_Url"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_Civilite"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_Civilite"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_Fonction"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_Fonction"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_Nom"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_Nom"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_Prenom"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_Prenom"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_Tel"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_Tel"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_Fax"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_Fax"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_Portable"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_Portable"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_EMail"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_EMail"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ContactLiv_Url"] != null)
                FacturesLinesDataGridView.Columns["ContactLiv_Url"].Visible = false;
            if (FacturesLinesDataGridView.Columns["IDPaiement"] != null)
                FacturesLinesDataGridView.Columns["IDPaiement"].Visible = false;
            if (FacturesLinesDataGridView.Columns["ModeTransport"] != null)
                FacturesLinesDataGridView.Columns["ModeTransport"].Visible = false;
            if (FacturesLinesDataGridView.Columns["NbArticles"] != null)
                FacturesLinesDataGridView.Columns["NbArticles"].Visible = false;
            if (FacturesLinesDataGridView.Columns["SPieceCodeModeRegtP"] != null)
                FacturesLinesDataGridView.Columns["SPieceCodeModeRegtP"].Visible = false;
            if (FacturesLinesDataGridView.Columns["SPiece_CodeTable"] != null)
                FacturesLinesDataGridView.Columns["SPiece_CodeTable"].Visible = false;
            if (FacturesLinesDataGridView.Columns["TiersEscompte"] != null)
                FacturesLinesDataGridView.Columns["TiersEscompte"].Visible = false;
            if (FacturesLinesDataGridView.Columns["Reference"] != null)
                FacturesLinesDataGridView.Columns["Reference"].Visible = false;

        }

        public static string addZero(string date)
        {
            if (date.Length == 1)
            {
                return "0" + date;
            }
            return date;
        }

        /// <summary>
        /// Génération du fichier d'export, lancement de l'application et exporter les factures
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

                var fileName = string.Format("Factures." + customer.sCliCode + "." + customer.codebarcf + ".{0:yyyyMMdd}.{0:hhmmss}.csv", DateTime.Now);

                using (StreamWriter writer = new StreamWriter(textBox1.Text + @"\" + fileName, false, Encoding.UTF8))
                {
                    writer.WriteLine("DEMAT-AAA;v01.0;;;" + DateTime.Today.Year + addZero(DateTime.Today.Month.ToString()) + addZero(DateTime.Today.Day.ToString()) + ";;");
                    writer.WriteLine("");
                    writer.WriteLine("");

                    for (int i = 0; i < FacturesAExporter.Count; i++)
                    {

                        //string[] tab = new string[] { "", "", "" };



                        //if (FacturesAExporter[i].OriginDocumentType == "8")
                        //{
                        //    tab = GetCommandeFacture(FacturesAExporter[i].Id).Split(';');
                        //}


                        writer.WriteLine("DEMAT-HD1;v01.0;;" + FacturesAExporter[i].NumeroNumero + ";380;9;" + ConvertDate(FacturesAExporter[i].CDate) + ";" + ConvertDate(FacturesAExporter[i].DateLivraison) + ";;;;;" + FacturesAExporter[i].SPieceCodeModeRegtP + ";" + FacturesAExporter[i].TiersRaisonSoc + ";;;0;;;" + FacturesAExporter[i].Reference + " ;;;;;;;;;;;;;;;;;;;;;;;;;" + FacturesAExporter[i].TiersDevise + ";;;;;" + FacturesAExporter[i].MontantEscompte.Replace(",", ".") + ";;;;;;" + FacturesAExporter[i].DateEch + ";;;;;;" + FacturesAExporter[i].ModeTransport + ";");
                        writer.WriteLine("");

                        writer.WriteLine("DEMAT-HD2;" + customer.codebarcf + ";" + FacturesAExporter[i].TiersRaisonSoc + ";" + FacturesAExporter[i].TiersAdresse2Ligne + ";" + FacturesAExporter[i].TiersAdresse2CodePo + ";" + FacturesAExporter[i].TiersAdresse2Ville + ";" + FacturesAExporter[i].TiersAdresse2CodePa + ";;;;;;;;;;;;;;;;;;;;;;;;;;;" + FacturesAExporter[i].TiersAdresse1Ligne + ";" + FacturesAExporter[i].TiersAdresse1CodePo + ";" + FacturesAExporter[i].TiersAdresse1Ville + ";" + FacturesAExporter[i].TiersAdresse1CodePa + ";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
                        writer.WriteLine("");

                        writer.WriteLine("DEMAT-CTA;" + FacturesAExporter[i].Contact_Fonction + ";;" + FacturesAExporter[i].Contact_Nom + " " + FacturesAExporter[i].Contact_Prenom + ";" + FacturesAExporter[i].Contact_EMail + ";" + FacturesAExporter[i].Contact_Fax + ";" + FacturesAExporter[i].Contact_Tel + ";;;;;;;;;;;;;;;;;;;;;;;;;" + FacturesAExporter[i].ContactLiv_Fonction + ";;" + FacturesAExporter[i].ContactLiv_Nom + " " + FacturesAExporter[i].ContactLiv_Prenom + ";" + FacturesAExporter[i].ContactLiv_EMail + ";" + FacturesAExporter[i].ContactLiv_Fax + ";" + FacturesAExporter[i].ContactLiv_Tel + ";;;;;;;");
                        writer.WriteLine("");

                        //if (FacturesAExporter[i].DiscountAmount != "0,00000000" || FacturesAExporter[i].DiscountAmount != "0,00000000")
                        //{
                        //    writer.WriteLine("DEMAT-REM;;A;;;;;;;;" + FacturesAExporter[i].DiscountAmount.Replace(",", ".") + ";" + FacturesAExporter[i].DiscountRate.Replace(",", ".") + ";;");
                        //    writer.WriteLine("");
                        //}

                        FacturesAExporter[i].lines = getDocumentLine(FacturesAExporter[i].NumeroPrefixe, FacturesAExporter[i].NumeroNumero);

                        int compteur_line = 10;

                        for (int j = 0; j < FacturesAExporter[i].lines.Count; j++)
                        {

                            writer.WriteLine("DEMAT-LIN;" + compteur_line + ";" + FacturesAExporter[i].lines[j].codebarre + ";EAN;;;" + customer.codebarcf + ";;;;" + FacturesAExporter[i].lines[j].Libelle + ";;" + FacturesAExporter[i].lines[j].PoidsNet.Replace(",", ".") + ";" + FacturesAExporter[i].lines[j].PoidsTotal.Replace(",", ".") + ";" + FacturesAExporter[i].lines[j].Volume.Replace(",", ".") + ";" + FacturesAExporter[i].lines[j].Quantite + ";;;;;;;;" + FacturesAExporter[i].lines[j].PxUnitBrut.Replace(",", ".") + ";;1;;;" + ConvertDate(FacturesAExporter[i].lines[j].DateLiv) + ";" + FacturesAExporter[i].lines[j].NombreColis + ";;;;;;;;;;;;;" + FacturesAExporter[i].lines[j].MontantNetTTC.Replace(",", ".") + ";;;;;;;;");
                            writer.WriteLine("");

                            //---- Remise ----

                            if (FacturesAExporter[i].lines[j].TauxRemise != "0")
                            {
                                writer.WriteLine("DEMAT-DED;;A;;;;;;;;;" + FacturesAExporter[i].lines[j].TauxRemise + ";;");
                                writer.WriteLine("");
                            }


                            compteur_line = compteur_line + 10;

                        }

                        //  Les lignes des taxes

                        int compteur_taxe = 0;

                        if (FacturesAExporter[i].TauxTVA0 != "0" || FacturesAExporter[i].MntTVA0 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA0.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA0.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA1 != "0" || FacturesAExporter[i].MntTVA1 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA1.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA1.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA2 != "0" || FacturesAExporter[i].MntTVA2 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA2.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA2.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA3 != "0" || FacturesAExporter[i].MntTVA3 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA3.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA3.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA4 != "0" || FacturesAExporter[i].MntTVA4 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA4.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA4.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA5 != "0" || FacturesAExporter[i].MntTVA5 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA5.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA5.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA6 != "0" || FacturesAExporter[i].MntTVA6 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA6.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA6.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA7 != "0" || FacturesAExporter[i].MntTVA7 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA7.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA7.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA8 != "0" || FacturesAExporter[i].MntTVA8 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA8.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA8.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        if (FacturesAExporter[i].TauxTVA9 != "0" || FacturesAExporter[i].MntTVA9 != "0")
                        {
                            writer.WriteLine("DEMAT-TTX;" + compteur_taxe + ";;TVA;" + FacturesAExporter[i].TauxTVA9.Replace(",", ".") + ";;" + FacturesAExporter[i].MntTVA9.Replace(",", ".") + ";;");
                            writer.WriteLine("");
                            compteur_taxe++;
                        }

                        writer.WriteLine("DEMAT-END;;;" + FacturesAExporter[i].NumeroNumero + ";" + FacturesAExporter[i].BrutHT.Replace(",", ".") + ";" + FacturesAExporter[i].TotalBrutTTC.Replace(",", ".") + ";;;;;" + FacturesAExporter[i].MontantEscompte.Replace(",", ".") + ";;" + FacturesAExporter[i].NetAPayer.Replace(",", ".") + ";;;;");
                        writer.WriteLine("");
                        writer.WriteLine("");
                    }

                    writer.WriteLine("DEMAT-ZZZ;v01.0;;;;");


                }

                MessageBox.Show("Nombre de facture : " + FacturesAExporter.Count, "Information !!",
                                             MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                Close();



            }
            catch (Exception ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show("" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            //importButton.Enabled = true;
            ExportFacture();
        }
    }
}
