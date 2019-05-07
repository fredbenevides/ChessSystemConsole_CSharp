namespace Board
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
    }
}
