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
    
    public partial class Majority
    {
        public short MajorityId { get; set; }
        public short LocaleId { get; set; }
        public byte RaceId { get; set; }
        public bool IsFirst { get; set; }
    
        public virtual Locale Locale { get; set; }
        public virtual Race Race { get; set; }
    }
}
