using _8BitEmu.Emulator.Extensions;
using System;
using System.Collections.Generic;

namespace _8BitEmu.Emulator.Handlers {
    class MathHandler : IHandler {
        private Registers _registers;
        private Dictionary<char, Func<short,bool>> opcodes = new Dictionary<char, Func<short,bool>>();

        public MathHandler(Registers registers) {
            _registers = registers;
            BuildDictionary();
        }

        private void BuildDictionary() {
            opcodes.Add('2', ExecuteCode2);
            opcodes.Add('4', ExecuteCode4);
            opcodes.Add('0', ExecuteCode0);
        }

        public void HandleOPCode(short opcode) {
            char identifier = Convert.ToChar(opcode.GetArgument(1, 3).ToString("x"));
            
            if (!opcodes.ContainsKey(identifier))
                throw new ArgumentException("Unrecognized opcode");

            opcodes[identifier].Invoke(opcode);
        }

        public bool ExecuteCode0(short opcode) {
            int register_x = opcode.GetArgument(1, 1);
            int register_y = opcode.GetArgument(1, 2);

            _registers.StoreValueInRegister(register_x, _registers.GetValueInRegister(register_y));
            Registers.IP += 2;

            return true;
        }

        public bool ExecuteCode4(short opcode) {
            int register_x = opcode.GetArgument(1, 1);
            int register_y = opcode.GetArgument(1, 2);

            byte val_1 = _registers.GetValueInRegister(register_x);
            byte val_2 = _registers.GetValueInRegister(register_y);

            //overflow bit
            _registers.StoreValueInRegister(15, 0x0);
            if (val_2 > (byte.MaxValue - val_1)) {
                _registers.StoreValueInRegister(15, 0x1);
            }

            byte res = (byte)(val_1 + val_2);
            _registers.StoreValueInRegister(register_x, res);

            Registers.IP += 2;
            return true;
        }

        public bool ExecuteCode2(short opcode) {
            int register_x = (int)Convert.ToInt16(opcode.GetArgument(1, 2));
            int register_y = (int)Convert.ToInt16(opcode.GetArgument(1, 3));
            byte val_1 = _registers.GetValueInRegister(register_x);
            byte val_2 = _registers.GetValueInRegister(register_y);
            byte res = (byte)(val_1 & val_2);
            _registers.StoreValueInRegister(register_x, res);
            Registers.IP += 2;
            return true;
        }
    }
}
