using System;
using GenericBoard.Entities;
using GenericBoard.Entities.Enums;
using Chess.Entities;

namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));
            try
            {
                board.PlacePiece(new Rook(board, Color.White), new Position(0, 1));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            board.PlacePiece(new King(board, Color.Black), new Position(2, 4));
            UI.PrintBoard(board);
            Position p = new Position(0, 0);
        }
    }
}
