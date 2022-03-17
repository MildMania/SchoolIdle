using MMFramework.TrackerSystem;
using System;

namespace WarHeroes.InventorySystem
{
    public enum EInventory
    {
        None = 0,
        PowerBelt = 1,
        Skin = 2,
        Chest = 3,
        Currency = 4,
    }

    public interface IInventory<TEnum>
        where TEnum : Enum
    {
        TEnum InventoryType { get; }
    }

    public abstract class InventoryBase<TTracker, TTrackable, TTrackInfo, TID, TEnum> : IInventory<TEnum>
        where TTracker : TrackerBase<TTrackable, TTrackInfo, TID>
        where TTrackable : ITrackable<TTrackInfo, TID>
        where TTrackInfo : TrackData<TID>
        where TID : IConvertible
        where TEnum : Enum
    {
        public TTracker Tracker { get; private set; }

        private TrackerLoadedTaskBase[] _trackerLoadedTasks
            = new TrackerLoadedTaskBase[0];

        private TrackerUpdatedTaskBase[] _trackerUpdatedTasks
            = new TrackerUpdatedTaskBase[0];

        private InitializeInventoryItemActionBase[] _initializeInventoryItemActions
            = new InitializeInventoryItemActionBase[0];

        public TEnum InventoryType => GetInventoryType();

        public InventoryBase(
            TTracker tracker)
        {
            Tracker = tracker;

            _trackerLoadedTasks = CreateTrackerLoadedTasks();
            _trackerUpdatedTasks = CreateTrackerUpdatedTasks();
            _initializeInventoryItemActions = CreateInitializeInventoryItemActions();

            Tracker.OnTrackerLoaded += OnTrackerLoaded;
            Tracker.OnTrackerUpdated += OnTrackerUpdated;
        }

        protected virtual TrackerLoadedTaskBase[] CreateTrackerLoadedTasks()
        {
            return new TrackerLoadedTaskBase[0];
        }

        protected virtual TrackerUpdatedTaskBase[] CreateTrackerUpdatedTasks()
        {
            return new TrackerUpdatedTaskBase[0];
        }

        protected virtual InitializeInventoryItemActionBase[] CreateInitializeInventoryItemActions()
        {
            return new InitializeInventoryItemActionBase[0];
        }

        private void OnTrackerLoaded()
        {
            foreach(TrackerLoadedTaskBase a in _trackerLoadedTasks)
                a.Execute(this, Tracker);

            InitializeInventoryItems();
        }

        private void OnTrackerUpdated(TTrackable trackable)
        {
            foreach (TrackerUpdatedTaskBase a in _trackerUpdatedTasks)
                a.Execute(this, Tracker);
        }


        private void InitializeInventoryItems()
        {
            foreach (TTrackable trackable in Tracker.Trackables)
            {
                foreach (InitializeInventoryItemActionBase a in _initializeInventoryItemActions)
                    a.Execute(this, trackable);
            }
        }

        protected abstract TEnum GetInventoryType();
    }
}
