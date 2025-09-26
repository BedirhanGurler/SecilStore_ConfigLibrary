using System.Text.Json.Serialization;

namespace SecilStore.Common.Results
{
    internal class GenericResult : IGenericResult
    {
        public GenericResult() { }
        public GenericResult(ResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
        }

        public GenericResult(string message)
        {
            Description = message;
        }

        //public GenericResult(ResultStatus resultStatus, bool result, string message)
        //{
        //    ResultStatus = resultStatus;
        //    Result = result;
        //    Description = message;
        //}

        public GenericResult(ResultStatus resultStatus, string message)
        {
            ResultStatus = resultStatus;
            Description = message;
        }

        public string? Description { get; set; }

        public string Status
        {
            get { return ResultStatus.ToString(); }
        }


        //public bool Result { get; set; }

        [JsonIgnore]
        public ResultStatus ResultStatus { get; set; }

        [JsonIgnore]
        public string? Message { get; set; }
    }
}
