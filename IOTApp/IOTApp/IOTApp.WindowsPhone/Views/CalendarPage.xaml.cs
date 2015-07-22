using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Appointments;

using Newtonsoft.Json;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace IOTApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            this.InitializeComponent();
        }
        static AppointmentStore appointmentStore;
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await GetAppointments();
            DateTime startDate = DateTime.Now;
            TimeSpan duration = new DateTimeOffset(startDate.AddDays(1)) - new DateTimeOffset(startDate);
            var calendar = await appointmentStore.FindAppointmentCalendarsAsync();
            List<Appointment> appointments = null;
            if (calendar.Count > 0)
            {
                appointments = (await calendar[0].FindAppointmentsAsync(startDate, duration)).ToList();
            }
            IAsyncOperation<IReadOnlyList<Appointment>> listofappoinment = appointmentStore.FindAppointmentsAsync(startDate, duration, new FindAppointmentsOptions() { IncludeHidden = true });

            listCalendarViewCanvas.ItemsSource = appointments;
        }
        
       
        
        
        public static async Task GetAppointments()
        {
            appointmentStore = await AppointmentManager.RequestStoreAsync(AppointmentStoreAccessType.AllCalendarsReadOnly);
        }
        

    }
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format((string)parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
       
}
