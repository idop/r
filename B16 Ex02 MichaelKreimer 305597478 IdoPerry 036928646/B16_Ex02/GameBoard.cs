using System;
using System.Collections.Generic;
using System.Text;

namespace Ex02_UiManager
{
    class GameBoard
    {
        private char[,] m_GameBoard;

        public GameBoard (int i, int j)
        {
            m_GameBoard = new char[i, j];
        }
        public char this[int row , int column]
        {
            get
            {
                return m_GameBoard[row,column];
            }
            set
            {
                m_GameBoard[row, column] = value;
            }
        }
             

    }
}
