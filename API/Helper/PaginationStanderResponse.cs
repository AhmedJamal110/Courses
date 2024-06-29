namespace API.Helper
{
    public class PaginationStanderResponse<T>
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Date { get; set; }

        public PaginationStanderResponse(int? pageSize, int? pageIndex, int count, IReadOnlyList<T> date)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Date = date;
        }

    
    
    
    }
}
