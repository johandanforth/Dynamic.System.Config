using Xunit;
using Xunit.Abstractions;

namespace System.AppSettings.DynamicConfig.Tests;

public class Tests
{
	private ITestOutputHelper Console { get; }

	public Tests(ITestOutputHelper testOutputHelper)
	{
		Console = testOutputHelper;
	}

	[Fact]
	public void ReadValues()
	{
		// implicit conversions from dynamic to string/int/...
		string stringValue = AppSettings<string>.Get.StringValue;
		int intValue = AppSettings<int>.Get.IntValue;
		Guid guidValue = AppSettings<Guid>.Get.GuidValue;
		DateTime dateValue = AppSettings<DateTime>.Get.DateValue;

		Console.WriteLine($"string val: {stringValue}");
		Console.WriteLine($"int val: {intValue}");
		Console.WriteLine($"guid val: {guidValue}");
		Console.WriteLine($"date val: {dateValue.ToShortDateString()}");

		Assert.Equal("The string", stringValue);
		Assert.Equal(42, intValue);
		Assert.Equal("C92A574B-98E8-4371-A193-C86F601433DC",
			guidValue.ToString().ToUpperInvariant());

		Assert.Equal(new DateTime(2022, 6, 11), dateValue);

		// cast
		var dateVal = (DateTime)AppSettings<DateTime>.Get.DateValue;
		var intVal = (int)AppSettings<int>.Get.IntValue;

		Console.WriteLine($"int val: {intVal}");
		Console.WriteLine($"date val: {dateVal.ToShortDateString()}");

		Assert.Equal(42, intVal);
		Assert.Equal(new DateTime(2022, 6, 11), dateVal);
	}
}