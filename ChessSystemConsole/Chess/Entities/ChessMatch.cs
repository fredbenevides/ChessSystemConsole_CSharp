using System.Collections.Generic;
using GenericBoard.Entities;
using GenericBoard.Entities.Enums;
using GenericBoard.Exceptions;

namespace Chess.Entities
{
    public class ChessMatch
    {

        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            Finished = false;
            Check = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
            VulnerableEnPassant = null;
        }

        public Piece MakeMove(Position orgin, Position target)
        {
            Piece piece = board.RemovePiece(orgin);
            piece.IncreaseQuantityOfMoves();
            Piece capturedPiece = board.RemovePiece(target);
            board.PlacePiece(piece, target);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            if (piece is Pawn && orgin.Collumn != target.Collumn && capturedPiece == null)
            {
                Position posP;
                if (piece.Color == Color.White)
                {
                    posP = new Position(target.Range + 1, target.Collumn);
                }
                else
                {
                    posP = new Position(target.Range - 1, target.Collumn);
                }
                capturedPiece = board.RemovePiece(posP);
                captured.Add(capturedPiece);
            }
            return capturedPiece;
        }

        private void UndoMove(Position origin, Position target, Piece capturedPiece)
        {
            Piece p = board.RemovePiece(target);
            p.DecreaseQuantityOfMoves();
            if (capturedPiece != null)
            {
                board.PlacePiece(capturedPiece, target);
                captured.Remove(capturedPiece);
            }
            board.PlacePiece(p, origin);
            if (IsCastle(origin, target))
            {
                if (origin.Collumn < target.Collumn)
                {
                    Position originPositionRook = new Position(origin.Range, origin.Collumn + 3);
                    Position targetPositionRook = new Position(origin.Range, origin.Collumn + 1);
                    Piece piece = board.RemovePiece(targetPositionRook);
                    piece.DecreaseQuantityOfMoves();
                    board.PlacePiece(piece, originPositionRook);
                }
                else
                {
                    Position originPositionRook = new Position(origin.Range, origin.Collumn - 4);
                    Position targetPositionRook = new Position(origin.Range, origin.Collumn - 1);
                    Piece piece = board.RemovePiece(targetPositionRook);
                    piece.DecreaseQuantityOfMoves();
                    board.PlacePiece(piece, originPositionRook);
                }
            }
            if (p is Pawn && origin.Collumn != target.Collumn && capturedPiece == VulnerableEnPassant)
            {
                Piece capturedEnPassant = board.RemovePiece(target);
                if(p.Color == Color.White)
                {
                    board.PlacePiece(capturedEnPassant, new Position(3, target.Collumn));
                }
                else
                {
                    board.PlacePiece(capturedEnPassant, new Position(4, target.Collumn));
                }
            }
        }

        public void RealiseMove(Position origin, Position target)
        {
            if (!IsCastle(origin, target))
            {
                Piece capturedPiece = MakeMove(origin, target);
                if (IsCheck(currentPlayer))
                {
                    UndoMove(origin, target, capturedPiece);
                    throw new BoardException("This move is not allowed! You can't put yourself in check!");
                }
                Check = IsCheck(Opponent(currentPlayer));
                if (IsCheckmate(Opponent(currentPlayer)))
                {
                    Finished = true;
                }
                else
                {
                    turn++;
                    ChangePlayer();
                }
            }
            else
            {
                if (origin.Collumn < target.Collumn) // roque pequeno
                {
                    Piece capturedPiece = MakeMove(origin, new Position(origin.Range, origin.Collumn + 1));
                    if (IsCheck(currentPlayer))
                    {
                        UndoMove(origin, new Position(origin.Range, origin.Collumn + 1), capturedPiece);
                        throw new BoardException("This move is not allowed! The king can not pass over a square in threat!");
                    }
                    UndoMove(origin, new Position(origin.Range, origin.Collumn + 1), capturedPiece);
                    capturedPiece = MakeMove(origin, target);
                    Position originPositionRook = new Position(origin.Range, origin.Collumn + 3);
                    Position targetPositionRook = new Position(origin.Range, origin.Collumn + 1);
                    Piece piece = board.RemovePiece(originPositionRook);
                    piece.IncreaseQuantityOfMoves();
                    board.PlacePiece(piece, targetPositionRook);
                    if (IsCheck(currentPlayer))
                    {
                        UndoMove(origin, target, capturedPiece);
                        throw new BoardException("This move is not allowed! You can't put yourself in check!");
                    }
                    Check = IsCheck(Opponent(currentPlayer));
                    if (IsCheckmate(Opponent(currentPlayer)))
                    {
                        Finished = true;
                    }
                    else
                    {
                        turn++;
                        ChangePlayer();
                    }
                }
                else //roque grande
                {
                    Piece capturedPiece = MakeMove(origin, new Position(origin.Range, origin.Collumn - 1));
                    if (IsCheck(currentPlayer))
                    {
                        UndoMove(origin, new Position(origin.Range, origin.Collumn - 1), capturedPiece);
                        throw new BoardException("This move is not allowed! The king can not pass over a square in threat!");
                    }
                    UndoMove(origin, new Position(origin.Range, origin.Collumn - 1), capturedPiece);
                    capturedPiece = MakeMove(origin, target);
                    Position originPositionRook = new Position(origin.Range, origin.Collumn - 4);
                    Position targetPositionRook = new Position(origin.Range, origin.Collumn - 1);
                    Piece piece = board.RemovePiece(originPositionRook);
                    piece.IncreaseQuantityOfMoves();
                    board.PlacePiece(piece, targetPositionRook);
                    if (IsCheck(currentPlayer))
                    {
                        UndoMove(origin, target, capturedPiece);
                        throw new BoardException("This move is not allowed! You can't put yourself in check!");
                    }
                    Check = IsCheck(Opponent(currentPlayer));
                    if (IsCheckmate(Opponent(currentPlayer)))
                    {
                        Finished = true;
                    }
                    else
                    {
                        turn++;
                        ChangePlayer();
                    }
                }
            }
            Piece p = board.Piece(target);
            VulnerableEnPassant = p is Pawn && (origin.Range == target.Range + 2 || origin.Range == target.Range - 2) ? p : null;
        }



        private bool IsCastle(Position origin, Position target)
        {
            return (board.Piece(origin) is King) && ((origin.Collumn == target.Collumn - 2) || (origin.Collumn == target.Collumn + 2));
        }

        public void ChangePlayer()
        {
            currentPlayer = currentPlayer == Color.White ? Color.Black : Color.White;
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
                for (int i = 0; i < board.Ranges; i++)
                {
                    for (int j = 0; j < board.Collumns; j++)
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
            if (!board.ThereIsAPiece(position))
            {
                throw new BoardException("There is no piece on the origin position selected!");
            }
            if (currentPlayer != board.Piece(position).Color)
            {
                throw new BoardException("The selected piece is not yours!");
            }
            if (!board.Piece(position).ThereIsAPossibleMove())
            {
                throw new BoardException("There is no possible move for the selected piece!");
            }
        }

        public void ValidateTargetPosition(Position origin, Position target)
        {
            if (!board.Piece(origin).PossibleMove(target))
            {
                throw new BoardException("Invalid target position!");
            }
        }

        public void PlaceNewPiece(char collumn, int range, Piece piece)
        {
            board.PlacePiece(piece, new ChessPosition(collumn, range).toPosition());
            pieces.Add(piece);
        }

        private void PlacePieces()
        {
            PlaceNewPiece('a', 1, new Rook(board, Color.White));
            PlaceNewPiece('b', 1, new Knight(board, Color.White));
            PlaceNewPiece('c', 1, new Bishop(board, Color.White));
            PlaceNewPiece('d', 1, new Queen(board, Color.White));
            PlaceNewPiece('e', 1, new King(board, Color.White, this));
            PlaceNewPiece('f', 1, new Bishop(board, Color.White));
            PlaceNewPiece('g', 1, new Knight(board, Color.White));
            PlaceNewPiece('h', 1, new Rook(board, Color.White));
            PlaceNewPiece('a', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('b', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('c', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('d', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('e', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('f', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('g', 2, new Pawn(board, Color.White, this));
            PlaceNewPiece('h', 2, new Pawn(board, Color.White, this));

            PlaceNewPiece('a', 8, new Rook(board, Color.Black));
            PlaceNewPiece('b', 8, new Knight(board, Color.Black));
            PlaceNewPiece('c', 8, new Bishop(board, Color.Black));
            PlaceNewPiece('d', 8, new Queen(board, Color.Black));
            PlaceNewPiece('e', 8, new King(board, Color.Black, this));
            PlaceNewPiece('f', 8, new Bishop(board, Color.Black));
            PlaceNewPiece('g', 8, new Knight(board, Color.Black));
            PlaceNewPiece('h', 8, new Rook(board, Color.Black));
            PlaceNewPiece('a', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('b', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('c', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('d', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('e', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('f', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('g', 7, new Pawn(board, Color.Black, this));
            PlaceNewPiece('h', 7, new Pawn(board, Color.Black, this));
        }
    }
}
