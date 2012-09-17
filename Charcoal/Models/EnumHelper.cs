using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Charcoal.Models
{
    public static class EnumHelper
    {
        
        public static List<SelectListItem> GetEnumItems<T>(this T enumType)
        {
            var values = (T[]) Enum.GetValues(typeof (T));
            return values.Select(e => new SelectListItem {Selected = enumType.Equals(e), Text = e.ToString(), Value = e.ToString()}).ToList();
        }

        public static string GetEnumStrings<T>()
        {
            var list= Enum.GetNames(typeof(T));
            var final = "[";
            var lastindex = list.Length - 1;
            for (var i = 0; i < lastindex; i++)
            {
                final += SurroundWithQuotes(list.ElementAt(i)) + ",";
            }
            final += SurroundWithQuotes(list.ElementAt(lastindex)) + "]";
            return final;
        }

        private static string SurroundWithQuotes(string str)
        {
            return "\"" + str + "\"";
        }
    }
}