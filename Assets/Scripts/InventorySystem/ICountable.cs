using System;

namespace WarHeroes.InventorySystem
{
    public interface ICountableTrackData
    {
        int CurrentCount { get; set; }
    }

    public interface ICountable
    {
        Countable Countable { get; }
    }

    public class Countable
    {
        public int Count
        {
            get => _countableTrackData.CurrentCount;
            private set => _countableTrackData.CurrentCount = value;
        }

        public Action<int> OnCountUpdated { get; set; }

        private ICountableTrackData _countableTrackData;

        public int MinCount { get; private set; }
        public int MaxCount { get; private set; }

        public Countable(
            ICountableTrackData countableTrackData)
        {
            _countableTrackData = countableTrackData;
            MinCount = int.MinValue;
            MaxCount = int.MaxValue;
        }

        public Countable WithMinCount(
            int minCount)
        {
            MinCount = minCount;

            return this;
        }

        public Countable WithMaxCount(
            int maxCount)
        {
            MaxCount = maxCount;

            return this;
        }

        public int SetCount(int count)
        {
            Count = count;

            if (count < MinCount)
                Count = MinCount;
            else if (count > MaxCount)
                Count = MaxCount;

            return Count;
        }

        public int IncreaseCount(int count)
        {
            int newCount = Count + count;

            return SetCount(newCount);
        }
    }
}
