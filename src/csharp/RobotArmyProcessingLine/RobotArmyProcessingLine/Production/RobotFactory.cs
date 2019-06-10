using RobotArmyProcessingLine.Interface;
using System.Collections.Generic;
using System.Linq;

namespace RobotArmyProcessingLine.Production
{
    /// <summary>
    /// Base class defining shared elements for all robot factories.
    /// Provides abstract members and <see cref="IRobotFactory"/> based members.
    /// </summary>
    public abstract class RobotFactory : IRobotFactory
    {
        #region Private Fields

        /// <summary>
        /// An <see cref="IPainter"/> based object for painting robots in the factory.
        /// </summary>
        private IPainter paintMachine;

        /// <summary>
        /// An <see cref="IWeaponAttacher"/> based object for arming robots in the factory.
        /// </summary>
        private IWeaponAttacher weaponAttacherMachine;

        #endregion Private Fields

        #region Protected (Get) Properties

        /// <summary>
        /// Allow a factory access to the robots (<see 
        /// cref="IRobot"/> types) in the production line.
        /// </summary>
        protected List<IRobot> Robots { get; private set; }

        #endregion Protected (Get) Properties

        #region Public Abstract Properties

        /// <summary>
        /// public abstract member that needs to be implemented by
        /// deriving factories to determine the conditions under which
        /// the factory is operational (i.e. the production line is not overloaded).
        /// </summary>
        public abstract bool FactoryOperational { get; }

        #endregion Public Abstract Properties

        #region Constructor

        /// <summary>
        /// Constructor for a RobotFactory that sets
        /// the <see cref="IPainter"/> and <see cref="IWeaponAttacher"/> implementing
        /// objects on this type (all factories can paint/arm robots).
        /// </summary>
        /// <param name="paintMachineToUse">An <see cref="IPainter"/> implementing object for this factory to paint robots.</param>
        /// <param name="weaponAttacherMachineToUse">An <see cref="IWeaponAttacher"/> implementing object for this factory to arm robots.</param>
        public RobotFactory(IPainter paintMachineToUse, IWeaponAttacher weaponAttacherMachineToUse)
        {
            Robots = new List<IRobot>();
            paintMachine = paintMachineToUse;
            weaponAttacherMachine = weaponAttacherMachineToUse;
        }

        #endregion Constructor

        #region IRobotFactory Interface Methods

        /// <summary>
        /// public method that paints robots on the production line.
        /// </summary>
        public void PaintRobots()
        {
            Robots?.ForEach(robot =>
            {
                paintMachine.Paint(robot);
            });
        }

        /// <summary>
        /// public method that attaches weapons to robots on the production line. 
        /// </summary>
        public void AttachWeapons()
        {
            Robots?.ForEach(robot =>
            {
                weaponAttacherMachine.AttachWeapons(robot);
            });
        }

        /// <summary>
        /// public method that adds robots (<see cref="IRobot"/> objects) on to the production line. 
        /// </summary>
        /// <param name="robotsToAdd">An array of type <see cref="IRobot"/> defining the robots to add to the production line.</param>
        public void AddRobots(params IRobot[] robotsToAdd)
        {
            if (robotsToAdd?.Count() > 0)
            {
                Robots.AddRange(robotsToAdd);
            }
        }

        /// <summary>
        /// public method that deploys robots off the production line.
        /// Clears down the production line in the factory and returns all robots to the caller.
        /// </summary>
        /// <returns>Returns an <see cref="IList{T}"/> of <see cref="IRobot"/> types.</returns>
        public IList<IRobot> DeployRobots()
        {
            // Copy robots to a new IList - Ready for deployment
            IList<IRobot> robotsToDeploy = new List<IRobot>(Robots);

            // Clear the current production line
            Robots.Clear();

            // Deploy!
            return robotsToDeploy;
        }

        #endregion IRobotFactory Interface Methods
    }
}