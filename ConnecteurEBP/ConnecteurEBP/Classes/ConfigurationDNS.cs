using ConnecteurEBP.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConnecteurEBP.Classes
{
    public class ConfigurationDNS
    {
        [XmlElement]
        public string DNS { get; set; }
        [XmlElement]
        public string Nom { get; set; }
        [XmlElement]
        public string Password { get; set; }

        public ConfigurationDNS()
        {
            //Load();
        }

        private static string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        public void Load()
        {
            if (File.Exists("Setting.xml"))
            {
                XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ConfigurationDNS));
                StreamReader file = new System.IO.StreamReader(pathModule + @"\Setting.xml");
                ConfigurationDNS setting = new ConfigurationDNS();
                setting = (ConfigurationDNS)reader.Deserialize(file);

                this.DNS = setting.DNS;
                this.Nom = setting.Nom;
                this.Password = Utils.Decrypt(setting.Password);
                file.Close();
            }
        }
    }
}
