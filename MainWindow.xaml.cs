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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace P1ClockJoe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NumberLoop();
            var gradientAnimation = new DoubleAnimation
            {
                From = 0.5,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(2)),
                RepeatBehavior = RepeatBehavior.Forever
            };
            gradientAnimation.AutoReverse = true;

            var gradientBrush = new RadialGradientBrush
            {
                GradientStops = new GradientStopCollection
                {
                    new GradientStop(Colors.DarkGray, 0),
                    new GradientStop(Colors.Gray, 0.5),
                    new GradientStop(Colors.White, 1)
                },
                Center = new Point(0.5, 0.5),
                RadiusX = 0.5,
                RadiusY = 0.5
            };
            gradientBrush.BeginAnimation(RadialGradientBrush.RadiusXProperty, gradientAnimation);
            gradientBrush.BeginAnimation(RadialGradientBrush.RadiusYProperty, gradientAnimation);

            clockFace.Fill = gradientBrush;



        }
        DispatcherTimer timer;



        private void NumberLoop()
        {

            for (int i = 1; i <= 12; i++)
            {
                TextBlock txt = new TextBlock();
                switch (i)
                {
                    case 1:
                        txt.Text = "I";
                        break;
                    case 2:
                        txt.Text = "II";
                        break;
                    case 3:
                        txt.Text = "III";
                        break;
                    case 4:
                        txt.Text = "IV";
                        break;
                    case 5:
                        txt.Text = "V";
                        break;
                    case 6:
                        txt.Text = "VI";
                        break;
                    case 7:
                        txt.Text = "VII";
                        break;
                    case 8:
                        txt.Text = "VIII";
                        break;
                    case 9:
                        txt.Text = "IX";
                        break;
                    case 10:
                        txt.Text = "X";
                        break;
                    case 11:
                        txt.Text = "XI";
                        break;
                    case 12:
                        txt.Text = "XII";
                        break;
                }
                txt.Foreground = Brushes.Black;
                txt.FontWeight = FontWeights.Bold;

                txt.RenderTransform = new TranslateTransform();
                double angle = (i / 12.0) * 360 - 90;
                double radians = (Math.PI / 180) * angle;
                double x = Math.Cos(radians) * 175;
                double y = Math.Sin(radians) * 175;
                y -= 10;
                x -= 10;
                txt.RenderTransform = new TranslateTransform(x, y);
                mainCanvas.Children.Add(txt);
            }


        }


        private void Update()
        {
            DateTime now = DateTime.Now;
            SecRotation.Angle = 6 * now.Second + (now.Millisecond / 1000.0) * 6;
            MinRotation.Angle = 6 * (now.Minute + (now.Second / 60.0));
            HourRotation.Angle = 30 * (now.Hour + (now.Minute / 60.0));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            timer.Start();
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = -100;
            animation.To = -120;
            animation.Duration = new Duration(TimeSpan.FromSeconds(1));
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;


            RolexText.RenderTransform.BeginAnimation(TranslateTransform.YProperty, animation);

            UpdateDayCount();


        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            Update();
        }
        private void UpdateDayCount()
        {

            DateTime currentDate = DateTime.Now;
            int dateCount = currentDate.Day;
            TextBlock dateCountText = new TextBlock
            {
                Text = dateCount.ToString(),
                Foreground = System.Windows.Media.Brushes.Black,
                FontSize = 30,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            Rectangle dateCountRectangle = new Rectangle
            {
                Stroke = System.Windows.Media.Brushes.Black,
                StrokeThickness = 1,
                Width = 50,
                Height = 50,
                RadiusX = 10,
                RadiusY = 10
            };
            Canvas.SetLeft(dateCountRectangle, 115);
            Canvas.SetTop(dateCountRectangle, -7);
            mainCanvas.Children.Add(dateCountRectangle);
            Canvas.SetLeft(dateCountText, 125);
            Canvas.SetTop(dateCountText, -5);
            mainCanvas.Children.Add(dateCountText);

        }


    }
}
