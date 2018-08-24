using SpiralKeys.IOManagement;
using SpiralKeys.SpiralManagement.Interfaces;

namespace SpiralKeys.SpiralManagement
{
    public class KeyPressAction : IKeyAction
    {
        private readonly OutputManager outputManager;
        private string characterString;

        public KeyPressAction(string characterString)
        {
            this.characterString = characterString;
            outputManager = OutputManager.GetOutputManager();
        }

        public void ExecuteAction()
        {
            //todo: better error checking
            outputManager.ExecuteKeySequence(characterString);
        }
    }
}
