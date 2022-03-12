using Blog.Application.SysManager.Dtos;
using Blog.Core.Entities.SysManager;
using Blog.Core.Repository;
using Blog.Framework.Generate;
using Blog.Framework.Result;
using Mapster;

namespace Blog.Application.SysManager
{
    public class SysRoleService : AppService<SysRole>, ISysRoleService
    {
        public SysRoleService(IAppRepository<SysRole> repository) : base(repository)
        {
        }

        /// <summary>
        /// 新增/修改角色
        /// </summary>
        /// <param name="dto">角色信息</param>
        /// <returns></returns>
        public async Task<UnifyResult> Save(SysRoleInputDto dto)
        {
            var sysRole = dto.Adapt<SysRole>();
            if (await Repository.AnyAsync(c => c.EnCode == sysRole.EnCode && c.Id != sysRole.Id))
            {
                return "角色编码已存在";
            }
            if (string.IsNullOrWhiteSpace(sysRole.Id))
            {
                sysRole.Id = SnowflakeId.NextStringId();
                return await InsertAsync(sysRole);
            }
            else
            {
                return await UpdateAsync(sysRole, i => new { i.CreatorTime, i.CreatorAccountId, i.DeleteMark });
            }
        }
    }
}