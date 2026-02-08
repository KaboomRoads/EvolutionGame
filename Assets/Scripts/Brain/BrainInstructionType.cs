using System;

namespace Brain
{
    public class BrainInstructionType
    {
        public readonly string name;
        public readonly int cost;
        public readonly Func<bool> unlockCriteria;
        public readonly string description;
        public readonly Type[] requiredArgs;
        public readonly Type[] returnTypes;
        public readonly BrainInstructionAction action;

        public BrainInstructionType(string name, int cost, Func<bool> unlockCriteria, string description, Type[] requiredArgs, Type[] returnTypes, BrainInstructionAction action)
        {
            this.name = name;
            this.cost = cost;
            this.unlockCriteria = unlockCriteria;
            this.description = description;
            this.requiredArgs = requiredArgs;
            this.returnTypes = returnTypes;
            this.action = action;
        }
    }
}