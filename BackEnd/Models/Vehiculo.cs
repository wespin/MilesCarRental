namespace BackEnd.Models
{
    public class Vehiculo
    {
        public long Id { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Mercado { get; set; }
        public ICollection<DisponibilidadVehiculo> DisponibilidadVehiculos { get; set; }


    }
}
