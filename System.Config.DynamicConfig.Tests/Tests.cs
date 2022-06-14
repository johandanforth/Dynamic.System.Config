using Xunit;
using Xunit.Abstractions;


namespace System.Config.DynamicConfig.Tests;

public class Tests
{
	private ITestOutputHelper Console { get; }

	public Tests(ITestOutputHelper testOutputHelper)
	{
		Console = testOutputHelper;
	}
	

	[Fact]
	public void GetTypedEnvironmentVariables()
	{
		string path = Config<string>.Get.Path;
		Assert.NotNull(path);
	}

	
	[Fact]
	public void GetTypedAppSettings()
	{
		// implicit conversions from dynamic to string/int/...
		string stringValue = Config<string>.Get.StringValue;
		int intValue = Config<int>.Get.IntValue;
		Guid guidValue = Config<Guid>.Get.GuidValue;
		DateTime dateValue = Config<DateTime>.Get.DateValue;

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
		var dateVal = (DateTime)Config<DateTime>.Get.DateValue;
		var intVal = (int)Config<int>.Get.IntValue;

		Console.WriteLine($"int val: {intVal}");
		Console.WriteLine($"date val: {dateVal.ToShortDateString()}");

		Assert.Equal(42, intVal);
		Assert.Equal(new DateTime(2022, 6, 11), dateVal);
	}
}