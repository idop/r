using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    public class GameUtils
    {
        public const int v_FirstPlayerIndex = 0;
        public const int v_SecondPlayerIndex = 1;
        public const char k_ExitChar = 'q';

        public enum eGameMode : byte
        {
            PlayerVsPlayer,
            PlayerVsAi
        }
    }
}
