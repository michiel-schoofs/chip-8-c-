using System;
using System.IO;

namespace _8BitEmu.Emulator {
    public class ROM {
        private byte[] memory = new byte[4096];
        
        public void LoadInApplication(string path) {
            FileStream fs = File.Open(path,FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] bytes = br.ReadBytes((int)fs.Length);
            Array.Copy(bytes, 0, memory, 512, bytes.Length);
        }

        public short ReadDataAt(short index) {
            return BitConverter.ToInt16(new byte[] { memory[(int)index+1], memory[((int)index)]}, 0);
        }

        public byte[] ReadNBytesAt(short index, int n){
            byte[] result = new byte[n];
            for (int i = 0; i < n; i++) {
                result[i] = memory[index];
                index++;
            }
            return result;
        }
    }
}
