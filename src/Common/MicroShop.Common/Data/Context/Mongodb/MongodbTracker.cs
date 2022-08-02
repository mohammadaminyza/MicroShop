namespace MicroShop.Common.Data.Context.Mongodb;

public class MongodbTracker
{
    #region Fields

    private readonly HashSet<Func<Task>> _mongodbCommands;
    public HashSet<Func<Task>> MongodbCommands=> _mongodbCommands;

    #endregion

    #region Ctor

    public MongodbTracker()
    {
        _mongodbCommands = new HashSet<Func<Task>>();
    }

    #endregion

    #region Methods

    /// <summary>
    /// Add Command To MongodbFunctions
    /// </summary>
    /// <param name="command"></param>
    public void AddCommand(Func<Task> command)
    {
        _mongodbCommands.Add(command);
    }

    public IEnumerable<Task> ExcuteCommands()
    {
        var actions = MongodbCommands.Select(a => a());
        return actions;
    }

    #endregion
}