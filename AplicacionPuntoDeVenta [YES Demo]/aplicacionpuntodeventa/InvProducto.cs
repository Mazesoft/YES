//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplicacionPuntoDeVenta
{
    using System;
    using System.Collections.Generic;
    
    public partial class InvProducto
    {
        public InvProducto()
        {
            this.InvInventarios = new HashSet<InvInventario>();
        }
    
        public int IdProducto { get; set; }
        public string Producto { get; set; }
    
        public virtual ICollection<InvInventario> InvInventarios { get; set; }
    }
}
