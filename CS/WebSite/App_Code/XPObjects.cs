using System;
using DevExpress.Xpo;

public class Task : XPObject
{
    public Task(Session session) : base(session) { }

    public bool AllDay  // Appointment.AllDay
    {
        get { return fAllDay; }
        set { SetPropertyValue(nameof(AllDay), ref fAllDay, value); }
    }
    bool fAllDay;


    [Size(SizeAttribute.Unlimited)]  // !! To set the Memo field type.    
    public string Description // Appointment.Description
    { 
        get { return fDescription; }
        set { SetPropertyValue(nameof(Description), ref fDescription, value); }
    }
    string fDescription;

    public DateTime Finish // Appointment.End
    {
        get { return fFinish; }
        set { SetPropertyValue(nameof(Finish), ref fFinish, value); }
    }
    DateTime fFinish;

    public int Label // Appointment.Label
    {
        get { return fLabel; }
        set { SetPropertyValue(nameof(Label), ref fLabel, value); }
    }
    int fLabel;


    public string Location // Appointment.Location
    {
        get { return fLocation; }
        set { SetPropertyValue(nameof(Location), ref fLocation, value); }
    }
    string fLocation;          

    [Size(SizeAttribute.Unlimited)]  // !! To set the Memo field type.    
    public string Recurrence // Appointment.RecurrenceInfo
    {
        get { return fRecurrence; }
        set { SetPropertyValue(nameof(Recurrence), ref fRecurrence, value); }
    }
    string fRecurrence;

    [Size(SizeAttribute.Unlimited)]  // !! To set the Memo field type.    
    public string Reminder // Appointment.ReminderInfo
    {
        get { return fReminder; }
        set { SetPropertyValue(nameof(Reminder), ref fReminder, value); }
    }
    string fReminder;

    
    public DateTime Created // Appointment.Start
    {
        get { return fCreated; }
        set { SetPropertyValue(nameof(Created), ref fCreated, value); }
    }
    DateTime fCreated;

    public int Status // Appointment.Status
    {
        get { return fStatus; }
        set { SetPropertyValue(nameof(Status), ref fStatus, value); }
    }
    int fStatus;        

    public string Subject // Appointment.Subject
    {
        get { return fSubject; }
        set { SetPropertyValue(nameof(Subject), ref fSubject, value); }
    }
    string fSubject;    

    public int AppointmentType // Appointment.Type
    {
        get { return fAppointmentType; }
        set { SetPropertyValue(nameof(AppointmentType), ref fAppointmentType, value); }
    }
    int fAppointmentType;

    public Employee Employee // Appointment.Type
    {
        get { return fEmployee; }
        set { SetPropertyValue(nameof(Employee), ref fEmployee, value); }
    }    

    public Employee fEmployee;
}

public class Employee : XPObject
{
    public Employee(Session session) : base(session) { }

    [Size(SizeAttribute.Unlimited)]  // !! To set the Memo field type.
    public string Name // Resource.Caption    
    {
        get { return fName; }
        set { SetPropertyValue(nameof(Name), ref fName, value); }
    }
    string fName;
}

