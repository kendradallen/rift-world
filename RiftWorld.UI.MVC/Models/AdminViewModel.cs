using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RiftWorld.UI.MVC.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }

    }

    public class EditUserViewModel2
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Discord Name")]
        [StringLength(32)]
        public string DiscordName { get; set; }

        [Required]
        [Range(0000, 9999)]
        [Display(Name = "Discord Discriminator")]
        [DisplayFormat(DataFormatString = "{0:D4}")]
        public short DiscordDiscriminator { get; set; }

        public string ConsentFileName { get; set; }

        public Nullable<short> CurrentCharacterId { get; set; }

        public bool IsApproved { get; set; }
    }

    public class EditUserViewModel3
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Discord Name")]
        [StringLength(32)]
        public string DiscordName { get; set; }

        [Required]
        [Range(0000, 9999)]
        [Display(Name = "Discord Discriminator")]
        [DisplayFormat(DataFormatString ="{0:D4}")]
        public short DiscordDiscriminator { get; set; }

        [Display(Name = "Consent File")]
        public string ConsentFileName { get; set; }

        [Display(Name ="Current Character")]
        public Nullable<short> CurrentCharacterId { get; set; }

        public bool IsApproved { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }

    }

    public class UsersRolesViewModel
    {
        public List<ApplicationUser> Users { set; get; }
        public List<IdentityRole> Roles { set; get; }
    }
}