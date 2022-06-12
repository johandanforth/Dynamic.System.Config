using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;
using Xunit.Abstractions;

namespace System.AppSettings.DynamicConfig.Tests
{
    public class Tests
    {
	    private readonly ITestOutputHelper _testOutputHelper;
	    public Tests(ITestOutputHelper testOutputHelper)
	    {
		    _testOutputHelper = testOutputHelper;
	    }

	    [Fact]
	    public void ReadValues()
	    {
		    // implicit conversions from dynamic to string/int/...
		    string stringValue = AppSettings<string>.Get.StringValue;
		    int intValue = AppSettings<int>.Get.IntValue;
		    Guid guidValue = AppSettings<Guid>.Get.GuidValue;
		    DateTime dateValue = AppSettings<DateTime>.Get.DateValue;

		    _testOutputHelper.WriteLine($"string val: {stringValue}");
		    _testOutputHelper.WriteLine($"int val: {intValue}");
		    _testOutputHelper.WriteLine($"guid val: {guidValue}");
		    _testOutputHelper.WriteLine($"date val: {dateValue.ToShortDateString()}");

			Assert.Equal("The string",stringValue);
			Assert.Equal(42,intValue);
			Assert.Equal("C92A574B-98E8-4371-A193-C86F601433DC", guidValue.ToString().ToUpperInvariant());
			Assert.Equal("2022-06-11", dateValue.ToShortDateString());

            // cast
            var dateVal = (DateTime)AppSettings<DateTime>.Get.DateValue;
		    var intVal = (int)AppSettings<int>.Get.IntValue;

		    _testOutputHelper.WriteLine($"int val: {intVal}");
		    _testOutputHelper.WriteLine($"date val: {dateVal.ToShortDateString()}");

		    Assert.Equal(42, intVal);
		    Assert.Equal("2022-06-11", dateVal.ToShortDateString());


        }
    }
}
