using System;
using System.IO;
using _8BitEmu.Emulator.Extensions;

namespace _8BitEmu.Emulator {
    public class CPU {
        //private StreamWriter sw;
        private short _currentOPCode;
        public static ROM rom;
        public static Screen _sc;

        public CPU(Screen sc) {
            //Directory.CreateDirectory("logs");
            //sw = new StreamWriter(File.Create("logs/log-mine.txt"));
            _sc = sc; 
            Initialize();
        }

        public void Initialize() {
            rom = new ROM();
            rom.LoadInApplication("Programs/pong2.ch8");
        }

        public void Tick() {
            _currentOPCode = rom.ReadDataAt(Registers.IP);
            //sw.WriteLine(_currentOPCode.ToString("x"));
            char identifier = _currentOPCode.GetIdentifier();
            OPCodeDictionary.HandleOpCode(_currentOPCode, identifier);
        }
    }
}
