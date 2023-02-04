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
using System.Windows.Ink;
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
        bool inkCanvasEdit = true;
        public string colorStr = "Black";
        Point prevPoint;
        Point currPoint;



        public MainWindow()
        {
            InitializeComponent();
        }
        private enum myShapes
        {
            Line, Circle, Rectangle, None
        }

        private myShapes currShape = myShapes.None;

        //ink canvas events
        private void Brush_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            inkCanvasEdit = true;
            currShape = myShapes.None;
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {
            currShape = myShapes.Line;
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
            inkCanvasEdit = false;
        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            currShape = myShapes.Rectangle;
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
            inkCanvasEdit = false;
        }

        private void Circle_Click(object sender, RoutedEventArgs e)
        {
            currShape = myShapes.Circle;
            inkCanvas.EditingMode = InkCanvasEditingMode.None;
            inkCanvasEdit = false;
        }

        //overlay canvas events
        private void myOverlayCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            prevPoint = e.GetPosition(this);
        }

        private void myOverlayCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                currPoint = e.GetPosition(this);
            }
        }

        private void myOverlayCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            switch (currShape)
            {
                case myShapes.Rectangle:
                    DrawRectangle();
                    break;
                case myShapes.Circle:
                    DrawCircle();
                    break;
                case myShapes.Line:
                    DrawLine();
                    break;
            }
        }
        private void DrawRectangle()
        {
            Color color = (Color)ColorConverter.ConvertFromString(colorStr);
            SolidColorBrush shapeBrush = new SolidColorBrush(color);

            Rectangle newRect = new Rectangle()
            {
                Stroke = shapeBrush,
                StrokeThickness = 3,
                Height = 10,
                Width = 10
            };

            newRect.SetValue(Canvas.LeftProperty, Math.Min(prevPoint.X, currPoint.X));
            newRect.SetValue(Canvas.TopProperty, Math.Min(prevPoint.Y - 100, currPoint.Y - 100));
            newRect.Width = Math.Abs(currPoint.X - prevPoint.X);
            newRect.Height = Math.Abs(currPoint.Y - prevPoint.Y);

            myOverlayCanvas.Children.Add(newRect);
        }
        private void DrawCircle()
        {
            Color color = (Color)ColorConverter.ConvertFromString(colorStr);
            SolidColorBrush shapeBrush = new SolidColorBrush(color);

            Ellipse newEllipse = new Ellipse()
            {
                Stroke = shapeBrush,
                StrokeThickness = 3,
                Height = 10,
                Width = 10
            };

            newEllipse.SetValue(Canvas.LeftProperty, Math.Min(prevPoint.X, currPoint.X));
            newEllipse.SetValue(Canvas.TopProperty, Math.Min(prevPoint.Y - 100, currPoint.Y - 100));
            newEllipse.Width = Math.Abs(currPoint.X - prevPoint.X);
            newEllipse.Height = Math.Abs(currPoint.Y - prevPoint.Y);

            myOverlayCanvas.Children.Add(newEllipse);
        }
        private void DrawLine()
        {
            Color color = (Color)ColorConverter.ConvertFromString(colorStr);
            SolidColorBrush shapeBrush = new SolidColorBrush(color);

            Line newLine = new Line()
            {

                Stroke = shapeBrush,
                StrokeThickness = 3,
                X1 = prevPoint.X,
                Y1 = prevPoint.Y - 100,
                X2 = currPoint.X,
                Y2 = currPoint.Y - 100
            };

            myOverlayCanvas.Children.Add(newLine);
        }
        private void ColorChange_click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            strokeAttr.Color = (Color)ColorConverter.ConvertFromString(button.Name.ToString());
            colorStr = strokeAttr.Color.ToString();

        }
        private void upSize_Click(object sender, RoutedEventArgs e)
        {
            strokeAttr.Width += 1;
            strokeAttr.Height += 1;
        }


        //i probably need to update brushdouble here so it can go in the label brushSize.Content test
        private void downSize_Click(object sender, RoutedEventArgs e)
        {
            if (strokeAttr.Width > 1 && strokeAttr.Height > 1)
            {
                strokeAttr.Width -= 1;
                strokeAttr.Height -= 1;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDlg = new SaveFileDialog();
            saveDlg.ShowDialog();
            saveDlg.DefaultExt = ".bmp";
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDlg= new OpenFileDialog();
            openDlg.ShowDialog();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.ShowDialog();
        }
    }
}