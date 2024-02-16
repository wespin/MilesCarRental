using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using BackEnd.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Repositories;


namespace BackEndTest
{
    public class DisponibilidadVehiculosTests
    {


        [Fact]
        public async Task GetDisponibilidadVehiculosLocalidadRecogida_DeberiaRetornarOkConVehiculosDisponibles()
        {
            // Arrange
            var localidadRecogida = "Bogota";
            var vehiculosDisponiblesEsperados = new List<Vehiculo>
            {
                new Vehiculo { Id = 1, Marca = "Toyota", Modelo = "Corolla" },
                new Vehiculo { Id = 2, Marca = "Honda", Modelo = "Civic" }
            };

            var mockDbContext = new Mock<BackEndContext>();
            mockDbContext.Setup(db => db.DisponibilidadVehiculos)
                .Returns(MockDbSet(new List<DisponibilidadVehiculo>
                {
                    new DisponibilidadVehiculo { Id = 1, VehiculoId = 1, Localidad = new Localidad { Nombre = localidadRecogida }, Disponible = true },
                    new DisponibilidadVehiculo { Id = 2, VehiculoId = 2, Localidad = new Localidad { Nombre = localidadRecogida }, Disponible = true }
                }));

            var controller = new VehiculoesController(mockDbContext.Object);

            // Act
            var response = await controller.GetDisponibilidadVehiculosLocalidadRecogida(localidadRecogida);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var vehiculosDisponibles = Assert.IsAssignableFrom<IEnumerable<Vehiculo>>(okResult.Value);
            Assert.Equal(vehiculosDisponiblesEsperados.Count, vehiculosDisponibles.Count());
            Assert.Equal(vehiculosDisponiblesEsperados, vehiculosDisponibles);
        }

        [Fact]
        public async Task GetDisponibilidadVehiculosLocalidadRecogida_DeberiaRetornarBadRequestCuandoLocalidadRecogidaNoProporcionada()
        {
            // Arrange
            var localidadRecogida = "";
            var mockDbContext = new Mock<BackEndContext>();
            var controller = new VehiculoesController(mockDbContext.Object);

            // Act
            var response = await controller.GetDisponibilidadVehiculosLocalidadRecogida(localidadRecogida);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal("Localidad de recogida no proporcionada", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDisponibilidadVehiculosLocalidadRecogida_DeberiaRetornarNotFoundCuandoNoHayVehiculosDisponibles()
        {
            // Arrange
            var localidadRecogida = "Cucuta";
            var mockDbContext = new Mock<BackEndContext>();
            mockDbContext.Setup(db => db.DisponibilidadVehiculos)
                .Returns(MockDbSet(new List<DisponibilidadVehiculo>()));

            var controller = new VehiculoesController(mockDbContext.Object);

            // Act
            var response = await controller.GetDisponibilidadVehiculosLocalidadRecogida(localidadRecogida);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal("No hay vehículos disponibles en la localidad de recogida especificada", notFoundResult.Value);
        }


        [Fact]
        public async Task GetDisponibilidadVehiculosLocalidadDevolucion_DeberiaRetornarOkConVehiculosDisponibles()
        {
            // Arrange
            var localidadRecogida = "Bogota";
            var localidadDevolucion = "Bogota";
            var vehiculosDisponiblesRecogida = new List<Vehiculo>
            {
                new Vehiculo { Id = 1, Marca = "Toyota", Modelo = "Corolla" },
                new Vehiculo { Id = 2, Marca = "Honda", Modelo = "Civic" }
            };
            var vehiculosDisponiblesDevolucion = new List<Vehiculo>
            {
                new Vehiculo { Id = 1, Marca = "Toyota", Modelo = "Corolla" }
            };

            var mockDbContext = new Mock<BackEndContext>();
            mockDbContext.Setup(db => db.DisponibilidadVehiculos)
                .Returns(MockDbSet(new List<DisponibilidadVehiculo>
                {
                    new DisponibilidadVehiculo { Id = 1, VehiculoId = 1, Localidad = new Localidad { Nombre = localidadRecogida }, Disponible = true },
                    new DisponibilidadVehiculo { Id = 2, VehiculoId = 2, Localidad = new Localidad { Nombre = localidadRecogida }, Disponible = true },
                    new DisponibilidadVehiculo { Id = 3, VehiculoId = 1, Localidad = new Localidad { Nombre = localidadDevolucion }, Disponible = true }
                }));

            var controller = new VehiculoesController(mockDbContext.Object);

            // Act
            var response = await controller.GetDisponibilidadVehiculosLocalidadDevolucion(localidadRecogida, localidadDevolucion);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var vehiculosDisponibles = Assert.IsAssignableFrom<IEnumerable<Vehiculo>>(okResult.Value);
            Assert.Equal(1, vehiculosDisponibles?.Count());
            Assert.Equal(vehiculosDisponiblesDevolucion, vehiculosDisponibles);
        }

        [Fact]
        public async Task GetDisponibilidadVehiculosLocalidadDevolucion_DeberiaRetornarBadRequestCuandoLocalidadRecogidaNoProporcionada()
        {
            // Arrange
            var localidadRecogida = "";
            var localidadDevolucion = "Medellin";

            var mockDbContext = new Mock<BackEndContext>();

            var controller = new VehiculoesController(mockDbContext.Object);

            // Act
            var response = await controller.GetDisponibilidadVehiculosLocalidadDevolucion(localidadRecogida, localidadDevolucion);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal("Localidad de recogida no proporcionada", badRequestResult.Value);
        }

        [Fact]
        public async Task GetDisponibilidadVehiculosLocalidadDevolucion_DeberiaRetornarNotFoundCuandoNoHayVehiculosDisponiblesEnLocalidadRecogida()
        {
            // Arrange
            var localidadRecogida = "Cartagena";
            var localidadDevolucion = "Medellin";

            var mockDbContext = new Mock<BackEndContext>();
            mockDbContext.Setup(db => db.DisponibilidadVehiculos)
                .Returns(MockDbSet(new List<DisponibilidadVehiculo>()));

            var controller = new VehiculoesController(mockDbContext.Object);

            // Act
            var response = await controller.GetDisponibilidadVehiculosLocalidadDevolucion(localidadRecogida, localidadDevolucion);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal("No hay vehículos disponibles en la localidad de recogida especificada", notFoundResult.Value);
        }



        [Fact]
        public void GetDisponibilidadVehiculosMercado_DeberiaRetornarOkConVehiculosDisponibles()
        {
            // Arrange
            var localidadRecogida = "Bogota";
            var ubicacionCliente = "COL";
            var vehiculosDisponiblesEsperados = new List<Vehiculo>
            {
                new Vehiculo { Id = 1, Marca = "Toyota", Modelo = "Corolla", Mercado = "COL" },
                new Vehiculo { Id = 2, Marca = "Honda", Modelo = "Civic", Mercado = "COL" }
            };

            var mockDbContext = new Mock<BackEndContext>();
            mockDbContext.Setup(db => db.Vehiculos)
                .Returns(MockDbSet(vehiculosDisponiblesEsperados));

            var controller = new VehiculoesController(mockDbContext.Object);

            // Act
            var response = controller.GetDisponibilidadVehiculosMercado(localidadRecogida, ubicacionCliente);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var vehiculosDisponibles = Assert.IsAssignableFrom<IEnumerable<Vehiculo>>(okResult.Value);
            Assert.Equal(vehiculosDisponiblesEsperados.Count, vehiculosDisponibles.Count());
            Assert.Equal(vehiculosDisponiblesEsperados, vehiculosDisponibles);
        }

        [Fact]
        public void GetDisponibilidadVehiculosMercado_DeberiaRetornarNotFoundCuandoNoHayVehiculosDisponibles()
        {
            // Arrange
            var localidadRecogida = "Pereira";
            var ubicacionCliente = "COL";
            var vehiculosDisponiblesEsperados = new List<Vehiculo>();

            var mockDbContext = new Mock<BackEndContext>();
            mockDbContext.Setup(db => db.Vehiculos)
                .Returns(MockDbSet(vehiculosDisponiblesEsperados));

            var controller = new VehiculoesController(mockDbContext.Object);

            // Act
            var response = controller.GetDisponibilidadVehiculosMercado(localidadRecogida, ubicacionCliente);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal("No hay vehículos disponibles para el mercado especificado y la localidad de recogida.", notFoundResult.Value);
        }

        private static DbSet<T> MockDbSet<T>(IEnumerable<T> data) where T : class
        {
            var queryableData = data.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryableData.GetEnumerator());
            return mockDbSet.Object;
        }
       
    }
}