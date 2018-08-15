using SpiralKeys.SpiralManagement;
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

namespace SpiralKeys.UIControls
{
    /// <summary>
    /// Interaction logic for SpiralSelector.xaml
    /// </summary>
    public partial class SpiralSelector : UserControl
    {
        public Point WheelCenter { get; set; }
        public int VisibleKeys { get; set; }
        public LinkedList<SpiralItem> SpiralList { get; set; }
        public int MaxKeyIndex { get; set; }
        public int MinKeyIndex { get; set; }
        public double InnerRadius { get; set; }
        public double RadiusSpread { get; set; }
        public int KeyFontSize { get; set; }
        public int SelectedKeyIndex { get; set; }
        private SpiralModelManager spiralModel;

        public SpiralSelector()
        {
            InitializeComponent();

            WheelCenter = new Point(SpiralCanvas.Width / 2, SpiralCanvas.Height / 2);
            VisibleKeys = 36;
            InnerRadius = 50.0;
            RadiusSpread = 4.0;
            KeyFontSize = 20;
            SelectedKeyIndex = 18;
            spiralModel = new SpiralModelManager();

            InitializeSpiral();
            DrawSpiral();

            Image image = new Image();
            image.Height = 24;
            image.Width = 24;
            image.Source = new BitmapImage(new Uri("../assets/keyboard-shift.png", UriKind.Relative));
            SpiralCanvas.Children.Add(image);

        }

        private void InitializeSpiral()
        {
            SpiralList = new LinkedList<SpiralItem>();
            MinKeyIndex = 0;

            for (int i = MinKeyIndex; i < VisibleKeys; ++i)
            {

                TextBlock keyLabel = new TextBlock
                {
                    Text = spiralModel.GetItemForInitialization(),
                    FontSize = KeyFontSize,

                };
                SpiralList.AddLast(new SpiralItem { Content = keyLabel, Index = i });
                MaxKeyIndex = i;

            }
        }

        private void DrawSpiral()
        {
            double angle = Math.PI / 12;
            int alphaIndex = 0;

            foreach (var item in SpiralList)
            {
                var theta = angle * item.Index;
                var alpha = InnerRadius + RadiusSpread * alphaIndex;
                Canvas.SetLeft(item.Content, WheelCenter.X + alpha * Math.Cos(theta));
                Canvas.SetTop(item.Content, WheelCenter.Y + alpha * Math.Sin(theta));
                SpiralCanvas.Children.Add(item.Content);
                alphaIndex++;

                if (SelectedKeyIndex == item.Index)
                {
                    Ellipse selectedCircle = new Ellipse
                    {
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeThickness = 1,

                        Width = 25,
                        Height = 25
                    };
                    Canvas.SetLeft(selectedCircle, WheelCenter.X + alpha * Math.Cos(theta)-5);
                    Canvas.SetTop(selectedCircle, (WheelCenter.Y + alpha * Math.Sin(theta)) + 2);
                    SpiralCanvas.Children.Add(selectedCircle);
                }
            }

        }

        public void IncrementSelector()
        {
            TextBlock keyLabel = new TextBlock
            {
                Text = spiralModel.GetNextItem(),
                FontSize = KeyFontSize,
            };
            MaxKeyIndex = ++MaxKeyIndex;
            MinKeyIndex = ++MinKeyIndex;
            SpiralList.AddLast(new SpiralItem { Content = keyLabel, Index = MaxKeyIndex });
            SpiralList.RemoveFirst();
            SelectedKeyIndex++;
            UpdateKeyWheel();

        }

        public void DecrementSelector()
        {
            TextBlock keyLabel = new TextBlock
            {
                Text = spiralModel.GetPreviousItem(),
                FontSize = KeyFontSize,
            };
            MaxKeyIndex = --MaxKeyIndex;
            MinKeyIndex = --MinKeyIndex;
            SpiralList.AddFirst(new SpiralItem { Content = keyLabel, Index = MinKeyIndex });
            SpiralList.RemoveLast();
            SelectedKeyIndex--;
            UpdateKeyWheel();
        }

        private void UpdateKeyWheel()
        {
            Dispatcher.Invoke((Action)(() =>
            {
                SpiralCanvas.Children.Clear();
                DrawSpiral();
            }));
        }
    }
}

