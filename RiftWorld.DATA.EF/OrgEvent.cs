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
    
    public partial class OrgEvent
    {
        public int OrgEventsId { get; set; }
        public short OrgId { get; set; }
        public short EventId { get; set; }
        public string Blurb { get; set; }
    
        public virtual Event Event { get; set; }
        public virtual Org Org { get; set; }
    }
}
