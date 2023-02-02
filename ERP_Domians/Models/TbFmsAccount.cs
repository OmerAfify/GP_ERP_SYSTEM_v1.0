using System;
using System.Collections.Generic;

#nullable disable

namespace ERP_BusinessLogic.Models
{
    public partial class TbFmsAccount
    {
        public TbFmsAccount()
        {
            TbFmsJournalEntryJeaccount1Navigations = new HashSet<TbFmsJournalEntry>();
            TbFmsJournalEntryJeaccount2Navigations = new HashSet<TbFmsJournalEntry>();
        }

        public int AccId { get; set; }
        public string AccName { get; set; }
        public string AccCategories { get; set; }
        public decimal? AccBalance { get; set; }

        public virtual ICollection<TbFmsJournalEntry> TbFmsJournalEntryJeaccount1Navigations { get; set; }
        public virtual ICollection<TbFmsJournalEntry> TbFmsJournalEntryJeaccount2Navigations { get; set; }
    }
}
