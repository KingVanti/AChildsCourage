using System;

namespace PADEAH.Game20483D.TestUtility
{

    public class EventMonitor<TEventArgs> where TEventArgs : EventArgs
    {

        #region Properties

        public TEventArgs LastRaised { get; private set; }

        #endregion

        #region Constructors

        public EventMonitor(object eventSource, string eventName)
        {
            SubscribeToEvent(eventSource, eventName);
        }

        public EventMonitor(object eventSource)
        {
            var eventArgsName = typeof(TEventArgs).Name;
            var eventName = $"On{eventArgsName.Substring(0, eventArgsName.Length - 9)}";

            SubscribeToEvent(eventSource, eventName);
        }

        #endregion

        #region Methods

        private void SubscribeToEvent(object eventSource, string eventName)
        {
            var sourceType = eventSource.GetType();
            var eventInfo = sourceType.GetEvent(eventName);

            if (eventInfo == null)
                throw new ArgumentException($"No event with the name {eventName} found on type {sourceType.Name}", nameof(eventName));

            var @delegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, nameof(OnEvent), false, false);
            eventInfo.AddEventHandler(eventSource, @delegate);
        }

        private void OnEvent(object sender, TEventArgs eventArgs)
        {
            LastRaised = eventArgs;
        }

        #endregion

    }

}