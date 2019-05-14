using GenericBoard.Entities.Enums;
using GenericBoard.Entities;

namespace Chess.Entities
{
    public class Rook : Piece
    {
        public Rook(Board board, Color color) : base(color, board)
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

            p.DefinePosition(Position.Range - 1, Position.Collumn);
            while (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
                if(Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.Range = p.Range - 1;
            }

            p.DefinePosition(Position.Range + 1, Position.Collumn);
            while (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.Range = p.Range +1;
            }

            p.DefinePosition(Position.Range, Position.Collumn - 1);
            while (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.Collumn = p.Collumn - 1;
            }

            p.DefinePosition(Position.Range, Position.Collumn + 1);
            while (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
                if (Board.Piece(p) != null && Board.Piece(p).Color != Color)
                {
                    break;
                }
                p.Collumn = p.Collumn + 1;
            }

            return mat;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
