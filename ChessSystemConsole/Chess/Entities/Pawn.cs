using GenericBoard.Entities.Enums;
using GenericBoard.Entities;

namespace Chess.Entities
{
    public class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(color, board)
        {
        }

        private bool ThereIsFoe(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece != null && piece.Color != Color;
        }

        private bool FreePosition(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleTargetPositions()
        {
            bool[,] mat = new bool[Board.Ranges, Board.Collumns];
            Position p = new Position(0, 0);

            if (Color == Color.White)
            {
                p.DefinePosition(Position.Range - 1, Position.Collumn);
                if (Board.ValidPosition(p) && FreePosition(p))
                {
                    mat[p.Range, p.Collumn] = true;
                }
                p.DefinePosition(Position.Range - 2, Position.Collumn);
                if (Board.ValidPosition(p) && FreePosition(p) && FreePosition(new Position(p.Range + 1, p.Collumn)) && QuantityOfMoves == 0)
                {
                    mat[p.Range, p.Collumn] = true;
                }
                p.DefinePosition(Position.Range - 1, Position.Collumn - 1);
                if (Board.ValidPosition(p) && ThereIsFoe(p))
                {
                    mat[p.Range, p.Collumn] = true;
                }
                p.DefinePosition(Position.Range - 1, Position.Collumn + 1);
                if (Board.ValidPosition(p) && ThereIsFoe(p))
                {
                    mat[p.Range, p.Collumn] = true;
                }
            }
            else
            {
                p.DefinePosition(Position.Range + 1, Position.Collumn);
                if (Board.ValidPosition(p) && FreePosition(p))
                {
                    mat[p.Range, p.Collumn] = true;
                }
                p.DefinePosition(Position.Range + 2, Position.Collumn);
                if (Board.ValidPosition(p) && FreePosition(p) && FreePosition(new Position(p.Range - 1, p.Collumn)) && QuantityOfMoves == 0)
                {
                    mat[p.Range, p.Collumn] = true;
                }
                p.DefinePosition(Position.Range + 1, Position.Collumn - 1);
                if (Board.ValidPosition(p) && ThereIsFoe(p))
                {
                    mat[p.Range, p.Collumn] = true;
                }
                p.DefinePosition(Position.Range + 1, Position.Collumn + 1);
                if (Board.ValidPosition(p) && ThereIsFoe(p))
                {
                    mat[p.Range, p.Collumn] = true;
                }
            }
            return mat;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
