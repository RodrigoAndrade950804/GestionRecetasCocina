using SQLite;

namespace GestionRecetasCocina.Models
{
    public class Receta
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [NotNull]

        public string Nombre { get; set; } = string.Empty;
        [NotNull]
        public string Ingredientes { get; set; } = string.Empty;
        [NotNull]
        public int TiempoPreparacionMinutos { get; set; }
        [NotNull]
        public bool EsVegetariana { get; set; }
    }
}