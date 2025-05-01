namespace Deployer_Web.Data
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string? Note { get; set; }
    }
}
