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
        public bool finished { get; private set; }
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            PlacePieces();
        }

        public void MakeMove(Position orgin, Position target)
        {
            Piece piece = board.RemovePiece(orgin);
            piece.IncreaseQuantityOfMoves();
            Piece capturedPiece = board.RemovePiece(target);
            board.PlacePiece(piece, target);
            if(capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
        }

        public void RealiseMove(Position origin, Position target)
        {
            MakeMove(origin, target);
            turn++;
            ChangePlayer();
        }

        public void ChangePlayer()
        {
            currentPlayer = currentPlayer == Color.White ? Color.Black : Color.White;
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
            PlaceNewPiece('c', 1, new Rook(board, Color.White));
            PlaceNewPiece('c', 2, new Rook(board, Color.White));
            PlaceNewPiece('d', 1, new King(board, Color.White));
            PlaceNewPiece('d', 2, new Rook(board, Color.White));
            PlaceNewPiece('e', 1, new Rook(board, Color.White));
            PlaceNewPiece('e', 2, new Rook(board, Color.White));

            PlaceNewPiece('c', 8, new Rook(board, Color.Black));
            PlaceNewPiece('c', 7, new Rook(board, Color.Black));
            PlaceNewPiece('d', 8, new King(board, Color.Black));
            PlaceNewPiece('d', 7, new Rook(board, Color.Black));
            PlaceNewPiece('e', 8, new Rook(board, Color.Black));
            PlaceNewPiece('e', 7, new Rook(board, Color.Black));
        }
    }
}
