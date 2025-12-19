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

        public RunningBrainProgram Instantiate()
        {
            return new RunningBrainProgram(this);
        }
    }
}