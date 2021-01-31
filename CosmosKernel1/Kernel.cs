using Cosmos.Core;
using Cosmos.HAL.Drivers.PCI.Video;
using Cosmos.System;
using System.Drawing;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        public static readonly int screenWidth = 640;
        public static readonly int screenHeight = 480;

        public static VMWareSVGAII videoDriver = new VMWareSVGAII();
        public static Graphics mainGraphics = new Graphics(screenWidth, screenHeight);

        static int[] cursor = new int[]
        {
                1,0,0,0,0,0,0,0,0,0,0,0,
                1,1,0,0,0,0,0,0,0,0,0,0,
                1,2,1,0,0,0,0,0,0,0,0,0,
                1,2,2,1,0,0,0,0,0,0,0,0,
                1,2,2,2,1,0,0,0,0,0,0,0,
                1,2,2,2,2,1,0,0,0,0,0,0,
                1,2,2,2,2,2,1,0,0,0,0,0,
                1,2,2,2,2,2,2,1,0,0,0,0,
                1,2,2,2,2,2,2,2,1,0,0,0,
                1,2,2,2,2,2,2,2,2,1,0,0,
                1,2,2,2,2,2,2,2,2,2,1,0,
                1,2,2,2,2,2,2,2,2,2,2,1,
                1,2,2,2,2,2,2,1,1,1,1,1,
                1,2,2,2,1,2,2,1,0,0,0,0,
                1,2,2,1,0,1,2,2,1,0,0,0,
                1,2,1,0,0,1,2,2,1,0,0,0,
                1,1,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,1,2,2,1,0,0,
                0,0,0,0,0,0,0,1,1,0,0,0
        };

        public void DrawCursor(Graphics graphics, int x, int y)
        {
            for (int h = 0; h < 19; h++)
            {
                for (int w = 0; w < 12; w++)
                {
                    if (cursor[h * 12 + w] == 1)
                    {
                        graphics.DrawPoint(Color.Black, w + x, h + y);
                    }
                    if (cursor[h * 12 + w] == 2)
                    {
                        graphics.DrawPoint(Color.White, w + x, h + y);
                    }
                }
            }
        }

        protected override void BeforeRun()
        {
            videoDriver.SetMode((uint)screenWidth, (uint)screenHeight);
            MouseManager.ScreenWidth = (uint)screenWidth;
            MouseManager.ScreenHeight = (uint)screenHeight;
        }

        protected override void Run()
        {
            FPSMeter.Update();

            mainGraphics.Clear(Color.Black);
            mainGraphics.DrawString(Color.White, $"你好\nHola a todos\nHello there\nOlá\nПривет\nこんにちは\nHallo\nBonjour\n{FPSMeter.FPS} FPS\nInstalled RAM {CPU.GetAmountOfRAM()}MB\nUsed RAM {CPU.GetEndOfKernel() / 1048576}MB", 10, 10);

            DrawCursor(mainGraphics, (int)MouseManager.X, (int)MouseManager.Y);

            videoDriver.Video_Memory.Copy(mainGraphics.memory);

            videoDriver.Update(0, 0, (uint)screenWidth, (uint)screenHeight);
        }
    }
}
