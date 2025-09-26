namespace SecilStore.Common.Results
{
    public class DataResult<T> : IDataResult<T>
    {
        public DataResult()
        {
        }

        public DataResult(ResultStatus success) { }

        public DataResult(T? data)
        {
            Data = data;
        }

        public DataResult(string? message)
        {
            Message = message;
        }

        public DataResult(string? message, T? data)
        {
            Message = message;
            Data = data;
        }

        public DataResult(ResultStatus resultstatus, string message)
        {
            ResultStatus = resultstatus;
            Message = message;
        }

        public DataResult(ResultStatus resultstatus, T? data)
        {
            ResultStatus = resultstatus;
            Data = data;
        }
        public DataResult(ResultStatus resultstatus, string message, T? data)
        {
            ResultStatus = resultstatus;
            Message = message;
            Data = data;
        }

        public T? Data { get; set; }

        public string? Message { get; set; }

        public string? Status
        {
            get { return ResultStatus.ToString(); }
        }

        public bool Result { get; set; }

        public ResultStatus ResultStatus { get; set; }

        public string? Description { get; set; }

    }
}
