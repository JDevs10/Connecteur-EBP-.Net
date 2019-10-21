using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConnecteurSage
{
    public partial class ImportClient : Form
    {
        private static string filename = "";

        public ImportClient()
        {
            InitializeComponent();
        }

        private void exportCustomersFileBrowseButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "CSV|*.csv";
                //dialog.AddExtension = true;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (System.IO.Path.GetExtension(dialog.FileName).ToLower() == ".csv")
                    {
                        exportCustomersFilenameTextBox.Text = dialog.FileName;
                        filename = dialog.FileName;
                    }
                    else
                    {
                        exportCustomersFilenameTextBox.Text = string.Empty;
                        MessageBox.Show("Le format de ce fichier doit être de type CSV.");
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(exportCustomersFilenameTextBox.Text))
            {
                MessageBox.Show("Le chemin du fichier d'import de commande doit être renseigné");
                return;
            }

            try
            {
                string[] lines = System.IO.File.ReadAllLines(filename);
                Classes.ClientExport client = new Classes.ClientExport();
                client.ListAdresse = new List<Classes.Adresse>();
                client.ListContact = new List<Classes.Contact>();

                foreach(string line in lines)
                {
                    string[] ligne = line.Split(';');
                    switch (ligne[0])
                    {
                        case "CLI-DEBUT":
                            if(ligne.Length != 4)
                            {
                                MessageBox.Show("Il y a une erreur dans la ligne CLI-DEBUT.\r\nLa longueur de la ligne est incorrecte.", "Erreur !!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                        case "CLI-CODE":
                            if (ligne.Length != 8)
                            {
                                MessageBox.Show("Il y a une erreur dans la ligne CLI-CODE.\r\nLa longueur de la ligne est incorrecte.", "Erreur !!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            client.sCliCode = ligne[1];
                            client.CodeBarCF = ligne[2];
                            client.sCliCivilite = ligne[3];
                            client.sCliRaisonSoc = ligne[4];
                            client.sCli_siret = ligne[6];
                            break;
                        case "CLI-ADRESSE":
                            if (ligne.Length != 10)
                            {
                                MessageBox.Show("Il y a une erreur dans la ligne CLI-ADRESSE.\r\nLa longueur de la ligne est incorrecte.", "Erreur !!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            Classes.Adresse adr = new Classes.Adresse();
                            adr.bFacturation = (ligne[2] == "F" ? "1" : "0");
                            adr.bPrincipal = ligne[3];
                            adr.bFacturation = ligne[4];
                            adr.Adresse_Ligne = ligne[5];
                            adr.Adresse_CodePostal = ligne[6];
                            adr.Adresse_Ville = ligne[7];
                            adr.Adresse_CodePays = ligne[8];


                            break;
                        case "CLI-CONTACT":
                            if (ligne.Length != 16)
                            {
                                MessageBox.Show("Il y a une erreur dans la ligne CLI-CONTACT.\r\nLa longueur de la ligne est incorrecte.", "Erreur !!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                        case "CLI-VENTE":
                            if (ligne.Length != 7)
                            {
                                MessageBox.Show("Il y a une erreur dans la ligne CLI-VENTE.\r\nLa longueur de la ligne est incorrecte.", "Erreur !!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                        case "CLI-BANQUE":
                            if (ligne.Length != 7)
                            {
                                MessageBox.Show("Il y a une erreur dans la ligne CLI-BANQUE.\r\nLa longueur de la ligne est incorrecte.", "Erreur !!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                        case "CLI-REGL":
                            if (ligne.Length != 3)
                            {
                                MessageBox.Show("Il y a une erreur dans la ligne CLI-REGL.\r\nLa longueur de la ligne est incorrecte.", "Erreur !!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                        case "CLI-FIN":
                            if (ligne.Length != 2)
                            {
                                MessageBox.Show("Il y a une erreur dans la ligne CLI-FIN.\r\nLa longueur de la ligne est incorrecte.", "Erreur !!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur!!",
                                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
  
        }
    }
}
