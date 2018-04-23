Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo

Public Class Task
	Inherits XPObject
	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub
	Public AllDay As Boolean ' Appointment.AllDay

	<Size(SizeAttribute.Unlimited)> _
	Public Description As String ' Appointment.Description

	Public Finish As DateTime ' Appointment.End
	Public Label As Integer ' Appointment.Label
	Public Location As String ' Appointment.Location

	<Size(SizeAttribute.Unlimited)> _
	Public Recurrence As String ' Appointment.RecurrenceInfo

	<Size(SizeAttribute.Unlimited)> _
	Public Reminder As String ' Appointment.ReminderInfo

	Public Created As DateTime ' Appointment.Start
	Public Status As Integer ' Appointment.Status
	Public Subject As String ' Appointment.Subject
	Public AppointmentType As Integer ' Appointment.Type
	Public Employee As Employee
End Class

Public Class Employee
	Inherits XPObject
	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	<Size(SizeAttribute.Unlimited)> _
	Public Name As String ' Resource.Caption
End Class

