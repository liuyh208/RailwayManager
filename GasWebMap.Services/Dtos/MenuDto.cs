using System.Collections.Generic;

namespace GasWebMap.Services.Dtos
{
    public class MenuDto
    {
        public string Text { get; set; }
        public string Url { get; set; }

        public string Image { get; set; }
    }

    public class MenuGroupDto
    {
        public MenuGroupDto()
        {
            Items = new List<MenuDto>();
        }

        public string Text { get; set; }

        public IList<MenuDto> Items { get; set; }
    }
}