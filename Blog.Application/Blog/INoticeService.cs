using Blog.Application.Blog.Dtos;
using Blog.Core.Entities.Blog;
using Blog.Framework.Result;

namespace Blog.Application.Blog
{
    public interface INoticeService : IAppService<Noticeinfo>
    {
        /// <summary>
        /// 添加/编辑通知
        /// </summary>
        /// <param name="dto">通知信息</param>
        /// <returns></returns>
        Task<UnifyResult> Save(NoticeInputDto dto);
    }
}