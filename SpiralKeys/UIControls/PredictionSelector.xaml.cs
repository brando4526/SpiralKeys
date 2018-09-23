using System.Collections.Generic;
using System.Windows.Controls;

namespace SpiralKeys.UIControls
{
    /// <summary>
    /// Interaction logic for PredictionSelector.xaml
    /// </summary>
    public partial class PredictionSelector : UserControl
    {
        private List<string> predictionOptions;

        public PredictionSelector()
        {
            InitializeComponent();
            predictionOptions = new List<string> { "optionone", "optiontwo", "optionthree", "optionfour" };
            PredictionListBox.ItemsSource = predictionOptions;
        }
    }
}
