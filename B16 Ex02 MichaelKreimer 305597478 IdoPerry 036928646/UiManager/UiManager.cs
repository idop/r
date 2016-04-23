using System;
using System.Text;

namespace B16_Ex02
{
    public class UiManager
    {
        private const int k_MinDimensitonSize = 4;
        private const int k_MaxDimensitonSize = 8;
        private const string k_Rows = "Rows";
        private const string k_Columns = "Columns";
        private string m_BoardHeader;
        private string m_SeperatorLine;

        public void startGameBoardInitialization(out int o_Rows, out int o_Columns)
        {
            string weclomeMessage = "Welcome to the 4 in a Row game.";
            string getDimenstionMessage = "Please enter the number of {2} you want to pay with (between {0} and {1})";
            Console.WriteLine(weclomeMessage);
            Console.WriteLine(getDimenstionMessage, k_MinDimensitonSize, k_MaxDimensitonSize, k_Rows);
            o_Rows = getBoardDimenstionFromUser();
            Console.WriteLine(getDimenstionMessage, k_MinDimensitonSize, k_MaxDimensitonSize, k_Columns);
            o_Columns = getBoardDimenstionFromUser();
            setBoardHeaders(o_Columns);
            setSeperatorLine(o_Columns);
        }
   
        private int getBoardDimenstionFromUser()
        {
            string invlaidInputMessage = "invalid input please enter a nunmber between 4 and 8 as your selection";
            int result = 0;
            bool invalidInput = true;
            while (invalidInput)
            {
                if (int.TryParse(Console.ReadLine(), out result) && result >= k_MinDimensitonSize && result <= k_MaxDimensitonSize)
                {
                    invalidInput = false;
                }
                else
                {
                    Console.WriteLine(invlaidInputMessage);
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

        public void RenderScrean(GameBoard i_GameBoard)
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
