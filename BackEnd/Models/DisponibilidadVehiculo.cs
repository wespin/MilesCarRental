namespace BackEnd.Models
{
    public class DisponibilidadVehiculo
    {
        public int Id { get; set; }
        public int VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public int LocalidadId { get; set; }
        public Localidad Localidad { get; set; }

        public bool Disponible { get; set; }
    }
}
