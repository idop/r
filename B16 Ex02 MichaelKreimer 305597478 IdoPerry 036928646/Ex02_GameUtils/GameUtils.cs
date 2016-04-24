using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    public class GameUtils
    {
        public const char k_Player1Symbol = 'x';
        public const char k_Player2Symbol = 'o';
        public const int v_HumanIndex = 0;
        public const int v_RobotIndex = 1;
        public const int v_FirstPlayerIndex = 0;
        public const int v_SecondPlayerIndex = 1;
        public enum eGameMode : byte
        {
            PlayerVsPlayer,
            PlayerVsAi
        }

    }
}
