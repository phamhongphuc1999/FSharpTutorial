namespace MyPracticeTest.NumberService

open NUnit.Framework
open MyNumber.Service.Int

module IntServiceTest =
    [<Test>]
    [<TestCase("123", true)>]
    [<TestCase("-00123", true)>]
    [<TestCase("-123", true)>]
    [<TestCase("12.3", false)>]
    [<TestCase("123a", false)>]
    let IsIntTest (number: string) (expected: bool) =
        let check = IsInt number

        match expected with
        | true -> Assert.IsTrue(check)
        | _ -> Assert.IsFalse(check)

    [<Test>]
    [<TestCase("123", 1, "123")>]
    [<TestCase("-123", -1, "123")>]
    [<TestCase("-00123", -1, "00123")>]
    let GetUIntNumberTest (number: string) (expectedSign: int) (expectedInteger: string) =
        let (sign: int, num: string) = GetUIntNumber number

        (sign = expectedSign && num = expectedInteger)
        |> Assert.IsTrue

    [<Test>]
    [<TestCase("123", "123")>]
    [<TestCase("-00123", "-123")>]
    [<TestCase("-0", "0")>]
    let FormatIntTest (number: string) (expected: string) =
        let num = FormatInt number
        (num = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("123", 1, "123")>]
    [<TestCase("-123", -1, "123")>]
    [<TestCase("-00123", -1, "123")>]
    let DeepGetIntNumber (number: string) (expectedSign: int) (expectedInteger: string) =
        let (sign: int, num: string) = DeepGetIntNumber number

        (sign = expectedSign && num = expectedInteger)
        |> Assert.IsTrue

    [<Test>]
    [<TestCase("10", "-10", 1)>]
    [<TestCase("523", "529", -1)>]
    [<TestCase("-483", "-483", 0)>]
    let IntCompareTest (number1: string) (number2: string) (expected: int) =
        let check = IntCompare number1 number2
        (check = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("12", "-10", "2")>]
    [<TestCase("12456", "-47859878", "-47847422")>]
    [<TestCase("0", "1", "1")>]
    let AddIntTest (number1: string) (number2: string) (expected: string) =
        let result = (AddInt number1 number2) |> FormatInt
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("12", "-10", "22")>]
    [<TestCase("12456", "-478", "12934")>]
    [<TestCase("0", "1", "-1")>]
    let SubtractIntTest (number1: string) (number2: string) (expected: string) =
        let result = (SubtractInt number1 number2) |> FormatInt
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "-2", "-30")>]
    [<TestCase("-99", "-3", "297")>]
    [<TestCase("-99", "1234", "-122166")>]
    let MultipliedIntTest (number1: string) (number2: string) (expected: string) =
        let result = MultipliedInt number1 number2
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("15", "3", "5")>]
    [<TestCase("1234", "5", "246")>]
    [<TestCase("123456", "789", "156")>]
    let DivideIntTest (number1: string) (number2: string) (expected: string) =
        let result = DivideInt number1 number2
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("-15", "3", "-15000")>]
    [<TestCase("12", "10", "120000000000")>]
    let MultipliedInt10Test (number1: string) (number2: string) (expected: string) =
        let result = MultipliedInt10 number1 number2
        (result = expected) |> Assert.IsTrue

    [<Test>]
    [<TestCase("-15", "3", "-3375")>]
    [<TestCase("-12", "10", "61917364224")>]
    let PowIntTest (number1: string) (number2: string) (expected: string) =
        let result = PowInt number1 number2
        (result = expected) |> Assert.IsTrue
