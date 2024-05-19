
using NLayer.Architecture.Bussines.Models;
using System.Text.Json;


namespace NLayer.Architecture.Bussines.Services
{
    public class PokemonService : IPokemonService
    {
        // Este es un campo de solo lectura. Una vez inicializado en el constructor, su valor nunca cambiará.
        // Esto asegura que la instancia de HttpClient no será modificada después de la construcción del objeto.
        private readonly HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Pokemon> GetInfoPokemon(string pokemonName)
        {
            // Definimos la URL base de la API de Pokémon.
            string baseUrl = "https://pokeapi.co/api/v2/";

            // - La variable `baseUrl` contiene la parte constante de la URL de la API de Pokémon, incluyendo el protocolo "https://" y la ruta base "/api/v2/".
            // - Usamos la interpolación de cadenas (`$""`) para insertar dinámicamente el nombre del Pokémon en la URL.
            // - "pokemon/" es la ruta de la API que indica que queremos obtener información sobre un Pokémon específico.
            string url = $"{baseUrl}pokemon/{pokemonName.ToLower()}"; // Convertir el nombre del Pokémon a minúsculas

            try
            {
                // Realiza una solicitud HTTP GET asíncrona a la URL 
                var response = await _httpClient.GetAsync(url);
                // Verifica si la solicitud HTTP fue exitosa y es una funcion propia del lenguaje.
                if (response.IsSuccessStatusCode)
                {
                    // Lee el contenido de la respuesta HTTP como una cadena de caracteres
                    var jsonData = await response.Content.ReadAsStringAsync();
                    // Analiza la cadena de caracteres JSON y la convierte en un objeto JsonDocument.
                    var pokemonData = JsonDocument.Parse(jsonData);
                    // se crea una instacia de la clase pokemon y se la asignan los valores
                    var pokemon = new Pokemon
                    {
                        Nombre = pokemonData.RootElement.GetProperty("name").GetString(),
                        Tipo = pokemonData.RootElement.GetProperty("types")[0].GetProperty("type").GetProperty("name").GetString(),
                        UrlSprite = pokemonData.RootElement.GetProperty("sprites").GetProperty("front_default").GetString(),
                        //este bloque de código transforma una lista de movimientos de un Pokémon en formato JSON en una lista de cadenas de caracteres que contienen los nombres de esos movimientos.
                        Moves = pokemonData.RootElement.GetProperty("moves").EnumerateArray().Select(move => move.GetProperty("move").GetProperty("name").GetString()).ToList()
                    };

                    return pokemon;
                }
                else
                {
                    // Lanza una excepción HttpRequestException con un mensaje que indica el código de estado de la respuesta HTTP no exitosa
                    throw new HttpRequestException($"Error: {response.StatusCode}");
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                throw;
            }
        }
    }
}

