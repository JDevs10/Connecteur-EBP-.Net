using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using ConsoleApplication1.Helpers;

namespace ConsoleApplication1
{
    class Test
    {
        static void Main(string[] args)
        {
            //Microsoft.Win32.RegistryKey key;
            //key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\EBP\\SDKImport");
            //key.SetValue("Version", "0.1");
            //key.SetValue("Name", "SDK import de commande");
            //key.SetValue("LocalImportPlanifier", @"C:\");
            //key.Close();

            //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\EBP\\SDKImport");
           // key.GetValue.GetSubKeyNames("Version",);
            //Console.WriteLine(key.GetValue("LocalImportPlanifier"));
            //Console.Read();
            //MessageBox.Show("" + key.GetValue("LocalImportPlanifier"));

            //ConsoleApplication1.Classes.ActionPlanifier a = new ConsoleApplication1.Classes.ActionPlanifier();
            //a.importPlanifier();


            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\TrackBack0\\Values");
                if (key != null)
                {
                    string thekey = Utils.Decrypt(key.GetValue("Key").ToString());
                    string value0 = Utils.Decrypt(key.GetValue("Value0").ToString());
                    string value1 = key.GetValue("Value1").ToString();

                    int isCompatible = thekey.IndexOf(value0);
                    //MessageBox.Show(""+isCompatible+" - "+thekey+" - "+value0);
                    if (isCompatible != -1 && value0 != "" && thekey != "" && value1 == "0122682246948784852")
                    {

                        Classes.ActionPlanifier action = new Classes.ActionPlanifier();
                        action.lancerTache();

                    }
                    else
                    {
                        //MessageBox.Show("Votre licence n'est pas valide", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        //Application.EnableVisualStyles();
                        //Application.SetCompatibleTextRenderingDefault(false);
                        //Application.Run(new Validation());
                        Console.WriteLine("Votre licence n'est pas valide");
                        Console.Read();
                    }
                }

            }
            catch
            {
                Console.WriteLine("Votre licence n'est pas valide");
                Console.Read();

            }

        }
    }
}
