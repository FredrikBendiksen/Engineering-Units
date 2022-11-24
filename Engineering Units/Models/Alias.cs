namespace Engineering_Units.Models;

public class Alias
{
    public Alias(string aliasName, string uomName)
    {
        AliasName = aliasName;
        UOMName = uomName;
    }

    public string AliasName { get; set; }

    public string UOMName { get; set; }
}
