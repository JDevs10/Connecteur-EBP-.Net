using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32.TaskScheduler;
using System.IO;
using Microsoft.Win32;
using ImportPlanifier.Utilities;

namespace ImportPlanifier.Classes
{
    class Tack
    {
        //private void CreateSubKey()
        //{

        //    RegistryKey ProgName = Registry.CurrentUser.CreateSubKey(Application.ProductName);

        //    ProgName.SetValue("Path", Application.StartupPath);

        //    ProgName.SetValue("ExePath", Application.ExecutablePath);

        //    using (RegistryKey PCopyright = ProgName.CreateSubKey("Copyright"),

        //    Settings = ProgName.CreateSubKey("Initial Settings"),

        //    Security = ProgName.CreateSubKey("Sécurité"))
        //    {

        //        Settings.SetValue("Language", "Français");

        //        Settings.SetValue("Type", "Journal Personnel");

        //        Settings.SetValue("Version", Application.ProductVersion);

        //        Settings.SetValue("ID", "7cf0bd6a-d683-40a7-b61e-6c3d45b81fb7");

        //        PCopyright.SetValue("Propriétaire", "Daniel Morais");

        //        PCopyright.SetValue("Droit d'utilisation", Environment.UserName);

        //        //Placer ici le mot de passe Crypter 

        //        Security.SetValue("Password", "PWD");

        //    }

        //} 
        
        //static void Main(string[] args)
        //{
        //    const string taskName = "importCommande";

        //    TaskService ts = new TaskService();
        //    TaskDefinition td = ts.NewTask();
        //    if (ts.FindTask(taskName,true) == null)
        //    {
        //        DailyTrigger dt = (DailyTrigger)td.Triggers.Add(new DailyTrigger(1));
        //        dt.StartBoundary = DateTime.Parse("30 / 11 / 2012") + TimeSpan.FromHours(00) + TimeSpan.FromMinutes(13) + TimeSpan.FromSeconds(30);
        //        td.Actions.Add(new ExecAction("notepad.exe", null, null));
        //        ts.RootFolder.RegisterTaskDefinition(taskName, td);
        //    }
        //    Task t = ts.GetTask(taskName);
        //    td = t.Definition;
        //    t.Enabled = false;
        //    Console.WriteLine(t.Enabled);
        //    Console.WriteLine(td.Triggers[0].StartBoundary);
        //    Console.WriteLine(td.Actions[0]);

        //    RegistryKey rKey1 = Registry.CurrentUser;
        //    RegistryKey rKey2 = rKey1.OpenSubKey(@"DM009\\Sécurité");
        //    string strKeyValue = rKey2.GetValue("Password").ToString();
        //    //t1.Text = strKeyValue.ToString();
        //    Console.WriteLine(strKeyValue.ToString());


        //    Console.ReadLine();
         
        //}
    }
}