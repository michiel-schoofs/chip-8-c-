namespace _8BitEmu.Emulator {
    class Stack {
        private short[] stack = new short[16];
        
        public void PutOntostack(short value) {
            stack[Registers.SP] = value;
            Registers.SP += 1;
        }

        public short PopFromStack() {
            Registers.SP -= 1;
            return stack[Registers.SP];
        }
    }
}
