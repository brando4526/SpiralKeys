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
            predictionOptions = new List<string> { "the", "quick", "brown", "fox","jumped" };
            PredictionListBox.ItemsSource = predictionOptions;
        }
    }
}
