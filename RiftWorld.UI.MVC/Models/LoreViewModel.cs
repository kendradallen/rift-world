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
    public class LoreCreateVM
    {

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Main")]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string TheContent { get; set; }

        public bool IsPublished { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }

    }

    public class LoreEditVM
    {
        public short InfoId { get; set; }
        public short LoreId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = " ")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Main")]
        [UIHint("MultilineText")]
        [AllowHtml]
        public string TheContent { get; set; }

        public bool IsPublished { get; set; }

        [Required]
        [StringLength(350, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        [Display(Name = "Secret?")]
        public bool IsSecret { get; set; }

        #region ctor
        public LoreEditVM(){ }
        public LoreEditVM(Lore lore, Info info)
        {
            InfoId = lore.InfoId;
            LoreId = lore.LoreId;
            Name = lore.Name;
            Blurb = info.Blurb;
            TheContent = lore.TheContent;
            IsPublished = lore.IsPublished;
            IsSecret = info.IsSecret;
        }
        #endregion
    }
}