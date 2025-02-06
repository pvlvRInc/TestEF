namespace Library.DTOs.Abstraction;

public abstract class ModelBase
{
    public int      Id        { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}