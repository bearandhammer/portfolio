using System.Collections.Generic;

namespace RobotArmyProcessingLine.Interface
{
    /// <summary>
    /// Core interface describing shared behaviours
    /// for a 'Factory' in this application.
    /// </summary>
    public interface IRobotFactory
    {
        #region Interface Properties

        /// <summary>
        /// Interface property that can be implemented
        /// to control the specifics behind 
        /// whether a 'Factory'is operational or not.
        /// </summary>
        bool FactoryOperational { get; }

        #endregion Interface Properties

        #region Interface Methods

        /// <summary>
        /// Interface method that can be implemented to kick
        /// off a Factories ability to Paint Robots.
        /// See <seealso cref="IPainter"/> and 
        /// <seealso cref="IRobot"/> for linked behaviour objects.
        /// </summary>
        void PaintRobots();

        /// <summary>
        /// Interface method that can be implemented to kick
        /// off a Factories ability to arm Robots.
        /// See <seealso cref="IWeaponAttacher"/> and 
        /// <seealso cref="IRobot"/> for linked behaviour objects.
        /// </summary>
        void AttachWeapons();

        /// <summary>
        /// Interface method that can be implemented to control
        /// how <see cref="IRobot"/> based objects are added
        /// to a particular Factories production line.
        /// </summary>
        /// <param name="robotsToAdd">An array (params or set array) of <see cref="IRobot"/> implementing objects to add to the Factory production line.</param>
        void AddRobots(params IRobot[] robotsToAdd);

        /// <summary>
        /// Interface method that can be implemented to control
        /// how <see cref="IRobot"/> objects, within a factory, are
        /// deployed back to the front lines (aka, the method caller).
        /// </summary>
        /// <returns>An <see cref="IList{T}"/> of <see cref="IRobot"/> based objects.</returns>
        IList<IRobot> DeployRobots();

        #endregion Interface Methods
    }
}