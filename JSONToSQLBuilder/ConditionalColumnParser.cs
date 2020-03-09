using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace JSONToSQLBuilder
{
    public class ConditionalColumnParser : IObjectParser
    {
        /// <summary>
        /// Parse the conditional columns based on the field name, operator name and field value, process them and return as List of Conditional COlumns
        /// </summary>
        /// <param name="lstConditionalColumns">Un processed Json Extracted COnditional Column List</param>
        /// <returns>processed COnditional Column List</returns>
        public dynamic ParseConditionalColumns(dynamic lstConditionalColumns)
        {
            try
            {
                List<ConditionalColumn> conditionalColumns = new List<ConditionalColumn>();
                foreach (var conditionalColumn in lstConditionalColumns)
                {
                    ProcessFieldName(conditionalColumn);
                    ProcessOperatorNames(conditionalColumn);
                    ProcessFieldValue(conditionalColumn);
                    conditionalColumns.Add(conditionalColumn);
                }

                return conditionalColumns;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Process Field Value in Each Conditional Column Based on Field Types.
        /// </summary>
        /// <param name="conditionalColumn">Conditional Column Object With unprocessed Field Value</param>
        /// <returns>Processsed Field Value Conditional Column Object</returns>

        private ConditionalColumn ProcessFieldValue(ConditionalColumn conditionalColumn)
        {
            try
            {
                IFieldTypeParser processFieldType = null;
                if (conditionalColumn.FieldType != null)
                {
                    processFieldType = CommonInputs.dictColumnFieldTypeParser.Where(i => i.Value.Any(j => j.Contains(conditionalColumn.FieldType.ToUpper())))
                        .Select(x => x.Key).FirstOrDefault();

                }
                else
                {
                    processFieldType = CommonInputs.dictColumnFieldTypeParser.Where(i => i.Value.Any(j => j.Contains("STRING")))
                       .Select(x => x.Key).FirstOrDefault();
                }

                if (processFieldType != null)
                {
                    conditionalColumn.FieldValue = processFieldType?.ProcessFieldValueBasedOnTypes(conditionalColumn);
                    return conditionalColumn;
                }


                return null;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        /// <summary>
        /// Process Operator Name in Each Conditional Column Based on Field Types.
        /// </summary>
        /// <param name="conditionalColumn">Conditional Column Object With unprocessed Operator Name</param>
        /// <returns>Processed Conditional Column With Processed Operator Name</returns>
        private ConditionalColumn ProcessOperatorNames(ConditionalColumn conditionalColumn)
        {
            try
            {
                var str = CommonInputs.ColumnInputs.dictOperatorByNames.GetValueForKey(conditionalColumn.OperatorName);
                if (string.IsNullOrEmpty(str))
                {
                    return conditionalColumn;
                }
                else
                {
                    conditionalColumn.OperatorName = str;
                    return conditionalColumn;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        /// <summary>
        /// Process Field Name in Each Conditional Column Based on Field Types.
        /// </summary>
        /// <param name="conditionalColumn">Conditional Column Object With  Field Name</param>
        /// <returns>Conditional Column Object With  Field Name</returns>
        private ConditionalColumn ProcessFieldName(ConditionalColumn conditionalColumn)
        {
            //No Business Logic Evolved, just returning the filed name as is. Logic to be added based on Bsiness Rules in Future.
            return conditionalColumn;
        }
    }

    public interface IObjectParser
    {
        dynamic ParseConditionalColumns(dynamic lstConditionalColumns);
    }
}
