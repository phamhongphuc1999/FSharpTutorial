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

    [<Test>]
    [<TestCase("10", "10", "0")>]
    [<TestCase("11", "10", "1")>]
    let SubtractUIntTest (number1: string) (number2: string) (expected: string) =
        let result = (SubtractUInt number1 number2) |> FormatUInt
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "2", "30")>]
    [<TestCase("99", "3", "297")>]
    [<TestCase("99", "1234", "122166")>]
    let MultiplyUIntTest (number1: string) (number2: string) (expected: string) =
        let result = MultiplyUInt number1 number2
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "3", "5")>]
    [<TestCase("1234", "5", "246")>]
    [<TestCase("123456", "789", "156")>]
    let DivideUIntTest (number1: string) (number2: string) (expected: string) =
        let result = DivideUInt number1 number2
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "3", "0")>]
    [<TestCase("1234", "5", "4")>]
    [<TestCase("123456", "789", "372")>]
    let DivideModUIntTest (number1: string) (number2: string) (expected: string) =
        let result = DivideModUInt number1 number2
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "3", "5", "0")>]
    [<TestCase("1234", "5", "246", "4")>]
    [<TestCase("123456", "789", "156", "372")>]
    let RealDivideUIntTest (number1: string) (number2: string) (a: string) (b: string) =
        let (a1: string, b1: string) = RealDivideUInt number1 number2
        (a1 = a && b1 = b) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "3", "15000")>]
    [<TestCase("12", "10", "120000000000")>]
    let MultiplyUInt10Test (number1: string) (number2: string) (expected: string) =
        let result = MultiplyUInt10 number1 number2
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "3", "3375")>]
    [<TestCase("12", "10", "61917364224")>]
    let PowUIntTest (number1: string) (number2: string) (expected: string) =
        let result = PowUInt number1 number2
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "5", "5")>]
    [<TestCase("5", "1", "1")>]
    let CalculateGreatestCommonFactorTest (number1: string) (number2: string) (expected: string) =
        let result = (CalculateGreatestCommonFactor number1 number2) |> FormatUInt
        (result = expected) |> Assert.IsTrue
