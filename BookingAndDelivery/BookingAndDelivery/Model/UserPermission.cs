//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookingAndDelivery.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserPermission
    {
        public long ID { get; set; }
        public long RoleID { get; set; }
        public Nullable<bool> AdminFn { get; set; }
        public Nullable<bool> PartnerFn { get; set; }
        public Nullable<bool> CustomerFn { get; set; }
        public Nullable<bool> DriverFn { get; set; }
        public Nullable<bool> StaffFn { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual Role Role { get; set; }
    }
}
