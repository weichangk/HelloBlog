using Blog.Application.Blog.Dtos;
using Blog.Core.Entities.Blog;
using Blog.Framework.Result;

namespace Blog.Application.Blog
{
    public interface IBannerService : IAppService<BannerInfo>
    {
        /// <summary>
        /// 添加、编辑banner
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<UnifyResult> Save(BannerInputDto dto);
    }
}