using System;
using GenericBoard.Entities;
namespace ChessSystemConsole
{
    public class UI
    {
        public static void PrintBoard(Board board)
        {
            for(int i = 0; i < board.Ranges; i++)
            {
                for(int j = 0; j < board.Collumns; j++)
                {
                    if(board.Piece(i, j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(board.Piece(i, j) + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
