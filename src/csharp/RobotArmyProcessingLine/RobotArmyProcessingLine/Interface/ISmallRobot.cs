namespace RobotArmyProcessingLine.Interface
{
    /// <summary>
    /// Interface describing additional behaviours
    /// for a 'Small' Robot.
    /// </summary>
    public interface ISmallRobot
    {
        #region Interface Methods

        /// <summary>
        /// Interface method that can be implemented
        /// to perform a 'sneak' action.
        /// </summary>
        void Sneak();

        #endregion Interface Methods
    }
}