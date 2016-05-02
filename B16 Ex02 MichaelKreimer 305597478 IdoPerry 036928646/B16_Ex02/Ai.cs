using System.Collections.Generic;
using Ex02_GameUtils;

namespace B16_Ex02
{
    internal class Ai
    {
        private TreeNode<GameBoard> root;

        public int GetNextMove(GameBoard i_GameBoard)
        {
            int indexChosen = -1;
            root = new TreeNode<GameBoard>(i_GameBoard, 0);
            buildDesicionsTree(root, GameUtils.k_MaxEfficientDepth, true);
            int bestValue = minMax(root, GameUtils.k_MaxEfficientDepth, true);
            foreach (TreeNode<GameBoard> tNode in root.Children)
            {
                if (tNode.Score == bestValue)
                {
                    indexChosen = tNode.Index;
                    break;
                }
            }

            foreach (TreeNode<GameBoard> tNode in root.Children)
            {
                int index = tNode.Index;
                int potentialIndex;
                bool foundPotential = tryToGetMoveWithPotentialSerie(i_GameBoard, out potentialIndex);
                if (tNode.Score == bestValue && foundPotential)
                {
                    if (isIndexIsTheBestMove(root.Children, bestValue, potentialIndex))
                    {
                        indexChosen = potentialIndex;
                    }
                    else
                    {
                        indexChosen = tNode.Index;
                    }

                    break;
                }
            }

            return indexChosen;
        }

        private bool isIndexIsTheBestMove(ICollection<TreeNode<GameBoard>> children, int bestValue, int potentialIndex)
        {
            bool result = false;
            foreach (TreeNode<GameBoard> gameBoard in children)
            {
                if (gameBoard.Index == potentialIndex && gameBoard.Score == bestValue)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        private bool tryToGetMoveWithPotentialSerie(GameBoard i_GameBoard, out int io_IndexChosen)
        {
            bool foundPotential = false;
            io_IndexChosen = -1;
            for (int i = 0; i < i_GameBoard.Columns && !foundPotential; i++)
            {
                if (i_GameBoard.IsTherePotentialSerie(i))
                {
                    io_IndexChosen = i;
                    foundPotential = true;
                }
            }

            return foundPotential;
        }

        private void buildDesicionsTree(TreeNode<GameBoard> root, int depth, bool io_IsComputerPlaying)
        {
            GameBoard.eBoardSquare eBoard = io_IsComputerPlaying ? GameBoard.eBoardSquare.Player2Square : GameBoard.eBoardSquare.Player1Square;
            if (!root.Data.BoardStatus.Equals(GameBoard.eBoardStatus.NextPlayerCanPlay) || depth == 0)
            {
                return;
            }

            for (int i = 0; i < root.Data.Columns; i++)
            {
                GameBoard newBoard = (GameBoard)root.Data.Clone();
                bool isColumnFull = newBoard.TryToSetColumnSquare(i, eBoard);
                if (isColumnFull == false)
                {
                    root.AddChild(newBoard, i);
                }
            }

            foreach (TreeNode<GameBoard> childNode in root.Children)
            {
                buildDesicionsTree(childNode, depth - 1, !io_IsComputerPlaying);
            }
        }

        private int minMax(TreeNode<GameBoard> root, int depth, bool maximizing)
        {
            int bestValue;
            if (!root.Data.BoardStatus.Equals(GameBoard.eBoardStatus.NextPlayerCanPlay) || depth == 0)
            { 
                return calculateScoreForResult(root.Data, maximizing);
            }

            if (maximizing == true)
            {
                bestValue = int.MinValue;
                foreach (TreeNode<GameBoard> childNode in root.Children)
                {
                    childNode.Score = minMax(childNode, depth - 1, false);
                    if (bestValue < childNode.Score)
                    {
                        bestValue = childNode.Score;
                    }
                }
            }
            else
            {
                bestValue = int.MaxValue;
                foreach (TreeNode<GameBoard> childNode in root.Children)
                {
                    childNode.Score = minMax(childNode, depth - 1, true);
                    if (bestValue > childNode.Score)
                    {
                        bestValue = childNode.Score;
                    }
                }
            }

            return bestValue;
        }

        private int calculateScoreForResult(GameBoard i_BoardData, bool i_IsComputerTurn)
        {
            int score = 0;
            if (i_BoardData.BoardStatus.Equals(GameBoard.eBoardStatus.PlayerWon))
            {
                if (i_IsComputerTurn == false)
                {
                    score = 1;
                }
                else
                {
                    score = -1;
                }
            }

            return score;
        }
    }
}