using Ex02_GameUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    internal class Ai
    {
        TreeNode<GameBoard> root;
        public int GetNextMove(GameBoard i_GameBoard)
        {
            int indexChosen = -1;
            root = new TreeNode<GameBoard>(i_GameBoard,0);
            buildDesicionsTree(root,3,true); // TODO: change 5 to constant depth which will be decided later
            int bestValue = minMax(root, 3, true);

            foreach (TreeNode<GameBoard> tNode in root.Children)
            {
                if (tNode.Score == bestValue)
                {
                    indexChosen = tNode.Index;
                    break;
                }
            }
            return indexChosen;
        }

        private void buildDesicionsTree(TreeNode<GameBoard> root, int depth,bool io_IsComputerPlaying)
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
                else
                {
                    Console.Beep();
                }
            }
            foreach (TreeNode<GameBoard> childNode in root.Children)
            {
                buildDesicionsTree(childNode, depth - 1,!io_IsComputerPlaying);
            }
        }

        private int minMax(TreeNode<GameBoard> root, int depth, bool maximizing)
        {
            //TODO: optionalchange true and false to constants v_Maximizing / v_Minimizing
            int bestValue;
            if (!root.Data.BoardStatus.Equals(GameBoard.eBoardStatus.NextPlayerCanPlay) || depth == 0)
            { // if game is in terminal state or no more tree levels
                return calculateScoreForResult(root.Data,maximizing);
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
                    if (i_IsComputerTurn == true)
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
