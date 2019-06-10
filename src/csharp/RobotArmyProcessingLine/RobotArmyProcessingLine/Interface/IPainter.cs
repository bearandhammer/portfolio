namespace RobotArmyProcessingLine.Interface
{
    /// <summary>
    /// Interface describing behaviours
    /// for a Painting Machine that can operate 
    /// against an <see cref="IRobot"/> implementing object.
    /// </summary>
    public interface IPainter
    {
        #region Interface Methods

        /// <summary>
        /// Interface method that can be implemented
        /// to 'Paint' an <see cref="IRobot"/> implementing object.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> implementing type to paint.</param>
        void Paint(IRobot robot);

        #endregion Interface Methods
    }
}