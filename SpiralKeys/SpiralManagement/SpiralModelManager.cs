using System.Collections.Generic;
using System.Linq;

namespace SpiralKeys.SpiralManagement
{
    public class SpiralModelManager
    {
        public List<SpiralKey> Keys { get; set; }
        public int StartIndex { get; set; }
        public int SelectedIndex { get; set; }
        public int EndIndex { get; set; }
        public SpiralKeyMode SelectedKeyMode { get; set; }
        public List<SpiralKeyMode> SpiralKeyModes { get; set; }

        public SpiralModelManager()
        {
            InitializeKeys();
            Keys = SpiralKeyModes[0].Keys;
            StartIndex = 0;
            SelectedIndex = 0;
        }

        public void ResetIndex()
        {
            SelectedIndex = 0;
        }

        public SpiralKey GetItemForInitialization()
        {
            SpiralKey itemToReturn = Keys.ElementAt(SelectedIndex);
            EndIndex = SelectedIndex;

            SelectedIndex = GetNextIndex(SelectedIndex);

            return itemToReturn;
        }

        public SpiralKey GetNextItem()
        {
            EndIndex = GetNextIndex(EndIndex);
            StartIndex = GetNextIndex(StartIndex);
            return Keys.ElementAt(EndIndex);
        }

        public SpiralKey GetPreviousItem()
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


        private void InitializeKeys()
        {
            string alphabetString = "abcdefghijklmnopqrstuvwxyz";
            string alphabetCapsString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string specialsString = "0123456789~!@#$%^&*()_+-={}[]|:\"'<>,.?/\\";

            SpiralKey shiftUpKey= new SpiralKey
            {
                Name = "shift",
                Content = "../assets/keyboard-shift.png",
                ContentType = KeyContentType.Image
            };
            SpiralKey spacebar = new SpiralKey
            {
                Name = "space",
                Content = "../assets/space_bar.png",
                ContentType = KeyContentType.Image
            };
            SpiralKey backspaceKey = new SpiralKey
            {
                Name = "delete",
                Content = "../assets/backspace.png",
                ContentType = KeyContentType.Image
            };
            SpiralKey enterKey = new SpiralKey
            {
                Name = "enter",
                Content = "../assets/keyboard-return.png",
                ContentType = KeyContentType.Image
            };
            SpiralKeyModes = new List<SpiralKeyMode>();

            SpiralKeyMode alphaMode = new SpiralKeyMode
            {
                Name = "Alphabet Lower Case",
                Keys = new List<SpiralKey>()
            };
            alphaMode.Keys.Add(shiftUpKey);
            alphaMode.Keys.Add(enterKey);
            alphaMode.Keys.Add(backspaceKey);
            alphaMode.Keys.Add(spacebar);
            foreach (var character in alphabetString.ToCharArray())
            {
                alphaMode.Keys.Add(new SpiralKey
                {
                    Name=character.ToString(),
                    Content=character.ToString(),
                    ContentType=KeyContentType.Text
                });
            }
            SpiralKeyModes.Add(alphaMode);

        }
    }
}
