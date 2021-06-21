using System;

namespace CosmosKernel1
{
    static class FPSMeter
    {
        public static int FPS = 0;

        public static int LastS = -1;
        public static int Ticken = 0;

        public static void Update()
        {
            if (LastS == -1)
            {
                LastS = DateTime.Now.Second;
            }
            if (DateTime.Now.Second - LastS != 0)
            {
                if (DateTime.Now.Second > LastS)
                {
                    FPS = Ticken / (DateTime.Now.Second - LastS);
                }
                LastS = DateTime.Now.Second;
                Ticken = 0;
            }
            Ticken++;
        }
    }
}
