using SecilStore.ConfigLib;

Console.WriteLine("Merhaba, Bu Seçil Store Case Çalışmasının Test Aşamasıdır");
Console.WriteLine("---------------------------------------------------------");


var reader = new ConfigurationReader(connectionString: "Data Source=.;Initial Catalog=SecilStoreConfigLib;Integrated Security=True", applicationName: "SecilStore.TestConsoleApp", intervalInMs: 5000);
string site = reader.GetValue<string>("SiteName");

Console.WriteLine($"Site Name: {site}");