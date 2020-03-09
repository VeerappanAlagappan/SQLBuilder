using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JSONToSQLBuilder
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Extension Method that Get Values for the given Key in a Dictionary
        /// </summary>
        /// <typeparam name="TKey">Generic Key type in Dict</typeparam>
        /// <typeparam name="TValue">Generic Value type in Dict</typeparam>
        /// <param name="dictionaryInstance">Dictionary of TKey and TValue which calls this Extension Method</param>
        /// <param name="key">Intended Key for searching in the dictionary</param>
        /// <returns>Specifc Vlaue fo the intended Key</returns>
        public static TValue GetValueForKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionaryInstance, TKey key)
        {
            var keyString = key as string;
            //Check for TKey as String Type to ignore the case
            if (keyString != null)
            {
                if (
                dictionaryInstance.Keys.OfType<string>()
                .Any(k => string.Equals(k, keyString, StringComparison.OrdinalIgnoreCase)))
                {
                    return dictionaryInstance[(dictionaryInstance.Keys.OfType<string>()
                        .Where(k => string.Equals(k, keyString, StringComparison.OrdinalIgnoreCase))
                        .OfType<TKey>()
                        .Select(i => i).FirstOrDefault())];
                }
            }
            //Typical contains key logic to get the value for remaining types
            else
            {
                
                if (dictionaryInstance.ContainsKey(key))
                {
                    return dictionaryInstance[key];
                }
            }

            return default(TValue);
        }

        /// <summary>
        /// Extension Method for Splitting the String with specific Delimeter. 
        /// This extension method is customized to get a string array even for not matching delimter
        /// </summary>
        /// <param name="str">string that calls this method</param>
        /// <param name="delimeter">Delimter used for split condition</param>
        /// <returns>String array after splitting the string</returns>
        public static string[] SplitString(this string str, char delimeter)
        {
            string[] stringArr = str.Split(delimeter);
            if (stringArr.Length <= 1)
            {
                return new string[] { str };
            }
            else
                return stringArr;
        }


    }
}
