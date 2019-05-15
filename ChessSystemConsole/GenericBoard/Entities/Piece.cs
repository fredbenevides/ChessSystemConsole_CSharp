using GenericBoard.Entities.Enums;

namespace GenericBoard.Entities
{
    public abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityOfMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece()
        {
        }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            QuantityOfMoves = 0;
        }

        public void IncreaseQuantityOfMoves()
        {
            QuantityOfMoves++;
        }

        public bool ThereIsAPossibleMove()
        {
            bool[,] mat = PossibleTargetPositions();
            for(int i = 0; i < Board.Ranges; i++)
            {
                for(int j = 0; j < Board.Collumns; j++)
                {
                    if(mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanMoveTo(Position position)
        {
            return PossibleTargetPositions()[position.Range, position.Collumn];
        }

        public abstract bool[,] PossibleTargetPositions();
    }
}
