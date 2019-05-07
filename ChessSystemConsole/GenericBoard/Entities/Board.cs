using GenericBoard.Exceptions;

namespace GenericBoard.Entities
{
    public class Board
    {
        public int Ranges { get; set; }
        public int Collumns { get; set; }
        private Piece[,] Pieces;

        public Board()
        {
        }

        public Board(int ranges, int collumns)
        {
            Ranges = ranges;
            Collumns = collumns;
            Pieces = new Piece[ranges, collumns];
        }

        public Piece Piece(int range, int collumn)
        {
            return Pieces[range, collumn];
        }

        public Piece Piece(Position position)
        {
            return Pieces[position.Range, position.Collumn];
        }

        public bool ThereIsAPiece(Position position)
        {
            ValidatePosition(position);
            return Piece(position) != null;
        }

        public void PlacePiece(Piece piece, Position position)
        {
            if (ThereIsAPiece(position))
            {
                throw new BoardException("There is already a piece in that position!");
            }
            Pieces[position.Range, position.Collumn] = piece;
            piece.Position = position;
        }

        public bool ValidPosition(Position position) => position.Range >= 0 && position.Range < Ranges && position.Collumn >= 0 && position.Collumn < Collumns;

        public void ValidatePosition(Position position)
        {
            if (!ValidPosition(position))
            {
                throw new BoardException("Invalid Position!");
            }
        }
    }
}
