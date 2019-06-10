namespace RobotArmyProcessingLine.Interface
{
    /// <summary>
    /// Core interface describing shared behaviours
    /// for a 'Robot' in this application.
    /// </summary>
    public interface IRobot
    {
        #region Interface Properties

        /// <summary>
        /// Interface property that describes
        /// a robots paint job.
        /// </summary>
        string PaintDescription { get; }

        /// <summary>
        /// Interface property that describes
        /// a robots weapon attachment.
        /// </summary>
        string WeaponDescription { get; }

        #endregion Interface Properties

        #region Interface Methods

        /// <summary>
        /// Interface method that, when 
        /// implemented, 'moves' a robot.
        /// </summary>
        void Move();

        /// <summary>
        /// Interface method that, when 
        /// implemented, makes a robot 'speak'.
        /// </summary>
        void Speak();

        /// <summary>
        /// Interface method that, when 
        /// implemented, makes a robot 'speak'.
        /// </summary>
        void Attack();

        /// <summary>
        /// Interface method that, when implemented, controls
        /// the specifics of how a robot is painted. This ties 
        /// to an<seealso cref="IPainter"/> implementing 
        /// object (of an <seealso cref="IRobotFactory" /> object).
        /// </summary>
        /// <param name="robotPaintDescription">A string that provides details on the robot paint job.</param>
        void Paint(string robotPaintDescription);

        /// <summary>
        /// Interface method that, when implemented, controls
        /// the specifics of how a robot receives weapon attachments.
        /// This ties to an <seealso cref="IWeaponAttacher"/> 
        /// implementing object (of an <seealso cref="IRobotFactory" /> object).
        /// </summary>
        /// <param name="robotWeaponAttachmentDescription">A string that provides details on the robots weapon attachments.</param>
        void AttachWeapons(string robotWeaponAttachmentDescription);

        #endregion Interface Methods
    }
}