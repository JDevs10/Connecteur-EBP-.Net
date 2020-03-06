using System;
using System.Collections.Generic;
using System.Text;

namespace ConnecteurEBP.Classes
{
    class Client
    {
        #region Constructeurs
    /// <summary>
    /// Création d'une instance de Customer
    /// </summary>
        public Client()
    {
    }

        public Client(string id, string id2, string name)
    {
        ID_BASE_X = id;
        Id_EBP = id2;
        Name = name;
    }

    #endregion

        #region Propriétés
        /// <summary>
        /// Retourne et modifie l'identifiant du client
        /// </summary>
        public string ID_BASE_X { get; set; }
        /// <summary>
        /// Retourne et modifie le nom du client
        /// </summary>
        public string Id_EBP { get; set; }
        /// <summary>
        /// Retourne et modifie l'adresse du client
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
