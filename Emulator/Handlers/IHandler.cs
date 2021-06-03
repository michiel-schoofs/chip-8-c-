using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8BitEmu.Emulator.Handlers {
    interface IHandler {
        void HandleOPCode(short opcode);
    }
}
