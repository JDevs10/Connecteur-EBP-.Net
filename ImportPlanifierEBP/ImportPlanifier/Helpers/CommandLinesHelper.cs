using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleApplication1.Classes;

namespace ConsoleApplication1.Helpers
{

    /// <summary>
    /// Classe statique permettant d'utiliser les lignes de commandes
    /// </summary>
    public static class CommandLinesHelper
    {
        #region Constantes
        public const string Gui = "/Gui=true";
        #endregion

        #region Propriétés
        /// <summary>
        /// Retourne la ligne de commande concernant l'ouverture du dossier
        /// </summary>
        private static string DatabaseCommandLine
        {
            get
            {
                string appLogin = Settings.Instance.ApplicationLogin;
                string appPassword = Settings.Instance.ApplicationPassword;
                if (string.IsNullOrEmpty(appLogin))
                    appLogin = "ADM";
                return string.Format("/Database=\"{0}\";{1};{2}", Settings.Instance.ShortcutPath, appLogin, appPassword);
            }
        }

        /// <summary>
        /// Ligne de commande pour importer des commandes
        /// </summary>
        public static string ImportOrder
        {
            get
            {
                return string.Format("{0} {1} {2}", DatabaseCommandLine, Gui, "/Import=\"{0}\";SaleOrders;Commandes2");
            }
        }

        /// <summary>
        /// Ligne de commande pour importer des prospects
        /// </summary>
        public static string ImportProspects
        {
            get
            {
                return string.Format("{0} {1} {2}", DatabaseCommandLine, Gui, "/Import=\"{0}\";Customers;Prospects");
            }
        }

        /// <summary>
        /// Ligne de commande pour exporter des articles
        /// </summary>
        public static string ExportItems
        {
            get
            {
                return string.Format("{0} {1} {2}", DatabaseCommandLine, Gui, "/Export=\"{0}\";Items;Articles_Biens");
            }
        }

        /// <summary>
        /// Ligne de commande pour exporter des clients
        /// </summary>
        public static string ExportCustomers
        {
            get
            {
                return string.Format("{0} {1} {2}", DatabaseCommandLine, Gui, "/Export=\"{0}\";Customers;Clients");
            }
        }

        /// <summary>
        /// Ligne de commande pour exporter des Factures
        /// </summary>
        public static string ExportFactures
        {
            get
            {
                return string.Format("{0} {1} {2}", DatabaseCommandLine, Gui, "/Export=\"{0}\";SaleInvoices;Factures_Vente");
            }
        }
        /// <summary>
        /// Ligne de commande pour importer des Factures
        /// </summary>
        public static string ImportFactures
        {
            get
            {
                return string.Format("{0} {1} {2}", DatabaseCommandLine, Gui, "/Import=\"{0}\";SaleInvoices;Factures_Vente");
            }
        }

        /// <summary>
        /// Ligne de commande pour ouvrir une fiche
        /// </summary>
        public static string OpenForm
        {
            get
            {
                return string.Format("{0} {1} {2}", DatabaseCommandLine, Gui, "/OpenForm=Customer;{0}");
            }
        }

        /// <summary>
        /// Ligne de commande pour imprimer une fiche
        /// </summary>
        public static string PrintForm
        {
            get
            {
                return string.Format("{0} {1} {2}", DatabaseCommandLine, Gui, "/Print=Item;{0};{1}");
            }
        }
        #endregion
    }
}
