namespace Models;

public class Image
{
    public string Name { get; set; }
    public string Path { get; set; }
}

public interface IGameElement
{
    string Type { get; set; }
    string Name { get; set; }
    List<Image> Images { get; set; }
}

public class Scene : IGameElement
{
    public string Type { get; set; }
    public string Name { get; set; }
    public List<Image> Images { get; set; }
    public List<string> Textes { get; set; }
    public string Next { get; set; }
    public bool IsEnd { get; set; }
}

public class Dialog : IGameElement
{
    public string Type { get; set; }
    public string Name { get; set; }
    public List<Image> Images { get; set; }
    public string Question { get; set; }
    public List<Answer> Answers { get; set; }
}

public class Answer
{
    public string Text { get; set; }
    public string Next { get; set; }
}

public class Game
{
    public string Name { get; set; }
    public List<IGameElement> Elements { get; set; }
}