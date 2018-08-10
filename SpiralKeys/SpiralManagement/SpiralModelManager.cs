using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpiralKeys.SpiralManagement
{
    public class SpiralModelManager
    {
        public List<string> Keys { get; set; }
        public int StartIndex { get; set; }
        public int SelectedIndex { get; set; }
        public int EndIndex { get; set; }

        public SpiralModelManager()
        {
            Keys = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            StartIndex = 0;
            SelectedIndex = 0;
        }

        public void ResetIndex()
        {
            SelectedIndex = 0;
        }

        public string GetItemForInitialization()
        {
            string itemToReturn = Keys.ElementAt(SelectedIndex);
            EndIndex = SelectedIndex;

            SelectedIndex = GetNextIndex(SelectedIndex);

            return itemToReturn;
        }

        public string GetNextItem()
        {
            EndIndex = GetNextIndex(EndIndex);
            StartIndex = GetNextIndex(StartIndex);
            return Keys.ElementAt(EndIndex);
        }

        public string GetPreviousItem()
        {
            StartIndex = GetPreviousIndex(StartIndex);
            EndIndex = GetPreviousIndex(EndIndex);
            return Keys.ElementAt(StartIndex);
        }

        private int GetNextIndex(int index)
        {
            if (index == Keys.Count - 1)
            {
                return 0;
            }
            else
            {
                return ++index;
            }
        }

        private int GetPreviousIndex(int index)
        {
            if (index == 0)
            {
                return Keys.Count - 1;
            }
            else
            {
                return --index;
            }
        }
    }
}
