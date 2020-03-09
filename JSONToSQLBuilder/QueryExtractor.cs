using JSONToSQLBuilder.ResponseModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONToSQLBuilder
{
    public class QueryExtractor : SQLObjectExtractor, ISQLObjectExtractor, IQueryBuilder
    {
        private string _queryKeyword;
        private string _jsonType;
        public QueryExtractor(string sqlObjectKeyword, string jsonType)
        {
            _queryKeyword = sqlObjectKeyword;
            _jsonType = jsonType;
        }
        public dynamic ReadSQLObjectsFromJson(string json)
        {
            try
            {
                var readObject = base.ReadSQLObjectsFromJson(json, _jsonType, _queryKeyword) as JToken;
                if (readObject != null)
                {
                    return Convert.ToString(readObject.Select(i => i.ToString()).FirstOrDefault());
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Prepare the SQL SELECT Statement in the Query Builder
        /// </summary>
        /// <param name="queryString">Select Statement in SQL Query extracted from JSON</param>
        /// <param name="queryBuilder">Query Builder constructed with SELECT Statement</param>
        public void PrepareQueryBuilder(dynamic queryString, ref StringBuilder queryBuilder)
        {
            try
            {
                if (queryString != null)
                    queryBuilder.Append($"{queryString}\t");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
