using Blog.Core.Entities.Blog;
using Blog.Core.Repository;

namespace Blog.Application.Blog
{
    public class ArticleCategoryService : AppService<ArticleCategory>, IArticleCategoryService
    {
        public ArticleCategoryService(IAppRepository<ArticleCategory> repository) : base(repository)
        {
        }
    }
}