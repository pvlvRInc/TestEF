namespace LicenseManagerClient.Extensions;

public static class StringExtensions
{
    public static string ToNiceJson(this string str) => 
        str.Replace("{", "{\n").Replace("}", "\n}").Replace(",",",\n");
}