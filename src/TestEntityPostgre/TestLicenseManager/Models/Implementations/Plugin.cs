using Library.DTOs.Abstraction;

namespace TestLicenseManager.Models;

/// <summary>
/// Are we need this table?
/// </summary>
public class Plugin : ModelBase
{
    public string Name        { get; set; }
    public string Description { get; set; }
    public string Version     { get; set; }
}