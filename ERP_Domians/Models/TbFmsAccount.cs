using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_Domians.Models
{
    public partial class TbFmsAccount
    {
        public TbFmsAccount()
        {
            TbFmsAccCats = new HashSet<TbFmsAccCat>();
            TbFmsJournalEntryJeaccount1Navigations = new HashSet<TbFmsJournalEntry>();
            TbFmsJournalEntryJeaccount2Navigations = new HashSet<TbFmsJournalEntry>();
            TbFmsTemplateAccounts = new HashSet<TbFmsTemplateAccount>();
        }

        public int AccId { get; set; }
        public string AccName { get; set; }
        public decimal? AccBalance { get; set; }
        public decimal? AccDebit { get; set; }
        public decimal? AccCredit { get; set; }
        public int IncreaseMode { get; set; }

        public virtual ICollection<TbFmsAccCat> TbFmsAccCats { get; set; }
        public virtual ICollection<TbFmsJournalEntry> TbFmsJournalEntryJeaccount1Navigations { get; set; }
        public virtual ICollection<TbFmsJournalEntry> TbFmsJournalEntryJeaccount2Navigations { get; set; }
        public virtual ICollection<TbFmsTemplateAccount> TbFmsTemplateAccounts { get; set; }
    }
}
