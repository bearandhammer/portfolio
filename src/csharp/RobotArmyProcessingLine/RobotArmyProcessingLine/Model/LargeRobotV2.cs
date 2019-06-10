using RobotArmyProcessingLine.Interface;

namespace RobotArmyProcessingLine.Model
{
    /// <summary>
    /// Provides the definition for a Version 2, 'Large Robot'.
    /// </summary>
    public sealed class LargeRobotV2 : Robot, ILargeRobot
    {
        #region Constructor

        /// <summary>
        /// Constructor for a LargeRobotV2, which passes
        /// a <see cref="IRobotOutputter"/> object to the 
        /// <seealso cref="Robot"/> base class for future use.
        /// </summary>
        /// <param name="outputter">The <see cref="IRobotOutputter"/> object to determine how this robot outputs command details.</param>
        public LargeRobotV2(IRobotOutputter outputter)
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
            MessageOutputter.WriteLine($"LargeRobotV2 { PaintDescription ?? string.Empty } attacking with a { WeaponDescription ?? string.Empty }!");
        }

        /// <summary>
        /// Public method that makes this robot move.
        /// </summary>
        public override void Move()
        {
            MessageOutputter.WriteLine($"LargeRobotV2 { PaintDescription ?? string.Empty } moving!");
        }

        /// <summary>
        /// Public method that makes this robot speak.
        /// </summary>
        public override void Speak()
        {
            MessageOutputter.WriteLine($"LargeRobotV2 { PaintDescription ?? string.Empty } speaking!");
        }

        #endregion IRobot Interface Methods

        #region ILargeRobot Interface Methods

        /// <summary>
        /// Public method that makes this robot crush.
        /// </summary>
        public void Crush()
        {
            MessageOutputter.WriteLine($"LargeRobotV2 { PaintDescription ?? string.Empty } crushing!");
        }

        #endregion ILargeRobot Interface Methods
    }
}