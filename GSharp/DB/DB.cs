using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using GSharp.Extensions.Array;
using GSharp.Extensions.Object;

namespace GSharp.DB {
    public class DB {
        #region Static

        private static DB _instance;

        public static DB I {
            get {
                if (_instance == null) {
                    _instance = new DB(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BackupProjectDB.bin");
                    //_instance.initDB(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BackupProjectDB.bin");
                }
                return _instance;
            }
        }

        public static ByteArrayExtensions.DataFormatType DefaultFormatType = ByteArrayExtensions.DataFormatType.Binary;
        #endregion


        public ByteArrayExtensions.DataFormatType FormatType = ByteArrayExtensions.DataFormatType.Binary;

        private bool _editMode = false;

        public FileInfo dbFile;
        public Dictionary<object, object> dicDb = new Dictionary<object, object>();

        public DB(string file) {
            this.FormatType = DefaultFormatType;
            this.initDB(file);
        }

        public DB(string file, ByteArrayExtensions.DataFormatType format) {
            this.FormatType = format;
            this.initDB(file);
        }

        public Object this[object index] {
            get {
                if (dicDb.ContainsKey(index)) return dicDb[index];
                else return null;
            }
            set {
                if (dicDb.ContainsKey(index)) {
                    dicDb[index] = value;
                } else {
                    dicDb.Add(index, value);
                }
                if (!this._editMode)
                    _saveDB();
            }
        }

        public void initDB(string dbfile) {
            dbFile = new FileInfo(dbfile);
            try {
                System.IO.FileStream fs = new System.IO.FileStream(dbFile.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                fs.Close();
                dicDb = (Dictionary<object, object>)data.DeSerialize(FormatType);
                if (dicDb == null)
                    dicDb = new Dictionary<object, object>();
            } catch (Exception ex) { }
        }
        public object[] GetKeys() {
            return dicDb.Keys.ToArray();
        }

        private void _saveDB() {
            dicDb.Serialize(FormatType).SaveToFile(dbFile.FullName);
        }

        public void BeginEdit() { this._editMode = true; }
        public void EndEdit() { this._editMode = false; _saveDB(); }
    }

}
