Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxScheduler
Imports System.Data.OleDb
Imports DevExpress.XtraScheduler

Partial Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)

    End Sub

    Private lastInsertedAppointmentId As Integer

    Protected Sub ASPxScheduler1_AppointmentRowInserting(ByVal sender As Object, ByVal e As ASPxSchedulerDataInsertingEventArgs)
        ' Remove unnecessary ID field.
        e.NewValues.Remove("ID")
    End Sub

    Protected Sub appointmentDataSource_Inserted(ByVal sender As Object, ByVal e As SqlDataSourceStatusEventArgs)

        ' Obtain the identity value.
        Dim connection As OleDbConnection = CType(e.Command.Connection, OleDbConnection)
        Using cmd As New OleDbCommand("SELECT @@IDENTITY", connection)
            Me.lastInsertedAppointmentId = DirectCast(cmd.ExecuteScalar(), Integer)
        End Using

    End Sub

    Protected Sub ASPxScheduler1_AppointmentRowInserted(ByVal sender As Object, ByVal e As ASPxSchedulerDataInsertedEventArgs)
        ' Specify new ID field value.
        e.KeyFieldValue = Me.lastInsertedAppointmentId
    End Sub

    Protected Sub ASPxScheduler1_OnAppointmentsInserted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
        ' Store the new appointment.
        Dim count As Integer = e.Objects.Count
        System.Diagnostics.Debug.Assert(count = 1)
        Dim apt As Appointment = CType(e.Objects(0), Appointment)
        Dim storage As ASPxSchedulerStorage = DirectCast(sender, ASPxSchedulerStorage)
        storage.SetAppointmentId(apt, lastInsertedAppointmentId)
    End Sub

    Protected Sub ASPxButton1_Click(ByVal sender As Object, ByVal e As EventArgs)
        If Me.ASPxComboBox1.Value IsNot Nothing Then
            Dim time As TimeSpan = TimeSpan.Parse(Me.ASPxComboBox1.Value.ToString())
            Dim subject As String = Me.ASPxComboBox1.SelectedItem.Text
            Dim current As Date = Me.ASPxScheduler1.Start
            Dim apt As Appointment = Me.ASPxScheduler1.Storage.CreateAppointment(AppointmentType.Normal)
            apt.Start = current.Date.Add(time)
            apt.Subject = subject
            Me.ASPxScheduler1.Storage.Appointments.Add(apt)
        End If
    End Sub
End Class
