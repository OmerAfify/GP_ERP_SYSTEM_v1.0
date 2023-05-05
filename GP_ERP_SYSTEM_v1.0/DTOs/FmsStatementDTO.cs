using System;
using System.Collections.Generic;

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

    public class ViewFmsStatementDTO : FmsStatementDTO
    {
        public List<FmsStatementAccountDTO> accounts { get; set; }
    }

    public class FmsStatementAccountDTO
    {
        public string AccName { get; set; }
        public int StaId { get; set; }
        public decimal? AccBalance { get; set; }
    }

    public class AddFmsTemplateDTO : FmsTemplateDTO
    {
        public List<FmsAccountIdDto> Accounts { get; set; }
    }

    public class FmsTemplateDTO
    {
        public string TempName { get; set; }
        public DateTime? TempDate { get; set; }
    }

  
    public class ViewFmsTemplateDTO : FmsTemplateDTO
    {

        public int TempId { get; set; }
        public List<FmsAccountDTO> Accounts { get; set; }
    }

    public class ViewFmsTemplateListDTO : FmsTemplateDTO
    {

        public int TempId { get; set; }
        public List<int> Accounts { get; set; }
    }

    public class FmsTemplateAccountDTO
    {
        public int AccId { get; set; }
        public int TempId { get; set; }

    }




}
