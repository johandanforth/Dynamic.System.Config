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
var stringValue = Config<string>.Get.StringValue;
var intValue = Config<int>.Get.IntValue;
var guidValue = Config<Guid>.Get.GuidValue;
var dateValue = Config<DateTime>.Get.DateValue;

```
