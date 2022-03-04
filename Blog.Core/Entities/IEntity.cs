namespace Blog.Core.Entities
{
    public interface IEntity<TKey>
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        TKey Id { get; set; }
    }
}
