
using LiteBrite.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LiteBrite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const int ellipseSize = 14;
        public readonly string DefaultColor = "#FF383838";


        MainViewModel  mainVM = new MainViewModel();
        public MainWindow()
        {
            InitializeComponent(); 
            this.DataContext = mainVM;
            InitializeGrid();
          
        }



        private void InitializeGrid()
        {
            GridLengthConverter myGridLengthConverter = new GridLengthConverter();
            GridLength side = (GridLength)myGridLengthConverter.ConvertFromString("Auto");
            for (int i = 0; i < 50; i++)
            { 
                LayoutRoot.ColumnDefinitions.Add(new ColumnDefinition());
                LayoutRoot.ColumnDefinitions[i].Width = side;
                LayoutRoot.RowDefinitions.Add(new RowDefinition());
                LayoutRoot.RowDefinitions[i].Height = side;
            }
            int count = 0;
            Ellipse[,] ellipseObj = new Ellipse[50, 50];
            for (int row = 0; row < 50; row++)
            {
                for (int col = 0; col < 50; col++,count++)
                { 
                    ellipseObj[row, col] = new Ellipse();
                    ellipseObj[row, col].DataContext = mainVM.EllipsesCollection[count];
                    ellipseObj[row, col].Height = ellipseSize;
                    ellipseObj[row, col].Width = ellipseSize;

                    Binding bindingObj = new Binding("color");
                    bindingObj.Mode = BindingMode.TwoWay;
                    ellipseObj[row, col].SetBinding(Ellipse.FillProperty, bindingObj);
               
                    //  ellipseObj[row, col].Fill = new SolidColorBrush(System.Windows.Media.Color.FromRgb(56, 56, 56));
                    Grid.SetColumn(ellipseObj[row, col], col);
                    Grid.SetRow(ellipseObj[row, col], row);
                    LayoutRoot.Children.Add(ellipseObj[row, col]);
                }
            }

        }

        private void Button_MouseMove(object sender, MouseEventArgs e)
        {
            // Find the data behind the listBoxItem
            string theItem = ((Button)e.Source).Background.ToString();
            // initialize drag and drop
            // Step 2: create a DataObject containing the string to be "dragged"
            DataObject dragData = new DataObject(typeof(string), theItem);
            // Step 3: initialize the dragging
            DragDrop.DoDragDrop(this, dragData, DragDropEffects.Move);
        }


        private void GridDestination_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(string)) || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }
        private void GridDestination_Drop(object sender, DragEventArgs e)
        {

            try
            {
                if (e.Data.GetDataPresent(typeof(string)))
                {
                    String theItem = e.Data.GetData(typeof(string)).ToString();

                    Ellipse rec = (Ellipse)e.Source;

                    var converter = new System.Windows.Media.BrushConverter();
                    var brush = (Brush)converter.ConvertFromString(theItem);
                    rec.Fill = brush;
                    rec.Fill.Opacity = 0.7;
                    
                }
            }
            catch
            {

            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MediaButton.Content == FindResource("Off"))
            {
                MediaButton.Content = FindResource("On");
            }
            else
            {
                MediaButton.Content = FindResource("Off");
                var eclipseList = LayoutRoot.Children;

                foreach (var item in eclipseList)
                {
                    var ec = item as Ellipse;

                    var selectedColor = ec.Fill.ToString();

                    if (selectedColor != DefaultColor )
                    {
                        ec.Fill.Opacity = 0.7;
                    }
                }//end foreach

            }
        }

        private void MediaButton_Checked(object sender, RoutedEventArgs e)
        {
            var eclipseList = LayoutRoot.Children;

            foreach (var item  in eclipseList)
            {               
                var ec = item as Ellipse;

                var selectedColor = ec.Fill.ToString();

                if (selectedColor != DefaultColor)
                {

                    ec.Fill.Opacity = 1.0;
                }              
            }//end foreach
        }//end MediaButton
    }
}
