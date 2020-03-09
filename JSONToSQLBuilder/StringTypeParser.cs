using System;
using System.Collections.Generic;
using System.Text;

namespace JSONToSQLBuilder
{
    class StringTypeParser : IFieldTypeParser
    {
        /// <summary>
        /// Process Field Values Based on String Type
        /// </summary>
        /// <param name="conditionalColumn">conditional column object with unprocessed Field Value</param>
        /// <returns>Processed Field Value</returns>
        public string ProcessFieldValueBasedOnTypes(ConditionalColumn conditionalColumn)
        {
            try
            {
                var str = CommonInputs.ColumnInputs.dictFieldValueByNames.GetValueForKey(conditionalColumn.OperatorName);
                if (!string.IsNullOrEmpty(str))
                {
                    var splitString = conditionalColumn.FieldValue.SplitString(',');
                    for (int i = 0; i <= splitString.Length; i++)
                    {
                        if (i + 1 == splitString.Length)
                        {
                            if (splitString[i].GetType() == typeof(String))
                            {
                                var result = Convert.ToString(str).Replace("#PLACE_HOLDER_1#", splitString[i]);
                                if (!result.Contains("'"))
                                    return String.Format($"'{result}'");
                                return result;
                            }
                        }
                        if (i + 1 < splitString.Length)
                        {
                            if (splitString[i].GetType() == typeof(String) &&
                                splitString[i + 1].GetType() == typeof(String))
                            {
                                var result = Convert.ToString(str).Replace("#PLACE_HOLDER_1#", splitString[i])
                                    .Replace("#PLACE_HOLDER_2#", splitString[i + 1]);
                                if (!result.Contains("'"))
                                {
                                    return String.Format($"'{result}'");
                                }
                                return result;
                            }
                        }

                    }
                }
                if (!conditionalColumn.FieldValue.Contains("'"))
                    return String.Format($"'{conditionalColumn.FieldValue}'");
                return conditionalColumn.FieldValue;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }

}
