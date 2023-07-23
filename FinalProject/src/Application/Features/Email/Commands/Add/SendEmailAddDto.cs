namespace Application.Features.Email.Commands.Add;

public class SendEmailAddDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Link { get; set; }

    public SendEmailAddDto(string firstName, string lastName, string email, string link)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Link = link;
    }
}