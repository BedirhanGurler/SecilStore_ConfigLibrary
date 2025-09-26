namespace SecilStore.Common.Results
{
    public interface IDataResult<T> : IGenericResult
    {
        public T? Data { get; set; }
    }
}
