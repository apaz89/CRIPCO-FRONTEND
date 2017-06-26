namespace FrontEndCripco.Models
{
    public class DetalleArticuloModels
    {
        public int ArticuloDetalleId { get; set; }
        public int ArticuloId { get; set; }

        public string Imagen { get; set; }

        public string UrlVideo { get; set; }
    }
}