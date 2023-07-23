namespace Domain.Common
{
    public interface IDeletedByEntity
    {
        DateTimeOffset? DeletedOn { get; set; }
        
        bool IsDeleted { get; set; }
    }
}
