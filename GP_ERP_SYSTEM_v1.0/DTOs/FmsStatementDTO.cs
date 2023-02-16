using System;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddFmsStatementDTO
    {
        
        public string StaName { get; set; }
        public decimal? StaBalance { get; set; }
        public DateTime? StaDate { get; set; }
    }

    public class FmsStatementDTO : AddFmsStatementDTO
    {
        public int StaId { get; set; }
    }

    public class AddFmsStatementAccountDTO
    {
        public string AccName { get; set; }
        public int? StaId { get; set; }
        public decimal? AccBalance { get; set; }
    }
    public class FmsStatementAccountDTO : AddFmsStatementAccountDTO
    {
        public int Id { get; set; }
    }

    public class AddFmsStatementTemplateDTO
    {
        public string TempName { get; set; }
        public DateTime? TempDate { get; set; }
    }

    public class FmsStatementTemplateDTO : AddFmsStatementTemplateDTO
    {
        public int TempId { get; set; }
    }

    public class AddFmsTemplateAccountDTO
    {
        public int? AccId { get; set; }
        public int? TempId { get; set; }

    }

    public class FmsTemplateAccountDTO : AddFmsTemplateAccountDTO
    {
        public int Id { get; set; }
    }




}
