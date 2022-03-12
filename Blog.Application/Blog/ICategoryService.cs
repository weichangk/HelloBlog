using Blog.Application.Blog.Dtos;
using Blog.Core.Entities.Blog;
using Blog.Framework.Result;

namespace Blog.Application.Blog
{
    public interface ICategoryService : IAppService<CategoryInfo>
    {
        /// <summary>
        /// 新增修改栏目
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<UnifyResult> Save(CategoryInputDto dto);

        /// <summary>
        /// 获取所有一级分类
        /// </summary>
        /// <returns></returns>
        Task<List<CategoryDto>> GetRootCategories();
    }
}