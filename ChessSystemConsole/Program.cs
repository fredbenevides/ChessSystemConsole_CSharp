﻿using System;
using GenericBoard.Entities;
using GenericBoard.Entities.Enums;
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
                Console.Clear();
                UI.PrintBoard(match.board);
                Console.WriteLine();
                Console.Write("Origin: ");
                Position origin = UI.ReadChessPosition().toPosition();
                Console.Write("Target: ");
                Position target = UI.ReadChessPosition().toPosition();
                match.MakeMove(origin, target);
            }
        }
    }
}
