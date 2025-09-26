using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecilStore.Data.BaseEntity
{
    public class BaseEntity<TKey>(TKey id) : IEntityBase<TKey>
    {
        [Key]
        [Column(Order = 1)]
        public TKey Id { get; set; } = id;
    }
}
