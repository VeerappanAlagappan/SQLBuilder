using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONToSQLBuilder
{
    public class TableExtractor:SQLObjectExtractor,ISQLObjectExtractor, IQueryBuilder
    {
        private string _tableKeyword;
        private string _jsonType;
       
        public TableExtractor(string sqlObjectKeyword, string jsonType)
        {
            _tableKeyword = sqlObjectKeyword;
            _jsonType = jsonType;
           
        }

        
        public dynamic ReadSQLObjectsFromJson(string json)
        {

            try
            {
                var readObject = base.ReadSQLObjectsFromJson(json, _jsonType, _tableKeyword) as JToken;
                if (readObject != null)
                {
                    return readObject.Select(i => new Table() { Name = i.ToString() }).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        /// <summary>
        /// Prepare the Query Builder with tables
        /// </summary>
        /// <param name="lstTables">List of Tables extracted from Json String</param>
        /// <param name="queryBuilder">Query Builder constructed with tables</param>
        public void PrepareQueryBuilder(dynamic lstTables, ref StringBuilder queryBuilder)
        {
            try
            {
                if (lstTables != null)
                {
                    if (CommonInputs.lstJoinTables.Count <= 0)
                    {

                        if (lstTables.Count > 0)
                        {
                            queryBuilder.Append($"{lstTables[0].Name}\t");
                        }

                    }
                    else
                    {
                        CommonInputs.lstJoinTables = new List<JoinColumns>();
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
