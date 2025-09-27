using Moq;
using SecilStore.Business.Abstract;
using SecilStore.Common.DTOs;
using SecilStore.Common.Results;
using SecilStore.ConfigLib;
using SecilStore.Data.Model;

namespace SecilStore.Test
{
    public class ConfigurationReaderTests
    {
        private Mock<IConfigItemService<ConfigurationItem>> _mockService;
        private ConfigurationReader _reader;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<IConfigItemService<ConfigurationItem>>();

            var configItems = new List<ConfigurationItemDto>
            {
                new ConfigurationItemDto
                {
                    Name = "SiteName",
                    Type = "string",
                    Value = "Qantin"
                },
                new ConfigurationItemDto
                {
                    Name = "IsBasketEnabled",
                    Type = "bool",
                    Value = "true"
                },
                new ConfigurationItemDto
                {
                    Name = "MaxItemCount",
                    Type = "int",
                    Value = "15"
                }
            };

            var configItemData = new ConfigurationItemData
            {
                numberOfAllKeys = 3,
                numberOfActiveKeys = 3,
                data = configItems
            };

            var dataResult = new DataResult<ConfigurationItemData>(ResultStatus.Success, "Success", configItemData);

            _mockService
                .Setup(s => s.GetActiveConfigsByAppAsync("SERVICE-A"))
                .ReturnsAsync(dataResult);

            _reader = new ConfigurationReader(_mockService.Object, "SERVICE-A", 5000);
        }

        [Test]
        public void Test_Read_String_Config()
        {
            var siteName = _reader.GetValue<string>("SiteName");
            Assert.That(siteName, Is.EqualTo("Qantin"));
        }

        [Test]
        public void Test_Read_Bool_Config()
        {
            var isBasketEnabled = _reader.GetValue<bool>("IsBasketEnabled");
            Assert.That(isBasketEnabled, Is.True);
        }

        [Test]
        public void Test_Read_Int_Config()
        {
            var maxItemCount = _reader.GetValue<int>("MaxItemCount");
            Assert.That(maxItemCount, Is.EqualTo(15));
        }
    }
}
