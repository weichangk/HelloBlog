using Blog.Application.SysManager.Dtos;
using Blog.Core.Entities.SysManager;
using Blog.Framework.Result;

namespace Blog.Application.SysManager
{
    public interface ISysModuleService : IAppService<SysModule>
    {
        /// <summary>
        /// 新增/修改菜单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<UnifyResult> Save(SysModuleInputDto dto);

        /// <summary>
        /// 菜单按钮树
        /// </summary>
        /// <returns></returns>
        Task<List<TreeOutputDto>> Tree();
    }
}