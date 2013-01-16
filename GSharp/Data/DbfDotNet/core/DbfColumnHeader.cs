using System;
using System.Collections.Generic;
using System.Text;

namespace GSharp.Data.DbfDotNet.Core
{
    [Record(FieldMapping = FieldMapping.ExplicitColumnsOnly)]
    internal class DbfColumnHeader
    {
        [Column(Width = 11, Type = GSharp.Data.DbfDotNet.ColumnType.CHARACTER)]
        public string ColumnName;

        [Column(Type = GSharp.Data.DbfDotNet.ColumnType.CHAR)]
        public char ColumnType;

        [Column(Type = GSharp.Data.DbfDotNet.ColumnType.DELAYED, Width = 4)]
        public byte[] FieldDataAddress;

        [Column(Type = GSharp.Data.DbfDotNet.ColumnType.BYTE)]
        public byte ColumnWidth;

        [Column(Type = GSharp.Data.DbfDotNet.ColumnType.BYTE)]
        public byte Decimals;

        [Column(Type = GSharp.Data.DbfDotNet.ColumnType.DELAYED, Width = 14)]
        public byte[] Reserved2;

        public DbfColumnHeader()
        {
        }

        public void a()
        {
            FieldDataAddress = null;
            Reserved2 = null;
        }
    }
}
