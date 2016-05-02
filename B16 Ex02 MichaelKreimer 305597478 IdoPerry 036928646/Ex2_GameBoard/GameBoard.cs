using System;

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
        private const int k_SerieSize = 4;

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

        public int GetFirstAvailableColumn()
        {
            int firstAvailableColumn = -1;
            for (int i = 0; i < Columns; i++)
            {
                if (this.m_CurrentEmptyRowInColumn[i] > 0)
                {
                    firstAvailableColumn = i;
                    break;
                }
            }

            return firstAvailableColumn;
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

        public int[] EmptyRowsInColumn
        {
            get
            {
                return m_CurrentEmptyRowInColumn;
            }
        }

        public eBoardSquare[,] Board
        {
            get
            {
                return m_GameBoard;
            }
        }

        public GameBoard(int[] i_CurrentEmptyRowInColumn, eBoardSquare[,] i_GameBoardSquare, int i_NumberOfEmptySquares, eBoardStatus i_BoardStatus)
        {
            m_CurrentEmptyRowInColumn = i_CurrentEmptyRowInColumn;
            m_GameBoard = i_GameBoardSquare;
            m_NumberOfEmptySquares = i_NumberOfEmptySquares;
            m_BoardStatus = i_BoardStatus;
        }

        public eBoardStatus BoardStatus
        {
            get
            {
                return m_BoardStatus;
            }
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
            int lastInstertedRow = m_CurrentEmptyRowInColumn[i_LastInsertedColumn] - 1;
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
            bool playerWon = checkLeftDiagonal(i_LastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
            if (!playerWon)
            {
                playerWon = checkRightDiagonal(i_LastInstertedRow, i_LastInsertedColumn, currentPlayerSquare);
            }

            return playerWon;
        }

        private bool checkRightDiagonal(int i_LastInstertedRow, int i_LastInsertedColumn, eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            int maxNumberofSquaresInARow = 0;
            int diagonalRowIndex = getCurrentDiagoanlUpperRightRow(i_LastInstertedRow, i_LastInsertedColumn);
            int diagonalColumnIndex = getCurrentDiagoanlUpperRightolumn(i_LastInstertedRow, i_LastInsertedColumn);

            while (diagonalRowIndex > 0 && diagonalColumnIndex < Columns && !playerWon)
            {
                if (m_GameBoard[diagonalRowIndex, diagonalColumnIndex] == currentPlayerSquare)
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
                ++diagonalColumnIndex;
                --diagonalRowIndex;
            }

            return playerWon;
        }

        private int getCurrentDiagoanlUpperRightolumn(int i_LastInstertedRow, int i_LastInsertedColumn)
        {
            return Math.Max(i_LastInsertedColumn - (Rows - i_LastInstertedRow - 1), 0);
        }

        private int getCurrentDiagoanlUpperRightRow(int i_LastInstertedRow, int i_LastInsertedColumn)
        {
            return Math.Min(i_LastInstertedRow + i_LastInsertedColumn, Columns - 1);
        }

        private bool checkLeftDiagonal(int i_LastInstertedRow, int i_LastInsertedColumn, eBoardSquare currentPlayerSquare)
        {
            bool playerWon = false;
            int maxNumberofSquaresInARow = 0;
            int diagonalRowIndex = getCurrentDiagoanlUpperLeftRow(i_LastInstertedRow, i_LastInsertedColumn);
            int diagonalColumnIndex = getCurrentDiagoanlUpperLeftColumn(i_LastInstertedRow, i_LastInsertedColumn);

            while (diagonalRowIndex < Rows && diagonalColumnIndex < Columns && !playerWon)
            {
                if (m_GameBoard[diagonalRowIndex, diagonalColumnIndex] == currentPlayerSquare)
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
                ++diagonalColumnIndex;
                ++diagonalRowIndex;
            }

            return playerWon;
        }

        private int getCurrentDiagoanlUpperLeftColumn(int i_LastInstertedRow, int i_LastInsertedColumn)
        {
            return getCurrentDiagoanlUpperLeftRow(i_LastInsertedColumn, i_LastInstertedRow);
        }

        private int getCurrentDiagoanlUpperLeftRow(int i_LastInstertedRow, int i_LastInsertedColumn)
        {
            return Math.Max(i_LastInstertedRow - i_LastInsertedColumn, 0);
        }

        private bool isValidColumn(int i_ColumnIndex)
        {
            return i_ColumnIndex >= 0 && i_ColumnIndex < Columns;
        }

        public bool checkRowSeriePotential(int i_RowToCheck, int i_ColumnToCheck)
        {
            int potentialSerieCount = 0, potentialUnderCount = 0, potentialOverCount = 0;
            calcRowSerieCount(i_RowToCheck, i_ColumnToCheck, ref potentialOverCount, true);
            calcRowSerieCount(i_RowToCheck, i_ColumnToCheck, ref potentialUnderCount, false);
            potentialSerieCount = potentialOverCount + potentialUnderCount + 1; // +1 the cell is itself
            return potentialSerieCount >= k_SerieSize;
        }

        private void calcRowSerieCount(int i_RowToCheck, int i_ColumnToCheck, ref int potentialOverCount, bool isAbove)
        {
            int i = 1;
            bool legalPotentialSerie = true;
            int mul = isAbove ? 1 : -1;
            while (legalPotentialSerie && i_ColumnToCheck + (i * mul) < Columns && i_ColumnToCheck + (i * mul) >= 0)
            {
                int nextRowToCheck = 0;
                if (Board[i_RowToCheck, i_ColumnToCheck + (i * mul)] != GameBoard.eBoardSquare.Player1Square)
                {
                    nextRowToCheck = i_RowToCheck + 1;
                    if (isValidRow(nextRowToCheck) && Board[nextRowToCheck, i_ColumnToCheck + (i * mul)] == GameBoard.eBoardSquare.EmptySquare)
                    {
                        legalPotentialSerie = false;
                    }
                    else
                    {
                        potentialOverCount++;
                        i++;
                    }
                }
                else
                {
                    nextRowToCheck = i_RowToCheck - 1;
                    if (isValidRow(nextRowToCheck) && Board[nextRowToCheck, i_ColumnToCheck + (i * mul)] == GameBoard.eBoardSquare.EmptySquare)
                    {
                        potentialOverCount = 0;
                    }

                    legalPotentialSerie = false;
                }
            }
        }

        private bool isValidRow(int i_Row)
        {
            return i_Row >= 0 && i_Row < Rows;
        }

        public bool IsTherePotentialSerie(int i_PotentialColumnToInsert)
        {
            bool hasPotential = false;
            int rowToCheck = EmptyRowsInColumn[i_PotentialColumnToInsert] - 1;
            if (rowToCheck >= 0)
            {
                hasPotential = hasPotential | checkRowSeriePotential(rowToCheck, i_PotentialColumnToInsert);
                hasPotential = hasPotential | checkColumnSeriePotential(rowToCheck, i_PotentialColumnToInsert);
                hasPotential = hasPotential | checkDiagonalSeriePotential(rowToCheck, i_PotentialColumnToInsert);
            }

            return hasPotential;
        }

        private bool checkDiagonalSeriePotential(int i_RowToCheck, int i_ColumnToCheck)
        {
            int potentialSerieCount1 = 0, potentialSerieCount2 = 0;
            int potentialUnderCount = 0, potentialAboveCount = 0;

            calcDiagonalSerieCount(i_RowToCheck, i_ColumnToCheck, out potentialAboveCount, true, true);
            calcDiagonalSerieCount(i_RowToCheck, i_ColumnToCheck, out potentialUnderCount, false, false);
            potentialSerieCount1 = potentialAboveCount + potentialUnderCount;

            calcDiagonalSerieCount(i_RowToCheck, i_ColumnToCheck, out potentialAboveCount, false, true);
            calcDiagonalSerieCount(i_RowToCheck, i_ColumnToCheck, out potentialUnderCount, true, false);
            potentialSerieCount2 = potentialAboveCount + potentialUnderCount;

            int maxPotentialSerie = Math.Max(potentialSerieCount1, potentialSerieCount2);
            return maxPotentialSerie >= k_SerieSize;
        }

        private void calcDiagonalSerieCount(int i_RowToCheck, int i_ColumnToCheck, out int potentialCount, bool aboveXIndex, bool aboveYIndex)
        {
            int i = 0;
            potentialCount = 0;
            int mulX = aboveXIndex ? 1 : -1, mulY = aboveYIndex ? 1 : -1;
            bool legalPotentialSerie = true;
            while (legalPotentialSerie && i_ColumnToCheck + (i * mulY) < Columns && i_RowToCheck + (i * mulX) < Rows
                   && i_ColumnToCheck + (i * mulX) >= 0 && i_RowToCheck + (i * mulY) >= 0)
            {
                if (Board[i_RowToCheck + (i * mulY), i_ColumnToCheck + (i * mulX)] != GameBoard.eBoardSquare.Player1Square)
                {
                    int nextRowtoCheck = i_RowToCheck + (i * mulY) - 1;
                    if (isValidRow(nextRowtoCheck) && Board[nextRowtoCheck, i_ColumnToCheck + (i * mulX)] == GameBoard.eBoardSquare.EmptySquare)
                    {
                        legalPotentialSerie = false;
                    }
                    else
                    {
                        potentialCount++;
                        i++;
                    }
                }
                else
                {
                    if (i_RowToCheck == 0 || Board[i_RowToCheck + (i * mulY) - 1, i_ColumnToCheck + (i * mulX)] == GameBoard.eBoardSquare.EmptySquare)
                    {
                        potentialCount = 0;
                    }

                    legalPotentialSerie = false;
                }
            }
        }

        private bool checkColumnSeriePotential(int i_RowToCheck, int i_ColumnToCheck)
        {
            int potentialSerieCount = 0, potentialUnderCount = 0, potentialAboveCount = 0;
            calcColumnSerieCount(i_RowToCheck, i_ColumnToCheck, ref potentialAboveCount, true);
            calcColumnSerieCount(i_RowToCheck, i_ColumnToCheck, ref potentialUnderCount, false);
            potentialSerieCount = potentialAboveCount + potentialUnderCount + 1; 
            return potentialSerieCount >= k_SerieSize;
        }

        private void calcColumnSerieCount(int i_RowToCheck, int i_ColumnToCheck, ref int potentialCount, bool isAbove)
        {
            bool sameSquare = true;
            int i = 1;
            int mul = isAbove ? 1 : -1;

            while (sameSquare && i_RowToCheck + (i * mul) < Rows && i_RowToCheck + (i * mul) >= 0)
            {
                int nextRowtoCheck = i_RowToCheck + (i * mul);
                if (isValidRow(nextRowtoCheck) && Board[nextRowtoCheck, i_ColumnToCheck] != GameBoard.eBoardSquare.Player1Square)
                {
                    potentialCount++;
                    i++;
                }
                else
                {
                    sameSquare = false;
                }
            }
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
