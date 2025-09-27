using SecilStore.Business.Abstract;
using SecilStore.Data.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecilStore.Library
{
    /// <summary>
    /// Konfigürasyon cache yönetimi ve periyodik yenileme servisi.
    /// </summary>
    public class ConfigurationService
    {
        private readonly IConfigItemService<ConfigurationItem> _repository;
        private readonly string _applicationName;
        private readonly int _intervalInMs;
        private readonly ConcurrentDictionary<string, string> _cache;
        private Timer _timer;

        public ConfigurationService(IConfigItemService<ConfigurationItem> repository, string applicationName, int intervalInMs)
        {
            _repository = repository;
            _applicationName = applicationName;
            _intervalInMs = intervalInMs;
            _cache = new ConcurrentDictionary<string, string>();

            // İlk senkron yükleme
            Task.Run(async () => await ReloadConfigs()).GetAwaiter().GetResult();

            StartTimer();
        }

        private void StartTimer()
        {
            _timer = new Timer(async _ => await ReloadConfigs(), null, 0, _intervalInMs);
        }

        private async Task ReloadConfigs()
        {
            var result = await _repository.GetActiveConfigsByAppAsync(_applicationName);

            if (result.Data?.data != null)
            {
                foreach (var config in result.Data.data)
                {
                    _cache[config.Name] = config.Value;
                }
            }
        }

        public T GetValue<T>(string key)
        {
            if (_cache.TryGetValue(key, out var value))
            {
                try
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
                catch
                {
                    throw new InvalidCastException($"Config key '{key}' cannot be converted to {typeof(T).Name}");
                }
            }

            throw new KeyNotFoundException($"Key not found: {key}");
        }
    }
}
