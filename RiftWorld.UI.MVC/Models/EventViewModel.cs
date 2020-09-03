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
    public class EventCreateVM
    {
        #region Fields
        private string _aboutText;
        private string _normalParticipants;
        #endregion

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name ="Historical")]
        public bool IsHistory { get; set; }

        [Range(1, 12, ErrorMessage = "Between {1} & {2} please")]
        public Nullable<byte> DateMonth { get; set; }

        [Range(1, 4, ErrorMessage = "Between {1} & {2} please")]
        public Nullable<byte> DateSeason { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Normal Participants")]
        [AllowHtml]
        public string NormalParticipants
        {
            get { return _normalParticipants; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _normalParticipants = "Nada";
                }
                else
                {
                    _normalParticipants = value;
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

        public bool IsPublished { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }

    }

    public class EventEditPostVM
    {
        #region Fields
        private string _aboutText;
        private string _normalParticipants;
        #endregion

        public short InfoId { get; set; }
        public short EventId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Historical")]
        public bool IsHistory { get; set; }

        [Range(1, 12, ErrorMessage = "Between {1} & {2} please")]
        public Nullable<byte> DateMonth { get; set; }

        [Range(1, 4, ErrorMessage = "Between {1} & {2} please")]
        public Nullable<byte> DateSeason { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Normal Participants")]
        [AllowHtml]
        public string NormalParticipants
        {
            get { return _normalParticipants; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _normalParticipants = "Nada";
                }
                else
                {
                    _normalParticipants = value;
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

        public bool IsPublished { get; set; }

        [Display(Name ="Secret?")]
        public bool IsSecret { get; set; }

        #region ctors
        public EventEditPostVM() { }
        public EventEditPostVM(Event taevent, Info info)
        {
            InfoId = taevent.InfoId;
            EventId = taevent.EventId;
            Name = taevent.Name;
            IsHistory = taevent.IsHistory;
            AboutText = taevent.AboutText;
            NormalParticipants = taevent.NormalParticipants;
            IsPublished = taevent.IsPublished;
            DateMonth = taevent.DateMonth;
            DateSeason = taevent.DateSeason;
            Blurb = info.Blurb;
            IsSecret = info.IsSecret;
        }
        #endregion
    }

    public class EventEditVM
    {
        #region Fields
        private string _aboutText;
        private string _normalParticipants;
        #endregion

        public short InfoId { get; set; }
        public short EventId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Historical")]
        public bool IsHistory { get; set; }

        [Range(1, 12, ErrorMessage = "Between {1} & {2} please")]
        public Nullable<byte> DateMonth { get; set; }

        [Range(1, 4, ErrorMessage = "Between {1} & {2} please")]
        public Nullable<byte> DateSeason { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Normal Participants")]
        [AllowHtml]
        public string NormalParticipants
        {
            get
            {
                if (_normalParticipants == "Nada")
                {
                    return "";
                }
                else
                {
                    return _normalParticipants;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _normalParticipants = "Nada";
                }
                else
                {
                    _normalParticipants = value;
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

        public bool IsPublished { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }


        #region ctors
        public EventEditVM() { }
        public EventEditVM(Event taevent, Info info)
        {
            InfoId = taevent.InfoId;
            EventId = taevent.EventId;
            Name = taevent.Name;
            IsHistory = taevent.IsHistory;
            AboutText = taevent.AboutText;
            NormalParticipants = taevent.NormalParticipants;
            IsPublished = taevent.IsPublished;
            DateMonth = taevent.DateMonth;
            DateSeason = taevent.DateSeason;
            Blurb = info.Blurb;
            IsSecret = info.IsSecret;
        }
        public EventEditVM(EventEditPostVM taevent)
        {
            InfoId = taevent.InfoId;
            EventId = taevent.EventId;
            Name = taevent.Name;
            IsHistory = taevent.IsHistory;
            AboutText = taevent.AboutText;
            NormalParticipants = taevent.NormalParticipants;
            IsPublished = taevent.IsPublished;
            DateMonth = taevent.DateMonth;
            DateSeason = taevent.DateSeason;
            Blurb = taevent.Blurb;
            IsSecret = taevent.IsSecret;
        }
        #endregion
    }

    public class AssoEventVM
    {
        public short InfoId { get; set; }
        public short EventId { get; set; }
        public string Submit { get; set; }
        public string Name { get; set; }

        public List<AssoLocale_Event> AssoLocales { get; set; }
        public List<AssoOrg_Event> AssoOrgs { get; set; }
    }

    public class AssoLocale_Event
    {
        public short LocaleId { get; set; }

        public AssoLocale_Event() { }
        public AssoLocale_Event(LocaleEvent localeEvent)
        {
            LocaleId = localeEvent.LocaleId;
        }
    }

    public class AssoOrg_Event
    {
        public short OrgId { get; set; }

        [StringLength(100, ErrorMessage = " ")]
        public string Blurb { get; set; }

        public AssoOrg_Event() { }
        public AssoOrg_Event(OrgEvent orgEvent)
        {
            OrgId = orgEvent.OrgId;
            Blurb = orgEvent.Blurb;
        }
    }

    public class _EventOrgsVM
    {
        public string Name { get; set; }
        public short Id { get; set; }
    }

    public class _EventLocalesVM
    {
        public string Name { get; set; }
        public short Id { get; set; }
    }
}