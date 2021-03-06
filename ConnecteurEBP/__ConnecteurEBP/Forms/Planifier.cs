﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System.IO;
using System.Xml.Serialization;

namespace ConnecteurEBP.Forms
{
    public partial class Planifier : Form
    {
        private string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private const string taskName = "importCommandeEBPv15";

        public Planifier()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            Classes.Path path = ReturnPath();

            TaskService ts = new TaskService();
            if (ts.FindTask(taskName, true) != null && path != null)
            {
                checkBox1.Checked = true;
                Task t = ts.GetTask(taskName);
                TaskDefinition td = t.Definition;
                label6.Text = "Tâche Planifiée :";
                string info = td.Triggers[0].ToString();
                if (info.Length > 50)
                {
                    label7.Text = "" + info.Insert(56, "\n");
                }
                else
                {
                    label7.Text = "" + info;
                }
                dateTimePicker2.Text = "" + td.Triggers[0].StartBoundary.ToString().Substring(0, 10);
                dateTimePicker1.Text = "" + td.Triggers[0].StartBoundary.ToString().Substring(11, 8);
                textBox1.Text = path.path;
                checkBox6.Checked = path.ImportCommande;
                checkBox3.Checked = path.ExportCommande;
                checkBox4.Checked = path.ExportBonLivraison;
                checkBox5.Checked = path.ExportFacture;

                // Microsoft.Win32.RegistryKey key= Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\EBP\\SDKImport");
                //chargement du fichier
                //td.Triggers[0].Repetition.
                int interval = int.Parse(td.Triggers[0].Repetition.Interval.ToString().Substring(0, 2));

                if (interval != 0)
                {

                    comboBox2.Text = "" + interval;
                    groupBox3.Enabled = true;
                    checkBox2.Checked = true;

                }
                else
                {
                    groupBox3.Enabled = false;
                    checkBox2.Checked = false;
                }



            }

            if ((ts.FindTask(taskName, true) == null && path == null) || (ts.FindTask(taskName, true) != null && path == null) || (ts.FindTask(taskName, true) == null && path != null))
            {

                groupBox1.Enabled = false;

                groupBox2.Enabled = false;

                enregistrerButton.Enabled = false;

                checkBox1.Checked = false;

                groupBox3.Enabled = false;
                checkBox2.Checked = false;
                checkBox2.Enabled = false;
                groupBox4.Enabled = false;

                label6.Text = "Tâche Planifiée :";
                label7.Text = "cochez la case ci-dessous";

            }

            if (ts.FindTask(taskName, true) != null && path == null)
            {
                //Task t = ts.GetTask(taskName);
                //TaskDefinition td = t.Definition;
                //t.Enabled = false;
                ts.RootFolder.DeleteTask(taskName);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                groupBox1.Enabled = false;

                groupBox2.Enabled = false;

                groupBox3.Enabled = false;

                groupBox4.Enabled = false;

                checkBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;

                groupBox2.Enabled = true;

                groupBox4.Enabled = true;

                enregistrerButton.Enabled = true;

                checkBox2.Enabled = true;

                if (!checkBox2.Checked)
                {
                    groupBox3.Enabled = false;

                }
                else
                {
                    groupBox3.Enabled = true;

                }

            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                //Tester s'il y a une dossier spécifié
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Veuillez indiquer l'emplacement du dossier de l'impot");
                    return;
                }

                //Tester si une ou plusieurs taches sont cochés
                if (!checkBox3.Checked && !checkBox4.Checked && !checkBox5.Checked && !checkBox6.Checked)
                {
                    MessageBox.Show("Il faut cocher une seule tache au minimum !!");
                    return;
                }

                //Tester si le dossier existe
                if (!Directory.Exists(textBox1.Text))
                {
                    MessageBox.Show("Le chemin n'est pas valide !!");
                    return;
                }

                try
                {
                    //Enregistrer la tache planifiée
                    EnregistrerLaTache(dateTimePicker2.Text, dateTimePicker1.Text);

                    //Enregistrer l'emplacement dans Path.xml
                    Classes.Path path = new Classes.Path(textBox1.Text, checkBox6.Checked, checkBox3.Checked, checkBox4.Checked, checkBox5.Checked);


                    var myfile = File.Create(pathModule + @"\Path.xml");
                    XmlSerializer xml = new XmlSerializer(typeof(Classes.Path));
                    xml.Serialize(myfile, path);
                    myfile.Close();




                    // ***********************************************************************
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }
            }
            else
            {
                TaskService ts = new TaskService();
                if (ts.FindTask(taskName, true) != null)
                {
                    //ts.RootFolder.
                    ts.RootFolder.DeleteTask(taskName);
                }

            }
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Planifier_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

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

        public void EnregistrerLaTache(string date, string time)
        {
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            DailyTrigger dt = (DailyTrigger)td.Triggers.Add(new DailyTrigger(1));
            dt.StartBoundary = DateTime.Parse(date) + TimeSpan.FromHours(Convert.ToDouble(time.Substring(0, 2))) + TimeSpan.FromMinutes(Convert.ToDouble(time.Substring(3, 2)));
            if (checkBox2.Checked)
            {
                dt.Repetition.Duration = TimeSpan.FromDays(1);
                dt.Repetition.Interval = TimeSpan.FromHours(int.Parse(comboBox2.Text));
            }
            td.Actions.Add(new ExecAction(pathModule + @"\importPlanifier.exe", null, null));
            ts.RootFolder.RegisterTaskDefinition(taskName, td);
        }

        public Classes.Path ReturnPath()
        {

            try
            {
                Classes.Path path = new Classes.Path();
                path.Load();
                return path;

            }
            catch
            {
                //Exception pouvant survenir si l'objet path n'est pas chargé
                MessageBox.Show("Erreur[P1] : path file.");
                return null;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox2.Checked)
            {
                groupBox3.Enabled = false;

            }
            else
            {
                groupBox3.Enabled = true;

            }


        }


    }
}
