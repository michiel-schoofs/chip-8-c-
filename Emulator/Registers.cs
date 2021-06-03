using System;

namespace _8BitEmu.Emulator {
    class Registers {
        private byte[] registers = new byte[16];
        private short IRegister = new short();
        public static short IP = 0x0200;
        public static byte SP = 0x0;

        public void StoreValueInRegister(int register, byte value) {
            if (register > registers.Length || register < 0)
                throw new ArgumentException("trying to write to register that doesn't exist");

            registers[register] = value;
        }

        public void StoreValueInIRegister(short value) {
            IRegister = value;
        }

        public short GetIRegister() {
            return IRegister;
        }

        public byte GetValueInRegister(int register) {
            if (register > registers.Length || register < 0)
                throw new ArgumentException("trying to write to register that doesn't exist");

            return registers[register];
        }
    }
}
