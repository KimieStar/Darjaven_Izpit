namespace Darjaven_Izpit.Model;

public class Tickets
{
    public int Id
    {
        get => id;
        set => id = value;
    }

    public string Eventname
    {
        get => eventname;
        set => eventname = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Userown
    {
        get => userown;
        set => userown = value ?? throw new ArgumentNullException(nameof(value));
    }

    private int id;
    private string eventname;
    private string userown;
}