namespace Application.Common.Models.CrawlerService;

public class WorkerServiceSendTokenDto
{
    public string AccessToken { get; set; }

    public WorkerServiceSendTokenDto(string accessToken)
    {
        AccessToken = accessToken;
    }
}