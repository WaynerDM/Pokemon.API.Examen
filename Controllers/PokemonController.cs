using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Pokemon.API.Services; 
using Pokemon.API.Models;

namespace PokemonApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly PokemonService _pokemonService;

        public PokemonController(PokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> GetPokemon(string name)
        {
            var pokemon = await _pokemonService.GetPokemonAsync(name);

            if (pokemon == null)
                return NotFound();

            return Ok(pokemon);
        }
    }
}
