using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eLearn.Domain.Core
{
    [ExcludeFromCodeCoverage]
    public class PageResult<TEntity> where TEntity : class
    {
        public int Total { get; set; }

        public IEnumerable<TEntity> Items { get; set; }
    }
}
