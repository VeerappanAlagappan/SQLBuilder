using System;
using System.Collections.Generic;
using System.Text;

namespace JSONToSQLBuilder
{
    public class FromExtractor:ISQLObjectExtractor
    {
        private string _fromKeyword;
        
        public FromExtractor()
        {
            _fromKeyword = "FROM";
            
        }
        public dynamic ReadSQLObjectsFromJson(string json)
        {
            return _fromKeyword;

        }

        public void PrepareQueryBuilder(dynamic queryString, ref StringBuilder queryBuilder)
        {

            queryBuilder.Append($"{queryString}\t");

        }
    }
}
