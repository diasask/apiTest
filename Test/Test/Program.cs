
using Newtonsoft.Json.Linq;

class Program
{

    private static int currentPokemonId = 1;
    private static readonly HttpClient client = new HttpClient();

    static async Task Main(string[] args)
    {
        while (true)
        {
            await DisplayPokemon(currentPokemonId);
            Console.WriteLine("Введите 'next' для следующего покемона, 'back' для предыдущего покемона или 'exit' для выхода:");
            string command = Console.ReadLine();
            if (command.ToLower() == "back" && currentPokemonId > 1)
            {
                currentPokemonId--;
            }
            else if (command.ToLower() == "next")
            {
                currentPokemonId++;
            }
            else if (command.ToLower() == "exit")
            {
                break;
            }
            else
            {
                Console.WriteLine("Неверная команда. Пожалуйста, попробуйте снова.");
            }
        }
    }
    private static async Task DisplayPokemon(int id)
    {
        try
        {
            string url = $"https://pokeapi.co/api/v2/pokemon/{id}";

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            JObject pokemonData = JObject.Parse(jsonResponse);
            string name = pokemonData["name"].ToString();
            int height = (int)pokemonData["height"];
            int weight = (int)pokemonData["weight"];
            int pokemonId = (int)pokemonData["id"];
            var abilities = pokemonData["abilities"];
            Console.WriteLine($"ID: {pokemonId}");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Height: {height}");
            Console.WriteLine($"Weight: {weight}");
            Console.WriteLine("Abilities:");
            foreach (var ability in abilities)
            {
                string abilityName = ability["ability"]["name"].ToString();
                Console.WriteLine($"-{abilityName}");

            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Ошибка при получении данных: {e.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }


    }


}
