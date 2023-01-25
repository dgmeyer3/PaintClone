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

        private myShapes currShape = myShapes.Rectangle;

        //ink canvas events
        private void Brush_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            inkCanvasEdit = true;
            currShape = myShapes.None;
        }

        private void Line_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rectangle_Click(object sender, RoutedEventArgs e)
        {
            currShape = myShapes.Rectangle;
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
            Rectangle newRect = new Rectangle()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Height = 10,
                Width = 10
            };

            newRect.SetValue(Canvas.LeftProperty, Math.Min(prevPoint.X, currPoint.X));
            newRect.SetValue(Canvas.TopProperty, Math.Min(prevPoint.Y - 50, currPoint.Y - 50));
            newRect.Width = Math.Abs(currPoint.X - prevPoint.X);
            newRect.Height = Math.Abs(currPoint.Y - prevPoint.Y);

            myOverlayCanvas.Children.Add(newRect);
        }
        private void DrawCircle()
        {
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Height = 10,
                Width = 10
            };

            newEllipse.SetValue(Canvas.LeftProperty, Math.Min(prevPoint.X, currPoint.X));
            newEllipse.SetValue(Canvas.TopProperty, Math.Min(prevPoint.Y - 50, currPoint.Y - 50));
            newEllipse.Width = Math.Abs(currPoint.X - prevPoint.X);
            newEllipse.Height = Math.Abs(currPoint.Y - prevPoint.Y);

            myOverlayCanvas.Children.Add(newEllipse);
        }
        private void DrawLine()
        {

        }
    }
}
