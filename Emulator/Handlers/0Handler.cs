using System;

namespace _8BitEmu.Emulator.Handlers {
    class _0Handler : IHandler {
        private Stack _stack;
        private Screen _screen;

        public _0Handler(Stack stack, Screen screen) {
            _stack = stack;
            _screen = screen;
        }

        public void HandleOPCode(short opcode) {
            string identifier = opcode.ToString("x");
            if (identifier.Equals("ee")) {
                short ip = _stack.PopFromStack();
                Registers.IP = ip;
                Registers.IP += 2;
            } else if(identifier.Equals("e0")){
                _screen.EmptyScreen();
                Registers.IP += 2;
            }
        }
    }
}
