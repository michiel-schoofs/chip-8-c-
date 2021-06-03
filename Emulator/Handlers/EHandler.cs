using _8BitEmu.Emulator.Extensions;
using System;

namespace _8BitEmu.Emulator.Handlers {
    class EHandler : IHandler {
        public static byte PressedKey = 0x0;

        public void HandleOPCode(short opcode) {
            string identifier = opcode.GetArgument(2, 2).ToString("x");
            switch (identifier) {
                case "e9":
                    HandleE9Code(opcode);
                    break;
                case "a1":
                    HandleA1Code(opcode);
                    break;
                default:
                    throw new ArgumentException("unhandled opcode");
            }
        }

        private void HandleA1Code(short opcode) {
            byte key = (byte)opcode.GetArgument(1, 1);
            if (key != PressedKey) {
                Registers.IP += 2;
            }

            Registers.IP += 2;
        }

        public void HandleE9Code(short opcode) {
            byte key = (byte)opcode.GetArgument(1, 1);
            if (key == PressedKey) {
                Registers.IP += 2;
            }

            Registers.IP += 2;
        }
    }
}
