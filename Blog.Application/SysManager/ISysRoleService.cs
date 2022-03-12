using Blog.Application.SysManager.Dtos;
using Blog.Core.Entities.SysManager;
using Blog.Framework.Result;

namespace Blog.Application.SysManager
{
    public interface ISysRoleService : IAppService<SysRole>
    {
        /// <summary>
        /// 新增/修改角色
        /// </summary>
        /// <param name="dto">角色信息</param>
        /// <returns></returns>
        Task<UnifyResult> Save(SysRoleInputDto dto);
    }
}