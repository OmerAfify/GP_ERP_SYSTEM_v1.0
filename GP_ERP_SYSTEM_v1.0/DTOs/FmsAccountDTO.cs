using System;
using System.Collections.Generic;

namespace GP_ERP_SYSTEM_v1._0.DTOs
{
    public class AddFmsAccountDTO
    {
        
        public string AccName { get; set; }
        
        public string IncreaseMode { get; set; }
    }
    public class FmsAccountDTO : AddFmsAccountDTO
    {
       public int AccId { get; set; }
       public decimal? AccBalance { get; set; }
       public decimal? AccDebit { get; set; }
       public decimal? AccCredit { get; set; }
    }

    public class FmsAccountIdDto
    {
        public int AccId { get; set; }
    }
    public class ViewFmsAccountDTO : FmsAccountDTO
    {
        public List<string> AccCategories { get; set; }
    }
    public class AddFmsCategoryDTO
    {
        
        public string CatName { get; set; }
        public string CatDescription { get; set; }
    }

    public class FmsCategoryDTO : AddFmsCategoryDTO
    {
        public int CatId { get; set; }
    }

    public class ViewFmsCategoryDTO : FmsCategoryDTO
    {
        public List<string> catAccounts { get; set; }
    }

    public class FmsAccCatDTO
    {
        public int AccId { get; set; }
        public int CatId { get; set; }
    }

 
}
