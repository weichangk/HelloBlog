using Blog.Core.Entities.SysManager;

namespace Blog.Application.SysManager.Dtos
{
    public class SysAllModuleDto
    {
        public List<MenuSettingDto> topMenus { get; set; }

        public Dictionary<string, List<MenuSettingDto>> childMenus { get; set; }

        public Dictionary<string, List<SysButton>> rowButtons { get; set; }

        public Dictionary<string, List<SysButton>> toolButtons { get; set; }
    }
}