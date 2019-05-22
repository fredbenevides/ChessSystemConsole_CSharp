using System.Collections.Generic;
using GenericBoard.Entities;
using GenericBoard.Entities.Enums;
using GenericBoard.Exceptions;

namespace Chess.Entities
{
    public class ChessMatch
    {

        public Board Board { get; private set; }
        public int Turn { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            Board = new Board(8, 8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
            VulnerableEnPassant = null;
        }

        public Piece MakeMove(Position orgin, Position target)
        {
            Piece piece = Board.RemovePiece(orgin);
            piece.IncreaseQuantityOfMoves();
            Piece capturedPiece = Board.RemovePiece(target);
            Board.PlacePiece(piece, target);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            if (piece is Pawn && orgin.Collumn != target.Collumn && capturedPiece == null)
            {
                Position posP = piece.Color == Color.White ? new Position(target.Range + 1, target.Collumn) : new Position(target.Range - 1, target.Collumn);
                capturedPiece = Board.RemovePiece(posP);
                captured.Add(capturedPiece);
            }
            return capturedPiece;
        }

        private void UndoMove(Position origin, Position target, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(target);
            p.DecreaseQuantityOfMoves();
            if (capturedPiece != null)
            {
                Board.PlacePiece(capturedPiece, target);
                captured.Remove(capturedPiece);
            }
            Board.PlacePiece(p, origin);
            if (IsCastle(origin, target))
            {
                if (origin.Collumn < target.Collumn)
                {
                    Position originPositionRook = new Position(origin.Range, origin.Collumn + 3);
                    Position targetPositionRook = new Position(origin.Range, origin.Collumn + 1);
                    Piece piece = Board.RemovePiece(targetPositionRook);
                    piece.DecreaseQuantityOfMoves();
                    Board.PlacePiece(piece, originPositionRook);
                }
                else
                {
                    Position originPositionRook = new Position(origin.Range, origin.Collumn - 4);
                    Position targetPositionRook = new Position(origin.Range, origin.Collumn - 1);
                    Piece piece = Board.RemovePiece(targetPositionRook);
                    piece.DecreaseQuantityOfMoves();
                    Board.PlacePiece(piece, originPositionRook);
                }
            }
            if (p is Pawn && origin.Collumn != target.Collumn && capturedPiece == VulnerableEnPassant)
            {
                Piece capturedEnPassant = Board.RemovePiece(target);
                if (p.Color == Color.White)
                {
                    Board.PlacePiece(capturedEnPassant, new Position(3, target.Collumn));
                }
                else
                {
                    Board.PlacePiece(capturedEnPassant, new Position(4, target.Collumn));
                }
            }
        }

        public void RealiseMove(Position origin, Position target)
        {
            if (!IsCastle(origin, target))
            {
                Piece capturedPiece = MakeMove(origin, target);
                if (IsCheck(CurrentPlayer))
                {
                    UndoMove(origin, target, capturedPiece);
                    throw new BoardException("This move is not allowed! You can't put yourself in check!");
                }
                Piece piece = Board.Piece(target);
                if (piece is Pawn && (target.Range == 0 || target.Range == 7))
                {
                    piece = Board.RemovePiece(target);
                    pieces.Remove(piece);
                    Piece queen = new Queen(Board, piece.Color);
                    Board.PlacePiece(queen, target);
                    pieces.Add(queen);
                }
                Check = IsCheck(Opponent(CurrentPlayer));
                if (IsCheckmate(Opponent(CurrentPlayer)))
                {
                    Finished = true;
                }
                else
                {
                    Turn++;
                    ChangePlayer();
                }
            }
            else
            {
                if (origin.Collumn < target.Collumn) // roque pequeno
                {
                    Piece capturedPiece = MakeMove(origin, new Position(origin.Range, origin.Collumn + 1));
                    if (IsCheck(CurrentPlayer))
                    {
                        UndoMove(origin, new Position(origin.Range, origin.Collumn + 1), capturedPiece);
                        throw new BoardException("This move is not allowed! The king can not pass over a square in threat!");
                    }
                    UndoMove(origin, new Position(origin.Range, origin.Collumn + 1), capturedPiece);
                    capturedPiece = MakeMove(origin, target);
                    Position originPositionRook = new Position(origin.Range, origin.Collumn + 3);
                    Position targetPositionRook = new Position(origin.Range, origin.Collumn + 1);
                    Piece piece = Board.RemovePiece(originPositionRook);
                    piece.IncreaseQuantityOfMoves();
                    Board.PlacePiece(piece, targetPositionRook);
                    if (IsCheck(CurrentPlayer))
                    {
                        UndoMove(origin, target, capturedPiece);
                        throw new BoardException("This move is not allowed! You can't put yourself in check!");
                    }
                    Check = IsCheck(Opponent(CurrentPlayer));
                    if (IsCheckmate(Opponent(CurrentPlayer)))
                    {
                        Finished = true;
                    }
                    else
                    {
                        Turn++;
                        ChangePlayer();
                    }
                }
                else //roque grande
                {
                    Piece capturedPiece = MakeMove(origin, new Position(origin.Range, origin.Collumn - 1));
                    if (IsCheck(CurrentPlayer))
                    {
                        UndoMove(origin, new Position(origin.Range, origin.Collumn - 1), capturedPiece);
                        throw new BoardException("This move is not allowed! The king can not pass over a square in threat!");
                    }
                    UndoMove(origin, new Position(origin.Range, origin.Collumn - 1), capturedPiece);
                    capturedPiece = MakeMove(origin, target);
                    Position originPositionRook = new Position(origin.Range, origin.Collumn - 4);
                    Position targetPositionRook = new Position(origin.Range, origin.Collumn - 1);
                    Piece piece = Board.RemovePiece(originPositionRook);
                    piece.IncreaseQuantityOfMoves();
                    Board.PlacePiece(piece, targetPositionRook);
                    if (IsCheck(CurrentPlayer))
                    {
                        UndoMove(origin, target, capturedPiece);
                        throw new BoardException("This move is not allowed! You can't put yourself in check!");
                    }
                    Check = IsCheck(Opponent(CurrentPlayer));
                    if (IsCheckmate(Opponent(CurrentPlayer)))
                    {
                        Finished = true;
                    }
                    else
                    {
                        Turn++;
                        ChangePlayer();
                    }
                }
            }
            Piece p = Board.Piece(target);
            VulnerableEnPassant = p is Pawn && (origin.Range == target.Range + 2 || origin.Range == target.Range - 2) ? p : null;
        }

        private bool IsCastle(Position origin, Position target)
        {
            return (Board.Piece(origin) is King) && ((origin.Collumn == target.Collumn - 2) || (origin.Collumn == target.Collumn + 2));
        }

        public void ChangePlayer()
        {
            CurrentPlayer = CurrentPlayer == Color.White ? Color.Black : Color.White;
        }

        private Color Opponent(Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        private Piece King(Color color)
        {
            foreach (Piece x in PiecesOnTheBoard(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool IsCheck(Color color)
        {
            Piece k = King(color);
            if (k == null)
            {
                throw new BoardException("There is no " + color + " king on the board!");
            }
            foreach (Piece x in PiecesOnTheBoard(Opponent(color)))
            {
                bool[,] mat = x.PossibleTargetPositions();
                if (mat[k.Position.Range, k.Position.Collumn])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsCheckmate(Color color)
        {
            if (!IsCheck(color))
            {
                return false;
            }
            foreach (Piece x in PiecesOnTheBoard(color))
            {
                bool[,] mat = x.PossibleTargetPositions();
                for (int i = 0; i < Board.Ranges; i++)
                {
                    for (int j = 0; j < Board.Collumns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position target = new Position(i, j);
                            Piece capturedPiece = MakeMove(origin, target);
                            bool testCheck = IsCheck(color);
                            UndoMove(origin, target, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> PiecesOnTheBoard(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(CapturedPieces(color));
            return aux;
        }

        public void ValidateOriginPosition(Position position)
        {
            if (!Board.ThereIsAPiece(position))
            {
                throw new BoardException("There is no piece on the origin position selected!");
            }
            if (CurrentPlayer != Board.Piece(position).Color)
            {
                throw new BoardException("The selected piece is not yours!");
            }
            if (!Board.Piece(position).ThereIsAPossibleMove())
            {
                throw new BoardException("There is no possible move for the selected piece!");
            }
        }

        public void ValidateTargetPosition(Position origin, Position target)
        {
            if (!Board.Piece(origin).PossibleMove(target))
            {
                throw new BoardException("Invalid target position!");
            }
        }

        public void PlaceNewPiece(char collumn, int range, Piece piece)
        {
            Board.PlacePiece(piece, new ChessPosition(collumn, range).ToPosition());
            pieces.Add(piece);
        }

        private void PlacePieces()
        {
            PlaceNewPiece('a', 1, new Rook(Board, Color.White));
            PlaceNewPiece('b', 1, new Knight(Board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('d', 1, new Queen(Board, Color.White));
            PlaceNewPiece('e', 1, new King(Board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(Board, Color.White));
            PlaceNewPiece('g', 1, new Knight(Board, Color.White));
            PlaceNewPiece('h', 1, new Rook(Board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(Board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(Board, Color.White, this));

            PlaceNewPiece('a', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('b', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(Board, Color.Black));
            PlaceNewPiece('e', 8, new King(Board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(Board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(Board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(Board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }
    }
}