using RiftWorld.DATA.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RiftWorld.UI.MVC.Models
{
    public class MiniJournalVM
    {
        public int JournalId { get; set; }
        public string ContentMini { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Written { get; set; }
        public bool IsLonger { get; set; }

        public MiniJournalVM(Journal journal)
        {
            JournalId = journal.JournalId;
            Written = journal.OocDateWritten;
            if (journal.TheContent.Length > 50)
            {
                IsLonger = true;
                ContentMini = ((journal.TheContent).ToString()).Substring(0, 50);
            }
            else
            {
                IsLonger = false;
                ContentMini = journal.TheContent;
            }
        }
    }
    public class JournalVM
    {
        public List<MiniJournalVM> Journals { get; set; }
        public bool HasMore { get; set; }
        public short CharacterId { get; set; }
    }

    public class UnderApprovalVM
    {
        public List<Rumor> Rumors { get; set; }
        public List<Journal> Journals { get; set; }
        public List<CharOrg> Orgs { get; set; }
    }
}