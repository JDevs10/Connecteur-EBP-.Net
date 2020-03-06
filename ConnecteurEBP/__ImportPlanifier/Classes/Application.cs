using System;

namespace ImportPlanifier.Classes
{
    /// <summary>
    /// Classe représentant une application EBP
    /// </summary>
    public class Application
    {
        #region Constructeur
        /// <summary>
        /// Créé une instance de Application
        /// </summary>
        public Application()
        {
        }
        #endregion

        #region Propriétés
        /// <summary>
        /// Guid de l'application
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ProductId de l'application
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// Version de l'application
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Nom de l'application
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// Chemin de l'executable de l'application
        /// </summary>
        public string ExecutablePath { get; set; }
        #endregion

        #region Méthodes
        /// <summary>
        /// Redéfinit la méthode ToString pour renvoyer une chaine contenant le nom du produit suivi de sa version
        /// </summary>
        /// <returns>Retourne la chaine formatée</returns>
        public override string ToString()
        {
            return string.Format("{0} {1}", ProductName, Version);
        }
        /// <summary>
        /// Définit l'égalité entre le produit courant et celui passé en paramètre
        /// </summary>
        /// <param name="obj">le produit à comparer</param>
        /// <returns>Retourne le résultat de l'égalité</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Application app = obj as Application;
            if (app == null)
                return false;
            return (this.ProductId == app.ProductId && this.Version == app.Version);
        }

        /// <summary>
        /// Définit le HashCode à calculer
        /// </summary>
        /// <returns>Retourne le HashCode calculé</returns>
        public override int GetHashCode()
        {
            return this.ProductId.GetHashCode() ^ this.Version.GetHashCode();
        }
        #endregion
    }
}
