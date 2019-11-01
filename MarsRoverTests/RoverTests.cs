using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MarsRoverTests
{
    [TestClass]
    public class RoverTests
    {
        Random random = new Random();

        [TestMethod]
        public void Test_ExecuteCommand_VerticalMotion()
        {
            Rover rover = new Rover(1, 1, 'N');
            rover.ExecuteCommand('M');

            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(2, rover.VerticalLocation);
            Assert.AreEqual('N', rover.Orientation);
        }

        [TestMethod]
        public void Test_ExecuteCommand_HorizontalMotion()
        {
            Rover rover = new Rover(1, 1, 'E');
            rover.ExecuteCommand('M');

            Assert.AreEqual(2, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('E', rover.Orientation);
        }

        [TestMethod]
        public void Test_ExecuteCommand_RightTurns()
        {
            Rover rover = new Rover(1, 1, 'N');
            rover.ExecuteCommand('R');
            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('E', rover.Orientation);
            rover.ExecuteCommand('R');
            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('S', rover.Orientation);
            rover.ExecuteCommand('R');
            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('W', rover.Orientation);
            rover.ExecuteCommand('R');
            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('N', rover.Orientation);
        }

        [TestMethod]
        public void Test_ExecuteCommand_LeftTurns()
        {
            Rover rover = new Rover(1, 1, 'N');
            rover.ExecuteCommand('L');
            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('W', rover.Orientation);
            rover.ExecuteCommand('L');
            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('S', rover.Orientation);
            rover.ExecuteCommand('L');
            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('E', rover.Orientation);
            rover.ExecuteCommand('L');
            Assert.AreEqual(1, rover.HorizontalLocation);
            Assert.AreEqual(1, rover.VerticalLocation);
            Assert.AreEqual('N', rover.Orientation);
        }

        [TestMethod]
        public void Test_ExecuteCommand_SquareDance()
        {
            var horizontalStart = random.Next(1,1000);
            var verticalStart = random.Next(1, 1000);

            char startOrientation = 'N';

            switch (random.Next(1, 4))
            {
                case 1:
                    startOrientation = 'N';
                    break;
                case 2:
                    startOrientation = 'S';
                    break;
                case 3:
                    startOrientation = 'E';
                    break;
                case 4:
                    startOrientation = 'W';
                    break;
            }

            char direction = 'L';

            switch (random.Next(1, 2))
            {
                case 1:
                    direction = 'R';
                    break;
                case 2:
                    direction = 'L';
                    break;
            }

            Rover rover = new Rover(horizontalStart, verticalStart, startOrientation);

            var size = random.Next(1, 1000);

            for (int i = 0; i < 4; i++)
            {
                for (int cnt = 0; cnt < size; cnt++)
                {
                    rover.ExecuteCommand('M');
                }
                rover.ExecuteCommand(direction);
            }

            Assert.AreEqual(horizontalStart, rover.HorizontalLocation);
            Assert.AreEqual(verticalStart, rover.VerticalLocation);
            Assert.AreEqual(startOrientation, rover.Orientation);       
        }

        [TestMethod]
        public void Test_PlanValidation_Boundary_NonNumericChars()
        {
            Exception caught = null;
            try
            {
                var invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                int i;
                while (int.TryParse(invalidChar.ToString(), out i))
                {
                    invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                }

                var p = new Plan($"{invalidChar}2", "12 E", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Invalid boundary specified.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_Boundary_NonSquare()
        {
            Exception caught = null;
            try
            {
                int r = random.Next();
                while (r % 2 == 0)
                {
                    r = random.Next();
                }
                
                string randomBoundary = r.ToString();
                var p = new Plan(randomBoundary, "12 E", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Boundary is not a square.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_Boundary_Square()
        {
            Exception caught = null;
            try
            {
                string randomBoundary = random.Next().ToString();
                var p = new Plan($"{randomBoundary}{randomBoundary}", "12 E", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual(null, caught);
        }

        [TestMethod]
        public void Test_PlanValidation_Boundary_TooSmall()
        {
            Exception caught = null;
            try
            {
                var p = new Plan("00", "12 E", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Boundary limit must be greater than 0", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_BadFormat_1()
        {
            Exception caught = null;

            var invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));

            try
            {
                var p = new Plan("11", $"12 E {invalidChar}", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Invalid starting co-ordinate format.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_BadFormat_2()
        {
            Exception caught = null;
            try
            {
                var p = new Plan("11", "1 E", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Invalid starting co-ordinates.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_BadFormat_3()
        {
            Exception caught = null;

            try
            {
                var invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                int i;
                while (int.TryParse(invalidChar.ToString(), out i))
                {
                    invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                }

                var p = new Plan("11", $"1{invalidChar} E", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Invalid starting co-ordinates.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_BadFormat_4()
        {
            Exception caught = null;

            try
            {
                var p = new Plan("11", $"{random.Next(100,int.MaxValue)} E", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Invalid starting co-ordinates.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_BadFormat_5()
        {
            Exception caught = null;
            try
            {
                var invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));

                var p = new Plan("11", $"12 E{invalidChar}", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Invalid start orientation.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_BadFormat_6()
        {
            Exception caught = null;
            try
            {
                var invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                while (invalidChar == 'N' || invalidChar == 'S' || invalidChar == 'W' || invalidChar == 'E')
                {
                    invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                }
                
                var p = new Plan("11", $"12 {invalidChar}", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Invalid start orientation.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_OutOfBounds_1()
        {
            Exception caught = null;
            try
            {
                var p = new Plan("11", "21 N", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Starting co-ordinates outside of boundary.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_OutOfBounds_2()
        {
            Exception caught = null;
            try
            {
                var p = new Plan("11", "12 N", "M");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Starting co-ordinates outside of boundary.", caught.Message);
        }

        [TestMethod]
        public void Test_PlanValidation_StartingPosition_InvalidCommand()
        {
            Exception caught = null;
            try
            {
                var invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                while (invalidChar == 'M' || invalidChar == 'R' || invalidChar == 'L')
                {
                    invalidChar = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                }

                var p = new Plan("11", "12 N", "B");
            }
            catch (Exception e)
            {
                caught = e;
            }

            Assert.AreEqual("Starting co-ordinates outside of boundary.", caught.Message);
        }
    }
}
