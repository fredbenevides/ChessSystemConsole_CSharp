using GenericBoard.Entities.Enums;
using GenericBoard.Entities;

namespace Chess.Entities
{
    public class Knight : Piece
    {
        public Knight(Board board, Color color) : base(color, board)
        {
        }

        private bool CanMoveTo(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        public override bool[,] PossibleTargetPositions()
        {
            bool[,] mat = new bool[Board.Ranges, Board.Collumns];
            Position p = new Position(0, 0);

            p.DefinePosition(Position.Range - 1, Position.Collumn - 2);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range - 2, Position.Collumn - 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range - 2, Position.Collumn + 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range - 1, Position.Collumn + 2);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range + 1, Position.Collumn + 2);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range + 2, Position.Collumn + 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range + 2, Position.Collumn - 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range + 1, Position.Collumn - 2);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }
            return mat;
        }

        public override string ToString()
        {
            return "N";
        }
    }
}