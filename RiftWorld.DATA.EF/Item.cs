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
    
    public partial class Item
    {
        public short ItemId { get; set; }
        public short InfoId { get; set; }
        public string Name { get; set; }
        public string PictureFileName { get; set; }
        public string DescriptionText { get; set; }
        public string PropertyText { get; set; }
        public string HistoryText { get; set; }
        public bool IsPublished { get; set; }
        public string Artist { get; set; }
    
        public virtual Info Info { get; set; }
    }
}
