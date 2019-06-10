namespace RobotArmyProcessingLine.Interface
{
    /// <summary>
    /// Interface describing behaviours
    /// for a Robot Output Module (so that a Robot
    /// can provide details via a designated IO channel
    /// on its various actions (e.g. movement, attacking, etc.).
    /// This is currently utilised by <seealso cref="IRobot"/> implementing objects.
    /// </summary>
    public interface IRobotOutputter
    {
        #region Interface Methods

        /// <summary>
        /// Interface method that can be implemented to control
        /// how (i.e. to what IO Channel, for example) an object publishes messages.
        /// </summary>
        /// <param name="outputMessage"></param>
        void WriteLine(string outputMessage);

        #endregion Interface Methods
    }
}