namespace WarHeroes.InventorySystem
{
    public interface ICounter
    {
        Counter Counter { get; }
        void CountUpdated(ICountable countable);
    }

    public class Counter
    {
        private ICounter _counter;

        public Counter(
            ICounter counter)
        {
            _counter = counter;
        }

        public int SetCount(
            ICountable countable,
            int count)
        {
            int result = countable.Countable.SetCount(count);

            _counter.CountUpdated(countable);

            return result;
        }

        public int IncreaseCount(
            ICountable countable,
            int count)
        {
            int result = countable.Countable.IncreaseCount(count);

            _counter.CountUpdated(countable);

            return result;
        }
    }
}
