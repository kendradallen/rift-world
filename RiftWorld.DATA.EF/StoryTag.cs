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
    
    public partial class StoryTag
    {
        public int StoryTagId { get; set; }
        public short StoryId { get; set; }
        public short TagId { get; set; }
    
        public virtual Story Story { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
