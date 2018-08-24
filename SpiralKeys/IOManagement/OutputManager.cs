using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpiralKeys.IOManagement
{
    public class OutputManager
    {
        private static OutputManager instance;
        private static object syncLock = new object();

        private OutputManager()
        {

        }

        public static OutputManager GetOutputManager()
        {
            if (instance == null)
            {
                lock (syncLock)
                {
                    if (instance == null)
                    {
                        instance = new OutputManager();
                    }
                }
            }

            return instance;
        }

        public void ExecuteKeySequence(string keySequence)
        {
            SendKeys.SendWait(keySequence);
        }
    }
}
