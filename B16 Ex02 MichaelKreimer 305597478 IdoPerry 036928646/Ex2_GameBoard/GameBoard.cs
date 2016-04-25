using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    public class GameBoard
    {
        public const int k_MinDimensitonSize = 4;
        public const int k_MaxDimensitonSize = 8;
        private const char k_EmptySquare = ' ';
        private const char k_Player1Symbol = 'x';
        private const char k_Player2Symbol = 'o';

        public enum eBoardSquare : byte
        {
            EmptySquare = (byte)k_EmptySquare,
            Player1Square = (byte)k_Player1Symbol,
            Player2Square = (byte)k_Player2Symbol
        }

        public enum eBoardStatus : byte
        {
            BoardHasEmptySquares,
            BoardIsFull,
            PlayerWon
        }

        private eBoardStatus m_BoardStatus;
        private eBoardSquare[,] m_GameBoard;
        private int m_NumberOfEmptySquares;
        private int[] m_CurrentEmptyRowInColumn;

        public GameBoard(int i, int j)
        {
            m_GameBoard = new eBoardSquare[i, j];
            m_CurrentEmptyRowInColumn = new int[j];
            ClearBoard();
        }

        public void ClearBoard()
        {
            m_NumberOfEmptySquares = Rows * Columns;

            for (int i = 0; i < Columns; ++i)
            {
                m_CurrentEmptyRowInColumn[i] = Rows;
                for (int j = 0; j < Rows; ++j)
                {
                    m_GameBoard[j, i] = eBoardSquare.EmptySquare;
                }
            }
            m_BoardStatus = eBoardStatus.BoardHasEmptySquares;
        }

        public eBoardSquare this[int row, int column]
        {
            get
            {
                return m_GameBoard[row, column];
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

        public bool TryToSetColumnSquare(int i_ColumnIndex, eBoardSquare i_PlayerSquare)
        {
            bool isColumnFull = true;
            if (isValidColumn(i_ColumnIndex))
            {
                if (m_CurrentEmptyRowInColumn[i_ColumnIndex] != 0)
                {
                    m_GameBoard[m_CurrentEmptyRowInColumn[i_ColumnIndex] - 1, i_ColumnIndex] = i_PlayerSquare;
                    --m_NumberOfEmptySquares;
                    setNewBoardStatus(i_ColumnIndex);
                    --m_CurrentEmptyRowInColumn[i_ColumnIndex];
                    isColumnFull = false;

                }
            }

            return isColumnFull;
        }

        private void setNewBoardStatus(int i_LastUsedColumnIndex)
        {
            bool playerWon = checkIfPlayerWon(i_LastUsedColumnIndex);
            if (playerWon)
            {
                m_BoardStatus = eBoardStatus.PlayerWon;
            }
            else if (m_NumberOfEmptySquares == 0)
            {
                m_BoardStatus = eBoardStatus.BoardIsFull;
            }
        }

        private bool checkIfPlayerWon(int i_LastUsedColumnIndex)
        {
            bool playerWon = false;
            return playerWon;
        }

        public eBoardStatus GetBoardStatus()
        {
            return m_BoardStatus;
        }

        private bool isValidColumn(int i_ColumnIndex)
        {
            return i_ColumnIndex >= 0 && i_ColumnIndex < Columns;
        }
    }
}
