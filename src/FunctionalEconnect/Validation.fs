module Validation
open System
open Chessie.ErrorHandling
open FSharp.Core

let genericValidator validator getValue elem= 
    trial{
     let! res = validator <| getValue elem 
     return elem
    } 
    

let checkLength length x = 
    if x.ToString().Length > length then  
        fail  (sprintf "exeeds length of %i" length)
    else 
        pass x

let failOnNull s = 
    if isNull s then
        fail "Null or empty string" 
    else
        s|> pass 

let failOnDefaultValue<'T when 'T:equality> (x:'T) =
    let foo =Microsoft.FSharp.Core.Operators.Unchecked.defaultof<'T>
    if x = foo then
        fail "defaut value not allowed"
    else
        pass x

let failOnEmptyString s = 
    if String.IsNullOrEmpty s then
        fail "Null or empty string" 
    else
        s|> pass 



// Check empty or null and return string or warn message
let warnOnNull s = 
    if String.IsNullOrEmpty(s) then
        warn "Null or empty string" <| s
    else
        s|> pass         