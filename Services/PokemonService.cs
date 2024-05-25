using RestSharp;
using System.Linq;
using System.Threading.Tasks;
using Pokemon.API.Models;

namespace Pokemon.API.Services
{
    public class PokemonService
    {
        private readonly RestClient _client;

        public PokemonService()
        {
            _client = new RestClient("https://pokeapi.co/api/v2/");
        }

        public async Task<PokemonG> GetPokemonAsync(string name)
        {
            var request = new RestRequest($"pokemon/{name}", Method.Get);
            var response = await _client.ExecuteAsync<PokeApiResponse>(request);

            if (!response.IsSuccessful || response.Data == null)
                return null;

            var pokemon = new PokemonG
            {
                Name = response.Data.Name,
                Type = response.Data.Types.First().Type.Name,
                SpriteUrl = response.Data.Sprites.FrontDefault,
                Moves = response.Data.Moves.Select(m => m.Move.Name).ToList()
            };

            return pokemon;
        }
    }

    public class PokeApiResponse
    {
        public string Name { get; set; }
        public List<TypeInfo> Types { get; set; }
        public Sprites Sprites { get; set; }
        public List<MoveInfo> Moves { get; set; }
    }

    public class TypeInfo
    {
        public Type Type { get; set; }
    }

    public class Type
    {
        public string Name { get; set; }
    }

    public class Sprites
    {
        public string FrontDefault { get; set; }
    }

    public class MoveInfo
    {
        public Move Move { get; set; }
    }

    public class Move
    {
        public string Name { get; set; }
    }
}
