using Blog.Core.Entities.User;
using Blog.Core.Repository;

namespace Blog.Application.User
{
    public class SysUserService : AppService<SysUser>, ISysUserService
    {
        public SysUserService(IAppRepository<SysUser> repository) : base(repository)
        {
        }
    }
}