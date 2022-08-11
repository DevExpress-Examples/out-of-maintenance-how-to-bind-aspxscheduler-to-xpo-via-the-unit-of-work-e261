<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="DevExpress.Xpo.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Xpo" tagprefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxScheduler ID="ASPxScheduler1" runat="server" 
            AppointmentDataSourceID="appointmentDataSource"
            ResourceDataSourceID="resourceDataSource"
            OnAppointmentRowInserted="ASPxScheduler1_OnAppointmentRowInserted"
            OnAppointmentRowDeleted="ASPxScheduler1_AppointmentRowDeleted"
            OnAppointmentRowUpdated="ASPxScheduler1_AppointmentRowUpdated"
            GroupType="Resource">
            <Storage>
                <Appointments>
                    <Mappings AllDay="AllDay" Description="Description" End="Finish" Label="Label" Location="Location"
                        RecurrenceInfo="Recurrence" ReminderInfo="Reminder" ResourceId="Employee!Key"
                        Start="Created" Status="Status" Subject="Subject" Type="AppointmentType" AppointmentId="Oid" />
                </Appointments>
                <Resources>
                    <Mappings Caption="Name" ResourceId="Oid" />
                </Resources>
            </Storage>
        </dx:ASPxScheduler>
        <dx:XpoDataSource ID="appointmentDataSource" runat="server" TypeName="Task" />
        <dx:XpoDataSource ID="resourceDataSource" runat="server" TypeName="Employee" />
    </div>
    </form>
</body>
</html>
