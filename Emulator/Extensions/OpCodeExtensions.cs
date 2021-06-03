using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8BitEmu.Emulator.Extensions {
    public static class OpCodeExtensions {
        public static char GetIdentifier(this short opcode) {
            var result = opcode & 0xF000;
            result >>= 12;
            return Convert.ToChar(result.ToString("x"));
        }

        public static short GetArgument(this short opcode,int length, int offset) {
            if (length + offset > 4 || length < 0 || offset < 0)
                throw new ArgumentException("Something went wrong with the length and offset");

            string hex = new string('0', offset) + new string('F', length) + new string('0', 4 - offset - length);
            short val = Int16.Parse(hex, System.Globalization.NumberStyles.HexNumber);

            var result = opcode & val;
            result >>= 4 * (4 - offset - length);

            return Convert.ToInt16(result);
        }
    }
}
