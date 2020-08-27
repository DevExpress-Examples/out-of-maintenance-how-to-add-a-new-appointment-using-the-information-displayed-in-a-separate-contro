<%@ Page Language="vb" AutoEventWireup="true"  CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxScheduler.v15.2, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxScheduler" tagprefix="dxwschs" %>
<%@ Register assembly="DevExpress.XtraScheduler.v15.2.Core, Version=15.2.20.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.XtraScheduler" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Please select an Appointment type <dx:ASPxComboBox ID="ASPxComboBox1" 
            runat="server" ValueType="System.String">
            <Items>
                <dx:ListEditItem Text="9:00 AM Breakfast" Value="9:00:00" />
                <dx:ListEditItem Text="1:00 PM Lunch" Value="13:00:00" />
                <dx:ListEditItem Text="6:00 PM Dinner" Value="18:00:00" />
            </Items>
        </dx:ASPxComboBox>
        and then press the 
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Create Appointment" 
            onclick="ASPxButton1_Click">
        </dx:ASPxButton> button.
        <br />
        <dxwschs:ASPxScheduler ID="ASPxScheduler1" runat="server" 
            AppointmentDataSourceID="AccessDataSource1" Start="2008-07-11" 
            onappointmentrowinserted="ASPxScheduler1_AppointmentRowInserted" 
            onappointmentrowinserting="ASPxScheduler1_AppointmentRowInserting" 
            onappointmentsinserted="ASPxScheduler1_OnAppointmentsInserted">
            <Storage>
                <Appointments>
                    <Mappings AppointmentId="ID" Description="Description" End="EndTime" 
                        Label="Label" Start="StartTime" Subject="Subject" Type="EventType" />
                </Appointments>
            </Storage>
<Views>
<DayView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</DayView>

<WorkWeekView><TimeRulers>
<cc1:TimeRuler></cc1:TimeRuler>
</TimeRulers>
</WorkWeekView>
</Views>
        </dxwschs:ASPxScheduler>

        <asp:AccessDataSource ID="AccessDataSource1" runat="server" 
            ConflictDetection="CompareAllValues" DataFile="~/App_Data/CarsDB2.mdb" 
            DeleteCommand="DELETE FROM [CarScheduling] WHERE [ID] = ?" 
            InsertCommand="INSERT INTO [CarScheduling] ([Subject], [Description], [Label], [StartTime], [EndTime], [EventType]) VALUES (?, ?, ?, ?, ?, ?)" 
            OldValuesParameterFormatString="original_{0}" 
            SelectCommand="SELECT [ID], [Subject], [Description], [Label], [StartTime], [EndTime], [EventType] FROM [CarScheduling]" 

            UpdateCommand="UPDATE [CarScheduling] SET [Subject] = ?, [Description] = ?, [Label] = ?, [StartTime] = ?, [EndTime] = ?, [EventType] = ? WHERE [ID] = ?" 
            oninserted="appointmentDataSource_Inserted">
            <DeleteParameters>
                <asp:Parameter Name="original_ID" Type="Int32" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="Subject" Type="String" />
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="Label" Type="Int32" />
                <asp:Parameter Name="StartTime" Type="DateTime" />
                <asp:Parameter Name="EndTime" Type="DateTime" />
                <asp:Parameter Name="EventType" Type="Int32" />
                <asp:Parameter Name="original_ID" Type="Int32" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="Subject" Type="String" />
                <asp:Parameter Name="Description" Type="String" />
                <asp:Parameter Name="Label" Type="Int32" />
                <asp:Parameter Name="StartTime" Type="DateTime" />
                <asp:Parameter Name="EndTime" Type="DateTime" />
                <asp:Parameter Name="EventType" Type="Int32" />
            </InsertParameters>
        </asp:AccessDataSource>

    </div>
    </form>
</body>
</html>