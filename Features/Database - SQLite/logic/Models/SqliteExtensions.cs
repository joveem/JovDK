// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

using System.Linq;
using System.Data.Common;
using System.Reflection;
using System.Text;
using System.Data;

using UnityEngine;
using UnityEngine.UI;
using SystemRandom = System.Random;
using UnityRandom = UnityEngine.Random;

// third
// using DG.Tweening;
// using R3;
// using TMPro;
using Mono.Data.Sqlite;

// from company
using JovDK.Debugging;
using JovDK.SafeActions;
using JovDK.SerializingTools.Json;

// from project
// ...


namespace JovDK.Database.SQLite
{

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class SqliteTableAttribute : Attribute
    {
        public string TableName { get; }

        public SqliteTableAttribute(string tableName)
        {
            TableName = tableName;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class SqliteColumnAttribute : Attribute
    {
        public string ColumnName { get; }
        public bool IsPrimaryKey { get; }

        public SqliteColumnAttribute(string columnName, bool isPrimaryKey = false)
        {
            ColumnName = columnName;
            IsPrimaryKey = isPrimaryKey;
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class SqliteComputedColumnAttribute : Attribute
    {
        public string ColumnName { get; }

        public SqliteComputedColumnAttribute(string columnName)
        {
            ColumnName = columnName;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class SqliteDateTimeDualAttribute : Attribute
    {
        public string ColumnUtcText { get; }
        public string ColumnEpoch { get; }

        public SqliteDateTimeDualAttribute(string columnUtcText, string columnEpoch)
        {
            ColumnUtcText = columnUtcText;
            ColumnEpoch = columnEpoch;
        }
    }

    public static class SqliteQueryBuilder
    {
        public static string GenerateUpsertQuery(object obj)
        {
            Type type = obj.GetType();

            SqliteTableAttribute tableAttr = type.GetCustomAttribute<SqliteTableAttribute>();
            if (tableAttr == null)
                throw new Exception($"Class '{type.Name}' is missing [SqliteTable] attribute.");

            string tableName = tableAttr.TableName;

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            List<string> columnNames = new List<string>();
            List<string> parameterNames = new List<string>();

            foreach (FieldInfo field in fields)
            {
                SqliteDateTimeDualAttribute dualAttr = field.GetCustomAttribute<SqliteDateTimeDualAttribute>();
                if (dualAttr != null)
                {
                    columnNames.Add(dualAttr.ColumnUtcText);
                    columnNames.Add(dualAttr.ColumnEpoch);
                    parameterNames.Add("@" + dualAttr.ColumnUtcText);
                    parameterNames.Add("@" + dualAttr.ColumnEpoch);
                    continue;
                }

                SqliteColumnAttribute columnAttr = field.GetCustomAttribute<SqliteColumnAttribute>();
                if (columnAttr != null)
                {
                    columnNames.Add(columnAttr.ColumnName);
                    parameterNames.Add("@" + columnAttr.ColumnName);
                }
            }

            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (MethodInfo method in methods)
            {
                SqliteComputedColumnAttribute methodAttr = method.GetCustomAttribute<SqliteComputedColumnAttribute>();
                if (methodAttr != null)
                {
                    columnNames.Add(methodAttr.ColumnName);
                    parameterNames.Add("@" + methodAttr.ColumnName);
                }
            }

            if (columnNames.Count == 0)
                throw new Exception($"No mapped columns found in type '{type.Name}'.");

            string insertClause = $"INSERT OR REPLACE INTO {tableName} ({string.Join(", ", columnNames)})";
            string valuesClause = $"VALUES ({string.Join(", ", parameterNames)});";

            string query = $"{insertClause} {valuesClause}";
            return query;
        }
    }

    public static class SqliteColumnHelper
    {
        public static string GetColumnName(this object obj, string fieldName)
        {
            Type type = obj.GetType();
            FieldInfo field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (field == null)
                throw new ArgumentException($"Field '{fieldName}' not found on type {type.Name}");

            SqliteColumnAttribute attribute = field.GetCustomAttribute<SqliteColumnAttribute>();

            if (attribute == null)
                throw new InvalidOperationException($"Field '{fieldName}' does not have SqliteColumnAttribute");

            return attribute.ColumnName;
        }

        public static string GetColumnName(this Type type, string fieldName)
        {
            var field = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);

            if (field == null)
                throw new ArgumentException($"Field '{fieldName}' not found on type {type.FullName}");

            var attribute = field.GetCustomAttribute<SqliteColumnAttribute>();
            if (attribute == null)
                throw new InvalidOperationException($"Field '{fieldName}' does not have SqliteColumnAttribute");

            return attribute.ColumnName;
        }
    }
}
