
namespace ERP_Domians.RequestParameters
{
    public class RequestParam
    {
        const int maxPageSize = 50;

        public int PageNumber
        {
            get { return _pageNumber; }
            set { _pageNumber = (value <= 0) ? 1 : value; }
        }


        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = (value > maxPageSize) ? maxPageSize : (value <= 0) ? 1 : value; }
        }

        private int _pageSize = 20;

        private int _pageNumber = 1;
    }
}
