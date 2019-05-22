using GenericBoard.Entities;

namespace Chess.Entities
{
    public class ChessPosition
    {
        public char Collumn { get; set; }
        public int Range { get; set; }

        public ChessPosition(char collumn, int range)
        {
            Collumn = collumn;
            Range = range;
        }

        public Position ToPosition()
        {
            return new Position(8 - Range, Collumn - 'a');
        }

        public override string ToString()
        {
            return "" + Collumn + Range;
        }
    }
}
