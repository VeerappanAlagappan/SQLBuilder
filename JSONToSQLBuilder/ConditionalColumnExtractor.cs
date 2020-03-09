using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONToSQLBuilder
{
    public class ConditionalColumnExtractor:SQLObjectExtractor,ISQLObjectExtractor, IQueryBuilder
    {
        private string _conditionalColumnExtractor;
        private string _andKeyword;
        private string _whereKeyword;
        private string _jsonType;
        private IObjectParser _objectParser;
        public ConditionalColumnExtractor(string sqlObjectKeyword, string jsonType):this(new ConditionalColumnParser())
        {
            _conditionalColumnExtractor = sqlObjectKeyword;
            _andKeyword = "AND";
            _whereKeyword = "WHERE";
            _jsonType = jsonType;
        }

        public ConditionalColumnExtractor(IObjectParser objectParser)
        {
            _objectParser = objectParser;
        }
        public dynamic ReadSQLObjectsFromJson(string json)
        {
            try
            {
                var readObject = base.ReadSQLObjectsFromJson(json, _jsonType, _conditionalColumnExtractor) as JToken;
                var lstConditionalColums = (readObject.ToObject<List<ConditionalColumn>>());
               return _objectParser.ParseConditionalColumns(lstConditionalColums);
                  
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Prepare the SQL Conditional Columns in the Query Builder
        /// </summary>
        /// <param name="lstConditionalColumns">List of Conditional Columns extracted from JSON</param>
        /// <param name="queryBuilder">Query Builder constructed with conditions</param>
        public void PrepareQueryBuilder(dynamic lstConditionalColumns, ref StringBuilder queryBuilder)
        {
            try
            {
                if (lstConditionalColumns != null)
                {
                    queryBuilder.Append($"{_whereKeyword}\t");
                    foreach (var conditionalColumn in lstConditionalColumns)
                    {
                        queryBuilder.Append($"{conditionalColumn.FieldName}\t{conditionalColumn.OperatorName}\t{conditionalColumn.FieldValue}\t");
                        if (!(lstConditionalColumns.Count > 1 && lstConditionalColumns.Count - 1 == lstConditionalColumns.IndexOf(conditionalColumn)))
                        {
                            queryBuilder.Append($"{_andKeyword}\t");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

    }
}
