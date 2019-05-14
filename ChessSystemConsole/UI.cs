using System;
using GenericBoard.Entities;
using GenericBoard.Entities.Enums;
using Chess.Entities;
namespace ChessSystemConsole
{
    public class UI
    {
        public static void PrintBoard(Board board)
        {
            ConsoleColor aux = Console.ForegroundColor;
            for (int i = 0; i < board.Ranges; i++)
            {

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = aux;
                for(int j = 0; j < board.Collumns; j++)
                {
                    if(board.Piece(i, j) == null)
                    {
                        if ((i + j) % 2 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("- ");
                            Console.ForegroundColor = aux;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("- ");
                            Console.ForegroundColor = aux;
                        }
                    }
                    else
                    {
                        PrintPiece(board.Piece(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("  a b c d e f g h");
            Console.ForegroundColor = aux;
        }

        public static void PrintPiece(Piece piece)
        {
            ConsoleColor aux = Console.ForegroundColor;
            if(piece.Color == Color.White)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece);
                Console.ForegroundColor = aux;
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char collumn = s[0];
            int range = int.Parse(s[1] + "");
            return new ChessPosition(collumn, range);
        }
    }
}
