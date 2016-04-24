using System;
using System.Collections.Generic;

namespace B16_Ex02
{
    internal class GameManager
    {
        private const int k_NumberOfPlayers = 2;
        private GameBoard m_GameBoard;
        private UiManager m_UiManager = new UiManager();
        private GameUtils.eGameMode m_GameMode;
        private int m_TurnNumber = 0;
        private bool playerWantsToPlay = true;
        private Player[] m_Players = new Player[2];

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
        private void initializePlayers()
        {
            m_Players[GameUtils.v_FirstPlayerIndex] = new Player(GameUtils.v_FirstPlayerIndex);
            m_Players[GameUtils.v_SecondPlayerIndex] = new Player(GameUtils.v_SecondPlayerIndex);
        }
        private void playGame()
        {
            while(playerWantsToPlay)
            {
                playTurn();
                m_UiManager.RenderScreen(m_GameBoard);
                checkBoardStatus();
            }

            startPlayerForfeitAction();
        }

        private void startPlayerForfeitAction()
        {
            //TODO
        }

        private void checkBoardStatus()
        {
            //TODO
        }

        private void playTurn()
        {
            if (m_TurnNumber == GameUtils.v_HumanIndex)
            {
                playHumanTurn();
            }
            else // computer player 
            {
                playComputerTurn();
            }
            switchTurn();
        }

        private void playHumanTurn()
        {
            //userWantsToPlay = m_UiManager.AskIfUserWantsToPlay();
            //if (userWantsToPlay)
            //{
            //}
        }

        private void playComputerTurn()
        {
            throw new NotImplementedException();
        }

        private void switchTurn()
        {
            m_TurnNumber = (m_TurnNumber + 1) % k_NumberOfPlayers;
        }

        private void playAnotherLevel()
        {
            //TODO
        }

    }
}