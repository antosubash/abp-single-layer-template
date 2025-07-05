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
        /// Gets a friendly identifier for the type, optionally including generic type parameters and optionally using only the short name.
        /// </summary>
        /// <param name="type">The type to get a friendly ID for.</param>
        /// <param name="fullyQualified">Whether to include the full namespace.</param>
        /// <param name="shortName">Whether to use only the class name (no namespace). Takes precedence over fullyQualified if true.</param>
        /// <param name="isUnique">A function to check if a given string is unique.</param>
        /// <returns>A friendly string representation of the type.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="type"/> is null.</exception>
        public static string FriendlyId(
            this Type type,
            bool fullyQualified = false,
            bool shortName = true,
            Func<string, bool> isUnique = null
        )
        {
            ArgumentNullException.ThrowIfNull(type);

            // If shortName and isUnique is not provided, make it automatic
            Func<string, bool> autoIsUnique = isUnique;
            if (shortName && isUnique == null)
            {
                var allTypes = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a =>
                    {
                        try { return a.GetTypes(); } catch { return Array.Empty<Type>(); }
                    })
                    .Where(t => t.IsClass || t.IsValueType)
                    .ToList();
                autoIsUnique = name => allTypes.Count(t => t.Name == name) == 1;
            }

            string typeName;
            if (shortName)
            {
                var className = type.Name;
                if (autoIsUnique != null && autoIsUnique(className))
                {
                    typeName = className;
                }
                else if (autoIsUnique != null)
                {
                    // Remove common root namespace (e.g., Volo.CmsKit)
                    var ns = type.Namespace ?? string.Empty;
                    var nsParts = ns.Split('.', StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (nsParts.Count > 2 && nsParts[0] == "Volo" && nsParts[1] == "CmsKit")
                    {
                        nsParts = nsParts.Skip(2).ToList();
                    }
                    // Try adding namespace segments from right to left
                    typeName = null;
                    for (int i = nsParts.Count - 1; i >= 0; i--)
                    {
                        var candidate = string.Concat(nsParts.Skip(i)) + className;
                        if (autoIsUnique(candidate))
                        {
                            typeName = candidate;
                            break;
                        }
                    }
                    // Fallback: fully qualified name with no dots
                    if (typeName == null)
                    {
                        typeName = (type.FullName ?? (ns + "." + className)).Replace(".", "");
                    }
                }
                else
                {
                    // Fallback to previous logic if no uniqueness check is provided
                    var ns = type.Namespace ?? string.Empty;
                    var nsParts = ns.Split('.', StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (nsParts.Count > 2 && nsParts[0] == "Volo" && nsParts[1] == "CmsKit")
                    {
                        nsParts = nsParts.Skip(2).ToList();
                    }
                    // Split class name into PascalCase segments
                    var classParts = new List<string>();
                    int lastIdx = 0;
                    for (int i = 1; i < className.Length; i++)
                    {
                        if (char.IsUpper(className[i]))
                        {
                            classParts.Add(className.Substring(lastIdx, i - lastIdx));
                            lastIdx = i;
                        }
                    }
                    classParts.Add(className.Substring(lastIdx));

                    // Merge nsParts and classParts, removing consecutive duplicates (case-insensitive)
                    var merged = new List<string>();
                    foreach (var part in nsParts.Concat(classParts))
                    {
                        if (
                            merged.Count == 0
                            || !string.Equals(merged.Last(), part, StringComparison.OrdinalIgnoreCase)
                        )
                        {
                            merged.Add(part);
                        }
                    }
                    typeName = string.Join("", merged);
                }
            }
            else if (fullyQualified)
            {
                typeName = type.FullNameSansTypeParameters().Replace("+", ".");
            }
            else
            {
                typeName = type.Name;
            }

            if (!type.IsGenericType)
            {
                return typeName;
            }

            var argIds = type.GetGenericArguments()
                .Select(t => t.FriendlyId(fullyQualified, shortName, autoIsUnique))
                .ToArray();

            return new StringBuilder(typeName)
                .Replace($"`{argIds.Length}", string.Empty)
                .Append($"[{string.Join(",", argIds)}]")
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
