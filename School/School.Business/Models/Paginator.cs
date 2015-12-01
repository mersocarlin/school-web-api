using System.Collections.Generic;

namespace School.Business.Models
{
    public abstract class Paginator<T> where T : class
    {
        #region Ctor
        public Paginator()
        {
            this.Data = new List<T>();
        }

        public Paginator(int page, int total)
            : this()
        {
            this.Page = page;
            this.Total = total;
        }
        #endregion

        #region Properties
        public int Page { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }
        #endregion

        #region Methods
        public abstract object DataToJson();

        public object ToJson()
        {
            return new
            {
                Page = this.Page,
                Total = this.Total,
                Data = this.DataToJson()
            };
        }
        #endregion
    }
}
