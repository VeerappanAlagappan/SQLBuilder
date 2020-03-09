using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSONToSQLBuilder.ResponseModel;
namespace JSONToSQLBuilder
{
    public class ColumnExtractor : SQLObjectExtractor, ISQLObjectExtractor,IQueryBuilder
    {
        private string _columnKeyword;
        private string _fromKeyword;
        private string _jsonType;
        public ColumnExtractor(string sqlObjectKeyword,string jsonType)
        {
            _columnKeyword = sqlObjectKeyword;
            _fromKeyword = "FROM";
            _jsonType = jsonType;
        }
        /// <summary>
        /// Reads adn Extracts the SQL Object from the Json
        /// </summary>
        /// <param name="json">JSON String</param>
        /// <returns>SQL Object</returns>
        public dynamic ReadSQLObjectsFromJson(string json)
        {
            try
            {
                var readObject = base.ReadSQLObjectsFromJson(json, _jsonType, _columnKeyword) as JToken;
                if (readObject != null)
                {
                    return readObject.Select(i => new Columns { Name = Convert.ToString(i.SelectToken(CommonInputs.ColumnInputs.FIELD_NAME)) }).ToList();
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// Prepare the SQL Columns in the Query Builder
        /// </summary>
        /// <param name="lstColumns">List of Columns Extracted from JSON String</param>
        /// <param name="queryBuilder">appends the SQL Query constructed in a String Builder</param>
        public void PrepareQueryBuilder(dynamic lstColumns, ref StringBuilder queryBuilder)
        {
            try
            {
                if (lstColumns != null)
                {
                    foreach (var column in lstColumns)
                    {
                        if (lstColumns.IndexOf(column) == lstColumns.Count - 1)
                        {
                            queryBuilder.Append($"{column.Name}\t");
                        }
                        else
                        {
                            queryBuilder.Append($"{column.Name},");
                        }
                    }
                    queryBuilder.Append($"{_fromKeyword}\t");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
