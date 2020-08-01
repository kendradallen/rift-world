
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace RiftWorld.DATA.EF
{
    #region Character Metadata
    public class CharacterMetaData
    {
        //public short CharacterId { get; set; }
        [Required]
        [Display(Name = "Player")]
        public string PlayerId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(25)]
        public string CharacterName { get; set; }

        [Required]
        [Display(Name = "Species")]
        public byte RaceId { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public byte GenderId { get; set; }

        public string PortraitFileName { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [StringLength(8000)]
        public string About { get; set; }

        [Required] //may need to be removed due to weirdness during inital creation
        [Display(Name = "Current Location")]
        public short CurrentLocationId { get; set; }

        [Required]
        [Display(Name = "Tier")]
        public byte TierId { get; set; }

        public bool IsRetired { get; set; }

        public bool IsApproved { get; set; }
    }

    [MetadataType(typeof(CharacterMetaData))]
    public partial class Character { }

    #endregion

    #region CharOrg Metadata
    public class CharOrgMetaData
    {

        //public short CharOrgsId { get; set; }

        //public short CharId { get; set; }

        //public short OrgId { get; set; }
        [StringLength(50)]
        public string BlurbOrgPage { get; set; }

        public bool IsPublic { get; set; }

        public bool IsCurrent { get; set; }

        public bool Proposed { get; set; }

    }

    [MetadataType(typeof(CharOrgMetaData))]
    public partial class CharOrg { }

    #endregion

    #region CharSecret Metadata
    public class CharSecretMetaData
    {

    }

    [MetadataType(typeof(CharSecretMetaData))]
    public partial class CharSecret { }

    #endregion

    #region Class Metadata
    public class ClassMetaData
    {
        //public byte ClassId { get; set; }
        [Required(ErrorMessage = "Katherine, seriously. If you're going to make a new class you need to actually name it.")]
        [StringLength(15, ErrorMessage = "Too long. You have 15 characters; try again.")]
        public string ClassName { get; set; }
        public bool IsPlayerEnabled { get; set; }

    }

    [MetadataType(typeof(ClassMetaData))]
    public partial class Class { }

    #endregion

    #region  ClassCharacter Metadata
    //public class ClassCharacterMetaData
    //{

    //}

    //[MetadataType(typeof(ClassCharacterMetaData))]
    //public partial class ClassCharacter { }

    #endregion

    #region ClassNPC Metadata
    //public class ClassNPCMetaData
    //{

    //}

    //[MetadataType(typeof(ClassNPCMetaData))]
    //public partial class ClassNPC { }

    #endregion

    #region Event Metadata
    public class EventMetaData
    {

        //public short EventId { get; set; }

        //may need to not have this commented out; unsure --- edit: until I'm ready to experiment, I'm leaving it in
        public short InfoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsHistory { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime Date { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "About")]
        public string AboutText { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Normal Participants")]
        public string NormalParticipants { get; set; }

        public bool IsWorkInProgress { get; set; }

    }

    [MetadataType(typeof(EventMetaData))]
    public partial class Event { }

    #endregion

    #region Gender Metadata
    public class GenderMetaData
    {
        //public byte GenderId { get; set; }
        [Required(ErrorMessage = "No matter how much you want it, I can't let you make a nameless gender.")]
        [StringLength(15, ErrorMessage = "Do me a favor and please make it shorter. I only planned for gender names of 15 characters.")]
        [Display(Name = "Gender")]
        public string GenderName { get; set; }

        [StringLength(15)]
        [Display(Name = "Icon")]
        public string IconName { get; set; }

    }

    [MetadataType(typeof(GenderMetaData))]
    public partial class Gender { }

    #endregion

    #region Info Metadata
    public class InfoMetaData
    {

        //public short InfoId { get; set; }

        [Required]
        public byte InfoTypeId { get; set; }

        public Nullable<short> IdWithinType { get; set; }

        [UIHint("MultilineText")]
        [Required]
        [StringLength(100)]
        public string Blurb { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsPublished { get; set; }

    }

    [MetadataType(typeof(InfoMetaData))]
    public partial class Info { }

    #endregion

    #region InfoTag Metadata
    //public class InfoTagMetaData
    //{

    //}

    //[MetadataType(typeof(InfoTagMetaData))]
    //public partial class InfoTag { }

    #endregion

    #region InfoType Metadata
    public class InfoTypeMetaData
    {
        //public byte InfoTypeId { get; set; }
        [Required(ErrorMessage = "I have so many questions. HOW ARE YOU HERE?! I sure as heck didn't make this an editable as this is really a new version kind of thing.")]
        [StringLength(15)]
        public string TypeName { get; set; }

    }

    [MetadataType(typeof(InfoTypeMetaData))]
    public partial class InfoType { }

    #endregion

    #region Item Metadata
    public class ItemMetadata
    {

        //public short ItemId { get; set; }

        public short InfoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        [UIHint("MultilineText")]
        public string Blurb { get; set; }

        public string PictureFileName { get; set; }

        [Required]
        [Display(Name = "Description")]
        [UIHint("MultilineText")]
        public string DescriptionText { get; set; }

        //unsure about whether or not to use commented out stuff. If it does work, most strings will be using this (as I didn't make columns nullable for space purposes)
        //TODO experiment with below to see if it works for database
        //[Required(AllowEmptyStrings =true)]
        //[DisplayFormat(ConvertEmptyStringToNull =false)]
        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Properties")]
        public string PropertyText { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "History")]
        public string HistoryText { get; set; }

        public bool IsWorkInProgress { get; set; }

    }

    [MetadataType(typeof(ItemMetadata))]
    public partial class Item { }
    #endregion

    #region Journal Metadata
    public class JournalMetadata
    {

        //public int JournalId { get; set; }

        public short CharacterId { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Published: ")]
        public System.DateTime OocDateWritten { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public Nullable<System.DateTime> ICDateWritten { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Journal Entry")]
        public string TheContent { get; set; }

        public bool IsApproved { get; set; }

    }

    [MetadataType(typeof(JournalMetadata))]
    public partial class Journal { }
    #endregion

    #region Locale Metadata
    public class LocaleMetadata
    {
        //public short LocaleId { get; set; }

        public short InfoId { get; set; }

        [Required]
        [StringLength(50)]
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

        [Required]
        [StringLength(8000)]
        [UIHint("MultilineText")]
        public string Appointed { get; set; }

        [Required]
        [UIHint("MultilineText")]
        public string Environment { get; set; }

        [Required]
        [UIHint("MultilineText")]
        public string About { get; set; }

        public bool IsWorkInProgress { get; set; }

        [Required]
        [StringLength(3000)]
        [UIHint("MultilineText")]
        public string AvgLifestyle { get; set; }

    }

    [MetadataType(typeof(LocaleMetadata))]
    public partial class Locale { }
    #endregion

    #region LocaleEvent Metadata
    //public class LocaleEventMetadata
    //{

    //}

    //[MetadataType(typeof(LocaleEventMetadata))]
    //public partial class LocaleEvent { }
    #endregion

    #region LocaleLevel Metadata
    public class LocaleLevelMetadata
    {
        //public byte LocaleLevelId { get; set; }
        [Required]
        [Display(Name = "Locale Level")]
        [StringLength(15)]
        public string LocaleName { get; set; }
    }

    [MetadataType(typeof(LocaleLevelMetadata))]
    public partial class LocaleLevel { }
    #endregion

    #region Lore Metadata
    public class LoreMetadata
    {
        //public short LoreId { get; set; }

        public short InfoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Main")]
        [UIHint("MultilineText")]
        public string TheContent { get; set; }

        public bool IsWorkInProgress { get; set; }

    }

    [MetadataType(typeof(LoreMetadata))]
    public partial class Lore { }
    #endregion

    #region Majority Metadata
    //public class MajorityMetadata
    //{

    //}

    //[MetadataType(typeof(MajorityMetadata))]
    //public partial class Majority { }
    #endregion

    #region NPC Metadata
    public class NPCMetadata
    {
        //public short NpcId { get; set; }

        public short InfoId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        [UIHint("MultilineText")]
        [Display(Name = "Known Aliases")]
        public string Alias { get; set; }

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
        public string ApperanceText { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "About")]
        public string AboutText { get; set; }

        [Display(Name = "Last Location")]
        public Nullable<short> LastLocationId { get; set; }

        public bool IsWorkInProgress { get; set; }

    }

    [MetadataType(typeof(NPCMetadata))]
    public partial class NPC { }
    #endregion

    #region NpcOrg Metadata
    public class NpcOrgMetadata
    {
        //public short NpcOrgId { get; set; }

        public short NpcId { get; set; }

        public short OrgId { get; set; }

        [Display(Name = "Priority of org display on NPC's page")]
        public Nullable<byte> OrgOrder { get; set; }

        [StringLength(50)]
        [Display(Name = "Blurb for NPC's page about postion in org")]
        public string BlurbNpcPage { get; set; }

        [StringLength(50)]
        [Display(Name = "Blurb for org's page about NPC's role")]
        public string BlurbOrgPage { get; set; }

        public bool IsCurrent { get; set; }

        [Display(Name = "Priority of NPC display on org's page")]
        public Nullable<byte> MemberOrder { get; set; }

    }

    [MetadataType(typeof(NpcOrgMetadata))]
    public partial class NpcOrg { }
    #endregion

    #region Org Metadata
    public class OrgMetadata
    {
        //public short OrgId { get; set; }

        public short InfoId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public bool IsPlayerEnabled { get; set; }

        public string SymbolFileName { get; set; }

        public Nullable<short> BaseLocationId { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "About")]
        public string AboutText { get; set; }

        public bool IsWorkInProgress { get; set; }

    }

    [MetadataType(typeof(OrgMetadata))]
    public partial class Org { }
    #endregion

    #region OrgEvent Metadata
    public class OrgEventMetadata
    {
        //public int OrgEventsId { get; set; }
        public short OrgId { get; set; }
        public short EventId { get; set; }

        [StringLength(100)]
        public string Blurb { get; set; }
    }

    [MetadataType(typeof(OrgEventMetadata))]
    public partial class OrgEvent { }
    #endregion

    #region Race Metadata
    public class RaceMetadata
    {
        //public byte RaceId { get; set; }

        [Required(ErrorMessage = "Katherine... I feel like we need talk. About how things need names. But also about what to do if something needs to be blank.")]
        [StringLength(15, ErrorMessage = "Well... this is awkward. So, I figured race names would never be more than 15 characters... guess I'm wrong. So, either contact me for a fix or figure out how to shorten it.")]
        [Display(Name = "Species Name")]
        public string RaceName { get; set; }
        public bool IsPlayerEnabled { get; set; }

    }

    [MetadataType(typeof(RaceMetadata))]
    public partial class Race { }
    #endregion

    #region Rift Metadata
    public class RiftMetadata
    {
        //public short RiftId { get; set; }

        public short InfoId { get; set; }

        [StringLength(50)]
        public string Nickname { get; set; }

        [Required]
        [StringLength(300)]
        [UIHint("MultilineText")]
        public string Location { get; set; }

        [Required]
        [UIHint("MultilineText")]
        public string Environment { get; set; }

        [Required]
        [UIHint("MultilineText")]
        public string Hazards { get; set; }

        public bool IsWorkInProgress { get; set; }

    }

    [MetadataType(typeof(RiftMetadata))]
    public partial class Rift { }
    #endregion

    #region Rumor Metadata
    public class RumorMetadata
    {
        //public int RumorsId { get; set; }

        public short RumorOfId { get; set; }

        public short AuthorId { get; set; }

        public bool IsApproved { get; set; }

        [Required]
        [StringLength(300)]
        [UIHint("MultilineText")]
        [Display(Name = "Content")]
        public string TheContent { get; set; }

    }

    [MetadataType(typeof(RumorMetadata))]
    public partial class Rumor { }
    #endregion

    #region Secret Metadata
    public class SecretMetadata
    {
        //public short SecretId { get; set; }
        public short IsAboutId { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Content")]
        public string TheContent { get; set; }

    }

    [MetadataType(typeof(SecretMetadata))]
    public partial class Secret { }
    #endregion

    #region SecretSecretTag Metadata
    public class SecretSecretTagMetadata
    {

    }

    [MetadataType(typeof(SecretSecretTagMetadata))]
    public partial class SecretSecretTag { }
    #endregion

    #region SecretTag Metadata
    public class SecretTagMetadata
    {
        //public short SecretTagId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(75)]
        public string Description { get; set; }
    }

    [MetadataType(typeof(SecretTagMetadata))]
    public partial class SecretTag { }
    #endregion

    #region Story Metadata
    public class StoryMetadata
    {
        //public short StoryId { get; set; }

        public short IsAboutId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public System.DateTime DateTold { get; set; }

        [StringLength(50)]
        public string CommissionedBy { get; set; }

        public bool IsCannon { get; set; }

        [Required]
        [UIHint("MultilineText")]
        [Display(Name = "Content")]
        public string TheContent { get; set; }

        [Required]
        [StringLength(20)]
        public string Title { get; set; }

    }

    [MetadataType(typeof(StoryMetadata))]
    public partial class Story { }
    #endregion

    #region StoryTag Metadata
    //public class StoryTagMetadata
    //{

    //}

    //[MetadataType(typeof(StoryTagMetadata))]
    //public partial class StoryTag { }
    #endregion

    #region Tag Metadata
    public class TagMetadata
    {
        //public short TagId { get; set; }
        [Required]
        [StringLength(25)]
        [Display(Name = "Name")]
        public string TagName { get; set; }

        [StringLength(75)]
        public string Description { get; set; }

    }

    [MetadataType(typeof(TagMetadata))]
    public partial class Tag { }
    #endregion

    #region Tier Metadata
    public class TierMetadata
    {
        //public byte TierId { get; set; }
        [Required]
        [StringLength(10)]
        public string TierName { get; set; }

        [Required]
        [Display(Name = "Minimum Level")]
        [Range(1, 20)]
        public byte MinLevel { get; set; }

        [Required]
        [Display(Name = "Maximum Level")]
        [Range(1, 20)]
        public byte MaxLevel { get; set; }

    }

    [MetadataType(typeof(TierMetadata))]
    public partial class Tier { }
    #endregion

    #region UserDetail Metadata
    public class UserDetailMetadata
    {
        //public string UserId { get; set; }

        [Required]
        [StringLength(32)]
        [Display(Name = "Discord Name")]
        public string DiscordName { get; set; }

        //DONE change the discord discriminator to a small int and add range to it
        [Required]
        [Range(0000, 9999)]
        public short DiscordDiscriminator { get; set; }

        public string ConsentFileName { get; set; }

        public Nullable<short> CurrentCharacterId { get; set; }

        public bool IsApproved { get; set; }
    }

    [MetadataType(typeof(UserDetailMetadata))]
    public partial class UserDetail { }
    #endregion

    #region VarietyOfInhabitant Metadata
    public class VarietyOfInhabitantMetadata
    {

    }

    [MetadataType(typeof(VarietyOfInhabitantMetadata))]
    public partial class VarietyOfInhabitant { }

    #endregion

}