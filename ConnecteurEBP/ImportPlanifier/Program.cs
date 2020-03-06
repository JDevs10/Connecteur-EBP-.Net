using ImportPlanifier.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportPlanifier
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            try
            {
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

                        try
                        {
                            Process progress = Process.Start(pathModule + @"\ConnecteurEBP.exe");
                            progress.WaitForExit();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Pas de licence");

                    try
                    {
                        Process progress = Process.Start(pathModule + @"\ConnecteurEBP.exe");
                        progress.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            catch
            {
                Console.WriteLine("Votre licence n'est pas valide");

                try
                {
                    Process progress = Process.Start(pathModule + @"\ConnecteurEBP.exe");
                    progress.WaitForExit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
