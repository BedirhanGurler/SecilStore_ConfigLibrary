using SecilStore.Data.BaseEntity;

namespace SecilStore.Data.Model
{
    public class ConfigurationItem(Guid id) : BaseEntity<Guid>(id)
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
        public bool? IsActive { get; set; }
        public string? ApplicationName { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
