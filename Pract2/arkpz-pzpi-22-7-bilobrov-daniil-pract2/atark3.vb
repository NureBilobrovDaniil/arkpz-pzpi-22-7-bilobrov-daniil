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
