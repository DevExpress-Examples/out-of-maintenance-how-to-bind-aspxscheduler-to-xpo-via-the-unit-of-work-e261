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
	Private unitOfWork As UnitOfWork
	Public Shared RandomInstance As New Random()
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		PrepareDataSources()
		SetupMappings()
		ProvideRowInsertion()
		AttachDataSources()
		PopulateDataSources()
	End Sub
	Private Sub ProvideRowInsertion()
		Dim aptProvider As New XPORowInsertionProvider()
		aptProvider.ProvideRowInsertion(ASPxScheduler1, appointmentDataSource, unitOfWork)
	End Sub
	Private Sub PrepareDataSources()
		unitOfWork = New UnitOfWork(TryCast(Application("XpoLayer"), IDataLayer))
		appointmentDataSource.Session = unitOfWork
		resourceDataSource.Session = unitOfWork
	End Sub
	Private Sub AttachDataSources()
		ASPxScheduler1.AppointmentDataSource = appointmentDataSource
		ASPxScheduler1.ResourceDataSource = resourceDataSource
		ASPxScheduler1.DataBind()
	End Sub
	Private Sub SetupMappings()
		ASPxScheduler1.Storage.BeginUpdate()
		Try
			Dim aptMappings As ASPxAppointmentMappingInfo = ASPxScheduler1.Storage.Appointments.Mappings
			aptMappings.AllDay = "AllDay"
			aptMappings.Description = "Description"
			aptMappings.End = "Finish"
			aptMappings.Label = "Label"
			aptMappings.Location = "Location"
			aptMappings.RecurrenceInfo = "Recurrence"
			aptMappings.ReminderInfo = "Reminder"
			aptMappings.ResourceId = "Employee!Key"
			aptMappings.Start = "Created"
			aptMappings.Status = "Status"
			aptMappings.Subject = "Subject"
			aptMappings.Type = "AppointmentType"
			aptMappings.AppointmentId = "Oid"

			Dim resourceMappings As ASPxResourceMappingInfo = ASPxScheduler1.Storage.Resources.Mappings
			resourceMappings.Caption = "Name"
			resourceMappings.ResourceId = "Oid"
		Finally
			ASPxScheduler1.Storage.EndUpdate()
		End Try
	End Sub

	Private Sub CommitChanges()
		unitOfWork.CommitChanges()
	End Sub
	Protected Sub ASPxScheduler1_OnAppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
		CommitChanges()
	End Sub
	Protected Sub ASPxScheduler1_AppointmentsDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
		CommitChanges()
	End Sub
	Protected Sub ASPxScheduler1_AppointmentsChanged(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
		CommitChanges()
	End Sub

	   Public Class XPORowInsertionProvider
		Private insertedAppointmentsId As New List(Of Integer)()
		Private control As ASPxScheduler
		Private unitOfWork As UnitOfWork

		Public Sub ProvideRowInsertion(ByVal control As ASPxScheduler, ByVal dataSource As XpoDataSource, ByVal unitOfWork As UnitOfWork)
			Me.unitOfWork = unitOfWork
			Me.control = control
			AddHandler control.AppointmentRowInserted, AddressOf ASPxScheduler1_AppointmentRowInserted
			AddHandler dataSource.Inserted, AddressOf dataSource_OnInserted
			AddHandler control.AppointmentsInserted, AddressOf ControlOnAppointmentsInserted
		End Sub
		Private Sub dataSource_OnInserted(ByVal sender As Object, ByVal e As XpoDataSourceInsertedEventArgs)
			Me.unitOfWork.CommitChanges()
			Me.insertedAppointmentsId.Add((CType(e.Value, XPObject)).Oid)
		End Sub
		Protected Sub ASPxScheduler1_AppointmentRowInserted(ByVal sender As Object, ByVal e As ASPxSchedulerDataInsertedEventArgs)
			e.KeyFieldValue = Me.insertedAppointmentsId(Me.insertedAppointmentsId.Count - 1)
		End Sub
		Private Sub ControlOnAppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
			Dim count As Integer = e.Objects.Count
			System.Diagnostics.Debug.Assert(count = insertedAppointmentsId.Count)
			SetAppointmentsId(sender, e)
		End Sub
		Private Sub SetAppointmentsId(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
			Dim appointments As AppointmentBaseCollection = CType(e.Objects, AppointmentBaseCollection)
			Dim storage As ASPxSchedulerStorage = CType(sender, ASPxSchedulerStorage)
			Dim count As Integer = appointments.Count
			System.Diagnostics.Debug.Assert(count = insertedAppointmentsId.Count)
			For i As Integer = 0 To count - 1
				Dim apt As Appointment = appointments(i)
				storage.SetAppointmentId(apt, insertedAppointmentsId(i))
			Next i
			insertedAppointmentsId.Clear()
		End Sub
	   End Class


	#Region "Populate database with initial data"
	Private Sub PopulateDataSources()
		If ASPxScheduler1.Storage.Appointments.Count = 0 Then
			ASPxScheduler1.Storage.Resources.Clear()
			PopulateDataSourcesCore()
			CommitChanges()
			ASPxScheduler1.DataBind()
		End If
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
