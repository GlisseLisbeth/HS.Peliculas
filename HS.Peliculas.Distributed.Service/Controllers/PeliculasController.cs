using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using HS.Peliculas.Distributed.Service.Contexts;
using HS.Peliculas.Distributed.Service.Models;
using HS.Peliculas.Distributed.Service.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HS.Peliculas.Distributed.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeliculasController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public PeliculasController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        // GET: api/<PeliculasController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PeliculaDTO>>> Get()
        {
            var peliculas = await context.Pelicula.ToListAsync();
            var peliculasDTO = mapper.Map<List<PeliculaDTO>>(peliculas);
            return peliculasDTO;
        }

        // GET api/<PeliculasController>/5
        [HttpGet("{id}", Name = "ObtenerPelicula")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id)
        {
            var pelicula = await context.Pelicula.FirstOrDefaultAsync(x => x.Id == id);

            if (pelicula == null)
            {
                return NotFound();
            }

            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);

            return peliculaDTO;
        }

        // POST api/<PeliculasController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PeliculaCreacionDTO peliculaCreacion)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreacion);
            context.Add(pelicula);
            await context.SaveChangesAsync();
            var peliculaDTO = mapper.Map<PeliculaDTO>(pelicula);
            return new CreatedAtRouteResult("ObtenerPelicula", new { id = pelicula.Id }, peliculaDTO);
        }

        // PUT api/<PeliculasController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] PeliculaCreacionDTO peliculaActualizacion)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaActualizacion);
            pelicula.Id = id;
            context.Entry(pelicula).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaCreacionDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var peliculaBD = await context.Pelicula.FirstOrDefaultAsync(x => x.Id == id);

            if (peliculaBD == null)
            {
                return NotFound();
            }

            var peliculaDTO = mapper.Map<PeliculaCreacionDTO>(peliculaBD);

            patchDocument.ApplyTo(peliculaDTO, (Microsoft.AspNetCore.JsonPatch.Adapters.IObjectAdapter)ModelState);

            mapper.Map(peliculaDTO, peliculaBD);

            var isValid = TryValidateModel(peliculaBD);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<PeliculasController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pelicula>> Delete(int id)
        {
            var pelicula = await context.Pelicula.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);
            if (pelicula == default(int))
            {
                return NotFound();
            }

            context.Remove(new Pelicula { Id = pelicula });
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
