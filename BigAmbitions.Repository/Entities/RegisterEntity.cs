namespace BigAmbitions.Repository.Entities;
public abstract class RegisterEntity
{
    public int Id { get; set; }
    public DateTime InsertDatetime { get; set; }
    public DateTime? UpdateDatetime { get; set; }
}
