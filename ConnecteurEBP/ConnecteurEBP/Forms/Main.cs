using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ConnecteurEBP.Classes;
using ConnecteurEBP.Utilities;

namespace ConnecteurEBP.Forms
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    if (!Settings.CheckSettings())
            //        return;
            //    using (Form1 form = new Form1())
            //    {
            //        form.ShowDialog();
            //    }
            //}
            //// Récupération d'une possible SDKException
            //catch (SDKException ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Settings.CheckSettings())
                    return;
                using (ExportItemsForm form = new ExportItemsForm())
                {
                    form.ShowDialog();
                }
            }
            // Récupération d'une possible SDKException
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                //recherche de produits installés compatibles avec la DEMO SDK
                if (!Utils.IsCompatibleProductsExists())
                {
                    MessageBox.Show("Aucune application EBP compatible avec cette version du SDK n'a été trouvée", "Application compatible non trouvée", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Ouverture de la fenêtre des paramètres
                //using (SettingsForm form = new SettingsForm())
                    //if (form.ShowDialog() == DialogResult.OK)
                        //FormatMainFormAccordingCurrentApplication();
            }
            catch (SDKException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }
    }


}
