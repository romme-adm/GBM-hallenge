namespace GBM.Challenge.OpenTransactios.Application.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IPublishEvent" />.
    /// </summary>
    public interface IPublishEvent
    {
        /// <summary>
        /// The Publish.
        /// </summary>
        /// <param name="@event">The event<see cref="object"/>.</param>
        void Publish(object @event);
    }
}
