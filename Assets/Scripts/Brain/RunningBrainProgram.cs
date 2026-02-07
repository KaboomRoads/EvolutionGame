using System.Collections.Generic;
using JetBrains.Annotations;

namespace Brain
{
    public class RunningBrainProgram
    {
        public readonly CompiledBrainProgram compiledProgram;
        public readonly Dictionary<string, object> fields = new();
        public readonly Stack<object> stack = new();
        [CanBeNull] public RunningBrainFunction currentFunction;

        public RunningBrainProgram(CompiledBrainProgram compiledProgram)
        {
            this.compiledProgram = compiledProgram;
            currentFunction = compiledProgram.functions["main()V"].Instantiate(this, null);
        }

        public void Step()
        {
            currentFunction!.Step();
        }
    }
}