using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Brain
{
    public static class BrainInstructionRegistry
    {
        public static readonly SortedDictionary<string, BrainInstructionType> BY_NAME = new();

        public static readonly BrainInstructionType PUSH_EMPTY = Register("push.empty", 1, () => true, "Pushes an empty value to the stack", Type.EmptyTypes, new[] { typeof(object) }, (runningProgram, locals, args, returnPointer) => { runningProgram.stack.Push(null); });

        public static readonly BrainInstructionType PUSH_INT = Register("push.int", 1, () => true, "Pushes an integer to the stack", Type.EmptyTypes, new[] { typeof(int) }, (runningProgram, locals, args, returnPointer) =>
        {
            if (args.Length > 0 && int.TryParse(args[0], out int i)) runningProgram.stack.Push(i);
        });

        public static readonly BrainInstructionType CONV_INT = Register("conv.int", 1, () => true, "Converts a value to an integer", new[] { typeof(object) }, new[] { typeof(int) }, (runningProgram, locals, args, returnPointer) => { runningProgram.stack.Push(Convert.ToInt32(runningProgram.stack.Pop())); });

        public static readonly BrainInstructionType PUSH_DEC = Register("push.dec", 1, () => true, "Pushes a float to the stack", Type.EmptyTypes, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) =>
        {
            if (args.Length > 0 && float.TryParse(args[0], out float f)) runningProgram.stack.Push(f);
        });

        public static readonly BrainInstructionType CONV_DEC = Register("conv.dec", 1, () => true, "Converts a value to a float", new[] { typeof(object) }, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) => { runningProgram.stack.Push(Convert.ToSingle(runningProgram.stack.Pop())); });

        public static readonly BrainInstructionType PUSH_BOOL = Register("push.bool", 1, () => true, "Pushes a boolean to the stack", Type.EmptyTypes, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            if (args.Length > 0 && bool.TryParse(args[0], out bool b)) runningProgram.stack.Push(b);
        });

        public static readonly BrainInstructionType ADD_INT = Register("add.int", 1, () => true, "Adds 2 integers", new[] { typeof(int), typeof(int) }, new[] { typeof(int) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 + i2);
        });

        public static readonly BrainInstructionType ADD_DEC = Register("add.dec", 1, () => true, "Adds 2 floats", new[] { typeof(float), typeof(float) }, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 + f2);
        });

        public static readonly BrainInstructionType GR_INT = Register("gr.int", 1, () => true, "Compares 2 integers using \"greater than\"", new[] { typeof(int), typeof(int) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 > i2);
        });

        public static readonly BrainInstructionType GR_DEC = Register("gr.dec", 1, () => true, "Adds 2 floats using \"greater than\"", new[] { typeof(float), typeof(float) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 > f2);
        });

        public static readonly BrainInstructionType GREQ_INT = Register("greq.int", 1, () => true, "Compares 2 integers using \"greater or equal to\"", new[] { typeof(int), typeof(int) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 >= i2);
        });

        public static readonly BrainInstructionType GREQ_DEC = Register("greq.dec", 1, () => true, "Adds 2 floats using \"greater or equal to\"", new[] { typeof(float), typeof(float) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 >= f2);
        });

        public static readonly BrainInstructionType LS_INT = Register("ls.int", 1, () => true, "Compares 2 integers using \"lesser than\"", new[] { typeof(int), typeof(int) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 < i2);
        });

        public static readonly BrainInstructionType LS_DEC = Register("ls.dec", 1, () => true, "Adds 2 floats using \"lesser than\"", new[] { typeof(float), typeof(float) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 < f2);
        });

        public static readonly BrainInstructionType LSEQ_INT = Register("lseq.int", 1, () => true, "Compares 2 integers using \"lesser or equal to\"", new[] { typeof(int), typeof(int) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 <= i2);
        });

        public static readonly BrainInstructionType LSEQ_DEC = Register("lseq.dec", 1, () => true, "Adds 2 floats using \"lesser or equal to\"", new[] { typeof(float), typeof(float) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 <= f2);
        });

        public static readonly BrainInstructionType SUB_INT = Register("sub.int", 1, () => true, "Subtracts 2 integers", new[] { typeof(int), typeof(int) }, new[] { typeof(int) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 - i2);
        });

        public static readonly BrainInstructionType SUB_DEC = Register("sub.dec", 1, () => true, "Subtracts 2 floats", new[] { typeof(float), typeof(float) }, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 - f2);
        });

        public static readonly BrainInstructionType MUL_INT = Register("mul.int", 1, () => true, "Multiplies 2 integers", new[] { typeof(int), typeof(int) }, new[] { typeof(int) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 * i2);
        });

        public static readonly BrainInstructionType MUL_DEC = Register("mul.dec", 1, () => true, "Multiplies 2 floats", new[] { typeof(float), typeof(float) }, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 * f2);
        });

        public static readonly BrainInstructionType DIV_INT = Register("div.int", 1, () => true, "Divides 2 integers", new[] { typeof(int), typeof(int) }, new[] { typeof(int) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 / i2);
        });

        public static readonly BrainInstructionType DIV_DEC = Register("div.dec", 1, () => true, "Divides 2 floats", new[] { typeof(float), typeof(float) }, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 / f2);
        });

        public static readonly BrainInstructionType REM_INT = Register("rem.int", 1, () => true, "Gets the remainder of 2 divided integers", new[] { typeof(int), typeof(int) }, new[] { typeof(int) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push(i1 % i2);
        });

        public static readonly BrainInstructionType REM_DEC = Register("rem.dec", 1, () => true, "Gets the remainder of 2 divided floats", new[] { typeof(float), typeof(float) }, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push(f1 % f2);
        });

        public static readonly BrainInstructionType POW_INT = Register("pow.int", 1, () => true, "Gets the power of 2 ints", new[] { typeof(int), typeof(int) }, new[] { typeof(int) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int i2 && stack.Pop() is int i1) stack.Push((int)Math.Pow(i1, i2));
        });

        public static readonly BrainInstructionType POW_DEC = Register("pow.dec", 1, () => true, "Gets the power of 2 floats", new[] { typeof(float), typeof(float) }, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float f2 && stack.Pop() is float f1) stack.Push((float)Math.Pow(f1, f2));
        });

        public static readonly BrainInstructionType BOOL_NOT = Register("bool.not", 1, () => true, "Negates a boolean", new[] { typeof(bool), typeof(bool) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is bool b) stack.Push(!b);
        });

        public static readonly BrainInstructionType BOOL_AND = Register("bool.and", 1, () => true, "Compares 2 booleans using \"and\"", new[] { typeof(bool), typeof(bool) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is bool b2 && stack.Pop() is bool b1) stack.Push(b1 && b2);
        });

        public static readonly BrainInstructionType BOOL_OR = Register("bool.or", 1, () => true, "Compares 2 booleans using \"or\"", new[] { typeof(bool), typeof(bool) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is bool b2 && stack.Pop() is bool b1) stack.Push(b1 || b2);
        });

        public static readonly BrainInstructionType EQ_BOOL = Register("eq.bool", 1, () => true, "Compares 2 booleans using \"equals\"", new[] { typeof(bool), typeof(bool) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is bool b2 && stack.Pop() is bool b1) stack.Push(b1 == b2);
        });

        public static readonly BrainInstructionType EQ_INT = Register("eq.int", 1, () => true, "Compares 2 integers using \"equals\"", new[] { typeof(int), typeof(int) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is int b2 && stack.Pop() is int b1) stack.Push(b1 == b2);
        });

        public static readonly BrainInstructionType EQ_DEC = Register("eq.dec", 1, () => true, "Compares 2 floats using \"equals\"", new[] { typeof(float), typeof(float) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (stack.Pop() is float b2 && stack.Pop() is float b1) stack.Push(Mathf.Approximately(b1, b2));
        });

        public static readonly BrainInstructionType IS_EMPTY = Register("is.empty", 1, () => true, "Checks if a value is empty", new[] { typeof(object) }, new[] { typeof(bool) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            stack.Push(stack.Pop() is null);
        });

        public static readonly BrainInstructionType STLOC = Register("stloc", 1, () => true, "Stores a value in a local variable at the specified index", new[] { typeof(object), typeof(int) }, Type.EmptyTypes, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            object value = stack.Pop();
            if (args.Length > 0 && int.TryParse(args[0], out int i)) locals[i] = value;
        });

        public static readonly BrainInstructionType LDLOC = Register("ldloc", 1, () => true, "Loads a value from a local variable at the specified index", new[] { typeof(int) }, new[] { typeof(object) }, (runningProgram, locals, args, returnPointer) =>
        {
            if (args.Length > 0 && int.TryParse(args[0], out int i)) runningProgram.stack.Push(locals[i]);
        });

        public static readonly BrainInstructionType STFLD = Register("stfld", 1, () => true, "Stores a value in a global field with the specified name", new[] { typeof(object), typeof(string) }, Type.EmptyTypes, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            object value = stack.Pop();
            if (args.Length > 0 && args[0] is { } s) runningProgram.fields[s] = value;
        });

        public static readonly BrainInstructionType LDFLD = Register("ldfld", 1, () => true, "Loads a value from a global field with the specified name", new[] { typeof(string) }, new[] { typeof(object) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            if (args.Length > 0 && args[0] is { } s) stack.Push(runningProgram.fields[s]);
        });

        public static readonly BrainInstructionType RET = Register("ret", 0, () => true, "Returns to the source of the current function call", Type.EmptyTypes, Type.EmptyTypes, (runningProgram, locals, args, returnPointer) =>
        {
            if (returnPointer is not null)
                runningProgram.currentFunction = new RunningBrainFunction(runningProgram, returnPointer.function.compiledFunction, returnPointer.function.returnPointer)
                {
                    instructionIndex = returnPointer.index + 1
                };
            else runningProgram.currentFunction = null;
        });

        public static readonly BrainInstructionType JMP = Register("jmp", 1, () => true, "Moves the instruction cursor of the current function to the specified label", Type.EmptyTypes, Type.EmptyTypes, (runningProgram, locals, args, returnPointer) =>
        {
            if (args.Length > 0 && args[0] is { } s) runningProgram.currentFunction!.instructionIndex = runningProgram.currentFunction!.compiledFunction.jumpLabels[s];
        });

        public static readonly BrainInstructionType JMP_IF = Register("jmp.if", 1, () => true, "Moves the instruction cursor of the current function to the specified label if the top element of the stack is true", Type.EmptyTypes, Type.EmptyTypes, (runningProgram, locals, args, returnPointer) =>
        {
            if (runningProgram.stack.Pop() is true && args.Length > 0 && args[0] is { } s) runningProgram.currentFunction!.instructionIndex = runningProgram.currentFunction!.compiledFunction.jumpLabels[s];
        });

        public static readonly BrainInstructionType JMP_UNL = Register("jmp.unl", 1, () => true, "Moves the instruction cursor of the current function to the specified label if the top element of the stack is false", Type.EmptyTypes, Type.EmptyTypes, (runningProgram, locals, args, returnPointer) =>
        {
            if (runningProgram.stack.Pop() is false && args.Length > 0 && args[0] is { } s) runningProgram.currentFunction!.instructionIndex = runningProgram.currentFunction!.compiledFunction.jumpLabels[s];
        });

        public static readonly BrainInstructionType CALL = Register("call", 2, () => true, "Calls the specified function", Type.EmptyTypes, Type.EmptyTypes, (runningProgram, locals, args, returnPointer) =>
        {
            if (args.Length > 0 && args[0] is { } s) runningProgram.currentFunction = new RunningBrainFunction(runningProgram, runningProgram.compiledProgram.functions[s], new BrainReturnPoint(runningProgram.currentFunction!, runningProgram.currentFunction!.instructionIndex));
        });

        public static readonly BrainInstructionType POP = Register("pop", 1, () => true, "Pops the top element of the stack", Type.EmptyTypes, Type.EmptyTypes, (runningProgram, locals, args, returnPointer) => { runningProgram.stack.Pop(); });

        public static readonly BrainInstructionType LIMB = Register("limb", 1, () => true, "Gets a limb from the lizard", Type.EmptyTypes, new[] { typeof(BodyPart) }, (runningProgram, locals, args, returnPointer) =>
        {
            if (args.Length > 0 && args[0] is { } s)
            {
                var stack = runningProgram.stack;
                var evolutionData = EvolutionData.instance;
                stack.Push(evolutionData.LimbFromName(s));
            }
        });

        public static readonly BrainInstructionType ECHO = Register("echo", 3, () => EvolutionData.instance.AnyEcho(), "Pushes the distance of the closest food or an empty result to the stack", new[] { typeof(BodyPart) }, new[] { typeof(float) }, (runningProgram, locals, args, returnPointer) =>
        {
            var stack = runningProgram.stack;
            var evolutionData = EvolutionData.instance;
            var found = Object.FindObjectsByType<Food>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            float closestDist = found.Select(food => Vector2.Distance(food.transform.position, evolutionData.transform.position)).Prepend(float.MaxValue).Min();
            if (closestDist is not float.MaxValue) stack.Push(closestDist);
            else stack.Push(null);
        });

        public static BrainInstructionType Register(string name, int cost, Func<bool> unlockCriteria, string description, Type[] requiredArgs, Type[] returnTypes, BrainInstructionAction instructionAction)
        {
            var type = new BrainInstructionType(name, cost, unlockCriteria, description, requiredArgs, returnTypes, instructionAction);
            BY_NAME[name] = type;
            return type;
        }
    }
}