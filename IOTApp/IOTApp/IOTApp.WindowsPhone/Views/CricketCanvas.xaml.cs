using IOTApp.DataModel;
using IOTApp.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System.Runtime;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace IOTApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CricketCanvas : Page
    {
        public CricketCanvas()
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            List<CricketDataModel> abc = await CricketRepository.GetMatchData();
            //var items = new ObservableCollection<LvItem>();
            ////listCricketView.DataContext = abc;
            //foreach(var data in abc){
            //    var canvas=new Canvas();
            //    //TextBlock txt1 = new TextBlock();
            //    //txt1.FontSize = 14;
            //    //txt1.Text = "Hello World!";
            //    canvas.Children.Add(new Ellipse() { Width = 50, Height = 50, Fill = new SolidColorBrush(Colors.Yellow) });
            //    items.Add(new LvItem() { Graph = canvas });
            //    }
            //myListView.ItemsSource = items;

            listCricketViewCanvas.DataContext = abc;
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

        private void Score_ItemClick(object sender, ItemClickEventArgs e)
        {
            var senderType = sender.GetType();
            var boo = e.ClickedItem.GetType();
            var itemType = e.GetType();
            var groupId = ((CricketDataModel)e.ClickedItem).GameTitle;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //  ListViewItem item = this.listCricketViewCanvas.ItemContainerGenerator.ContainerFromIndex(0) as ListViewItem;
            var currentSelectedListBoxItem = this.listCricketViewCanvas.ContainerFromIndex(0) as ListViewItem;
            Canvas canv = FindDescendant<Canvas>(currentSelectedListBoxItem);
            //Canvas canv2 = (Canvas)canv.MemberwiseClone();

        }

       

        

    }

}
