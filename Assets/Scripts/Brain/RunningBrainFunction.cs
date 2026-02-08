using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Brain
{
    public class RunningBrainFunction
    {
        public readonly RunningBrainProgram runningProgram;
        public readonly CompiledBrainFunction compiledFunction;
        [CanBeNull] public readonly BrainReturnPoint returnPointer;
        public readonly List<object> locals = new();
        public int instructionIndex = 0;

        public RunningBrainFunction(RunningBrainProgram runningProgram, CompiledBrainFunction compiledFunction, [CanBeNull] BrainReturnPoint returnPointer)
        {
            this.runningProgram = runningProgram;
            this.compiledFunction = compiledFunction;
            this.returnPointer = returnPointer;
        }

        public void Step()
        {
            if (instructionIndex >= compiledFunction.commands.Count)
            {
                BrainInstructionRegistry.RET.action.Invoke(runningProgram, locals, Array.Empty<string>(), returnPointer);
                return;
            }

            BrainInstruction instruction = compiledFunction.commands[instructionIndex];
            instruction.action.Invoke(runningProgram, locals, instruction.args, returnPointer);
            instructionIndex++;
        }
    }
}