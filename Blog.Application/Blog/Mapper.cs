using Blog.Application.Blog.Dtos;
using Mapster;
using Blog.Core.Entities.Blog;

namespace Blog.Application.Blog
{
    /// <summary>
    /// 配置映射
    /// </summary>
    public class Mapper : IMapperTag
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<ArticleInfo, ArticleInputDto>()
                .Ignore(x => x.Tags)
                .Ignore(x => x.Categories);
        }
    }
}