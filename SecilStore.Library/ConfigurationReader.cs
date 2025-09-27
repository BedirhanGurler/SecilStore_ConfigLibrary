using SecilStore.Business.Abstract;
using SecilStore.Business.Concrete;
using SecilStore.Data.Model;
using SecilStore.Library;

namespace SecilStore.ConfigLib
{
    public class ConfigurationReader
    {
        private readonly ConfigurationService _service;

        public ConfigurationReader(string applicationName, string connectionString, int intervalInMs)
        {
            // connectionString parametresini sadece formalite olarak alıyoruz,
            // çünkü BaseDal kendi factory’sinden context üretmekte.
            // İleride context injection’a geçilirse buradan kullanılabilir.

            var repository = new ConfigItemService<ConfigurationItem>();
            _service = new ConfigurationService(repository, applicationName, intervalInMs);
        }

        //// Testlerde kullanılacak overload constructor
        //public ConfigurationReader(IConfigItemService<ConfigurationItem> repository, string applicationName, int intervalInMs)
        //{
        //    _service = new ConfigurationService(repository, applicationName, intervalInMs);
        //}


        /// <summary>
        /// Config değerini generic tip dönüşüyle getirir.
        /// </summary>
        public T GetValue<T>(string key) => _service.GetValue<T>(key);

        /// <summary>
        /// Config değerini bulamazsa default değer döner.
        /// </summary>
        public T GetValueOrDefault<T>(string key, T defaultValue = default!)
        {
            try
            {
                return GetValue<T>(key);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
