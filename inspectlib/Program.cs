using System;
using System.Reflection;

var path = "C:\Users\y1dar\dotnet-migrate\legacy-webforms-migration\src\LegacySharedAssembly\bin\Debug\LegacySharedAssembly.dll";
var a = Assembly.LoadFrom(path);
foreach (var t in a.GetTypes())
    Console.WriteLine(t.FullName);
