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
    
    public partial class Journal
    {
        public int JournalId { get; set; }
        public short CharacterId { get; set; }
        public System.DateTime OocDateWritten { get; set; }
        public string TheContent { get; set; }
        public bool IsApproved { get; set; }
        public bool HasUnseenEdit { get; set; }
    
        public virtual Character Character { get; set; }
    }
}
