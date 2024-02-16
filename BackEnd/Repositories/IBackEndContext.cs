using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public interface IBackEndContext
    {
        DbSet<DisponibilidadVehiculo> DisponibilidadVehiculos { get; set; }
        DbSet<Vehiculo> Vehiculos { get; set; }
        DbSet<Reserva> Reservas { get; set; }
        DbSet<Localidad> Localidades { get; set; }

    }
}
