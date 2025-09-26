using SecilStore.Data.BaseEntity;
using System.Linq.Expressions;

namespace SecilStore.Data.Abstract
{
    public interface IBaseDal<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        /// <summary>
        /// İlgili tabloda kayıtlı olan bütün verileri getirir.
        /// </summary>
        /// <returns></returns>
        IEnumerable<TEntity> Select();
        /// <summary>
        /// İlgili tabloda şartlara göre kayıtlı olan verileri getirir.
        /// </summary>
        /// <param name="predicates">şartlar</param>
        /// <returns></returns>
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>>[] predicates);
        /// <summary>
        /// İlgili tabloda şartlara göre veya şartsız kayıtlı olan verileri asenkron getirir.
        /// </summary>
        /// <param name="predicates">şartlar</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> SelectAsync(params Expression<Func<TEntity, bool>>[] predicates);
        /// <summary>
        /// İlgili tablodan şartlara göre senkron olarak tek bir veri getirir.
        /// </summary>
        /// <param name="filter">şartlar</param>
        /// <returns></returns>
        TEntity? Get(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// İlgili tablodan şartlara göre asenkron olarak tek bir veri getirir.
        /// </summary>
        /// <param name="filter">şartlar</param>
        /// <returns></returns>
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// İlgili tablodan şartlara göre veri olup olmadığını bilgisini verir.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> HasAnyData(Expression<Func<TEntity, bool>> filter);
        /// <summary>
        /// İlgili tabloda ilişkili olan tablonun verisini de alarak sorgu çalıştırıldığında verileri yükler ve hafızaya alır.
        /// </summary>
        /// <param name="children">Dahil edilecek ilişkili tablo ismi</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAllWithEagerLoad(string[] children);
        /// <summary>
        /// İlgili tabloda ilişkili olan tablonun verisini şartlara göre alarak sorgu çalıştırıldığında verileri yükler ve hafızaya alır.
        /// </summary>
        /// <param name="predicates">Şartlar</param>
        /// <param name="children">Dahil edilecek ilişkili tablo ismi</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAllWithEagerLoad(Expression<Func<TEntity, bool>>[]? predicates, string[] children);
        /// <summary>
        /// İlgili tabloda id'si verilen kayıtlı olan veriyi getirir.
        /// </summary>
        /// <param name="key">id</param>
        /// <returns></returns>
        TEntity? SelectWithId(TKey key);
        /// <summary>
        /// İlgili tabloda id'si verilen kayıtlı olan veriyi asenkron getirir.
        /// </summary>
        /// <param name="key">id</param>
        /// <returns></returns>
        Task<TEntity?> SelectWithIdAsync(TKey key);
        /// <summary>
        /// İlgili tabloda veriyi kaydeder.
        /// </summary>
        /// <param name="entity">model</param>
        /// <returns></returns>
        TEntity Insert(TEntity entity);
        /// <summary>
        /// İlgili tabloda veriyi asenkron olarak kaydeder.
        /// </summary>
        /// <param name="entity">model</param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);
        /// <summary>
        /// İlgili tablodaki veri listesini asenkron olarak kaydeder.
        /// </summary>
        /// <param name="entity">model</param>
        /// <returns></returns>
        Task<List<object>> InsertListAsync(List<object> entity);
        /// <summary>
        /// İlgili tablodaki veriyi günceller.
        /// </summary>
        /// <param name="key">id</param>
        /// <param name="entity">model</param>
        /// <returns></returns>
        TEntity? Update(TKey key, TEntity entity);
        /// <summary>
        /// İlgili tablodaki veriyi asenkron olarak günceller.
        /// </summary>
        /// <param name="key">id</param>
        /// <param name="entity">model</param>
        /// <returns></returns>
        Task<TEntity?> UpdateAsync(TKey? key, TEntity entity);
        /// <summary>
        /// İlgili tablodaki veriyi kalıcı olarak siler.
        /// </summary>
        /// <param name="key">id</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(TKey key);
        /// <summary>
        /// İlgili tablodaki veriyi pasif hale getirir.
        /// </summary>
        /// <param name="key">id</param>
        /// <returns></returns>
        //Task<bool> PacifyAsync(TKey key);
    }
}

