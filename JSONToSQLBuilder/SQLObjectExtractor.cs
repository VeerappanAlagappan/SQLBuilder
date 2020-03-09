using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONToSQLBuilder
{
    public class SQLObjectExtractor
    {
        /// <summary>
        /// Read SQL Objects from JSON based on the JSON Type like JProperty or JArray and with SQL objects like Table,Columns etc
        /// </summary>
        /// <param name="jsonString">Json String</param>
        /// <param name="jsonType">Json Type like JProperty,JArray</param>
        /// <param name="keyword">Keyword that has SQL Objects like Table,Columns, COnditional Columns</param>
        /// <returns></returns>
        public dynamic ReadSQLObjectsFromJson(string jsonString, string jsonType, string keyword)
        {
            try
            {
                if (Enum.TryParse(jsonType, out JTokenType myToken))
                {
                    return (GetChildren(JObject.Parse(jsonString))
                     .FirstOrDefault(c => c.Type == myToken && c.Path.Contains(keyword)));
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private static IEnumerable<JToken> GetChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;
                foreach (var cc in GetChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}
