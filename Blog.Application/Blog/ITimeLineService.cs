using Blog.Application.Blog.Dtos;
using Blog.Core.Entities.Blog;
using Blog.Framework.Result;

namespace Blog.Application.Blog
{
    public interface ITimeLineService : IAppService<TimeLine>
    {
        /// <summary>
        /// 添加/编辑时间轴
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<UnifyResult> Save(TimeLineInputDto dto);
    }
}