using System;
using RobotArmyProcessingLine.Interface;

namespace RobotArmyProcessingLine.Model
{
    /// <summary>
    /// Provides an abstract, base definition for all robots
    /// in this application, utlising the <see cref="IRobot"/> interface.
    /// </summary>
    public abstract class Robot : IRobot
    {
        #region Protected (Get) Properties

        /// <summary>
        /// The <see cref="IRobotOutputter"/> implementing object that can
        /// be used to determine how this robot outputs messages.
        /// </summary>
        protected IRobotOutputter MessageOutputter { get; private set; }

        #endregion Protected Properties

        #region Public (Get) Properties

        /// <summary>
        /// The description of the paint job applied to this robot
        /// after it has been through the factory production line (or some other process).
        /// </summary>
        public string PaintDescription { get; private set; }

        /// <summary>
        /// The description of the weapon attachments applied to this robot
        /// after it has been through the factory production line (or some other process).
        /// </summary>
        public string WeaponDescription { get; private set; }

        #endregion Public (Get) Properties

        #region Constructor

        /// <summary>
        /// Constructor for this base class that consumes and sets
        /// this types <see cref="IRobotOutputter"/> object for
        /// handling how a robot pushes out messages.
        /// </summary>
        /// <param name="outputter">The <see cref="IRobotOutputter"/> object for the robot to use for messaging.</param>
        public Robot(IRobotOutputter outputter)
        {
            // Robots must be able to output, no exceptions (i.e. throw an exception! Probably bad comment wording ahem)
            if (outputter == null)
            {
                throw new ArgumentNullException(nameof(outputter));
            }

            MessageOutputter = outputter;
        }

        #endregion Constructor

        #region Public Abstract IRobot Interface Methods

        /// <summary>
        /// Public abstract method that must be overriden by
        /// deriving types to determine how a robot attacks.
        /// </summary>
        public abstract void Attack();

        /// <summary>
        /// Public abstract method that must be overriden by
        /// deriving types to determine how a robot moves.
        /// </summary>
        public abstract void Move();

        /// <summary>
        /// Public abstract method that must be overriden by
        /// deriving types to determine how a robot speaks.
        /// </summary>
        public abstract void Speak();

        #endregion public abstract IRobot Interface Methods

        #region Public IRobot Interface Methods

        /// <summary>
        /// Public method that is used to pass details
        /// of a paint job to this robot.
        /// </summary>
        /// <param name="robotPaintDescription">The paint job applied to the robot.</param>
        public void Paint(string robotPaintDescription)
        {
            PaintDescription = robotPaintDescription;
        }

        /// <summary>
        /// Public method that that is used to pass details
        /// of the weapon that is to be used by this robot.
        /// </summary>
        /// <param name="robotWeaponAttachmentDescription">The weapon added to the robot.</param>
        public void AttachWeapons(string robotWeaponAttachmentDescription)
        {
            WeaponDescription = robotWeaponAttachmentDescription;
        }

        #endregion Public IRobot Interface Methods
    }
}