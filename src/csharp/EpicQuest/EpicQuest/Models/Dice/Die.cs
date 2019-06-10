using EpicQuest.GameEnums;

namespace EpicQuest.Models
{
    /// <summary>
    /// Base class for all Epic Quest Die objects.
    /// </summary>
    public abstract class Die
    {
        #region Abstract Members

        /// <summary>
        /// Each Die must implement it's own array of
        /// DieFace enum values (i.e. the possible results
        /// supported by the Die in question).
        /// </summary>
        public abstract DieFace[] DieFaces { get; }

        #endregion Abstract Members
    }
}