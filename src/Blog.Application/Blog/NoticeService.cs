using Blog.Application.Blog.Dtos;
using Blog.Core.Entities.Blog;
using Blog.Core.Repository;
using Blog.Framework.Generate;
using Blog.Framework.Result;
using Mapster;

namespace Blog.Application.Blog
{
    public class NoticeService : AppService<Noticeinfo>, INoticeService
    {
        public NoticeService(IAppRepository<Noticeinfo> repository) : base(repository)
        {
        }

        /// <summary>
        /// 添加/编辑通知
        /// </summary>
        /// <param name="dto">通知信息</param>
        /// <returns></returns>
        public async Task<UnifyResult> Save(NoticeInputDto dto)
        {
            var noticeinfo = dto.Adapt<Noticeinfo>();
            if (string.IsNullOrWhiteSpace(noticeinfo.Id))
            {
                noticeinfo.Id = SnowflakeId.NextStringId();
                return await InsertRemoveCacheAsync(noticeinfo);
            }
            else
            {
                return await UpdateRemoveCacheAsync(noticeinfo, f => new { f.DeleteMark, f.CreatorTime });
            }
        }
    }
}