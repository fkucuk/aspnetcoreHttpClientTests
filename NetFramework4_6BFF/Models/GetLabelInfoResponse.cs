using System;

namespace NetFramework4_6BFF.Models
{
    public class GetLabelInfoResponse
    {

        public LabelDetail LabelDetailField { get; set; }

        public PriceDetail PriceDetailField { get; set; }

        public ProductDetail ProductDetailField { get; set; }

        public StockDetail StockDetailField { get; set; }
        public TransferDetail TransferDetailField { get; set; }



    }

    public class LabelDetail
    {

        public string AtamaTarihField { get; set; }

        public string BuyukBeyazEtiketZplField { get; set; }

        public System.Nullable<int> EtiketTipField { get; set; }

        public bool FirsatUrunuMuField { get; set; }

        public System.Nullable<System.DateTime> FiyatDegisimTarihField { get; set; }

        public string IcBarkodEtiketZplField { get; set; }

        public string IlkFiyatDegisimEtiketZplField { get; set; }

        public string IndirimliEtiketZplField { get; set; }

        public System.Nullable<decimal> IskontoOranField { get; set; }

        public string KontrolBarkodField { get; set; }

        public bool TaksitEngelliMiField { get; set; }
    }

    public class PriceDetail
    {
        public System.Nullable<double> IlkPesinFiyatField { get; set; }
        public System.Nullable<double> IlkTaksitliFiyatField { get; set; }
        public System.Nullable<double> PesinFiyatField { get; set; }
        public System.Nullable<double> TaksitliFiyatField { get; set; }
    }

    public class ProductDetail
    {

        public string AdField { get; set; }

        public string BarkodField { get; set; }

        public string BedenField { get; set; }

        public int BedenSiraField { get; set; }

        public string LineTanimField { get; set; }

        public string OzelKodField { get; set; }

        public string RenkField { get; set; }

        public string RenkHexKodField { get; set; }

        public string RenkKodField { get; set; }

        public int UrunIdField { get; set; }

        public string UrunKlasmanField { get; set; }

        public bool YerliMaliMiField { get; set; }
    }

    public class StockDetail
    {

        public Nullable<int> DepoStokField { get; set; }

        public Nullable<int> OptionStokField { get; set; }

        public Nullable<int> ReyonStokField { get; set; }
    }

    public class TransferDetail
    {

        public TransferEntity[] TransferListField { get; set; }

    }

    public class TransferEntity 
    {

        public string BarkodField { get; set; }

        public string BedenField { get; set; }

        public string BoyField { get; set; }

        public string ToDepoField { get; set; }

        public System.Nullable<int> TransferMiktarField { get; set; }

        public string TransferTipField { get; set; }
    }
    }