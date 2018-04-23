using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DevExpress.Xpo;
using DevExpress.Web.ASPxScheduler;
using DevExpress.XtraScheduler;
using System.Collections;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page {
    UnitOfWork unitOfWork;
    public static Random RandomInstance = new Random();
    protected void Page_Load(object sender, EventArgs e) {
        PrepareDataSources();
        SetupMappings();
        ProvideRowInsertion();
        AttachDataSources();
        PopulateDataSources();
    }
    void ProvideRowInsertion() {
        XPORowInsertionProvider aptProvider = new XPORowInsertionProvider();
        aptProvider.ProvideRowInsertion(ASPxScheduler1, appointmentDataSource, unitOfWork);
    }
    void PrepareDataSources() {
        unitOfWork = new UnitOfWork(Application["XpoLayer"] as IDataLayer);
        appointmentDataSource.Session = unitOfWork;
        resourceDataSource.Session = unitOfWork;
    }
    void AttachDataSources() {
        ASPxScheduler1.AppointmentDataSource = appointmentDataSource;
        ASPxScheduler1.ResourceDataSource = resourceDataSource;
        ASPxScheduler1.DataBind();
    }
    void SetupMappings() {
        ASPxScheduler1.Storage.BeginUpdate();
        try {
            ASPxAppointmentMappingInfo aptMappings = ASPxScheduler1.Storage.Appointments.Mappings;
            aptMappings.AllDay = "AllDay";
            aptMappings.Description = "Description";
            aptMappings.End = "Finish";
            aptMappings.Label = "Label";
            aptMappings.Location = "Location";
            aptMappings.RecurrenceInfo = "Recurrence";
            aptMappings.ReminderInfo = "Reminder";
            aptMappings.ResourceId = "Employee!Key";
            aptMappings.Start = "Created";
            aptMappings.Status = "Status";
            aptMappings.Subject = "Subject";
            aptMappings.Type = "AppointmentType";
            aptMappings.AppointmentId = "Oid";

            ASPxResourceMappingInfo resourceMappings = ASPxScheduler1.Storage.Resources.Mappings;
            resourceMappings.Caption = "Name";
            resourceMappings.ResourceId = "Oid";
        }
        finally {
            ASPxScheduler1.Storage.EndUpdate();
        }
    }

    private void CommitChanges() {
        unitOfWork.CommitChanges();
    }
    protected void ASPxScheduler1_OnAppointmentsInserted(object sender, PersistentObjectsEventArgs e) {
        CommitChanges();
    }
    protected void ASPxScheduler1_AppointmentsDeleted(object sender, PersistentObjectsEventArgs e) {
        CommitChanges();
    }
    protected void ASPxScheduler1_AppointmentsChanged(object sender, PersistentObjectsEventArgs e) {
        CommitChanges();
    }

       public class XPORowInsertionProvider {
        List<int> insertedAppointmentsId = new List<int>();
        ASPxScheduler control;
        UnitOfWork unitOfWork;

        public void ProvideRowInsertion(ASPxScheduler control, XpoDataSource dataSource, UnitOfWork unitOfWork) {
            this.unitOfWork = unitOfWork;
            this.control = control;
            control.AppointmentRowInserted += new ASPxSchedulerDataInsertedEventHandler(ASPxScheduler1_AppointmentRowInserted);
            dataSource.Inserted += new XpoDataSourceInsertedEventHandler(dataSource_OnInserted);
            control.AppointmentsInserted += new PersistentObjectsEventHandler(ControlOnAppointmentsInserted);
        }
        void dataSource_OnInserted(object sender, XpoDataSourceInsertedEventArgs e) {
            this.unitOfWork.CommitChanges();
            this.insertedAppointmentsId.Add(((XPObject)e.Value).Oid);
        }
        protected void ASPxScheduler1_AppointmentRowInserted(object sender, ASPxSchedulerDataInsertedEventArgs e) {
            e.KeyFieldValue = this.insertedAppointmentsId[this.insertedAppointmentsId.Count - 1];
        }
        void ControlOnAppointmentsInserted(object sender, PersistentObjectsEventArgs e) {
            int count = e.Objects.Count;
            System.Diagnostics.Debug.Assert(count == insertedAppointmentsId.Count);
            SetAppointmentsId(sender, e);
        }
        void SetAppointmentsId(object sender, PersistentObjectsEventArgs e) {
            AppointmentBaseCollection appointments = (AppointmentBaseCollection)e.Objects;
            ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
            int count = appointments.Count;
            System.Diagnostics.Debug.Assert(count == insertedAppointmentsId.Count);
            for (int i = 0; i < count; i++) {
                Appointment apt = appointments[i];
                storage.SetAppointmentId(apt, insertedAppointmentsId[i]);
            }
            insertedAppointmentsId.Clear();
        }
    }


    #region Populate database with initial data
    void PopulateDataSources() {
        if (ASPxScheduler1.Storage.Appointments.Count == 0) {
            ASPxScheduler1.Storage.Resources.Clear();
            PopulateDataSourcesCore();
            CommitChanges();
            ASPxScheduler1.DataBind();
        }
    }
    void PopulateDataSourcesCore() {
        Employee employee;
        for (int i = 0; i < 5; i++) {
            employee = CreateEmployee("Employee" + i.ToString());
            CreateTasks(employee);
        }
    }
    Employee CreateEmployee(string name) {
        Employee employee = new Employee(unitOfWork);
        employee.Name = name;
        employee.Save();
        return employee;
    }
    void CreateTasks(Employee employee) {
        CreateTask(employee, "meeting", RandomInstance.Next(0, 4), RandomInstance.Next(0, 5));
        CreateTask(employee, "meeting", RandomInstance.Next(0, 4), RandomInstance.Next(0, 5));
    }

    void CreateTask(Employee employee, string taskName, int status, int label) {
        Task task = new Task(unitOfWork);

        task.Employee = employee;
        task.Subject = employee.Name + "'s " + taskName;

        int rangeInHours = 72;
        task.Created = DateTime.Today + TimeSpan.FromHours(RandomInstance.Next(0, rangeInHours));
        task.Finish = task.Created + TimeSpan.FromHours(RandomInstance.Next(0, rangeInHours / 12));

        task.Status = status;
        task.Label = label;

        task.Save();
    }
    #endregion
}
