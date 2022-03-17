using Blog.Application.SysManager.Dtos;
using Blog.Core.Entities.SysManager;
using Blog.Framework.Result;

namespace Blog.Application.SysManager
{
    public interface ISysButtonService : IAppService<SysButton>
    {
        /// <summary>
        /// 新增/修改按钮
        /// </summary>
        /// <param name="dto">按钮实体</param>
        /// <returns></returns>
        Task<UnifyResult> Save(SysButtonInputDto dto);
    }
}