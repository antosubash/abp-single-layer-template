using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace AbpTemplate.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="Type"/> to provide friendly type identification and serialization utilities.
    /// </summary>
    public static class TypeExtensions
    {
        private static readonly Dictionary<Type, string[]> _enumNamesCache = new();

        /// <summary>
        /// Gets a friendly identifier for the type, optionally including generic type parameters.
        /// </summary>
        /// <param name="type">The type to get a friendly ID for.</param>
        /// <param name="fullyQualified">Whether to include the full namespace.</param>
        /// <returns>A friendly string representation of the type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
        public static string FriendlyId(this Type type, bool fullyQualified = false)
        {
            ArgumentNullException.ThrowIfNull(type);

            var typeName = fullyQualified
                ? type.FullNameSansTypeParameters().Replace("+", ".")
                : type.Name;

            if (!type.IsGenericType)
            {
                return typeName;
            }

            var genericArguments = type.GetGenericArguments();
            if (genericArguments.Length == 0)
            {
                return typeName;
            }

            var genericArgumentIds = genericArguments
                .Select(t => t.FriendlyId(fullyQualified))
                .ToArray();

            return new StringBuilder(typeName)
                .Replace($"`{genericArgumentIds.Length}", string.Empty)
                .Append($"[{string.Join(",", genericArgumentIds)}]")
                .ToString();
        }

        /// <summary>
        /// Gets the full name of the type without generic type parameters.
        /// </summary>
        /// <param name="type">The type to get the full name for.</param>
        /// <returns>The full name without generic type parameters.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
        public static string FullNameSansTypeParameters(this Type type)
        {
            ArgumentNullException.ThrowIfNull(type);

            var fullName = type.FullName;
            if (string.IsNullOrEmpty(fullName))
            {
                fullName = type.Name;
            }

            var chopIndex = fullName.IndexOf("[[", StringComparison.Ordinal);
            return chopIndex == -1 ? fullName : fullName[..chopIndex];
        }

        /// <summary>
        /// Gets enum names for serialization, respecting <see cref="EnumMemberAttribute"/> values.
        /// Results are cached for performance.
        /// </summary>
        /// <param name="enumType">The enum type to get names for.</param>
        /// <returns>Array of enum names suitable for serialization.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="enumType"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown when <paramref name="enumType"/> is not an enum.</exception>
        public static string[] GetEnumNamesForSerialization(this Type enumType)
        {
            ArgumentNullException.ThrowIfNull(enumType);

            if (!enumType.IsEnum)
            {
                throw new ArgumentException(
                    $"Type {enumType.Name} is not an enum.",
                    nameof(enumType)
                );
            }

            // Check cache first
            if (_enumNamesCache.TryGetValue(enumType, out var cachedNames))
            {
                return cachedNames;
            }

            var names = enumType
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)
                .Select(fieldInfo =>
                {
                    var memberAttribute = fieldInfo
                        .GetCustomAttributes<EnumMemberAttribute>(false)
                        .FirstOrDefault();

                    return string.IsNullOrWhiteSpace(memberAttribute?.Value)
                        ? fieldInfo.Name
                        : memberAttribute.Value;
                })
                .ToArray();

            // Cache the result
            lock (_enumNamesCache)
            {
                _enumNamesCache[enumType] = names;
            }

            return names;
        }

        /// <summary>
        /// Gets a simplified type name without namespace, useful for display purposes.
        /// </summary>
        /// <param name="type">The type to get a simple name for.</param>
        /// <returns>The simple name of the type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
        public static string GetSimpleName(this Type type)
        {
            ArgumentNullException.ThrowIfNull(type);
            return type.Name;
        }

        /// <summary>
        /// Checks if the type is a nullable value type.
        /// </summary>
        /// <param name="type">The type to check.</param>
        /// <returns>True if the type is a nullable value type; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
        public static bool IsNullable(this Type type)
        {
            ArgumentNullException.ThrowIfNull(type);
            return Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        /// Gets the underlying type of a nullable type, or the type itself if it's not nullable.
        /// </summary>
        /// <param name="type">The type to get the underlying type for.</param>
        /// <returns>The underlying type or the type itself.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
        public static Type GetUnderlyingType(this Type type)
        {
            ArgumentNullException.ThrowIfNull(type);
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// Gets a human-readable description of the type, including generic constraints.
        /// </summary>
        /// <param name="type">The type to describe.</param>
        /// <returns>A human-readable description of the type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
        public static string GetDescription(this Type type)
        {
            ArgumentNullException.ThrowIfNull(type);

            if (type.IsGenericType)
            {
                var baseName = type.GetGenericTypeDefinition().Name;
                var genericArgs = type.GetGenericArguments()
                    .Select(t => t.GetDescription())
                    .ToArray();

                return $"{baseName}<{string.Join(", ", genericArgs)}>";
            }

            return type.Name;
        }
    }
}
