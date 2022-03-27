namespace MicroShop.Common.Logger;
public interface IScopeInformation
{
    Dictionary<string, string> HostScopeInfo { get; }
    Dictionary<string, string> RequestScopeInfo { get; }
}

