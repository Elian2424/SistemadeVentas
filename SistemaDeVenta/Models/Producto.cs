using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVentas.Models
{
    public class Producto
    {
        // Agregamos  Using datanotation  para validar nuestro formulario.
        //Agregamos Key para indicar que la propiedad Id sera una primary key en la base de datos.
        //Ponemos display para agregarle un label especifico a la propiedad.
        //ponemos required para indicarle al usuario  con el mensaje de error de que el campo debe de ser obligatorio.
        //creamos la propiedad.
        // al Final ponemos NOtMapped para indicarle a la migration que no mapee esa propiedad en la base de datos.


        [Key] //data componente
        public int Id { get; set; }  //propiedad

        [Display(Name = "Nombre:")]
        [Required(ErrorMessage = "Debe ingresar el nombre del Producto")]
        public string Nombre { get; set; }


        [Display(Name = "Descripción:")]
        [Required(ErrorMessage = "Debe ingresar una descripción del Producto")]
        public string Descripcion { get; set; }


        [Display(Name = "Precio:")]
        [Required(ErrorMessage = "Debe ingresar el precio del Producto")]
        public decimal Precio { get; set; }

        public Byte[] ImageFile { get; set; }


        [NotMapped]
        [Display(Name = "Imagen:")]

        public IFormFile AvatarImage { get; set; }




    }
}
