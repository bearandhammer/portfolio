using RobotArmyProcessingLine.Interface;
using System;
using System.IO;

namespace RobotArmyProcessingLine.Model
{
    /// <summary>
    /// Type that is used to define an IO (file-based) output
    /// method as a robot messaging output.
    /// </summary>
    public class RobotFileOutput : IRobotOutputter
    {
        #region IRobotOutputter Interface Methods

        /// <summary>
        /// Allows a message to be output to a file.
        /// See <see cref="IRobot"/> implementing classes for usage.
        /// </summary>
        /// <param name="outputMessage">The message to process.</param>
        public void WriteLine(string outputMessage)
        {
            File.AppendAllText("Robot-output.txt", outputMessage + Environment.NewLine);
        }

        #endregion IRobotOutputter Interface Methods
    }
}