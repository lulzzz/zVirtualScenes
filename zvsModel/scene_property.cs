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
    using System.Collections.ObjectModel;
    
    public partial class scene_property
    {
        public scene_property()
        {
            this.scene_property_option = new ObservableCollection<scene_property_option>();
            this.scene_property_value = new ObservableCollection<scene_property_value>();
        }
    
        public int id { get; set; }
        public string friendly_name { get; set; }
        public string defualt_value { get; set; }
        public int value_data_type { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    
        public virtual ObservableCollection<scene_property_option> scene_property_option { get; set; }
        public virtual ObservableCollection<scene_property_value> scene_property_value { get; set; }
    }
}
