using SecilStore.ConfigLib;

class Program
{
    static async Task Main(string[] args)
    {
        var reader = new ConfigurationReader(
            connectionString: "Data Source=.;Initial Catalog=SecilStoreConfigLib;Integrated Security=True",
            applicationName: "SERVICE-A",
            intervalInMs: 5000
        );

        Console.WriteLine("Config Reader başlatıldı! 5 saniyede bir DB kontrolü yapılacak değişiklik yansıtılacak.");
        Console.WriteLine("-------------------------");

        while (true)
        {
            try
            {
                string siteName = reader.GetValue<string>("SiteName");
                bool basketEnabled = reader.GetValue<bool>("IsBasketEnabled");
                int maxItemCount = reader.GetValue<int>("MaxItemCount");

                Console.WriteLine($"{DateTime.Now:T} - SiteName: {siteName}, BasketEnabled: {basketEnabled}, MaxItemCount: {maxItemCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
            }

            await Task.Delay(5000);
        }
    }
}
