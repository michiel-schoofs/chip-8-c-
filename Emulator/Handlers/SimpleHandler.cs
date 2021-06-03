using System;
using _8BitEmu.Emulator.Extensions;

namespace _8BitEmu.Emulator.Handlers {
    class SimpleHandler : IHandler {
        private Random _random= new Random();
        private Registers _registers;
        private Stack _stack;

        public SimpleHandler(Registers register, Stack stack) {
            _registers = register;
            _stack = stack;
        }

        public void HandleOPCode(short opcode) {
            char identifier = opcode.GetIdentifier();
            switch (identifier) {
                case '1':
                    short address = opcode.GetArgument(3, 1);
                    Registers.IP = address;
                    break;
                case '3':
                    Handle3Code(opcode);
                    break;
                case 'c':
                    HandleCCode(opcode);
                    break;
                case 'a':
                    _registers.StoreValueInIRegister(opcode.GetArgument(3, 1));
                    Registers.IP += 2;
                    break;
                case '2':
                    Handle2Code(opcode);
                    break;
                case '6':
                    Handle6Code(opcode);
                    break;
                case '7':
                    Handle7Code(opcode);
                    break;
                case '4':
                    Handle4Code(opcode);
                    break;
            }
        }

        private void Handle2Code(short opcode) {
            _stack.PutOntostack(Registers.IP);
            short address = opcode.GetArgument(3,1);
            Registers.IP = address;
        }

        private void Handle4Code(short opcode) {
            byte to_compare = (byte)opcode.GetArgument(2, 2);
            int register_x = Convert.ToInt32(opcode.GetArgument(1, 1));
            byte value_in_x = _registers.GetValueInRegister(register_x);

            if (value_in_x != to_compare)
                Registers.IP += 2;

            Registers.IP += 2;
        }

        private void Handle3Code(short opcode) {
            byte value_to_compare = (byte)opcode.GetArgument(2, 2);
            int register_indx = opcode.GetArgument(1, 1);
            byte registervalue = _registers.GetValueInRegister(register_indx);
            if (value_to_compare == registervalue) {
                Registers.IP += 2;
            }

            Registers.IP += 2;
        }

        private void Handle6Code(short opcode) {
            int registerIndx = (int)opcode.GetArgument(1, 1);
            byte value = (byte)opcode.GetArgument(2, 2);
            _registers.StoreValueInRegister(registerIndx, value);
            Registers.IP += 2;
        }

        private void Handle7Code(short opcode) {
            byte to_add = (byte)opcode.GetArgument(2, 2);
            int register_indx = (int)opcode.GetArgument(1, 1);
            byte to_add_to = _registers.GetValueInRegister(register_indx);
            byte result = (byte)(to_add + to_add_to);
            _registers.StoreValueInRegister(register_indx, result);
            Registers.IP += 2;
        }

        private void HandleCCode(short opcode) {
            byte random = Convert.ToByte(_random.Next(0, 256));
            short arg = opcode.GetArgument(2, 2);
            byte result = Convert.ToByte(random & Convert.ToByte(arg));
            int registerIndx = Convert.ToInt32(opcode.GetArgument(1, 1));
            _registers.StoreValueInRegister(registerIndx, result);
            Registers.IP += 2;
        }
    }
}
