using EpicQuest.Interfaces;
using System;

namespace EpicQuest.Models
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class GameCharacter : IUniqueItem
    {
        private readonly Guid uniqueRef = Guid.NewGuid();

        public Guid UniqueRef
        {
            get 
            {
                return uniqueRef; 
            }
        }

        public virtual string CharacterOpeningSpeech()
        {
            return "I will rip your flesh from your bones!!!";
        }
    }
}
