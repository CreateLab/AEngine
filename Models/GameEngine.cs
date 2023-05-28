namespace Models;

public class GameEngine
{
    private Game _game;

    private IGameElement? _currentElement;
    private Dictionary<string, IGameElement> _elements;
    private uint _currentTextIndex;

    public GameEngine(Game game)
    {
        _game = game;
        _elements = game.Elements.ToDictionary(x => x.Name);
        _currentElement = null;
        _currentTextIndex = 0;
    }

    public IResult MoveNext(string? next = null)
    {
        return !string.IsNullOrEmpty(next) ? MoveToElement(next) : MoveNextStep();
    }

    private IResult MoveNextStep()
    {
        switch (_currentElement)
        {
            case null:
            {
                _currentElement = _game.Elements[0];
                if (_currentElement is Scene scene)
                    return new ResultScene { Text = scene.Name, Images = GetImagePaths(scene.Images) };
                var dialog = _currentElement as Dialog;
                return   new ResultDialog
                {
                    Images = GetImagePaths(dialog.Images),
                    Question = dialog.Question,
                    Answers = dialog.Answers.Select(a => a.Text).ToList()
                };
            }

            case Scene scene when _currentTextIndex < scene.Textes.Count:
            {
                var text = scene.Textes[(int)_currentTextIndex++];
                return new ResultText { Text = text };
            }

            default:
            {
                return MoveNextElement();
            }
        }
    }

    private IResult MoveNextElement()
    {
        return _currentElement switch
        {
            Scene { IsEnd: true } => new EndResult(),
            Scene scene => MoveToElement(scene.Next),
            Dialog dialog => MoveToElement(dialog.Answers[0].Next),
            _ => throw new InvalidOperationException("Invalid game element type.")
        };
    }

    private IResult MoveToElement(string next)
    {
        _currentElement = _elements[next];
        _currentTextIndex = 0;

        return _currentElement switch
        {
            Scene scene => new ResultScene { Text = scene.Name, Images = GetImagePaths(scene.Images) },
            Dialog dialog => new ResultDialog
            {
                Images = GetImagePaths(dialog.Images),
                Question = dialog.Question,
                Answers = dialog.Answers.Select(a => a.Text).ToList()
            },
            _ => throw new InvalidOperationException("Invalid game element type.")
        };
    }
    

    private string[] GetImagePaths(List<Image> images)
    {
        return images?.Select(image => image.Path).ToArray();
    }
}