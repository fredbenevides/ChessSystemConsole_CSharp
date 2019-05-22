using GenericBoard.Entities.Enums;
using GenericBoard.Entities;

namespace Chess.Entities
{
    public class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match) : base(color, board)
        {
            this.match = match;
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
            Position left = new Position(Position.Range, Position.Collumn - 1);
            Position right = new Position(Position.Range, Position.Collumn + 1);

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
                if (Position.Range == 3)
                {
                    if (Board.ValidPosition(left) && ThereIsFoe(left) && Board.Piece(left) == match.VulnerableEnPassant)
                    {
                        mat[Position.Range - 1, Position.Collumn - 1] = true;
                    }
                    if (Board.ValidPosition(right) && ThereIsFoe(right) && Board.Piece(right) == match.VulnerableEnPassant)
                    {
                        mat[Position.Range - 1, Position.Collumn + 1] = true;
                    }
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
                if (Position.Range == 4)
                {
                    if (Board.ValidPosition(left) && ThereIsFoe(left) && Board.Piece(left) == match.VulnerableEnPassant)
                    {
                        mat[Position.Range + 1, Position.Collumn - 1] = true;
                    }
                    if (Board.ValidPosition(right) && ThereIsFoe(right) && Board.Piece(right) == match.VulnerableEnPassant)
                    {
                        mat[Position.Range + 1, Position.Collumn + 1] = true;
                    }
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
