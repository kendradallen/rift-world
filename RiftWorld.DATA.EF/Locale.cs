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
    
    public partial class Locale
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Locale()
        {
            this.Characters = new HashSet<Character>();
            this.LocaleEvents = new HashSet<LocaleEvent>();
            this.Locales1 = new HashSet<Locale>();
            this.Locales11 = new HashSet<Locale>();
            this.Majorities = new HashSet<Majority>();
            this.NPCs = new HashSet<NPC>();
            this.Orgs = new HashSet<Org>();
        }
    
        public short LocaleId { get; set; }
        public short InfoId { get; set; }
        public string Name { get; set; }
        public byte LevelOfLocaleId { get; set; }
        public Nullable<short> RegionId { get; set; }
        public Nullable<short> ClosestCityId { get; set; }
        public Nullable<short> CouncilDelegateId { get; set; }
        public string Appointed { get; set; }
        public string Environment { get; set; }
        public string About { get; set; }
        public bool IsPublished { get; set; }
        public string AvgLifestyle { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Character> Characters { get; set; }
        public virtual Info Info { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocaleEvent> LocaleEvents { get; set; }
        public virtual LocaleLevel LocaleLevel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Locale> Locales1 { get; set; }
        public virtual Locale Locale1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Locale> Locales11 { get; set; }
        public virtual Locale Locale2 { get; set; }
        public virtual NPC NPC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Majority> Majorities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NPC> NPCs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Org> Orgs { get; set; }
    }
}
