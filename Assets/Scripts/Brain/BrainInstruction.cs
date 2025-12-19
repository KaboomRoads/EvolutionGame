namespace Brain
{
    public class BrainInstruction
    {
        public readonly BrainInstructionAction action;
        public readonly string[] args;

        public BrainInstruction(BrainInstructionAction action, string[] args)
        {
            this.action = action;
            this.args = args;
        }
    }
}