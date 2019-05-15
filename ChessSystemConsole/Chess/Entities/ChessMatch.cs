using System;
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

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            PlacePieces();
        }

        public void MakeMove(Position orgin, Position target)
        {
            Piece piece = board.RemovePiece(orgin);
            piece.IncreaseQuantityOfMoves();
            Piece capturedPiece =  board.RemovePiece(target);
            board.PlacePiece(piece, target);
        }

        public void RealiseMove(Position origin, Position target)
        {
            MakeMove(origin, target);
            turn++;
            ChangePlayer();
        }

        public void ChangePlayer()
        {
           if(currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public void ValidateOriginPosition(Position position)
        {
            if(!board.ThereIsAPiece(position))
            {
                throw new BoardException("There is no piece on the origin position selected!");
            }
            if(currentPlayer != board.Piece(position).Color)
            {
                throw new BoardException("The selected piece is not yours!");
            }
            if (!board.Piece(position).ThereIsAPossibleMove()){
                throw new BoardException("There is no possible move for the selected piece!");
            }
        }

        public void ValidateTargetPosition(Position origin, Position target)
        {
            if (!board.Piece(origin).CanMoveTo(target))
            {
                throw new BoardException("Invalid target position!");
            }
        }

        private void PlacePieces()
        {
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('c', 1).toPosition());
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('c', 2).toPosition());
            board.PlacePiece(new King(board, Color.White), new ChessPosition('d', 1).toPosition());
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('d', 2).toPosition());
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('e', 1).toPosition());
            board.PlacePiece(new Rook(board, Color.White), new ChessPosition('e', 2).toPosition());

            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('c', 8).toPosition());
            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('c', 7).toPosition());
            board.PlacePiece(new King(board, Color.Black), new ChessPosition('d', 8).toPosition());
            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('d', 7).toPosition());
            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('e', 8).toPosition());
            board.PlacePiece(new Rook(board, Color.Black), new ChessPosition('e', 7).toPosition());

        }
    }
}
