module ValidationTest
open FunctionalEconnect
open Expecto
open Validation
open Chessie.ErrorHandling

[<Tests>]
let checkLengthtestCorrectResult =
  testCase "Test string length less than x  with correct result" <| fun () ->
    let input = "Ahmed"
    let actual = checkLength 6 input 
    let expected = pass input
    Expect.equal actual expected "The result"
    

[<Tests>]
let checkLengthtestWrongResultTest =
  testCase "Test string length less than x  with wrong result" <| fun () ->
    let input = "Ahmed"
    let actual = checkLength 2 input 
    let expected = fail(sprintf "exceeds length of %i" 2)
    Expect.equal actual expected "The result  "

[<Tests>]
let failOnNullTest =
    testList "failOnNullTests group" [
      testCase "Test string isNullOrEmpty with correct result" <| fun () ->
        let input = "Tulkarm Palestine"
        let actual = failOnNull input 
        let expected = pass input
        Expect.equal actual expected "The result"
      testCase "Test string isNullOrEmpty with correct result" <| fun _ ->
        let input = ""
        let actual = failOnNull input 
        let expected = fail "Null or empty string" 
        Expect.equal actual expected "The result"
    ]