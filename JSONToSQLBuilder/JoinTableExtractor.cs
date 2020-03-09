using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONToSQLBuilder
{
    public class JoinTableExtractor : SQLObjectExtractor, ISQLObjectExtractor, IQueryBuilder
    {
        private string _joinKeyword;
        private string _jsonType;

        public JoinTableExtractor(string sqlObjectKeyword, string jsonType)
        {
            _joinKeyword = sqlObjectKeyword;
            _jsonType = jsonType;


        }


        public dynamic ReadSQLObjectsFromJson(string json)
        {

            try
            {
                var readObject = base.ReadSQLObjectsFromJson(json, _jsonType, _joinKeyword) as JToken;
                if (readObject != null)
                {
                    CommonInputs.lstJoinTables = readObject.Select(i => new JoinColumns()
                    {
                        JoinType = Convert.ToString(i.SelectToken("joinType")),
                        JoinColumn1 = Convert.ToString(i.SelectToken("joinColumn1")),
                        JoinColumn2 = Convert.ToString(i.SelectToken("joinColumn2"))
                    }).ToList();
                    return CommonInputs.lstJoinTables;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        /// <summary>
        /// Prepare the SQL JOIN Tables in the Query Builder
        /// </summary>
        /// <param name="lstJoinTables">List of Join Tables and Columns extracted from JSON</param>
        /// <param name="queryBuilder">Query Builder constructed with JOINS</param>
        public void PrepareQueryBuilder(dynamic lstJoinTables, ref StringBuilder queryBuilder)
        {
            try
            {
                if (lstJoinTables != null)
                {
                    foreach (var joinColumnNames in lstJoinTables)
                    {
                        var table1 = joinColumnNames.JoinColumn1.Split('.')[0].ToString();
                        var table2 = joinColumnNames.JoinColumn2.Split('.')[0].ToString();

                        if (!queryBuilder.ToString().Trim().ToUpper().Contains($"\t{table1.ToUpper()}\t") && !queryBuilder.ToString().Trim().ToUpper().Contains($"\t{table2.ToUpper()}\t"))
                            queryBuilder.Append($"{table1}\t{joinColumnNames.JoinType}\t{table2}\tON {joinColumnNames.JoinColumn1}\t = {joinColumnNames.JoinColumn2}\t");
                        else if (!queryBuilder.ToString().Trim().ToUpper().Contains($"\t{table1.ToUpper()}\t"))
                            queryBuilder.Append($"{joinColumnNames.JoinType}\t{table1}\tON {joinColumnNames.JoinColumn1}\t = {joinColumnNames.JoinColumn2}\t");
                        else if (!queryBuilder.ToString().Trim().ToUpper().Contains($"\t{table2.ToUpper()}\t"))
                            queryBuilder.Append($"{joinColumnNames.JoinType}\t{table2}\tON {joinColumnNames.JoinColumn1}\t = {joinColumnNames.JoinColumn2}\t");


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
