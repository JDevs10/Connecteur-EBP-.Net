using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurSage.Classes
{
    public class ClientMini
    {
        public string Cli_Code { get; set; }
        public string Cli_GLN { get; set; }

        public ClientMini()
        {

        }

        public ClientMini(string Cli_Code, string Cli_GLN)
        {
            this.Cli_Code = Cli_Code;
            this.Cli_GLN = Cli_GLN;
        }
    }
}
