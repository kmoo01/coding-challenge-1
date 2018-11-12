using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurveyShips
{
    /// <summary>
    /// Ship factory to deturmin instructions from known list, to be dependancy injected, used generic as could be indext by a key event for example
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IShipInstructionFactory<T>
    {
        /// <summary>
        /// returns an instance of the correct instuction, based on the instruction identifier
        /// </summary>
        /// <param name="instructionIdentifier"></param>
        /// <returns></returns>
        Instruction getInstruction(T instructionIdentifier);
    }

    /// <summary>
    /// Factory class inorder to move the responisbility of which algorithm to choose based on the char instruction, 
    /// hiding it behind the IShipInstructionFactory.
    /// NOTE ideally the configuration this would be depandancy injected, but i have hard coded this elsewhere for simplicity as a dictonary
    /// </summary>
 
    public class ShipInstructionFactory : IShipInstructionFactory<char>
    {
        private readonly IDictionary<char, Instruction> _instructions;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="instructions">char to concrete instruction disctonary</param>
        public ShipInstructionFactory(IDictionary<char, Instruction> instructions)
        {
            _instructions = instructions;
        }

        public Instruction getInstruction(char instructionIdentifier)
        {
            Instruction instruction;

            //for the given char, identify the instruction needed (with a instance that lasts the lifetime of the application)
            if (_instructions == null || !_instructions.TryGetValue(instructionIdentifier, out instruction))
            {
                throw new NotImplementedException(string.Format("There is no implemntation of Instruction for the given Instruction Identifier '{0}'", instructionIdentifier));
            }

            return instruction;
        }
    }
}
