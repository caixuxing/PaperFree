namespace Trasen.PaperFree.Domain.Shared.Extend
{
    /// <summary>
    /// Oracle Raw互转Guid
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static class GuIdExtend
    {
        /// <summary>
        /// 转GuId
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid ToGuid(this String str)
        {
            try
            {
                if (string.IsNullOrEmpty(str)) return Guid.Empty;
                string data = str.ToString();
                if (!data.Contains("-"))
                {
                    data = data.Substring(6, 2) + data.Substring(4, 2) + data.Substring(2, 2) + data.Substring(0, 2) + data.Substring(10, 2) + data.Substring(8, 2) + data.Substring(14, 2) + data.Substring(12, 2) + data.Substring(16);
                }
                return new Guid(data);
            }
            catch
            {
                return new Guid("11111111-1111-1111-1111-111111111111");
            }
        }

        public static List<Guid> ToGuidList(this List<string> str)
        {
            List<Guid> result = new List<Guid>();
            str.ForEach(item => result.Add(item.ToGuid()));
            return result;
        }
    }
}