using System.Collections.Generic;
using JetBrains.Annotations;

namespace Brain
{
    public delegate void BrainInstructionAction(RunningBrainProgram runningProgram, List<object> locals, string[] args, [CanBeNull] BrainReturnPoint returnPointer);
}