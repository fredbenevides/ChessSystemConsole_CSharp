using System;
using GenericBoard.Entities.Enums;
using GenericBoard.Entities;

namespace Chess.Entities
{
    public class ChessPosition
    {
        public char collumn { get; set; }
        public int range { get; set; }

        public ChessPosition(char collumn, int range)
        {
            this.collumn = collumn;
            this.range = range;
        }

        public Position toPosition()
        {
            return new Position(8 - range, collumn - 'a');
        }

        public override string ToString()
        {
            return "" + collumn + range;
        }
    }
}
