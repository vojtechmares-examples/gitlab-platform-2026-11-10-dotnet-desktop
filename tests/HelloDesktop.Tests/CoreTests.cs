using System.ComponentModel;
using HelloDesktop.Core;

namespace HelloDesktop.Tests;

public class GreeterTests
{
    [Theory]
    [InlineData(2, "Good night, Ada!")]
    [InlineData(9, "Good morning, Ada!")]
    [InlineData(14, "Good afternoon, Ada!")]
    [InlineData(21, "Good evening, Ada!")]
    public void The_greeting_depends_on_the_time_of_day(int hour, string expected)
    {
        var now = new DateTimeOffset(2026, 11, 10, hour, 0, 0, TimeSpan.Zero);

        Assert.Equal(expected, Greeter.Greet("Ada", now));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void A_missing_name_greets_the_world(string? name)
    {
        var now = new DateTimeOffset(2026, 11, 10, 10, 0, 0, TimeSpan.Zero);

        Assert.Equal("Good morning, world!", Greeter.Greet(name, now));
    }

    [Fact]
    public void The_name_is_trimmed()
    {
        var now = new DateTimeOffset(2026, 11, 10, 10, 0, 0, TimeSpan.Zero);

        Assert.Equal("Good morning, Ada!", Greeter.Greet("  Ada  ", now));
    }
}

public class GreetingViewModelTests
{
    private static readonly DateTimeOffset Noon = new(2026, 11, 10, 13, 0, 0, TimeSpan.Zero);

    [Fact]
    public void Greet_updates_the_greeting_and_counts()
    {
        var vm = new GreetingViewModel(() => Noon) { Name = "Grace" };

        vm.Greet();
        vm.Greet();

        Assert.Equal("Good afternoon, Grace!", vm.Greeting);
        Assert.Equal(2, vm.Greetings);
    }

    [Fact]
    public void Changing_a_property_raises_PropertyChanged()
    {
        var vm = new GreetingViewModel(() => Noon);
        var changed = new List<string?>();
        vm.PropertyChanged += (_, e) => changed.Add(e.PropertyName);

        vm.Name = "Linus";
        vm.Greet();

        Assert.Equal(["Name", "Greeting", "Greetings"], changed);
    }

    [Fact]
    public void Setting_the_same_value_raises_nothing()
    {
        var vm = new GreetingViewModel { Name = "same" };
        var raised = false;
        ((INotifyPropertyChanged)vm).PropertyChanged += (_, _) => raised = true;

        vm.Name = "same";

        Assert.False(raised);
    }
}

public class PlatformInfoTests
{
    [Fact]
    public void Describe_names_the_system_and_the_runtime()
    {
        var text = PlatformInfo.Describe();

        Assert.Contains("HelloDesktop", text);
        Assert.Contains(PlatformInfo.OperatingSystem, text);
        Assert.Contains(".NET", text);
    }

    [Fact]
    public void The_version_comes_from_the_assembly()
    {
        Assert.Matches(@"^\d+\.\d+\.\d+", PlatformInfo.AppVersion);
    }
}
