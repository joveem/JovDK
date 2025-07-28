// system / unity
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Reflection;
using System.Threading.Tasks;
using JovDK.Debugging;
using JovDK.SerializingTools.Json;



// third
using Mono.Data.Sqlite;

// from company
// ...

// from project
// ...


namespace JovDK.Database.SQLite
{
    public class DbConnection
    {

        // state
        string _connectionString = "UNDEFINED";

        public DbConnection(string connectionString)
        {
            _connectionString = connectionString;
        }


        #region Controller
        public static DbConnection With(string connectionString)
        {
            DbConnection dbConnection = new DbConnection(connectionString);
            return dbConnection;
        }
        #endregion Controller

        #region Controller - CRUD
        public async Task Upsert<T>(T obj)
        {
            Type type = typeof(T);
            SqliteTableAttribute tableAttr = type.GetCustomAttribute<SqliteTableAttribute>();
            if (tableAttr == null)
                throw new Exception($"Class '{type.Name}' is missing [SqliteTable] attribute.");

            string tableName = tableAttr.TableName;

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            List<string> columnNames = new List<string>();
            List<string> parameterNames = new List<string>();
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            string primaryKeyColumn = null;

            foreach (FieldInfo field in fields)
            {
                SqliteColumnAttribute columnAttr = field.GetCustomAttribute<SqliteColumnAttribute>();
                if (columnAttr != null)
                {
                    object value = field.GetValue(obj) ?? DBNull.Value;

                    if (value is Guid guidValue)
                        value = guidValue.ToString();

                    columnNames.Add(columnAttr.ColumnName);
                    parameterNames.Add("@" + columnAttr.ColumnName);
                    parameters.Add("@" + columnAttr.ColumnName, value);

                    if (columnAttr.IsPrimaryKey)
                        primaryKeyColumn = columnAttr.ColumnName;

                    continue;
                }

                SqliteDateTimeDualAttribute dualAttr = field.GetCustomAttribute<SqliteDateTimeDualAttribute>();
                if (dualAttr != null)
                {
                    DateTime dateTimeValue = (DateTime)field.GetValue(obj);
                    string utcString = dateTimeValue.ToUniversalTime().ToString("o");
                    long epoch = new DateTimeOffset(dateTimeValue.ToUniversalTime()).ToUnixTimeSeconds();

                    columnNames.Add(dualAttr.ColumnUtcText);
                    columnNames.Add(dualAttr.ColumnEpoch);

                    parameterNames.Add("@" + dualAttr.ColumnUtcText);
                    parameterNames.Add("@" + dualAttr.ColumnEpoch);

                    parameters.Add("@" + dualAttr.ColumnUtcText, utcString);
                    parameters.Add("@" + dualAttr.ColumnEpoch, epoch);

                    continue;
                }
            }

            foreach (MethodInfo method in methods)
            {
                SqliteComputedColumnAttribute methodAttr = method.GetCustomAttribute<SqliteComputedColumnAttribute>();
                if (methodAttr != null)
                {
                    object value = method.Invoke(obj, null);

                    columnNames.Add(methodAttr.ColumnName);
                    parameterNames.Add("@" + methodAttr.ColumnName);
                    parameters.Add("@" + methodAttr.ColumnName, value);
                }
            }

            if (columnNames.Count == 0 || string.IsNullOrEmpty(primaryKeyColumn))
                throw new Exception($"No valid columns or primary key found in type '{type.Name}'");

            List<string> updateClauseParts = new List<string>();
            foreach (string column in columnNames)
            {
                if (column != primaryKeyColumn)
                    updateClauseParts.Add($"{column} = excluded.{column}");
            }

            string query =
                $"INSERT INTO {tableName} ({string.Join(", ", columnNames)})\n" +
                $"VALUES ({string.Join(", ", parameterNames)})\n" +
                $"ON CONFLICT({primaryKeyColumn}) DO UPDATE SET {string.Join(", ", updateClauseParts)};";

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    foreach (KeyValuePair<string, object> kvp in parameters)
                        command.Parameters.AddWithValue(kvp.Key, kvp.Value);

                    try
                    {
                        // DebugExtension.DevLog("> query = ", query.SerializeObjectToJSON());
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (Exception exception)
                    {
                        DebugExtension.DevLogError(
                            "$$$> ".ToColor(GoodColors.Red),
                            "exception = ", "\n", exception.ToString());

                        // throw;
                    }
                }
            }
        }

        public async Task UpsertAll<T>(List<T> baseList)
        {
            if (baseList == null || baseList.Count == 0)
                return;

            Type type = typeof(T);
            SqliteTableAttribute tableAttr = type.GetCustomAttribute<SqliteTableAttribute>();
            if (tableAttr == null)
                throw new Exception($"Class '{type.Name}' is missing [SqliteTable] attribute.");

            string tableName = tableAttr.TableName;

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            List<string> columnNames = new List<string>();
            Dictionary<string, FieldInfo> fieldMap = new Dictionary<string, FieldInfo>();
            Dictionary<string, MethodInfo> computedMap = new Dictionary<string, MethodInfo>();

            string primaryKeyColumn = null;

            foreach (FieldInfo field in fields)
            {
                SqliteDateTimeDualAttribute dualAttr = field.GetCustomAttribute<SqliteDateTimeDualAttribute>();
                if (dualAttr != null)
                {
                    columnNames.Add(dualAttr.ColumnUtcText);
                    columnNames.Add(dualAttr.ColumnEpoch);
                    fieldMap[dualAttr.ColumnUtcText] = field;
                    fieldMap[dualAttr.ColumnEpoch] = field;
                    continue;
                }

                SqliteColumnAttribute columnAttr = field.GetCustomAttribute<SqliteColumnAttribute>();
                if (columnAttr != null)
                {
                    columnNames.Add(columnAttr.ColumnName);
                    fieldMap[columnAttr.ColumnName] = field;

                    if (columnAttr.IsPrimaryKey && primaryKeyColumn == null)
                        primaryKeyColumn = columnAttr.ColumnName;
                }
            }

            foreach (MethodInfo method in methods)
            {
                SqliteComputedColumnAttribute methodAttr = method.GetCustomAttribute<SqliteComputedColumnAttribute>();
                if (methodAttr != null)
                {
                    columnNames.Add(methodAttr.ColumnName);
                    computedMap[methodAttr.ColumnName] = method;
                }
            }

            if (columnNames.Count == 0)
                throw new Exception($"No mapped columns found in type '{type.Name}'.");

            if (primaryKeyColumn == null)
                throw new Exception($"No primary key defined in type '{type.Name}'.");

            List<string> setClauses = new List<string>();
            foreach (string col in columnNames)
            {
                if (col != primaryKeyColumn)
                    setClauses.Add($"{col} = excluded.{col}");
            }

            string insertClause = $"INSERT INTO {tableName} ({string.Join(", ", columnNames)})";
            string valuesClause = $"VALUES ({string.Join(", ", columnNames.ConvertAll(name => "@" + name))})";
            string conflictClause = $"ON CONFLICT({primaryKeyColumn}) DO UPDATE SET {string.Join(", ", setClauses)}";
            string query = $"{insertClause} {valuesClause} {conflictClause};";

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqliteTransaction transaction = connection.BeginTransaction())
                using (SqliteCommand command = new SqliteCommand(query, connection, transaction))
                {
                    foreach (T obj in baseList)
                    {
                        command.Parameters.Clear();

                        foreach (KeyValuePair<string, FieldInfo> pair in fieldMap)
                        {
                            string column = pair.Key;
                            FieldInfo field = pair.Value;

                            SqliteDateTimeDualAttribute dualAttr = field.GetCustomAttribute<SqliteDateTimeDualAttribute>();
                            if (dualAttr != null)
                            {
                                DateTime dateTimeValue = (DateTime)field.GetValue(obj);
                                string utcString = dateTimeValue.ToUniversalTime().ToString("o");
                                long epoch = new DateTimeOffset(dateTimeValue.ToUniversalTime()).ToUnixTimeSeconds();

                                command.Parameters.AddWithValue("@" + dualAttr.ColumnUtcText, utcString);
                                command.Parameters.AddWithValue("@" + dualAttr.ColumnEpoch, epoch);
                                continue;
                            }

                            object value = field.GetValue(obj) ?? DBNull.Value;

                            if (value is Guid guidValue)
                                value = guidValue.ToString();

                            command.Parameters.AddWithValue("@" + column, value);
                        }

                        foreach (KeyValuePair<string, MethodInfo> pair in computedMap)
                        {
                            string column = pair.Key;
                            MethodInfo method = pair.Value;

                            object value = method.Invoke(obj, null) ?? DBNull.Value;

                            if (value is Guid guidValue)
                                value = guidValue.ToString();

                            command.Parameters.AddWithValue("@" + column, value);
                        }

                        try
                        {
                            // DebugExtension.DevLog("> query = ", query.SerializeObjectToJSON());
                            await command.ExecuteNonQueryAsync();
                        }
                        catch (Exception exception)
                        {
                            DebugExtension.DevLogError(
                                "$$$> ".ToColor(GoodColors.Red),
                                "exception = ", "\n", exception.ToString());
                        }
                    }

                    transaction.Commit();
                }
            }
        }


        public async Task<T> Get<T>(object primaryKeyValue) where T : new()
        {
            Type type = typeof(T);
            SqliteTableAttribute tableAttr = type.GetCustomAttribute<SqliteTableAttribute>();
            if (tableAttr == null)
                throw new Exception($"Class '{type.Name}' is missing [SqliteTable] attribute.");

            string tableName = tableAttr.TableName;

            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            SqliteColumnAttribute primaryKeyAttr = null;
            string primaryKeyColumn = null;

            foreach (FieldInfo field in fields)
            {
                SqliteColumnAttribute attr = field.GetCustomAttribute<SqliteColumnAttribute>();
                if (attr != null && attr.IsPrimaryKey)
                {
                    primaryKeyAttr = attr;
                    primaryKeyColumn = attr.ColumnName;
                    break;
                }
            }

            if (primaryKeyColumn == null)
                throw new Exception($"No primary key column found in '{type.Name}'.");

            string query = $"SELECT * FROM {tableName} WHERE \"{primaryKeyColumn}\" = @pk LIMIT 1;";

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqliteCommand command = new SqliteCommand(query, connection))
                {
                    object safeValue = (primaryKeyValue is Guid guid) ? guid.ToString() : primaryKeyValue;
                    command.Parameters.AddWithValue("@pk", safeValue);

                    try
                    {
                        // DebugExtension.DevLog("> query = ", query.SerializeObjectToJSON());

                        using (DbDataReader reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                T item = new T();
                                PopulateObjectFromReader(reader, item);
                                return item;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        DebugExtension.DevLogError(
                            "$$$> ".ToColor(GoodColors.Red),
                            "exception = ", "\n", exception.ToString());

                        // throw;
                    }
                }
            }

            return default;
        }

        public async Task<List<T>> GetAll<T>(string overrideWhereStatment = null) where T : new()
        {
            List<T> result = new List<T>();

            Type type = typeof(T);
            SqliteTableAttribute tableAttr = type.GetCustomAttribute<SqliteTableAttribute>();
            if (tableAttr == null)
                throw new Exception($"Class '{type.Name}' is missing [SqliteTable] attribute.");

            string tableName = tableAttr.TableName;

            string query = $"SELECT * FROM {tableName}";

            if (String.IsNullOrWhiteSpace(overrideWhereStatment))
                query += ";";
            else
                query += " " + overrideWhereStatment;

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                await connection.OpenAsync();

                try
                {
                    // DebugExtension.DevLog("> query = ", query.SerializeObjectToJSON());

                    using (SqliteCommand command = new SqliteCommand(query, connection))
                    using (DbDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            T item = new T();
                            PopulateObjectFromReader(reader as SqliteDataReader, item);
                            result.Add(item);
                        }
                    }
                }
                catch (Exception exception)
                {
                    DebugExtension.DevLogError(
                        "$$$> ".ToColor(GoodColors.Red),
                        "exception = ", "\n", exception.ToString());

                    // throw;
                }
            }

            return result;
        }

        private void PopulateObjectFromReader<T>(DbDataReader reader, T obj)
        {
            Type type = typeof(T);
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                SqliteColumnAttribute columnAttr = field.GetCustomAttribute<SqliteColumnAttribute>();
                SqliteDateTimeDualAttribute dualAttr = field.GetCustomAttribute<SqliteDateTimeDualAttribute>();

                if (dualAttr != null)
                {
                    string columnEpoch = dualAttr.ColumnEpoch;
                    if (reader[columnEpoch] != DBNull.Value)
                    {
                        long epoch = Convert.ToInt64(reader[columnEpoch]);
                        DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(epoch).UtcDateTime;
                        field.SetValue(obj, dateTime);
                    }
                    continue;
                }

                if (columnAttr != null)
                {
                    object value = reader[columnAttr.ColumnName];

                    try
                    {
                        object parsedValue =
                            field.FieldType == typeof(Guid)
                            ?
                            Guid.Parse(value.ToString())
                            :
                            Convert.ChangeType(value, field.FieldType);

                        field.SetValue(obj, parsedValue);
                    }
                    catch (Exception exception)
                    {
                        DebugExtension.DevLogError(
                            "$$$> ".ToColor(GoodColors.Red),
                            "columnAttr.ColumnName = ", columnAttr.ColumnName.SerializeObjectToJSON(), "\n",
                            "field.FieldType = ", field.FieldType.Name.SerializeObjectToJSON(), "\n",
                            "value = ", value.SerializeObjectToJSON(), "\n",
                            "exception = ", "\n", exception.ToString());

                        // throw;
                    }
                }
            }
        }
        #endregion Controller - CRUD
    }
}
