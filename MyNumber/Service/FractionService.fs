namespace MyNumber.Service

module Fraction =
    type Fraction(numerator: string, denominator: string) =
        let numerator = numerator
        let denominator = denominator

        override this.ToString() =
            if denominator = "1" then
                numerator
            else
                numerator + "/" + denominator
