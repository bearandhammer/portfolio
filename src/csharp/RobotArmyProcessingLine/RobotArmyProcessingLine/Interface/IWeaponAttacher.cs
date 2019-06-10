namespace RobotArmyProcessingLine.Interface
{
    /// <summary>
    /// Interface describing behaviours
    /// for a Weapon Attachment Machine that can operate 
    /// against an <see cref="IRobot"/> implementing object.
    /// </summary>
    public interface IWeaponAttacher
    {
        #region Interface Methods

        /// <summary>
        /// Interface method that can be implemented
        /// to 'attach weapons' to an <see cref="IRobot"/> implementing object.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> implementing type to attach weapons to.</param>
        void AttachWeapons(IRobot robot);

        #endregion Interface Methods
    }
}