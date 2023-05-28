using System.Text.Json;

namespace Models;

public static class GameBuilder
{
    public static GameEngine Build(string pathToFile)
    {
        var json = File.ReadAllText(pathToFile);
        var options = new JsonSerializerOptions();
        options.Converters.Add(new GameElementConverter());
        var game = JsonSerializer.Deserialize<Game>(json, options);
        return new GameEngine(game);
    }
}