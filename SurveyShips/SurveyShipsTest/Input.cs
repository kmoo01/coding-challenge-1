using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SurveyShips;

namespace SurveyShipsTest
{
    /// <summary>
    /// Summary description for Input
    /// </summary>
    [TestClass]
    public class Input
    {
        public Input()
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

        string sampleInput = @"5 3
1 1 E
RFRFRFRF

3 2 N
FRRFLLFFRRFLL

0 3 W
LLFFFLFLFL";

        [TestMethod]
        public void Should_Return_Empty_Input_When_Invalid_Grid_Setup()
        {
            //setup
            var input = new SpaceDelimitedCharInstructionGridCommandPairInput();
            
            //test
            var actual = input.ParseSurveyInput("3 t");

            //assert
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Should_Return_Partial_Input_When_Valid_Grid_Setup()
        {
            //setup
            var input = new SpaceDelimitedCharInstructionGridCommandPairInput();
           
            //test
            var actual = input.ParseSurveyInput("4 6");

            Assert.AreEqual(4, actual.TopRightCoordinate.X);
            Assert.AreEqual(6, actual.TopRightCoordinate.Y);
        }

        //blar balr blar ... would add more here
    
        [TestMethod]
        public void Should_Return_Full_Input_When_Test_Data_Parsed()
        {
            //setup
            var input = new SpaceDelimitedCharInstructionGridCommandPairInput();

            //test
            var actual = input.ParseSurveyInput(sampleInput);

            //assert overall no issues
            Assert.IsNotNull(actual);

            //assert grid
            Assert.AreEqual(5, actual.TopRightCoordinate.X);
            Assert.AreEqual(3, actual.TopRightCoordinate.Y);

            //assert 3 commands
            Assert.IsNotNull(actual.Commands);
            Assert.AreEqual(3, actual.Commands.Count());

            //doesnt match sample output :S !!!!!
            Assert.AreEqual(Orientation.South, actual.Commands[2].Position.Orientation);
        }
    }
}
