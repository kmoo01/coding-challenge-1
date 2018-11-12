using System;
using System.Collections.Generic;
using System.Linq;

using SurveyShips;
using System.IO;
namespace ExampleUse
{
    class Program
    {
        static void Main(string[] args)
        {
            //I would normally use dependancy injection for these, rather than hard coded 

            /*read and parse input*/
            string filein = "input.txt";
            string fileout = "output.txt";
            if (!File.Exists(filein))
            {
                return;
            }
            
            var inputfile = new SpaceDelimitedCharInstructionGridCommandPairInput();
            var parsedInput = inputfile.ParseSurveyInput(File.ReadAllText(filein));

            /*execute the commands*/

            //instantiate the factory, again normally done via config
            var dictonary = new Dictionary<char, Instruction> { 
             {(char)InstructionIdentifier.Left, new LeftInstruction(parsedInput.TopRightCoordinate, inputfile.BottomLeftCoordinate)},
             {(char)InstructionIdentifier.Right, new RightInstruction(parsedInput.TopRightCoordinate, inputfile.BottomLeftCoordinate)},
             {(char)InstructionIdentifier.Forward, new ForwardInstruction(1, parsedInput.TopRightCoordinate, inputfile.BottomLeftCoordinate)}};
            
            var factory = new ShipInstructionFactory(dictonary);


            //I would have also abstracted this out a little more so the inplementation isnt all in client (here)
            foreach (var command in parsedInput.Commands)
            { 
                foreach(var instrutionIdentifier in command.Instructions)
                {
                    var instruction = factory.getInstruction(instrutionIdentifier);
                    
                    //if lost already, dont preceed
                    if (!command.Position.Lost)
                    {
                        instruction.Move(command.Position);
                    }
                }
            }

            /*write file*/
            File.WriteAllText(fileout, String.Empty);
            using (StreamWriter w = File.AppendText(fileout))
            {
                foreach (var command in parsedInput.Commands)
                {
                    Console.WriteLine(command.Position.ToString());
                    w.WriteLine(command.Position.ToString());
                }
            }
        }
    }
}
