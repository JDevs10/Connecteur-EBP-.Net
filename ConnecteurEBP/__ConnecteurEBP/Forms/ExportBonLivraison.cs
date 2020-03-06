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
    public partial class ExportBonLivraison : Form
    {
        public ExportBonLivraison()
        {
            InitializeComponent();
        }

        private List<Document> BonLivrasonAExporter;
        private _Client customer;


        private List<Document> GetBonLivraisonFromDataBase(string client)
        {
            try
            {
                //DocumentVente Facture = new DocumentVente();
                List<Document> listDocument = new List<Document>();
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {
                    //Document documentVente;
                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.ListBonLivraison(client), connection);
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
                                    reader[92].ToString(), reader[93].ToString(), null, null
                                    );
                                listDocument.Add(documentVente);
                            }
                        }
                    }
                    return listDocument;

                }

            }

            catch (Exception e)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show("" + e.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""), "Erreur!!",
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
                MessageBox.Show("" + e.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""), "Erreur!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        private List<_Client> GetListClients()
        {
            try
            {
                List<_Client> listClient = new List<_Client>();
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
                                _Client client = new _Client(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString(), reader[12].ToString(), reader[13].ToString(), reader[14].ToString(), reader[15].ToString(), reader[16].ToString(), reader[17].ToString(), reader[18].ToString(), reader[19].ToString(), reader[20].ToString(), reader[21].ToString(), reader[22].ToString());
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
                MessageBox.Show("" + e.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""), "Erreur!!",
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
                                if (BonLivraisonDataGridView.InvokeRequired)
                                {
                                    BonLivraisonDataGridView.Invoke(new MethodInvoker(delegate
                                    {
                                        BonLivraisonDataGridView.DataSource = GetBonLivraisonFromDataBase(customer.sCliCode);

                                        for (int n = 76; n < 90; n++)
                                        {
                                            Thread.Sleep(1);
                                            ProgressDialog.UpdateProgress(n);
                                        }

                                        importButton.Enabled = BonLivraisonDataGridView.Rows.Count > 0;

                                        if (BonLivraisonDataGridView.Columns["NumeroPrefixe"] != null)
                                            BonLivraisonDataGridView.Columns["NumeroPrefixe"].HeaderText = "Préfixe";
                                        if (BonLivraisonDataGridView.Columns["NumeroNumero"] != null)
                                            BonLivraisonDataGridView.Columns["NumeroNumero"].HeaderText = "Numéro";
                                        if (BonLivraisonDataGridView.Columns["CDate"] != null)
                                            BonLivraisonDataGridView.Columns["CDate"].HeaderText = "Date";
                                        if (BonLivraisonDataGridView.Columns["TiersCode"] != null)
                                            BonLivraisonDataGridView.Columns["TiersCode"].HeaderText = "Client";
                                        if (BonLivraisonDataGridView.Columns["TiersRaisonSoc"] != null)
                                            BonLivraisonDataGridView.Columns["TiersRaisonSoc"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersAdresse1Ligne"] != null)
                                            BonLivraisonDataGridView.Columns["TiersAdresse1Ligne"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersAdresse1CodePo"] != null)
                                            BonLivraisonDataGridView.Columns["TiersAdresse1CodePo"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersAdresse1Ville"] != null)
                                            BonLivraisonDataGridView.Columns["TiersAdresse1Ville"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersAdresse1CodePa"] != null)
                                            BonLivraisonDataGridView.Columns["TiersAdresse1CodePa"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersAdresse2CodePo"] != null)
                                            BonLivraisonDataGridView.Columns["TiersAdresse2CodePo"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersAdresse2Ville"] != null)
                                            BonLivraisonDataGridView.Columns["TiersAdresse2Ville"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersAdresse2CodePa"] != null)
                                            BonLivraisonDataGridView.Columns["TiersAdresse2CodePa"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersAdresse2Ligne"] != null)
                                            BonLivraisonDataGridView.Columns["TiersAdresse2Ligne"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersFraisPort"] != null)
                                            BonLivraisonDataGridView.Columns["TiersFraisPort"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersModeReglement"] != null)
                                            BonLivraisonDataGridView.Columns["TiersModeReglement"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersRemise"] != null)
                                            BonLivraisonDataGridView.Columns["TiersRemise"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersDevise"] != null)
                                            BonLivraisonDataGridView.Columns["TiersDevise"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersCivilite"] != null)
                                            BonLivraisonDataGridView.Columns["TiersCivilite"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Tiers_Escompte"] != null)
                                            BonLivraisonDataGridView.Columns["Tiers_Escompte"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersRemsieProduits"] != null)
                                            BonLivraisonDataGridView.Columns["TiersRemsieProduits"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersRemiseServices"] != null)
                                            BonLivraisonDataGridView.Columns["TiersRemiseServices"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersRemiseForfaits"] != null)
                                            BonLivraisonDataGridView.Columns["TiersRemiseForfaits"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Tiers_Siret"] != null)
                                            BonLivraisonDataGridView.Columns["Tiers_Siret"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TotalVolume"] != null)
                                            BonLivraisonDataGridView.Columns["TotalVolume"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TotalPoids"] != null)
                                            BonLivraisonDataGridView.Columns["TotalPoids"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TotalColis"] != null)
                                            BonLivraisonDataGridView.Columns["TotalColis"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA0"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA0"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA1"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA1"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA2"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA2"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA3"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA3"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA4"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA4"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA5"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA5"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA6"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA6"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA7"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA7"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA8"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA8"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BaseTVA9"] != null)
                                            BonLivraisonDataGridView.Columns["BaseTVA9"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA0"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA0"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA1"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA1"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA2"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA2"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA3"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA3"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA4"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA4"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA5"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA5"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA6"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA6"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA7"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA7"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA8"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA8"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TauxTVA9"] != null)
                                            BonLivraisonDataGridView.Columns["TauxTVA9"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA0"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA0"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA1"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA1"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA2"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA2"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA3"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA3"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA4"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA4"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA5"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA5"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA6"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA6"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA7"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA7"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA8"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA8"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MntTVA9"] != null)
                                            BonLivraisonDataGridView.Columns["MntTVA9"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Acompte"] != null)
                                            BonLivraisonDataGridView.Columns["Acompte"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["BrutHT"] != null)
                                            BonLivraisonDataGridView.Columns["BrutHT"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TotalBrutTTC"] != null)
                                            BonLivraisonDataGridView.Columns["TotalBrutTTC"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["NetAPayer"] != null)
                                            BonLivraisonDataGridView.Columns["NetAPayer"].HeaderText = "Net A Payer";
                                        if (BonLivraisonDataGridView.Columns["FraisPort"] != null)
                                            BonLivraisonDataGridView.Columns["FraisPort"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["FraisSuppl"] != null)
                                            BonLivraisonDataGridView.Columns["FraisSuppl"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["DateLivraison"] != null)
                                            BonLivraisonDataGridView.Columns["DateLivraison"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MontantEscompte"] != null)
                                            BonLivraisonDataGridView.Columns["MontantEscompte"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MontantRemise"] != null)
                                            BonLivraisonDataGridView.Columns["MontantRemise"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["MontantRemiseTTC"] != null)
                                            BonLivraisonDataGridView.Columns["MontantRemiseTTC"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["DelaiLiv"] != null)
                                            BonLivraisonDataGridView.Columns["DelaiLiv"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TotalPoidsNet"] != null)
                                            BonLivraisonDataGridView.Columns["TotalPoidsNet"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["UnitePoids"] != null)
                                            BonLivraisonDataGridView.Columns["UnitePoids"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["EscompteGlobal"] != null)
                                            BonLivraisonDataGridView.Columns["EscompteGlobal"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_Civilite"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_Civilite"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_Fonction"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_Fonction"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_Nom"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_Nom"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_Prenom"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_Prenom"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_Tel"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_Tel"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_Fax"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_Fax"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_Portable"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_Portable"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_EMail"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_EMail"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["Contact_Url"] != null)
                                            BonLivraisonDataGridView.Columns["Contact_Url"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_Civilite"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_Civilite"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_Fonction"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_Fonction"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_Nom"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_Nom"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_Prenom"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_Prenom"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_Tel"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_Tel"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_Fax"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_Fax"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_Portable"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_Portable"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_EMail"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_EMail"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ContactLiv_Url"] != null)
                                            BonLivraisonDataGridView.Columns["ContactLiv_Url"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["IDPaiement"] != null)
                                            BonLivraisonDataGridView.Columns["IDPaiement"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["ModeTransport"] != null)
                                            BonLivraisonDataGridView.Columns["ModeTransport"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["NbArticles"] != null)
                                            BonLivraisonDataGridView.Columns["NbArticles"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["SPieceCodeModeRegtP"] != null)
                                            BonLivraisonDataGridView.Columns["SPieceCodeModeRegtP"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["SPiece_CodeTable"] != null)
                                            BonLivraisonDataGridView.Columns["SPiece_CodeTable"].Visible = false;
                                        if (BonLivraisonDataGridView.Columns["TiersEscompte"] != null)
                                            BonLivraisonDataGridView.Columns["TiersEscompte"].Visible = false;


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
                MessageBox.Show("" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""), "Erreur!!",
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
                MessageBox.Show("" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""), "Erreur!!",
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

                customer = customersDataGridView.SelectedRows[0].DataBoundItem as _Client;
                if (customer == null)
                    throw new NullReferenceException("customer");
                thirdIdTextBox.Text = customer.sCliCode;
                thirdNameTextBox.Text = customer.codebarcf;
                addressTextBox.Text = customer.sCliAdresse1Ligne;
                zipCodeTextBox.Text = customer.sCliAdresse1CodePos;
                cityTextBox.Text = customer.sCliAdresse1Ville;
                countryTextBox.Text = customer.sCliAdresse1CodePay;

                BonLivraisonDataGridView.DataSource = GetBonLivraisonFromDataBase(customer.sCliCode);
                importButton.Enabled = BonLivraisonDataGridView.Rows.Count > 0;
                if (BonLivraisonDataGridView.Columns["NumeroPrefixe"] != null)
                    BonLivraisonDataGridView.Columns["NumeroPrefixe"].HeaderText = "Préfixe";
                if (BonLivraisonDataGridView.Columns["NumeroNumero"] != null)
                    BonLivraisonDataGridView.Columns["NumeroNumero"].HeaderText = "Numéro";
                if (BonLivraisonDataGridView.Columns["CDate"] != null)
                    BonLivraisonDataGridView.Columns["CDate"].HeaderText = "Date";
                if (BonLivraisonDataGridView.Columns["TiersCode"] != null)
                    BonLivraisonDataGridView.Columns["TiersCode"].HeaderText = "Client";
                if (BonLivraisonDataGridView.Columns["TiersRaisonSoc"] != null)
                    BonLivraisonDataGridView.Columns["TiersRaisonSoc"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersAdresse1Ligne"] != null)
                    BonLivraisonDataGridView.Columns["TiersAdresse1Ligne"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersAdresse1CodePo"] != null)
                    BonLivraisonDataGridView.Columns["TiersAdresse1CodePo"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersAdresse1Ville"] != null)
                    BonLivraisonDataGridView.Columns["TiersAdresse1Ville"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersAdresse1CodePa"] != null)
                    BonLivraisonDataGridView.Columns["TiersAdresse1CodePa"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersAdresse2CodePo"] != null)
                    BonLivraisonDataGridView.Columns["TiersAdresse2CodePo"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersAdresse2Ville"] != null)
                    BonLivraisonDataGridView.Columns["TiersAdresse2Ville"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersAdresse2CodePa"] != null)
                    BonLivraisonDataGridView.Columns["TiersAdresse2CodePa"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersAdresse2Ligne"] != null)
                    BonLivraisonDataGridView.Columns["TiersAdresse2Ligne"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersFraisPort"] != null)
                    BonLivraisonDataGridView.Columns["TiersFraisPort"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersModeReglement"] != null)
                    BonLivraisonDataGridView.Columns["TiersModeReglement"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersRemise"] != null)
                    BonLivraisonDataGridView.Columns["TiersRemise"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersDevise"] != null)
                    BonLivraisonDataGridView.Columns["TiersDevise"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersCivilite"] != null)
                    BonLivraisonDataGridView.Columns["TiersCivilite"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Tiers_Escompte"] != null)
                    BonLivraisonDataGridView.Columns["Tiers_Escompte"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersRemsieProduits"] != null)
                    BonLivraisonDataGridView.Columns["TiersRemsieProduits"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersRemiseServices"] != null)
                    BonLivraisonDataGridView.Columns["TiersRemiseServices"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersRemiseForfaits"] != null)
                    BonLivraisonDataGridView.Columns["TiersRemiseForfaits"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Tiers_Siret"] != null)
                    BonLivraisonDataGridView.Columns["Tiers_Siret"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TotalVolume"] != null)
                    BonLivraisonDataGridView.Columns["TotalVolume"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TotalPoids"] != null)
                    BonLivraisonDataGridView.Columns["TotalPoids"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TotalColis"] != null)
                    BonLivraisonDataGridView.Columns["TotalColis"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA0"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA0"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA1"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA1"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA2"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA2"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA3"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA3"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA4"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA4"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA5"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA5"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA6"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA6"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA7"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA7"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA8"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA8"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BaseTVA9"] != null)
                    BonLivraisonDataGridView.Columns["BaseTVA9"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA0"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA0"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA1"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA1"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA2"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA2"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA3"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA3"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA4"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA4"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA5"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA5"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA6"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA6"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA7"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA7"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA8"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA8"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TauxTVA9"] != null)
                    BonLivraisonDataGridView.Columns["TauxTVA9"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA0"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA0"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA1"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA1"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA2"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA2"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA3"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA3"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA4"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA4"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA5"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA5"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA6"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA6"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA7"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA7"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA8"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA8"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MntTVA9"] != null)
                    BonLivraisonDataGridView.Columns["MntTVA9"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Acompte"] != null)
                    BonLivraisonDataGridView.Columns["Acompte"].Visible = false;
                if (BonLivraisonDataGridView.Columns["BrutHT"] != null)
                    BonLivraisonDataGridView.Columns["BrutHT"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TotalBrutTTC"] != null)
                    BonLivraisonDataGridView.Columns["TotalBrutTTC"].Visible = false;
                if (BonLivraisonDataGridView.Columns["NetAPayer"] != null)
                    BonLivraisonDataGridView.Columns["NetAPayer"].HeaderText = "Net A Payer";
                if (BonLivraisonDataGridView.Columns["FraisPort"] != null)
                    BonLivraisonDataGridView.Columns["FraisPort"].Visible = false;
                if (BonLivraisonDataGridView.Columns["FraisSuppl"] != null)
                    BonLivraisonDataGridView.Columns["FraisSuppl"].Visible = false;
                if (BonLivraisonDataGridView.Columns["DateLivraison"] != null)
                    BonLivraisonDataGridView.Columns["DateLivraison"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MontantEscompte"] != null)
                    BonLivraisonDataGridView.Columns["MontantEscompte"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MontantRemise"] != null)
                    BonLivraisonDataGridView.Columns["MontantRemise"].Visible = false;
                if (BonLivraisonDataGridView.Columns["MontantRemiseTTC"] != null)
                    BonLivraisonDataGridView.Columns["MontantRemiseTTC"].Visible = false;
                if (BonLivraisonDataGridView.Columns["DelaiLiv"] != null)
                    BonLivraisonDataGridView.Columns["DelaiLiv"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TotalPoidsNet"] != null)
                    BonLivraisonDataGridView.Columns["TotalPoidsNet"].Visible = false;
                if (BonLivraisonDataGridView.Columns["UnitePoids"] != null)
                    BonLivraisonDataGridView.Columns["UnitePoids"].Visible = false;
                if (BonLivraisonDataGridView.Columns["EscompteGlobal"] != null)
                    BonLivraisonDataGridView.Columns["EscompteGlobal"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_Civilite"] != null)
                    BonLivraisonDataGridView.Columns["Contact_Civilite"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_Fonction"] != null)
                    BonLivraisonDataGridView.Columns["Contact_Fonction"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_Nom"] != null)
                    BonLivraisonDataGridView.Columns["Contact_Nom"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_Prenom"] != null)
                    BonLivraisonDataGridView.Columns["Contact_Prenom"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_Tel"] != null)
                    BonLivraisonDataGridView.Columns["Contact_Tel"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_Fax"] != null)
                    BonLivraisonDataGridView.Columns["Contact_Fax"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_Portable"] != null)
                    BonLivraisonDataGridView.Columns["Contact_Portable"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_EMail"] != null)
                    BonLivraisonDataGridView.Columns["Contact_EMail"].Visible = false;
                if (BonLivraisonDataGridView.Columns["Contact_Url"] != null)
                    BonLivraisonDataGridView.Columns["Contact_Url"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_Civilite"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_Civilite"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_Fonction"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_Fonction"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_Nom"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_Nom"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_Prenom"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_Prenom"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_Tel"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_Tel"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_Fax"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_Fax"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_Portable"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_Portable"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_EMail"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_EMail"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ContactLiv_Url"] != null)
                    BonLivraisonDataGridView.Columns["ContactLiv_Url"].Visible = false;
                if (BonLivraisonDataGridView.Columns["IDPaiement"] != null)
                    BonLivraisonDataGridView.Columns["IDPaiement"].Visible = false;
                if (BonLivraisonDataGridView.Columns["ModeTransport"] != null)
                    BonLivraisonDataGridView.Columns["ModeTransport"].Visible = false;
                if (BonLivraisonDataGridView.Columns["NbArticles"] != null)
                    BonLivraisonDataGridView.Columns["NbArticles"].Visible = false;
                if (BonLivraisonDataGridView.Columns["SPieceCodeModeRegtP"] != null)
                    BonLivraisonDataGridView.Columns["SPieceCodeModeRegtP"].Visible = false;
                if (BonLivraisonDataGridView.Columns["SPiece_CodeTable"] != null)
                    BonLivraisonDataGridView.Columns["SPiece_CodeTable"].Visible = false;
                if (BonLivraisonDataGridView.Columns["TiersEscompte"] != null)
                    BonLivraisonDataGridView.Columns["TiersEscompte"].Visible = false;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void BonLivraisonDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            LinesDataGridView.DataSource = null;
            BonLivrasonAExporter = new List<Document>();
            foreach (DataGridViewRow row in BonLivraisonDataGridView.SelectedRows)
            {
                Document item = row.DataBoundItem as Document;
                if (item == null)
                    throw new NullReferenceException("item");
                BonLivrasonAExporter.Add(item);
            }
            LinesDataGridView.DataSource = BonLivrasonAExporter;
            importButton.Enabled = LinesDataGridView.Rows.Count > 0;
            if (LinesDataGridView.Columns["NumeroPrefixe"] != null)
                LinesDataGridView.Columns["NumeroPrefixe"].HeaderText = "Préfixe";
            if (LinesDataGridView.Columns["NumeroNumero"] != null)
                LinesDataGridView.Columns["NumeroNumero"].HeaderText = "Numéro";
            if (LinesDataGridView.Columns["CDate"] != null)
                LinesDataGridView.Columns["CDate"].HeaderText = "Date";
            if (LinesDataGridView.Columns["TiersCode"] != null)
                LinesDataGridView.Columns["TiersCode"].HeaderText = "Client";
            if (LinesDataGridView.Columns["TiersRaisonSoc"] != null)
                LinesDataGridView.Columns["TiersRaisonSoc"].Visible = false;
            if (LinesDataGridView.Columns["TiersAdresse1Ligne"] != null)
                LinesDataGridView.Columns["TiersAdresse1Ligne"].Visible = false;
            if (LinesDataGridView.Columns["TiersAdresse1CodePo"] != null)
                LinesDataGridView.Columns["TiersAdresse1CodePo"].Visible = false;
            if (LinesDataGridView.Columns["TiersAdresse1Ville"] != null)
                LinesDataGridView.Columns["TiersAdresse1Ville"].Visible = false;
            if (LinesDataGridView.Columns["TiersAdresse1CodePa"] != null)
                LinesDataGridView.Columns["TiersAdresse1CodePa"].Visible = false;
            if (LinesDataGridView.Columns["TiersAdresse2CodePo"] != null)
                LinesDataGridView.Columns["TiersAdresse2CodePo"].Visible = false;
            if (LinesDataGridView.Columns["TiersAdresse2Ville"] != null)
                LinesDataGridView.Columns["TiersAdresse2Ville"].Visible = false;
            if (LinesDataGridView.Columns["TiersAdresse2CodePa"] != null)
                LinesDataGridView.Columns["TiersAdresse2CodePa"].Visible = false;
            if (LinesDataGridView.Columns["TiersAdresse2Ligne"] != null)
                LinesDataGridView.Columns["TiersAdresse2Ligne"].Visible = false;
            if (LinesDataGridView.Columns["TiersFraisPort"] != null)
                LinesDataGridView.Columns["TiersFraisPort"].Visible = false;
            if (LinesDataGridView.Columns["TiersModeReglement"] != null)
                LinesDataGridView.Columns["TiersModeReglement"].Visible = false;
            if (LinesDataGridView.Columns["TiersRemise"] != null)
                LinesDataGridView.Columns["TiersRemise"].Visible = false;
            if (LinesDataGridView.Columns["TiersDevise"] != null)
                LinesDataGridView.Columns["TiersDevise"].Visible = false;
            if (LinesDataGridView.Columns["TiersCivilite"] != null)
                LinesDataGridView.Columns["TiersCivilite"].Visible = false;
            if (LinesDataGridView.Columns["Tiers_Escompte"] != null)
                LinesDataGridView.Columns["Tiers_Escompte"].Visible = false;
            if (LinesDataGridView.Columns["TiersRemsieProduits"] != null)
                LinesDataGridView.Columns["TiersRemsieProduits"].Visible = false;
            if (LinesDataGridView.Columns["TiersRemiseServices"] != null)
                LinesDataGridView.Columns["TiersRemiseServices"].Visible = false;
            if (LinesDataGridView.Columns["TiersRemiseForfaits"] != null)
                LinesDataGridView.Columns["TiersRemiseForfaits"].Visible = false;
            if (LinesDataGridView.Columns["Tiers_Siret"] != null)
                LinesDataGridView.Columns["Tiers_Siret"].Visible = false;
            if (LinesDataGridView.Columns["TotalVolume"] != null)
                LinesDataGridView.Columns["TotalVolume"].Visible = false;
            if (LinesDataGridView.Columns["TotalPoids"] != null)
                LinesDataGridView.Columns["TotalPoids"].Visible = false;
            if (LinesDataGridView.Columns["TotalColis"] != null)
                LinesDataGridView.Columns["TotalColis"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA0"] != null)
                LinesDataGridView.Columns["BaseTVA0"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA1"] != null)
                LinesDataGridView.Columns["BaseTVA1"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA2"] != null)
                LinesDataGridView.Columns["BaseTVA2"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA3"] != null)
                LinesDataGridView.Columns["BaseTVA3"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA4"] != null)
                LinesDataGridView.Columns["BaseTVA4"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA5"] != null)
                LinesDataGridView.Columns["BaseTVA5"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA6"] != null)
                LinesDataGridView.Columns["BaseTVA6"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA7"] != null)
                LinesDataGridView.Columns["BaseTVA7"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA8"] != null)
                LinesDataGridView.Columns["BaseTVA8"].Visible = false;
            if (LinesDataGridView.Columns["BaseTVA9"] != null)
                LinesDataGridView.Columns["BaseTVA9"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA0"] != null)
                LinesDataGridView.Columns["TauxTVA0"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA1"] != null)
                LinesDataGridView.Columns["TauxTVA1"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA2"] != null)
                LinesDataGridView.Columns["TauxTVA2"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA3"] != null)
                LinesDataGridView.Columns["TauxTVA3"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA4"] != null)
                LinesDataGridView.Columns["TauxTVA4"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA5"] != null)
                LinesDataGridView.Columns["TauxTVA5"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA6"] != null)
                LinesDataGridView.Columns["TauxTVA6"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA7"] != null)
                LinesDataGridView.Columns["TauxTVA7"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA8"] != null)
                LinesDataGridView.Columns["TauxTVA8"].Visible = false;
            if (LinesDataGridView.Columns["TauxTVA9"] != null)
                LinesDataGridView.Columns["TauxTVA9"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA0"] != null)
                LinesDataGridView.Columns["MntTVA0"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA1"] != null)
                LinesDataGridView.Columns["MntTVA1"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA2"] != null)
                LinesDataGridView.Columns["MntTVA2"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA3"] != null)
                LinesDataGridView.Columns["MntTVA3"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA4"] != null)
                LinesDataGridView.Columns["MntTVA4"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA5"] != null)
                LinesDataGridView.Columns["MntTVA5"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA6"] != null)
                LinesDataGridView.Columns["MntTVA6"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA7"] != null)
                LinesDataGridView.Columns["MntTVA7"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA8"] != null)
                LinesDataGridView.Columns["MntTVA8"].Visible = false;
            if (LinesDataGridView.Columns["MntTVA9"] != null)
                LinesDataGridView.Columns["MntTVA9"].Visible = false;
            if (LinesDataGridView.Columns["Acompte"] != null)
                LinesDataGridView.Columns["Acompte"].Visible = false;
            if (LinesDataGridView.Columns["BrutHT"] != null)
                LinesDataGridView.Columns["BrutHT"].Visible = false;
            if (LinesDataGridView.Columns["TotalBrutTTC"] != null)
                LinesDataGridView.Columns["TotalBrutTTC"].Visible = false;
            if (LinesDataGridView.Columns["NetAPayer"] != null)
                LinesDataGridView.Columns["NetAPayer"].HeaderText = "Net A Payer";
            if (LinesDataGridView.Columns["FraisPort"] != null)
                LinesDataGridView.Columns["FraisPort"].Visible = false;
            if (LinesDataGridView.Columns["FraisSuppl"] != null)
                LinesDataGridView.Columns["FraisSuppl"].Visible = false;
            if (LinesDataGridView.Columns["DateLivraison"] != null)
                LinesDataGridView.Columns["DateLivraison"].Visible = false;
            if (LinesDataGridView.Columns["MontantEscompte"] != null)
                LinesDataGridView.Columns["MontantEscompte"].Visible = false;
            if (LinesDataGridView.Columns["MontantRemise"] != null)
                LinesDataGridView.Columns["MontantRemise"].Visible = false;
            if (LinesDataGridView.Columns["MontantRemiseTTC"] != null)
                LinesDataGridView.Columns["MontantRemiseTTC"].Visible = false;
            if (LinesDataGridView.Columns["DelaiLiv"] != null)
                LinesDataGridView.Columns["DelaiLiv"].Visible = false;
            if (LinesDataGridView.Columns["TotalPoidsNet"] != null)
                LinesDataGridView.Columns["TotalPoidsNet"].Visible = false;
            if (LinesDataGridView.Columns["UnitePoids"] != null)
                LinesDataGridView.Columns["UnitePoids"].Visible = false;
            if (LinesDataGridView.Columns["EscompteGlobal"] != null)
                LinesDataGridView.Columns["EscompteGlobal"].Visible = false;
            if (LinesDataGridView.Columns["Contact_Civilite"] != null)
                LinesDataGridView.Columns["Contact_Civilite"].Visible = false;
            if (LinesDataGridView.Columns["Contact_Fonction"] != null)
                LinesDataGridView.Columns["Contact_Fonction"].Visible = false;
            if (LinesDataGridView.Columns["Contact_Nom"] != null)
                LinesDataGridView.Columns["Contact_Nom"].Visible = false;
            if (LinesDataGridView.Columns["Contact_Prenom"] != null)
                LinesDataGridView.Columns["Contact_Prenom"].Visible = false;
            if (LinesDataGridView.Columns["Contact_Tel"] != null)
                LinesDataGridView.Columns["Contact_Tel"].Visible = false;
            if (LinesDataGridView.Columns["Contact_Fax"] != null)
                LinesDataGridView.Columns["Contact_Fax"].Visible = false;
            if (LinesDataGridView.Columns["Contact_Portable"] != null)
                LinesDataGridView.Columns["Contact_Portable"].Visible = false;
            if (LinesDataGridView.Columns["Contact_EMail"] != null)
                LinesDataGridView.Columns["Contact_EMail"].Visible = false;
            if (LinesDataGridView.Columns["Contact_Url"] != null)
                LinesDataGridView.Columns["Contact_Url"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_Civilite"] != null)
                LinesDataGridView.Columns["ContactLiv_Civilite"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_Fonction"] != null)
                LinesDataGridView.Columns["ContactLiv_Fonction"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_Nom"] != null)
                LinesDataGridView.Columns["ContactLiv_Nom"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_Prenom"] != null)
                LinesDataGridView.Columns["ContactLiv_Prenom"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_Tel"] != null)
                LinesDataGridView.Columns["ContactLiv_Tel"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_Fax"] != null)
                LinesDataGridView.Columns["ContactLiv_Fax"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_Portable"] != null)
                LinesDataGridView.Columns["ContactLiv_Portable"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_EMail"] != null)
                LinesDataGridView.Columns["ContactLiv_EMail"].Visible = false;
            if (LinesDataGridView.Columns["ContactLiv_Url"] != null)
                LinesDataGridView.Columns["ContactLiv_Url"].Visible = false;
            if (LinesDataGridView.Columns["IDPaiement"] != null)
                LinesDataGridView.Columns["IDPaiement"].Visible = false;
            if (LinesDataGridView.Columns["ModeTransport"] != null)
                LinesDataGridView.Columns["ModeTransport"].Visible = false;
            if (LinesDataGridView.Columns["NbArticles"] != null)
                LinesDataGridView.Columns["NbArticles"].Visible = false;
            if (LinesDataGridView.Columns["SPieceCodeModeRegtP"] != null)
                LinesDataGridView.Columns["SPieceCodeModeRegtP"].Visible = false;
            if (LinesDataGridView.Columns["SPiece_CodeTable"] != null)
                LinesDataGridView.Columns["SPiece_CodeTable"].Visible = false;
            if (LinesDataGridView.Columns["TiersEscompte"] != null)
                LinesDataGridView.Columns["TiersEscompte"].Visible = false;


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


                var fileName = string.Format("BonLivraison{0:yyyyMMdd}." + customer.sCliCode + "." + customer.codebarcf + ".{0:hhmmss}.csv", DateTime.Now);

                using (StreamWriter writer = new StreamWriter(textBox1.Text + @"\" + fileName, false, Encoding.UTF8))
                {
                    //writer.WriteLine("DEMAT-AAA;v01.0;;;" + DateTime.Today.Year + addZero(DateTime.Today.Month.ToString()) + addZero(DateTime.Today.Day.ToString()) + ";;");
                    //writer.WriteLine("");
                    //writer.WriteLine("");

                    for (int i = 0; i < BonLivrasonAExporter.Count; i++)
                    {
                        //string EANClient = GetEANClient(BonLivrasonAExporter[i].CustomerId);

                        //string[] tab = new string[] { "", "", "" };



                        //if (BonLivrasonAExporter[i].OriginDocumentType == "8")
                        //{ // Return la commande d'origin                            
                        //    tab = GetCommandeFacture(BonLivrasonAExporter[i].Id).Split(';');
                        //}




                        writer.WriteLine("DESHDR;v01.0;;" + BonLivrasonAExporter[i].NumeroNumero + ";" + customer.codebarcf + ";9;;9;" + customer.codebarcf + ";9;" + customer.codebarcf + ";9;;9;;9;;9;;" + ConvertDate(BonLivrasonAExporter[i].DateLivraison) + ";;;;;" + BonLivrasonAExporter[i].TiersAdresse2Ligne + ";;;;;;;;;;;;;;9;");
                        writer.WriteLine("");

                        writer.WriteLine("DESHD2;" + customer.codebarcf + ";" + customer.sCliRaisonSoc + ";;" + customer.sCliAdresse1Ligne + ";;" + customer.sCliAdresse1CodePos + ";" + customer.sCliAdresse1Ville + ";" + customer.sCliAdresse1CodePay + ";;;;;;;;;;;;;;;;;;;;;;;;;;;" + customer.codebarcf + ";" + BonLivrasonAExporter[i].TiersRaisonSoc + ";;" + BonLivrasonAExporter[i].TiersAdresse2Ligne + ";;" + BonLivrasonAExporter[i].TiersAdresse2CodePo + ";" + BonLivrasonAExporter[i].TiersAdresse2Ville + ";" + BonLivrasonAExporter[i].TiersAdresse2CodePa + ";" + BonLivrasonAExporter[i].Contact_Nom + ";" + BonLivrasonAExporter[i].Contact_Tel + ";" + BonLivrasonAExporter[i].ContactLiv_Fax + ";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
                        writer.WriteLine("");

                        //if (BonLivrasonAExporter[i].IntrastatTransportMode != "")
                        //{ // Return mode de transport                           
                        //    BonLivrasonAExporter[i].IntrastatTransportMode = GetModeTransport(BonLivrasonAExporter[i].IntrastatTransportMode);
                        //}

                        if (BonLivrasonAExporter[i].ModeTransport != "")
                        {
                            if (BonLivrasonAExporter[i].ModeTransport == "0")
                            {
                                BonLivrasonAExporter[i].ModeTransport = "Non définit";
                            }
                            else
                            {
                                BonLivrasonAExporter[i].ModeTransport = GetModeTransport(BonLivrasonAExporter[i].ModeTransport);
                            }
                            writer.WriteLine("DESTRP;;1;;;" + BonLivrasonAExporter[i].ModeTransport + ";;;;;");
                            writer.WriteLine("");
                        }

                        writer.WriteLine("DESLOG;1;;;" + BonLivrasonAExporter[i].TotalPoids.Replace(",", ".") + ";;" + BonLivrasonAExporter[i].TotalPoidsNet.Replace(",", ".") + ";;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;");
                        writer.WriteLine("");


                        BonLivrasonAExporter[i].lines = getDocumentLine(BonLivrasonAExporter[i].NumeroPrefixe, BonLivrasonAExporter[i].NumeroNumero);
                        MessageBox.Show("NOMBRE:" + BonLivrasonAExporter[i].lines.Count + " - PREFIX:" + BonLivrasonAExporter[i].NumeroPrefixe + " - NUMERO:" + BonLivrasonAExporter[i].NumeroNumero);
                        int compteur = 10;

                        for (int j = 0; j < BonLivrasonAExporter[i].lines.Count; j++)
                        {

                            writer.WriteLine("DESLIN;" + compteur + ";;" + BonLivrasonAExporter[i].lines[j].codebarre + ";;;;;;;" + BonLivrasonAExporter[i].lines[j].Libelle + ";;;" + BonLivrasonAExporter[i].lines[j].Quantite + ";;;;;;;;;;;;;" + ConvertDate(BonLivrasonAExporter[i].lines[j].DateLiv) + ";;;" + BonLivrasonAExporter[i].lines[j].PxUnitBrut.Replace(",", ".") + ";;;;;;" + BonLivrasonAExporter[i].lines[j].MontantNetHT.Replace(",", ".") + ";;" + BonLivrasonAExporter[i].lines[j].NombreColis + ";;;;;;;;;;;");
                            writer.WriteLine("");

                            compteur = compteur + 10;
                        }


                        writer.WriteLine("DESEND;" + BonLivrasonAExporter[i].lines.Count + ";;;" + BonLivrasonAExporter[i].NetAPayer.Replace(",", ".") + ";;" + BonLivrasonAExporter[i].TotalPoidsNet.Replace(",", ".") + ";" + BonLivrasonAExporter[i].TotalVolume.Replace(",", ".") + ";;;" + BonLivrasonAExporter[i].TotalColis.Replace(",", ".") + ";");
                        writer.WriteLine("");
                        writer.WriteLine("");
                    }



                }

                MessageBox.Show("Nombre bon de livraison : " + BonLivrasonAExporter.Count, "Information !!",
                                             MessageBoxButtons.OK, MessageBoxIcon.Asterisk);


                Close();



            }
            catch (Exception ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show("" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""), "Erreur!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            //importButton.Enabled = true;
            ExportFacture();
        }

        private void closeButton_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private string GetModeTransport(string code)
        {
            try
            {
                using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
                {

                    connection.Open();
                    //Exécution de la requête permettant de récupérer les articles du dossier
                    OdbcCommand command = new OdbcCommand(QueryHelper.getModeTransport(code), connection);
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader[0].ToString();
                            }
                            return null;
                        }
                    }


                }

            }

            catch (Exception e)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show("" + e.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", ""), "Erreur!!",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }
    }
}
