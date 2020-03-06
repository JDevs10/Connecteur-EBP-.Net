using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConnecteurEBP.Classes
{
    public class DocumentVenteLine
    {
        // Numéro de ligne
        public string LineOrder { get; set; }

        // Description Clear
        public string DescriptionClear { get; set; }

        //Code article
        public string ItemId { get; set; }

        // Quantity
        public string Quantity { get; set; }

        // Quantité totale
        public string RealQuantity { get; set; }

        // Prix d'achat
        public string PurchasePrice { get; set; }

        // Prix de revient unitaire
        public string CostPrice { get; set; }

        // % remise cumulée
        public string TotalDiscountRate { get; set; }

        // PV Net HT
        public string NetPriceVatExcluded { get; set; }

        // PV Net TTC
        public string NetPriceVatIncluded { get; set; }

        // PV Net HT remisé
        public string NetPriceVatExcludedWithDiscount { get; set; }

        // PV Net TTC remisé
        public string NetPriceVatIncludedWithDiscount { get; set; }

        // Montant Net HT
        public string NetAmountVatExcluded { get; set; }

        // Montant Net HT remisé
        public string NetAmountVatExcludedWithDiscount { get; set; }

        // Montant Net TTC
        public string NetAmountVatIncluded { get; set; }

        // Montant Net TTC remisé
        public string NetAmountVatIncludedWithDiscount { get; set; }

        // TVA
        public string VatId { get; set; }

        // Montant de TVA
        public string VatAmount { get; set; }

        // Quantité commandée
        public string OrderedQuantity { get; set; }

        // Date de livraison
        public string DeliveryDate { get; set; }

        // Quantité livrée
        public string DeliveredQuantity { get; set; }

        // Poids Net
        public string NetWeight { get; set; }

        // Poids Net total
        public string TotalNetWeight { get; set; }

        // Montant total Net HT
        public string RealNetAmountVatExcluded { get; set; }

        // Montant total Net HT remisé
        public string RealNetAmountVatExcludedWithDiscount { get; set; }

        // Montant total Net TTC
        public string RealNetAmountVatIncluded { get; set; }

        // Montant total Net TTC remisé
        public string RealNetAmountVatIncludedWithDiscount { get; set; }

        // Montant total Net HT remisé escompté
        public string RealNetAmountVatExcludedWithDiscountAndFinancialDiscount { get; set; }

        // Montant total Net TTC remisé escompté
        public string RealNetAmountVatIncludedWithDiscountAndFinancialDiscount { get; set; }

        // PV HT
        public string SalePriceVatExcluded { get; set; }

        // PV TTC
        public string SalePriceVatIncluded { get; set; }

        // Série/lot
        public string TrackingNumber { get; set; }

        // Volume
        public string Volume { get; set; }

        // Volume total (m3)
        public string TotalVolume { get; set; }

        // Nombre d'article/colis
        public string NumberOfItemByPackage { get; set; }

        // ------------------------------------------

        // % remise(0)
        public string Discounts0_UnitDiscountRate { get; set; }

        // Montant unitaire de la remise HT(0)
        public string Discounts0_UnitDiscountAmountVatExcluded { get; set; }

        // Type de remise(0)
        public string Discounts0_DiscountType { get; set; }

        // % remise(1)
        public string Discounts1_UnitDiscountRate { get; set; }

        // Montant unitaire de la remise HT(1)
        public string Discounts1_UnitDiscountAmountVatExcluded { get; set; }

        // Type de remise(1)
        public string Discounts1_DiscountType { get; set; }

        // % remise(2)
        public string Discounts2_UnitDiscountRate { get; set; }

        // Montant unitaire de la remise HT(2)
        public string Discounts2_UnitDiscountAmountVatExcluded { get; set; }

        // Type de remise(2)
        public string Discounts2_DiscountType { get; set; }

        // ------------------------------------------------------

        // Valeur de la taxe(0)
        public string OtherTaxes0_TaxValue { get; set; }

        // Montant de la taxe(0)
        public string OtherTaxes0_TaxAmount { get; set; }

        // Valeur de la taxe(1)
        public string  OtherTaxes1_TaxValue { get; set; }

        // Montant de la taxe(1)
        public string OtherTaxes1_TaxAmount { get; set; }

        // Valeur de la taxe(2)
        public string OtherTaxes2_TaxValue { get; set; }

        // Montant de la taxe(2)
        public string OtherTaxes2_TaxAmount { get; set; }


        public DocumentVenteLine()
        {

        }

        public DocumentVenteLine(string LineOrder,
        string DescriptionClear,
        string ItemId,
        string Quantity,
        string RealQuantity,
        string PurchasePrice,
        string CostPrice,
        string TotalDiscountRate,
        string NetPriceVatExcluded,
        string NetPriceVatIncluded,
        string NetPriceVatExcludedWithDiscount,
        string NetPriceVatIncludedWithDiscount,
        string NetAmountVatExcluded,
        string NetAmountVatExcludedWithDiscount,
        string NetAmountVatIncluded,
        string NetAmountVatIncludedWithDiscount,
        string VatId,
        string OrderedQuantity,
        string VatAmount,
        string DeliveryDate,
        string DeliveredQuantity,
        string NetWeight,
        string TotalNetWeight,
        string RealNetAmountVatExcluded,
        string RealNetAmountVatExcludedWithDiscount,
        string RealNetAmountVatIncluded,
        string RealNetAmountVatIncludedWithDiscount,
        string RealNetAmountVatExcludedWithDiscountAndFinancialDiscount,
        string RealNetAmountVatIncludedWithDiscountAndFinancialDiscount,
        string SalePriceVatExcluded,
        string SalePriceVatIncluded,
        string TrackingNumber,
        string Volume,
        string TotalVolume,
        string Discounts0_UnitDiscountRate,
        string Discounts0_UnitDiscountAmountVatExcluded,
        string Discounts0_DiscountType,
        string Discounts1_UnitDiscountRate,
        string Discounts1_UnitDiscountAmountVatExcluded,
        string Discounts1_DiscountType,
        string Discounts2_UnitDiscountRate,
        string Discounts2_UnitDiscountAmountVatExcluded,
        string Discounts2_DiscountType,
        string OtherTaxes0_TaxValue,
        string OtherTaxes0_TaxAmount,
        string OtherTaxes1_TaxValue,
        string OtherTaxes1_TaxAmount,
        string OtherTaxes2_TaxValue,
        string OtherTaxes2_TaxAmount,
        string NumberOfItemByPackage)
        {
         this.LineOrder=LineOrder;
         this.DescriptionClear = DescriptionClear;
         this.ItemId = ItemId;
         this.Quantity = Quantity;
         this.RealQuantity = RealQuantity;
         this.PurchasePrice = PurchasePrice;
         this.CostPrice = CostPrice;
         this.TotalDiscountRate = TotalDiscountRate;
         this.NetPriceVatExcluded = NetPriceVatExcluded;
         this.NetPriceVatIncluded = NetPriceVatIncluded;
         this.NetPriceVatExcludedWithDiscount = NetPriceVatExcludedWithDiscount;
         this.NetPriceVatIncludedWithDiscount = NetPriceVatIncludedWithDiscount;
         this.NetAmountVatExcluded = NetAmountVatExcluded;
         this.NetAmountVatExcludedWithDiscount = NetAmountVatExcludedWithDiscount;
         this.NetAmountVatIncluded = NetAmountVatIncluded;
         this.NetAmountVatIncludedWithDiscount = NetAmountVatIncludedWithDiscount;
         this.VatId = VatId;
         this.OrderedQuantity = OrderedQuantity;
         this.VatAmount = VatAmount;
         this.DeliveryDate = DeliveryDate;
         this.DeliveredQuantity = DeliveredQuantity;
         this.NetWeight = NetWeight;
         this.TotalNetWeight = TotalNetWeight;
         this.RealNetAmountVatExcluded = RealNetAmountVatExcluded;
         this.RealNetAmountVatExcludedWithDiscount = RealNetAmountVatExcludedWithDiscount;
         this.RealNetAmountVatIncluded = RealNetAmountVatIncluded;
         this.RealNetAmountVatIncludedWithDiscount = RealNetAmountVatIncludedWithDiscount;
         this.RealNetAmountVatExcludedWithDiscountAndFinancialDiscount = RealNetAmountVatExcludedWithDiscountAndFinancialDiscount;
         this.RealNetAmountVatIncludedWithDiscountAndFinancialDiscount = RealNetAmountVatIncludedWithDiscountAndFinancialDiscount;
         this.SalePriceVatExcluded = SalePriceVatExcluded;
         this.SalePriceVatIncluded = SalePriceVatIncluded;
         this.TrackingNumber = TrackingNumber;
         this.Volume = Volume;
         this.TotalVolume = TotalVolume;
         this.Discounts0_UnitDiscountRate = Discounts0_UnitDiscountRate;
         this.Discounts0_UnitDiscountAmountVatExcluded = Discounts0_UnitDiscountAmountVatExcluded;
         this.Discounts0_DiscountType = Discounts0_DiscountType;
         this.Discounts1_UnitDiscountRate = Discounts1_UnitDiscountRate;
         this.Discounts1_UnitDiscountAmountVatExcluded = Discounts1_UnitDiscountAmountVatExcluded;
         this.Discounts1_DiscountType = Discounts1_DiscountType;
         this.Discounts2_UnitDiscountRate = Discounts2_UnitDiscountRate;
         this.Discounts2_UnitDiscountAmountVatExcluded = Discounts2_UnitDiscountAmountVatExcluded;
         this.Discounts2_DiscountType = Discounts2_DiscountType;
         this.OtherTaxes0_TaxValue = OtherTaxes0_TaxValue;
         this.OtherTaxes0_TaxAmount = OtherTaxes0_TaxAmount;
         this.OtherTaxes1_TaxValue = OtherTaxes1_TaxValue;
         this.OtherTaxes1_TaxAmount = OtherTaxes1_TaxAmount;
         this.OtherTaxes2_TaxValue = OtherTaxes2_TaxValue;
         this.OtherTaxes2_TaxAmount = OtherTaxes2_TaxAmount;
         this.NumberOfItemByPackage = NumberOfItemByPackage;
        }

    }
}
