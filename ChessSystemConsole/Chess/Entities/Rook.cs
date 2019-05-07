using GenericBoard.Entities.Enums;
using GenericBoard.Entities;

namespace Chess.Entities
{
    public class Rook : Piece
    {
        public Rook(Board board, Color color) : base(color, board)
        {
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
