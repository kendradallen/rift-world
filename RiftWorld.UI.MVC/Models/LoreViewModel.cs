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
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

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
        [StringLength(100, ErrorMessage = " ")]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        #region ctor
        public LoreEditVM(){ }
        public LoreEditVM(Lore lore, string blurb)
        {
            InfoId = lore.InfoId;
            LoreId = lore.LoreId;
            Name = lore.Name;
            Blurb = blurb;
            TheContent = lore.TheContent;
            IsPublished = lore.IsPublished;
        }
        #endregion
    }
}