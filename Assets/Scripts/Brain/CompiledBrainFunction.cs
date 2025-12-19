using System.Collections.Generic;
using JetBrains.Annotations;

namespace Brain
{
    public class CompiledBrainFunction
    {
        public readonly List<BrainInstruction> commands;
        public readonly Dictionary<string, int> jumpLabels;

        public CompiledBrainFunction(List<BrainInstruction> commands, Dictionary<string, int> jumpLabels)
        {
            this.commands = commands;
            this.jumpLabels = jumpLabels;
        }

        public RunningBrainFunction Instantiate(RunningBrainProgram runningProgram, [CanBeNull] BrainReturnPoint returnPointer)
        {
            return new RunningBrainFunction(runningProgram, this, returnPointer);
        }
    }
}