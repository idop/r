using System;

namespace B16_Ex02
{
    internal class GameManager
    {
        private GameBoard m_GameBoard;
        private UiManager m_UiManager = new UiManager();

        public void Start()
        {
            initializeGameBoard();
            m_UiManager.RenderScrean(m_GameBoard);
        }

        private void initializeGameBoard()
        {
            int rows, columns;
            m_UiManager.startGameBoardInitialization(out rows, out columns);
            m_GameBoard = new GameBoard(rows, columns);
        }
    }
}