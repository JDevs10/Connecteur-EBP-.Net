using ImportPlanifier.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ImportPlanifier.Classes
{
    public class ValidationKey
    {
        [XmlElement]
        public string Version { get; set; }
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public string Key { get; set; }
        [XmlElement]
        public string Value0 { get; set; }
        [XmlElement]
        public string Value1 { get; set; }

        private static string pathModule = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

        public ValidationKey()
        {
        }

        public ValidationKey(string Version, string Name, string Key, string Value0, string Value1)
        {
            this.Version = Version;
            this.Name = Name;
            this.Key = Key;
            this.Value0 = Value0;
            this.Value1 = Value1;
        }

        public void saveInfo(ValidationKey key, string fileName)
        {
            try
            {
                var myfile = File.Create(pathModule + @"\" + fileName);
                XmlSerializer xml = new XmlSerializer(typeof(ValidationKey));
                key.Key = Utils.Encrypt(key.Key);
                key.Value0 = Utils.Encrypt(key.Value0);
                key.Value1 = Utils.Encrypt(key.Value1);
                xml.Serialize(myfile, key);
                myfile.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("********** Erreur SaveInfo(ValidationKey)  **********");
                Console.WriteLine("" + ex.Message);
                Console.WriteLine("" + ex.StackTrace);
            }
        }
        public void Load(string fileName)
        {
            if (File.Exists(fileName))
            {
                XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(ValidationKey));
                StreamReader file = new System.IO.StreamReader(pathModule + @"\" + fileName);
                ValidationKey key = new ValidationKey();
                key = (ValidationKey)reader.Deserialize(file);

                this.Version = key.Version;
                this.Name = key.Name;
                this.Key = Utils.Decrypt(key.Key);
                this.Value0 = Utils.Decrypt(key.Value0);
                this.Value1 = Utils.Decrypt(key.Value1);
                file.Close();
            }
        }
    }
}
