namespace SecilStore.Common.DTOs
{
    public class ConfigurationItemData
    {
        public int numberOfAllKeys { get; set; }
        public int numberOfActiveKeys { get; set; }
        public List<ConfigurationItemDto>? data { get; set; } = new List<ConfigurationItemDto>();
        public ConfigurationItemData() { }
    }
    public class ConfigurationItemDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
        public bool? IsActive { get; set; }
        public string? ApplicationName { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
