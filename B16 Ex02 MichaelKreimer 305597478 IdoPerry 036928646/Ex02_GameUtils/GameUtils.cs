using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    public class GameUtils
    {
        public const char k_Player1Symbol = 'x';
        public const char k_Player2Symbol = 'o';
        public enum eGameMode : byte
        {
            PlayerVsPlayer,
            PlayerVsAi
        }

    }
}
