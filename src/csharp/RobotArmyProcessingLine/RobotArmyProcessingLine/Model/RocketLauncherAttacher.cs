using RobotArmyProcessingLine.Interface;

namespace RobotArmyProcessingLine.Model
{
    /// <summary>
    /// Defines a type that can be used to arm <see cref="IRobot"/>
    /// objects with a rocket launcher.
    /// </summary>
    public class RocketLauncherAttacher : IWeaponAttacher
    {
        #region Private Constants

        /// <summary>
        /// Private constant that holds a description of how this weapon
        /// attachment machine will arm a given robot.
        /// </summary>
        private const string weaponDescription = "rocket launcher attachment!";

        #endregion Private Constants

        #region Public IWeaponAttacher Interface Methods

        /// <summary>
        /// Public method that arms the passed in 
        /// <see cref="IRobot"/> object.
        /// </summary>
        /// <param name="robot">The <see cref="IRobot"/> to be armed (if it's not null).</param>
        public void AttachWeapons(IRobot robot)
        {
            robot?.AttachWeapons(weaponDescription);
        }

        #endregion Public IWeaponAttacher Interface Methods
    }
}