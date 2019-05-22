using System;
using GenericBoard.Entities;
using GenericBoard.Exceptions;
using Chess.Entities;

namespace ChessSystemConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessMatch match = new ChessMatch();
            while (!match.Finished)
            {
                try
                {
                    Console.Clear();
                    UI.PrintMatch(match);
                    Console.WriteLine();
                    Console.Write("Origin: ");
                    Position origin = UI.ReadChessPosition().ToPosition();
                    match.ValidateOriginPosition(origin);
                    bool[,] PossibleMoves = match.Board.Piece(origin).PossibleTargetPositions();
                    Console.Clear();
                    UI.PrintBoard(match.Board, PossibleMoves);
                    Console.WriteLine();
                    Console.Write("Target: ");
                    Position target = UI.ReadChessPosition().ToPosition();
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