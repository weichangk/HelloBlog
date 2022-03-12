using Blog.Core.Entities.Blog;
using Blog.Core.Repository;

namespace Blog.Application.Blog
{
    public class ArticleTagsService : AppService<ArticleTags>, IArticleTagsService
    {
        public ArticleTagsService(IAppRepository<ArticleTags> repository) : base(repository)
        {
        }
    }
}