namespace MyPracticeTest.NumberService

open NUnit.Framework
open MyNumber.Service.UInt

module UIntServiceTest =
    [<Test>]
    [<TestCase("123", true)>]
    [<TestCase("00123", true)>]
    [<TestCase("-123", false)>]
    [<TestCase("12.3", false)>]
    [<TestCase("123a", false)>]
    let IsUIntTest (number: string) (expected: bool) =
        let check = IsUInt number

        match expected with
        | true -> Assert.IsTrue(check)
        | _ -> Assert.IsFalse(check)

    [<Test>]
    [<TestCase("123", "123")>]
    [<TestCase("00123", "123")>]
    [<TestCase("0012300", "12300")>]
    let FormatUIntTest (number: string) (expected: string) =
        let fNum = FormatUInt number
        Assert.IsTrue(fNum.Equals(expected))

    [<Test>]
    [<TestCase("123", "123", 0)>]
    [<TestCase("123", "543", -1)>]
    [<TestCase("99", "123", -1)>]
    [<TestCase("11111", "99", 1)>]
    let UIntCompareTest (number1: string) (number2: string) (expected: int) =
        let check = UIntCompare number1 number2
        (check = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("123", "321", "444")>]
    [<TestCase("1", "0", "1")>]
    let AddUIntTest (number1: string) (number2: string) (expected: string) =
        let result = AddUInt number1 number2
        (result = expected) |> Assert.IsTrue
