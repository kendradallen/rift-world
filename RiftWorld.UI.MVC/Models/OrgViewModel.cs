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
    public class OrgCreateVM
    {
        #region Fields
        private string _aboutText;
        #endregion

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        public bool IsPlayerEnabled { get; set; }

        public string SymbolFileName { get; set; }

        [Display(Name = "Base of Operations")]
        public Nullable<short> BaseLocationId { get; set; }

        //[Required]
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

        public bool IsPublished { get; set; }

        [StringLength(40)]
        public string Artist { get; set; }

    }

    public class OrgEditPostVM
    {
        #region prop
        public short InfoId { get; set; }
        public short OrgId { get; set; }

        #region OrgCreate
        #region Fields
        private string _aboutText;
        #endregion

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        public bool IsPlayerEnabled { get; set; }

        public string SymbolFileName { get; set; }

        [Display(Name = "Base of Operations")]
        public Nullable<short> BaseLocationId { get; set; }

        //[Required]
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

        public bool IsPublished { get; set; }

        [StringLength(40)]
        public string Artist { get; set; }

        #endregion
        #endregion

        #region ctor
        public OrgEditPostVM() { }
        public OrgEditPostVM(Org org, string blurb)
        {
            InfoId = org.InfoId;
            OrgId = org.OrgId;
            Name = org.Name;
            IsPlayerEnabled = org.IsPlayerEnabled;
            SymbolFileName = org.SymbolFileName;
            BaseLocationId = org.BaseLocationId;
            AboutText = org.AboutText;
            IsPublished = org.IsPublished;
            Artist = org.Artist;
            Blurb = blurb;
        }
        #endregion
    }

    public class OrgEditVM
    {
        #region prop
        public short InfoId { get; set; }
        public short OrgId { get; set; }

        #region OrgCreate
        #region Fields
        private string _aboutText;
        #endregion

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        public bool IsPlayerEnabled { get; set; }

        public string SymbolFileName { get; set; }

        [Display(Name = "Base of Operations")]
        public Nullable<short> BaseLocationId { get; set; }

        //[Required]
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

        public bool IsPublished { get; set; }

        [StringLength(40)]
        public string Artist { get; set; }

        #endregion
        #endregion

        #region ctor
        public OrgEditVM() { }
        public OrgEditVM(Org org, string blurb)
        {
            InfoId = org.InfoId;
            OrgId = org.OrgId;
            Name = org.Name;
            IsPlayerEnabled = org.IsPlayerEnabled;
            SymbolFileName = org.SymbolFileName;
            BaseLocationId = org.BaseLocationId;
            AboutText = org.AboutText;
            IsPublished = org.IsPublished;
            Artist = org.Artist;
            Blurb = blurb;
        }
        public OrgEditVM(OrgEditPostVM org)
        {
            InfoId = org.InfoId;
            OrgId = org.OrgId;
            Name = org.Name;
            IsPlayerEnabled = org.IsPlayerEnabled;
            SymbolFileName = org.SymbolFileName;
            BaseLocationId = org.BaseLocationId;
            AboutText = org.AboutText;
            IsPublished = org.IsPublished;
            Artist = org.Artist;
            Blurb = org.Blurb;
        }
        #endregion
    }

    public class AssoOrgVM
    {
        public short InfoId { get; set; }
        public short OrgId { get; set; }
        public string Submit { get; set; }
        public string Name { get; set; }

        public List<AssoNpc_Org> AssoNpcs { get; set; }
        public List<AssoEvent_Org> AssoEvents { get; set; }
        public List<AssoChar_Org> AssoChars { get; set; }
    }

    public class AssoNpc_Org
    {
        public short NpcId { get; set; }

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

        public AssoNpc_Org() { }
        public AssoNpc_Org(NpcOrg npcOrg)
        {
            NpcId = npcOrg.NpcId;
            OrgOrder = npcOrg.OrgOrder;
            BlurbNpcPage = npcOrg.BlurbNpcPage;
            BlurbOrgPage = npcOrg.BlurbOrgPage;
            MemberOrder = npcOrg.MemberOrder;
            IsCurrent = npcOrg.IsCurrent;
        }
    }

    public class AssoEvent_Org
    {
        public short EventId { get; set; }

        [StringLength(100, ErrorMessage = " ")]
        public string Blurb { get; set; }

        public AssoEvent_Org() { }
        public AssoEvent_Org(OrgEvent orgEvent)
        {
            EventId = orgEvent.EventId;
            Blurb = orgEvent.Blurb;
        }
    }

    public class AssoChar_Org
    {
        public short CharId { get; set; }

        [StringLength(50, ErrorMessage = " ")]
        [Display(Name = "Blurb for org's page about character's role")]
        public string BlurbOrgPage { get; set; }

        public bool IsPublic { get; set; }
        public bool IsCurrent { get; set; }
        public bool KatherineApproved { get; set; }

        public AssoChar_Org() { }
        public AssoChar_Org(CharOrg charOrg)
        {
            CharId = charOrg.CharId;
            BlurbOrgPage = charOrg.BlurbOrgPage;
            IsPublic = charOrg.IsPublic;
            IsCurrent = charOrg.IsCurrent;
            KatherineApproved = charOrg.KatherineApproved;
        }
    }

    //display vms
    public class _MemberFullVM
    {
        public List<_MembersVM> CurrentMembers { get; set; }
        public List<_MembersVM> PastMembers { get; set; }
        public List<_MembersVM> SecretMembers { get; set; }
    }
    public class _MembersVM
    {
        public string Name { get; set; }
        public string Blurb { get; set; }
        public short Id { get; set; }
        public bool IsPlayer { get; set; }
        public Nullable<byte> DisplayOrder { get; set; }

        public _MembersVM() { }
        public _MembersVM(CharOrg charorg)
        {
            Name = charorg.Character.CharacterName;
            Blurb = charorg.BlurbOrgPage;
            Id = charorg.CharId;
            IsPlayer = true;
            DisplayOrder = null;
        }
        public _MembersVM(NpcOrg npcOrg)
        {
            Name = npcOrg.NPC.Name;
            Blurb = npcOrg.BlurbOrgPage;
            Id = npcOrg.NpcId;
            IsPlayer = false;
            DisplayOrder = npcOrg.MemberOrder;
        }
    }

    public class _EventOrgFullVM
    {
        public List<_EventsOrgVM> Holidays { get; set; }
        public List<_EventsOrgVM> PastEvents { get; set; }
    }

    public class _EventsOrgVM
    {
        public string Name { get; set; }
        public short Id { get; set; }
        public string Blurb { get; set; }
    }
}