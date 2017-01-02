module ValidationTest
open FunctionalEconnect
open Expecto
open Validation
open Chessie.ErrorHandling
open GLTransactionLineInsert
open Microsoft.Dynamics.GP.eConnect;
open Microsoft.Dynamics.GP.eConnect.Serialization;


[<Tests>]
let checkLengthTests =
    testList "failOnNullTests group" [
      testCase "Test string length less than x  with correct result" <| fun () ->
        let input = "Ahmed"
        let actual = checkLength 6 input 
        let expected = pass input
        Expect.equal actual expected "The result"
      testCase "Test string length less than x  with wrong result" <| fun () ->
        let input = "Ahmed"
        let actual = checkLength 2 input 
        let expected = fail(sprintf "exceeds length of %i" 2)
        Expect.equal actual expected "The result  "
    ]

[<Tests>]
let failOnNullTest =
    testList "failOnNullTests group" [
      testCase "Test string is null with correct result" <| fun () ->
        let input = "Tulkarm Palestine"
        let actual = failOnNull input 
        let expected = pass input
        Expect.equal actual expected "The result"
      testCase "Test string is null with wrong result" <| fun _ ->
        let input = null
        let actual = failOnNull input 
        let expected = fail "Null refernce" 
        Expect.equal actual expected "The result"
    ]
[<Tests>]
let failOnEmptyStringTest =
    testList "failOnEmptyString group" [
      testCase "Test string isNullOrEmpty with correct result" <| fun () ->
        let input = "Tulkarm Palestine"
        let actual = failOnEmptyString input 
        let expected = pass input
        Expect.equal actual expected "The result"
      testCase "Test string isNullOrEmpty with wrong result" <| fun _ ->
        let input = ""
        let actual = failOnEmptyString input 
        let expected = fail "Null or empty string" 
        Expect.equal actual expected "The result"
    ]
    //k2
[<Tests>]
let failOnDefaultValueTest =
    testList "failOnDefaultValueTest group" [
      testCase "Test is the value is default with correct decimal result" <| fun () ->
        let input = 5m
        let actual = failOnDefaultValue input 
        let expected = pass input
        Expect.equal actual expected "The result"
      testCase "Test is the value is default with wrong decimal result" <| fun _ ->
        let input = 0m
        let actual = failOnDefaultValue input 
        let expected = fail "defaut value not allowed" 
        Expect.equal actual expected "The result" 
      testCase "Test is the value is default with wrong int result" <| fun _ ->
        let input = 0
        let actual = failOnDefaultValue input 
        let expected = fail "defaut value not allowed" 
        Expect.equal actual expected "The result"
    ]

[<Tests>]
let WarnOnNullTest =
    testList "failOnNullTests group" [
      testCase "Test string isNullOrEmpty with correct result" <| fun () ->
        let input = "Tulkarm Palestine"
        let actual = warnOnNull input 
        let expected = pass input
        Expect.equal actual expected "The result"
      testCase "Test string isNullOrEmpty with wrong result" <| fun _ ->
        let input = ""
        let actual = warnOnNull input 
        let expected = warn "Null or empty string" <| input
        Expect.equal actual expected "The result"
    ]

[<Tests>]
let genericValidatorTest =
    testList "genericValidatorTest group" [
      testCase "Test the genericValidator method whith correct result with validator = failOnEmptyString and  batch number property" <| fun () ->        
        let gl = new  taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert()
        gl.BACHNUMB <- "sunTrust"
        let BatchValidaton = genericValidator (failOnEmptyString) (fun (x:G)-> x.BACHNUMB) 
        let actual = BatchValidaton  gl
        let expected = pass gl
        Expect.equal actual expected "The result"
      testCase "Test the genericValidator method whith wrong result with validator = failOnEmptyString and batch number property" <| fun () ->        
        let gl = new  taGLTransactionLineInsert_ItemsTaGLTransactionLineInsert()
        //gl.BACHNUMB <- ""
        let BatchValidaton = genericValidator (failOnNull)  (fun (x:G)-> x.BACHNUMB) 
        let actual = BatchValidaton  gl
        let expected = fail "Null refernce" 
        Expect.equal actual expected "The result"
    ]

    //GLTransactionLineInsert