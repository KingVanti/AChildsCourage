using System;

namespace PADEAH.TestUtility
{

    public static class Extensions
    {

        #region Methods

        public static EventMonitor<TEventArgs> AttachMonitor<TEventArgs>(this object eventSource) where TEventArgs : EventArgs
        {
            return new EventMonitor<TEventArgs>(eventSource);
        }

        public static TEventArgs Capture<TEventArgs>(this object eventSource, Action action) where TEventArgs : EventArgs
        {
            var eventMonitor = eventSource.AttachMonitor<TEventArgs>();

            action();

            return eventMonitor.LastRaised;
        }

        #endregion

    }

}