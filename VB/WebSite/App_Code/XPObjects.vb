Imports System
Imports DevExpress.Xpo

Public Class Task
	Inherits XPObject

	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	Public Property AllDay() As Boolean ' Appointment.AllDay
		Get
			Return fAllDay
		End Get
		Set(ByVal value As Boolean)
			SetPropertyValue(NameOf(AllDay), fAllDay, value)
		End Set
	End Property
	Private fAllDay As Boolean


	<Size(SizeAttribute.Unlimited)>
	Public Property Description() As String ' Appointment.Description -  !! To set the Memo field type.
		Get
			Return fDescription
		End Get
		Set(ByVal value As String)
			SetPropertyValue(NameOf(Description), fDescription, value)
		End Set
	End Property
	Private fDescription As String

	Public Property Finish() As Date ' Appointment.End
		Get
			Return fFinish
		End Get
		Set(ByVal value As Date)
			SetPropertyValue(NameOf(Finish), fFinish, value)
		End Set
	End Property
	Private fFinish As Date

	Public Property Label() As Integer ' Appointment.Label
		Get
			Return fLabel
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(NameOf(Label), fLabel, value)
		End Set
	End Property
	Private fLabel As Integer


	Public Property Location() As String ' Appointment.Location
		Get
			Return fLocation
		End Get
		Set(ByVal value As String)
			SetPropertyValue(NameOf(Location), fLocation, value)
		End Set
	End Property
	Private fLocation As String

	<Size(SizeAttribute.Unlimited)>
	Public Property Recurrence() As String ' Appointment.RecurrenceInfo -  !! To set the Memo field type.
		Get
			Return fRecurrence
		End Get
		Set(ByVal value As String)
			SetPropertyValue(NameOf(Recurrence), fRecurrence, value)
		End Set
	End Property
	Private fRecurrence As String

	<Size(SizeAttribute.Unlimited)>
	Public Property Reminder() As String ' Appointment.ReminderInfo -  !! To set the Memo field type.
		Get
			Return fReminder
		End Get
		Set(ByVal value As String)
			SetPropertyValue(NameOf(Reminder), fReminder, value)
		End Set
	End Property
	Private fReminder As String


	Public Property Created() As Date ' Appointment.Start
		Get
			Return fCreated
		End Get
		Set(ByVal value As Date)
			SetPropertyValue(NameOf(Created), fCreated, value)
		End Set
	End Property
	Private fCreated As Date

	Public Property Status() As Integer ' Appointment.Status
		Get
			Return fStatus
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(NameOf(Status), fStatus, value)
		End Set
	End Property
	Private fStatus As Integer

	Public Property Subject() As String ' Appointment.Subject
		Get
			Return fSubject
		End Get
		Set(ByVal value As String)
			SetPropertyValue(NameOf(Subject), fSubject, value)
		End Set
	End Property
	Private fSubject As String

	Public Property AppointmentType() As Integer ' Appointment.Type
		Get
			Return fAppointmentType
		End Get
		Set(ByVal value As Integer)
			SetPropertyValue(NameOf(AppointmentType), fAppointmentType, value)
		End Set
	End Property
	Private fAppointmentType As Integer

	Public Property Employee() As Employee ' Appointment.Type
		Get
			Return fEmployee
		End Get
		Set(ByVal value As Employee)
			SetPropertyValue(NameOf(Employee), fEmployee, value)
		End Set
	End Property

	Public fEmployee As Employee
End Class

Public Class Employee
	Inherits XPObject

	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	<Size(SizeAttribute.Unlimited)>
	Public Property Name() As String ' Resource.Caption -  !! To set the Memo field type.
		Get
			Return fName
		End Get
		Set(ByVal value As String)
			SetPropertyValue(NameOf(Name), fName, value)
		End Set
	End Property
	Private fName As String
End Class

