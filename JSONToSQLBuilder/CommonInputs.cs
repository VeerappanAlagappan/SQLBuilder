using System;
using System.Collections.Generic;
using System.Text;

namespace JSONToSQLBuilder
{
     class CommonInputs
    {
        //public static List<ISQLObjectExtractor> lstSQLObjectExtractor = new List<ISQLObjectExtractor>()
        //{
        //    {new QueryExtractor() },
        //    { new ColumnExtractor() },
        //    {new JoinTableExtractor() },
        //    {new TableExtractor() },
        //    {new ConditionalColumnExtractor() }
           

        //};
        public static Dictionary<string,IQueryBuilder> dictSQLQueryBuilder= new Dictionary<string, IQueryBuilder>()
        {
            { "JSONToSQLBuilder.QueryExtractor",new QueryExtractor(string.Empty,string.Empty)},
            { "JSONToSQLBuilder.ColumnExtractor",new ColumnExtractor(string.Empty,string.Empty)},
            { "JSONToSQLBuilder.JoinTableExtractor",new JoinTableExtractor(string.Empty,string.Empty)},
            { "JSONToSQLBuilder.TableExtractor",new TableExtractor(string.Empty,string.Empty)},
            { "JSONToSQLBuilder.ConditionalColumnExtractor",new ConditionalColumnExtractor(string.Empty,string.Empty)}


        };

        public static Dictionary<IFieldTypeParser, List<string>> dictColumnFieldTypeParser = new Dictionary<IFieldTypeParser, List<string>>()
        {
            {new NumberTypeParser(),new List<string>() { "INT", "NUMBER" }},
            {new StringTypeParser(),new List<string>() { "STRING" }}

        };

        public static List<JoinColumns> lstJoinTables = new List<JoinColumns>();
        public static List<Table> lstTables = new List<Table>();

        internal static class ColumnInputs
        {
            internal static readonly string FIELD_NAME = "fieldName";
            internal static readonly string FIELD_VALUE = "fieldValue";
            internal static readonly string OPERATOR_NAME = "operator";
            internal static Dictionary<string, string> dictOperatorByNames = new Dictionary<string, string>()
            {
                { "EQUAL","="},
                { "==","="},
                { "=","="},
                { ">=",">="},
                { "GTE",">="},
                { "GT",">"},
                { ">",">"},
                { "LTE","<="},
                { "LT","<"},
                { "<","<"},
                { "NOT EQUAL","<>"},
                { "NOTEQUAL","<>"},
                { "!=","<>"},
                {"LIKE","LIKE" }
            };
            internal static Dictionary<string, string> dictFieldValueByNames = new Dictionary<string, string>()
            {
                { "IN","(#PLACE_HOLDER_1#)"},
                { "LIKE","'%#PLACE_HOLDER_1#%'"},
                { "BETWEEN","#PLACE_HOLDER_1# AND #PLACE_HOLDER_2#"}
            };
            

            internal static  List<Type> lstTypesForReplacement = new List<Type>()
            {
                { typeof(Int32)},
                { typeof(Int64)},
                { typeof(Double)}
            };
            

        }
    }
}
