using System;

namespace Brain
{
    public class BrainInstructionType
    {
        public readonly string name;
        public readonly string description;
        public readonly Type[] requiredArgs;
        public readonly Type[] returnTypes;
        public readonly BrainInstructionAction action;

        public BrainInstructionType(string name, string description, Type[] requiredArgs, Type[] returnTypes, BrainInstructionAction action)
        {
            this.name = name;
            this.description = description;
            this.requiredArgs = requiredArgs;
            this.returnTypes = returnTypes;
            this.action = action;
        }
    }
}