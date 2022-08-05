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
