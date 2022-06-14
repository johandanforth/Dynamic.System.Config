# System.AppSettings

A dead simple way to read appSettings.json values
dynamically. Works great in Console apps and 
smaller, quicker applications that doesn't need 
the overheade of typed classes read from 
IConfig.

## Nuget

Install from Nuget with

``` 
nuget install System.AppSettings
```

## Usage

Install the package, then just get the value
from `System.AppSettings<T>` like so:

``` csharp
// implicit conversions from dynamic to string/int/...
string stringValue = AppSettings<string>.Get.StringValue;
int intValue = AppSettings<int>.Get.IntValue;
Guid guidValue = AppSettings<Guid>.Get.GuidValue;
DateTime dateValue = AppSettings<DateTime>.Get.DateValue;

// cast
var dateVal = (DateTime)AppSettings<DateTime>.Get.DateValue;
var intVal = (int)AppSettings<int>.Get.IntValue;
```
