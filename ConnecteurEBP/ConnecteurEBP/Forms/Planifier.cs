using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32.TaskScheduler;
using System.IO;
using System.Xml;
using ConnecteurEBP.Utilities;
using System.Data.SqlClient;

namespace ConnecteurEBP.Forms
{
    public partial class Planifier : Form
    {
        private string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
        private const string taskName = "importCommande";
        private bool import_commande = false;
        private bool import_bonlivraison = false;
        private bool import_facture = false;
        private bool export_commande = false;
        private bool export_bonlivraison = false;
        private bool export_facture = false;


        public Planifier()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            string path = ReturnPath();
            GET_TACHE_PLANIFIER();

            TaskService ts = new TaskService();
            if (ts.FindTask(taskName, true) != null && path != null)
            {
                checkBox1.Checked = true;
                Task t = ts.GetTask(taskName);
                TaskDefinition td = t.Definition;
                label6.Text = "Tâche Planifiée :";
                label7.Text = "" + td.Triggers[0];
                dateTimePicker2.Text = "" + td.Triggers[0].StartBoundary.ToString().Substring(0, 10);
                dateTimePicker1.Text = "" + td.Triggers[0].StartBoundary.ToString().Substring(11, 8);
                textBox1.Text = path;
                checkBox2.Checked = import_commande;
                checkBox3.Checked = export_commande;
                checkBox4.Checked = export_bonlivraison;
                checkBox5.Checked = export_facture;
                checkBox6.Checked = import_bonlivraison;
                checkBox7.Checked = import_facture;
                // Microsoft.Win32.RegistryKey key= Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\EBP\\SDKImport");
                //chargement du fichier

            }

            if ((ts.FindTask(taskName, true) == null && path == null) || (ts.FindTask(taskName, true) != null && path == null) || (ts.FindTask(taskName, true) == null && path != null))
            {

                groupBox1.Enabled = false;

                groupBox2.Enabled = false;

                groupBox3.Enabled = false;

                enregistrerButton.Enabled = false;

                checkBox1.Checked = false;

                label6.Text = "Tâche Planifiée :";
                label7.Text = "cochez la case ci-dessous";

            }

            if (ts.FindTask(taskName, true) != null && path == null) 
            {
                Task t = ts.GetTask(taskName);
                TaskDefinition td = t.Definition;
                t.Enabled = false;
            }
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                groupBox1.Enabled = false;

                groupBox2.Enabled = false;

                groupBox3.Enabled = false;


            }
            else
            {
                groupBox1.Enabled = true;

                groupBox2.Enabled = true;

                groupBox3.Enabled = true;

                enregistrerButton.Enabled = true;

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
                if (checkBox7.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false && checkBox5.Checked == false && checkBox6.Checked == false)
                {
                    MessageBox.Show("Veuillez cochez une tâche pour activé la planification.");
                    return;
                }

                //Tester s'il y a une dossier spécifié
                if (string.IsNullOrEmpty(textBox1.Text))
                {
                    MessageBox.Show("Veuillez indiquer l'emplacement du dossier de l'impot");
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

                    //Enregistrer l'emplacement dans le registre
                    //Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\EBP\\SDKImport");
                    //key.SetValue("LocalImportPlanifier", textBox1.Text);
                    //key.Close();
                    
                    // ********************* Creer fichier XML pour enregistrer path *********
                    /*
                    //declaration d'un document XML 
                    XmlTextWriter xml = new XmlTextWriter(pathModule + @"\Path.xml", System.Text.Encoding.UTF8);
                    xml.WriteStartDocument(); //creation d'un noeud 
                    xml.WriteStartElement("Path");
                    //ajout d'attribut 
                    //xml.WriteAttributeString("Path", "1");
                    xml.WriteRaw(textBox1.Text);
                    xml.WriteEndElement();
                    //fermeture du document  xml.Flush(); //vider le buffer 
                    xml.Close(); //fermeture du document 
                    */
                    // inserer path
                    using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
                    {
                        try
                        {
                            sqlConnection.Open();
                            SqlCommand cmd = new SqlCommand(QueryHelper.deleteImportPlanifier + ";" + QueryHelper.deleteTachePlanifier + ";" + "INSERT into Path_ImportPlanifier values('" + textBox1.Text + "')" + ";" + "INSERT into TachePlanifier values(" + (checkBox2.Checked == true ? '1' : '0') + "," + (checkBox6.Checked == true ? '1' : '0') + "," + (checkBox7.Checked == true ? '1' : '0') + "," + (checkBox3.Checked == true ? '1' : '0') + " , " + (checkBox4.Checked == true ? '1' : '0') + "," + (checkBox5.Checked == true ? '1' : '0') + ")", sqlConnection);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }

                    // ***********************************************************************
                }
                catch(Exception ex) 
                {
                    MessageBox.Show(""+ex);
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

        public void EnregistrerLaTache(string date,string time)
        {
            TaskService ts = new TaskService();
            TaskDefinition td = ts.NewTask();
            DailyTrigger dt = (DailyTrigger)td.Triggers.Add(new DailyTrigger(1));
            dt.StartBoundary = DateTime.Parse(date) + TimeSpan.FromHours(Convert.ToDouble(time.Substring(0, 2))) + TimeSpan.FromMinutes(Convert.ToDouble(time.Substring(3, 2)));
            td.Actions.Add(new ExecAction(pathModule + @"\importPlanifier.exe", null, null));
            ts.RootFolder.RegisterTaskDefinition("importCommande", td);
        }

        public string ReturnPath()
        {
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

        public void GET_TACHE_PLANIFIER()
        {
            using (SqlConnection sqlConnection = Utils.CreateSqlConnection())
            {
                try
                {
                    sqlConnection.Open();
                    //Exécution de la requête permettant de récupérer le prochain identifiant de commandes à utiliser
                    //QueryHelper.returnClient(id);
                    using (SqlCommand cmd = new SqlCommand(QueryHelper.getTachePlanifier, sqlConnection))
                    {
                        using (IDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                //string t = reader[0].ToString();
                                import_commande = reader[0].ToString() == "False" ? false : true;
                                import_bonlivraison = reader[1].ToString() == "False" ? false : true;
                                import_facture = reader[2].ToString() == "False" ? false : true;
                                export_commande = reader[3].ToString() == "False" ? false : true;
                                export_bonlivraison = reader[4].ToString() == "False" ? false : true;
                                export_facture = reader[5].ToString() == "False" ? false : true;

                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    //Exception pouvant survenir si l'objet SqlConnection est dans l'état 'Fermé'
                    MessageBox.Show(e.Message);
                }
            }
        }


    }
}
