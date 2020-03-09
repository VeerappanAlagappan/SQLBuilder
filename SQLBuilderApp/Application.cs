using System;
using System.Collections.Generic;
using System.Text;
using JSONToSQLBuilder;
using System.Linq;
using Autofac.Features.Indexed;

namespace SQLBuilderApp
{
    class Application
    {
        protected readonly ISQLBuilder _sqlBuilder;
        protected readonly List<ISQLObjectExtractor> _sQLObjectExtractor;
        
        
        public Application(ISQLBuilder sqlBuilder, IEnumerable<ISQLObjectExtractor> sQLObjectExtractor)
        {
            _sqlBuilder = sqlBuilder; 
            _sQLObjectExtractor = (sQLObjectExtractor).Cast<ISQLObjectExtractor>().ToList();
            


        }

        /// <summary>
        /// Invoke the Build SQL Query method from JSONToSQLBuilder Library with Json as String
        /// </summary>
        /// <param name="json">Json in String Type</param>
        /// <returns></returns>
        public string InvokeSQLBuilder(string json)
        {
            try
            {
                return _sqlBuilder?.BuildSQLQueryFromJsonString(_sQLObjectExtractor, json);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
