using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace YourNamespace.Tests;

public class AppSettingsTests
{
    [Fact]
    public void ShouldGetAppSettingsDateValue()
    {
        var dateValue = AppSettings.Get<DateTime>().DateValue;
        Assert.Equal(new DateTime(2022, 6, 11), dateValue);
    }

    [Fact]
    public void ShouldGetAppSettingsGuidValue()
    {
        var guidValue = AppSettings.Get<Guid>().GuidValue;
        Assert.Equal("C92A574B-98E8-4371-A193-C86F601433DC",
            guidValue.ToString().ToUpperInvariant());
    }

    [Fact]
    public void ShouldGetAppSettingsIntValue()
    {
        var intValue = AppSettings.Get<int>().IntValue;
        Assert.Equal(42, intValue);
    }

    [Fact]
    public void ShouldGetAppSettingsIntValuePlusTwo()
    {
        var intValue = AppSettings.Get<int>().IntValue;
        Assert.Equal(42 + 2, intValue + 2);
    }

    [Fact]
    public void ShouldGetAppSettingsStringValue()
    {
        var stringValue = AppSettings.Get<string>().StringValue;
        Assert.Equal("The string", stringValue);
    }

    [Fact]
    public void ShouldThrowExceptionForMissingIntValue()
    {
        var settings = AppSettings.Get<int>();
        Assert.Throws<RuntimeBinderException>(() =>
        {
            var missingValue = settings.MissingInt;
        });
    }

    [Fact]
    public void ShouldThrowExceptionForMissingStringValue()
    {
        var settings = AppSettings.Get<string>();
        Assert.Throws<RuntimeBinderException>(() =>
        {
            var missingValue = settings.MissingString;
        });
    }

    [Fact]
    public void ShouldUseDefaultIntWhenKeyIsMissing()
    {
        var value = AppSettings.Get(99).MissingInt;
        Assert.Equal(99, value);
    }

    [Fact]
    public void ShouldUseDefaultStringWhenKeyIsMissing()
    {
        var value= AppSettings.Get("DefaultString").MissingString;
        Assert.Equal("DefaultString", value);
    }
}