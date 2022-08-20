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
    
    public partial class Branch
    {
        public Branch()
        {
            this.Orders = new HashSet<Order>();
            this.ProductBranches = new HashSet<ProductBranch>();
        }
    
        public long ID { get; set; }
        public long PartnerID { get; set; }
        public string Branchname { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
    
        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ProductBranch> ProductBranches { get; set; }
    }
}