using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConnecteurEBP.Forms
{
    public partial class Loading : Form
    {
        public Loading()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
    }
}
