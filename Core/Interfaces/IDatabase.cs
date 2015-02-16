﻿using System.Collections.Generic;
using Core.Descriptions;

namespace Core.Interfaces {
    public interface IDatabase {
        PrimaryKeyDescription GetPrimaryKey(TableDescription table);
        IList<ForeignKeyDescription> GetForeignKeysReferencing(ConstraintDescription primaryKeyDescription);
        IList<ForeignKeyDescription> GetForeignKeys(TableDescription tableDescription);
        IList<UniqueDescription> GetUniqueKeys(TableDescription tableDescription);
        UniqueDescription GetUniqueKey(string uniqueKeyName);
        DefaultDescription GetDefault(string defaultName, string schema);
        IList<DefaultDescription> GetDefaults();
        ColumnDescription GetColumn(string schema, string tableName, string columnName);
        TableDescription GetTable(string schema, string tableName);
        IndexDescription GetIndex(string schema, string tableName, string indexName);
        IList<IndexDescription> GetIndexes(string schema, string tableName);
    }
}