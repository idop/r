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
        private bool m_PlayerWantsToPlay = true;
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
            while(m_PlayerWantsToPlay)
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
            if (m_TurnNumber % 2 == 0 || m_GameMode == GameUtils.eGameMode.PlayerVsPlayer)
            {
                playHumanTurn();
            }
            else  
            {
                playComputerTurn();
            }

            endTurn();
        }

        private void playHumanTurn()
        {
            GameBoard.eBoardSquare playerSquare = m_TurnNumber % 2 == 0 ? GameBoard.eBoardSquare.Player1Square : GameBoard.eBoardSquare.Player2Square;
            m_PlayerWantsToPlay = m_UiManager.GetMoveFormUser(m_Players[m_TurnNumber % 2].Name, playerSquare, m_GameBoard);
        }

        private void playComputerTurn()
        {
            Ai ai = new Ai();
            int nextMove = ai.GetNextMove(m_GameBoard);
            m_GameBoard.TryToSetColumnSquare(nextMove, GameBoard.eBoardSquare.Player2Square);
        }

        private void endTurn()
        {
           ++m_TurnNumber;
        }

        private void playAnotherLevel()
        {
            m_TurnNumber = 0;
            m_GameBoard.ClearBoard();
            m_UiManager.RenderScreen(m_GameBoard);
        }
    }
}