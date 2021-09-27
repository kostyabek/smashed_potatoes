namespace CourseWork.Application.Pagination
{
    public class PaginatedQuery
    {
        public const int MaxPageSize = 25;

        private int _pageNumber = 1;

        private int _pageSize = 10;

        public int PageNumber
        {
            get => _pageNumber;
            set => _pageNumber = value <= 0 ? 1 : value;
        }

        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                _pageSize = value > MaxPageSize ? MaxPageSize : value;
            }
        }
    }
}
