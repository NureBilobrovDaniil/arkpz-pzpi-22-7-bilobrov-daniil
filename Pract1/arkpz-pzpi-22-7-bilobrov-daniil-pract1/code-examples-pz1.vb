' Оформлення довгих виразів на кількох рядках для покращення читабельності
Dim total As Double = (quantity * price * (1 - discount)) + 
                      shippingCost - 
                      couponDiscount

' Умовна конструкція з відступами і на окремих рядках
If total > 1000 Then
    Console.WriteLine("Total is greater than 1000")
Else
    Console.WriteLine("Total is less than or equal to 1000")
End If

' Цикл з відступами
For i As Integer = 0 To 9 Step 2
    Console.WriteLine("Iteration " & i)
Next

' Оформлення складного виразу на кількох рядках
Dim result As Double = (a * b) + 
                       (c * d) - 
                       (e / f) + 
                       (g * h)

' Вкладена умовна конструкція з відступами
If result > 0 Then
    If x > y Then
        Console.WriteLine("Positive result and x > y")
    Else
        Console.WriteLine("Positive result and x <= y")
    End If
Else
    Console.WriteLine("Negative result")
End If
