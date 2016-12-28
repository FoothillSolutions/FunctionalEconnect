module Validation
open System
open Chessie.ErrorHandling

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
    if String.IsNullOrEmpty(s) then
        fail "Null or empty string" 
    else
        s|> pass 
// Check empty or null and return string or warn message
let warnOnNull s = 
    if String.IsNullOrEmpty(s) then
        warn "Null or empty string" <| s
    else
        s|> pass         