using System;

namespace Ecom.Core.Sharing
{
    public class ProductParams
    {
        public string? sort { get; set; }
        public int? categoryId { get; set; }
        public string? Search { get; set; }

        public int MaxPageSize { get; set; } = 6;
        private int _pageSize = 3;
        private int _pageNumber;

        public int pageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public int pageNumber
        {
            get => _pageNumber <= 0 ? 1 : _pageNumber;
            set => _pageNumber = value;
        }
    }
}
