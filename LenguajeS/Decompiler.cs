using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;

namespace LenguajeS
{
    public class Decompiler
    {

        public List<string> DecompileProgram(BigInteger code)
        {
            var instructionCodes = DecodeProgram(code);

            return GenerateInstructions(instructionCodes);
        }


        private List<string> GenerateInstructions(List<int> codes)
        {
            return codes.ConvertAll(t =>
            {
                DecodeInstruction(t, out int a, out int b, out int c);
                return GenerateInstruction(a, b, c);
            });
        }
        private List<int> DecodeProgram(BigInteger code)
        {
            code = code + 1;


            var factors = code.Factors().ToList();
                
            var logs = factors.ConvertAll(t =>
            {
                int i = 0;
                var temp = code;

                while (temp > 1 && t.Divides(temp))
                {
                    temp /= t;
                    i++;
                }

                return i;
            });


            return logs;
        }



        static void DecodeInstruction(int code, out int a, out int b, out int c)
        {
            code = code + 1;

            a = b = c = 0;

            while (code % 2 == 0)
            {
                a++;
                code = code / 2;
            }

            code = code - 1;
            code = code / 2;

            code = code + 1;

            while (code % 2 == 0)
            {
                b++;
                code = code / 2;
            }

            code = code - 1;
            code = code / 2;
            c = code;

        }

        static string GenerateInstruction(int a, int b, int c)
        {
            string? tag = GenerateTag(a);
            string variable = GenerateVariable(c);

            string instruction = "";

            switch (b)
            {
                case 0:
                    instruction = "{TAG}    {VAR} <- {VAR}";
                    break;
                case 1:
                    instruction = "{TAG}    {VAR} <- {VAR} + 1";
                    break;
                case 2:
                    instruction = "{TAG}    {VAR} <- {VAR} - 1";
                    break;
                default:
                    instruction = "{TAG}    IF {VAR} != 0 GOTO " + GenerateTag(b - 2) ?? "";
                    break;
            }

            instruction= instruction.Replace("{TAG}", tag ?? "  ");
            instruction = instruction.Replace("{VAR}", variable);

            return instruction;
        }

        private static string GenerateVariable(int b)
        {
            if (b == 0)
                return "Y ";

            if (b % 2 == 1)
            {
                return "X" + ((b / 2) +1);
            }

            return "Z" + b / 2;
        }

        static string? GenerateTag(int a)
        {
            if (a == 0)
                return null;

            string? tag = "";


            int quotient = Math.DivRem(a, 5, out int reminder);

            switch (reminder)
            {
                case 0: tag += "E"; break;
                case 1: tag += "A"; break;
                case 2: tag += "B"; break;
                case 3: tag += "C"; break;
                case 4: tag += "D"; break;
            }

            tag += quotient + 1;

            return tag;
        }

    }
}
