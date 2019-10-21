using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ConnecteurSage.Classes;
using System.Globalization;
using System.Data.Odbc;
using ConnecteurSage.Helpers;

namespace ConnecteurSage.Forms
{
    public partial class importManuel : Form
    {
        private static string filename = "";
        private static List<string> MessageErreur;
        private int tva = 0;
        private static int pal = 0;
        private static int carton = 0;

        public importManuel()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exportCustomersFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "CSV|*.csv";
                //dialog.AddExtension = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Path.GetExtension(dialog.FileName).ToLower() == ".csv")
                    {
                        exportCustomersFilenameTextBox.Text = dialog.FileName;
                        filename = dialog.FileName;
                    }
                    else
                    {
                        exportCustomersFilenameTextBox.Text = string.Empty;
                        MessageBox.Show("Le format de ce fichier doit être de type CSV.");
                    }
                }
            }
        }

        public static string ConvertDate(string date)
        {
            if(date.Length == 8) {
            return date.Substring(0, 4) + "-" + date.Substring(4, 2) + "-" + date.Substring(6, 2);
            }
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(exportCustomersFilenameTextBox.Text))
            {
                MessageBox.Show("Le chemin du fichier d'import de commande doit être renseigné");
                return;
            }

            try
            {
                Boolean prixDef = false;
                Order order = new Order();
                order.Id = MaxNumPiece() ;

                if (order.Id == "erreur")
                {
                    return;
                }

                if (order.Id == null)
                {
                    MessageBox.Show("Erreur [10] : numéro de piece non valide", "Erreur !!",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                order.Lines = new List<OrderLine>();
                long pos = 1;
                string[] lines = System.IO.File.ReadAllLines(filename);

                if (lines[0].Split(';')[0] == "ORDERS" && lines[0].Split(';').Length == 11)
                {
                    order.NumCommande = lines[0].Split(';')[1];
                    if (order.NumCommande.Length > 10)
                    {
                        MessageBox.Show("Numéro de commande doit être < 10", "Erreur de lecture !!",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (order.NumCommande == "")
                    {
                        MessageBox.Show("Le champ numéro de commande est vide.", "Erreur !!",
                                                                           MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    string existe = existeCommande(order.NumCommande);

                    if (existe != null && existe != "erreur")
                    {
                        MessageBox.Show("La commande N° " + order.NumCommande + " existe deja dans la base.\nN° de pièce : CC "+existe+"", "Erreur !!",
                                                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (existe == "erreur")
                    {
                      return;
                    }


                    order.codeClient = lines[0].Split(';')[2];


                    Client client = getClient(order.codeClient);

                    if (client == null)
                    {
                        return;
                    }
                    if (client != null)
                    {
                        if (int.Parse(client.sCliAdresse2CodePay) == 1)
                        {
                            tva = 1;
                        }else 
                        {
                            tva = 2;
                        }
                    
                    }

                    
                    order.adresseLivraison = lines[0].Split(';')[7];
                    string[] tab_adress = order.adresseLivraison.Split('.');
                    if (tab_adress.Length != 5)
                    {
                        MessageBox.Show("La forme de l'adresse de livraison est incorrecte, Veuillez respecter la forme suivante :\nNom.Adresse.CodePostal.Ville.Pays", "Erreur !!",
                                                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    order.nom_contact = tab_adress[0].Replace("'", "''");
                    order.adresse = tab_adress[1].Replace("'", "''");
                    order.codepostale = tab_adress[2];
                    order.ville = tab_adress[3].Replace("'", "''");
                    order.pays = tab_adress[4].Replace("'", "''");

                    if (order.pays != "")
                    {
                        order.pays = SelectPays(order.pays);
                    }
                    if (order.pays == null)
                    {
                            order.pays = "1";
                    }
  
                    // Ajouter ville dans la réference
                    //string[] part = order.adresseLivraison.Split('.');
                    //if (part.Length >= 2)
                    //{
                    //  order.Reference = part[part.Length - 2];
                    //}

                    //order.Reference = order.ville;

                    order.deviseCommande = lines[0].Split(';')[8];
                    
                    //if (order.deviseCommande != "")
                    //{
                    //order.deviseCommande = getDevise(order.deviseCommande);
                    //}

                    //if (order.deviseCommande == "erreur")
                    //{
                    //    return;
                    //}


                    if (lines[1].Split(';')[0] == "ORDHD1" && lines[1].Split(';').Length == 5)
                    {
                        
                        if (lines[1].Split(';')[1].Length == 8)
                        {
                            order.DateCommande = ConvertDate(lines[1].Split(';')[1]);

                            if (lines[2].Split(';')[0] == "ORDLIN" && lines[2].Split(';').Length == 23)
                            {
                                //decimal total = 0m;
                                foreach (string ligneDuFichier in lines)
                                {

                                    string[] tab = ligneDuFichier.Split(';');
                                    //if (tab[0] == "ORDEND" && tab.Length == 3)
                                    //{
                                    //    order.MontantTotal = tab[1];
                                    //}

                                    switch (tab[0])
                                    {
                                        case "ORDEND":
                                            if (tab.Length == 3)
                                            {
                                                order.MontantTotal = tab[1];
                                            }
                                            else
                                            {
                                                MessageBox.Show("Erreur dans la ligne " + pos + " du fichier.", "Erreur de lecture !!",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                return;
                                            }
                                            break;
                                        case "ORDLIN":
                                            if (tab.Length == 23)
                                            {
                                                OrderLine line = new OrderLine();
                                                line.NumLigne = tab[1];
                                                line.article = getArticle(tab[2]);
                                                
                                                if (line.article == null)
                                                {
                                                    return;
                                                }


                                                //if (line.article.AR_Nomencl == "2" || line.article.AR_Nomencl == "3")
                                                //{
                                                //    line.article.AR_REFCompose = line.article.AR_REF;
                                                //}

                                                //if (line.article.gamme1 != "0")
                                                //{
                                                //    line.article.gamme1 = testGamme(0, line.article.AR_REF, line.article.gamme1);
                                                //}

                                                //if(line.article.gamme2 != "0")
                                                //{
                                                //line.article.gamme2 = testGamme(1,line.article.AR_REF, line.article.gamme2);
                                                //}

                                                line.Quantite = tab[9].Replace(",", ".");
                                                line.TypeQuantite = tab[10];
                                                decimal d = Decimal.Parse(line.Quantite, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
                                                if (d == 0)
                                                {

                                                    line.Quantite = "1";

                                                }
                                                line.PrixNetHT = tab[14].Replace(",",".");
                                                line.MontantLigne = tab[11];
                                                line.DateLivraison = ConvertDate(tab[21]);
                                                //if (line.DateLivraison.Length==6)
                                                //{
                                                //    line.DateLivraison = "Null";
                                                //}

                                                //// Calcule de palette/carton
                                                //if (line.article.typebarcode == "1")
                                                //{
                                                //    carton = carton + calcule_palette_carton(d, 1, (line.TypeQuantite == "MTK" ? 0 : 1),line.article.nbrRouleaux);
                                                //}
                                                //else
                                                //{
                                                //    pal = pal + calcule_palette_carton(d, 0, (line.TypeQuantite == "MTK" ? 0 : 1), line.article.nbrRouleaux);
                                                //}

                                                line.codeAcheteur = tab[4];
                                                line.codeFournis = tab[5];
                                                line.descriptionArticle = tab[8];
                                                if (line.descriptionArticle == "")
                                                {
                                                    line.descriptionArticle = line.article.description;
                                                }
                                                //total = total + Decimal.Parse(tab[11], NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

                                                decimal prix = Decimal.Parse(line.PrixNetHT, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
                                                decimal prixEBp = Decimal.Parse(line.article.prixVente.Replace(",","."), NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);

                                                if (prix != prixEBp)
                                                {
                                                    DialogResult resultDialog = MessageBox.Show("Prix de l'article " + line.article.code + "(" + tab[2] + ") dans la base est : " + prixEBp + "\nIl est différent du prix envoyer par le client : " + prix + ".",
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
                                                        prixDef = true;
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

                                //order.MontantTotal = total;

                               

                                order.DateLivraison = "";

                                for (int i = 0; i < order.Lines.Count; i++)
                                {
                                    if (order.Lines[i].DateLivraison.Length == 10)
                                    {
                                        order.DateLivraison = order.Lines[i].DateLivraison;
                                        goto jamp;
                                    }
                                }

                            jamp:

                    

                                if (order.codeClient != "")
                                {                                    

                                    //order.Reference = order.Reference + "/" + order.NumCommande;
                                    order.Reference =  order.NumCommande;


                                    //if(prixDef)
                                    //{
                                    //    string pr = "/AP";
                                    //   // order.Reference = order.Reference + pr;
                                    //}

                                    if(order.Lines.Count == 0)
                                    {
                                        MessageBox.Show("Aucun ligne de commande enregistré.", "Erreur de lecture !!",
                                                     MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        return;
                                    }
                                    MessageErreur = new List<string>();

                                    client.sCli_Adresse1_NPAI = (client.sCli_Adresse1_NPAI == "False" ? "0" : "1");
                                    client.sCli_Adresse2_NPAI = (client.sCli_Adresse2_NPAI == "False" ? "0" : "1");
                                    client.bRemiseTTC = (client.bRemiseTTC == "False" ? "0" : "1");
                                    client.b30jEquivalentMois = (client.b30jEquivalentMois == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq1 = (client.sCondReglEchb30jEq1 == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq2 = (client.sCondReglEchb30jEq2 == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq3 = (client.sCondReglEchb30jEq3 == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq4 = (client.sCondReglEchb30jEq4 == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq5 = (client.sCondReglEchb30jEq5 == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq6 = (client.sCondReglEchb30jEq6 == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq7 = (client.sCondReglEchb30jEq7 == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq8 = (client.sCondReglEchb30jEq8 == "False" ? "0" : "1");
                                    client.sCondReglEchb30jEq9 = (client.sCondReglEchb30jEq9 == "False" ? "0" : "1");

                                  //if (client.sCondRegl_Net == "\0")
                                  //  {

                                  //      char charValue = (char)value;
                                  //      client.sCondRegl_Net = stringValue;
                                  //      value = Convert.ToInt32(charValue);
                                  //      // Convert the decimal value to a hexadecimal value in string form. 
                                  //      string hexOutput = String.Format("{0:X}", value);
                                  //  }

                                  //  int value = Convert.ToInt32("00", 16);
                                  //// Get the character corresponding to the integral value. 
                                  //string stringValue = "'"+Char.ConvertFromUtf32(value)+"'";

                                  string stringValue = "CHAR(0)";

                                  

                                    client.sCondRegl_Net = (client.sCondRegl_Net == "\0" ? stringValue : "'" + client.sCondRegl_Net + "'");
  

                                    client.sCondRegl_JourLe = (client.sCondRegl_JourLe == "\0" ? stringValue : "'" + client.sCondRegl_JourLe + "'");

                                    client.sCondRegl_JourLe1 = (client.sCondRegl_JourLe1 == "\0" ? stringValue : "'" + client.sCondRegl_JourLe1 + "'");
                                    client.sCondRegl_JourLe2 = (client.sCondRegl_JourLe2 == "\0" ? stringValue : "'"+client.sCondRegl_JourLe2+"'");
                                    client.sCondRegl_JourLe3 = (client.sCondRegl_JourLe3 == "\0" ? stringValue : "'"+client.sCondRegl_JourLe3+"'");
                                    client.sCondRegl_JourLe4 = (client.sCondRegl_JourLe4 == "\0" ? stringValue : "'"+client.sCondRegl_JourLe4+"'");
                                    client.sCondRegl_JourLe5 = (client.sCondRegl_JourLe5 == "\0" ? stringValue : "'"+client.sCondRegl_JourLe5+"'");
                                    client.sCondRegl_JourLe6 = (client.sCondRegl_JourLe6 == "\0" ? stringValue : "'"+client.sCondRegl_JourLe6+"'");
                                    client.sCondRegl_JourLe7 = (client.sCondRegl_JourLe7 == "\0" ? stringValue : "'"+client.sCondRegl_JourLe7+"'");
                                    client.sCondRegl_JourLe8 = (client.sCondRegl_JourLe8 == "\0" ? stringValue : "'"+client.sCondRegl_JourLe8+"'");
                                    client.sCondRegl_JourLe9 = (client.sCondRegl_JourLe9 == "\0" ? stringValue : "'"+client.sCondRegl_JourLe9+"'");

                                    client.sCondReglEchPource0 = (client.sCondReglEchPource0 == "\0" ? stringValue : "'" + client.sCondReglEchPource0 + "'");
                                    client.sCondReglEchPource1 = (client.sCondReglEchPource1 == "\0" ? stringValue : "'" + client.sCondReglEchPource1 + "'");
                                    client.sCondReglEchPource2 = (client.sCondReglEchPource2 == "\0" ? stringValue : "'" + client.sCondReglEchPource2 + "'");
                                    client.sCondReglEchPource3 = (client.sCondReglEchPource3 == "\0" ? stringValue : "'" + client.sCondReglEchPource3 + "'");
                                    client.sCondReglEchPource4 = (client.sCondReglEchPource4 == "\0" ? stringValue : "'" + client.sCondReglEchPource4 + "'");
                                    client.sCondReglEchPource5 = (client.sCondReglEchPource5 == "\0" ? stringValue : "'" + client.sCondReglEchPource5 + "'");
                                    client.sCondReglEchPource6 = (client.sCondReglEchPource6 == "\0" ? stringValue : "'" + client.sCondReglEchPource6 + "'");
                                    client.sCondReglEchPource7 = (client.sCondReglEchPource7 == "\0" ? stringValue : "'" + client.sCondReglEchPource7 + "'");
                                    client.sCondReglEchPource8 = (client.sCondReglEchPource8 == "\0" ? stringValue : "'" + client.sCondReglEchPource8 + "'");
                                    client.sCondReglEchPource9 = (client.sCondReglEchPource9 == "\0" ? stringValue : "'" + client.sCondReglEchPource9 + "'");

                                    client.sCondReglEchTypeEc1 = (client.sCondReglEchTypeEc1 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc1 + "'");
                                    client.sCondReglEchTypeEc2 = (client.sCondReglEchTypeEc2 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc2 + "'");
                                    client.sCondReglEchTypeEc3 = (client.sCondReglEchTypeEc3 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc3 + "'");
                                    client.sCondReglEchTypeEc4 = (client.sCondReglEchTypeEc4 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc4 + "'");
                                    client.sCondReglEchTypeEc5 = (client.sCondReglEchTypeEc5 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc5 + "'");
                                    client.sCondReglEchTypeEc6 = (client.sCondReglEchTypeEc6 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc6 + "'");
                                    client.sCondReglEchTypeEc7 = (client.sCondReglEchTypeEc7 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc7 + "'");
                                    client.sCondReglEchTypeEc8 = (client.sCondReglEchTypeEc8 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc8 + "'");
                                    client.sCondReglEchTypeEc9 = (client.sCondReglEchTypeEc9 == "\0" ? stringValue : "'" + client.sCondReglEchTypeEc9 + "'");


                                    if (insertCommande(client, order))
                                    {

                                    //if (insertCommandeVide(client, order))
                                    //{
                                    //    if (1 == 1)
                                    //    {
                                            int nbr = 0;


                                            for (int i = 0; i < order.Lines.Count; i++)
                                            {
                                                //if (order.Lines[i].article.AR_SuiviStock == "0")
                                                //{
                                                //    order.Lines[i].article.AR_StockId = "0";
                                                //}
                                                //else
                                                //{
                                                //    order.Lines[i].article.AR_StockId = order.StockId;
                                                //}

                                                // calcule de palette 
                                                //int b = palette(float.Parse(order.Lines[i].Quantite, CultureInfo.InvariantCulture.NumberFormat), System.Convert.ToInt64(order.Lines[i].article.Colisage));
                                                //pal = pal + b;




                                                if (insertCommandeLine(client, order, order.Lines[i], tva))
                                                {
                                                    nbr++;

                                                }
                                                //if (i == (order.Lines.Count - 1))
                                                //{
                                                //    Boolean isCaton = false;

                                                //    if (order.Lines[(order.Lines.Count - 1)].article.typebarcode == "1")
                                                //    {
                                                //        isCaton = true;
                                                //    }

                                                //    if (insertPalette(order, pal, int.Parse(order.Lines[(order.Lines.Count - 1)].NumLigne), isCaton))
                                                //    {
                                                //        pal = 0;
                                                //    }
                                                //}
                                            }

                                            // Inserer nombre de carton et palette 

                                            //string info_palette_carton = "";

                                            //if (pal != 0 && carton != 0)
                                            //{
                                            //    info_palette_carton = "Nombre de palette = " + pal + "; Nombre de carton = " + carton;
                                            //}
                                            //if (pal != 0 && carton == 0)
                                            //{
                                            //    info_palette_carton = "Nombre de palette = " + pal;
                                            //}
                                            //if (pal == 0 && carton != 0)
                                            //{
                                            //    info_palette_carton = "Nombre de carton = " + carton;
                                            //}

                                            //if (nbr != 0)
                                            //{
                                            //    if (insertPalette(order, int.Parse(order.Lines[(order.Lines.Count - 1)].NumLigne), info_palette_carton))
                                            //    {
                                            //        pal = 0;
                                            //        carton = 0;
                                            //    }
                                            //}

                                            string mot = "";
                                            for (int i = 0; i < MessageErreur.Count; i++)
                                            {
                                                mot = mot + MessageErreur[i] + "\n";
                                            }

                                            if (nbr == 0)
                                            {
                                                deleteCommande(order.Id);
                                            }
                                            Close();

                                            //if (nbr != 0)
                                            //{
                                            //    // Creer dossier sortie "LOG Directory" --------------------------
                                            //    var dirName = string.Format("LogEBP15(manuelle) {0:dd-MM-yyyy HH.mm.ss}", DateTime.Now);
                                            //    string outputFile = System.IO.Path.GetDirectoryName(filename) + @"\" + dirName;
                                            //    System.IO.Directory.CreateDirectory(outputFile);
                                            //    //deplacer les fichiers csv
                                            //    System.IO.File.Move(filename, outputFile + @"\" + System.IO.Path.GetFileName(filename));
                                            //}

                                            MessageBox.Show("" + nbr + "/" + order.Lines.Count + " ligne(s) enregistrée(s).\n" + mot, "Information d'insertion",
                                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        //}
                                        //else
                                        //{
                                        //    MessageBox.Show("Erreur 2");
                                        //}
                                    }
                                    //else
                                    //{
                                    //    MessageBox.Show("Erreur 1");
                                    //}
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


                
            }
            catch (Exception ex) {
                MessageBox.Show(" ERREUR[0]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR",""), "Erreur!!",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static Client getClient(string id)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.getClient(id), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Client cli = new Client(reader[0].ToString(),reader[1].ToString(),reader[2].ToString(),reader[3].ToString(),reader[4].ToString(),reader[5].ToString(),reader[6].ToString(),reader[7].ToString(),reader[8].ToString(),reader[9].ToString(),reader[10].ToString(),
                                    reader[11].ToString(),reader[12].ToString(),reader[13].ToString(),reader[14].ToString(),reader[15].ToString(),reader[16].ToString(),reader[17].ToString(),reader[18].ToString(),reader[19].ToString(),reader[20].ToString(),
                                    reader[21].ToString(),reader[22].ToString(),reader[23].ToString(),reader[24].ToString(),reader[25].ToString(),reader[26].ToString(),reader[27].ToString(),reader[28].ToString(),reader[29].ToString(),reader[30].ToString(),
                                    reader[31].ToString(), reader[32].ToString(), reader[33].ToString(), reader[34].ToString(), reader[35].ToString(), reader[36].ToString(), reader[37].ToString(), reader[38].ToString(), reader[39].ToString(), reader[40].ToString(), reader[41].ToString(),
                                    reader[42].ToString(), reader[43].ToString(), reader[44].ToString(), reader[45].ToString(), reader[46].ToString(), reader[47].ToString(), reader[48].ToString(), reader[49].ToString(), reader[50].ToString(), reader[51].ToString(), reader[52].ToString(),
                                    reader[53].ToString(), reader[54].ToString(), reader[55].ToString(), reader[56].ToString(), reader[57].ToString(), reader[58].ToString(), reader[59].ToString(), reader[60].ToString(), reader[61].ToString(), reader[62].ToString(), reader[63].ToString(),
                                    reader[64].ToString(), reader[65].ToString(), reader[66].ToString(), reader[67].ToString(), reader[68].ToString(), reader[69].ToString(), reader[70].ToString(), reader[71].ToString(), reader[72].ToString(), reader[73].ToString(), reader[74].ToString() ,
                                    reader[75].ToString(), reader[76].ToString(), reader[77].ToString(), reader[78].ToString(), reader[79].ToString(), reader[80].ToString(), reader[81].ToString(), reader[82].ToString(), reader[83].ToString(), reader[84].ToString(), reader[85].ToString(),
                                    reader[86].ToString(), reader[87].ToString(), reader[88].ToString(), reader[89].ToString(), reader[90].ToString()
                                    );
                                connection.Close();
                                return cli;
                            }
                            else
                            {
                                MessageBox.Show("Code Client " + id + " n'existe pas dans la base EBP v15.", "Erreur !!",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return null;
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[1]" + ex.Message.Replace("[CBase]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", ""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }

        }

        public static string getStockId()
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.getStockId(), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //MessageBox.Show(reader[0].ToString());
                                string id=reader[0].ToString();
                                connection.Close();
                                return id;

                            }
                            else
                            {
                                MessageBox.Show("Il n'y a pas de stock enregistré.", "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return null;
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[2]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR",""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }

        }

        public static string getNumLivraison(string client_num)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.getNumLivraison(client_num), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //MessageBox.Show(reader[0].ToString());
                                string num = reader[0].ToString();
                                connection.Close();
                                return num;

                            }
                            else
                            {
                                MessageBox.Show("Numero de livraison n'existe pas pour le tier " + client_num + "", "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return null;
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[3]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR",""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }

        }

        public static Boolean UpdateCommandeVide(Client client, Order order)
        {

            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();

                    OdbcCommand command = new OdbcCommand(QueryHelper.UpdateCommande(client,order), connection);
                    MessageBox.Show(command.CommandText);
                    command.ExecuteReader();

                    connection.Close();
                    return true;


                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[4UpdateVide]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR", ""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

        }

        public static Boolean insertCommandeVide(Client client, Order order)
        {

            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();

                    OdbcCommand command = new OdbcCommand(QueryHelper.insertCommandeVide(order), connection);
                    MessageBox.Show(command.CommandText);
                    command.ExecuteReader();

                    connection.Close();
                    return true;


                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[4Vide]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR", ""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

        }

        public static Boolean insertCommande(Client client,Order order)
        {
            
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();

                    OdbcCommand command = new OdbcCommand(QueryHelper.insertCommande(client, order), connection);
                    //MessageBox.Show(command.CommandText);
                    command.ExecuteReader();

                        connection.Close();
                        return true;
                    

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[4]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR",""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }

        }

        public static Boolean insertCommandeLine(Client client, Order order, OrderLine orderLine, int typeTVA)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {   
                try
                {
                    connection.Open();
                    OdbcCommand command = new OdbcCommand(QueryHelper.insertLigneCommande(client, order, orderLine, typeTVA, 0), connection);
                    Console.Read();
                    command.ExecuteReader();

                    connection.Close();
                    return true;


                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //MessageBox.Show("Echec d'insertion de la ligne '"+orderLine.NumLigne+"' de la commande "+order.NumCommande+"." + "\n"+ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", ""), "Erreur!!",
                      //                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    MessageErreur.Add("Echec d'insertion de la ligne " + orderLine.NumLigne + " de la commande " + order.NumCommande + "." + "\n" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR",""));
                    return false;
                }
            }

        }

        public static Boolean deleteCommande(string NumCommande)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    OdbcCommand command = new OdbcCommand(QueryHelper.deleteCommande(NumCommande), connection);
                    command.ExecuteReader();

                    connection.Close();
                    return true;


                }
                catch (Exception ex)
                {
                  return false;
                }
            }

        }

        public static Article getArticle(string code_article)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.getArticle(code_article), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Article article = new Article(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                                //MessageBox.Show(reader[0].ToString());
                                //MessageBox.Show(article.AR_REF+" gamme1:"+ article.gamme1+" gamme2:"+article.gamme2 );
                                connection.Close();
                                return article;

                            }
                            else
                            {
                                MessageBox.Show("code article "+code_article+" n'existe pas dans la base.", "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return null;
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[5]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR",""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return null;
                }
            }

        }

        public static string testGamme(int type,string code_article,string gamme)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.getGAMME(type,code_article), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            List<string> list=new List<string>();
                            while (reader.Read())
                            {
                                list.Add(reader[0].ToString());
                            }

                            Boolean ok = false;

                            for (int i=0; i < list.Count; i++)
                            {
                                if (gamme == list[i])
                                    ok = true;
                            }


                            if (!ok && list.Count > 0)
                            {
                                return list[0];
                            }

                            return gamme;
                             
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[6]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR",""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return gamme;
                }
            }

        }

        public static string getDevise(string codeIso)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.getDevise(codeIso), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //MessageBox.Show(reader[0].ToString());
                                string num = reader[0].ToString();
                                connection.Close();
                                return num;

                            }
                            else
                            {
                                return null;
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[7]" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR",""), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "erreur";
                }
            }

        }


        public static string existeCommande(string num)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.get_NumPiece_SPiece_codeTable(num), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //MessageBox.Show(reader[0].ToString());
                                string numero = reader[0].ToString();
                                connection.Close();
                                return numero;

                            }
                            else
                            {
                                return null;
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[7]" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "erreur";
                }
            }

        }

        public static string SelectPays(string code)
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.SelectPays(code), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //MessageBox.Show(reader[0].ToString());
                                string numero = reader[0].ToString();
                                connection.Close();
                                return numero;

                            }
                            else
                            {
                                return null;
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[12]" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "erreur";
                }
            }

        }

        public static string MaxNumPiece()
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.MaxNumPiece(), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //MessageBox.Show(reader[0].ToString());
                                string num = reader[0].ToString();
                                connection.Close();
                                if (IsNumeric(num))
                                {
                                    return "" + (int.Parse(num) + 1);
                                }
                                else
                                {
                                    return "1";
                                }

                            }
                            else
                            {
                                return "1";
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[8]" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "erreur";
                }
            }

        }

        public static string NextNumPiece()
        {
            try
            {
                string NumCommande = MaxNumPiece();

                if (NumCommande=="erreur")
                {
                    return "erreur";
                }

                NumCommande = NumCommande.Replace("BC", "");

                if (IsNumeric(NumCommande))
                {
                    int Nombre = int.Parse(NumCommande) + 1;
                    string num = Nombre.ToString();

                    while (num.Length < 5)
                    {
                        num = "0" + num;
                    }

                    NumCommande = "BC" + num;

                    return NumCommande;

                }

                return null;
            }
            catch (Exception ex)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show(" ERREUR[9]" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return "erreur";
            }

            

        }



        public static Boolean IsNumeric(string Nombre)
        {
            try
            {
                int.Parse(Nombre);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string get_next_num_piece_commande()
        {
            // Insertion dans la base sage : cbase
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    using (OdbcCommand command = new OdbcCommand(QueryHelper.get_Next_NumPiece_BonCommande(), connection))
                    {
                        using (IDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //MessageBox.Show(reader[0].ToString());
                                string num = reader[0].ToString();
                                connection.Close();
                                return num;

                            }
                            else
                            {
                                return NextNumPiece();
                            }
                        }

                    }

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[10]" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return "erreur";
                }
            }

        }
        //calculer la palette = (quantité /colis)/270
        public static int palette(float a, long b)
        {
            int val ;
            int resultat;

                try
                {
                    if ((a / b) % 270 == 0)
                    {
                        val = (int)(a / b / 270);
                        resultat = val;
                    }
                    else
                    {
                        val = (int)(a / b / 270);
                        resultat = val + 1;
                    }

                    return resultat;

                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    MessageBox.Show(" ERREUR[10]" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return 0;
                }
            

        }

        /*
         * calculer la palette ou carton :
             si type MTK = quantité/270
              si type KO = quantité/18 (pour palette) 
                        ou quantité/16 (pour carton)
         * 
        */
        public static int calcule_palette_carton(decimal quantite, int Palette_carton,int MTK_KO,string nbrRouleaux)
        {
            int val;
            int resultat;
            int divise;

            try
            {
                // type = MTK = 0
                if (MTK_KO == 0)
                {
                    //divise = 270;
                    divise = int.Parse(nbrRouleaux);
                }
                // type = KO = 1
                else
                {
                    divise = (Palette_carton == 0 ? 18 : 16);
                }
                   
                if (quantite % divise == 0)
                    {
                        val = (int)(quantite / divise);
                        resultat = val;
                    }
                    
                else
                    {
                        val = (int)(quantite / divise);
                        resultat = val + 1;
                    }
                

                return resultat;

            }
            catch (Exception ex)
            {
                //Exceptions pouvant survenir durant l'exécution de la requête SQL
                MessageBox.Show(" ERREUR[10]" + ex.Message.Replace("ERROR", "Erreur").Replace("[Pervasive]", " ").Replace("[ODBC Client Interface]", "").Replace("[LNA]", " ").Replace("[Pervasive]", "").Replace("[ODBC Engine Interface]", " "), "Erreur!!",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }


        }


        public static Boolean insertPalette(Order order, int count, string info)
        {
           
            using (OdbcConnection connection = Connexion.CreateOdbcConnextion())
            {
                try
                {
                    connection.Open();
                    OdbcCommand command = new OdbcCommand(QueryHelper.insertLignePalette(order, (count+10),info), connection);
                    Console.Read();
                    command.ExecuteReader();

                    connection.Close();
                    return true;


                }
                catch (Exception ex)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //MessageBox.Show("Echec d'insertion de la ligne '"+orderLine.NumLigne+"' de la commande "+order.NumCommande+"." + "\n"+ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", ""), "Erreur!!",
                    //                          MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    MessageErreur.Add("Echec d'insertion de la ligne de la commande " + order.NumCommande + "." + "\n" + ex.Message.Replace("[CBase]", "").Replace("[Simba]", " ").Replace("[Simba ODBC Driver]", "").Replace("[Microsoft]", " ").Replace("[Gestionnaire de pilotes ODBC]", "").Replace("[SimbaEngine ODBC Driver]", " ").Replace("[DRM File Library]", "").Replace("ERROR", ""));
                    return false;
                }
            }
          
           

        }
         

    }
}
