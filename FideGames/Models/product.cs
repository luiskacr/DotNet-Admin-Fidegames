//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FideGames.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.invoice_detail = new HashSet<invoice_detail>();
        }
    
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int type_product_id { get; set; }
        public int deparment_id { get; set; }
        public int brand_id { get; set; }
        public string product_description { get; set; }
        public int quantities { get; set; }
        public decimal price { get; set; }
    
        public virtual brand_product brand_product { get; set; }
        public virtual deparment_product deparment_product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<invoice_detail> invoice_detail { get; set; }
        public virtual type_product type_product { get; set; }
    }
}
