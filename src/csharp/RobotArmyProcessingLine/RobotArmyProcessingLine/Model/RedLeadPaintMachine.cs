using RobotArmyProcessingLine.Interface;

namespace RobotArmyProcessingLine.Model
{
    /// <summary>
    /// Defines a type that can be used to paint <see cref="IRobot"/>
    /// objects with red, lead-based paint.
    /// </summary>
    public class RedLeadPaintMachine : IPainter
    {
        #region Private Constant

        /// <summary>
        /// Private constant that holds a description of how this paint
        /// machine will paint a given robot.
        /// </summary>
        private const string paintDescription = "painted in red, lead-based paint";

        #endregion Private Constant

        /// <summary>
        /// Public method that paints the passed in 
        /// <see cref="IRobot"/> object.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> to be painted (if it's not null).</param>
        public void Paint(IRobot robot)
        {
            // If the robot is not null then paint it
            robot?.Paint(paintDescription);
        }
    }
}