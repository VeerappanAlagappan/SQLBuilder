using System;
using System.Collections.Generic;
using System.Text;

namespace JSONToSQLBuilder
{
    public interface IFieldTypeParser
    {
        string ProcessFieldValueBasedOnTypes(ConditionalColumn conditionalColumn);
    }
}
