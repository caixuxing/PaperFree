namespace Trasen.PaperFree.Application.Dto
{
    /// <summary>
    /// 下拉选择框公共Dto
    /// </summary>
    public class DropSelectDto<T>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public T Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}