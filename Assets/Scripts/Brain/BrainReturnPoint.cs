namespace Brain
{
    public class BrainReturnPoint
    {
        public readonly RunningBrainFunction function;
        public readonly int index;

        public BrainReturnPoint(RunningBrainFunction function, int index)
        {
            this.function = function;
            this.index = index;
        }
    }
}