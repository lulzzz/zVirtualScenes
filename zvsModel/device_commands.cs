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
    
    public partial class device_commands
    {
        public device_commands()
        {
            this.device_command_options = new ObservableCollection<device_command_options>();
            this.device_command_que = new ObservableCollection<device_command_que>();
        }
    
        public int id { get; set; }
        public int device_id { get; set; }
        public string name { get; set; }
        public string friendly_name { get; set; }
        public int arg_data_type { get; set; }
        public string custom_data1 { get; set; }
        public string custom_data2 { get; set; }
        public string description { get; set; }
        public Nullable<int> sort_order { get; set; }
        public string help { get; set; }
    
        public virtual ObservableCollection<device_command_options> device_command_options { get; set; }
        public virtual ObservableCollection<device_command_que> device_command_que { get; set; }
        public virtual device device { get; set; }
    }
}
