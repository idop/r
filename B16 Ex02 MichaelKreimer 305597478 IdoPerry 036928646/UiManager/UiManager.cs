using System;
using System.Text;

namespace B16_Ex02
{
    public class UiManager
    {
        private const string k_Rows = "Rows";
        private const string k_Columns = "Columns";
        private string m_BoardHeader;
        private string m_SeperatorLine;

        public void GetBoardDimensions(out int o_Rows, out int o_Columns)
        {
            string weclomeMessage = "Welcome to the 4 in a Row game.";
            string getDimenstionMessage = "Please enter the number of {2} you want to play with (between {0} and {1})";
            Console.WriteLine(weclomeMessage);
            Console.WriteLine(getDimenstionMessage, GameBoard.k_MinDimensitonSize, GameBoard.k_MaxDimensitonSize, k_Rows);
            o_Rows = getIntegerFromUser(GameBoard.k_MinDimensitonSize, GameBoard.k_MaxDimensitonSize);
            Console.WriteLine(getDimenstionMessage, GameBoard.k_MinDimensitonSize, GameBoard.k_MaxDimensitonSize, k_Columns);
            o_Columns = getIntegerFromUser(GameBoard.k_MinDimensitonSize, GameBoard.k_MaxDimensitonSize);
            setBoardHeaders(o_Columns);
            setSeperatorLine(o_Columns);
        }

        public void GetGameMode(ref GameUtils.eGameMode io_GameMode)
        {
            string requestMessage = "Please Choose the game mod. enter {0} for Player vs Player or {1} for Player vs Computer";
            Console.WriteLine(requestMessage, (byte)GameUtils.eGameMode.PlayerVsPlayer, (byte)GameUtils.eGameMode.PlayerVsAi);
            io_GameMode = GetGameModeInput();
        }

        private GameUtils.eGameMode GetGameModeInput()
        {
            int input = getIntegerFromUser((byte)GameUtils.eGameMode.PlayerVsPlayer, (byte)GameUtils.eGameMode.PlayerVsAi);
            GameUtils.eGameMode gameMode;

            if (input == 0)
            {
                gameMode = GameUtils.eGameMode.PlayerVsPlayer;
            }
            else
            {
                gameMode = GameUtils.eGameMode.PlayerVsAi;
            }

            return gameMode;
        }

        public void PresentCurrentScore(string i_Player1Name, int i_Player1Score , string i_Player2Name, int i_Player2Score)
        {
            string message = string.Format("{0} Score is {1}, {2} Score is {3}", i_Player1Name, i_Player1Score, i_Player2Name, i_Player2Score);
            Console.WriteLine(message);
        }

        private void setSeperatorLine(int i_Columns)
        {
            StringBuilder seperatorLine = new StringBuilder((i_Columns * 2) + 2);
            for (int i = 0; i < i_Columns; ++i)
            {
                seperatorLine.Append("==");
            }

            seperatorLine.Append("=");
            m_SeperatorLine = seperatorLine.ToString();
        }

        public void DeclareWinner(string i_WinnerName)
        {
            string message = string.Format("{0} Won!!!!", i_WinnerName);
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(message);
        }

        public void DeclareForfit(string i_PlayerName)
        {
            string message = string.Format("{0} forfits the game.", i_PlayerName);
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(message);
        }

        public void DeclareDraw()
        {
            string message = "Game ended in a draw.";
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(message);
        }

        public bool CheckIfPplayerWantsToPlayAnotherGame()
        {
            string message = "Press 1 to play another game and 0 to exit";
            int input; 
            Console.WriteLine(message);
            input = getIntegerFromUser(GameUtils.k_playerWantsToQuit,GameUtils.k_playerWantsToPlayAnotherLevel); ;
            return input == GameUtils.k_playerWantsToPlayAnotherLevel;
        }

        private void setBoardHeaders(int i_Columns)
        {
            StringBuilder boardHeader = new StringBuilder((i_Columns * 2) + 1);
            for (int i = 0; i < i_Columns; ++i)
            {
                boardHeader.AppendFormat(" {0}", i + 1);
            }

            m_BoardHeader = boardHeader.ToString();
        }

        public void RenderScreen(GameBoard i_GameBoard)
        {
            int rows = i_GameBoard.Rows;
            int columns = i_GameBoard.Columns;
            StringBuilder currentBoardLine = new StringBuilder((columns * 2) + 2);
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine(m_BoardHeader);
            for (int i = 0; i < rows; ++i)
            {
                currentBoardLine.Append("|");
                for (int j = 0; j < columns; ++j)
                {
                    currentBoardLine.AppendFormat("{0}|", (char)i_GameBoard[i, j]);
                }

                Console.WriteLine(currentBoardLine);
                Console.WriteLine(m_SeperatorLine);
                currentBoardLine.Remove(0, currentBoardLine.Length);
            }
        }

        private int getIntegerFromUser(int i_MinValue, int i_MaxValue)
        {
            string invalidInputMessage = "invalid input please enter a number between {0} and {1} as your selection";
            int inputNumber = 0;
            bool invalidInput = true;

            while (invalidInput)
            {
                if (int.TryParse(Console.ReadLine(), out inputNumber) && inputNumber >= i_MinValue && inputNumber <= i_MaxValue)
                {

                    invalidInput = false;
                }
                else
                {
                    Console.WriteLine(invalidInputMessage, i_MinValue, i_MaxValue);
                }
            }

            return inputNumber;
        }

        public bool GetMoveFormUser(string i_PlayerName, GameBoard.eBoardSquare i_playerSquare, GameBoard i_GameBoard) //TODO Refactor this and fix logic
        {
            string message = "Hi {0}, it's your turn. Please Choose a Valid Column or q to forfit";
            string invlaidInputMessage = "Invalid Input Please Choose a Valid Column or q to forfit";
            char inputChar;
            int columnChoise;
            bool playerWantsToPlay = true;
            bool invalidInput = true;
            Console.WriteLine(message, i_PlayerName);
            while (invalidInput)
            {
                if (char.TryParse(Console.ReadLine(), out inputChar))
                {
                    if (inputChar == GameUtils.k_ForfitChar)
                    {
                        playerWantsToPlay = false;
                        invalidInput = false;
                    }
                    else if (char.IsDigit(inputChar))
                    {
                        columnChoise = (int)char.GetNumericValue(inputChar);
                        invalidInput = i_GameBoard.TryToSetColumnSquare(columnChoise - 1, i_playerSquare);
                    }
                }

                if (invalidInput)
                {
                    Console.WriteLine(invlaidInputMessage);
                }
            }

            return playerWantsToPlay;
        }
    }
}