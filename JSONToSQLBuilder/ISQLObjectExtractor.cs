using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace JSONToSQLBuilder
{
    public interface ISQLObjectExtractor 
    {
        dynamic ReadSQLObjectsFromJson(string jsonString);

       
    }

  
}
