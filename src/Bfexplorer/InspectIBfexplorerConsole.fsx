#I @"C:\Program Files\BeloSoft\Bfexplorer\"
#r "BeloSoft.Data.dll"
#r "BeloSoft.Bfexplorer.Domain.dll"
#r "BeloSoft.Bfexplorer.Service.Core.dll"
#r "BeloSoft.Bfexplorer.FootballScoreProvider.dll"

open System
open System.Reflection
open BeloSoft.Bfexplorer.Service

let printIBfexplorerConsoleMembers () =
    let t = typeof<IBfexplorerConsole>
    let members = t.GetMembers(BindingFlags.Public ||| BindingFlags.Instance)
    members |> Array.sortBy (fun m -> m.Name) |> Array.iter (fun m -> 
        let returnType = 
            match m with
            | :? PropertyInfo as p -> p.PropertyType.FullName
            | :? MethodInfo as meth -> meth.ReturnType.FullName
            | _ -> ""
        printfn "%s (%A) : %s" m.Name m.MemberType returnType
    )

printIBfexplorerConsoleMembers() ;;