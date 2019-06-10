using RobotArmyProcessingLine.Interface;

namespace RobotArmyProcessingLine.Model
{
    /// <summary>
    /// Provides the definition for a Version 1, 'Small Robot'.
    /// </summary>
    public sealed class SmallRobotV1 : Robot, ISmallRobot
    {
        #region Constructor

        /// <summary>
        /// Constructor for a SmallRobotV1, which passes
        /// a <see cref="IRobotOutputter"/> object to the 
        /// <seealso cref="Robot"/> base class for future use.
        /// </summary>
        /// <param name="outputter">The <see cref="IRobotOutputter"/> object to determine how this robot outputs command details.</param>
        public SmallRobotV1(IRobotOutputter outputter)
            : base(outputter)
        {

        }

        #endregion Constructor

        #region IRobot Interface Methods

        /// <summary>
        /// Public method that makes this robot attack.
        /// </summary>
        public override void Attack()
        {
            MessageOutputter.WriteLine($"SmallRobotV1 { PaintDescription ?? string.Empty } attacking with a { WeaponDescription ?? string.Empty }!");
        }

        /// <summary>
        /// Public method that makes this robot move.
        /// </summary>
        public override void Move()
        {
            MessageOutputter.WriteLine($"SmallRobotV1 { PaintDescription ?? string.Empty } moving!");
        }

        /// <summary>
        /// Public method that makes this robot speak.
        /// </summary>
        public override void Speak()
        {
            MessageOutputter.WriteLine($"SmallRobotV1 { PaintDescription ?? string.Empty } speaking!");
        }

        #endregion IRobot Interface Methods

        #region ISmallRobot Interface Methods

        /// <summary>
        /// Public method that makes this robot sneak.
        /// </summary>
        public void Sneak()
        {
            MessageOutputter.WriteLine($"SmallRobotV1 { PaintDescription ?? string.Empty } sneaking!");
        }

        #endregion ISmallRobot Interface Methods
    }
}