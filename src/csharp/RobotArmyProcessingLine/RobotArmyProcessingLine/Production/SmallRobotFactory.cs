using RobotArmyProcessingLine.Interface;

namespace RobotArmyProcessingLine.Production
{
    /// <summary>
    /// Type that defines a 'small' factory that can process
    /// (paint, arm and deploy) <see cref="IRobot"/> implementing objects.
    /// Utilises <seealso cref="IPainter"/> and <seealso cref="IWeaponAttacher"/> utility classes.
    /// </summary>
    public class SmallRobotFactory : RobotFactory
    {
        #region Public Overriden Properties

        /// <summary>
        /// public overriden property that determines if this
        /// factory is operational. This factory is not operational
        /// if the production line (robot count) exceeds the maximum limit
        /// (we could of course add additional logic in here for this factory type).
        /// </summary>
        public override bool FactoryOperational
        {
            get
            {
                return Robots?.Count <= 10;
            }
        }

        #endregion Public Overriden Properties

        #region Constructor

        /// <summary>
        /// Constructor for a SmallRobotFactory that passes
        /// the <see cref="IPainter"/> and <see cref="IWeaponAttacher"/> implementing
        /// object to the base class (all factories can paint/arm robots).
        /// </summary>
        /// <param name="paintMachineToUse">An <see cref="IPainter"/> implementing object for this factory to paint robots.</param>
        /// <param name="weaponAttacherMachineToUse">An <see cref="IWeaponAttacher"/> implementing object for this factory to arm robots.</param>
        public SmallRobotFactory(IPainter paintMachineToUse, IWeaponAttacher weaponAttacherMachineToUse)
            : base(paintMachineToUse, weaponAttacherMachineToUse)
        {

        }

        #endregion Constructor
    }
}