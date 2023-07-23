namespace Application.Services
{
    public interface IUrlHelperService
    {
        string ApiUrl { get; }
        string SignalRUrl { get; }
    }
}
