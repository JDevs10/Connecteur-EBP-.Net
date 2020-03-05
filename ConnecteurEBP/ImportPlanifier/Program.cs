using ImportPlanifier.Classes;
using ImportPlanifier.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportPlanifier
{
    class Program
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
                        Classes.ActionPlanifier action = new Classes.ActionPlanifier();
                        action.lancerTache();
                    }
                    else
                    {
                        Console.WriteLine("Votre licence n'est pas valide");
                        Console.Read();
                    }
                }
                else
                {
                    Console.WriteLine("Pas de licence");
                    Console.Read();
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
