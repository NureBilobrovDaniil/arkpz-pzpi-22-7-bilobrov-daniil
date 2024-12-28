Public Function CalculateTotalAmount(principal As Double, rate As Double, years As Integer) As Double
    ' Логіка обчислення складних відсотків
    Dim total As Double = principal * Math.Pow((1 + rate / 100), years)
    
    ' Логіка округлення
    total = Math.Round(total, 2)
    
    ' Логіка виведення
    Console.WriteLine("The total amount is: " & total.ToString("F2"))
    
    Return total
End Function 



' Основна функція, що обчислює суму
Public Function CalculateTotalAmount(principal As Double, rate As Double, years As Integer) As Double
    ' Викликаємо новий метод для обчислення складних відсотків
    Dim total As Double = CalculateCompoundInterest(principal, rate, years)
    
    ' Округлюємо результат
    total = RoundAmount(total)
    
    ' Виводимо результат
    DisplayAmount(total)
    
    Return total
End Function

' Новий метод для обчислення складних відсотків
Private Function CalculateCompoundInterest(principal As Double, rate As Double, years As Integer) As Double
    Return principal * Math.Pow((1 + rate / 100), years)
End Function

' Метод для округлення значення
Private Function RoundAmount(amount As Double) As Double
    Return Math.Round(amount, 2)
End Function

' Метод для виведення результату
Private Sub DisplayAmount(amount As Double)
    Console.WriteLine("The total amount is: " & amount.ToString("F2"))
End Sub
