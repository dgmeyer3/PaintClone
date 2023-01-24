using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace PaintClone
{
    //TODO: Add erase/select modes, undo/redo, and enable shape drawing.
    public partial class MainWindow : Window
    {
        double brushDouble = 3;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog savedlg = new SaveFileDialog();
            savedlg.ShowDialog();
        }
        private void New_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog opendlg = new OpenFileDialog();
            opendlg.ShowDialog();
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Triangle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Upsize_Click(object sender, RoutedEventArgs e)
        {
            //brushSize textbox needs a method that updates the brushAttr height/width when manually changed outside of a button cick
            if(brushDouble < Int32.MaxValue)
            {
                brushDouble++;
            }

            brushSize.Text = brushDouble.ToString();
            
            strokeAttr.Height = brushDouble;
            strokeAttr.Width = brushDouble;
        }

        private void Downsize_Click(object sender, RoutedEventArgs e)
        {
            if(brushDouble > 1)
            {
                brushDouble--;
            }

            brushSize.Text = brushDouble.ToString();

            strokeAttr.Height = brushDouble;
            strokeAttr.Width = brushDouble;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void colorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string comboitem = colorBox.SelectedItem.ToString();
            var brushColor = comboitem.Split(' '); 

            strokeAttr.Color = (Color)ColorConverter.ConvertFromString(brushColor[1]);
        }
    }
}
