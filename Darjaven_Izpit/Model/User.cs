namespace Darjaven_Izpit.Model;

public class User
{
    private string username;
    private string password;
    private string firstName;
    private string lastName;
    
    public string Username
    {
        get => username;
        set => username = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Password
    {
        get => password;
        set => password = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string FirstName
    {
        get => firstName;
        set => firstName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string LastName
    {
        get => lastName;
        set => lastName = value ?? throw new ArgumentNullException(nameof(value));
    }

    
}