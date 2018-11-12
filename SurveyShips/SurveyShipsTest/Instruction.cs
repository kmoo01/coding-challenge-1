using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SurveyShips;
namespace SurveyShipsTest
{
    /// <summary>
    /// Summary description for Instruction
    /// </summary>
    [TestClass]
    public class Instruction
    {
        public Instruction()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //setup
        Coordinate bottomLeft = new Coordinate(0, 0);
        Coordinate topRight = new Coordinate(20, 20);

        [TestMethod]
        public void Should_Change_Orientation_Counterclockwise_When_LeftInstruction_Moves()
        {
            
            ShipPosition position = new ShipPosition(5, 5, Orientation.West);
            LeftInstruction instruction = new LeftInstruction(topRight, bottomLeft);
            
            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(Orientation.South, actual.Orientation, "orientation not correct");
            Assert.AreEqual(position.X, actual.X, "ship should not have moved x corordinate");
            Assert.AreEqual(position.Y, actual.Y, "ship should not have moved y corordinate");
            Assert.AreEqual(false, actual.Lost, "ship should not be lost");
        }

        [TestMethod]
        public void Should_Change_Orientation_Clockwise_When_RightInstruction_Moves()
        {
            ShipPosition position = new ShipPosition(5, 5, Orientation.West);
            RightInstruction instruction = new RightInstruction(topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(Orientation.North, actual.Orientation, "orientation not correct");
            Assert.AreEqual(position.X, actual.X, "ship should not have moved x corordinate");
            Assert.AreEqual(position.Y, actual.Y, "ship should not have moved y corordinate");
            Assert.AreEqual(false, actual.Lost, "ship should not be lost");
        }

        [TestMethod]
        public void Should_Change_Position_West_When_ForwardInstruction_Moves()
        {
            int move = 1;
            int xPos = 5;
            Orientation orientation = Orientation.West;
            
            ShipPosition position = new ShipPosition(xPos, 5, orientation);
            ForwardInstruction instruction = new ForwardInstruction(move, topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(orientation, actual.Orientation, "orientation should not have moved");
            Assert.AreEqual(xPos - 1, actual.X, "ship should have moved x corordinate");
            Assert.AreEqual(position.Y, actual.Y, "ship should not have moved y corordinate");
            Assert.AreEqual(false, actual.Lost, "ship should not be lost");
        }

        [TestMethod]
        public void Should_Change_Position_East_When_ForwardInstruction_Moves()
        {
            int move = 1;
            int xPos = 5;
            Orientation orientation = Orientation.East;

            ShipPosition position = new ShipPosition(xPos, 5, orientation);
            ForwardInstruction instruction = new ForwardInstruction(move, topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(orientation, actual.Orientation, "orientation should not have moved");
            Assert.AreEqual(xPos + 1, actual.X, "ship should have moved x corordinate");
            Assert.AreEqual(position.Y, actual.Y, "ship should not have moved y corordinate");
            Assert.AreEqual(false, actual.Lost, "ship should not be lost");
        }


        [TestMethod]
        public void Should_Change_Position_North_When_ForwardInstruction_Moves()
        {
            int move = 1;
            int yPos = 5;
            Orientation orientation = Orientation.North;

            ShipPosition position = new ShipPosition(5, yPos, orientation);
            ForwardInstruction instruction = new ForwardInstruction(move, topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(orientation, actual.Orientation, "orientation should not have moved");
            Assert.AreEqual(position.X, actual.X, "ship should not have moved x corordinate");
            Assert.AreEqual(yPos + 1, actual.Y, "ship should have moved y corordinate");
            Assert.AreEqual(false, actual.Lost, "ship should not be lost");
        }

        [TestMethod]
        public void Should_Change_Position_South_When_ForwardInstruction_Moves()
        {
            int move = 1;
            int yPos = 5;
            Orientation orientation = Orientation.South;

            ShipPosition position = new ShipPosition(5, yPos, orientation);
            ForwardInstruction instruction = new ForwardInstruction(move, topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(orientation, actual.Orientation, "orientation should not have moved");
            Assert.AreEqual(position.X, actual.X, "ship should not have moved x corordinate");
            Assert.AreEqual(yPos - 1, actual.Y, "ship should have moved y corordinate");
            Assert.AreEqual(false, actual.Lost, "ship should not be lost");
        }



        [TestMethod]
        public void Should_Mark_As_Lost_When_ForwardInstruction_Moves_South_Over_Boarder()
        {
            int move = 1;
            int yPos = bottomLeft.Y;
            Orientation orientation = Orientation.South;

            ShipPosition position = new ShipPosition(5, yPos, orientation);
            ForwardInstruction instruction = new ForwardInstruction(move, topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(orientation, actual.Orientation, "orientation should not have moved");
            Assert.AreEqual(position.X, actual.X, "ship should not have moved x corordinate");
            Assert.AreEqual(position.Y, actual.Y, "ship should not have moved x corordinate");
            Assert.AreEqual(true, actual.Lost, "ship should be lost");
        }

        [TestMethod]
        public void Should_Mark_As_Lost_When_ForwardInstruction_Moves_North_Over_Boarder()
        {
            int move = 1;
            int yPos = topRight.Y;
            Orientation orientation = Orientation.North;

            ShipPosition position = new ShipPosition(5, yPos, orientation);
            ForwardInstruction instruction = new ForwardInstruction(move, topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(orientation, actual.Orientation, "orientation should not have moved");
            Assert.AreEqual(position.X, actual.X, "ship should not have moved x corordinate");
            Assert.AreEqual(position.Y, actual.Y, "ship should not have moved x corordinate");
            Assert.AreEqual(true, actual.Lost, "ship should not be lost");
        }


        [TestMethod]
        public void Should_Mark_As_Lost_When_ForwardInstruction_Moves_East_Over_Boarder()
        {
            int move = 1;
            int xPos = topRight.X;
            Orientation orientation = Orientation.East;

            ShipPosition position = new ShipPosition(xPos, 5, orientation);
            ForwardInstruction instruction = new ForwardInstruction(move, topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(orientation, actual.Orientation, "orientation should not have moved");
            Assert.AreEqual(position.X, actual.X, "ship should not have moved x corordinate");
            Assert.AreEqual(position.Y, actual.Y, "ship should not have moved x corordinate");
            Assert.AreEqual(true, actual.Lost, "ship should not be lost");
        }

        [TestMethod]
        public void Should_Mark_As_Lost_When_ForwardInstruction_Moves_West_Over_Boarder()
        {
            int move = 1;
            int xPos = bottomLeft.X;
            Orientation orientation = Orientation.West;

            ShipPosition position = new ShipPosition(xPos, 5, orientation);
            ForwardInstruction instruction = new ForwardInstruction(move, topRight, bottomLeft);

            //test
            ShipPosition actual = instruction.Move(position);

            //assert
            Assert.AreEqual(orientation, actual.Orientation, "orientation should not have moved");
            Assert.AreEqual(position.X, actual.X, "ship should not have moved x corordinate");
            Assert.AreEqual(position.Y, actual.Y, "ship should not have moved x corordinate");
            Assert.AreEqual(true, actual.Lost, "ship should not be lost");
        }
    }
}
