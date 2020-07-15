using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace LenguajeS
{
    public static class InstructionExtensions
    {
        public static BigInteger GedelsNumber(this List<Compiler.Instruction> instructions)
        {
            var primes = MathExtensions.GetFirstNPrimers(instructions.Count);

            BigInteger value = 1;

            for (int i = 0; i < primes.Count; i++)
            {
                value *= BigInteger.Pow(primes[i], instructions[i].Code);
            }

            return value-1;
        }
    }
    public class Compiler
    {
        public BigInteger ProgramCode { get; }
        public List<Instruction> Instructions { get; } = new List<Instruction>();
        public Compiler(List<string> instructions)
        {
            Instructions = instructions.ConvertAll(t => new Instruction(t));

            ProgramCode = Instructions.GedelsNumber();
        }

        public class Instruction
        {
            public ulong a { get; }
            public ulong b { get; }
            public ulong c { get; }

            public string InstructionString { get; }

            public int Code =>  (int) Pair(a,Pair(b,c));

            private const string INSTRUCTION_TYPE_EMPTY = @"^\s*(?<TAG>\[?[a-eA-E]\d+\]?)?\s*(?<INSTRUCTION>(?<VAR>(([yY])|([XxZz]\d+)))\s*<-\s*(?<VAR2>(([yY])|([XxZz]\d+))))\s*$";
            private const string INSTRUCTION_TYPE_INC = @"^\s*(?<TAG>\[?[a-eA-E]\d+\]?)?\s*(?<INSTRUCTION>(?<VAR>[yY]|[XxZz]\d+)\s*<-\s*(?<VAR2>[yY]|[XxZz]\d+)\s*\+\s*1\s*)\s*$";
            private const string INSTRUCTION_TYPE_DEC = @"^\s*(?<TAG>\[?[a-eA-E]\d+\]?)?\s*(?<INSTRUCTION>(?<VAR>[yY]|[XxZz]\d+)\s*<-\s*(?<VAR2>[yY]|[XxZz]\d+)\s*\-\s*1\s*)\s*$";
            private const string INSTRUCTION_TYPE_JUMP = @"^\s*(?<TAG>\[?[a-eA-E]\d+\]?)?\s*(?<INSTRUCTION>IF\s*(?<VAR1>[yY]|[XxZz]\d+)\s*!=\s*0\s*GOTO\s*(?<DEST>[a-eA-E]\d+))\s*$";


            private const string INSTRUCTION_TYPE_ASSIGN = @"^\s*(?<TAG>\[?[a-zA-Z]\d+\]?)?\s*(?<INSTRUCTION>(?<VAR1>[yY]|[XxZz]\d+)\s*<-\s*(?<VAR2>[yY]|[XxZz]\d+)\s*)\s*$";

            public Instruction(string instruction)
            {
                Match match;
                this.InstructionString = instruction;
                if ((match = Regex.Match(instruction, INSTRUCTION_TYPE_EMPTY)).Success)
                {
                    b = 0;
                }
                else if ((match = Regex.Match(instruction, INSTRUCTION_TYPE_INC)).Success)
                {
                    b = 1;
                }
                else if ((match = Regex.Match(instruction, INSTRUCTION_TYPE_DEC)).Success)
                {
                    b = 2;
                }
                else if ((match = Regex.Match(instruction, INSTRUCTION_TYPE_JUMP)).Success)
                {

                    var dest = match.Groups["DEST"];

                    if (dest != null && dest.Success)
                    {
                        b = (ulong) (2 + DecodeTag(dest.Value));
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                else
                {
                    throw new ArgumentException();
                }

                var tag = match.Groups["TAG"];

                if (tag != null && tag.Success)
                {
                    a = (ulong) DecodeTag(tag.Value);
                }
                else
                {
                    a = 0;
                }

                var variable = match.Groups["VAR"];

                if (variable != null && variable.Success)
                {
                    c = (ulong) DecodeVariable(variable.Value);
                }
                else
                {
                    throw new ArgumentException();
                }

            }

            private int DecodeTag(string tag)
            {
                tag = tag.Replace("[", "").Replace("]", "").ToUpper();

                char letter = tag[0];
                tag = tag.Replace(letter, '0');

                int reference = int.Parse(tag);

                return 1 + ((reference-1) * 5 + (int) letter - 'A');
            }

            private int DecodeVariable(string var)
            {
                var = var.ToUpper();
                char letter = var[0];
                var = var.Replace(letter, '0');

                int reference = int.Parse(var);

                if (letter == 'Y')
                    return 0;

                return letter == 'Z' ? 2 * reference : 2 * reference - 1;
            }

            private ulong Pair(ulong a, ulong b)
            {
                return (ulong) (Math.Pow(2, a) * (2 * b + 1) - 1);
            }

            
        }
    }
}
