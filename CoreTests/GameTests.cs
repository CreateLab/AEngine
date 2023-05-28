using System.Text.Json;
using Models;

namespace CoreTests;

public class GameTests
{
    const string testJson =

        #region Json

        @"{
    ""Name"": ""Game1"",
    ""Elements"": [
        {
            ""Type"": ""Scene"",
            ""Name"": ""GameScene"",
            ""Images"": [
                {
                    ""Name"": ""Background"",
                    ""Path"": ""Images/Background.png""
                },
                {
                    ""Name"": ""Player"",
                    ""Path"": ""Images/Player.png""
                },
                {
                    ""Name"": ""Enemy"",
                    ""Path"": ""Images/Enemy.png""
                }
            ],
            ""Textes"": [
                ""Hello World!"",
                ""Hello World!""
            ],
            ""Next"": ""New Scene""
        },
        {
            ""Type"": ""Scene"",
            ""Name"": ""New Scene"",
            ""Images"": [
                {
                    ""Name"": ""Background"",
                    ""Path"": ""Images/Background.png""
                },
                {
                    ""Name"": ""Player"",
                    ""Path"": ""Images/Player.png""
                },
                {
                    ""Name"": ""Enemy"",
                    ""Path"": ""Images/Enemy.png""
                }
            ],
            ""Textes"": [
                ""Hello World!"",
                ""Hello World!""
            ],
            ""Next"": ""Variante Scene""
        },
        {
            ""Type"": ""Dialog"",
            ""Name"": ""Variante Scene"",
            ""Images"": [
                {
                    ""Name"": ""Background"",
                    ""Path"": ""Images/Background.png""
                },
                {
                    ""Name"": ""Player"",
                    ""Path"": ""Images/Player.png""
                },
                {
                    ""Name"": ""Enemy"",
                    ""Path"": ""Images/Enemy.png""
                }
            ],
            ""Question"": ""What Scene wanna go?"",
            ""Answers"": [
                {
                    ""Text"": ""GameScene"",
                    ""Next"": ""GameScene""
                },
                {
                    ""Text"": ""New Scene"",
                    ""Next"": ""New Scene""
                },
                {
                    ""Text"": ""End"",
                    ""Next"": ""End""
                }
            ]
        },
        {
            ""Type"": ""Scene"",
            ""Name"": ""End"",
            ""Images"": [
                {
                    ""Name"": ""Background"",
                    ""Path"": ""Images/Background.png""
                },
                {
                    ""Name"": ""Player"",
                    ""Path"": ""Images/Player.png""
                },
                {
                    ""Name"": ""Enemy"",
                    ""Path"": ""Images/Enemy.png""
                }
            ],
            ""Textes"": [
                ""Hello World!"",
                ""Hello World!""
            ],
            ""IsEnd"": true
        }
    ]
}
                    ";

    #endregion

    [Fact]
    public void SerialisationTest()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new GameElementConverter());


        var game = JsonSerializer.Deserialize<Game>(testJson, options);

        Assert.Equal(4, game.Elements?.Count);
    }

    [Fact]
    public void CheckFirstScene()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new GameElementConverter());


        var game = JsonSerializer.Deserialize<Game>(testJson, options);
        var ge = new GameEngine(game);
        var sc = ge.MoveNext();
        var data = sc is ResultText;
        Assert.True(sc is ResultScene);
    }
    
}