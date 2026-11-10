using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HelloDesktop.Core;

/// <summary>What the window binds to. No UI types, so the tests run without a display.</summary>
public sealed class GreetingViewModel(Func<DateTimeOffset>? clock = null) : INotifyPropertyChanged
{
    private readonly Func<DateTimeOffset> _clock = clock ?? (() => DateTimeOffset.Now);
    private string _name = "";
    private string _greeting = "Type a name and press the button.";
    private int _greetings;

    public event PropertyChangedEventHandler? PropertyChanged;

    public string Name { get => _name; set => Set(ref _name, value); }

    public string Greeting { get => _greeting; private set => Set(ref _greeting, value); }

    public int Greetings { get => _greetings; private set => Set(ref _greetings, value); }

    public string Platform => PlatformInfo.Describe();

    public void Greet()
    {
        Greeting = Greeter.Greet(Name, _clock());
        Greetings++;
    }

    private void Set<T>(ref T field, T value, [CallerMemberName] string? property = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
}
