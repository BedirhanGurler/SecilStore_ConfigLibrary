namespace SecilStore.Common.Results
{
    public interface IGenericResult
    {
        string? Message { get; set; }
        string? Description { get; set; }
        string? Status { get; }
        //bool Result { get; set; }
        ResultStatus ResultStatus { get; set; }
    }
}
