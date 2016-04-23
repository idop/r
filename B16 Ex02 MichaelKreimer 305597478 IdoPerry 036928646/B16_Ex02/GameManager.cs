using System;

namespace B16_Ex02
{
    internal class GameManager
    {
        private const int k_NumberOfPlayers = 2;
        private GameBoard m_GameBoard;
        private UiManager m_UiManager = new UiManager();
        private GameUtils.eGameMode m_GameMode;
        private int m_TurnNumber= 0;
        private bool playerWantsToPlay = true;
        Player[] m_Players;
        public void Start()
        {
            init();
            m_UiManager.RenderScreen(m_GameBoard);
            playGame();
        }

        private void init()
        {
            initializeGameBoard();
            initializeGameMode();
            initializePlayers();
        }

        private void initializePlayers()
        {
            //TODO
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

        private void playGame()
        {
            while(playerWantsToPlay)
            {
                playerWantsToPlay = playTurn();
                m_UiManager.RenderScreen(m_GameBoard);
                checkBoardStatus();
            }
            startPlayerForfitAction();
        }

        private void startPlayerForfitAction()
        {
            //TODO
        }

        private void checkBoardStatus()
        {
            //TODO
        }

        private bool playTurn()
        {
            return true; //TODO
        }

        private void playAnotherLevel()
        {
            //TODO
        }

    }
}