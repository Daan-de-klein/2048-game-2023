using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game
{
    internal class GameFunctions
    {
        private string[,] board;
        private string[,] oldBoard;
        private HashSet<(int, int)> emptyCells;
        private int score;
        public GameFunctions()
        {
            score = 0;
            CreateBoard();
        }
        public void CreateBoard()
        {
            board = new string[4, 4];
            emptyCells = new HashSet<(int, int)>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    board[i, j] = "";
                    emptyCells.Add((i, j));
                }
            }
        }
        public string[,] getBoard()
        {
            return board;
        }
        public string GetColor(int number)
        {
            switch (number)
            {
                default:
                    return "#CDC1B4";
                case 2:
                    return "#EEE4DA";
                case 4:
                    return "#EDE0C8";
                case 8:
                    return "#F2B179";
                case 16:
                    return "#F59563";
                case 32:
                    return "#F67C5F";
                case 64:
                    return "#F65E3B";
                case 128:
                    return "#EDCF72";
                case 256:
                    return "#EDCC61";
                case 512:
                    return "#EDC850";
                case 1048:
                    return "#EDC53F";
                case 2048:
                    return "#EDC22E";
            }
        }
        public void UpdateEmptyCells()
        {
            emptyCells = new HashSet<(int, int)>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (board[i, j] == "")
                    {
                        emptyCells.Add((i, j));
                    }
                }
            }
        }
        public string[,] AddRandomTile()
        {
            UpdateEmptyCells();
            if (emptyCells.Count == 0)
            {
                return board;
            }
            else
            {
                if (AreBothArraysEquel(oldBoard, board))
                {
                    return board;
                }
                else
                {
                    Random random = new Random();
                    Random tile = new Random();

                    int index = random.Next(emptyCells.Count);
                    var (i, j) = emptyCells.ElementAt(index);
                    int spawnedTile = tile.Next(1,6);
                    if (spawnedTile == 5)
                    {
                        board[i, j] = "4";
                    }
                    else
                    {
                        board[i, j] = "2";
                    }
                    emptyCells.Remove((i, j));

                    return board;
                }
            }
        }
        public bool AreBothArraysEquel(string[,] _oldboard, string[,] _board)
        {
            if (_oldboard == null)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (_oldboard[i, j] != _board[i, j])
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
        }
        public int GetScore()
        {
            return this.score;
        }
        public string[,] PlayMove(string direction)
        {
            oldBoard = (string[,])board.Clone();
            switch (direction)
            {
                case "Left":
                    return MoveLeft();
                case "Right":
                    return MoveRight();
                case "Up":
                    return MoveUp();
                case "Down":
                    return MoveDown();
            }
            return null;
        }
        public string[,] MoveLeft()
        {
            // saves the board data and starts a vertical for loop
            string[,] newBoard = new string[4,4];
            Array.Copy(board, newBoard, board.Length);

            for (int col = 0; col < 4; col++)
            {
                // assigns a targetindex and starts a horizontal loop
                int targetIndex = 0;
                for (int row = 0; row < 4; row++)
                {
                    // checks if the index contains a number
                    if (newBoard[col, row] != "")
                    {
                        // checks if the targetIndex contains a number
                        if (newBoard[col, targetIndex] != "")
                        {
                            // checks if the targetIndex has the same value as the index and it checks if the targetIndex is not the same as the index 
                            if (newBoard[col, targetIndex] == newBoard[col, row] && targetIndex != row)
                            {
                                string boardScore = newBoard[col, targetIndex];
                                int newscore = Convert.ToInt32(boardScore);
                                newscore = newscore * 2;
                                this.score += newscore;
                                newBoard[col, targetIndex] = Convert.ToString(newscore);
                                newBoard[col, row] = "";
                                targetIndex++;
                            }
                            else
                            {
                                if (newBoard[col, targetIndex] != newBoard[col, row])
                                {
                                    targetIndex++;

                                    if (targetIndex != row)
                                    {
                                        newBoard[col, targetIndex] = newBoard[col, row];
                                        newBoard[col, row] = "";
                                    }  
                                }
                            }
                        }
                        // puts the data from the index to the targetIndex location and changes the targetIndex with 1. it also clears the original index location
                        else
                        {
                            newBoard[col, targetIndex] = newBoard[col, row];
                            newBoard[col, row] = "";
                        }
                    }
                }
            }
            board = newBoard;
            return board;
        }
        public string[,] MoveRight()
        {
            // saves the board data and starts a vertical for loop
            string[,] newBoard = new string[4, 4];
            Array.Copy(board, newBoard, board.Length);

            for (int col = 0; col < 4; col++)
            {
                // assigns a targetindex and starts a horizontal loop
                int targetIndex = 3;
                for (int row = 3; row > -1; row--)
                {
                    // checks if the index contains a number
                    if (newBoard[col, row] != "")
                    {
                        // checks if the targetIndex contains a number
                        if (newBoard[col, targetIndex] != "")
                        {
                            // checks if the targetIndex has the same value as the index and it checks if the targetIndex is not the same as the index 
                            if (newBoard[col, targetIndex] == newBoard[col, row] && targetIndex != row)
                            {
                                string boardScore = newBoard[col, targetIndex];
                                int newscore = Convert.ToInt32(boardScore);
                                newscore = newscore * 2;
                                this.score += newscore;
                                newBoard[col, targetIndex] = Convert.ToString(newscore);
                                newBoard[col, row] = "";
                                targetIndex--;
                            }
                            else
                            {
                                if (newBoard[col, targetIndex] != newBoard[col, row])
                                {
                                    targetIndex--;

                                    if (targetIndex != row)
                                    {
                                        newBoard[col, targetIndex] = newBoard[col, row];
                                        newBoard[col, row] = "";
                                    }
                                }
                            }
                        }
                        // puts the data from the index to the targetIndex location and changes the targetIndex with 1. it also clears the original index location
                        else
                        {
                            newBoard[col, targetIndex] = newBoard[col, row];
                            newBoard[col, row] = "";
                        }
                    }
                }
            }
            board = newBoard;
            return board;
        }
        public string[,] MoveUp()
        {
            // saves the board data and starts a vertical for loop
            string[,] newBoard = new string[4, 4];
            Array.Copy(board, newBoard, board.Length);

            for (int col = 0; col < 4; col++)
            {
                // assigns a targetindex and starts a horizontal loop
                int targetIndex = 0;
                for (int row = 0; row < 4; row++)
                {
                    // checks if the index contains a number
                    if (newBoard[row, col] != "")
                    {
                        // checks if the targetIndex contains a number
                        if (newBoard[targetIndex, col] != "")
                        {
                            // checks if the targetIndex has the same value as the index and it checks if the targetIndex is not the same as the index 
                            if (newBoard[targetIndex, col] == newBoard[row, col] && targetIndex != row)
                            {
                                string boardScore = newBoard[targetIndex, col];
                                int newscore = Convert.ToInt32(boardScore);
                                newscore = newscore * 2;
                                this.score += newscore;
                                newBoard[targetIndex, col] = Convert.ToString(newscore);
                                newBoard[row, col] = "";
                                targetIndex++;
                            }
                            else
                            {
                                if (newBoard[targetIndex, col] != newBoard[row, col])
                                {
                                    targetIndex++;

                                    if (targetIndex != row)
                                    {
                                        newBoard[targetIndex, col] = newBoard[row, col];
                                        newBoard[row, col] = "";
                                    }
                                }
                            }
                        }
                        // puts the data from the index to the targetIndex location and changes the targetIndex with 1. it also clears the original index location
                        else
                        {
                            newBoard[targetIndex, col] = newBoard[row, col];
                            newBoard[row, col] = "";
                        }
                    }
                }
            }
            board = newBoard;
            return board;
        }
        public string[,] MoveDown()
        {
            // saves the board data and starts a vertical for loop
            string[,] newBoard = new string[4, 4];
            Array.Copy(board, newBoard, board.Length);

            for (int col = 0; col < 4; col++)
            {
                // assigns a targetindex and starts a horizontal loop
                int targetIndex = 3;
                for (int row = 3; row > -1; row--)
                {
                    // checks if the index contains a number
                    if (newBoard[row, col] != "")
                    {
                        // checks if the targetIndex contains a number
                        if (newBoard[targetIndex, col] != "")
                        {
                            // checks if the targetIndex has the same value as the index and it checks if the targetIndex is not the same as the index 
                            if (newBoard[targetIndex, col] == newBoard[row, col] && targetIndex != row)
                            {
                                string boardScore = newBoard[targetIndex, col];
                                int newscore = Convert.ToInt32(boardScore);
                                newscore = newscore * 2;
                                this.score += newscore;
                                newBoard[targetIndex, col] = Convert.ToString(newscore);
                                newBoard[row, col] = "";
                                targetIndex--;
                            }
                            else
                            {
                                if (newBoard[targetIndex, col] != newBoard[row, col])
                                {
                                    targetIndex--;

                                    if (targetIndex != row)
                                    {
                                        newBoard[targetIndex, col] = newBoard[row, col];
                                        newBoard[row, col] = "";
                                    }
                                }
                            }
                        }
                        // puts the data from the index to the targetIndex location and changes the targetIndex with 1. it also clears the original index location
                        else
                        {
                            newBoard[targetIndex, col] = newBoard[row, col];
                            newBoard[row, col] = "";
                        }
                    }
                }
            }
            board = newBoard;
            return board;
        }
    }
}
