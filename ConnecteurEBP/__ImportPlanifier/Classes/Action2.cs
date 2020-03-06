using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImportPlanifier.Utilities;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32.TaskScheduler;
using System.Xml;
using System.Globalization;

namespace ImportPlanifier.Classes
{
    class Action2
    {
        private static string ordersFilename = string.Format(@"{0}\importCommande.txt", Path.GetTempPath());
        //private static string filename = "";
        private string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private string dir;
        private static int nbr = 0;
        private static int commandeValider = 0;
        private static StreamWriter LogFile;
        public void importPlanifier()
        {
            string outputFile = "";

            try
            {
                //tester s'il existe des fichies .csv
                Boolean FileExiste = false;

                int count = 0;
                string[] tabCommande = new string[200];
                List<Order> ordersList = new List<Order>();
                dir = DirectoryName();
                Console.WriteLine("##############################################");
                Console.WriteLine("############ L'import planifier ##############");
                Console.WriteLine("##############################################");
                Console.WriteLine("");

                if (dir == null)
                {
                    //MessageBox.Show("Erreur de l'import planifier\nAction Annuler!!", "Erreur de lecture !!",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(DateTime.Now + " : *********** Erreur **********");
                    Console.WriteLine(DateTime.Now + " : L'emplacement de l'import n'est pas enregistrer");
                    Console.WriteLine(DateTime.Now + " : Import annulée");
                    goto goError;
                }

                DirectoryInfo fileListing = new DirectoryInfo(dir);
                if (InfoTachePlanifier() == null)
                {
                    Console.WriteLine(DateTime.Now + " : Aucune importation planifiée trouvé");
                    Console.WriteLine(DateTime.Now + " : Import annulée");
                    goto goError;
                }
                Console.WriteLine("Dossier : " + fileListing);
                Console.WriteLine("");
                Console.WriteLine(DateTime.Now + " : Scan du dossier ...");

                // Creer dossier sortie "LOG Directory" --------------------------
                var dirName = string.Format("LogEBP(planifiée) {0:dd-MM-yyyy hh.mm.ss}", DateTime.Now);
                outputFile = dir + @"\" + dirName;
                System.IO.Directory.CreateDirectory(outputFile);
                // Creer fichier de sortie "LOG File" ------------------------
                LogFile = new StreamWriter(outputFile + @"\logFile.log");
                LogFile.WriteLine("##############################################");
                LogFile.WriteLine("############ L'import planifier ##############");
                LogFile.WriteLine("##############################################");
                LogFile.WriteLine("");
                LogFile.WriteLine(InfoTachePlanifier());
                LogFile.WriteLine("Dossier : " + fileListing);
                LogFile.WriteLine("");
                LogFile.WriteLine(DateTime.Now + " : Scan du dossier ...");


                // Recherche des fichiers .csv
                foreach (FileInfo filename in fileListing.GetFiles("*.csv"))
                {
                    nbr++;
                    FileExiste = true;
                    long pos = 1;
                    Console.WriteLine(DateTime.Now + " : un fichier \".cvs\" trouvé :");
                    Console.WriteLine(DateTime.Now + " : -----> " + nbr + "- " + filename);
                    Console.WriteLine(DateTime.Now + " : Scan fichier...");
                    LogFile.WriteLine(DateTime.Now + " : un fichier \".cvs\" trouvé :");
                    LogFile.WriteLine(DateTime.Now + " : -----> " + nbr + "- " + filename);
                    LogFile.WriteLine(DateTime.Now + " : Scan fichier...");

                    Boolean Motref = false;
                    string NumCommande = "";
                    Order order = new Order();
                    order.Different = false;
                    order.Id = "";
                    order.NumCommande = "";
                    //order.Date = //DateTime.Now;
                    //order.ThirdId = thirdIdTextBox.Text;
                    order.ThirdName = "";
                    order.Address = "";
                    order.ZipCode = "";
                    order.City = "";
                    order.Country = "";
                    //order.Lines = orderLinesDataGridView.DataSource as List<OrderLine>;
                    order.Lines = new List<OrderLine>();

                    //Boolean ORDER_HDR = false;
                    //Boolean ORDER_LIN = false;

                    //if (CreerTableBase())
                    //{
                    //    Console.WriteLine(DateTime.Now + " : Configuration de la base de donnée");
                    //    LogFile.WriteLine(DateTime.Now + " : Configuration de la base de donnée");
                    //}
                    //else
                    //{
                    //    Console.WriteLine(DateTime.Now + " : ******** Erreur : Configuration DateBase echouée ******");
                    //    LogFile.WriteLine(DateTime.Now + " : ******** Erreur : Configuration DateBase echouée ******");
                    //    goto goOut;
                    //}

                    string[] lines = System.IO.File.ReadAllLines(fileListing + @"\" + filename);

                    if (lines.Length < 4)
                    {
                        //MessageBox.Show("Le fichier est invalide", "Erreur !!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }


                    if (lines[0].Split(';')[0] == "ORDERS" && lines[0].Split(';').Length == 11)
                    {
                        NumCommande = lines[0].Split(';')[1];
                        if (NumCommande.Length > 9)
                        {
                            Console.WriteLine(DateTime.Now + " : Erreur[0] : Numéro de commande doit être < 10");
                            LogFile.WriteLine(DateTime.Now + " : Erreur[0] : Numéro de commande doit être < 10");
                            goto goOut;
                        }

                        if (NumCommande == "")
                        {
                            Console.WriteLine(DateTime.Now + " : Erreur[1] : Le champ numéro de commande est vide.");
                            LogFile.WriteLine(DateTime.Now + " : Erreur[1] : Le champ numéro de commande est vide.");
                            goto goOut;
                        }


                        if (!IsNumeric(NumCommande))
                        {
                            Console.WriteLine(DateTime.Now + " : Erreur[2] : Numéro de commande n'est pas valide.");
                            LogFile.WriteLine(DateTime.Now + " : Erreur[2] : Numéro de commande n'est pas valide.");
                            goto goOut;
                        }

                        //Test Numero de commande

                        if (!TestNumeroCommande(Convert.ToDouble(NumCommande).ToString()))
                        {
                            Console.WriteLine(DateTime.Now + " : Erreur[3] : Numero de commande déja enregistrer : " + NumCommande);
                            LogFile.WriteLine(DateTime.Now + " : Erreur[3] : Numero de commande déja enregistrer : " + NumCommande);
                            goto goOut;
                        }

                        //order.codeClient = lines[0].Split(';')[2];
                        if (lines[0].Split(';')[2] == "")
                        {
                            Console.WriteLine(DateTime.Now + " : Erreur[4] : Le champ GLN émetteur est vide.");
                            LogFile.WriteLine(DateTime.Now + " : Erreur[4] : Le champ GLN émetteur est vide.");
                            goto goOut;
                        }

                        if (!IsNumeric(lines[0].Split(';')[2]))
                        {
                            Console.WriteLine(DateTime.Now + " : Erreur[5] : Numéro GLN émetteur n'est pas valide.");
                            LogFile.WriteLine(DateTime.Now + " : Erreur[5] : Numéro GLN émetteur n'est pas valide.");
                            goto goOut;
                        }

                        order.ThirdId = GetClientId(lines[0].Split(';')[2]);

                        if (order.ThirdId != null)
                        {
                            if (!TestAdresseFacturationClient(order.ThirdId))
                            {
                                Console.WriteLine(DateTime.Now + " : Erreur[6] : Veuillez completer les informations du client " + order.ThirdId + " dans la base EBP.! Adresse de facturation : \"Code Postale\", \"ville\", \"pays\" doient etre renseigné.");
                                LogFile.WriteLine(DateTime.Now + " : Erreur[6] : Veuillez completer les informations du client " + order.ThirdId + " dans la base EBP.! Adresse de facturation : \"Code Postale\", \"ville\", \"pays\" doient etre renseigné.");
                                goto goOut;
                            }
                            //return nom du client
                            order.ThirdName = GetNomClient(order.ThirdId);
                        }

                        order.NumCommande = NumCommande;

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
                                    Console.WriteLine(DateTime.Now + " : Erreur[7] : Numéro GLN émetteur n'est pas valide.");
                                    LogFile.WriteLine(DateTime.Now + " : Erreur[7] : Numéro GLN émetteur n'est pas valide.");
                                    goto goOut;
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

                                                        Console.WriteLine(DateTime.Now + " : Prix de l'article " + line.ItemId + "(" + tab[2] + ") dans la base est : " + prixEbp + "\nIl est différent du prix envoyer par le client : " + prix + ".");
                                                        LogFile.WriteLine(DateTime.Now + " : Prix de l'article " + line.ItemId + "(" + tab[2] + ") dans la base est : " + prixEbp + "\nIl est différent du prix envoyer par le client : " + prix + ".");

                                                            Motref = true;
                                                        
                                                    }

                                                    order.Lines.Add(line);
                                                }
                                                else
                                                {
                                                    Console.WriteLine(DateTime.Now + " : Erreur[8] : Erreur dans la ligne " + pos + " du fichier.");
                                                    LogFile.WriteLine(DateTime.Now + " : Erreur[8] : Erreur dans la ligne " + pos + " du fichier.");
                                                    goto goOut;
                                                }
                                                break;

                                        }

                                        pos++;

                                    }


                                    if (order.ThirdId != "")
                                    {

                                        if (order.Lines.Count == 0)
                                        {
                                            Console.WriteLine(DateTime.Now + " : Erreur[9] : Aucun ligne de commande enregistré.");
                                            LogFile.WriteLine(DateTime.Now + " : Erreur[9] : Aucun ligne de commande enregistré.");
                                            goto goOut;
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine(DateTime.Now + " : Erreur[10] : Il faut mentionner le code client.");
                                        LogFile.WriteLine(DateTime.Now + " : Erreur[10] : Il faut mentionner le code client.");
                                        goto goOut;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine(DateTime.Now + " : Erreur[11] : Erreur dans la troisième ligne du fichier.");
                                    LogFile.WriteLine(DateTime.Now + " : Erreur[11] : Erreur dans la troisième ligne du fichier.");
                                    goto goOut;
                                }
                            }
                            else
                            {
                                Console.WriteLine(DateTime.Now + " : Erreur[12] : Date de la commande est incorrecte.");
                                LogFile.WriteLine(DateTime.Now + " : Erreur[12] : Date de la commande est incorrecte.");
                                goto goOut;
                            }
                        }
                        else
                        {
                            Console.WriteLine(DateTime.Now + " : Erreur[13] : Erreur dans la deuxième ligne du fichier.");
                            LogFile.WriteLine(DateTime.Now + " : Erreur[13] : Erreur dans la deuxième ligne du fichier.");
                            goto goOut;
                        }

                    }
                    else
                    {
                        Console.WriteLine(DateTime.Now + " : Erreur[14] : Erreur dans la première ligne du fichier.");
                        LogFile.WriteLine(DateTime.Now + " : Erreur[14] : Erreur dans la première ligne du fichier.");
                        goto goOut;
                    }

                    if (order.ThirdId != null)
                    {

                        ordersList.Add(order);


                        Console.WriteLine(DateTime.Now + " : Scan du fichier " + filename + " Terminé.");
                        LogFile.WriteLine(DateTime.Now + " : Scan du fichier " + filename + " Terminé.");

                        //deplacer les fichiers csv
                        Console.WriteLine(DateTime.Now + " : Déplacer le fichier vers :");
                        Console.WriteLine("                      " + outputFile);
                        LogFile.WriteLine(DateTime.Now + " : Déplacer le fichier vers :");
                        LogFile.WriteLine("                      " + outputFile);
                        System.IO.File.Move(dir + @"\" + filename, outputFile + @"\" + filename);

                        tabCommande[count] = NumCommande;
                        count++;


                    }

                goOut: ;
                }
                if (ordersList.Count != 0)
                {
                    using (StreamWriter writer = new StreamWriter(ordersFilename, false, Encoding.UTF8))
                    {

                        writer.WriteLine("OrderId;Date;ThirdId;ThirdName;Address;ZipCode;City;Country;ItemId;Quantity");

                        for (int i = 0; i < ordersList.Count; i++)
                        {
                            commandeValider++;
                            Order order1 = ordersList[i];

                            foreach (OrderLine line in order1.Lines)
                            {
                                if (line.ItemId != null)
                                {
                                    if (order1.Different)
                                    {
                                        writer.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                                        order1.NumCommande.Trim() + " (prix article différent)", order1.Date, order1.ThirdId, order1.ThirdName, order1.Address, order1.ZipCode,
                                        order1.City, order1.Country, line.ItemId, line.Quantity));
                                    }
                                    else
                                    {
                                        writer.WriteLine(string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                                        order1.NumCommande.Trim(), order1.Date, order1.ThirdId, order1.ThirdName, order1.Address, order1.ZipCode,
                                        order1.City, order1.Country, line.ItemId, line.Quantity));
                                    }
                                }
                            }
                        }
                    }


                    Console.WriteLine(DateTime.Now + " : Execution Commande de l'import");
                    LogFile.WriteLine(DateTime.Now + " : Execution Commande de l'import");
                    //Création des arguments de la ligne de commande
                    string importArguments = string.Format(CommandLinesHelper.ImportOrder, ordersFilename);
                    //Exécution de la ligne de commande
                    Utils.LaunchCommandLineProcess(importArguments);

                }

                if (!FileExiste)
                {
                    Console.WriteLine(DateTime.Now + " : Il y a pas de fichier .csv dans le dossier.");
                    LogFile.WriteLine(DateTime.Now + " : Il y a pas de fichier .csv dans le dossier.");
                    LogFile.WriteLine(DateTime.Now + " : Fin de l'execution");
                    LogFile.WriteLine("");
                    LogFile.WriteLine("Nombre de fichier scanner : " + nbr);
                    LogFile.WriteLine("Nombre des commandes validées : " + commandeValider);
                    LogFile.WriteLine("Nombre des commandes echouées : " + (nbr - commandeValider));
                    LogFile.Close();
                    var newlog = string.Format("logFile(0) {0:dd-MM-yyyy hh.mm.ss}.log", DateTime.Now);
                    System.IO.File.Move(outputFile + @"\logFile.log", dir + @"\" + newlog);
                    System.IO.Directory.Delete(outputFile);
                    goto goError;

                }
                if (FileExiste)
                {
                    LogFile.WriteLine(DateTime.Now + " : Fin de l'execution");
                    LogFile.WriteLine("");
                    LogFile.WriteLine("Nombre de fichier scanner : " + nbr);
                    LogFile.WriteLine("Nombre des commandes validées : " + commandeValider);
                    LogFile.WriteLine("Nombre des commandes echouées : " + (nbr - commandeValider));
                    LogFile.Close();
                }
            goError:
                Console.WriteLine(DateTime.Now + " : Fin de l'execution");
                Console.WriteLine("");
                Console.WriteLine("Nombre de fichier scanner : " + nbr);
                Console.WriteLine("Nombre des commandes validées : " + commandeValider);
                Console.WriteLine("Nombre des commandes echouées : " + (nbr - commandeValider));

                //MessageBox.Show("Nombre de commandes : " + nbr + "\nNombre de commandes validées : " + commandeValider + "\n" + "Nombre de commandes echouées : " + (nbr - commandeValider), "information !!",MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (UnauthorizedAccessException ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                //MessageBox.Show(ex.Message);
                // Creer fichier de sortie "LOG File" ------------------------

            }
            catch (IOException ex)
            {
                //Exception pouvant survenir si le chemin du fichier est trop long ou s'il est introuvable
                //MessageBox.Show(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                //Exception pouvant survenir si le format du fichier est incorrect
                //MessageBox.Show(ex.Message);
            }
            catch (SDKException ex)
            {
                //Exceptions issues de la méthode LaunchProcess
                //MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                StreamWriter LogFile = new StreamWriter(outputFile + @"\logFile.log");
                LogFile.WriteLine("" + ex);
                LogFile.Close();
                //Exceptions issues de la méthode LaunchProcess
                //MessageBox.Show(ex.Message);
            }

            //Console.WriteLine("");
            //Console.WriteLine("Cliquez Entrer pour sortir ..");

            //Console.Read();


        }
        //---------------------------------------- Fin import planifier ------------------------------------------------

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
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //MessageBox.Show(e.Message);
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
                        //MessageBox.Show("Le client " + id.Substring(0, 13) + " n'est pas enregistré dans la base.!", "Erreur !!",
                        //         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Console.WriteLine(DateTime.Now + " : Erreur : Le client " + id.Substring(0, 13) + " n'est pas enregistré dans la base.!");
                        LogFile.WriteLine(DateTime.Now + " : Erreur : Le client " + id.Substring(0, 13) + " n'est pas enregistré dans la base.!");

                        return null;
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //MessageBox.Show(e.Message);
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
                        //MessageBox.Show("L'article " + id + " n'est pas enregistré dans la base.!", "Erreur !!",
                        //MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Console.WriteLine(DateTime.Now + " : Erreur : L'article " + id + " n'est pas enregistré dans la base.!");
                        LogFile.WriteLine(DateTime.Now + " : Erreur : L'article " + id + " n'est pas enregistré dans la base.!");
                        return null;
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //MessageBox.Show(e.Message);
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

                        //MessageBox.Show("L'identifiant " + id + " n'est pas enregistré dans la base.!", "Erreur d'importation",
                        //         MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Console.WriteLine(DateTime.Now + " : L'identifiant " + id + " n'est pas enregistré dans la base.!");
                        LogFile.WriteLine(DateTime.Now + " : L'identifiant " + id + " n'est pas enregistré dans la base.!");
                        return null;
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //MessageBox.Show(e.Message);
                    return null;
                }
            }
        }

        public string DirectoryName()
        {
            try
            {
                //Microsoft.Win32.RegistryKey key;
                //key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\EBP\\SDKImport");
                //key.SetValue("Version", "0.1");
                //key.SetValue("Name", "SDK import de commande");
                //key.SetValue("LocalImportPlanifier", @"C:\");
                //key.Close();

                //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\EBP\\SDKImport");
                // key.GetValue.GetSubKeyNames("Version",);
                using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
                {
                    try
                    {
                        sqlConnection.Open();
                        //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                        //QueryHelper.returnClient(id);
                        using (SqlCommand cmd = new SqlCommand(QueryHelper.selectPath, sqlConnection))
                        {
                            using (IDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                    return reader["path"] as string;
                            }
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                        //MessageBox.Show(e.Message);
                        return null;
                    }
                }

            }
            catch (Exception e)
            {
                //MessageBox.Show("" + e);
                return null;
            }

        }

        public string InfoTachePlanifier()
        {
            try
            {
                string taskName = "importCommande";
                TaskService ts = new TaskService();
                if (ts.FindTask(taskName, true) != null)
                {
                    Task t = ts.GetTask(taskName);
                    TaskDefinition td = t.Definition;
                    Console.WriteLine("L'import des commandes Planifiée :");
                    Console.WriteLine("" + td.Triggers[0]);
                    return "" + td.Triggers[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                //MessageBox.Show("" + e);
                return null;
            }
        }//Fin

        //public Boolean CreerTableBase() 
        // {
        //     using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
        //     {
        //         try
        //         {
        //             string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        //             string fileName = "serializedEntity.xml";

        //             string sourceFile = System.IO.Path.Combine(pathModule, fileName);

        //             sqlConnection.Open();
        //             SqlCommand cmd = new SqlCommand(QueryHelper.EbpSysGenericImportSettings, sqlConnection);
        //             cmd.ExecuteNonQuery();

        //             //Chargement du fichier dans "dataXML"
        //             string dataXML = System.IO.File.ReadAllText(sourceFile);

        //             cmd = new SqlCommand(@"update EbpSysGenericImportSettings set serializedEntity='" + dataXML + "' where name='Commandes2' and serializedEntity='null'", sqlConnection);
        //             cmd.ExecuteNonQuery();
        //             return true;
        //         }

        //         catch (Exception ex)
        //         {
        //             return false;
        //         }
        //     }   
        // }

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
                    //MessageBox.Show(e.Message);
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
                    //MessageBox.Show(e.Message);
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

                        //MessageBox.Show("Aucun prix enregistrer dans la base", "Erreur !!",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return null;
                    }
                }
                catch (InvalidOperationException e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (IndexOutOfRangeException e)
                {
                    //Exception pouvant survenir si les champs de la requête ne sont plus en adéquation avec ceux de la base de données
                    //MessageBox.Show(e.Message);
                    return null;
                }
                catch (SqlException e)
                {
                    //Exceptions pouvant survenir durant l'exécution de la requête SQL
                    //MessageBox.Show(e.Message);
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