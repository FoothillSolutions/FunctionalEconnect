module GLTransactionLineInsert
    open Microsoft.Dynamics.GP.eConnect;
    open Microsoft.Dynamics.GP.eConnect.Serialization;
    open Chessie.ErrorHandling    
    open System
    open Validation
    type G = taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert
    type T = private GLTransactionLineInsert of taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert 


    
    let validateBatchNotNull  = genericValidator (failOnNull)  (fun (x:G)-> x.BACHNUMB) 
    let validateBatchLength  = genericValidator (checkLength 15) (fun (x:G)-> x.BACHNUMB ) 

    let validateAccountLength  (inp:taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert) = genericValidator (checkLength 15) (fun (x:G)-> x.ACTNUMST ) 

    let validateAccountNumberLength = genericValidator (checkLength 11 ) (fun (x:G)-> x.ACTNUMST )

    let validateDescriptionLength = genericValidator (checkLength 30) (fun (x:G)-> x.DSCRIPTN) 

    let validateOriginatingControlNumberLength = genericValidator (checkLength 20) (fun (x:G)-> x.ORCTRNUM) 
    
    let validateOriginatingDocumentNumberLength = genericValidator (checkLength 20) (fun (x:G)-> x.ORDOCNUM)
    
    let validateOriginatingMasterIDLength = genericValidator (checkLength 30)   (fun (x:G)-> x.ORMSTRID) 
    
    let validateOriginatingMasterNameLength = genericValidator (checkLength 64)   (fun (x:G)-> x.ORMSTRID) 
    
    let validateOriginatingTransactionDescriptionLength = genericValidator (checkLength 30)   (fun (x:G)-> x.ORTRXDESC) 
    
    let validateTaxDetailIDLength = genericValidator (checkLength 15) (fun (x:G)-> x.TAXDTLID)
    
    let validateTaxAcountStringLength = genericValidator (checkLength 75) (fun (x:G)-> x.TAXACTNUMST)
    
    let validateCurrencyIDLength = genericValidator (checkLength 15) (fun (x:G)-> x.TAXACTNUMST)
    
    let validateRateTypeIDLength  = genericValidator (checkLength 15) (fun (x:G)-> x.RATETPID)
    
    let validateExchangeIDDescriptionLength  = genericValidator (checkLength 30) (fun (x:G)-> x.EXGTBDSC)
    
    let validateExchangeRateSourceLength  = genericValidator (checkLength 50) (fun (x:G)-> x.EXTBLSRC)
    
    let validateUserDefinedField1Length  = genericValidator (checkLength 50) (fun (x:G)-> x.USRDEFND1)
    
    let validateUserDefinedField2Length = genericValidator (checkLength 50) (fun (x:G)-> x.USRDEFND2)
    
    let validateUserDefinedField3Length = genericValidator (checkLength 50) (fun (x:G)-> x.USRDEFND3)
    
    let validateUserDefinedField4Length  = genericValidator (checkLength 8000) (fun (x:G)-> x.USRDEFND4)
    
    let validateUserDefinedField5Length  = genericValidator (checkLength 8000) (fun (x:G)-> x.USRDEFND5)
    
    let validateDocumentDateLength  = genericValidator (checkLength 23) (fun (x:G)-> x.DOCDATE)
    
    let validateExpirationDateLength  = genericValidator (checkLength 23) (fun (x:G)-> x.EXPNDATE)
    
    let validateExchangeDateLength  = genericValidator (checkLength 23) (fun (x:G)-> x.EXCHDATE)
    
    let validateTime1Length  = genericValidator (checkLength 23) (fun (x:G)-> x.TIME1)



    let createGLTransactionLine (x:G) : Result<T ,string> =
            trial { 
                let! notNull = validateBatchNotNull x       
                let! res =
                   [validateBatchLength;validateAccountNumberLength]
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
   


