using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace AbpTemplate.Extensions
{
    public static class TypeExtensions
    {
        public static string FriendlyId(this Type type, bool fullyQualified = false)
        {
            var typeName = fullyQualified
                ? type.FullNameSansTypeParameters().Replace("+", ".")
                : type.Name;

            if (type.IsGenericType)
            {
                var genericArgumentIds = type.GetGenericArguments()
                    .Select(t => t.FriendlyId(fullyQualified))
                    .ToArray();

                return new StringBuilder(typeName)
                    .Replace(string.Format("`{0}", genericArgumentIds.Count()), string.Empty)
                    .Append(
                        string.Format("[{0}]", string.Join(",", genericArgumentIds).TrimEnd(','))
                    )
                    .ToString();
            }

            return typeName;
        }

        public static string FullNameSansTypeParameters(this Type type)
        {
            var fullName = type.FullName;
            if (string.IsNullOrEmpty(fullName))
                fullName = type.Name;
            var chopIndex = fullName.IndexOf("[[");
            return (chopIndex == -1) ? fullName : fullName.Substring(0, chopIndex);
        }

        public static string[] GetEnumNamesForSerialization(this Type enumType)
        {
            return enumType
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .Select(fieldInfo =>
                {
                    var memberAttribute = fieldInfo
                        .GetCustomAttributes(false)
                        .OfType<EnumMemberAttribute>()
                        .FirstOrDefault();
                    return (
                        memberAttribute == null || string.IsNullOrWhiteSpace(memberAttribute.Value)
                    )
                        ? fieldInfo.Name
                        : memberAttribute.Value;
                })
                .ToArray();
        }
    }
}
