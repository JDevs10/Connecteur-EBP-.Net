using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurEBP.Classes
{
    class DocumentVente
    {
        // Code document
        public string Id { get; set; }

        // Numéro du document
        public string DocumentNumber { get; set; }

        // Code client
        public string CustomerId { get; set; }

        // Nom du client
        public string CustomerName { get; set; }

        // Numero de prefix
        public string NumberPrefix { get; set; }

        // Numero de suffix
        public string NumberSuffix { get; set; }

        // Date document
        public string DocumentDate { get; set; }

        // Date livraison
        public string DeliveryDate { get; set; }

        // Adresse facturation
        public string Adresse1_facturation { get; set; }
        public string Adresse2_facturation { get; set; }
        public string Adresse3_facturation { get; set; }
        public string Adresse4_facturation { get; set; }
        public string Codepostal_facturation { get; set; }
        public string Ville_facturation { get; set; }
        public string codePays_facturation { get; set; }

        // Adresse livraison
        public string Adresse1_livraison { get; set; }
        public string Adresse2_livraison { get; set; }
        public string Adresse3_livraison { get; set; }
        public string Adresse4_livraison { get; set; }
        public string Codepostal_livraison { get; set; }
        public string Ville_livraison { get; set; }
        public string codePays_livraison { get; set; }

        // Solde du
        public string CommitmentsBalanceDue { get; set; }

        //Montant HT
        public string AmountVatExcluded { get; set; }

        // % remise
        public string DiscountRate { get; set; }

        // Montant de la remise
        public string DiscountAmount { get; set; }

        // Montant Net HT
        public string AmountVatExcludedWithDiscount { get; set; }

        // Montant total HT
        public string AmountVatExcludedWithDiscountAndShipping { get; set; }

        // Montant total HT hors éco-contribution
        public string AmountVatExcludedWithDiscountAndShippingWithoutEcotax { get; set; }

        // Montant TVA
        public string VatAmount { get; set; }

        // Montant TTC
        public string AmountVatIncluded { get; set; }
        
        // Montant de l'acompte
        public string DepositAmount { get; set; }

        // Montant de l'acompte en devise
        public string DepositCurrencyAmount { get; set; }

        // Net à payer
        public string TotalDueAmount { get; set; }

        // Taux de TVA(0)
        public string DetailVatAmount0_DetailVatRate { get; set; }

        // Montant HT(0)
        public string DetailVatAmount0_DetailAmountVatExcluded { get; set; }

        // Code moyen de paiement
        public string PaymentTypeId { get; set; }

        // Banque Id
        public string BankId { get; set; }

        // Type d'escompte
        public string FinancialDiscountType { get; set; }

        // % escompte
        public string FinancialDiscountRate { get; set; }

        // Montant de l'escompte
        public string FinancialDiscountAmount { get; set; }

        // Code ISO devise
        public string CurrencyId { get; set; }

        // Mode de transport
        public string IntrastatTransportMode { get; set; }

        // Type document
        public string DocumentType { get; set; }

        // Origin type document
        public string OriginDocumentType { get; set; }

        // Id document transferer
        public string TransferedDocumentId { get; set; }

        //------------------------------------

        // Nom (contact) (facturation)
        public string InvoicingContact_Name { get; set; }

        // Prénom (facturation)
        public string InvoicingContact_FirstName { get; set; }

        // Téléphone fixe (facturation)
        public string InvoicingContact_Phone { get; set; }

        // Téléphone portable (facturation)
        public string InvoicingContact_CellPhone { get; set; }

        // Fax (facturation)
        public string InvoicingContact_Fax { get; set; }

        // E-mail (facturation)
        public string InvoicingContact_Email { get; set; }

        // Fonction (facturation)
        public string InvoicingContact_Function { get; set; }

        //------------------------------------

        // Nom (contact) (livraison)
        public string DeliveryContact_Name { get; set; }

        // Prénom (livraison)
        public string DeliveryContact_FirstName { get; set; }

        // Téléphone fixe (livraison)
        public string DeliveryContact_Phone { get; set; }

        // Téléphone portable (livraison)
        public string DeliveryContact_CellPhone { get; set; }

        // Fax (livraison)
        public string DeliveryContact_Fax { get; set; }

        // E-mail (livraison)
        public string DeliveryContact_Email { get; set; }

        // Fonction (livraison)
        public string DeliveryContact_Function { get; set; }

        // --------------------------------------------------

        // Base de calcul(0)
        public string  DetailTaxAmount0_TaxCalculationBase { get; set; }

        // Base(0)
        public string DetailTaxAmount0_BaseAmount { get; set; }

        // Montant de la taxe(0)
        public string DetailTaxAmount0_TaxAmount { get; set; }

        // Libellé(0)
        public string DetailTaxAmount0_TaxCaption { get; set; }
        // --------------------------------------------------

        // Base de calcul(1)
        public string DetailTaxAmount1_TaxCalculationBase { get; set; }

        // Base(1)
        public string DetailTaxAmount1_BaseAmount { get; set; }

        // Montant de la taxe(1)
        public string DetailTaxAmount1_TaxAmount { get; set; }

        // Libellé(1)
        public string DetailTaxAmount1_TaxCaption { get; set; }

        // --------------------------------------------------

        // Base de calcul(2)
        public string DetailTaxAmount2_TaxCalculationBase { get; set; }

        // Base(2)
        public string DetailTaxAmount2_BaseAmount { get; set; }

        // Montant de la taxe(2)
        public string DetailTaxAmount2_TaxAmount { get; set; }

        // Libellé(2)
        public string DetailTaxAmount2_TaxCaption { get; set; }

        // --------------------------------------------------

        // Base de calcul(3)
        public string DetailTaxAmount3_TaxCalculationBase { get; set; }

        // Base(3)
        public string DetailTaxAmount3_BaseAmount { get; set; }

        // Montant de la taxe(3)
        public string DetailTaxAmount3_TaxAmount { get; set; }

        // Libellé(3)
        public string DetailTaxAmount3_TaxCaption { get; set; }

        // --------------------------------------------------

        // Base de calcul(4)
        public string DetailTaxAmount4_TaxCalculationBase { get; set; }

        // Base(4)
        public string DetailTaxAmount4_BaseAmount { get; set; }

        // Montant de la taxe(4)
        public string DetailTaxAmount4_TaxAmount { get; set; }

        // Libellé(4)
        public string DetailTaxAmount4_TaxCaption { get; set; }

        // --------------------------------------------------

        // Base de calcul(5)
        public string DetailTaxAmount5_TaxCalculationBase { get; set; }

        // Base(5)
        public string DetailTaxAmount5_BaseAmount { get; set; }

        // Montant de la taxe(5)
        public string DetailTaxAmount5_TaxAmount { get; set; }

        // Libellé(5)
        public string DetailTaxAmount5_TaxCaption { get; set; }

        // reference
        public string reference { get; set; }


        // Line du document
        public List<DocumentVenteLine> lines { get; set; }

        public DocumentVente()
        {

        }

       public DocumentVente(string Id,
       string DocumentNumber,
       string NumberPrefix,
       string NumberSuffix,
       string DocumentDate,
       string DeliveryDate,
       string Adresse1_facturation ,
       string Adresse2_facturation ,
       string Adresse3_facturation ,
       string Adresse4_facturation ,
       string Codepostal_facturation ,
       string Ville_facturation ,
       string codePays_facturation,
       string Adresse1_livraison ,
       string Adresse2_livraison ,
       string Adresse3_livraison ,
       string Adresse4_livraison ,
       string Codepostal_livraison ,
       string Ville_livraison ,
       string codePays_livraison,
       string CommitmentsBalanceDue,
       string AmountVatExcluded,
       string DiscountRate,
       string DiscountAmount,
       string AmountVatExcludedWithDiscount,
       string AmountVatExcludedWithDiscountAndShipping,
       string AmountVatExcludedWithDiscountAndShippingWithoutEcotax,
       string VatAmount,
       string AmountVatIncluded,
       string DepositAmount,
       string DepositCurrencyAmount,
       string TotalDueAmount,
       string DetailVatAmount0_DetailVatRate,
       string DetailVatAmount0_DetailAmountVatExcluded,
       string PaymentTypeId,
       string BankId,
       string FinancialDiscountType,
       string FinancialDiscountRate,
       string FinancialDiscountAmount,
       string CurrencyId,
       string IntrastatTransportMode,
       string CustomerId, 
       string CustomerName,
       string DocumentType,
       string OriginDocumentType,
       string TransferedDocumentId,
         string InvoicingContact_Name,
        string InvoicingContact_FirstName,
        string InvoicingContact_Phone,
        string InvoicingContact_CellPhone,
        string InvoicingContact_Fax,
        string InvoicingContact_Email,
        string InvoicingContact_Function,
        string DeliveryContact_Name,
        string DeliveryContact_FirstName,
        string DeliveryContact_Phone,
        string DeliveryContact_CellPhone,
        string DeliveryContact_Fax,
        string DeliveryContact_Email,
        string DeliveryContact_Function,
        string DetailTaxAmount0_TaxCalculationBase,
        string DetailTaxAmount0_BaseAmount,
        string DetailTaxAmount0_TaxAmount,
        string DetailTaxAmount0_TaxCaption,
        string DetailTaxAmount1_TaxCalculationBase,
        string DetailTaxAmount1_BaseAmount,
        string DetailTaxAmount1_TaxAmount,
        string DetailTaxAmount1_TaxCaption,
        string DetailTaxAmount2_TaxCalculationBase,
        string DetailTaxAmount2_BaseAmount,
        string DetailTaxAmount2_TaxAmount,
        string DetailTaxAmount2_TaxCaption,
        string DetailTaxAmount3_TaxCalculationBase,
        string DetailTaxAmount3_BaseAmount,
        string DetailTaxAmount3_TaxAmount,
        string DetailTaxAmount3_TaxCaption,
        string DetailTaxAmount4_TaxCalculationBase,
        string DetailTaxAmount4_BaseAmount,
        string DetailTaxAmount4_TaxAmount,
        string DetailTaxAmount4_TaxCaption,
        string DetailTaxAmount5_TaxCalculationBase,
        string DetailTaxAmount5_BaseAmount,
        string DetailTaxAmount5_TaxAmount,
        string DetailTaxAmount5_TaxCaption,
           string reference)
        {

       this.Id=Id;
       this.DocumentNumber=DocumentNumber;
       this.NumberPrefix=NumberPrefix;
       this.NumberSuffix=NumberSuffix;
       this.DocumentDate=DocumentDate;
       this.DeliveryDate=DeliveryDate;
       this.Adresse1_facturation =Adresse1_facturation;
       this.Adresse2_facturation =Adresse2_facturation;
       this.Adresse3_facturation =Adresse3_facturation;
       this.Adresse4_facturation =Adresse4_facturation;
       this.Codepostal_facturation =Codepostal_facturation;
       this.Ville_facturation =Ville_facturation;
       this.codePays_facturation=codePays_facturation;
       this.Adresse1_livraison =Adresse1_livraison;
       this.Adresse2_livraison =Adresse2_livraison;
       this.Adresse3_livraison =Adresse3_livraison;
       this.Adresse4_livraison =Adresse4_livraison;
       this.Codepostal_livraison =Codepostal_livraison;
       this.Ville_livraison =Ville_livraison;
       this.codePays_livraison=codePays_livraison;
       this.CommitmentsBalanceDue=CommitmentsBalanceDue;
       this.AmountVatExcluded=AmountVatExcluded;
       this.DiscountRate=DiscountRate;
       this.DiscountAmount=DiscountAmount;
       this.AmountVatExcludedWithDiscount=AmountVatExcludedWithDiscount;
       this.AmountVatExcludedWithDiscountAndShipping=AmountVatExcludedWithDiscountAndShipping;
       this.AmountVatExcludedWithDiscountAndShippingWithoutEcotax=AmountVatExcludedWithDiscountAndShippingWithoutEcotax;
       this.VatAmount=VatAmount;
       this.AmountVatIncluded=AmountVatIncluded;
       this.DepositAmount=DepositAmount;
       this.DepositCurrencyAmount=DepositCurrencyAmount;
       this.TotalDueAmount=TotalDueAmount;
       this.DetailVatAmount0_DetailVatRate=DetailVatAmount0_DetailVatRate;
       this.DetailVatAmount0_DetailAmountVatExcluded=DetailVatAmount0_DetailAmountVatExcluded;
       this.PaymentTypeId=PaymentTypeId;
       this.BankId=BankId;
       this.FinancialDiscountType=FinancialDiscountType;
       this.FinancialDiscountRate=FinancialDiscountRate;
       this.FinancialDiscountAmount=FinancialDiscountAmount;
       this.CurrencyId=CurrencyId;
       this.IntrastatTransportMode=IntrastatTransportMode;
       this.CustomerId= CustomerId;
       this.CustomerName = CustomerName;
       this.DocumentType = DocumentType;
       this.OriginDocumentType = OriginDocumentType;
       this.TransferedDocumentId = TransferedDocumentId;
       this.InvoicingContact_Name = InvoicingContact_Name;
       this.InvoicingContact_FirstName = InvoicingContact_FirstName;
       this.InvoicingContact_Phone = InvoicingContact_Phone;
       this.InvoicingContact_CellPhone = InvoicingContact_CellPhone;
       this.InvoicingContact_Fax = InvoicingContact_Fax;
       this.InvoicingContact_Email = InvoicingContact_Email;
       this.InvoicingContact_Function = InvoicingContact_Function;
       this.DeliveryContact_Name = DeliveryContact_Name;
       this.DeliveryContact_FirstName = DeliveryContact_FirstName;
       this.DeliveryContact_Phone = DeliveryContact_Phone;
       this.DeliveryContact_CellPhone = DeliveryContact_CellPhone;
       this.DeliveryContact_Fax = DeliveryContact_Fax;
       this.DeliveryContact_Email = DeliveryContact_Email;
       this.DeliveryContact_Function = DeliveryContact_Function;
       this.DeliveryContact_Function = DetailTaxAmount0_TaxCalculationBase;
       this.DetailTaxAmount0_BaseAmount = DetailTaxAmount0_BaseAmount;
       this.DetailTaxAmount0_TaxAmount = DetailTaxAmount0_TaxAmount;
       this.DetailTaxAmount0_TaxCaption = DetailTaxAmount0_TaxCaption;
       this.DetailTaxAmount1_TaxCalculationBase = DetailTaxAmount1_TaxCalculationBase;
       this.DetailTaxAmount1_BaseAmount = DetailTaxAmount1_BaseAmount;
       this.DetailTaxAmount1_TaxAmount = DetailTaxAmount1_TaxAmount;
       this.DetailTaxAmount1_TaxCaption = DetailTaxAmount1_TaxCaption;
       this.DetailTaxAmount2_TaxCalculationBase = DetailTaxAmount2_TaxCalculationBase;
       this.DetailTaxAmount2_BaseAmount = DetailTaxAmount2_BaseAmount;
       this.DetailTaxAmount2_TaxAmount = DetailTaxAmount2_TaxAmount;
       this.DetailTaxAmount2_TaxCaption = DetailTaxAmount2_TaxCaption;
       this.DetailTaxAmount3_TaxCalculationBase = DetailTaxAmount3_TaxCalculationBase;
       this.DetailTaxAmount3_BaseAmount = DetailTaxAmount3_BaseAmount;
       this.DetailTaxAmount3_TaxAmount = DetailTaxAmount3_TaxAmount;
                   this.DetailTaxAmount3_TaxCaption = DetailTaxAmount3_TaxCaption;
                   this.DetailTaxAmount4_TaxCalculationBase = DetailTaxAmount4_TaxCalculationBase;
                   this.DetailTaxAmount4_BaseAmount = DetailTaxAmount4_BaseAmount;
                   this.DetailTaxAmount4_TaxAmount = DetailTaxAmount4_TaxAmount;
                   this.DetailTaxAmount4_TaxCaption = DetailTaxAmount4_TaxCaption;
                   this.DetailTaxAmount5_TaxCalculationBase = DetailTaxAmount5_TaxCalculationBase;
                   this.DetailTaxAmount5_BaseAmount = DetailTaxAmount5_BaseAmount;
                   this.DetailTaxAmount5_TaxAmount = DetailTaxAmount5_TaxAmount;
                   this.DetailTaxAmount5_TaxCaption = DetailTaxAmount5_TaxCaption;
                   this.reference = reference;
       this.lines = new List<DocumentVenteLine>();

        }
        

    }
}
