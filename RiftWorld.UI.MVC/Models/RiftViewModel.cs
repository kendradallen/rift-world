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
    public class RiftCreateVM
    {
        #region Fields
        private string _environment;
        private string _hazards;
        #endregion

        [Required]
        [StringLength(50, ErrorMessage =" ")]
        public string Nickname { get; set; }

        [Required]
        [StringLength(300, ErrorMessage =" ")]
        [UIHint("MultilineText")]
        public string Location { get; set; }

        //[Required]
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

        //[Required]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string Hazards
        {
            get { return _hazards; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _hazards = "Nada";
                }
                else
                {
                    _hazards = value;
                }
            }
        }

        public bool IsPublished { get; set; }

        [Required]
        [StringLength(100, ErrorMessage =" ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

    }

    public class RiftEditPostVM
    {
        #region prop
        public short InfoId { get; set; }
        public short RiftId { get; set; }

        #region RiftCreate
        #region Fields
        private string _environment;
        private string _hazards;
        #endregion

        [Required]
        [StringLength(50, ErrorMessage =" ")]
        public string Nickname { get; set; }

        [Required]
        [StringLength(300, ErrorMessage =" ")]
        [UIHint("MultilineText")]
        public string Location { get; set; }


        //[Required]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string Environment
        {
            get{ return _environment; }
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

        //[Required]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string Hazards
        {
            get { return _hazards; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _hazards = "Nada";
                }
                else
                {
                    _hazards = value;
                }
            }
        }

        public bool IsPublished { get; set; }

        [Required]
        [StringLength(100, ErrorMessage =" ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }
        #endregion
        #endregion

        #region ctors
        public RiftEditPostVM() { }
        public RiftEditPostVM(Rift rift, string blurb)
        {
            InfoId = rift.InfoId;
            RiftId = rift.RiftId;
            Nickname = rift.Nickname;
            Location = rift.Location;
            Environment = rift.Environment;
            Hazards = rift.Hazards;
            IsPublished = rift.IsPublished;
            Blurb = blurb;
        }
        #endregion
    }

    public class RiftEditVM
    {
        #region prop
        public short InfoId { get; set; }
        public short RiftId { get; set; }

        #region RiftCreate
        #region Fields
        private string _environment;
        private string _hazards;
        #endregion

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Nickname { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Location { get; set; }


        //[Required]
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

        //[Required]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string Hazards
        {
            get
            {
                if (_hazards == "Nada")
                {
                    return "";
                }
                else
                {
                    return _hazards;
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _hazards = "Nada";
                }
                else
                {
                    _hazards = value;
                }
            }
        }

        public bool IsPublished { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }
        #endregion
        #endregion

        #region ctors
        public RiftEditVM() { }
        public RiftEditVM(Rift rift, string blurb)
        {
            InfoId = rift.InfoId;
            RiftId = rift.RiftId;
            Nickname = rift.Nickname;
            Location = rift.Location;
            Environment = rift.Environment;
            Hazards = rift.Hazards;
            IsPublished = rift.IsPublished;
            Blurb = blurb;
        }
        public RiftEditVM(RiftEditPostVM rift)
        {
            InfoId = rift.InfoId;
            RiftId = rift.RiftId;
            Nickname = rift.Nickname;
            Location = rift.Location;
            Environment = rift.Environment;
            Hazards = rift.Hazards;
            IsPublished = rift.IsPublished;
            Blurb = rift.Blurb;
        }
        #endregion
    }

    public class AssoRiftVM
    {
        public short InfoId { get; set; }
        public short RiftId { get; set; }
        public string Submit { get; set; }
        public string Name { get; set; }

        public List<AssoVar> Assos { get; set; }
    }
    public class AssoVar
    {
        public byte RaceId { get; set; }
        public Nullable<byte> RaceOrder { get; set; }

        public AssoVar(){}
        public AssoVar(VarietyOfInhabitant variety)
        {
            RaceId = variety.RaceId;
            RaceOrder = variety.RaceOrder;
        }
    }
    public class ComboCreate
    {
        #region RiftCreate
        [StringLength(50, ErrorMessage = " ")]
        public string Nickname { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Location { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string Environment { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string Hazards { get; set; }

        public bool IsPublished { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }
        #endregion

        public List<AssoVar> Assos { get; set; }
    }
}