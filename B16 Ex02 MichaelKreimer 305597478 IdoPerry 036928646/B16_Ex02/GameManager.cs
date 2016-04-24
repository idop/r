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
        private List<Player> m_Players = new List<Player>();

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
            int numOfHumanPlayers=0, numOfComputerizedPlayers=0;
            if (m_GameMode.Equals(GameUtils.eGameMode.PlayerVsPlayer))
            {
                numOfHumanPlayers = 2;
            }
            else if (m_GameMode.Equals(GameUtils.eGameMode.PlayerVsAi))
            {
                numOfComputerizedPlayers = numOfHumanPlayers = 1;
            }
            createGamePlayers(numOfHumanPlayers, numOfComputerizedPlayers);
        }

        private void createGamePlayers(int i_numOfHumanPlayers, int i_numOfComputerizedPlayers)
        {
            int index = 1;
            createGamePlayersByType(i_numOfHumanPlayers, GameUtils.v_Human, index);
            index = i_numOfHumanPlayers + 1;
            createGamePlayersByType(i_numOfComputerizedPlayers, GameUtils.v_Robot, index);
        }
        private void createGamePlayersByType(int i_numOfPlayers,bool i_isHuman, int i_Index)
        {
            while (i_numOfPlayers > 0)
            {
                addGamePlayer(i_isHuman, i_Index);
                i_Index++;
                i_numOfPlayers--;
            }
        }

        private void addGamePlayer(bool v_Human, int i_index)
        {
            m_Players.Add(new Player(v_Human, i_index));
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
            Player currentPlayer = m_Players[m_TurnNumber];
            if (currentPlayer.IsHuman == true)
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
            userWantsToPlay = m_UiManager.AskIfUserWantsToPlay();
            if (userWantsToPlay)
            {
                UiManager.g
            }
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