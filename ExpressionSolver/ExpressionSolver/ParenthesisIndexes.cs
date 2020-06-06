namespace ExpressionSolver
{
    public class ParenthesisIndexes
    {
        public int IndexStart { get; set; }
        public int IndexEnd { get; set; }

        public ParenthesisIndexes(int indexStart, int indexEnd)
        {
            IndexStart = indexStart;
            IndexEnd = indexEnd;
        }
    }
}
