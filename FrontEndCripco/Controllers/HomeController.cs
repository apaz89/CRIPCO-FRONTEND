using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrontEndCripco.Models;
using System.Data;
using System.Data.Entity;
using FrontEndCripco.BD;

namespace FrontEndCripco.assets
{
    public class HomeController : Controller
    {
        private GCEntities db = new GCEntities();

        private IEnumerable<SelectListItem> GetsTipos()
        {
            var tipos = new List<SelectListItem>(){
                         new SelectListItem() {Text="Menu", Value="M"},
                         new SelectListItem() {Text="Articulo", Value="A"}
                            };
            return tipos;
        }


        private IEnumerable<SelectListItem> GetListParent()
        {
            var cmsArticulo = db.CmsArticulos.ToList();

            var Parents = cmsArticulo.Select(x =>
                                  new SelectListItem
                                  {
                                      Value = x.ArticuloId.ToString(),
                                      Text = x.Titulo,
                                  });
            return Parents;
        }

        private List<TreeNodeModels> FillRecursive(List<CmsArticulosModels> flatObjects, int? parentId = null)
        {
            return flatObjects.Where(x => x.ParentArticuloId.Equals(parentId)).Select(item => new TreeNodeModels
            {
                ArticuloName = item.ArticuloName,
                ArticuloId = item.ArticuloId,
                Tipo = item.SelectedTipo,
                Children = FillRecursive(flatObjects, item.ArticuloId)
            }).ToList();
        }


        public void RecursiveLectura(ref string root_li, List<TreeNodeModels> headerTree)
        {

            foreach (var item in headerTree)
            {
                if (item.Tipo.Trim() == "M")
                {
                    root_li += "<li class=\"active dropdown\">";
                    root_li += "<a href =\"#home\" data-click =\"scroll-to-target\" data-toggle=\"dropdown\"> " + item.ArticuloName.ToUpper() + "<b class=\"caret\"></b> </a>";
                    root_li += "<ul class=\"dropdown-menu dropdown-menu-left animated fadeInLeft\">";
                    if (item.Children.Count > 0)
                    {
                        RecursiveLectura(ref root_li, item.Children);
                    }
                    else
                    {
                        root_li += "<li><a href = \"#"+ item.ArticuloId + "\">" + item.ArticuloName.ToUpper() + "</a></li>";
                    }
                    
                    root_li += "</ul></li>";
                }
                else
                {
                    root_li += "<li><a href = \"#" + item.ArticuloId + "\">" + item.ArticuloName.ToUpper() + "</a></li>";
                }
            }
        }


        public string GetAllCategoriesForTree()
        {
            List<CmsArticulosModels> articulos = new List<CmsArticulosModels>();
            var dt = db.CmsArticulos.ToList();

            if (dt != null && dt.Count > 0)
            {
                foreach (var row in dt)
                {
                    articulos.Add(
                        new CmsArticulosModels
                        {
                            ArticuloId = row.ArticuloId,
                            ArticuloName = row.Titulo,
                            SelectedTipo = row.Tipo,
                            ParentArticuloId = (row.PadreArticuloId != 0) ? row.PadreArticuloId : (int?)null
                        });
                }

                List<TreeNodeModels> headerTree = FillRecursive(articulos, null);

                string root_li = string.Empty;
                RecursiveLectura(ref root_li, headerTree);
                return root_li;
            }
            return "Record Not Found!!";
        }

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Tree = GetAllCategoriesForTree();
            var cmsArticulosModels = new List<CmsArticulosModels>();
            var cmsArticulos = db.CmsArticulos.Include(x => x.CmsArticulosDetalles).ToList();
            foreach (var art in cmsArticulos)
            {
                if (art.Tipo.Trim() != "M")
                {
                    var detalle = new List<DetalleArticuloModels>();
                    foreach(var det in art.CmsArticulosDetalles)
                    {
                        string base64String = Convert.ToBase64String(det.Imagen);
                        detalle.Add(new DetalleArticuloModels { ArticuloDetalleId = det.ArticuloDetalleId, ArticuloId = det.ArticuloId, Imagen = base64String });
                    }

                    cmsArticulosModels.Add(new CmsArticulosModels { ArticuloId=art.ArticuloId, ArticuloName=art.Titulo, Descripcion=art.Descripcion, detalleArticuloModels= detalle });
                }
            }

            return View(cmsArticulosModels);
        }
        
    }
}
