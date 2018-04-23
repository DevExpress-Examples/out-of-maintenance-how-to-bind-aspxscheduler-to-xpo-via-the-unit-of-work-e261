using System;
using DevExpress.Xpo;

public class Task : XPObject {
    public Task(Session session) : base(session) { }
    public bool AllDay;              // Appointment.AllDay

    [Size(SizeAttribute.Unlimited)]  // !! To set the Memo field type.
    public string Description;       // Appointment.Description

    public DateTime Finish;          // Appointment.End
    public int Label;                // Appointment.Label
    public string Location;          // Appointment.Location

    [Size(SizeAttribute.Unlimited)]  // !! To set the Memo field type.
    public string Recurrence;        // Appointment.RecurrenceInfo

    [Size(SizeAttribute.Unlimited)]  // !! To set the Memo field type.
    public string Reminder;          // Appointment.ReminderInfo

    public DateTime Created;         // Appointment.Start
    public int Status;               // Appointment.Status
    public string Subject;           // Appointment.Subject
    public int AppointmentType;      // Appointment.Type    
    public Employee Employee;
}

public class Employee : XPObject {
    public Employee(Session session) : base(session) { }

    [Size(SizeAttribute.Unlimited)]  // !! To set the Memo field type.
    public string Name;              // Resource.Caption    
}

