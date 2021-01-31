using Cosmos.Core;
using System.Drawing;

namespace CosmosKernel1
{
    public class Graphics
    {
        public ManagedMemoryBlock memory;
        public readonly int width;
        public readonly int height;
        public readonly int depth = 4;

        public Graphics(int width, int height)
        {
            this.width = width;
            this.height = height;
            memory = new ManagedMemoryBlock((uint)(width * height * depth));
        }

        public void Clear(Color color)
        {
            memory.Fill((uint)color.ToArgb());
        }

        public void DrawPoint(Color color, int x, int y)
        {
            if ((uint)((width * y + x) * depth) < memory.Size)
            {
                memory.Write32((uint)((width * y + x) * depth), (uint)color.ToArgb());
            }
        }
    }
}
