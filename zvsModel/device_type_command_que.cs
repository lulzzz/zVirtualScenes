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
    
    public partial class device_type_command_que
    {
        public int id { get; set; }
        public int device_type_command_id { get; set; }
        public int device_id { get; set; }
        public string arg { get; set; }
    
        public virtual device device { get; set; }
        public virtual device_type_commands device_type_commands { get; set; }
    }
}
