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
    
    public partial class CharOrg
    {
        public short CharOrgsId { get; set; }
        public short CharId { get; set; }
        public short OrgId { get; set; }
        public string BlurbOrgPage { get; set; }
        public bool IsPublic { get; set; }
        public bool IsCurrent { get; set; }
        public bool KatherineApproved { get; set; }
    
        public virtual Character Character { get; set; }
        public virtual Org Org { get; set; }
    }
}
