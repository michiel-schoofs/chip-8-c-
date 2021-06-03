using System;
using System.Collections.Generic;
using _8BitEmu.Emulator.Extensions;

namespace _8BitEmu.Emulator.Handlers {
    class FHandler : IHandler {
        private Registers registers;
        private Dictionary<string,Func<short,bool>> functions = new Dictionary<string, Func<short, bool>>();
        public static byte key = 0x0;

        public FHandler(Registers reg) {
            registers = reg;
            BuildDictionary();
        }

        public void BuildDictionary() {
            functions.Add("a", HandleKeyInput);
            functions.Add("18", Beep);
            functions.Add("1e", AddIToRegister);
            functions.Add("65", ReadFromMemoryAndPutInRegisters);
        }

        public void HandleOPCode(short opcode) {
            string identifier = opcode.GetArgument(2, 2).ToString("x");

            if (!functions.ContainsKey(identifier))
                throw new ArgumentException("Unhandled opcode");

            functions[identifier].Invoke(opcode);
        }

        public bool ReadFromMemoryAndPutInRegisters(short opcode) {
            int n = opcode.GetArgument(1, 1) + 1;
            byte[] res = CPU.rom.ReadNBytesAt(registers.GetIRegister(), n);
            
            for (int i = 0; i < n; i++) {
                registers.StoreValueInRegister(i, res[i]);
            }

            Registers.IP += 2;
            return true;
        }

        public bool AddIToRegister(short opcode) {
            int register_x = opcode.GetArgument(1, 1);
            int val_x = registers.GetValueInRegister(register_x);
            int val_i = registers.GetIRegister();
            short res = (short)(val_i + val_x);
            registers.StoreValueInIRegister(res);
            Registers.IP += 2;
            return true;
        }

        public bool Beep(short opcode) {
            int register_x = opcode.GetArgument(1, 1);
            int value = registers.GetValueInRegister(register_x);
            int ms = (value*(1000/60));
            Console.Beep(500, ms);
            Registers.IP += 2;
            return true;
        }

        public bool HandleKeyInput(short opcode) {
            if (key != 0x0) {
                int registerindx = opcode.GetArgument(1, 1);
                registers.StoreValueInRegister(registerindx, key);
                key = 0x0;
                Registers.IP += 2;
            }
            
            return true;
        }
    }
}
