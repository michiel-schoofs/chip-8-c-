using _8BitEmu.Emulator;
using _8BitEmu.Emulator.Handlers;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8BitEmu {
    public partial class Form1 : Form {
        private Emulator.Screen sc;
        private Bitmap screen;
        private CPU cpu;

        private char[] keys = new char[] { '0','1','2','3','4',
        '5','6','7','8','9','/','*','-','+','\r','.'};

        private Keys[] keys2 = new Keys[] { Keys.NumPad0, Keys.NumPad1,
        Keys.NumPad2, Keys.NumPad3, Keys.NumPad4, Keys.NumPad5,
        Keys.NumPad6, Keys.NumPad7, Keys.NumPad8, Keys.NumPad9,
        Keys.Divide,Keys.Multiply, Keys.Subtract, Keys.Add, 
        Keys.Enter, Keys.Decimal};


        public Form1() {
            InitializeComponent();
             sc = new Emulator.Screen(DrawScreen);
            cpu = new CPU(sc);
            screen = new Bitmap(64, 32);
        }

        private void RunGame() {
            while (true) {
                cpu.Tick();
            }
        }

        private void DrawScreen() { 
            var bits = screen.LockBits(new Rectangle(0, 0, screen.Width, screen.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            unsafe {
                byte* pointer = (byte*)bits.Scan0;

                for (var y = 0; y < screen.Height; y++) {
                    for (var x = 0; x < screen.Width; x++) {
                        if (sc.screen[y, x]) { 
                            pointer[0] = 220;
                            pointer[1] = 209;
                            pointer[2] = 255;
                        } else {
                            pointer[0] = 64;
                            pointer[1] = 64;
                            pointer[2] = 64;
                        }

                        pointer[3] = 255;
                        pointer += 4; 
                    }
                }
            }

            screen.UnlockBits(bits);
            pictureBoxWithInterpolationMode1.Image = screen;
            Thread.Sleep(9);
        }

        private void Form1_Load(object sender, EventArgs e) {
            Task.Run(RunGame);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) {

            int index = Array.IndexOf(keys2, e.KeyCode);

            byte indx = 0x0;
            if (index != -1)
                indx = Convert.ToByte(index);

            FHandler.key = indx;
            EHandler.PressedKey = indx;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) {
            EHandler.PressedKey = 0x0;
            FHandler.key = 0x0;
        }
    }
}
