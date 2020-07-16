using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace LenguajeS
{
    public static class Program
    {
        static void Main(string[] args)
        {

            if (args.Length > 0 && args[0].ToLowerInvariant() == "compile")
            {
                var uncompiledInstructions = File.ReadAllLines(args[1]).ToList();

                var compiler = new Compiler(uncompiledInstructions);


                Console.WriteLine("Program compiled to Number: " + compiler.ProgramCode);
                Console.WriteLine("The program was formed by {0} instructions");

            }
            else if (args.Length > 0 && args[0].ToLowerInvariant() == "decompile")
            {
                if (args.Length > 1 && BigInteger.TryParse(args[1], out BigInteger code))
                {
                    // code = BigInteger.Pow(2, 18) * BigInteger.Pow(3, 53) - 1;
                    var instructions = new Decompiler().DecompileProgram(code);
                    Console.WriteLine("Generating instructions for program number: " + code + "\n\n");
                    instructions.ForEach(Console.WriteLine);
                    Console.WriteLine("\n\n");

                    if (args.Length == 4 && args[2].ToLowerInvariant() == "save")
                    {
                        try
                        {
                            var fileLocation = args[3];
                            Console.WriteLine("Saving file to {0}", fileLocation);

                            instructions.Insert(0, $"Program Number: {code}");
                            instructions.Insert(1, "");
                            File.WriteAllLines(fileLocation, instructions);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Could't create File. Error: ", e.Message);
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Invalid code");
                }
            }
            else
            {
                Console.WriteLine("Invalid Argument");
                Console.WriteLine(
                    "Call LenguajeS compile Path/To/File to compile and generate the code for said program");
                Console.WriteLine(
                    "Call LenguajeS decompile ProgramNumber to generate the code of said program code");
            }


            Console.WriteLine("\n\n\n\nPress any key to exit...");
            Console.ReadKey(true);
        }
    }
}

