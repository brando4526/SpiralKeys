using SpiralKeys.SpiralManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SpiralKeys.SpiralManagement
{
    public class SpiralKey
    {
        public string Name { get; set; }
        public StackPanel UIComponent { get; set; }
        public IKeyAction KeyAction { get; set; }
    }
}
