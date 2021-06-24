using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
namespace SistemaVentas.Models
{

    public class Carrito
    {
        [Key]
        public int Id { get; set; }


        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [Display(Name = "Cantidad:")]
        [Required(ErrorMessage = "Este   campo es obligatorio ")]
        public decimal Cantidad { get; set; }

        public decimal Monto { get; set; }

        public string Usuariop { get; set; }
        [NotMapped]
        public decimal Precio { get; set; }
        [NotMapped]
        public decimal _monto
        {
            get
            {

                Monto = Precio * Cantidad;
                return Monto;
            }

            set
            {
                Monto = value;
            }
        }
    }
}
