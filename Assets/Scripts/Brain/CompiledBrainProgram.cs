using System.Collections.Generic;

namespace Brain
{
    public class CompiledBrainProgram
    {
        public readonly Dictionary<string, CompiledBrainFunction> functions;

        public CompiledBrainProgram(Dictionary<string, CompiledBrainFunction> functions)
        {
            this.functions = functions;
        }

        public RunningBrainProgram Instantiate(ControlledProjectile self)
        {
            return new RunningBrainProgram(this, self);
        }
    }
}