using EpicQuest.Interfaces;
using System;

namespace EpicQuest.Models.Items
{
    public class TreasureItem : ITreasure, IRoomContent
    {
        public int GoldValue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}