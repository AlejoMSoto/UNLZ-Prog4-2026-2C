using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Heladeria.Web.Models
{
    public class HeladoVM
    {
        public int Id { get; set; }

        public string Sabor { get; set; } = string.Empty;

        [Display(Name = "Tamaño")]
        public string Tamanio { get; set; } = string.Empty;

        public decimal Precio { get; set; }

    }

    public class HeladoPrepararVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Elegi un sabor")]
        [Display(Name = "Sabor")]
        public string Sabor { get; set; } = string.Empty;

        [Required(ErrorMessage = "Elegi el tamaño")]
        [Display(Name = "Tamaño")]
        public string Tamanio { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pone un precio")]
        [Range(0.01, 99999.99, ErrorMessage = "Precio invalido")]
        public decimal Precio { get; set; }

        public List<SelectListItem> ListaSabores { get; set; } = new();
        public List<SelectListItem> ListaTamanios { get; set; } = new();
    }
}
