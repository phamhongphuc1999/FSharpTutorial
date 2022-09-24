namespace MyPracticeTest.NumberServiceTest

open NUnit.Framework
open MyNumber.Service.Decimal

module DecimalServiceTest =
    [<Test>]
    [<TestCase("123", 1, "123", "0")>]
    [<TestCase("-123.9", -1, "123", "9")>]
    [<TestCase("-123", -1, "123", "0")>]
    let GetIntegerAndDecimalTest (number: string) (signExpected: int) (intExpected: string) (decExpected: string) =
        let (sign: int, integer: string, decimal: string) = GetIntegerAndDecimal number

        (sign = signExpected
         && integer = intExpected
         && decimal = decExpected)
        |> Assert.IsTrue

    [<Test>]
    [<TestCase("0012300.12300", "12300.123")>]
    [<TestCase("-00123.123", "-123.123")>]
    let FormatDecimalTest (number: string) (expected: string) =
        let num = FormatDecimal number
        num = expected |> Assert.IsTrue

    [<Test>]
    [<TestCase("123", 1, "123", "0")>]
    [<TestCase("-123.9", -1, "123", "9")>]
    [<TestCase("-123", -1, "123", "0")>]
    let DeepGetIntegerAndDecimalTest (number: string) (signExpected: int) (intExpected: string) (decExpected: string) =
        let (sign: int, integer: string, decimal: string) = DeepGetIntegerAndDecimal number

        (sign = signExpected
         && integer = intExpected
         && decimal = decExpected)
        |> Assert.IsTrue

    [<Test>]
    [<TestCase("123", true)>]
    [<TestCase("123.456", true)>]
    [<TestCase("0123.45a", false)>]
    [<TestCase("-123.4500", true)>]
    let IsDecimalTest (number: string) (expected: bool) =
        let num = IsDecimal number

        match num with
        | true -> Assert.IsTrue(expected)
        | _ -> Assert.IsFalse(expected)
