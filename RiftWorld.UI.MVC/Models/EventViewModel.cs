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
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name ="Historical")]
        public bool IsHistory { get; set; }

        [Required(ErrorMessage ="You need a day")]
        [Range(1, 40, ErrorMessage = "Between {1} & {2} please")]
        public byte DateDay { get; set; }

        [Required(ErrorMessage = "You need a month")]
        [Range(1, 15, ErrorMessage = "Between {1} & {2} please")]
        public byte DateMonth { get; set; }

        [Range(0, 9999, ErrorMessage = "Between {1} & {2:n0} please")]
        public Nullable<short> DateYear { get; set; }

        [StringLength(15, ErrorMessage = " ")]
        public string DateEra { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Normal Participants")]
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
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Historical")]
        public bool IsHistory { get; set; }

        [Required(ErrorMessage = "You need a day")]
        [Range(1, 40, ErrorMessage = "Between {1} & {2} please")]
        public byte DateDay { get; set; }

        [Required(ErrorMessage = "You need a month")]
        [Range(1, 15, ErrorMessage = "Between {1} & {2} please")]
        public byte DateMonth { get; set; }

        [Range(0, 9999, ErrorMessage = "Between {1} & {2:n0} please")]
        public Nullable<short> DateYear { get; set; }

        [StringLength(15, ErrorMessage = " ")]
        public string DateEra { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Normal Participants")]
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

        #region ctors
        public EventEditPostVM() { }
        public EventEditPostVM(Event taevent, string blurb)
        {
            InfoId = taevent.InfoId;
            EventId = taevent.EventId;
            Name = taevent.Name;
            IsHistory = taevent.IsHistory;
            AboutText = taevent.AboutText;
            NormalParticipants = taevent.NormalParticipants;
            IsPublished = taevent.IsPublished;
            DateDay = taevent.DateDay;
            DateMonth = taevent.DateMonth;
            DateYear = taevent.DateYear;
            DateEra = taevent.DateEra;
            Blurb = blurb;
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
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Historical")]
        public bool IsHistory { get; set; }

        [Required(ErrorMessage = "You need a day")]
        [Range(1, 40, ErrorMessage = "Between {1} & {2} please")]
        public byte DateDay { get; set; }

        [Required(ErrorMessage = "You need a month")]
        [Range(1, 15, ErrorMessage = "Between {1} & {2} please")]
        public byte DateMonth { get; set; }

        [Range(0, 9999, ErrorMessage = "Between {1} & {2:n0} please")]
        public Nullable<short> DateYear { get; set; }

        [StringLength(15, ErrorMessage = " ")]
        public string DateEra { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Normal Participants")]
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

        #region ctors
        public EventEditVM() { }
        public EventEditVM(Event taevent, string blurb)
        {
            InfoId = taevent.InfoId;
            EventId = taevent.EventId;
            Name = taevent.Name;
            IsHistory = taevent.IsHistory;
            AboutText = taevent.AboutText;
            NormalParticipants = taevent.NormalParticipants;
            IsPublished = taevent.IsPublished;
            DateDay = taevent.DateDay;
            DateMonth = taevent.DateMonth;
            DateYear = taevent.DateYear;
            DateEra = taevent.DateEra;
            Blurb = blurb;
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
            DateDay = taevent.DateDay;
            DateMonth = taevent.DateMonth;
            DateYear = taevent.DateYear;
            DateEra = taevent.DateEra;
            Blurb = taevent.Blurb;
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
}