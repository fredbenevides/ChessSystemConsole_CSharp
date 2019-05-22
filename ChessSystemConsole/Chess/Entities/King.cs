using GenericBoard.Entities.Enums;
using GenericBoard.Entities;

namespace Chess.Entities
{
    public class King : Piece
    {

        ChessMatch Match;

        public King(Board board, Color color, ChessMatch match) : base(color, board)
        {
            Match = match;
        }

        private bool CanMoveTo(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece == null || piece.Color != Color;
        }

        private bool TestRook(Position position)
        {
            Piece piece = Board.Piece(position);
            return piece is Rook && piece.QuantityOfMoves == 0;
        }

        private bool FreePosition(Position position)
        {
            return Board.Piece(position) == null;
        }

        public override bool[,] PossibleTargetPositions()
        {
            bool[,] mat = new bool[Board.Ranges, Board.Collumns];
            Position p = new Position(0, 0);

            p.DefinePosition(Position.Range - 1, Position.Collumn - 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range - 1, Position.Collumn);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range - 1, Position.Collumn + 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range, Position.Collumn - 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range, Position.Collumn + 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range + 1, Position.Collumn - 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range + 1, Position.Collumn);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            p.DefinePosition(Position.Range + 1, Position.Collumn + 1);
            if (Board.ValidPosition(p) && CanMoveTo(p))
            {
                mat[p.Range, p.Collumn] = true;
            }

            if (QuantityOfMoves == 0 && !Match.Check)
            {
                Position rookPosition = new Position(Position.Range, Position.Collumn + 3);
                if (TestRook(rookPosition))
                {
                    if (FreePosition(new Position(Position.Range, Position.Collumn + 1))
                         && FreePosition(new Position(Position.Range, Position.Collumn + 2)))
                    {
                        mat[Position.Range, Position.Collumn + 2] = true;
                    }
                }
                rookPosition = new Position(Position.Range, Position.Collumn - 4);
                if (TestRook(rookPosition))
                {
                    if (FreePosition(new Position(Position.Range, Position.Collumn - 1))
                         && FreePosition(new Position(Position.Range, Position.Collumn - 2))
                          && FreePosition(new Position(Position.Range, Position.Collumn - 3)))
                    {
                        mat[Position.Range, Position.Collumn - 2] = true;
                    }
                }
            }

            return mat;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
