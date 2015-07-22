using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IOTApp.DataModel;
using System.Text;
using Microsoft.Phone.UserData;

namespace IOTApp.Utility
{
    public static class CalendarRepository
    {
        public static List<Appointment> listOfAppointment;
        public static List<Appointment> GetCalenderAppointments()
        {
            Appointments appointment = new Appointments();
            appointment.SearchCompleted += new EventHandler<AppointmentsSearchEventArgs>(SearchAppointment);
            DateTime startDate = DateTime.Now;
            DateTime endDate = startDate.AddDays(1);
            appointment.SearchAsync(startDate, endDate, null);
            return listOfAppointment;
        }
        public static void SearchAppointment(object sender, AppointmentsSearchEventArgs e)
        {
            listOfAppointment = new List<Appointment>(e.Results);
            
        }  
        //public void  GetApp()
        //{
        //    AppointmentStore appointmentStore =  AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AllCalendarsReadOnly);
        //}
    }
}
