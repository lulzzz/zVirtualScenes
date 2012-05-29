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
    
    public partial class device
    {
        public device()
        {
            this.device_command_que = new ObservableCollection<device_command_que>();
            this.device_commands = new ObservableCollection<device_commands>();
            this.device_property_values = new ObservableCollection<device_property_values>();
            this.device_type_command_que = new ObservableCollection<device_type_command_que>();
            this.device_values = new ObservableCollection<device_values>();
            this.group_devices = new ObservableCollection<group_devices>();
            this.scene_commands = new ObservableCollection<scene_commands>();
        }
    
        public int id { get; set; }
        public int device_type_id { get; set; }
        public int node_id { get; set; }
        public string current_status { get; set; }
        public string friendly_name { get; set; }
        public Nullable<System.DateTime> last_heard_from { get; set; }
    
        public virtual ObservableCollection<device_command_que> device_command_que { get; set; }
        public virtual ObservableCollection<device_commands> device_commands { get; set; }
        public virtual ObservableCollection<device_property_values> device_property_values { get; set; }
        public virtual ObservableCollection<device_type_command_que> device_type_command_que { get; set; }
        public virtual device_types device_types { get; set; }
        public virtual ObservableCollection<device_values> device_values { get; set; }
        public virtual ObservableCollection<group_devices> group_devices { get; set; }
        public virtual ObservableCollection<scene_commands> scene_commands { get; set; }
    }
}