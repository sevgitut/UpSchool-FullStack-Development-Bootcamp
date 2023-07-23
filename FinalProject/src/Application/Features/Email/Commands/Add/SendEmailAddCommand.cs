using MediatR;

namespace Application.Features.Email.Commands.Add;

public class SendEmailAddCommand : IRequest<SendEmailAddDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Link { get; set; }
}