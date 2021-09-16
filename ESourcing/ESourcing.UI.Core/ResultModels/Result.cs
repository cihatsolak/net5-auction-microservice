namespace ESourcing.UI.Core.ResultModels
{
    public class Result<TModel> : IResult
    {
        #region Properties
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public TModel Data { get; set; }
        public int TotalCount { get; set; }
        #endregion

        #region Ctors
        public Result(bool isSuccess, string message) : this(isSuccess, message, default)
        {
        }

        public Result(bool isSuccess, string message, TModel data) : this(isSuccess, message, data, 0)
        {
        }

        public Result(bool isSuccess, string message, TModel data, int totalCount)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
            TotalCount = totalCount;
        }
        #endregion
    }
}
