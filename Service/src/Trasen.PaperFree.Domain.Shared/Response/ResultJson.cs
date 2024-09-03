namespace Trasen.PaperFree.Domain.Shared.Response
{
    /// <summary>
    /// 创星授权返回ResultJson基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultJson<T>
    {
        public int Code { get; set; }

        public bool Success { get; set; } = true;

        public string? Msg { get; set; }

        public T? Data { get; set; }

        public int Count { get; set; } = 0;
    }
}