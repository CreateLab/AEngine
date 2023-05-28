namespace Models;

public interface IResult
{
}

public class ResultText : IResult
{
    public string Text { get; set; }
}

public class ResultScene : IResult
{
    public string Text { get; set; }
    public string[] Images { get; set; }
}

public class ResultDialog : IResult
{
    public string[] Images { get; set; }
    public string Question { get; set; }
    public List<string> Answers { get; set; }
}

public class EndResult : IResult
{
}