using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEndCripco.Models
{
    public class TreeNodeModels
    {
        public int ArticuloId { get; set; }
        public string ArticuloName { get; set; }

        public TreeNodeModels ParentArticulo { get; set; }

        public string Tipo { get; set; }
        public List<TreeNodeModels> Children { get; set; }
    }
}