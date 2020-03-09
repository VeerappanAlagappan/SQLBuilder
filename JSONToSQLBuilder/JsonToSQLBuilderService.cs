using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSONToSQLBuilder.ResponseModel;
namespace JSONToSQLBuilder
{
    public class JsonToSQLBuilderService: ISQLBuilder
    {
       /// <summary>
       /// 1.Read and Extract each SQL Objects from the given Json like Tables, Conditional Clauses,Columns etc
       /// 2.Append the SQL Objects in a String Builder and Return the String.
       /// </summary>
       /// <param name="lstSQLObjectExtractor">List of ISQLObjectExtractor class</param>
       /// <param name="strJson">Json String</param>
       /// <returns>String in SQL Format</returns>
        public  string BuildSQLQueryFromJsonString(List<ISQLObjectExtractor> lstSQLObjectExtractor,string strJson)
        {
            try
            {
                StringBuilder objStrBuilder = new StringBuilder();
                foreach (var objFile in lstSQLObjectExtractor)
                {
                    //The SQL objects are extracted from ReadSQLObjectsFromJson using the instances of ISQLObjectExtractor Interface
                    //And the query is build in a String Builder from PrepareQueryBuilder using the instances of IQueryBuilder interface
                    if (CommonInputs.dictSQLQueryBuilder.ContainsKey(objFile.GetType()?.ToString()))
                    {
                        CommonInputs.dictSQLQueryBuilder[objFile.GetType().ToString()]?.PrepareQueryBuilder(objFile?.ReadSQLObjectsFromJson(strJson), ref objStrBuilder);
                    }
                    else
                    {
                        throw new Exception("The Key in the Dictionary for IQueryBuilder object does not exist");
                    }
                }

                return objStrBuilder.ToString();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
       
    }
    public interface ISQLBuilder
    {
         string BuildSQLQueryFromJsonString(List<ISQLObjectExtractor> lstSQLObjectExtractor, string strJson);

    }
}