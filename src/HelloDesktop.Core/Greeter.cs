namespace HelloDesktop.Core;

/// <summary>The "business logic" of the demo: it knows nothing about windows, so it is easy to test.</summary>
public static class Greeter
{
    public static string Greet(string? name, DateTimeOffset now)
    {
        var who = string.IsNullOrWhiteSpace(name) ? "world" : name.Trim();
        var part = now.Hour switch
        {
            < 5 => "Good night",
            < 12 => "Good morning",
            < 18 => "Good afternoon",
            _ => "Good evening",
        };
        return $"{part}, {who}!";
    }
}
