namespace RobotArmyProcessingLine.Interface
{
    /// <summary>
    /// Interface describing additional behaviours
    /// for a 'Large' Robot.
    /// </summary>
    public interface ILargeRobot
    {
        #region Interface Methods

        /// <summary>
        /// Interface method that can be implemented
        /// to perform a 'crush' action.
        /// </summary>
        void Crush();

        #endregion Interface Methods
    }
}