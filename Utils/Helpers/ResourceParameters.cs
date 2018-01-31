namespace Utils.Helpers
{
    public class ResourceParameters<T>
        where T : class, new()
    {
        const int defaultPageNumber = 1;
        const int defaultPageSize = 10;
        const int maxPageSize = 50;

        private int pageNumber = defaultPageNumber;
        private int pageSize = defaultPageSize;

        public T Filters { get; set; } = new T();

        public int PageNumber
        {
            get => pageNumber;
            set => pageNumber = value > 0 ? value : defaultPageNumber;
        }
        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value > 0 ? value : defaultPageSize;
                pageSize = pageSize < maxPageSize ? pageSize : maxPageSize;
            }
        }

        
    }
}
