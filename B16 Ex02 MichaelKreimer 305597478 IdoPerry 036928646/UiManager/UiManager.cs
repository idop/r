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
            string getDimenstionMessage = "Please enter the number of {2} you want to pay with (between {0} and {1})";
            Console.WriteLine(weclomeMessage);
            Console.WriteLine(getDimenstionMessage, GameBoard.k_MinDimensitonSize, GameBoard.k_MaxDimensitonSize, k_Rows);
            o_Rows = getBoardDimenstionFromUser();
            Console.WriteLine(getDimenstionMessage, GameBoard.k_MinDimensitonSize, GameBoard.k_MaxDimensitonSize, k_Columns);
            o_Columns = getBoardDimenstionFromUser();
            setBoardHeaders(o_Columns);
            setSeperatorLine(o_Columns);
        }

        public void GetGameMode(ref GameUtils.eGameMode io_GameMode)
        {
            string requestMessage = "Please Choose the game mod. enter {0} for Player vs Player or {1} for Player vs Computer";
            string invlaidInputMessage = "invalid input please enter a nunmber between {0} and {1}";
            bool invalidInput = true;
            Console.WriteLine(requestMessage, (byte)GameUtils.eGameMode.PlayerVsPlayer, (byte)GameUtils.eGameMode.PlayerVsAi);
            GetGameModeInput(ref io_GameMode, invlaidInputMessage, ref invalidInput);
        }

        private static void GetGameModeInput(ref GameUtils.eGameMode io_GameMode, string invlaidInputMessage, ref bool invalidInput)
        {
            byte input;
            while (invalidInput)
            {
                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 0:
                            io_GameMode = GameUtils.eGameMode.PlayerVsPlayer;
                            invalidInput = false;
                            break;
                        case 1:
                            io_GameMode = GameUtils.eGameMode.PlayerVsAi;
                            invalidInput = false;
                            break;
                        default:
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine(invlaidInputMessage, (byte)GameUtils.eGameMode.PlayerVsPlayer, (byte)GameUtils.eGameMode.PlayerVsAi);
                }
            }
        }

        private int getBoardDimenstionFromUser()
        {
            string invlaidInputMessage = "invalid input please enter a nunmber between {0} and {1} as your selection";
            int result = 0;
            bool invalidInput = true;
            while (invalidInput)
            {
                if (int.TryParse(Console.ReadLine(), out result))
                {
                    if (result >= GameBoard.k_MinDimensitonSize && result <= GameBoard.k_MaxDimensitonSize)
                    {
                        invalidInput = false;
                    }
                    else
                    {
                        Console.WriteLine(invlaidInputMessage, GameBoard.k_MinDimensitonSize, GameBoard.k_MaxDimensitonSize);
                    }
                }
            }

            return result;
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

        private void setBoardHeaders(int i_Columns)
        {
            StringBuilder boardHeader = new StringBuilder((i_Columns * 2) + 1);
            for (int i = 0; i < i_Columns; ++i)
            {
                boardHeader.AppendFormat(" {0}", i);
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
                    currentBoardLine.AppendFormat("{0}|", i_GameBoard[i, j]);
                }

                Console.WriteLine(currentBoardLine);
                Console.WriteLine(m_SeperatorLine);
                currentBoardLine.Remove(0, currentBoardLine.Length);
            }
        }
    }
}
