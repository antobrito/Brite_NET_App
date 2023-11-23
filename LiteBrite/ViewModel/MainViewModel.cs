
using LiteBrite.Helpers;
using LiteBrite.Model;
using LiteBrite.View;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

/****************************************************
	Coder:		   Antonio Brito
	Project:	   BRITO -LITE - GUI Application
	File Name:	   Class : MainViewModel

*****************************************************/

namespace LiteBrite.ViewModel
{
    class MainViewModel:DependencyObject
    {
        public RelayCommand SaveFileCommand { get; set; }
        public RelayCommand OpenFileCommand { get; set; }

        public RelayCommand AboutCommand { get; set; }
        public RelayCommand NewCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand LightCommand { get; set; }

        public readonly Brush DefaultColor = new SolidColorBrush(System.Windows.Media.Color.FromRgb(56, 56, 56));
 
        
        //=======================================================//
        //                   Constructor                         //
        //=======================================================//

        public MainViewModel()
        {
            SaveFileCommand = new RelayCommand(Savefile);
            OpenFileCommand = new RelayCommand(Openfile);
            AboutCommand = new RelayCommand(AboutWindow);
            NewCommand = new RelayCommand(NewGame);
            CloseCommand = new RelayCommand(CloseWindow);
            for (int row = 0; row < 2500; row++)
            {
                EllipseData ellipse = new EllipseData();
                ellipse.color = DefaultColor;
                EllipsesCollection.Add(ellipse);
            }

        }

        //=======================================================//
        //                   EllipseCollection                   //
        //=======================================================//

        public ObservableCollection<EllipseData> EllipsesCollection
        {
            get { return (ObservableCollection<EllipseData>)GetValue(EllipsesCollectionProperty); }
            set { SetValue(EllipsesCollectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EllipsesCollection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EllipsesCollectionProperty =
            DependencyProperty.Register("EllipsesCollection", typeof(ObservableCollection<EllipseData>), typeof(MainWindow), new UIPropertyMetadata(new ObservableCollection<EllipseData>()));


        //=======================================================//
        //                AboutWindow Method                        //
        //=======================================================//

        private void AboutWindow(object obj)
        {
            var win = new About();
            win.ShowDialog();
        }


        //=======================================================//
        //                AboutWindow Method                        //
        //=======================================================//

        private void NewGame(object obj)
        {
            int counter = 0;
            foreach (var item in EllipsesCollection)
            {

                EllipsesCollection[counter].color = DefaultColor;
                counter++;
            }
        }

        //=======================================================//
        //                Openfile Method                        //
        //=======================================================//

        private void Openfile(object obj)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog1.Filter = "json files (*.json)|*.json|JSON files (*.json)|*.json";
            openFileDialog1.DefaultExt = ".json";

            if (openFileDialog1.ShowDialog() == true)
            {
                using (StreamReader fileOp = new StreamReader(openFileDialog1.FileName))
                    try
                    {
                         JsonSerializer serializer = new JsonSerializer();
                        //EllipsesCollection.Clear();
                        var te = JsonConvert.DeserializeObject<List<EllipseData>>(fileOp.ReadToEnd());
                        
                        int counter = 0;
                        foreach (var item in te)
                        {

                            EllipsesCollection[counter].color = item.color;
                            
                            counter++;                         
                        }
                  
                    }
                    catch (Exception ex)
                    {
                        
                        MessageBox.Show(Properties.Resources.ErrorReadFile + ex.Message);
                    }
               
            }
        }

        //=======================================================//
        //                CloseWindow Method                     //
        //=======================================================//
        void CloseWindow(object parameter)
        {

            ((MainWindow)Application.Current.Windows[0]).Close();
        }
        //=======================================================//
        //                Savefile Method                        //
        //=======================================================//

        void Savefile(object parameter)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog1.Filter = "json files (*.json)|*.json|JSON files (*.json)|*.json";
            saveFileDialog1.DefaultExt = ".json";


            if (saveFileDialog1.ShowDialog() == true)
            {
                string path = saveFileDialog1.FileName;
                //Overwrite file
                 var EllipseString = JsonConvert.SerializeObject(EllipsesCollection);

                    StreamWriter outputUpdated = new StreamWriter(path);
                    outputUpdated.WriteLine(EllipseString);
                    outputUpdated.Close();
                    
                }
            }
        }//end Savefile
    }




