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
    
    public partial class Org
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Org()
        {
            this.CharOrgs = new HashSet<CharOrg>();
            this.NpcOrgs = new HashSet<NpcOrg>();
            this.OrgEvents = new HashSet<OrgEvent>();
        }
    
        public short OrgId { get; set; }
        public short InfoId { get; set; }
        public bool IsPlayerEnabled { get; set; }
        public string SymbolFileName { get; set; }
        public Nullable<short> BaseLocationId { get; set; }
        public string AboutText { get; set; }
        public string Artist { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CharOrg> CharOrgs { get; set; }
        public virtual Info Info { get; set; }
        public virtual Locale Locale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NpcOrg> NpcOrgs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrgEvent> OrgEvents { get; set; }
    }
}
