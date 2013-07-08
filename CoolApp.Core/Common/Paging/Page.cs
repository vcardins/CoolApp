using System.Collections.Generic;
using CoolApp.Core.Interfaces.Paging;
using CoolApp.Core.Models;

namespace CoolApp.Core.Common.Paging
{
    public partial class Page<T> : IPage<T> where T : DomainObject
    {
        public int CurrentPage { get; set; }
        public int PagesCount { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IEnumerable<T> Entities { get; set; }

        public Page(IEnumerable<T> entities, int count, int pageSize, int currentPage)
        {
            this.Entities = entities;
            this.Count = count;
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.PagesCount = count <= pageSize ? 1 : (count / pageSize) + 1;
        }

        public Page()
        {
        }
    } 
}