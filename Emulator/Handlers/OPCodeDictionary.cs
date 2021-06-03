using _8BitEmu.Emulator.Handlers;
using System;
using System.Collections.Generic;

namespace _8BitEmu.Emulator {
    class OPCodeDictionary {
        private static Registers registers = new Registers();
        private static Stack stack = new Stack();
        private static SimpleHandler simpleHandler = new SimpleHandler(registers,stack);

        public static Dictionary<char, IHandler> OpCodeDic = new Dictionary<char, IHandler>() {
            {'0', new _0Handler(stack, CPU._sc) },{ '1', simpleHandler},
            { '2', simpleHandler},{ '3',simpleHandler}, 
            { '4', simpleHandler},{ '6',simpleHandler},
            { '7',simpleHandler}, { '8', new MathHandler(registers)},
            {'a',simpleHandler },{'c',  simpleHandler},
            { 'd', new DisplayHandler(CPU._sc,registers) },
            { 'e', new EHandler()}, { 'f', new FHandler(registers) }
        };

        public static void HandleOpCode(short opcode, char identifier) {
            if (!OpCodeDic.ContainsKey(identifier))
                throw new Exception($"Can't handle opcode: {opcode:x}");

            OpCodeDic[identifier].HandleOPCode(opcode);
        }
    }
}
