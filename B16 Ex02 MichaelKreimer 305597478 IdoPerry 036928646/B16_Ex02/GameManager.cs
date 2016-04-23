using System;

namespace B16_Ex02
{
    internal class GameManager
    {
        private GameBoard m_GameBoard;
        private UiManager m_UiManager = new UiManager();
        private GameUtils.eGameMode m_GameMode;
        private int m_LevelNumber = 0;
        public void Start()
        {
            init();
            m_UiManager.RenderScreen(m_GameBoard);
        }

        private void init()
        {
            initializeGameBoard();
            initializeGameMode();
        }
        private void initializeGameBoard()
        {
            int rows, columns;
            m_UiManager.GetBoardDimensions(out rows, out columns);
            m_GameBoard = new GameBoard(rows, columns);
        }
        private void initializeGameMode()
        {
            m_UiManager.GetGameMode(ref m_GameMode);
        }

    }
}