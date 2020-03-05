using ConnecteurEBP.Forms;
using ConnecteurEBP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using ConnecteurEBP.Classes;
using System.Xml.Serialization;

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

                    int isCompatible = thekey.IndexOf(value0);
                    //MessageBox.Show(""+isCompatible+" - "+thekey+" - "+value0+" - "+value1);
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
                    string keyWord = GenererCle.Generer();
                    //MessageBox.Show("Votre licence n'est pas valide", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    MessageBox.Show("Key : " + keyWord, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    var myfile = File.Create("test_key.txt");
                    XmlSerializer xml = new XmlSerializer(typeof(string));
                    xml.Serialize(myfile, "Key : " + keyWord);
                    myfile.Close();

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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Validation());

            }



            //RegistryKey Nkey = Registry.CurrentUser;
            //try
            //{
            //    RegistryKey valKey = Nkey.OpenSubKey("Software\\TrackBack\\Values", true);
            //    if (valKey == null)
            //    {
            //        Nkey.CreateSubKey("Software\\TrackBack\\Values");
            //    }
            //    valKey.SetValue("Key", "{465456451518748744445}");
            //    valKey.SetValue("Value", "4556");
            //}
            //catch (Exception er)
            //{
            //    MessageBox.Show(er.Message, "MyApp", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //}
            //finally
            //{
            //    Nkey.Close();
            //} 


        }
    }
}
