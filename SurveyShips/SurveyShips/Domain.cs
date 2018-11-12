using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SurveyShips
{
    /// <summary>
    /// Represents the survey input from which ever souce
    /// </summary>
    public class SurveyInput<T>
    {
        //the maximum value for any coordinate is 50. 
        private const int MaxCorordinate = 50;

        private Coordinate _topRightCoordinate;

        /// <summary>
        /// Ship commands to place, orientate and move ship
        /// </summary>
        public IList<ShipCommand<T>> Commands { get; set; }

        /// <summary>
        /// The top right coordinate property
        /// </summary>
        public Coordinate TopRightCoordinate
        {
            get
            {
                return _topRightCoordinate;
            }
            set
            {
                //validate the corordinate based on the max 
                if (value.X > MaxCorordinate || value.Y > MaxCorordinate)
                {
                    throw new FormatException(string.Format("Cannot set Survey Inputs top right parameter, coordinate too large (max = {0})", MaxCorordinate));
                }

                _topRightCoordinate = value;
            }
        }
    }

    /// <summary>
    /// Coordinates
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// constructor from string array reperesentations [x,y]
        /// </summary>
        /// <param name="coordinates"></param>
        public Coordinate(string[] coordinates)
        {
            //attempt to parse string coordinate, falure to parse throws exception
            if (coordinates.Length >= 2)
            {
                int parsedValue = 0;
                if (int.TryParse(coordinates[0], out parsedValue) == true)
                {
                    X = parsedValue;
                }
                else
                {
                    throw new FormatException(string.Format("Cannot set corordinate, x position cannot be read '{0}')", coordinates[0]));
                }

                if (int.TryParse(coordinates[1], out parsedValue) == true)
                {
                    Y = parsedValue;
                }
                else
                {
                    throw new FormatException(string.Format("Cannot set corordinate, y position cannot be read '{0}')", coordinates[1]));
                }
            }
            else
            {
                throw new FormatException("Cannot set corordinate, invalid length");
            }
        }

        public int X { get; set; }
        public int Y { get; set; }

    }

    /// <summary>
    /// Compass orientation
    /// </summary>
    public enum Orientation
    {
        North = 'N',
        South = 'S',
        East = 'E',
        West = 'W',
    }

    /// <summary>
    /// Instruction identifier
    /// </summary>
    public enum InstructionIdentifier
    {
        Left = 'L',
        Right = 'R',
        Forward = 'F',
    }

    /// <summary>
    /// Ship posiotion reprsents the corordinate of the ship and the orientation
    /// </summary>
    public class ShipPosition : Coordinate
    {
        public ShipPosition(int x, int y, Orientation orintation)
            : base(x, y)
        {
            Orientation = orintation;
        }

        public ShipPosition(string[] position)
            : base(position)
        {

            //attempt to parse string coordinate, falure to parse throws exception
            if (position.Length >= 3 && position[2].Length == 1)
            {
                //the 3rd variable has already been vlidated, so safe to parse to char
                char identifier = char.Parse(position[2].ToUpper());

                //if the char is found in the list, we use the enum out value
                if (Enum.IsDefined(typeof(Orientation), (int)identifier))
                {
                    Orientation = (Orientation)identifier;
                }
                else
                {
                    throw new FormatException(string.Format("Cannot set ship position, Orientation invaid '{0}')", position[2]));
                }
            }
            else
            {
                throw new FormatException("Cannot set ship position in incorrect format");
            }
        }

        public Orientation Orientation { get; set; }
        public bool Lost { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", X, Y, (char)Orientation, Lost ? "LOST" : "");
        }
    }

    /// <summary>
    /// Command represents a combination of the ships starting positionand Instuctions to move
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ShipCommand<T>
    {
        //the maximum value for any instuction list is 100. 
        private const int MaxLen = 100;
        private const int MinLen = 1;
        private T[] _instructions;

        //ships starting position
        public ShipPosition Position { get; set; }

        /// <summary>
        /// list of instruction
        /// </summary>
        public T[] Instructions
        {
            get
            {
                return _instructions;
            }
            set
            {
                //validate the corordinate based on the max 
                if (value.Length > MaxLen || value.Length < MinLen)
                {
                    throw new FormatException(string.Format("Cannot set ship commands, out of range (min = {0}, max = {0})", MinLen, MaxLen));
                }

                _instructions = value;
            }
        }
    }
}





