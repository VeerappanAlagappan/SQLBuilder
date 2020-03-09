using System;
using System.Collections.Generic;
using System.Text;

namespace JSONToSQLBuilder
{
    public interface IQueryBuilder
    {
        void PrepareQueryBuilder(dynamic sqlObjects, ref StringBuilder queryBuilder);
    }
}
