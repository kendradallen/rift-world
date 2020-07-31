using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Runtime;


namespace RiftWorld.UI.MVC.Models
{
    #region testing work with Linking tables 


    public class AssociationWork
    {
        public short OrgId { get; set; }

        public Nullable<byte> Order { get; set; }

        //methods
        public bool Validate()
        {
            //should check if the orgId actually in db in theory. 
            //  done: handled in controller action as that is where all contact with the db is. 
            if (Order > 200)
            {
                return false;
            }
            return true;
        }
    }

    //public class AssociationWorkv2
    //{
    //    public Nullable<byte> Order { get; set; }
    //}


    public class AWSmol
    {
        public string Name { get; set; }

        public List<AssociationWork> Associations { get; set; }

        public AWSmol(string name, List<AssociationWork> associations)
        {
            Name = name;
            Associations = associations;
        }

        //methods
        public bool Validate()
        {
            if (Name.Length > 30 || String.IsNullOrEmpty(Name))
            {
                return false;
            }

            foreach (AssociationWork a in Associations)
            {
                if (!a.Validate())
                {
                    return false;
                }
            }

            return true;
        }
    }

    public class AssociationWork_Full
    {
        [Required(ErrorMessage = "ya need a name")]
        [StringLength(30)]
        public string Name { get; set; }

        public IEnumerable<SelectListItem> AssociationOptions { get; set; }

        public List<AssociationWork> Associations { get; set; }

        //Ctor
        public AssociationWork_Full()
        {
            AssociationOptions = new List<SelectListItem>();
        }

    }
    #endregion


    public class NpcCreateViewModel
    {
        #region Fields
        private string _alias;
        private string _apperanceText;
        private string _aboutText;
        #endregion

        #region Props
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        [UIHint("MultilineText")]
        [Display(Name = "Known Aliases")]
        public string Alias
        {
            get { return _alias; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
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
        [StringLength(150)]
        [UIHint("MultilineText")]
        public string Quote { get; set; }

        public string PortraitFileName { get; set; }

        [Display(Name = "Species")]
        public Nullable<byte> RaceId { get; set; }

        public string CrestFileName { get; set; }

        [Required]
        [StringLength(2000)]
        [UIHint("MultilineText")]
        [Display(Name = "Apperance")]
        [AllowHtml]
        public string ApperanceText
        {
            get { return _apperanceText; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _alias = "Nada";
                }
                else
                {
                    _apperanceText = value;
                }
            }

        }


        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "About")]
        [AllowHtml]
        public string AboutText
        {
            get { return _aboutText; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    _alias = "Nada";
                }
                else
                {
                    _aboutText = value;
                }
            }

        }


        [Display(Name = "Last Location")]
        public Nullable<short> LastLocationId { get; set; }

        public bool IsWorkInProgress { get; set; }

        //infos stuff
        [Required]
        [StringLength(100, ErrorMessage = "Blurb must be less than a 100 characters. We can talk about changing that if you don't like.")]
        public string Blurb { get; set; }



        #endregion

        #region Methods
        public bool Validate()
        {
            if (Name.Length > 30 || String.IsNullOrWhiteSpace(Name))
            {
                return false;
            }

            if (Alias.Length > 500)
            {
                return false;
            }

            if (Quote.Length > 150 || String.IsNullOrWhiteSpace(Quote))
            {
                return false;
            }

            if (ApperanceText.Length > 2000)
            {
                return false;
            }

            //blurb validation
            if (Blurb.Length > 100 || String.IsNullOrWhiteSpace(Blurb))
            {
                return false;
            }


            return true;
        }

        #endregion

    }


    /* 
     the three parts:
        1. the model that is sent with the options to choose from
        2. the model for the association table
        3. the model to attach to during post. (I'm not actually sure if that it would be strictly nessary but I feel far more comfortable given my struggle with binding to do it this way.
         */
    public class Npc_Org
    {
        #region Props
        public short OrgId { get; set; }
        public Nullable<byte> OrderOrg { get; set; }
        public Nullable<byte> OrderNpc { get; set; }

        [StringLength(50)]
        public string BlurbNpc { get; set; }

        [StringLength(50)]
        public string BlurbOrg { get; set; }

        #endregion

        #region Methods
        public bool Validate()
        {
            //should check if the orgId actually in db in theory. 

            if (BlurbNpc.Length > 50)
            {
                return false;
            }

            if (BlurbOrg.Length > 50)
            {
                return false;
            }

            return true;
        }
        #endregion
    }

    public class Npc_Class
    {
        #region Props
        public byte ClassId { get; set; }
        public Nullable<byte> ClassOrder { get; set; }
        #endregion


    }

    public class OrgVM //TODO - seriously, remaned this shit. It's terrible and was from testing
    {
        public string Name { get; set; }

        public string Blurb { get; set; }

        //public Nullable<byte> Order { get; set; }

    }

}
