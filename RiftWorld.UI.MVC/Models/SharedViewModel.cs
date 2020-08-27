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
    public class SecretCreateVM
    {
        [Display(Name = "Is About")]
        public short IsAboutId { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Content")]
        [AllowHtml]
        public string TheContent { get; set; }

    }

    public class SecretEditVM
    {
        public short SecretId { get; set; }

        [Display(Name = "Is About")]
        public short IsAboutId { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Content")]
        [AllowHtml]
        public string TheContent { get; set; }

        public SecretEditVM()
        {

        }
        public SecretEditVM(Secret secret)
        {
            SecretId = secret.SecretId;
            IsAboutId = secret.IsAboutId;
            TheContent = secret.TheContent;
        }
    }

    public class StoryCreateVM
    {
        [Display(Name = "Is About")]
        public short IsAboutId { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DateTold { get; set; }

        [StringLength(50, ErrorMessage = " ")]
        [DisplayFormat(NullDisplayText = "an anonymous source")]
        [Display(Name = "Commissioned By")]
        public string CommissionedBy { get; set; }

        public bool IsCannon { get; set; }

        [Required(ErrorMessage = "...I have no words. The story content was empty")]
        [UIHint("MultilineText")]
        [Display(Name = "Content")]
        [AllowHtml]
        public string TheContent { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = " ")]
        public string Title { get; set; }
    }

    public class StoryEditVM
    {
        public short StoryId { get; set; }

        [Display(Name = "Is About")]
        public short IsAboutId { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DateTold { get; set; }

        [StringLength(50, ErrorMessage = " ")]
        [DisplayFormat(NullDisplayText = "an anonymous source")]
        [Display(Name = "Commissioned By")]
        public string CommissionedBy { get; set; }

        public bool IsCannon { get; set; }

        [Required(ErrorMessage = "...I have no words. The story content was empty")]
        [UIHint("MultilineText")]
        [Display(Name = "Content")]
        [AllowHtml]
        public string TheContent { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = " ")]
        public string Title { get; set; }

        public StoryEditVM() { }
        public StoryEditVM(Story story)
        {
            StoryId = story.StoryId;
            IsAboutId = story.IsAboutId;
            DateTold = story.DateTold;
            CommissionedBy = story.CommissionedBy;
            IsCannon = story.IsCannon;
            TheContent = story.TheContent;
            Title = story.Title;
        }
    }

    public class RumorCreateVM
    {
        public short RumorOfId { get; set; }

        public short? AuthorId { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        [StringLength(300)]
        [UIHint("MultilineText")]
        [Display(Name = "Content")]
        public string RumorText { get; set; }

    }

    //public class GetSecretHubVM
    //{
    //    private RiftWorldEntities db = new RiftWorldEntities();

    //    public List<SecretComplete_PlayerVM> Secrets { get; set; }

    //    public GetSecretHubVM(short characterId)
    //    {
    //        var selfTags = db.CharSecrets.Where(x => x.CharId == characterId).Select(x => x.SecretId);
    //        Secrets = (from t in db.SecretSecretTags
    //                    where selfTags.Contains(t.SecretTagId)
    //                    select new SecretComplete_PlayerVM(t.SecretId, characterId)
    //                    ).ToList()
    //                    ;
    //    }
    //}

    public class SecretCompleteVM
    {
        public Secret Secret { get; set; }
        public List<SecretTag> Tags { get; set; }
        public List<Character> CharactersKnow { get; set; }
    }

    public class SecretComplete_PlayerVM
    {
        //private RiftWorldEntities db = new RiftWorldEntities();

        public Secret Secret { get; set; }
        public List<SecretTag> Tags { get; set; }

        //public SecretComplete_PlayerVM(short secretId, short characterId)
        //{
        //    Secret = db.Secrets.Find(secretId);

        //    var selfTags = db.CharSecrets.Where(x => x.CharId == characterId).Select(x => x.SecretId);
        //    Tags = (from s in db.SecretSecretTags
        //            join st in db.SecretTags on s.SecretTagId equals st.SecretTagId
        //            where s.SecretId == secretId && selfTags.Contains(s.SecretTagId)
        //            select st)
        //           .ToList()
        //           ;
        //}
    }
}