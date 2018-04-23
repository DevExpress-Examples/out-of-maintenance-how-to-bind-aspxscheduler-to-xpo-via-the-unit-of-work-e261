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
    public static Random RandomInstance = new Random();

    UnitOfWork unitOfWork;

    protected void Page_Init(object sender, EventArgs e) {
        this.unitOfWork = new UnitOfWork(Application["XpoLayer"] as IDataLayer);
        appointmentDataSource.Session = unitOfWork;
        resourceDataSource.Session = unitOfWork;
        InitDataSourcesWithDefaultValues();
    }

    protected void ASPxScheduler1_OnAppointmentRowInserted(object sender, DevExpress.Web.ASPxScheduler.ASPxSchedulerDataInsertedEventArgs e) {
        ICollection res = this.unitOfWork.GetObjectsToSave();
        System.Diagnostics.Debug.Assert(res.Count == 1);
        Task lastInsertedTask = null;
        foreach(Task task in res) 
            lastInsertedTask = task;
        CommitChanges();
        e.KeyFieldValue = lastInsertedTask.Oid;    
    }
    protected void ASPxScheduler1_AppointmentRowUpdated(object sender, ASPxSchedulerDataUpdatedEventArgs e) {
        CommitChanges();
    }
    protected void ASPxScheduler1_AppointmentRowDeleted(object sender, ASPxSchedulerDataDeletedEventArgs e) {
        CommitChanges();
    }
    void CommitChanges() {
        unitOfWork.CommitChanges();
    }

    #region Populate database with initial data
    void InitDataSourcesWithDefaultValues() {
        if(unitOfWork.FindObject<Employee>(null) != null)
            return;
        PopulateDataSourcesCore();
        this.unitOfWork.CommitChanges();
    }
    void PopulateDataSourcesCore() {
        Employee employee;
        for(int i = 0; i < 5; i++) {
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
