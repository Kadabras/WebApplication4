namespace WebApplication4.Models
{
    public class PaggerViewModel<T>
    {
        public const int AdditionalElementsNearCurrent = 5;
        public int TotalRecordsCount { get; set; }
        public int PerPage { get; set; }
        public int TotalPageCount
        {
            get
            {
                return (int)Math.Ceiling(
                    (decimal)TotalRecordsCount
                    / (decimal)PerPage);
            }
        }
        public int CurrPage { get; set; }
        public List<T> Records { get; set; }
    }
}
