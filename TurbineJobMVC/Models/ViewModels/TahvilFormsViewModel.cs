namespace TurbineJobMVC.Models.ViewModels
{
    public class TahvilFormsViewModel
    {
        public long CodeTahvil { get; set; }
        public short TypeTahvil { get; set; }
        public string DateTahvil { get; set; }
        public string TypeName { get; set; }
        public string DeliverCode { get; set; }
        public string RegisterNo { get; set; }
        public string ApproveCode { get; set; }
        public string Office { get; set; }
        public string Managment { get; set; }
        public long Wono { get; set; }
        public long AmvalNo { get; set; }
        public long PartNumber { get; set; }
        public short PartNumberCount { get; set; }
        public string Serial { get; set; }
        public string Des { get; set; }
        public string PartName { get; set; }
        public string PartType { get; set; }
        public string PartModelName { get; set; }
        public int DevilerCodeOrigin { get; set; }
        public string Icon { get; set; }
        public long? ActiveWono { get; set; }
        public bool? HasArchive { get; set; }
    }
}