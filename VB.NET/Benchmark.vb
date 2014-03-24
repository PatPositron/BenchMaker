Option Strict On

Imports System.Linq
Imports System.Diagnostics

Namespace Benchmark
    Public MustInherit Class Bench
        ''' <summary>
        ''' Executes the benchmark.
        ''' </summary>
        ''' <param name="operations">The amount of times to run each method</param>
        Public Function Execute(operations As Integer) As Result
            If operations <= 0 Then
                Throw New ArgumentOutOfRangeException("operations", "Amount must be greater than 0.")
            End If

            Dim bench1 = New List(Of Double)()
            Dim bench2 = New List(Of Double)()

            For i As Integer = 0 To operations - 1
                bench1.Add(TimeMethod(AddressOf BenchMethodOne))
                bench2.Add(TimeMethod(AddressOf BenchMethodTwo))
            Next

            Return New Result(operations, bench1.Average(), bench2.Average())
        End Function

        Private Function TimeMethod(method As Action) As Double
            Dim timer = Stopwatch.StartNew()
            method()
            timer.Stop()
            Return (timer.ElapsedTicks * ((1000 * 1000 * 1000) / CDbl(Stopwatch.Frequency)))
        End Function

        Protected MustOverride Sub BenchMethodOne()
        Protected MustOverride Sub BenchMethodTwo()
    End Class

    Public NotInheritable Class Result
        Private ReadOnly _operations As Integer
        Private ReadOnly method1, method2 As Double

        Sub New(operations As Integer, method1 As Double, method2 As Double)
            Me._operations = operations
            Me.method1 = method1
            Me.method2 = method2
        End Sub
        ''' <summary>
        ''' Gets the amount of times each method was executed.
        ''' </summary>
        Public ReadOnly Property Operations As Integer
            Get
                Return Me._operations
            End Get
        End Property
        ''' <summary>
        ''' Gets the average execution time in nanoseconds for method one.
        ''' </summary>
        Public ReadOnly Property MethodOneAverage As Double
            Get
                Return Me.method1
            End Get
        End Property
        ''' <summary>
        ''' Gets the average execution time in nanoseconds for method two.
        ''' </summary>
        Public ReadOnly Property MethodTwoAverage As Double
            Get
                Return Me.method2
            End Get
        End Property
    End Class
End Namespace