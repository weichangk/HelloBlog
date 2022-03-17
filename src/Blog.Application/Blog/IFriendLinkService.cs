using Blog.Application.Blog.Dtos;
using Blog.Core.Entities.Blog;
using Blog.Framework.Result;

namespace Blog.Application.Blog
{
    public interface IFriendLinkService : IAppService<FriendLink>
    {
        /// <summary>
        /// 新增/编辑友情链接
        /// </summary>
        /// <param name="link">友情链接信息</param>
        /// <returns></returns>
        Task<UnifyResult> Save(FriendLinkInputDto link);
    }
}