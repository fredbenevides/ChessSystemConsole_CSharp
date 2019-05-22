namespace GenericBoard.Entities
{
    public class Position
    {
        public int Range { get; set; }
        public int Collumn { get; set; }

        public Position()
        {
        }

        public Position(int range, int collumn)
        {
            Range = range;
            Collumn = collumn;
        }

        public void DefinePosition(int range, int collumn)
        {
            Range = range;
            Collumn = collumn;
        }

        public override string ToString()
        {
            return Range + ", " + Collumn;
        }
    }
}