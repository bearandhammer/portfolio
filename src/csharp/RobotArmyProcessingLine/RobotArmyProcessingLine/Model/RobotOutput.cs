using RobotArmyProcessingLine.Interface;
using System;

namespace RobotArmyProcessingLine.Model
{
    /// <summary>
    /// Type that is used to define an IO (console-based) output
    /// method as a robot messaging output.
    /// </summary>
    public class RobotOutput : IRobotOutputter
    {
        #region IRobotOutputter Interface Methods

        /// <summary>
        /// Allows a message to be output to the console.
        /// See <see cref="IRobot"/> implementing classes for usage.
        /// </summary>
        /// <param name="outputMessage">The message to process.</param>
        public void WriteLine(string outputMessage)
        {
            Console.WriteLine(outputMessage);
        }

        #endregion IRobotOutputter Interface Methods
    }
}