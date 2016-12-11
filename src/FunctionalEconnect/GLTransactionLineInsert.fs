module GLTransactionLineInsert
    open Microsoft.Dynamics.GP.eConnect;
    open Microsoft.Dynamics.GP.eConnect.Serialization;
    open Chessie.ErrorHandling    
    open System
    type T = private GLTransactionLineInsert of taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert 

    //move these into the Econnect helper class.
    let checkLength len (str:string) = 
        if(str.Length > len ) then 
            let s = String.Format("exeeded max length {0}", len)
            fail s //<|String15 s
        else 
            pass <| str
    let c = PMTransactionType()
    c.taPMTransactionInsert <- taPMTransactionInsert()
    
    let c2 = PMTransactionType()
    c2.taPMTransactionInsert <- taPMTransactionInsert()
    
    let foo= eConnectType()
    foo.PMTransactionType <- [|c;c2|]

    // partial function for length alidation
    let ValidateStringLength (len:int) =  checkLength len

    let validateBatch  =  stringEmptyNullWarn >> bind (ValidateStringLength 15)
    let BatchValidaton (inp:InpSchema) = validateBatch inp.Batch
    let validateAccountNumber str = lift Some ((ValidateStringLength 11 str))



    let createGLTransactionLine x : Result<T ,string> =
            trial {        
            let! listRes =                      
                [BatchValidaton]
                |>  List.map(fun f -> f x)
                |>collect
            let z = taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert()
            z.BACHNUMB<- x.Batch
            return GLTransactionLineInsert z
            //{bat=x.Batch;SourceDocument= option.None;``Discribution Reference``=x.``Discribution Reference`` ;AccountNumber= x.AccountNumber;JERef=x.``Discribution Reference``;Date=x.Date;``DIVISION AA CODE``=x.``DIVISION AA CODE``;``PROFIT CENTER AA CODE``=x.``PROFIT CENTER AA CODE``; Project=x.Project; Deal=x.Deal; Amount=x.Amount}  
        }
    
    // look into taking in rules.

    // look into taking in a map function


    (*
    let createWithCont success failure (s:string) = 
        if System.Text.RegularExpressions.Regex.IsMatch(s,@"^\S+@\S+\.\S+$") 
            then success (EmailAddress s)
            else failure "Email address must contain an @ sign"

    // create directly
    let create s = 
        let success e = Some e
        let failure _  = None
        createWithCont success failure s

    // unwrap with continuation
    let apply f (EmailAddress e) = f e

    // unwrap directly
    let value e = apply id e
    
    *)
   


