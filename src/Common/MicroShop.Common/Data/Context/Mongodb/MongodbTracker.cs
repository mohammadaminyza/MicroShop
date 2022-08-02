namespace MicroShop.Common.Data.Context.Mongodb;

public class MongodbTracker
{
    #region Fields

    private readonly HashSet<Func<Task>> _mongodbActions;
    public HashSet<Func<Task>> MongodbActions => _mongodbActions;

    #endregion

    #region Ctor

    public MongodbTracker()
    {
        _mongodbActions = new HashSet<Func<Task>>();
    }

    #endregion

    #region Methods

    public void AddAction(Func<Task> acion)
    {
        _mongodbActions.Add(acion);
    }

    public IEnumerable<Task> ExcuteActions()
    {
        var actions = MongodbActions.Select(a => a());
        return actions;
    }

    #endregion
}