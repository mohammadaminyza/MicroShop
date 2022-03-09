using MicroShop.Common.Events;

namespace MicroShop.Common.Entities;

public abstract class AggregateRoot : BaseEntity
{
    private readonly List<IDomainEvent> _events;

    protected AggregateRoot() => _events = new();

    protected AggregateRoot(IEnumerable<IDomainEvent> events)
    {
        _events = events.ToList() ?? new();
        if (!_events.Any())
            return;
    }

    protected void AddEvent(IDomainEvent @event) => _events.Add(@event);
    protected IEnumerable<IDomainEvent> GetEvents() => _events.AsEnumerable();
    protected void ClearEvents() => _events.Clear();
}