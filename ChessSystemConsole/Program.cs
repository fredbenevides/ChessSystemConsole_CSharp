using System;
using GenericBoard.Entities;
using GenericBoard.Entities.Enums;
using GenericBoard.Exceptions;
using Chess.Entities;

namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch match = new ChessMatch();
            while (!match.finished)
            {
                try
                {
                    Console.Clear();
                    UI.PrintMatch(match);

                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = UI.ReadChessPosition().toPosition();
                    match.ValidateOriginPosition(origin);
                    bool[,] PossibleMoves = match.board.Piece(origin).PossibleTargetPositions();
                    Console.Clear();
                    UI.PrintBoard(match.board, PossibleMoves);
                    Console.WriteLine();
                    Console.Write("Target: ");
                    Position target = UI.ReadChessPosition().toPosition();
                    match.ValidateTargetPosition(origin, target);
                    match.RealiseMove(origin, target);
                }
                catch (BoardException e)
                {
                    Console.WriteLine(e);
                    Console.ReadLine();
                }
            }
            Console.Clear();
            UI.PrintMatch(match);
        }
    }
}
