using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SurveyShips
{

    /// <summary>
    /// Abstract class to represent a movement instruction
    /// NOTE: made abstract as new instructions should be able to be extended, not ammends (s[0]olid)
    /// each subclass contains implementation of the proper algorytm according to the the identifier.
    /// As each of the instructionscould be differnt, we used different statagies to implement ... i.e. the strategy pattern
    /// </summary>
    public abstract class Instruction
    {
        protected readonly Coordinate TopRight;
        protected readonly Coordinate BottomLeft;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="topRight">the confugured top right coordinates</param>
        /// <param name="bottomLeft">the confugured bottom left coordinates</param>
        public Instruction(Coordinate topRight, Coordinate bottomLeft)
        {
            TopRight = topRight;
            BottomLeft = bottomLeft;
        }

        //for a given position, move to new position/orientation
        public abstract ShipPosition Move(ShipPosition position);

        /// <summary>
        /// WillBeLost : Validates whether the instruction can be executed on the ships current position
        /// </summary>
        /// <param name="position">current position of the ship</param>
        /// <returns></returns>
        public virtual bool WillBeLost(ShipPosition position)
        {
            return false;
        }
    }

    /// <summary>
    /// LeftInstruction the ship turns left 90 degrees and remains on the current grid point.
    /// </summary>
    public class LeftInstruction : Instruction
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="topRight">the confugured top right coordinates</param>
        /// <param name="bottomLeft">the confugured bottom left coordinates</param>
        public LeftInstruction(Coordinate topRight, Coordinate bottomLeft)
            : base(topRight, bottomLeft)
        {
        }

        public override ShipPosition Move(ShipPosition position)
        {
            if (position.Lost)
            {
                return position;
            }

            switch (position.Orientation)
            {
                case Orientation.East:
                    position.Orientation = Orientation.North;
                    break;
                case Orientation.North:
                    position.Orientation = Orientation.West;
                    break;
                case Orientation.West:
                    position.Orientation = Orientation.South;
                    break;
                case Orientation.South:
                    position.Orientation = Orientation.East;
                    break;
                default:
                    throw new NotImplementedException(string.Format("Ship's orientation {0} is not supported for the Left Instruction", position.Orientation.ToString()));
            }

            return position;
        }
    }

    /// <summary>
    /// RightInstruction the ship turns right 90 degrees and remains on the current grid point.
    /// </summary>
    public class RightInstruction : Instruction
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="topRight">the confugured top right coordinates</param>
        /// <param name="bottomLeft">the confugured bottom left coordinates</param>
        public RightInstruction(Coordinate topRight, Coordinate bottomLeft)
            : base(topRight, bottomLeft)
        {
        }

        public override ShipPosition Move(ShipPosition position)
        {
            if (position.Lost)
            {
                return position;
            }

            switch (position.Orientation)
            {
                case Orientation.East:
                    position.Orientation = Orientation.South;
                    break;
                case Orientation.North:
                    position.Orientation = Orientation.East;
                    break;
                case Orientation.West:
                    position.Orientation = Orientation.North;
                    break;
                case Orientation.South:
                    position.Orientation = Orientation.West;
                    break;
                default:
                    throw new NotImplementedException(string.Format("Ship's orientation {0} is not supported for the Right Instruction", position.Orientation.ToString()));
            }

            return position;
        }
    }

    /// <summary>
    /// ForwardInstruction the ship moves forward one grid point in the direction of the current orientation 
    /// maintains the same orientation. The direction North corresponds to the direction from grid point (x, y) 
    /// to grid point (x, y+1) and the direction east corresponds to the direction from grid point (x, y) to grid point (x+1, y).
    /// </summary>
    public class ForwardInstruction : Instruction
    {
        //the number of positions to move on each instruction
        private readonly int MovePositions;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="movePositions">the number of poistions to move on each execution</param>
        /// <param name="topRight">the confugured top right coordinates</param>
        /// <param name="bottomLeft">the confugured bottom left coordinates</param>
        public ForwardInstruction(int movePositions, Coordinate topRight, Coordinate bottomLeft)
            : base(topRight, bottomLeft)
        {
            MovePositions = movePositions;
        }

        public override ShipPosition Move(ShipPosition position)
        {
            if (position.Lost)
            {
                return position;
            }

            //validate if the move will result in the ship being lost
            if (WillBeLost(position))
            {
                //mark position as lost, keep old poistion
                position.Lost = true;
                return position;
            }

            //move position based on orientation
            switch (position.Orientation)
            {
                case Orientation.East:
                    position.X = position.X + MovePositions;
                    break;
                case Orientation.North:
                    position.Y = position.Y + MovePositions;
                    break;
                case Orientation.West:
                    position.X = position.X - MovePositions;
                    break;
                case Orientation.South:
                    position.Y = position.Y - MovePositions;
                    break;
                default:
                    throw new NotImplementedException(string.Format("Ship's orientation {0} is not supported for the Move Instruction", position.Orientation.ToString()));
            }

            return position;
        }

        public override bool WillBeLost(ShipPosition position)
        {
            //TODO refactor to make cleaner
            
            if (position.Orientation == Orientation.East && position.X < TopRight.X)
            {
                return false;
            }

            if (position.Orientation == Orientation.West && position.X > BottomLeft.X)
            {
                return false;
            }

            if (position.Orientation == Orientation.North && position.Y < TopRight.Y)
            {
                return false;
            }

            if (position.Orientation == Orientation.South && position.Y > BottomLeft.Y)
            {
                return false;
            }

            return true;
        }
    }
}
