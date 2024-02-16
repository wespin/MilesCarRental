using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
using BackEnd.Repositories;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoesController : ControllerBase
    {
        private readonly IBackEndContext _context;

        public VehiculoesController(IBackEndContext context)
        {
            _context = context;
        }

        [HttpGet("GetDisponibilidadVehiculosLocalidadRecogida")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetDisponibilidadVehiculosLocalidadRecogida(string localidadRecogida)
        {

            if (string.IsNullOrEmpty(localidadRecogida))
            {
                return BadRequest("Localidad de recogida no proporcionada");
            }

            var vehiculosDisponibles = await  _context.DisponibilidadVehiculos
                .Where(dv => dv.Localidad.Nombre == localidadRecogida && dv.Disponible)
                .Select(dv => dv.Vehiculo)
                .ToListAsync();

            if (!vehiculosDisponibles.Any() || vehiculosDisponibles.Count == 0)
            {
                return NotFound("No hay vehículos disponibles en la localidad de recogida especificada");
            }

            //return await Ok(_context.Localidades.ToListAsync();
            return Ok(vehiculosDisponibles);
        }

        [HttpGet("GetDisponibilidadVehiculosLocalidadDevolucion")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetDisponibilidadVehiculosLocalidadDevolucion(string localidadRecogida, string localidadDevolucion)
        {

            if (string.IsNullOrEmpty(localidadRecogida))
            {
                return BadRequest("Localidad de recogida no proporcionada");
            }
            if (string.IsNullOrEmpty(localidadDevolucion))
            {
                return BadRequest("Localidad de devolución no proporcionada");
            }

            var vehiculosDisponibles = await _context.DisponibilidadVehiculos
                .Where(dv => dv.Localidad.Nombre == localidadRecogida && dv.Disponible)
                .Select(dv => dv.Vehiculo)
                .ToListAsync();

            if (!vehiculosDisponibles.Any() || vehiculosDisponibles.Count == 0)
            {
                return NotFound("No hay vehículos disponibles en la localidad de recogida especificada");
            }

            vehiculosDisponibles =  vehiculosDisponibles
                .Where(v => _context.DisponibilidadVehiculos
                .Any(dv => dv.VehiculoId == v.Id && dv.Localidad.Nombre == localidadDevolucion && dv.Disponible))
                .ToList();

            if (vehiculosDisponibles == null || vehiculosDisponibles.Count == 0)
            {
                return NotFound("No hay vehículos disponibles en la localidad de devolución especificada.");
            }

            //return await Ok(_context.Localidades.ToListAsync();
            return Ok(vehiculosDisponibles);
        }


        [HttpGet("GetDisponibilidadVehiculosMercado")]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> GetDisponibilidadVehiculosMercado(string localidadRecogida, string ubicacionCliente)
        {

            if (string.IsNullOrEmpty(localidadRecogida))
            {
                return BadRequest("Localidad de recogida no proporcionada");
            }
            if (string.IsNullOrEmpty(ubicacionCliente))
            {
                return BadRequest("Ubicación del cliente  no proporcionada");
            }

            var vehiculosDisponibles = await _context.DisponibilidadVehiculos
                .Where(dv => dv.Localidad.Nombre == localidadRecogida && dv.Localidad.Mercado == ubicacionCliente && dv.Disponible)
                .Select(dv => dv.Vehiculo)
                .ToListAsync();

            if (!vehiculosDisponibles.Any() || vehiculosDisponibles.Count == 0)
            {
                return NotFound("No hay vehículos disponibles para el mercado especificado y la localidad de recogida.");
            }

            return Ok(vehiculosDisponibles);
        }





        //// GET: api/Vehiculoes/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Vehiculo>> GetVehiculo(long id)
        //{
        //    var vehiculo = await _context.Vehiculos.FindAsync(id);

        //    if (vehiculo == null)
        //    {
        //        return NotFound();
        //    }

        //    return vehiculo;
        //}

        //// PUT: api/Vehiculoes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutVehiculo(long id, Vehiculo vehiculo)
        //{
        //    if (id != vehiculo.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(vehiculo).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!VehiculoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Vehiculoes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Vehiculo>> PostVehiculo(Vehiculo vehiculo)
        //{
        //    _context.Vehiculos.Add(vehiculo);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetVehiculo", new { id = vehiculo.Id }, vehiculo);
        //}

        //// DELETE: api/Vehiculoes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteVehiculo(long id)
        //{
        //    var vehiculo = await _context.Vehiculos.FindAsync(id);
        //    if (vehiculo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Vehiculos.Remove(vehiculo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool VehiculoExists(long id)
        //{
        //    return _context.Vehiculos.Any(e => e.Id == id);
        //}
    }
}
