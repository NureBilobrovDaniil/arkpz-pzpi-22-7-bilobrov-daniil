Public Function Clc(amount As Double, rate As Double, years As Integer) As Double
    Return amount * Math.Pow((1 + rate / 100), years)
End Function


Public Function CalculateCompoundInterest(principal As Double, annualRate As Double, timeInYears As Integer) As Double
    ' Формула обчислення складних відсотків: A = P * (1 + r/n)^(n*t)
    Return principal * Math.Pow((1 + annualRate / 100), timeInYears)
End Function

Sub Main()
    Dim principal As Double = 1000
    Dim annualRate As Double = 5
    Dim timeInYears As Integer = 10

    Dim totalAmount As Double = CalculateCompoundInterest(principal, annualRate, timeInYears)
    Console.WriteLine("Total Amount after " & timeInYears & " years: " & totalAmount.ToString("F2"))
End Sub
