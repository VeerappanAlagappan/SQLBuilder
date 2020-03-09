using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONToSQLBuilder
{
    public class NumberTypeParser : IFieldTypeParser
    {
        /// <summary>
        /// Process Field Values Based on Number Type
        /// </summary>
        /// <param name="conditionalColumn">conditional column object with unprocessed Field Value</param>
        /// <returns>Processed Field Value returns as String as the Json is string and the Query Builder is string</returns>
        public string ProcessFieldValueBasedOnTypes(ConditionalColumn conditionalColumn)
        {
            try
            {
                var str = CommonInputs.ColumnInputs.dictFieldValueByNames.GetValueForKey(conditionalColumn.OperatorName);
                
                    //If the string is not contained in the dictionary
                    if (!string.IsNullOrEmpty(str))
                {
                    var splitString = conditionalColumn.FieldValue.SplitString(',');
                    for(int i = 0; i <= splitString.Length; i++)
                    {
                        if (i + 1 == splitString.Length)
                        {
                            if (Convert.ToInt32(splitString[i].Replace("\"", "").Replace("'", "")).GetType() == typeof(Int32))
                            {
                                return Convert.ToString(str).Replace("#PLACE_HOLDER_1#", splitString[i].Replace("\"", "").Replace("'", ""));
                            }
                        }
                        if (i + 1 < splitString.Length)
                        {
                            if (Convert.ToInt32(splitString[i].Replace("\"", "").Replace("'", "")).GetType() == typeof(Int32) &&
                                Convert.ToInt32(splitString[i+1].Replace("\"", "").Replace("'", "")).GetType() == typeof(Int32))
                            {
                                return Convert.ToString(str).Replace("#PLACE_HOLDER_1#", splitString[i].Replace("\"", "").Replace("'", ""))
                                    .Replace("#PLACE_HOLDER_2#", splitString[i+1].Replace("\"", "").Replace("'", ""));
                            }
                        }
                    }
                    
                }
              if(Convert.ToInt32(conditionalColumn.FieldValue.Replace("\"", "").Replace("'", "")).GetType() == typeof(Int32))  
                    return conditionalColumn.FieldValue.Replace("\"", "").Replace("'", "");

                return null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

    

    
}
