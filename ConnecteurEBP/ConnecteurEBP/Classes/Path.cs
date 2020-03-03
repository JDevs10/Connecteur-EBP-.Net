using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConnecteurEBP.Classes
{
    public class Path
    {
        [XmlElement]
        public string path;
        [XmlElement]
        public bool ImportCommande;
        [XmlElement]
        public bool ExportCommande;
        [XmlElement]
        public bool ExportFacture;
        [XmlElement]
        public bool ExportBonLivraison;

        public Path()
        {

        }

        public Path(string path)
        {
            this.path = path;
        }

        public Path(string path, bool ImportCommande, bool ExportCommande, bool ExportBonLivraison, bool ExportFacture)
        {
            this.path = path;
            this.ImportCommande = ImportCommande;
            this.ExportCommande = ExportCommande;
            this.ExportBonLivraison = ExportBonLivraison;
            this.ExportFacture = ExportFacture;
        }

        private static string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        public void Load()
        {
            if (File.Exists(pathModule + @"\Path.xml"))
            {
                XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Path));
                StreamReader file = new System.IO.StreamReader(pathModule + @"\Path.xml");
                Path setting = new Path();
                setting = (Path)reader.Deserialize(file);

                this.path = setting.path;
                this.ImportCommande = setting.ImportCommande;
                this.ExportCommande = setting.ExportCommande;
                this.ExportBonLivraison = setting.ExportBonLivraison;
                this.ExportFacture = setting.ExportFacture;

                file.Close();
            }
        }
    }
}
