//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UTEMerchant
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchasedProduct
    {
        public int PurchaseID { get; set; }
        public int Id_user { get; set; }
        public int Item_Id { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Delivery_Address { get; set; }
        public string Delivery_Status { get; set; }
    
        public virtual Item Item { get; set; }
        public virtual User User { get; set; }
    }
}
