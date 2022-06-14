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
from `System.Config<T>` like so:

``` csharp
// implicit conversions from dynamic to string/int/...
string stringValue = Config<string>.Get.StringValue;
int intValue = Config<int>.Get.IntValue;
Guid guidValue = Config<Guid>.Get.GuidValue;
DateTime dateValue = Config<DateTime>.Get.DateValue;

// cast
var dateVal = (DateTime)Config<DateTime>.Get.DateValue;
var intVal = (int)Config<int>.Get.IntValue;
```
