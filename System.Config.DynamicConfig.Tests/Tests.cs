using Xunit;
using Xunit.Abstractions;

namespace System.Config.DynamicConfig.Tests;

public class Tests
{
    public Tests(ITestOutputHelper testOutputHelper)
    {
        Console = testOutputHelper;
    }

    private ITestOutputHelper Console { get; }

    [Fact]
    public void GetTypedAppSettings()
    {
        // implicit conversions from dynamic to string/int/...
        var stringValue = AppSettings.Get<string>().StringValue;
        var intValue = AppSettings.Get<int>().IntValue;
        var guidValue = AppSettings.Get<Guid>().GuidValue;
        var dateValue = AppSettings.Get<DateTime>().DateValue;

        Console.WriteLine($"string val: {stringValue}");
        Console.WriteLine($"int val: {intValue}");
        Console.WriteLine($"int val+2: {intValue + 2}");
        Console.WriteLine($"guid val: {guidValue}");
        Console.WriteLine($"date val: {dateValue.ToShortDateString()}");

        Assert.Equal("The string", stringValue);
        Assert.Equal(42, intValue);
        Assert.Equal(42 + 2, intValue + 2);
        Assert.Equal("C92A574B-98E8-4371-A193-C86F601433DC",
            guidValue.ToString().ToUpperInvariant());
        Assert.Equal(new DateTime(2022, 6, 11), dateValue);
    }


    [Fact]
    public void GetTypedConfig()
    {
        // implicit conversions from dynamic to string/int/...
        var stringValue = Config<string>.Get.StringValue;
        var intValue = Config<int>.Get.IntValue;
        var guidValue = Config<Guid>.Get.GuidValue;
        var dateValue = Config<DateTime>.Get.DateValue;

        Console.WriteLine($"string val: {stringValue}");
        Console.WriteLine($"int val: {intValue}");
        Console.WriteLine($"int val+2: {intValue + 2}");
        Console.WriteLine($"guid val: {guidValue}");
        Console.WriteLine($"date val: {dateValue.ToShortDateString()}");

        Assert.Equal("The string", stringValue);
        Assert.Equal(42, intValue);
        Assert.Equal(42 + 2, intValue + 2);
        Assert.Equal("C92A574B-98E8-4371-A193-C86F601433DC",
            guidValue.ToString().ToUpperInvariant());
        Assert.Equal(new DateTime(2022, 6, 11), dateValue);
    }

    [Fact]
    public void GetTypedEnvironmentVariablesViaAppSettings()
    {
        string path = AppSettings.Get<string>().Path;
        Assert.NotNull(path);
    }

    [Fact]
    public void GetTypedEnvironmentVariablesViaConfig()
    {
        string path = Config<string>.Get.Path;
        Assert.NotNull(path);
    }
}