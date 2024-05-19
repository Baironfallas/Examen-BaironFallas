
using Microsoft.AspNetCore.Mvc;
using NLayer.Architecture.Bussines.Models;
using NLayer.Architecture.Bussines.Services;
using System.Threading.Tasks;

namespace NLayer.Architecture.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpGet("{pokemonName}")]
        public async Task<ActionResult<Pokemon>> GetPokemonInfo(string pokemonName)
        {
            try
            {
                // Llama al m�todo GetInfoPokemon del servicio de Pok�mon para obtener informaci�n sobre un Pok�mon espec�fico
                var pokemon = await _pokemonService.GetInfoPokemon(pokemonName);
                return Ok(pokemon);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}