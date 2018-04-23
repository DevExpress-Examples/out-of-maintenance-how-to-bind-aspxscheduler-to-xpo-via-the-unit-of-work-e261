Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Xpo
Imports DevExpress.Web.ASPxScheduler
Imports DevExpress.XtraScheduler
Imports System.Collections
Imports System.Collections.Generic

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Public Shared RandomInstance As New Random()

	Private unitOfWork As UnitOfWork

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		Me.unitOfWork = New UnitOfWork(TryCast(Application("XpoLayer"), IDataLayer))
		appointmentDataSource.Session = unitOfWork
		resourceDataSource.Session = unitOfWork
		InitDataSourcesWithDefaultValues()
	End Sub

	Protected Sub ASPxScheduler1_OnAppointmentRowInserted(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs)
		Dim res As ICollection = Me.unitOfWork.GetObjectsToSave()
		System.Diagnostics.Debug.Assert(res.Count = 1)
		Dim lastInsertedTask As Task = Nothing
		For Each task As Task In res
			lastInsertedTask = task
		Next task
		CommitChanges()
		e.KeyFieldValue = lastInsertedTask.Oid
	End Sub
	Protected Sub ASPxScheduler1_AppointmentRowUpdated(ByVal sender As Object, ByVal e As ASPxSchedulerDataUpdatedEventArgs)
		CommitChanges()
	End Sub
	Protected Sub ASPxScheduler1_AppointmentRowDeleted(ByVal sender As Object, ByVal e As ASPxSchedulerDataDeletedEventArgs)
		CommitChanges()
	End Sub
	Private Sub CommitChanges()
		unitOfWork.CommitChanges()
	End Sub

	#Region "Populate database with initial data"
	Private Sub InitDataSourcesWithDefaultValues()
		If unitOfWork.FindObject(Of Employee)(Nothing) IsNot Nothing Then
			Return
		End If
		PopulateDataSourcesCore()
		Me.unitOfWork.CommitChanges()
	End Sub
	Private Sub PopulateDataSourcesCore()
		Dim employee As Employee
		For i As Integer = 0 To 4
			employee = CreateEmployee("Employee" & i.ToString())
			CreateTasks(employee)
		Next i
	End Sub
	Private Function CreateEmployee(ByVal name As String) As Employee
		Dim employee As New Employee(unitOfWork)
		employee.Name = name
		employee.Save()
		Return employee
	End Function
	Private Sub CreateTasks(ByVal employee As Employee)
		CreateTask(employee, "meeting", RandomInstance.Next(0, 4), RandomInstance.Next(0, 5))
		CreateTask(employee, "meeting", RandomInstance.Next(0, 4), RandomInstance.Next(0, 5))
	End Sub
	Private Sub CreateTask(ByVal employee As Employee, ByVal taskName As String, ByVal status As Integer, ByVal label As Integer)
		Dim task As New Task(unitOfWork)

		task.Employee = employee
		task.Subject = employee.Name & "'s " & taskName

		Dim rangeInHours As Integer = 72
		task.Created = DateTime.Today + TimeSpan.FromHours(RandomInstance.Next(0, rangeInHours))
		task.Finish = task.Created + TimeSpan.FromHours(RandomInstance.Next(0, rangeInHours \ 12))

		task.Status = status
		task.Label = label

		task.Save()
	End Sub
	#End Region
End Class
