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
    public class LocaleCreateVM
    {
        #region Fields
        private string _appointed;
        private string _environment;
        private string _about;
        private string _avgLifestyle;
        #endregion

        [Required]
        [StringLength(50, ErrorMessage =" ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Level of Locale")]
        public byte LevelOfLocaleId { get; set; }

        [Display(Name = "Region")]
        public Nullable<short> RegionId { get; set; }

        [Display(Name = "Nearest City")]
        public Nullable<short> ClosestCityId { get; set; }

        [Display(Name = "Council Delegate")]
        public Nullable<short> CouncilDelegateId { get; set; }

        [StringLength(8000)]
        [UIHint("MultilineText")]
        [AllowHtml] 
        public string Appointed
        {
            get { return _appointed; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _appointed = "Nada";
                }
                else
                {
                    _appointed = value;
                }
            }
        }


        [UIHint("MultilineText")]
        [AllowHtml]
        public string Environment
        {
            get { return _environment; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _environment = "Nada";
                }
                else
                {
                    _environment = value;
                }
            }
        }


        [UIHint("MultilineText")]
        [AllowHtml]
        public string About
        {
            get { return _about; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _about = "Nada";
                }
                else
                {
                    _about = value;
                }
            }
        }


        public bool IsPublished { get; set; }

        [StringLength(3000)]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string AvgLifestyle
        {
            get { return _avgLifestyle; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _avgLifestyle = "Nada";
                }
                else
                {
                    _avgLifestyle = value;
                }
            }
        }


        [Required]
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

    }


    public class LocaleEditPostVM
    {
        #region Fields
        private string _appointed;
        private string _environment;
        private string _about;
        private string _avgLifestyle;
        #endregion

        public short InfoId { get; set; }
        public short LocaleId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Level of Locale")]
        public byte LevelOfLocaleId { get; set; }

        [Display(Name = "Region")]
        public Nullable<short> RegionId { get; set; }

        [Display(Name = "Nearest City")]
        public Nullable<short> ClosestCityId { get; set; }

        [Display(Name = "Council Delegate")]
        public Nullable<short> CouncilDelegateId { get; set; }

        [StringLength(8000)]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string Appointed
        {
            get { return _appointed; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _appointed = "Nada";
                }
                else
                {
                    _appointed = value;
                }
            }
        }


        [UIHint("MultilineText")]
        [AllowHtml]
        public string Environment
        {
            get { return _environment; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _environment = "Nada";
                }
                else
                {
                    _environment = value;
                }
            }
        }


        [UIHint("MultilineText")]
        [AllowHtml]
        public string About
        {
            get { return _about; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _about = "Nada";
                }
                else
                {
                    _about = value;
                }
            }
        }

        public bool IsPublished { get; set; }

        [StringLength(3000)]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string AvgLifestyle
        {
            get { return _avgLifestyle; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _avgLifestyle = "Nada";
                }
                else
                {
                    _avgLifestyle = value;
                }
            }
        }

        [Required]
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        #region ctors
        public LocaleEditPostVM(){ }
        public LocaleEditPostVM(Locale locale, string blurb)
        {
            InfoId = locale.InfoId;
            LocaleId = locale.LocaleId;
            Name = locale.Name;
            LevelOfLocaleId = locale.LevelOfLocaleId;
            RegionId = locale.RegionId;
            ClosestCityId = locale.ClosestCityId;
            CouncilDelegateId = locale.CouncilDelegateId;
            Appointed = locale.Appointed;
            Environment = locale.Environment;
            About = locale.About;
            IsPublished = locale.IsPublished;
            AvgLifestyle = locale.AvgLifestyle;
            Blurb = blurb;
        }
        #endregion
    }

    public class LocaleEditVM
    {
        #region Fields
        private string _appointed;
        private string _environment;
        private string _about;
        private string _avgLifestyle;
        #endregion

        public short InfoId { get; set; }
        public short LocaleId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Level of Locale")]
        public byte LevelOfLocaleId { get; set; }

        [Display(Name = "Region")]
        public Nullable<short> RegionId { get; set; }

        [Display(Name = "Nearest City")]
        public Nullable<short> ClosestCityId { get; set; }

        [Display(Name = "Council Delegate")]
        public Nullable<short> CouncilDelegateId { get; set; }

        [StringLength(8000)]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string Appointed
        {
            get
            {
                if (_appointed == "Nada")
                {
                    return "";
                }
                else
                {
                    return _appointed;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _appointed = "Nada";
                }
                else
                {
                    _appointed = value;
                }
            }
        }


        [UIHint("MultilineText")]
        [AllowHtml]
        public string Environment
        {
            get
            {
                if (_environment == "Nada")
                {
                    return "";
                }
                else
                {
                    return _environment;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _environment = "Nada";
                }
                else
                {
                    _environment = value;
                }
            }
        }


        [UIHint("MultilineText")]
        [AllowHtml]
        public string About
        {
            get
            {
                if (_about == "Nada")
                {
                    return "";
                }
                else
                {
                    return _about;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _about = "Nada";
                }
                else
                {
                    _about = value;
                }
            }
        }

        public bool IsPublished { get; set; }

        [StringLength(3000)]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string AvgLifestyle
        {
            get
            {
                if (_avgLifestyle == "Nada")
                {
                    return "";
                }
                else
                {
                    return _avgLifestyle;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _avgLifestyle = "Nada";
                }
                else
                {
                    _avgLifestyle = value;
                }
            }
        }

        [Required]
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        #region ctors
        public LocaleEditVM() { }
        public LocaleEditVM(Locale locale, string blurb)
        {
            InfoId = locale.InfoId;
            LocaleId = locale.LocaleId;
            Name = locale.Name;
            LevelOfLocaleId = locale.LevelOfLocaleId;
            RegionId = locale.RegionId;
            ClosestCityId = locale.ClosestCityId;
            CouncilDelegateId = locale.CouncilDelegateId;
            Appointed = locale.Appointed;
            Environment = locale.Environment;
            About = locale.About;
            IsPublished = locale.IsPublished;
            AvgLifestyle = locale.AvgLifestyle;
            Blurb = blurb;
        }
        public LocaleEditVM(LocaleEditPostVM locale)
        {
            InfoId = locale.InfoId;
            LocaleId = locale.LocaleId;
            Name = locale.Name;
            LevelOfLocaleId = locale.LevelOfLocaleId;
            RegionId = locale.RegionId;
            ClosestCityId = locale.ClosestCityId;
            CouncilDelegateId = locale.CouncilDelegateId;
            Appointed = locale.Appointed;
            Environment = locale.Environment;
            About = locale.About;
            IsPublished = locale.IsPublished;
            AvgLifestyle = locale.AvgLifestyle;
            Blurb = locale.Blurb;
        }
        #endregion
    }


    public class AssoLocaleVM
    {
        public short InfoId { get; set; }
        public short LocaleId { get; set; }
        public string Submit { get; set; }
        public string Name { get; set; }

        public List<AssoMaj> AssoMajs { get; set; }
        public List<AssoEvent_Locale> AssoEvents { get; set; }
    }

    public class AssoMaj
    {
        public byte RaceId { get; set; }
        public bool IsFirst { get; set; }

        public AssoMaj() { }
        public AssoMaj(Majority majority)
        {
            RaceId = majority.RaceId;
            IsFirst = majority.IsFirst;
        }
    }

    public class AssoEvent_Locale
    {
        public short EventId { get; set; }

        public AssoEvent_Locale() { }
        public AssoEvent_Locale(LocaleEvent localeEvent)
        {
            EventId = localeEvent.EventId;
        }
    }
}