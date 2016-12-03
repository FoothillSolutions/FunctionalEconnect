module GLTransaction 

type Series = All|Financial|Sales|Purchasing|Inventory|Payroll|Project
let getSeries= function
    | All -> 1
    | Financial -> 2
    | Sales -> 3
    | Purchasing ->4
    | Inventory ->5
    | Payroll -> 6
    | Project -> 7

type TransactionType = Regular|Reversing    
let GetTransactionType= function
    | Regular ->1
    | Reversing -> 2


type RateExpiration= None|Daily|Weekly|``Bi-Weekly``|Semiweekly|Monthly|Quarterly|Annually|Miscellaneous|None9
let GetRateExpiration = function 
    | None -> 0
    | Daily -> 1
    | Weekly -> 2
    | ``Bi-Weekly`` -> 3
    | Semiweekly -> 4
    | Monthly -> 5
    | Quarterly -> 6
    | Annually -> 7
    | Miscellaneous ->8
    | None9 -> 9


type TrxDefaultDate = ExactDate|NextDate|PreviousDate
let GetTrxDefaultDate = function
    | ExactDate -> 0
    | NextDate -> 1
    | PreviousDate -> 2

type RateCalculationMethod = Multiple|Divide
let GetRateCalculationMethod = function 
    | Multiple->1
    | Divide ->2


type DateLimits = Unlimited|Limited
let GetDateLimits = function
    | Unlimited-> 0
    | Limited -> 1

type ReqTrx = bool
let GetReqTrx = function
    | false-> 0
    | true -> 1


type LedgerID = Base|IFRS|Local
let GetLedgerID = function
    | Base-> 1
    | IFRS -> 2
    | Local -> 3

type AdjTrx  = bool
let GetAdjTrx= function
    | false-> 0
    | true -> 1