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
    
    public partial class InvCantidadPorTaco
    {
        public InvCantidadPorTaco()
        {
            this.InvInventarios = new HashSet<InvInventario>();
        }
    
        public int IdCantTaco { get; set; }
        public decimal Cantidad { get; set; }
    
        public virtual ICollection<InvInventario> InvInventarios { get; set; }
    }
}