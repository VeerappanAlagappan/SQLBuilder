using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Autofac;
using JSONToSQLBuilder;
using Microsoft.SqlServer.TransactSql.ScriptDom;
using System.Linq;
using System.Collections.Generic;

namespace SQLBuilderApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var str = GetJsonFromFile($"{AppContext.BaseDirectory}\\SQLFieldConfigs.Json");
                foreach (var s in str)
                {
                    var sqlBuilder = RegisterServices().Resolve<Application>()?.InvokeSQLBuilder(s);
                    if (IsSQLQueryValid(sqlBuilder))
                    {
                        Console.WriteLine("Valid SQL Query:");
                    }
                    else
                    {
                        Console.WriteLine("Invalid SQL Query:");
                    }
                    Console.WriteLine(sqlBuilder);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Message-{ex.Message}|Exception Stack Trace-{ex.StackTrace}");
            }

            Console.Read();
        }
        /// <summary>
        /// Instatiate Classes as Single Instance
        /// </summary>
        /// <returns></returns>
        private static IContainer RegisterServices()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<JsonToSQLBuilderService>().As<ISQLBuilder>();
            
            builder.RegisterType<QueryExtractor>().As<ISQLObjectExtractor>()
                   .UsingConstructor(typeof(string), typeof(string))
                .WithParameters(new[]
                {
                    new NamedParameter("sqlObjectKeyword", "QueryStatment"),
                    new NamedParameter("jsonType", "Property")
                }).SingleInstance();
            
       
            builder.RegisterType<ColumnExtractor>().As<ISQLObjectExtractor>()
                   .UsingConstructor(typeof(string), typeof(string))
                .WithParameters(new[]
                {
                    new NamedParameter("sqlObjectKeyword", "Columns"),
                    new NamedParameter("jsonType", "Array")
                }).SingleInstance();

               
            builder.RegisterType<JoinTableExtractor>().As<ISQLObjectExtractor>()
                   .UsingConstructor(typeof(string), typeof(string))
                .WithParameters(new[]
                {
                    new NamedParameter("sqlObjectKeyword", "joinColumns"),
                    new NamedParameter("jsonType", "Array")
                }).SingleInstance();
            builder.RegisterType<TableExtractor>().As<ISQLObjectExtractor>()
                   .UsingConstructor(typeof(string), typeof(string))
                .WithParameters(new[]
                {
                    new NamedParameter("sqlObjectKeyword", "Tables"),
                    new NamedParameter("jsonType", "Array")
                }).SingleInstance();
            builder.RegisterType<ConditionalColumnExtractor>().As<ISQLObjectExtractor>()
                   .UsingConstructor(typeof(string), typeof(string))
                .WithParameters(new[]
                {
                    new NamedParameter("sqlObjectKeyword", "conditionalColumns"),
                    new NamedParameter("jsonType", "Array")
                }).SingleInstance();

       //     builder.RegisterType<ConditionalColumnExtractor>().As<ISQLObjectExtractor>()
       //.UsingConstructor(typeof(IObjectParser))
       //.WithParameters(new[]
       //         {
       //             new NamedParameter("objectParser", new ConditionalColumnParser())
                    
       //         }).SingleInstance();

            return builder.Build();
        }

        /// <summary>
        /// Get the Json Content from Config file. Here Multiple Jsons in one file will be processed and stored in a List of String
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>List of Json String</returns>
        private static List<String> GetJsonFromFile(string fileName)
        {
            try
            {
                List<String> lstJsonItem = new List<string>();
                int BracketCount = 0;
                Stream s = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                string ExampleJSON = new StreamReader(s).ReadToEnd().Replace("\n", string.Empty).Replace("\r", string.Empty).Trim();
                StringBuilder Json = new StringBuilder();

                foreach (char c in ExampleJSON)
                {
                    if (c == '{')
                        ++BracketCount;

                    if (c == '}')
                        --BracketCount;


                    if (BracketCount == 0 && c != ' ')
                    {

                        Json.Append(c);
                        lstJsonItem.Add(Json.ToString());
                        Json = new StringBuilder();

                    }
                    else
                    {
                        Json.Append(c);
                    }

                }
                return lstJsonItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Checks the validity of the SQL Query build from the JSON
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool IsSQLQueryValid(string sql)
        {
            TSql100Parser parser = new TSql100Parser(false);
            TSqlFragment fragment;
            IList<ParseError> errors;
            List<string> errorList = null;
            fragment = parser.Parse(new StringReader(sql), out errors);
            if (errors != null && errors.Count > 0)
            {
                errorList = new List<string>();
                foreach (var error in errors)
                {
                    errorList.Add(error.Message);
                }
                
            }
            if (errorList != null && errorList.Count > 0)
                return false;
            return true;
        }
    }
}