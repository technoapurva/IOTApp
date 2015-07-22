using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using IOTApp.DataModel;
using IOTApp.Utility;
using Microsoft.Phone.UserData;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace IOTApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CalendarCanvas : Page
    {
        public CalendarCanvas()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
        private  void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<Appointment> appointments =  CalendarRepository.GetCalenderAppointments();
            

            listCalendarViewCanvas.DataContext = appointments;
        }
        public T FindDescendant<T>(DependencyObject obj) where T : DependencyObject
        {
            // Check if this object is the specified type
            if (obj is T)
                return obj as T;

            // Check for children
            int childrenCount = VisualTreeHelper.GetChildrenCount(obj);
            if (childrenCount < 1)
                return null;

            // First check all the children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child is T)
                    return child as T;
            }

            // Then check the childrens children
            for (int i = 0; i < childrenCount; i++)
            {
                DependencyObject child = FindDescendant<T>(VisualTreeHelper.GetChild(obj, i));
                if (child != null && child is T)
                    return child as T;
            }

            return null;
        }

        private void AppointmentClick(object sender, ItemClickEventArgs e)
        {
            var senderType = sender.GetType();
            var boo = e.ClickedItem.GetType();
            var itemType = e.GetType();
            var groupId = ((CalenderDataModel)e.ClickedItem).Subject;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            var currentSelectedListBoxItem = this.listCalendarViewCanvas.ContainerFromIndex(0) as ListViewItem;
            Canvas canv = FindDescendant<Canvas>(currentSelectedListBoxItem);

        }
    }
}
