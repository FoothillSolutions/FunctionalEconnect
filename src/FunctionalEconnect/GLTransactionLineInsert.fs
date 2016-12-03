module GLTransactionLineInsert
    open Microsoft.Dynamics.GP.eConnect;
    open Microsoft.Dynamics.GP.eConnect.Serialization;
    open Chessie.ErrorHandling    
    open System
    open Validation
    type G = taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert
    type T = private GLTransactionLineInsert of taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert 

    let c = PMTransactionType()
    c.taPMTransactionInsert <- taPMTransactionInsert()
    
    let c2 = PMTransactionType()
    c2.taPMTransactionInsert <- taPMTransactionInsert()
    
    let foo= eConnectType()
    foo.PMTransactionType <- [|c;c2|]
    
    let validateBatchNotNull  = genericValidator failOnNull (fun (x:G)-> x.BACHNUMB ) 
    let validateBatchLength  = genericValidator (checkLength 15) (fun (x:G)-> x.BACHNUMB ) 
    let validateAccountNumber = genericValidator (checkLength 11 ) (fun (x:G)-> x.ACTNUMST )

    

    let createGLTransactionLine (x:G) : Result<T ,string> =
            trial { 
                let! notNull = validateBatchNotNull x       
                let! res =
                   [validateBatchLength;validateAccountNumber]
                   |> List.map(fun f -> f x)                    
                   |> collect
                   |> (lift List.head)
                return GLTransactionLineInsert res          
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
   


