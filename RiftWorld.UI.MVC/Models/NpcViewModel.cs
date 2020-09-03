using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Runtime;
using RiftWorld.DATA.EF;

namespace RiftWorld.UI.MVC.Models
{
    public class NpcCreateVM
    {
        #region Fields
        private string _apperanceText;
        private string _aboutText;
        private string _alias;
        #endregion

        [Required]
        [StringLength(30, ErrorMessage = " ")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        [Display(Name = "Known Aliases")]
        public string Alias
        {
            get { return _alias; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _alias = "Nada";
                }
                else
                {
                    _alias = value;
                }
            }
        }


        [Required]
        [StringLength(150, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Quote { get; set; }

        public string PortraitFileName { get; set; }

        [Display(Name = "Species")]
        public Nullable<byte> RaceId { get; set; }

        public string CrestFileName { get; set; }

        [StringLength(2000)]
        [UIHint("MultilineText")]
        [Display(Name = "Apperance")]
        [AllowHtml]
        public string ApperanceText
        {
            get { return _apperanceText; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _apperanceText = "Nada";
                }
                else
                {
                    _apperanceText = value;
                }
            }
        }

        [UIHint("MultilineText")]
        [Display(Name = "About")]
        [AllowHtml]
        public string AboutText
        {
            get { return _aboutText; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _aboutText = "Nada";
                }
                else
                {
                    _aboutText = value;
                }
            }
        }

        [Display(Name = "Last Location")]
        public Nullable<short> LastLocationId { get; set; }

        public bool IsPublished { get; set; }

        [StringLength(40, ErrorMessage = " ")]
        public string PortraitArtist { get; set; }

        [StringLength(40, ErrorMessage = " ")]
        public string CrestArtist { get; set; }

        public bool IsDead { get; set; }
        public byte GenderId { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }

    }

    public class NpcEditPostVM
    {
        #region Fields
        private string _apperanceText;
        private string _aboutText;
        private string _alias;
        #endregion

        #region Props
        public short InfoId { get; set; }
        public short NpcId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = " ")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        [Display(Name = "Known Aliases")]
        public string Alias
        {
            get { return _alias; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _alias = "Nada";
                }
                else
                {
                    _alias = value;
                }
            }
        }


        [Required]
        [StringLength(150, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Quote { get; set; }

        public string PortraitFileName { get; set; }

        [Display(Name = "Species")]
        public Nullable<byte> RaceId { get; set; }

        public string CrestFileName { get; set; }

        [StringLength(2000)]
        [UIHint("MultilineText")]
        [Display(Name = "Apperance")]
        [AllowHtml]
        public string ApperanceText
        {
            get { return _apperanceText; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _apperanceText = "Nada";
                }
                else
                {
                    _apperanceText = value;
                }
            }
        }

        [UIHint("MultilineText")]
        [Display(Name = "About")]
        [AllowHtml]
        public string AboutText
        {
            get { return _aboutText; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _aboutText = "Nada";
                }
                else
                {
                    _aboutText = value;
                }
            }
        }

        [Display(Name = "Last Location")]
        public Nullable<short> LastLocationId { get; set; }

        public bool IsPublished { get; set; }

        [StringLength(40, ErrorMessage = " ")]
        public string PortraitArtist { get; set; }

        [StringLength(40, ErrorMessage = " ")]
        public string CrestArtist { get; set; }

        public bool IsDead { get; set; }
        public byte GenderId { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }
        #endregion

        #region ctor
        public NpcEditPostVM() { }
        public NpcEditPostVM(NPC npc, Info info)
        {
            InfoId = npc.InfoId;
            NpcId = npc.NpcId;
            Name = npc.Name;
            Alias = npc.Alias;
            Quote = npc.Quote;
            PortraitFileName = npc.PortraitFileName;
            RaceId = npc.RaceId;
            CrestFileName = npc.CrestFileName;
            ApperanceText = npc.ApperanceText;
            AboutText = npc.AboutText;
            LastLocationId = npc.LastLocationId;
            IsPublished = npc.IsPublished;
            PortraitArtist = npc.PortraitArtist;
            CrestArtist = npc.CrestArtist;
            IsDead = npc.IsDead;
            GenderId = npc.GenderId;
            Blurb = info.Blurb;
            IsSecret = info.IsSecret;
        }
        #endregion
    }

    public class NpcEditVM
    {
        #region Fields
        private string _apperanceText;
        private string _aboutText;
        private string _alias;
        #endregion

        #region prop
        public short InfoId { get; set; }
        public short NpcId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = " ")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        [Display(Name = "Known Aliases")]
        public string Alias
        {
            get
            {
                if (_alias == "Nada")
                {
                    return "";
                }
                else
                {
                    return _alias;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _alias = "Nada";
                }
                else
                {
                    _alias = value;
                }
            }
        }


        [Required]
        [StringLength(150, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Quote { get; set; }

        public string PortraitFileName { get; set; }

        [Display(Name = "Species")]
        public Nullable<byte> RaceId { get; set; }

        public string CrestFileName { get; set; }

        [StringLength(2000)]
        [UIHint("MultilineText")]
        [Display(Name = "Apperance")]
        [AllowHtml]
        public string ApperanceText
        {
            get
            {
                if (_apperanceText == "Nada")
                {
                    return "";
                }
                else
                {
                    return _apperanceText;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _apperanceText = "Nada";
                }
                else
                {
                    _apperanceText = value;
                }
            }
        }

        [UIHint("MultilineText")]
        [Display(Name = "About")]
        [AllowHtml]
        public string AboutText
        {
            get
            {
                if (_aboutText == "Nada")
                {
                    return "";
                }
                else
                {
                    return _aboutText;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _aboutText = "Nada";
                }
                else
                {
                    _aboutText = value;
                }
            }
        }

        [Display(Name = "Last Location")]
        public Nullable<short> LastLocationId { get; set; }

        public bool IsPublished { get; set; }

        [StringLength(40, ErrorMessage = " ")]
        public string PortraitArtist { get; set; }

        [StringLength(40, ErrorMessage = " ")]
        public string CrestArtist { get; set; }

        public bool IsDead { get; set; }
        public byte GenderId { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }

        #endregion

        #region ctor
        public NpcEditVM() { }
        public NpcEditVM(NPC npc, Info info)
        {
            InfoId = npc.InfoId;
            NpcId = npc.NpcId;
            Name = npc.Name;
            Alias = npc.Alias;
            Quote = npc.Quote;
            PortraitFileName = npc.PortraitFileName;
            RaceId = npc.RaceId;
            CrestFileName = npc.CrestFileName;
            ApperanceText = npc.ApperanceText;
            AboutText = npc.AboutText;
            LastLocationId = npc.LastLocationId;
            IsPublished = npc.IsPublished;
            PortraitArtist = npc.PortraitArtist;
            CrestArtist = npc.CrestArtist;
            IsDead = npc.IsDead;
            GenderId = npc.GenderId;
            Blurb = info.Blurb;
            IsSecret = info.IsSecret;
        }
        public NpcEditVM(NpcEditPostVM npc)
        {
            InfoId = npc.InfoId;
            NpcId = npc.NpcId;
            Name = npc.Name;
            Alias = npc.Alias;
            Quote = npc.Quote;
            PortraitFileName = npc.PortraitFileName;
            RaceId = npc.RaceId;
            CrestFileName = npc.CrestFileName;
            ApperanceText = npc.ApperanceText;
            AboutText = npc.AboutText;
            LastLocationId = npc.LastLocationId;
            IsPublished = npc.IsPublished;
            PortraitArtist = npc.PortraitArtist;
            CrestArtist = npc.CrestArtist;
            IsDead = npc.IsDead;
            GenderId = npc.GenderId;
            Blurb = npc.Blurb;
            IsSecret = npc.IsSecret;
        }
        #endregion
    }

    public class AssoNpcVM
    {
        public short InfoId { get; set; }
        public short NpcId { get; set; }
        public string Submit { get; set; }
        public string Name { get; set; }

        public List<AssoOrg_Npc> AssoOrgs { get; set; }
        public List<AssoClass_Npc> AssoClasses { get; set; }
    }

    public class AssoOrg_Npc
    {
        public short OrgId { get; set; }

        [Display(Name = "Priority of org display on NPC's page")]
        public Nullable<byte> OrgOrder { get; set; }

        [StringLength(50, ErrorMessage = " ")]
        [Display(Name = "Blurb for NPC's page about postion in org")]
        public string BlurbNpcPage { get; set; }

        [StringLength(50, ErrorMessage = " ")]
        [Display(Name = "Blurb for org's page about NPC's role")]
        public string BlurbOrgPage { get; set; }

        [Display(Name = "Priority of NPC display on org's page")]
        public Nullable<byte> MemberOrder { get; set; }

        public bool IsCurrent { get; set; }

        public AssoOrg_Npc() { }
        public AssoOrg_Npc(NpcOrg npcOrg)
        {
            OrgId = npcOrg.OrgId;
            OrgOrder = npcOrg.OrgOrder;
            BlurbNpcPage = npcOrg.BlurbNpcPage;
            BlurbOrgPage = npcOrg.BlurbOrgPage;
            MemberOrder = npcOrg.MemberOrder;
            IsCurrent = npcOrg.IsCurrent;
        }

    }

    public class AssoClass_Npc
    {
        public byte ClassId { get; set; }
        public Nullable<byte> ClassOrder { get; set; }

        public AssoClass_Npc() { }
        public AssoClass_Npc(ClassNPC classNPC)
        {
            ClassId = (byte)classNPC.ClassId;
            ClassOrder = classNPC.ClassOrder;
        }
    }

    //now displaying models 
    public class _NpcOrgsVM
    {
        public string Name { get; set; }
        public string Blurb { get; set; }
        public short Id { get; set; }
    }
}
