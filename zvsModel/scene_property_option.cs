//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace zVirtualScenesModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class scene_property_option
    {
        public int id { get; set; }
        public int scene_property_id { get; set; }
        public string options { get; set; }
    
        public virtual scene_property scene_property { get; set; }
    }
}
