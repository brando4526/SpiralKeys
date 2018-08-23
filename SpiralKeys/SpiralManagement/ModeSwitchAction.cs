using SpiralKeys.SpiralManagement.Interfaces;
using SpiralKeys.UIControls;

namespace SpiralKeys.SpiralManagement
{
    public class ModeSwitchAction : IKeyAction
    {
        private SpiralSelector handle;
        private string modeName;

        public ModeSwitchAction(SpiralSelector controlHandle, string modeName)
        {
            handle = controlHandle;
            this.modeName = modeName;

        }

        public void ExecuteAction()
        {
            handle.SwitchMode(modeName);
        }
    }
}
