using System.Collections.Generic;
using JetBrains.Annotations;

namespace Brain
{
    public class Brain
    {
        [CanBeNull] public RunningBrainProgram runningInstance = null;

        public static CompiledBrainProgram ParseProgram(List<string> lines)
        {
            var functions = new Dictionary<string, CompiledBrainFunction>();
            var currentFunction = new List<BrainInstruction>();
            var currentJumpLabels = new Dictionary<string, int>();
            string currentFunctionName = null;
            var functionIndex = 0;
            foreach (string line in lines)
                if (line.StartsWith('.'))
                {
                    functionIndex = 0;
                    if (currentFunctionName is not null)
                    {
                        functions[currentFunctionName] = new CompiledBrainFunction(currentFunction, currentJumpLabels);
                        currentFunction = new List<BrainInstruction>();
                        currentJumpLabels = new Dictionary<string, int>();
                    }

                    currentFunctionName = line[1..];
                }
                else if (line.StartsWith(':'))
                {
                    if (currentFunctionName is not null)
                    {
                        string label = line[1..];
                        currentJumpLabels[label] = functionIndex - 1;
                    }
                }
                else if (currentFunctionName is not null)
                {
                    string[] split = line.Split(' ');
                    string instructionName = split[0];
                    BrainInstructionType instructionType = BrainInstructionRegistry.BY_NAME[instructionName];
                    var instruction = new BrainInstruction(instructionType.action, split[1..]);
                    currentFunction.Add(instruction);
                    functionIndex++;
                }

            if (currentFunctionName is not null) functions[currentFunctionName] = new CompiledBrainFunction(currentFunction, currentJumpLabels);
            return new CompiledBrainProgram(functions);
        }

        public void Instantiate(CompiledBrainProgram compiledProgram)
        {
            if (runningInstance is null) runningInstance = new RunningBrainProgram(compiledProgram);
        }
    }
}