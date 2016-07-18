using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Data
{
    /// <summary>
    /// Mock EF DbContext.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MockedDbContext<T> : Mock<T> where T : DbContext
    {
        private Dictionary<string, object> _tables;

        public Dictionary<string, object> Tables
        {
            get { return _tables ?? (_tables = new Dictionary<string, object>()); }
        }
    }
}
