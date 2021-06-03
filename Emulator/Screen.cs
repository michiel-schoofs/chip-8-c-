using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8BitEmu.Emulator {
    public class Screen {
        private Action _draw;

        public Screen(Action drawScreen) {
            _draw = drawScreen;
        }

        public bool[,] screen = new bool[32,64];
        public bool pixelErased = false;

        public void EmptyScreen() {
            screen = new bool[32, 64];
            _draw();
        }

        public void DisplayBytesOnTheScreen(byte[] bytes, int x_start, int y_start) {
            pixelErased = false;
            foreach(byte display in bytes){
                for (int i = 0; i < 8; i++) { 
                    bool bit = (display & (1 << i )) != 0;
                    var original = screen[y_start%32, (x_start + i)%64];

                    if (screen[y_start%32, (x_start + i) % 64] == bit) {
                        screen[y_start%32, (x_start + i) % 64] = false;
                    } else {
                        screen[y_start%32, (x_start + i)% 64] = true;
                    }

                    var newval = screen[y_start, (x_start + i) % 64];
                    if (original && !newval) {
                        pixelErased = true;
                    }
                }
            }
            _draw();
        }
    }
}
