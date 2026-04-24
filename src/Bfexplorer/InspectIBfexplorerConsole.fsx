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
        let aReturnType = 
            match m with
            | :? PropertyInfo as p -> Some p.PropertyType
            | :? MethodInfo as meth -> Some meth.ReturnType
            | _ -> None

        aReturnType 
        |> Option.iter (fun returnType-> printfn "%s (%A) : %A" m.Name m.MemberType returnType)
    )

printIBfexplorerConsoleMembers()