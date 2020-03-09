using System.Collections.Generic;
using System.Text;
using JSONToSQLBuilder;
using FakeItEasy;
using NUnit.Framework;

namespace JsonToSqlBuilder.Test
{
    public class Tests
    {
        
        
        StringBuilder stringBuilder;
        List<ISQLObjectExtractor> lst;
        ISQLBuilder sqlBuilderService;
       [SetUp]
        public void Setup()
        {
        //    stringBuilder = new StringBuilder();
        //    lst = new List<ISQLObjectExtractor>() { { new QueryExtractor("","") } };
        //var s = new Mock<ISQLBuilder>();
        //    s.Setup(i => i.BuildSQLQueryFromJsonString(lst, )).Returns("SELECT");
            //mockIQueryBuilder = new Mock<IQueryBuilder>();
            //mockIQueryBuilder.Setup(i => i.PrepareQueryBuilder(dictSQLQueryBuilder, ref stringBuilder));
        }

        [Test]
        public void BuildSQLQueryFromJsonStringTest()
        {
            string strJson = "{\"QueryStatment\":\"SELECT\",\"Tables\":[\"TABLEA\",\"TABLEB\",\"TABLEC\",\"TABLED\"],\"Columns\":[{\"fieldName\":\"TABLEA.column1\"},{\"fieldName\":\"TABLEA.column2\"},{\"fieldName\":\"TABLEA.column3\"},{\"fieldName\":\"TABLEB.column1\"},{\"fieldName\":\"TABLEB.column2\"}],\"conditionalColumns\":[{\"operator\":\"IN\",\"fieldName\":\"TABLEA.column1\",\"fieldValue\":\"value\"},{\"operator\":\"<>\",\"fieldName\":\"TABLEA.column2\",\"fieldValue\":\"value\"},{\"operator\":\">=\",\"fieldName\":\"TABLEB.column2\",\"fieldValue\":\"value\"},{\"operator\":\"BETWEEN\",\"fieldName\":\"TABLEB.column2\",\"fieldValue\":\"value,value1\"},{\"operator\":\"LIKE\",\"fieldName\":\"TABLEC.column2\",\"fieldValue\":\"value\"}],\"joinColumns\":[{\"joinType\":\"INNER JOIN\",\"joinColumn1\":\"TABLEA.column1\",\"joinColumn2\":\"TABLEB.column1\"},{\"joinType\":\"LEFT JOIN\",\"joinColumn1\":\"TABLEB.column1\",\"joinColumn2\":\"TABLEC.column1\"},{\"joinType\":\"RIGHT JOIN\",\"joinColumn1\":\"TABLEB.column1\",\"joinColumn2\":\"TABLED.column1\"},{\"joinType\":\"INNER JOIN\",\"joinColumn1\":\"TABLED.column1\",\"joinColumn2\":\"TABLEE.column1\"}]}";
                lst = A.Fake<List<ISQLObjectExtractor>>();
            lst.Add(new QueryExtractor("", ""));
            sqlBuilderService = new JsonToSQLBuilderService();
            var result=sqlBuilderService.BuildSQLQueryFromJsonString(lst, strJson);
            Assert.IsNotNull(result);
            Assert.Pass();
        }
    }
}