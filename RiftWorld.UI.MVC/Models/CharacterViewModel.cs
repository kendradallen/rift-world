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

    public class AdminCharaCreate
    {
        [Display(Name = "Player")]
        public string PlayerId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(25)]
        public string CharacterName { get; set; }

        [Required]
        [Display(Name = "Species")]
        public byte RaceId { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public byte GenderId { get; set; }

        public string PortraitFileName { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [StringLength(8000)]
        public string About { get; set; }

        [Required] //may need to be removed due to weirdness during inital creation
        [Display(Name = "Current Location")]
        public short CurrentLocationId { get; set; }

        [Required]
        [Display(Name = "Tier")]
        public byte TierId { get; set; }

        public bool IsRetired { get; set; }

        public bool IsApproved { get; set; }

        [StringLength(40)]
        public string Artist { get; set; }

        [Required]
        [StringLength(40)]
        [Display(Name = "Class")]
        public string ClassString { get; set; }

        public bool HasUnseenEdit { get; set; }
        public bool IsDead { get; set; }

        [StringLength(32)]
        public string BackupPortrayerName { get; set; }

        public bool IsPlayerDemo { get; set; }
        public bool IsRequestingRetire { get; set; }

    }
}