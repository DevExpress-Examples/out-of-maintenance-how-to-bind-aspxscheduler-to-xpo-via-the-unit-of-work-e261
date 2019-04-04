<!-- default file list -->
*Files to look at*:

* [XPObjects.cs](./CS/WebSite/App_Code/XPObjects.cs) (VB: [XPObjects.vb](./VB/WebSite/App_Code/XPObjects.vb))
* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx.vb](./VB/WebSite/Default.aspx.vb))
<!-- default file list end -->
# How to bind ASPxScheduler to XPO via the Unit of Work


<p>This example illustrates how you can bind the ASPxScheduler control to an XPO (<a href="http://documentation.devexpress.com/#XPO/CustomDocument2065">eXpress Persistent Objects</a>) data source using the <strong>UnitOfWork</strong> session class. <br />
Two persistent object classes are defined - one for appointments, the other - for resources. They are named <strong>Task</strong> and <strong>Employee</strong>. Two XPO data sources are added to the page.<br />
The <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_AppointmentsInsertedtopic">ASPxScheduler.AppointmentsInserted</a>, <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_AppointmentsChangedtopic">ASPxScheduler.AppointmentsChanged</a> and <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxSchedulerASPxScheduler_AppointmentsDeletedtopic">ASPxScheduler.AppointmentsDeleted</a> events are handled to commit changes in the data source. They call the <a href="http://documentation.devexpress.com/#XPO/DevExpressXpoUnitOfWork_CommitChangestopic">UnitOfWork.CommitChanges</a> method of the Unit of Work object for this purpose.<br />
To obtain a correct appointment, a new <strong>XPORowInsertionProvider</strong> class is defined. It is aimed at retrieving the key identifier value of the last inserted appointment, and set the corresponding value of the appointment in the storage. It handles the <strong>Session.ObjectSaved</strong> event of the Session object and the <strong>ASPxScheduler.AppointmentsInserted</strong> event of the ASPxScheduler.</p><p>Note: an exception occurs when trying to drag and drop the recurring appointment. This is a known issue which is fixed in example code designed for XPO versions  v2008 vol.3.3, v2009 vol1  and higher.</p><p><strong>See Also:</strong><br />
<a href="https://www.devexpress.com/Support/Center/p/K18043">How to bind ASPxScheduler to ObjectDataSource</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E215">How to bind ASPxScheduler to MS SQL Server database</a><br />
<a href="https://www.devexpress.com/Support/Center/p/E409">How to bind ASPxScheduler to SyBase ASE 15 database</a></p>

<br/>


