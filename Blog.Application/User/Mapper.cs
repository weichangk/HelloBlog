using Blog.Application.User.Dtos;
using Blog.Core.Entities.User;
using Mapster;

namespace Blog.Application.User
{
    public class Mapper : IMapperTag
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<AccountDetailsDto, SysAccount>()
                .Map(s => s.Id, d => d.AccountId);

            config.ForType<AccountDetailsDto, SysUser>()
                .Map(d => d.Id, s => s.SysUserId);
        }
    }
}