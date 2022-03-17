using Blog.Application.Blog.Dtos;
using Blog.Core.Entities.Blog;
using Blog.Core.Repository;
using Blog.Framework.Generate;
using Blog.Framework.Result;
using Mapster;

namespace Blog.Application.Blog
{
    public class TimeLineService : AppService<TimeLine>, ITimeLineService
    {
        public TimeLineService(IAppRepository<TimeLine> repository) : base(repository)
        {
        }

        /// <summary>
        /// 添加/编辑时间轴
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<UnifyResult> Save(TimeLineInputDto dto)
        {
            var timeLine = dto.Adapt<TimeLine>();
            if (string.IsNullOrWhiteSpace(dto.Id))
            {
                timeLine.Id = SnowflakeId.NextStringId();
                return await InsertAsync(timeLine);
            }
            else
            {
                return await UpdateAsync(timeLine, f => new { f.DeleteMark, f.CreatorTime });
            }
        }
    }
}