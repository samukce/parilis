﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Castle.Core;
using Core;
using Core.Descriptions;
using Core.Exceptions;
using Core.Interfaces;

namespace SqlServer {
    [CastleComponent("SqlServer.Tables", typeof (ITable), Lifestyle = LifestyleType.Transient)]
    public class Tables : SqlServerEntity, ITable {
        public Tables(ConnectionInfo database) {
            Initialize(database);
        }

        public void Create(TableDescription tableDescription) {
            if (!TableNameIsValid(tableDescription))
                throw new InvalidTableNameException();

            var columns = new List<string>();
            foreach (var column in tableDescription.Columns ?? new List<ColumnDescription>()) {
                columns.Add(string.Format("[{0}] [{1}]{2} {3} {4}", column.Name, column.Type,
                    string.IsNullOrWhiteSpace(column.Length) ? string.Empty : string.Format("({0})", column.Length),
                    column.IsIdentity ? "IDENTITY(1,1)" : string.Empty, column.AllowsNull ? "NULL" : "NOT NULL"));
            }

            var command = new SqlCommand(string.Format(@"CREATE TABLE [{0}].[{1}]({2})",
                tableDescription.Schema, tableDescription.Name, string.Join(",", columns)));

            SqlServerDatabase.ExecuteNonQuery(command);
        }

        public void Remove(string schema, string tableName) {
            var table = SqlServerDatabase.GetTable(schema, tableName);
            if (table == null) throw new TableNotFoundException();

            var primaryKey = SqlServerDatabase.GetPrimaryKey(new TableDescription {Schema = schema, Name = tableName});
            if (primaryKey != null) {
                var foreignKeys = SqlServerDatabase.GetForeignKeysReferencing(primaryKey);
                if (foreignKeys.Any()) throw new ReferencedTableException();
            }

            var command = new SqlCommand(string.Format(@"DROP TABLE [{0}].[{1}]", schema, tableName));

            SqlServerDatabase.ExecuteNonQuery(command);
        }

        private bool TableNameIsValid(TableDescription tableDescription) {
            return !string.IsNullOrWhiteSpace(tableDescription.Name) &&
                   SqlServerDatabase.GetTable(tableDescription.Schema, tableDescription.Name) == null &&
                   SqlServerDatabase.IdentifierNameIsValid(tableDescription.Name);
        }
    }
}