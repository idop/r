namespace B16_Ex02
{
    public class GameUtils
    {
        public const int v_FirstPlayerIndex = 0;
        public const int v_SecondPlayerIndex = 1;
        public const char k_ForfitChar = 'q';
        public const int k_playerWantsToPlayAnotherLevel = 1;
        public const int k_playerWantsToQuit = 0;
        public const int k_MaxEfficientDepth = 6;

        public enum eGameMode : byte
        {
            PlayerVsPlayer,
            PlayerVsAi
        }
    }
}