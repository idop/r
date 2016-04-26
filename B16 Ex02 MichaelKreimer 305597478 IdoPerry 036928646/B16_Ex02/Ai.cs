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
            root = new TreeNode<GameBoard>(i_GameBoard);
            buildDesicionsTree(root,5); // TODO: change 5 to constant depth which will be decided later
            return minMax(root, 5, true);
        }

        private void buildDesicionsTree(TreeNode<GameBoard> root, int depth)
        {
            for (int i = 0; i < root.Data.Columns; i++)
            {

                if (newBoard.pieceCanBeInsertedToColumn(i))
                {
                    GameBoard newBoard = new GameBoard(root.Data.Rows, root.Data.Columns);
                    newBoard.insertPieceToColumn(i);
                    root.AddChild(newBoard);
                }
            }
        }

        private int minMax(TreeNode<GameBoard> root, int depth, bool maximizing)
        {
            //TODO: change true and false to constants v_Maximizing / v_Minimizing
            int bestValue,currentValue;
            if (/*root.Data.hasWinner ||*/ depth == 0)
            {
                return calculateScoreForResult(root.Data);
            }
            if (maximizing == true)
            {
                bestValue = int.MinValue;
                foreach (TreeNode<GameBoard> childNode in root.Children)
                {
                    currentValue = minMax(childNode, depth - 1, false);
                    bestValue = Math.Max(currentValue, bestValue);
                }
            }
            else
            {
                bestValue = int.MaxValue;
                foreach(TreeNode<GameBoard>childNode in root.Children)
                {
                    currentValue = minMax(childNode, depth - 1, true);
                    bestValue = Math.Min(currentValue, bestValue);
                }
            }
            return bestValue;
        }

        private int calculateScoreForResult(GameBoard data)
        {
            throw new NotImplementedException();
        }
    }
}
