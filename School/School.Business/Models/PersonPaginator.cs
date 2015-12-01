using School.Domain.Models;
using System.Linq;

namespace School.Business.Models
{
    public class PersonPaginator : Paginator<Person>
    {
        #region ctor
        public PersonPaginator()
            : base()
        {

        }

        public PersonPaginator(int page, int total)
            : base(page, total)
        {

        }
        #endregion

        #region Methods
        public override object DataToJson()
        {
            return from p in this.Data select p.ToJson();
        }
        #endregion
    }
}
