// ReSharper disable CheckNamespace
using System.Diagnostics;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// implicit conversions from dynamic to string/int/...
string stringValue = AppSettings<string>.Get.StringValue;
int intValue = AppSettings<int>.Get.IntValue;
Guid guidValue = AppSettings<Guid>.Get.GuidValue;
DateTime dateValue = AppSettings<DateTime>.Get.DateValue;
// cast
var dateVal = (DateTime)AppSettings<DateTime>.Get.DateValue;
var intVal = (int)AppSettings<int>.Get.IntValue;

Console.WriteLine($"string val: {stringValue}");
Console.WriteLine($"int val: {intValue}");
Console.WriteLine($"guid val: {guidValue}");
Console.WriteLine($"date val: {dateValue.ToShortDateString()}");

const int iterations = 10000;

Console.WriteLine($"Benchmarking get integer over {iterations} iterations:");

//benchmarking
var sw = new Stopwatch();
sw.Start();
for (var i = 0; i < iterations; i++)
{
	int val = AppSettings<int>.Get.IntVal;
}

sw.Stop();
Console.WriteLine($"\tDynamic config: {sw.ElapsedMilliseconds} ms / {sw.ElapsedTicks} ticks");

var config = Host.CreateDefaultBuilder().Build().Services.GetRequiredService<IConfiguration>();

sw.Reset();
sw.Start();
for (var i = 0; i < iterations; i++)
{
	var val = config.GetValue<int>("IntVal");
}

sw.Stop();
Console.WriteLine($"\tIConfiguration: {sw.ElapsedMilliseconds} ms / {sw.ElapsedTicks} ticks");