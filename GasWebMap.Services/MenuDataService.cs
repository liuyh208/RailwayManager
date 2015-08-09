using System.Collections.Generic;
using GasWebMap.Services.Dtos;
using ServiceStack.ServiceInterface;

namespace GasWebMap.Services
{
    public class MenuDataService
    {
        public static IList<MenuGroupDto> GetMenus(bool bl)
        {
            //MenuGroupDto group = new MenuGroupDto();
            //group.Text = "测试菜单";

            //for (int i = 0; i < 5; i++)
            //{
            //    var menu = new MenuDto();
            //    menu.Text = "测试" + i;
            //    menu.Url = "测试url" + i;
            //    group.Items.Add(menu);
            //}
            //return new MenuGroupDto[] { group };

            var menuService = new MenuService();

            return menuService.GetGroupDtos(bl);
        }

    }
}