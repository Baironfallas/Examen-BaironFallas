
using NLayer.Architecture.Bussines.Models;


namespace NLayer.Architecture.Bussines.Services
{
    public interface IPokemonService
    {
        //este metodo recibe como parametro el nombre del pokemon luego devolver el objeto
        Task<Pokemon> GetInfoPokemon(string pokemonName);
    }
}

