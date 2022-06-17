# Dynamic.System.Config

A dead simple way to read both Environment and 
appSettings.json values
dynamically. Works great in Console apps and 
smaller, quicker applications that won't benefit from
the overheade of typed classes read from 
IConfig.

## Nuget

Install from Nuget with

``` 
nuget install Dynamic.System.Config
```

## Usage

The code uses the `System` namespace, so no specific `usings` 
should be necessary.

Install the package (se above), then simply get the value
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
