//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RiftWorld.DATA.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Character
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Character()
        {
            this.CharOrgs = new HashSet<CharOrg>();
            this.CharSecrets = new HashSet<CharSecret>();
            this.Journals = new HashSet<Journal>();
            this.Rumors = new HashSet<Rumor>();
            this.UserDetails = new HashSet<UserDetail>();
        }
    
        public short CharacterId { get; set; }
        public string PlayerId { get; set; }
        public string CharacterName { get; set; }
        public byte RaceId { get; set; }
        public byte GenderId { get; set; }
        public string PortraitFileName { get; set; }
        public string Description { get; set; }
        public string About { get; set; }
        public short CurrentLocationId { get; set; }
        public byte TierId { get; set; }
        public bool IsRetired { get; set; }
        public bool IsApproved { get; set; }
        public string Artist { get; set; }
        public string ClassString { get; set; }
        public bool HasUnseenEdit { get; set; }
        public bool IsDead { get; set; }
        public string BackupPortrayerName { get; set; }
        public bool IsPlayerDemo { get; set; }
        public bool IsRequestingRetire { get; set; }
    
        public virtual Gender Gender { get; set; }
        public virtual Locale Locale { get; set; }
        public virtual Race Race { get; set; }
        public virtual Tier Tier { get; set; }
        public virtual UserDetail UserDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CharOrg> CharOrgs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CharSecret> CharSecrets { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Journal> Journals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Rumor> Rumors { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
