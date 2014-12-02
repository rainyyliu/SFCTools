using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SFC_Tools.Model
{
    class MDBomInfo
    {
    }
    public class MDBomMainInfo
    {
        public string ClientID { set; get; }
        public string PlantID { set; get; }
        public string BomNo { set; get; }
        public string MaterialNo { set; get; }
        public string SourceType { set; get; }
        public string UsefulLife { set; get; }
        public string SupplierName { set; get; }
        public string SupplierMaterialNo { set; get; }
        public decimal Length { set; get; }
        public decimal Width { set; get; }
        public decimal Height { set; get; }
        public decimal UnitPrice { set; get; }
        public decimal GrossWeight { set; get; }
        public decimal NetWeight { set; get; }
        public string Description { set; get; }
        public string IsActive { set; get; }
        public string CreatedBy { set; get; }
        public string UpdatedBy { set; get; }
    }

    public class MDBomDetailInfo
    {
        public string ClientID { set; get; }
        public string PlantID { set; get; }
        public string BomNo { set; get; }
        public string MaterialNo { set; get; }
        public string ParentMaterialNo { set; get; }
        public int SeqNo { set; get; }
        public string MaterialLevel { set; get; }
        public int UnityQty { set; get; }
        public string Description { set; get; }
        public string IsActive { set; get; }
        public string CreatedBy { set; get; }
        public string UpdatedBy { set; get; }


    }

    public class MDBomAltInfo
    {
        public string ClientID { set; get; }
        public string PlantID { set; get; }
        public string BomNo { set; get; }
        public string MaterialNo { set; get; }
        public string AltMaterialNo { set; get; }
        public int SeqNo { set; get; }
        public string Usage { set; get; }
        public string Altrate { set; get; }
        public string IsActive { set; get; }
        public string CreatedBy { set; get; }
        public string UpdatedBy { set; get; }
    }
}
