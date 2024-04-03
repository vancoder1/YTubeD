using Prism.Events;

namespace YTubeD.Utils
{
    public class EventAggregatorUtility
    {
        public static EventAggregator EventAggregator { get; set; }

        static EventAggregatorUtility()
        {
            EventAggregator = new EventAggregator();
        }
    }
}
