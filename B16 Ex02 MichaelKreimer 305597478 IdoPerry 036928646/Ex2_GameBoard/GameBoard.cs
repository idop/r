using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    public class GameBoard : ICloneable
    {
        public const int k_MinDimensitonSize = 4;
        public const int k_MaxDimensitonSize = 8;
        private const int k_NumberOfSquaresInARowNeededForVictory = 4;
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
            NextPlayerCanPlay,
            Draw,
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

        public GameBoard(int[] i_CurrentEmptyRowInColumn, eBoardSquare[,] i_GameBoardSquare, int i_NumberOfEmptySquares, eBoardStatus i_BoardStatus)
        {
            m_CurrentEmptyRowInColumn = i_CurrentEmptyRowInColumn;
            m_GameBoard = i_GameBoardSquare;
            m_NumberOfEmptySquares = i_NumberOfEmptySquares;
            m_BoardStatus = i_BoardStatus;
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
            m_BoardStatus = eBoardStatus.NextPlayerCanPlay;
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
                if (m_CurrentEmptyRowInColumn[i_ColumnIndex] > 0)
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
        public bool TryToSetColumnSquare2(int i_ColumnIndex, eBoardSquare i_PlayerSquare)
        {
            //For testing
            bool isColumnFull = true;
            if (isValidColumn(i_ColumnIndex))
            {
                if (m_CurrentEmptyRowInColumn[i_ColumnIndex] > 0)
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
        private void setNewBoardStatus(int i_LastInsertedColumn)
        {
            bool playerWon = checkIfPlayerWon(i_LastInsertedColumn);
            if (playerWon)
            {
                m_BoardStatus = eBoardStatus.PlayerWon;
            }
            else if (m_NumberOfEmptySquares == 0)
            {
                m_BoardStatus = eBoardStatus.Draw;
            }
        }

        private bool checkIfPlayerWon(int i_LastInsertedColumn)
        {
            int lastInstertedRow = m_CurrentEmptyRowInColumn[i_LastInsertedColumn]-1;
            eBoardSquare currentPlayerSquare = m_GameBoard[lastInstertedRow, i_LastInsertedColumn];
            bool playerWon = checkCurrentColumn(lastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
            if (!playerWon)
            {
                playerWon = checkCurrentRow(lastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
                if (!playerWon)
                {
                    playerWon = checkCurrentDiagonals(lastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
                }
            }

            return playerWon;
        }

        private bool checkCurrentColumn(int i_LastInstertedRow, int i_LastInsertedColumn, eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            int maxNumberofSquaresInARow = 1;
            for (int i = i_LastInstertedRow + 1; i < Rows && !playerWon; ++i)
            {
                if (m_GameBoard[i, i_LastInsertedColumn] == currentPlayerSquare)
                {
                    ++maxNumberofSquaresInARow;
                }
                else
                {
                    break;
                }

                if (maxNumberofSquaresInARow == k_NumberOfSquaresInARowNeededForVictory)
                {
                    playerWon = true;
                }
            }

            return playerWon;
        }

        private bool checkCurrentRow(int i_LastInstertedRow, int i_LastInsertedColumn, eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            int maxNumberofSquaresInARow = 0;
            for (int i = 0; i < Columns && !playerWon; ++i)
            {
                if (m_GameBoard[i_LastInstertedRow, i] == currentPlayerSquare)
                {
                    ++maxNumberofSquaresInARow;
                }
                else
                {
                    maxNumberofSquaresInARow = 0;
                }

                if (maxNumberofSquaresInARow == k_NumberOfSquaresInARowNeededForVictory)
                {
                    playerWon = true;
                }
            }

            return playerWon;
        }


        private bool checkCurrentDiagonals(int i_LastInstertedRow, int i_LastInsertedColumn, eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            return playerWon;
        }

        public eBoardStatus BoardStatus
        {
            get
            {
                return m_BoardStatus;
            }
        }

        private bool isValidColumn(int i_ColumnIndex)
        {
            return i_ColumnIndex >= 0 && i_ColumnIndex < Columns;
        }

        public object Clone()
        {
            int[] currentEmptyRowInColumn = (int[])m_CurrentEmptyRowInColumn.Clone();
            eBoardSquare[,] gameBoardToCopy = (eBoardSquare[,])m_GameBoard.Clone();
            int numberOfEmptySquares = m_NumberOfEmptySquares;
            eBoardStatus boardStatus = m_BoardStatus;
            return new GameBoard(currentEmptyRowInColumn, gameBoardToCopy, numberOfEmptySquares, boardStatus);
        }
    }
}
