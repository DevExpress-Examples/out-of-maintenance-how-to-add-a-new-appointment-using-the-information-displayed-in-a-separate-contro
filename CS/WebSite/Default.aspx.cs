using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxScheduler;
using System.Data.OleDb;
using DevExpress.XtraScheduler;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    int lastInsertedAppointmentId;

    protected void ASPxScheduler1_AppointmentRowInserting(object sender,
    ASPxSchedulerDataInsertingEventArgs e)
    {
        // Remove unnecessary ID field.
        e.NewValues.Remove("ID");
    }

    protected void appointmentDataSource_Inserted(object sender,
    SqlDataSourceStatusEventArgs e)
    {

        // Obtain the identity value.
        OleDbConnection connection = (OleDbConnection)e.Command.Connection;
        using (OleDbCommand cmd = new OleDbCommand("SELECT @@IDENTITY", connection))
        {
            this.lastInsertedAppointmentId = (int)cmd.ExecuteScalar();
        }

    }

    protected void ASPxScheduler1_AppointmentRowInserted(object sender,
    ASPxSchedulerDataInsertedEventArgs e)
    {
        // Specify new ID field value.
        e.KeyFieldValue = this.lastInsertedAppointmentId;
    }

    protected void ASPxScheduler1_OnAppointmentsInserted(object sender,
    PersistentObjectsEventArgs e)
    {
        // Store the new appointment.
        int count = e.Objects.Count;
        System.Diagnostics.Debug.Assert(count == 1);
        Appointment apt = (Appointment)e.Objects[0];
        ASPxSchedulerStorage storage = (ASPxSchedulerStorage)sender;
        storage.SetAppointmentId(apt, lastInsertedAppointmentId);
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        if (this.ASPxComboBox1.Value != null)
        {
            TimeSpan time = TimeSpan.Parse(this.ASPxComboBox1.Value.ToString());
            string subject = this.ASPxComboBox1.SelectedItem.Text;
            DateTime current = this.ASPxScheduler1.Start;
            Appointment apt = this.ASPxScheduler1.Storage.CreateAppointment(AppointmentType.Normal);
            apt.Start = current.Date.Add(time);
            apt.Subject = subject;
            this.ASPxScheduler1.Storage.Appointments.Add(apt);
        }
    }
}
