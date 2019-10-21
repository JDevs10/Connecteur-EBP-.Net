using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1.Helpers;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Win32.TaskScheduler;
using System.Xml;

namespace ConsoleApplication1.Classes
{
    class Action
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
                    MessageBox.Show("Erreur de l'import planifier\nAction Annuler!!", "Erreur de lecture !!",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Console.WriteLine(DateTime.Now+" : Scan du dossier ...");

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
                    Console.WriteLine(DateTime.Now + " : un fichier \".cvs\" trouvé :");
                    Console.WriteLine(DateTime.Now + " : -----> " + nbr + "- " + filename);
                    Console.WriteLine(DateTime.Now + " : Scan fichier...");
                    LogFile.WriteLine(DateTime.Now + " : un fichier \".cvs\" trouvé :");
                    LogFile.WriteLine(DateTime.Now + " : -----> " + nbr + "- " + filename);
                    LogFile.WriteLine(DateTime.Now + " : Scan fichier...");

                
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

                Boolean ORDER_HDR = false;
                Boolean ORDER_LIN = false;

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

                string[] lines = System.IO.File.ReadAllLines(fileListing+@"\"+filename);

                foreach (string myString in lines)
                {
                    if (myString != "" && myString.Length > 8)
                    {
                        string motCle = myString.Substring(0, 8);
                        switch (motCle)
                        {
                            case "ORDERHDR":
                                if (myString.Length == 567)
                                {
                                    NumCommande = myString.Substring(19, 35);
                                    if (!TestNumeroCommande(NumCommande))
                                    {
                                        Console.WriteLine(DateTime.Now + " : Erreur : Numero de commande déja enregistrer : " + NumCommande);
                                        LogFile.WriteLine(DateTime.Now + " : Erreur : Numero de commande déja enregistrer : " + NumCommande);

                                        goto goOut;
                                    }
                                    for (int i = 0; i < tabCommande.Length; i++)
                                    {
                                        if (NumCommande == tabCommande[i])
                                        {
                                            Console.WriteLine(DateTime.Now + " : Erreur : Le fichier contient un numero de commande déja scanner : " + NumCommande);
                                            LogFile.WriteLine(DateTime.Now + " : Erreur : Le fichier contient un numero de commande déja scanner : " + NumCommande);

                                            goto goOut;
                                        }
                                    }
                                    order.NumCommande = NumCommande;
                                        //order.Id= myString.Substring(19, 35);
                                        order.ThirdId = GetClientId(myString.Substring(57, 20));

                                    if (order.ThirdId != null)
                                    {
                                        if (!TestAdresseFacturationClient(order.ThirdId))
                                        {
                                            //MessageBox.Show("Veuillez completer les informations du client " + order.ThirdId + " dans la base EBP.!\nAdresse de facturation : \"Code Postale\", \"ville\", \"pays\" doient etre renseigné", "Erreur",
                                            //MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            Console.WriteLine(DateTime.Now + " : Erreur : Veuillez completer les informations du client " + order.ThirdId + " dans la base EBP.!\nAdresse de facturation : \"Code Postale\", \"ville\", \"pays\" doient etre renseigné !!");
                                            LogFile.WriteLine(DateTime.Now + " : Erreur : Veuillez completer les informations du client " + order.ThirdId + " dans la base EBP.!\nAdresse de facturation : \"Code Postale\", \"ville\", \"pays\" doient etre renseigné !!");

                                            goto goOut;
                                        }
                                        //Return nom du client
                                        order.ThirdName = GetNomClient(order.ThirdId);
                                    }
                                    string date = myString.Substring(224, 2) + "/" + myString.Substring(222, 2) + "/" + myString.Substring(218, 4);
                                    order.Date = Convert.ToDateTime(date);
                                    ORDER_HDR = true;
                                }
                                else
                                {
                                    //MessageBox.Show("la longeur de ligne ORDERHDR est différent de 567 !!", "Erreur de lecture !!",
                                      //      MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    Console.WriteLine(DateTime.Now + " : Erreur de lecture : la longeur de ligne ORDERHDR est différent de 567 !!");
                                    LogFile.WriteLine(DateTime.Now + " : Erreur de lecture : la longeur de ligne ORDERHDR est différent de 567 !!");
                                    //return;
                                    goto goOut;
                                }
                                break;

                            case "ORDERLIN":
                                if (myString.Length == 1016)
                                {
                                    OrderLine line = new OrderLine();
                                    line.ItemId = GetArticleId(myString.Substring(19, 13));
                                    if (line.ItemId == null)
                                    {
                                        goto goOut;
                                    }
                                    line.Quantity = Int32.Parse(myString.Substring(229, 13));
                                    order.Lines.Add(line);
                                    decimal prix = Convert.ToDecimal(myString.Substring(265, 15)) / 1000;
                                    decimal prixEbp = Convert.ToDecimal(VerifierPrixVente(line.ItemId));

                                    if (prix != prixEbp)
                                    {
                                        Console.WriteLine(DateTime.Now + " : Prix de l'article " + line.ItemId + "(" + myString.Substring(19, 13) + ") dans la base est : " + prixEbp + ". Il est différent du prix envoyer par le client : " + prix + ".");
                                        LogFile.WriteLine(DateTime.Now + " : Prix de l'article " + line.ItemId + "(" + myString.Substring(19, 13) + ") dans la base est : " + prixEbp + ". Il est différent du prix envoyer par le client : " + prix + ".");

                                        order.Different = true;
                                    }
                                    ORDER_LIN = true;
                                    //Console.WriteLine("Code EAN Article : " + myString.Substring(19, 35));
                                    //Console.WriteLine("Quantité commandée : " + myString.Substring(229, 15));
                                    //Console.WriteLine("Prix unitaire net HT : " + myString.Substring(265, 15));
                                    //Console.WriteLine("Prix total ligne HT : " + myString.Substring(331, 15));
                                }
                                else
                                {
                                    //MessageBox.Show("la longeur de ligne ORDERLIN est différent de 1016 !!", "Erreur de lecture !!",
                                            //MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    Console.WriteLine(DateTime.Now + " : Erreur de lecture : la longeur de ligne ORDERLIN est différent de 1016 !!");
                                    LogFile.WriteLine(DateTime.Now + " : Erreur de lecture : la longeur de ligne ORDERLIN est différent de 1016 !!");

                                    goto goOut;
                                }
                                break;

                            case "ORDEREND":

                                //Console.WriteLine("Montant Total HT pour la commande : " + myString.Substring(69, 15));

                                break;

                            default:
                                break;

                        }

                    }
                }

                if (!ORDER_HDR)
                {
                    Console.WriteLine(DateTime.Now + " : Erreur de lecture : Le Fichier ne contient pas ORDERHDR");
                    LogFile.WriteLine(DateTime.Now + " : Erreur de lecture : Le Fichier ne contient pas ORDERHDR");
                    goto goOut;
                }

                if (!ORDER_LIN)
                {
                    Console.WriteLine(DateTime.Now + " : Erreur de lecture : Le Fichier ne contient pas ORDERLIN");
                    LogFile.WriteLine(DateTime.Now + " : Erreur de lecture : Le Fichier ne contient pas ORDERLIN");
                    goto goOut;
                }


                if (order.ThirdId != null)
                {

                    ordersList.Add(order);


                    if (ORDER_HDR && ORDER_LIN)
                    {
                        Console.WriteLine(DateTime.Now + " : Convertir le fichier");
                        LogFile.WriteLine(DateTime.Now + " : Convertir le fichier");
                    }

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
                    LogFile.WriteLine("Nombre des commandes echouées : " + (nbr-commandeValider));
                    LogFile.Close();
                    var newlog = string.Format("logFile(0) {0:dd-MM-yyyy hh.mm.ss}.log", DateTime.Now);
                    System.IO.File.Move(outputFile + @"\logFile.log", dir +@"\"+ newlog);
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

                MessageBox.Show("Nombre de commandes : " + nbr + "\nNombre de commandes validées : " + commandeValider + "\n" + "Nombre de commandes echouées : " + (nbr - commandeValider), "information !!",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    
            }
            catch (UnauthorizedAccessException ex)
            {
                //Exception pouvant survenir si lorsque l'accès au disque dur est refusé
                MessageBox.Show(ex.Message);
                // Creer fichier de sortie "LOG File" ------------------------
               
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
            catch (Exception ex)
            {
                StreamWriter LogFile = new StreamWriter(outputFile + @"\logFile.log");
                LogFile.WriteLine(""+ex);
                LogFile.Close();
                //Exceptions issues de la méthode LaunchProcess
                MessageBox.Show(ex.Message);
            }

            Console.WriteLine("");
            Console.WriteLine("Cliquez Entrer pour sortir ..");
            
            Console.Read();
            

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
                        MessageBox.Show(e.Message);
                        return null;
                    }
                }

            }
            catch (Exception e) 
            {
                MessageBox.Show(""+e);
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
                MessageBox.Show("" + e);
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
               int.Parse(Nombre);
               return true;
           }
           catch
           {
               return false;
           }
       }
    }
}