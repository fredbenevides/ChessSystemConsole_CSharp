using System;
using GenericBoard.Entities;
using GenericBoard.Entities.Enums;
namespace Chess.Entities
{
    public class ChessMatch
    {
    
        public Board board { get; private set; }
        private int turn;
        private Color currentPlayer;
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
