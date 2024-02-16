namespace BackEnd.Models
{
    public class Localidad
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public string Mercado { get; set; }
        public ICollection<DisponibilidadVehiculo> DisponibilidadVehiculos { get; set; }
    }
}
