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
    
    public partial class Rift
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rift()
        {
            this.VarietyOfInhabitants = new HashSet<VarietyOfInhabitant>();
        }
    
        public short RiftId { get; set; }
        public short InfoId { get; set; }
        public string Location { get; set; }
        public string Environment { get; set; }
        public string Hazards { get; set; }
    
        public virtual Info Info { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VarietyOfInhabitant> VarietyOfInhabitants { get; set; }
    }
}
