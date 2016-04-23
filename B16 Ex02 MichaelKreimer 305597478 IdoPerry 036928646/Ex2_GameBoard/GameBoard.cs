using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    public class GameBoard
    {
        public const int k_MinDimensitonSize = 4;
        public const int k_MaxDimensitonSize = 8;
        private const char k_Space = ' ';
        private char[,] m_GameBoard;

        public GameBoard(int i, int j)
        {
            m_GameBoard = new char[i, j];
            ClearBoard();
        }

        public void ClearBoard()
        {
            for(int i = 0; i < m_GameBoard.GetLength(0); ++i)
            {
                for(int j = 0; j < m_GameBoard.GetLength(1); ++j)
                {
                    m_GameBoard[i, j] = k_Space;
                }
            } 
        }

        public char this[int row, int column]
        {
            get
            {
                return m_GameBoard[row, column];
            }

            set
            {
                m_GameBoard[row, column] = value;
            }
        }
         
        public int Rows
        {
            get
            {
                return m_GameBoard.GetLength(0);
            }
        }

        public int Columns
        {
            get
            {
                return m_GameBoard.GetLength(1);
            }
        }
    }
}
