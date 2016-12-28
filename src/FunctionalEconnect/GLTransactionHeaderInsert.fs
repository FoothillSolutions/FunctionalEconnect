module GLTransactionHeaderInsert

(*type GLTrxHeader = {BatchNumber:String15;JournalEntry:Int4;Reference:String30;TransactionDate:DateTime;ReversingDate:DateTime option ;TransactionType:TransactionType;SequenceLine:int option; Series:Series option;CurrencyID:String15 option;ExchangeRate:int option;
    RateTypeId:String15 option; ExpirationDate:DateTime option; ExchangeDate:DateTime option; RateExpiration:RateExpiration option ; DaysToIncrement:Int2 option;RateVariance: int option; TransactionDateDefault: TrxDefaultDate option;
    RateCalculationMethod:RateCalculationMethod option; PerviousDayLimits:Int2 option; DateLimits:DateLimits option; Time1:DateTime option ; RequestTransaction:ReqTrx;SourceDocument:String11 option;LedgerID:LedgerID option ;
    UserId:String15 option; AdjustmentTransaction:AdjTrx option; NoteText:String8k option}
*)

open Microsoft.Dynamics.GP.eConnect;
open Microsoft.Dynamics.GP.eConnect.Serialization;
open Chessie.ErrorHandling
open Validation;
open System
type GLTransactionHeaderInsert = private GLTransactionHeaderInsert of taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert 
module op1 =
// partial function for length alidation
    let ValidateStringLength (len:int) =  checkLength len

    let validateBatch  =  warnOnNull >> bind (ValidateStringLength 15)
    let BatchValidaton (inp:taGLTransactionHeaderInsert) = validateBatch inp.BACHNUMB
    let validateAccountNumber str = lift Some ((ValidateStringLength 11 str))



    let createGLTransactionLine (x:taGLTransactionHeaderInsert) : Result<GLTransactionHeaderInsert ,string> =
            trial {        
            let! listRes =                      
                [BatchValidaton]
                |>  List.map(fun f -> f x)
                |>collect
            let z = taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert()
            //z.BACHNUMB<- x.Batch
            return GLTransactionHeaderInsert z
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

module op2 =
        //open GLTransaction 
        open System
        type GLHeader (batchNumber:string,journalEntryNumber:int,reference:string,transactionDate:DateTime, transactionType:int)=
            let validateStringLength (len:int) =  checkLength len
            
            let validateProperty fatalValidation errorValidation   x=  trial { 
                // required to fail fast if result is null
               let! fail= fatalValidation x 
               let! results=             
                    errorValidation 
                    |> List.map(fun f -> f x ) 
                    |>collect
                    
            return results.Head
                }
            member val batchNumber=batchNumber |> validateProperty failOnNull [(validateStringLength 10) ;(validateStringLength 111) ;(validateStringLength 12)]                                                       
            member val JournalEntryNumber= journalEntryNumber 
            member val Reference= reference
            member val TransactionDate = transactionDate
            member val TransactionType= transactionType
        

        let x= new GLHeader(null,12,"ref",DateTime.Now,1)  
        