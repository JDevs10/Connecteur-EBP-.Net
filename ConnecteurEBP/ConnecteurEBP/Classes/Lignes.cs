using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnecteurEBP.Classes
{
    public class Lignes
    {
        public string PieceNumero { set; get; }
        public string IP { set; get; }
        public string CodeArt { set; get; }
        public string Libelle { set; get; }
        public string CDate { set; get; }
        public string Quantite { set; get; }
        public string PxUnitBrut { set; get; }
        public string PxUnitBrutTTC { set; get; }
        public string TauxRemise { set; get; }
        public string CodeTiers { set; get; }
        public string PrixAchat { set; get; }
        public string Volume { set; get; }
        public string Poids { set; get; }
        public string Colis { set; get; }
        public string MontantBrutHT { set; get; }
        public string MontantBrutTTC { set; get; }
        public string MontantNetHT { set; get; }
        public string MontantNetTTC { set; get; }
        public string VolumeTotal { set; get; }
        public string PoidsTotal { set; get; }
        public string NombreColis { set; get; }
        public string DateLiv { set; get; }
        public string PoidsNet { set; get; }
        public string PoidsTotalNet { set; get; }
        public string Devise { set; get; }
        public string codebarre { set; get; }
        public string ChampPerso { set; get; }

        public Lignes(string PieceNumero,
        string IP,
        string CodeArt,
        string Libelle,
        string CDate,
        string Quantite,
        string PxUnitBrut,
        string PxUnitBrutTTC,
        string TauxRemise,
        string CodeTiers,
        string PrixAchat,
        string Volume,
        string Poids,
        string Colis,
        string MontantBrutHT,
        string MontantBrutTTC,
        string MontantNetHT,
        string MontantNetTTC,
        string VolumeTotal,
        string PoidsTotal,
        string NombreColis,
        string DateLiv,
        string PoidsNet,
        string PoidsTotalNet,
        string Devise,
            string codebarre)
        {
            this.PieceNumero = PieceNumero;
            this.IP = IP;
            this.CodeArt = CodeArt;
            this.Libelle = Libelle;
            this.CDate = CDate;
            this.Quantite = Quantite;
            this.PxUnitBrut = PxUnitBrut;
            this.PxUnitBrutTTC = PxUnitBrutTTC;
            this.TauxRemise = TauxRemise;
            this.CodeTiers = CodeTiers;
            this.PrixAchat = PrixAchat;
            this.Volume = Volume;
            this.Poids = Poids;
            this.Colis = Colis;
            this.MontantBrutHT = MontantBrutHT;
            this.MontantBrutTTC = MontantBrutTTC;
            this.MontantNetHT = MontantNetHT;
            this.MontantNetTTC = MontantNetTTC;
            this.VolumeTotal = VolumeTotal;
            this.PoidsTotal = PoidsTotal;
            this.NombreColis = NombreColis;
            this.DateLiv = DateLiv;
            this.PoidsNet = PoidsNet;
            this.PoidsTotalNet = PoidsTotalNet;
            this.Devise = Devise;
            this.codebarre = codebarre;
        }

    }
}
