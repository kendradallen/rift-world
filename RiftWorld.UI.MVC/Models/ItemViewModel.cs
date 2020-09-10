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
    public class ItemCreateVM
    {
        private string _propertyText;
        private string _descriptionText;
        private string _historyTest;

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        public string PictureFileName { get; set; }

        [StringLength(40, ErrorMessage = "If you are reading this Katherine, you may need to actually get me. Because I set the limit on characters for artist to 40. You just went over it.")]
        public string Artist { get; set; }

        [Display(Name = "Description")]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string DescriptionText
        {
            get { return _descriptionText; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _descriptionText = "Nada";
                }
                else
                {
                    _descriptionText = value;
                }
            }
        }

        //comment out [required] to allow empty strings to be submitted
        //this allows nothing to be entered into property and still save to the db (say you wanted to save an "empty" value for properties. All that is required is the Required to be commented out for this to activate properly 
        [UIHint("MultilineText")]
        [Display(Name = "Properties")]
        [AllowHtml]
        public string PropertyText
        {
            get { return _propertyText; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _propertyText = "Nada";
                }
                else
                {
                    _propertyText = value;
                }
            }
        }

        [UIHint("MultilineText")]
        [Display(Name = "History")]
        [AllowHtml]
        public string HistoryText
        {
            get { return _historyTest; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _historyTest = "Nada";
                }
                else
                {
                    _historyTest = value;
                }
            }
        }


        public bool IsPublished { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }

    }

    public class ItemEditPostVM
    {
        public short InfoId { get; set; }
        public short ItemId { get; set; }

        //may need to write this out in full and not shortcut it for validation
        //[Required]
        //public ItemCreateVM Item { get; set; }

        #region essentially the create VM 
        private string _propertyText;
        private string _descriptionText;
        private string _historyTest;

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        public string PictureFileName { get; set; }

        [StringLength(40, ErrorMessage = "If you are reading this Katherine, you may need to actually get me. Because I set the limit on characters for artist to 40. You just went over it.")]
        public string Artist { get; set; }

        [Display(Name = "Description")]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string DescriptionText
        {
            get { return _descriptionText; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _descriptionText = "Nada";
                }
                else
                {
                    _descriptionText = value;
                }
            }
        }

        //comment out [required] to allow empty strings to be submitted
        //this allows nothing to be entered into property and still save to the db (say you wanted to save an "empty" value for properties. All that is required is the Required to be commented out for this to activate properly 
        [UIHint("MultilineText")]
        [Display(Name = "Properties")]
        [AllowHtml]
        public string PropertyText
        {
            get { return _propertyText; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _propertyText = "Nada";
                }
                else
                {
                    _propertyText = value;
                }
            }
        }

        [UIHint("MultilineText")]
        [Display(Name = "History")]
        [AllowHtml]
        public string HistoryText
        {
            get { return _historyTest; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _historyTest = "Nada";
                }
                else
                {
                    _historyTest = value;
                }
            }
        }

        public bool IsPublished { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }

        #endregion


        //found a way around needing these. These would be cleaner than what I did but I am struggling too much right now to worry about it, when I have a way that works
        //public List<short> SelectedTags { get; set; }
        //public List<Tag> Tags { get; set; }

        public ItemEditPostVM() { }
        public ItemEditPostVM(Item item, Info info) 
        {
            InfoId = item.InfoId;
            ItemId = item.ItemId;
            Name = info.Name;
            PictureFileName = item.PictureFileName;
            DescriptionText = item.DescriptionText;
            PropertyText = item.PropertyText;
            HistoryText = item.HistoryText;
            IsPublished = info.IsPublished;
            Artist = item.Artist;
            Blurb = info.Blurb;
            IsSecret = info.IsSecret;
        }
    }

    public class ItemEditVM
    {
        public short InfoId { get; set; }
        public short ItemId { get; set; }

        //may need to write this out in full and not shortcut it for validation
        //[Required]
        //public ItemCreateVM Item { get; set; }

        #region essentially the create VM 
        private string _propertyText;
        private string _descriptionText;
        private string _historyTest;

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        public string PictureFileName { get; set; }

        [StringLength(40, ErrorMessage = "If you are reading this Katherine, you may need to actually get me. Because I set the limit on characters for artist to 40. You just went over it.")]
        public string Artist { get; set; }

        [Display(Name = "Description")]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string DescriptionText
        {
            get
            {
                if (_descriptionText == "Nada")
                {
                    return "";
                }
                else
                {
                    return _descriptionText;
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _descriptionText = "Nada";
                }
                else
                {
                    _descriptionText = value;
                }
            }
        }

        //comment out [required] to allow empty strings to be submitted
        //this allows nothing to be entered into property and still save to the db (say you wanted to save an "empty" value for properties. All that is required is the Required to be commented out for this to activate properly 
        [UIHint("MultilineText")]
        [Display(Name = "Properties")]
        [AllowHtml]
        public string PropertyText
        {
            get
            {
                if (_propertyText == "Nada")
                {
                    return "";
                }
                else
                {
                    return _propertyText;
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _propertyText = "Nada";
                }
                else
                {
                    _propertyText = value;
                }
            }
        }

        [UIHint("MultilineText")]
        [Display(Name = "History")]
        [AllowHtml]
        public string HistoryText
        {
            get
            {
                if (_historyTest == "Nada")
                {
                    return "";
                }
                else
                {
                    return _historyTest;
                }
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _historyTest = "Nada";
                }
                else
                {
                    _historyTest = value;
                }
            }
        }

        public bool IsPublished { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }

        #endregion


        //found a way around needing these. These would be cleaner than what I did but I am struggling too much right now to worry about it, when I have a way that works
        //public List<short> SelectedTags { get; set; }
        //public List<Tag> Tags { get; set; }

        public ItemEditVM() { }
        public ItemEditVM(Item item, Info info)
        {
            InfoId = item.InfoId;
            ItemId = item.ItemId;
            Name = info.Name;
            PictureFileName = item.PictureFileName;
            DescriptionText = item.DescriptionText;
            PropertyText = item.PropertyText;
            HistoryText = item.HistoryText;
            IsPublished = info.IsPublished;
            Artist = item.Artist;
            Blurb = info.Blurb;
            IsSecret = info.IsSecret;
        }
        public ItemEditVM(ItemEditPostVM item)
        {
            InfoId = item.InfoId;
            ItemId = item.ItemId;
            Name = item.Name;
            Blurb = item.Blurb;
            PictureFileName = item.PictureFileName;
            DescriptionText = item.DescriptionText;
            PropertyText = item.PropertyText;
            HistoryText = item.HistoryText;
            IsPublished = item.IsPublished;
            Artist = item.Artist;
            IsSecret = item.IsSecret;
        }
    }

}