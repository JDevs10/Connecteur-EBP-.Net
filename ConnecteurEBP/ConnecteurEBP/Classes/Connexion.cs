using ConnecteurEBP.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConnecteurEBP.Classes
{
    class Connexion
    {
        private static string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        public static OdbcConnection CreateOdbcConnextion()
        {
            DbConnectionStringBuilder connectionString = new DbConnectionStringBuilder();
            connectionString.Add("Dsn", "");
            connectionString.Add("Driver", "{Pervasive ODBC Engine Interface}");


            string filename = pathModule + @"\Setting.xml";
            if (File.Exists(filename))
            {
                XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ConfigurationDNS));
                StreamReader file = new System.IO.StreamReader(filename);
                ConfigurationDNS setting = new ConfigurationDNS();
                setting = (ConfigurationDNS)reader.Deserialize(file);
                connectionString.Add("Dsn", setting.DNS);
                connectionString.Add("uid", setting.Nom);
                connectionString.Add("pwd", Utils.Decrypt(setting.Password));
                file.Close();
            }

            return new OdbcConnection(connectionString.ConnectionString);


        }

    }
}
