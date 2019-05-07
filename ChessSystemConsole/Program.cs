using System;
using GenericBoard;
using Chess;

namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            board.PlacePiece(new Rook(board, Color.Black), new Position(0, 0));
            board.PlacePiece(new Rook(board, Color.White), new Position(1, 3));
            board.PlacePiece(new King(board, Color.Black), new Position(2, 4));
            UI.PrintBoard(board);
        }
    }
}
