namespace BackEnd.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string LocalidadRecogida { get; set; }
        public string LocalidadDevolucion { get; set; }

        public int VehiculoId { get; set; }
        public Vehiculo Vehiculo { get; set; }
    }
}
