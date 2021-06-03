using _8BitEmu.Emulator.Extensions;

namespace _8BitEmu.Emulator.Handlers {
    class DisplayHandler : IHandler {
        private Screen _screen;
        private Registers _registers;

        public DisplayHandler(Screen screen, Registers registers) {
            _screen = screen;
            _registers = registers;
        }

        public void HandleOPCode(short opcode) {
            short iregister = _registers.GetIRegister();
            int n_bytes = opcode.GetArgument(1,3);
            byte[] ar = CPU.rom.ReadNBytesAt(iregister, n_bytes);
            int x = _registers.GetValueInRegister(opcode.GetArgument(1, 1));
            int y = _registers.GetValueInRegister(opcode.GetArgument(1, 2));
            _screen.DisplayBytesOnTheScreen(ar, x, y);
            _registers.StoreValueInRegister(15, _screen.pixelErased?(byte)0x01:(byte)0x00);
            Registers.IP += 2;
        }
    }
}
