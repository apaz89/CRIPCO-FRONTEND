using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace FrontEndCripco.Models
{
    public class CmsArticulosModels
    {
        public CmsArticulosModels()
        {
            Files = new List<HttpPostedFileBase>();
            detalleArticuloModels = new List<DetalleArticuloModels>();
        }

        public int ArticuloId { get; set; }

        [Display(Name = "Nombre ")]
        [Required(ErrorMessage = "Campo Requerido", AllowEmptyStrings = false)]
        public string ArticuloName { get; set; }
        [Required(ErrorMessage = "Campo Requerido", AllowEmptyStrings = false)]
        public string SelectedTipo { get; set; }
        public IEnumerable<SelectListItem> Tipos { get; set; }

        [Display(Name = "Articulo Padre ")]
        [Required(ErrorMessage = "Campo Requerido", AllowEmptyStrings = false)]
        public System.Nullable<int> ParentArticuloId { get; set; }
        
        public IEnumerable<SelectListItem> ListParent { get; set; }

        [Display(Name = "Texto a mostrar. ")]
        //[Required(ErrorMessage = "Campo Requerido", AllowEmptyStrings = false)]
        public string Descripcion { get;  set; }

        [Display(Name = "Imagenes Agregadas ")]
        public List<DetalleArticuloModels> detalleArticuloModels { get; set; }

        [Display(Name = "Cargar Imagenes ")]
        public List<HttpPostedFileBase> Files { get; set; }
    }
}