using System.Collections.Generic;
using JetBrains.Annotations;

namespace Brain
{
    public class RunningBrainProgram
    {
        public readonly ControlledProjectile self;
        public readonly CompiledBrainProgram compiledProgram;
        public readonly Dictionary<string, object> fields = new();
        public readonly Stack<object> stack = new();
        [CanBeNull] public RunningBrainFunction currentFunction;

        public RunningBrainProgram(CompiledBrainProgram compiledProgram, ControlledProjectile self)
        {
            this.compiledProgram = compiledProgram;
            this.self = self;
            currentFunction = GetInit();
        }

        public RunningBrainFunction GetMain()
        {
            return compiledProgram.functions["loop"].Instantiate(this, null);
        }

        public RunningBrainFunction GetInit()
        {
            return compiledProgram.functions["init"].Instantiate(this, null);
        }

        public void Step()
        {
            currentFunction!.Step();
        }
    }
}