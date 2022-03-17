using System;
using Blog.Core.Data;

namespace Blog.Core.Entities.Blog
{
    /// <summary>
    /// 时间轴
    /// </summary>
    [Serializable]
    public class TimeLine : Entity<string>, ISoftDelete
    {
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public System.DateTime PublishDate { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool DeleteMark { get; set; } = false;

        /// <summary>
        /// 创建日期
        /// </summary>
        public System.DateTime CreatorTime { get; set; } = DateTime.Now;
    }
}
