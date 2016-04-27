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
        private bool m_CurrentGameEnded = false;
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
            while (m_PlayerWantsToPlay)
            {
                while (!m_CurrentGameEnded)
                {
                    playTurn();
                    m_UiManager.RenderScreen(m_GameBoard);
                    checkBoardStatus();
                    ++m_TurnNumber;
                }
                checkIfPlayerWantsToPlayAnotherGame();
            }

          
        }

        private void checkBoardStatus()
        {
            if (m_PlayerWantsToPlay)
            {
                GameBoard.eBoardStatus boardStatus = m_GameBoard.BoardStatus;
                if (boardStatus != GameBoard.eBoardStatus.NextPlayerCanPlay)
                {
                    if (boardStatus == GameBoard.eBoardStatus.PlayerWon)
                    {
                        m_Players[m_TurnNumber % 2].Score++;
                        m_UiManager.DeclareWinner(m_Players[m_TurnNumber % 2].Name);
                        m_UiManager.PresentCurrentScore(m_Players[0].Name, m_Players[0].Score, m_Players[1].Name, m_Players[1].Score);
                        
                    }
                    else if(boardStatus == GameBoard.eBoardStatus.Draw)
                    {
                        m_UiManager.DeclareDraw();
                        m_UiManager.PresentCurrentScore(m_Players[0].Name, m_Players[0].Score, m_Players[1].Name, m_Players[1].Score);
                    }

                    m_CurrentGameEnded = true;
                }
            }
            else
            {
                m_Players[( m_TurnNumber + 1) % 2].Score++;
                m_UiManager.DeclareForfit(m_Players[m_TurnNumber % 2].Name);
                m_UiManager.PresentCurrentScore(m_Players[0].Name, m_Players[0].Score, m_Players[1].Name, m_Players[1].Score);
                m_CurrentGameEnded = true;
            }
        }

        private void checkIfPlayerWantsToPlayAnotherGame()
        {

            m_PlayerWantsToPlay = m_UiManager.CheckIfPplayerWantsToPlayAnotherGame();
            if (m_PlayerWantsToPlay)
            {
                playAnotherLevel();
            }
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

        }

        private void playHumanTurn()
        {
            GameBoard.eBoardSquare playerSquare = m_TurnNumber % 2 == 0 ? GameBoard.eBoardSquare.Player1Square : GameBoard.eBoardSquare.Player2Square;
            m_PlayerWantsToPlay = m_UiManager.GetMoveFormUser(m_Players[m_TurnNumber % 2].Name, playerSquare, m_GameBoard);
        }

        private void playComputerTurn()
        {
            throw new NotImplementedException();
        }

        private void playAnotherLevel()
        {
            m_TurnNumber = 0;
            m_CurrentGameEnded = false;
            m_GameBoard.ClearBoard();
            m_UiManager.RenderScreen(m_GameBoard);
        }
    }
}