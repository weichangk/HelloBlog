using System.ComponentModel;
using Blog.Application;
using Blog.Application.Blog;
using Blog.Application.Blog.Dtos;
using Blog.Core.Entities.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Hosting.Areas.BlogManage.Controllers
{
    [Area("BlogManage")]
    public class TimeLineController : AdminController
    {
        private readonly ITimeLineService _timeLineService;
        public TimeLineController(ITimeLineService timeLineService)
        {
            _timeLineService = timeLineService;
        }

        [Description("时光轴列表")]
        [HttpPost]
        public IActionResult Index(PageQueryInputDto query)
        {
            return Json(_timeLineService.GetListByPage(query), "yyyy-MM-dd HH:mm:ss");
        }

        [Description("添加/修改时光轴")]
        [HttpPost]
        public async Task<IActionResult> Form(TimeLineInputDto timeLine)
        {
            return Json(await _timeLineService.Save(timeLine));
        }

        [AllowAccess]
        [Description("时光轴详情")]
        public async Task<IActionResult> Detail(string key)
        {
            return Json(await _timeLineService.FindEntityAsync(key), "yyyy-MM-dd HH:mm:ss");
        }

        [Description("删除时光轴")]
        public async Task<IActionResult> Delete(string key)
        {
            return Json(await _timeLineService.UpdateAsync(f => new TimeLine() { DeleteMark = true }, c => c.Id == key));
        }
    }
}