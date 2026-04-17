using System;
using System.IO;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        var basePath = @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows";
        var assemblyFiles = new[]
        {
            Path.Combine(basePath, "BeloSoft.Data.dll"),
            Path.Combine(basePath, "BeloSoft.Betfair.API.dll"),
            Path.Combine(basePath, "BeloSoft.Bfexplorer.API.dll"),
            Path.Combine(basePath, "BeloSoft.Bfexplorer.Host.dll"),
            Path.Combine(basePath, "BeloSoft.Bfexplorer.Domain.dll"),
            Path.Combine(basePath, "BeloSoft.Bfexplorer.Service.Core.dll"),
            Path.Combine(basePath, "BeloSoft.Bfexplorer.Service.dll"),
            Path.Combine(basePath, "BeloSoft.Bfexplorer.Interoperability.dll")
        };

        var outputPath = Path.Combine(basePath, "relevant-methods.txt");
        using var writer = new StreamWriter(outputPath, false);

        foreach (var path in assemblyFiles)
        {
            writer.WriteLine($"Assembly: {path}");
            var assembly = Assembly.LoadFrom(path);
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.FullName == "BeloSoft.Bfexplorer.Domain.Market")
                {
                    writer.WriteLine($"Market type: {type.FullName}");
                    foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.FlattenHierarchy))
                    {
                        writer.WriteLine($"Property: {prop.Name} -> {prop.PropertyType.FullName}");
                    }
                    foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
                    {
                        writer.WriteLine($"Method: {method.Name} returns {method.ReturnType.FullName}");
                    }
                }
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                {
                    if (method.Name == "GetMyBetfairCredentials" || method.Name == "getActiveSelections" || method.Name == "CreateBfexplorerApplicationClient" || method.Name == "Login" || method.Name == "GetAccountFunds" || method.Name == "GetMarket" || method.Name == "UpdateMarket")
                    {
                        writer.WriteLine($"{type.FullName}.{method.Name} returns {method.ReturnType.FullName}");
                    }
                }
            }
            writer.WriteLine();
        }
    }
}
