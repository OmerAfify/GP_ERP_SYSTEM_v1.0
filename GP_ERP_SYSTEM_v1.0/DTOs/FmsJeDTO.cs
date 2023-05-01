using System;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddFmsJeDTO
    {
        public string Jename { get; set; }
        public string Jedescription { get; set; }
        public decimal? Jecredit { get; set; }
        public decimal? Jedebit { get; set; }
        public DateTime? Jedate { get; set; }
        public int Jeaccount1 { get; set; }
        public int Jeaccount2 { get; set; }
    }

    public class FmsJeDTO : AddFmsJeDTO
    {
        public int Jeid { get; set; }
    }
}
