using Microsoft.AspNetCore.Mvc;

namespace KamtkSchedule.Infrastructure.Utils
{
    public class PaginatedParameters
    {
        private const int maxPageSize = 100;
        private const int minPageSize = 1;

        [BindProperty(Name = "page")]
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        [BindProperty(Name = "page-size")]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value switch
                {
                    > maxPageSize => maxPageSize,
                    < minPageSize => minPageSize,
                    _ => value,
                };
            }
        }
    }
}
