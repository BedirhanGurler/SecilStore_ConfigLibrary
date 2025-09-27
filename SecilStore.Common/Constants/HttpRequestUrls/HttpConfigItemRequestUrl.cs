namespace SecilStore.Common.Constants.HttpRequestUrls
{
    public class HttpConfigItemRequestUrl
    {
        public const string CreateConfigItemUrl = "/secil-store/config-item";
        public const string GetConfigItemByApplicationNameUrl = "/secil-store/config-item/{appname}";
        public const string GetAllActivesUrl = "/secil-store/config-item/list";
        public const string GetById = "/secil-store/config-item/item/{id}";
        public const string Delete = "/secil-store/config-item/passives";
    }
}
