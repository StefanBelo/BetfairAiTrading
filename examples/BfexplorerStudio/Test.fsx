//(*
#I @"E:\Projects\Bfexplorer\Development\Applications\BeloSoft.Bfexplorer.App\bin\Debug\net10.0-windows\"
//#I @"C:\Program Files\BeloSoft\Bfexplorer\"

#r "BeloSoft.Bfexplorer.Domain.dll"
//*)

open System
open BeloSoft.Bfexplorer.Domain

let propertyType = typedefof<BetType>

Enum.Parse (propertyType, "Lay") :?> BetType
