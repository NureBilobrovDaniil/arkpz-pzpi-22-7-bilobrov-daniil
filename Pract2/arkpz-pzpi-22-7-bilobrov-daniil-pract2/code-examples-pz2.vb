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


Public Class Book
    Public Property Title As String
    Public Property Author As String
    Public Property Year As Integer

    Public Sub New(title As String, author As String, year As Integer)
        Me.Title = title
        Me.Author = author
        Me.Year = year
    End Sub
End Class

Public Class Library
    Public Books As List(Of Book)

    Public Sub New()
        Books = New List(Of Book)()
    End Sub
End Class



Public Class Book
    Public Property Title As String
    Public Property Author As String
    Public Property Year As Integer

    Public Sub New(title As String, author As String, year As Integer)
        Me.Title = title
        Me.Author = author
        Me.Year = year
    End Sub
End Class

Public Class Library
    ' Тепер Books є приватною колекцією
    Private Books As List(Of Book)

    Public Sub New()
        Books = New List(Of Book)()
    End Sub

    ' Метод для додавання книги з перевіркою на дублікати
    Public Sub AddBook(newBook As Book)
        ' Перевірка на наявність книги з таким самим заголовком та автором
        If Not Books.Any(Function(b) b.Title = newBook.Title AndAlso b.Author = newBook.Author) Then
            Books.Add(newBook)
        Else
            Console.WriteLine("Book already exists in the library.")
        End If
    End Sub

    ' Метод для отримання списку всіх книг
    Public Function GetBooks() As List(Of Book)
        Return Books
    End Function

    ' Метод для видалення книги
    Public Sub RemoveBook(bookToRemove As Book)
        Books.Remove(bookToRemove)
    End Sub
End Class
