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
        }

        private void InitializeSpiral()
        {
            SpiralList = new LinkedList<SpiralItem>();
            MinKeyIndex = 0;

            for (int i = MinKeyIndex; i < VisibleKeys; ++i)
            {
                SpiralList.AddLast(new SpiralItem { Key = spiralModel.GetItemForInitialization(), Index = i });
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
                UIElement uIElement = CreateUIElement(item.Key);
                Canvas.SetLeft(uIElement, WheelCenter.X + alpha * Math.Cos(theta));
                Canvas.SetTop(uIElement, WheelCenter.Y + alpha * Math.Sin(theta));
                SpiralCanvas.Children.Add(uIElement);
                alphaIndex++;

                if (SelectedKeyIndex == item.Index)
                {
                    Ellipse selectedCircle = new Ellipse
                    {
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeThickness = 1,

                        Width = 30,
                        Height = 30
                    };
                    Canvas.SetLeft(selectedCircle, WheelCenter.X + alpha * Math.Cos(theta)-5);
                    Canvas.SetTop(selectedCircle, (WheelCenter.Y + alpha * Math.Sin(theta)) + 2);
                    SpiralCanvas.Children.Add(selectedCircle);
                }
            }

        }

        public void IncrementSelector()
        {
            MaxKeyIndex = ++MaxKeyIndex;
            MinKeyIndex = ++MinKeyIndex;
            SpiralList.AddLast(new SpiralItem { Key = spiralModel.GetNextItem(), Index = MaxKeyIndex });
            SpiralList.RemoveFirst();
            SelectedKeyIndex++;
            UpdateKeyWheel();

        }

        public void DecrementSelector()
        {
            MaxKeyIndex = --MaxKeyIndex;
            MinKeyIndex = --MinKeyIndex;
            SpiralList.AddFirst(new SpiralItem { Key = spiralModel.GetPreviousItem(), Index = MinKeyIndex });
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

        private UIElement CreateUIElement(SpiralKey key)
        {
            if (key.ContentType == KeyContentType.Image)
            {
                return new Image
                {
                    Height = KeyFontSize+3,
                    Width = KeyFontSize+3,
                    Source = new BitmapImage(new Uri(key.Content, UriKind.Relative))
                };
            }
            else
            {
                return new TextBlock
                {
                    Text = key.Content,
                    FontSize = KeyFontSize,
                };
            }
        }
    }
}

