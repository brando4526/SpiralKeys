using SpiralKeys.SpiralManagement.Interfaces;
using System;

namespace SpiralKeys.SpiralManagement
{
    public class SpiralKey
    {
        public string Name { get; set; }
        public String Content { get; set; }
        public KeyContentType ContentType { get; set; }
        public IKeyAction KeyAction { get; set; }
    }

    public enum KeyContentType { Image, Text}
}
