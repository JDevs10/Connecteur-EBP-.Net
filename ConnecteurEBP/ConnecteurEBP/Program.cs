using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ConnecteurEBP.Classes;
using ConnecteurEBP.Forms;
using ConnecteurEBP.Utilities;

namespace ConnecteurEBP
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

                if (File.Exists(pathModule + @"\key.key"))
                {
                    ValidationKey key = new ValidationKey();
                    key.Load("key.key");

                    string thekey = key.Key;
                    string value0 = key.Value0;
                    string value1 = key.Value1;


                    // tester si la date est valide
                    string parseKey = (thekey.Replace(value0, "")).Replace("-","");
                    string crypterDate = parseKey.Substring(6, 10);
                    string decrypterDate = "";

                    List<int> ListIndexDate2 = new List<int>();

                    for (int i = 0; i < crypterDate.Length; i++)
                    {

                        for (int j = 0; j < GenererCle.date2.Length; j++)
                        {
                            if (crypterDate.ToString().Substring(i, 1) == GenererCle.date2[j].ToString())
                                ListIndexDate2.Add(j);
                            if (crypterDate.ToString().Substring(i, 1) == GenererCle.date3[j].ToString())
                                ListIndexDate2.Add(j);
                            if (crypterDate.ToString().Substring(i, 1) == GenererCle.date4[j].ToString())
                                ListIndexDate2.Add(j);
                            if (crypterDate.ToString().Substring(i, 1) == GenererCle.date5[j].ToString())
                                ListIndexDate2.Add(j);
                            if (crypterDate.ToString().Substring(i, 1) == GenererCle.date6[j].ToString())
                                ListIndexDate2.Add(j);
                        }


                    }

                    for (int i = 0; i < ListIndexDate2.Count; i++)
                    {
                        decrypterDate = decrypterDate + GenererCle.date1[ListIndexDate2[i]];

                    }

                    DateTime dateTime;
                    bool isValideDate = false;
                    MessageBox.Show("DateTime s : " + decrypterDate +  "\nToday: " + DateTime.Today);
                    if (DateTime.TryParse(decrypterDate, out dateTime))
                    {
                        if (dateTime >= DateTime.Today)
                        {
                            isValideDate = true;
                        }
                        else
                        {
                            isValideDate = false;
                        }
                    }
                    else
                    {
                        isValideDate = false;
                    }

                    int isCompatible = thekey.IndexOf(value0);
                    //MessageBox.Show(""+isCompatible+" - "+thekey+" - "+value0+" - "+value1);
                    if (isCompatible != -1 && value0 != "" && thekey != "" && value1 == "252564541856412515418924525155124651" && isValideDate)
                    {
                        //MessageBox.Show("ici");
                        System.Windows.Forms.Application.EnableVisualStyles();
                        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                        System.Windows.Forms.Application.Run(new New());
                    }
                    else
                    {
                        MessageBox.Show("Votre licence n'est pas valide", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        System.Windows.Forms.Application.EnableVisualStyles();
                        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                        System.Windows.Forms.Application.Run(new Validation());
                    }
                }
                else
                {
                    string keyWord = GenererCle.Generer();
                    MessageBox.Show("Votre licence n'est pas valide", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    //MessageBox.Show("Key : " + keyWord, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    try
                    {
                        StreamWriter file = new StreamWriter(pathModule + @"\test_key.txt");
                        XmlSerializer reader = new XmlSerializer(typeof(string));
                        reader.Serialize(file, "Key : " + keyWord);
                        file.Close();
                    }
                    catch(Exception e)
                    {
                        MessageBox.Show("Message Erreur : "+e.Message, "Error Key File View", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    System.Windows.Forms.Application.EnableVisualStyles();
                    System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new Validation());
                    using (Forms.Validation form = new Forms.Validation())
                    {
                        form.ShowDialog();
                    }

                    if (Validation.isValide)
                    {
                        System.Windows.Forms.Application.Run(new New());
                    }


                }

                /*
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\TrackBack1\\Values");
                if (key != null)
                {
                    string thekey = Utils.Decrypt(key.GetValue("Key").ToString());
                    string value0 = Utils.Decrypt(key.GetValue("Value0").ToString());
                    string value1 = key.GetValue("Value1").ToString();

                    int isCompatible = thekey.IndexOf(value0);
                    //MessageBox.Show(""+isCompatible+" - "+thekey+" - "+value0);
                    if (isCompatible != -1 && value0 != "" && thekey != "" && value1 == "252564541856412515418924525155124651")
                    {
                        //MessageBox.Show("ici");
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Main());
                    }
                    else
                    {
                        //MessageBox.Show("Votre licence n'est pas valide", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new Validation());
                    }
                }
                else
                {
                    //MessageBox.Show("Votre licence n'est pas valide", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    //Application.Run(new Validation());
                    using (Forms.Validation form = new Forms.Validation())
                    {
                        form.ShowDialog();
                    }

                    if (Validation.isValide)
                    {
                        Application.Run(new Main());
                    }


                }
                */
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                //MessageBox.Show("Votre licence n'est pas valide", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Windows.Forms.Application.Run(new Validation());

                Console.WriteLine("Error Message: "+ e.Message);
                Console.ReadLine();

            }

            //string sourceFile = @"C:\Documents and Settings\OTHMAN\Bureau\commande vente.csv";
            //   string destinationFile = @"C:\Documents and Settings\OTHMAN\Bureau\test.txt";

            //   // To move a file or folder to a new location:
            //   System.IO.File.Move(sourceFile, destinationFile);

            //     // To move an entire directory. To programmatically modify or combine
            //     // path strings, use the System.IO.Path class.
            //     //System.IO.Directory.Move(@"C:\Users\Public\public\test\", @"C:\Users\Public\private"); 
        }
    }
}
