using SpiralKeys.UIControls;
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
        private readonly SpiralSelector controlHandle;
        
        public List<SpiralKeyMode> SpiralKeyModes { get; set; }

        public SpiralModelManager(SpiralSelector controlHandle)
        {
            
            this.controlHandle = controlHandle;
            InitializeKeys();
            Keys = SpiralKeyModes[0].Keys;
            ResetIndexes();
        }

        public void ResetIndexes()
        {
            SelectedIndex = 0;
            StartIndex = 0;

        }

        public void ChangeKeyMode(string modeName)
        {
            //todo:error checking
            Keys = SpiralKeyModes.Find(x => x.Name.Contains(modeName)).Keys;
            ResetIndexes();
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

            SpiralKey shiftUpKey = new SpiralKey
            {
                Name = "shift",
                Content = "../assets/keyboard-shift.png",
                ContentType = KeyContentType.Image,
                KeyAction = new ModeSwitchAction(this.controlHandle, "Alphabet Upper Case")
            };
            SpiralKey shiftDownKey = new SpiralKey
            {
                Name = "shift down",
                Content = "../assets/keyboard-shift-filled.png",
                ContentType = KeyContentType.Image,
                KeyAction = new ModeSwitchAction(this.controlHandle, "Alphabet Lower Case")
            };
            SpiralKey specialsKey = new SpiralKey
            {
                Name = "special",
                Content = "123",
                ContentType = KeyContentType.Text,
                KeyAction = new ModeSwitchAction(this.controlHandle, "Specials")
            };
            SpiralKey alphabetKey = new SpiralKey
            {
                Name = "alphabet",
                Content = "ABC",
                ContentType = KeyContentType.Text,
                KeyAction = new ModeSwitchAction(this.controlHandle, "Alphabet Lower Case")
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
            alphaMode.Keys.Add(specialsKey);
            alphaMode.Keys.Add(enterKey);
            alphaMode.Keys.Add(backspaceKey);
            alphaMode.Keys.Add(spacebar);
            foreach (var character in alphabetString.ToCharArray())
            {
                alphaMode.Keys.Add(new SpiralKey
                {
                    Name=character.ToString(),
                    Content=character.ToString(),
                    ContentType=KeyContentType.Text,
                    KeyAction = new KeyPressAction(character.ToString())
                });
            }
            SpiralKeyModes.Add(alphaMode);

            //Capital letters mode
            SpiralKeyMode alphaCapMode = new SpiralKeyMode
            {
                Name = "Alphabet Upper Case",
                Keys = new List<SpiralKey>()
            };
            alphaCapMode.Keys.Add(shiftDownKey);
            alphaCapMode.Keys.Add(specialsKey);
            alphaCapMode.Keys.Add(enterKey);
            alphaCapMode.Keys.Add(backspaceKey);
            alphaCapMode.Keys.Add(spacebar);
            foreach (var character in alphabetCapsString.ToCharArray())
            {
                alphaCapMode.Keys.Add(new SpiralKey
                {
                    Name = character.ToString(),
                    Content = character.ToString(),
                    ContentType = KeyContentType.Text,
                    KeyAction = new KeyPressAction(character.ToString())
                });
            }
            SpiralKeyModes.Add(alphaCapMode);

            SpiralKeyMode specialsMode = new SpiralKeyMode
            {
                Name = "Specials",
                Keys = new List<SpiralKey>()
            };
            specialsMode.Keys.Add(alphabetKey);
            specialsMode.Keys.Add(enterKey);
            specialsMode.Keys.Add(backspaceKey);
            specialsMode.Keys.Add(spacebar);
            foreach (var character in specialsString.ToCharArray())
            {
                specialsMode.Keys.Add(new SpiralKey
                {
                    Name = character.ToString(),
                    Content = character.ToString(),
                    ContentType = KeyContentType.Text
                });
            }
            SpiralKeyModes.Add(specialsMode);

        }
    }
}
