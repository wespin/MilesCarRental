using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class BackEndContext : DbContext, IBackEndContext
    {
        public BackEndContext(DbContextOptions<BackEndContext> options)
            : base(options)
        {
        }

        public DbSet<Vehiculo> Vehiculos { get; set; } = null!;
        public DbSet<Reserva> Reservas { get; set; } = null!;
        public DbSet<Localidad> Localidades { get; set; } = null!;
        public DbSet<DisponibilidadVehiculo> DisponibilidadVehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entities and relationships

            // Seed data for Products table
            //modelBuilder.Entity<Localidad>().HasData(
            //    new Localidad { Id = 1, Nombre = "Usaquen", Ciudad = "Bogota" },
            //    new Localidad { Id = 2, Nombre = "Chapinero", Ciudad = "Bogota" }
            //);

            //            modelBuilder.Entity<Mercado>().HasData(
            //                new Mercado { IdMercado = 1, IdlocalidadRecogida = 1, IdlocalidadDevolucion = 1 },
            //                new Mercado { IdMercado = 1, IdlocalidadRecogida = 2, IdlocalidadDevolucion = 2 }
            //);

            // Seed data for Categories table

        }



        public void SeedData()
        {
            // Verificar si ya existen datos en la base de datos
            if (Vehiculos.Any() || Localidades.Any() || DisponibilidadVehiculos.Any())
            {
                return; // Si ya hay datos, no hacer nada
            }

            // Sembrar datos de prueba
            var localidad1 = new Localidad { Nombre = "Bogota", Mercado = "COL" };
            var localidad2 = new Localidad { Nombre = "Medellin", Mercado = "COL" };
            var localidad3 = new Localidad { Nombre = "Cali", Mercado = "COL" };

            var vehiculo1 = new Vehiculo { Marca = "Toyota", Modelo = "Corolla", Mercado = "COL" };
            var vehiculo2 = new Vehiculo { Marca = "Honda", Modelo = "Civic", Mercado = "COL" };
            var vehiculo3 = new Vehiculo { Marca = "Ford", Modelo = "Fusion", Mercado = "COL" };

            var disponibilidad1 = new DisponibilidadVehiculo { Vehiculo = vehiculo1, Localidad = localidad1, Disponible = true };
            var disponibilidad2 = new DisponibilidadVehiculo { Vehiculo = vehiculo2, Localidad = localidad2, Disponible = true };
            var disponibilidad3 = new DisponibilidadVehiculo { Vehiculo = vehiculo3, Localidad = localidad3, Disponible = true };

            var reserva1 = new Reserva { FechaInicio = DateTime.Now, FechaFin = DateTime.Now.AddDays(7), LocalidadRecogida = "Bogota", LocalidadDevolucion = "Bogota", VehiculoId = 1 };
            var reserva2 = new Reserva { FechaInicio = DateTime.Now, FechaFin = DateTime.Now.AddDays(2), LocalidadRecogida = "Bogota", LocalidadDevolucion = "Medellin", VehiculoId = 2 };



            // Agregar entidades al contexto y guardar cambios
            Localidades.AddRange(localidad1, localidad2, localidad3);
            Vehiculos.AddRange(vehiculo1, vehiculo2, vehiculo3);
            DisponibilidadVehiculos.AddRange(disponibilidad1, disponibilidad2, disponibilidad3);
            Reservas.AddRange(reserva1, reserva2);
            SaveChanges();
        }
    }
}
