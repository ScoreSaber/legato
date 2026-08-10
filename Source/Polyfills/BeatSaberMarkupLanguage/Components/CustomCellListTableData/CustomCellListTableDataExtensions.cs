#nullable enable

using BeatSaberMarkupLanguage.Components;
using HMUI;
using System.Collections;
using System.Collections.Generic;

namespace Legato {
    internal static class CustomCellListTableDataExtensions {
        internal static IList GetData(this CustomCellListTableData tableData) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            return tableData.data;
#else
            return tableData.Data;
#endif
        }

        internal static void SetData(this CustomCellListTableData tableData, IList data) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            var list = new List<object>();
            foreach (object item in data) {
                list.Add(item);
            }
            tableData.data = list;
#else
            tableData.Data = data;
#endif
        }

        internal static TableView GetTableView(this CustomCellListTableData tableData) {
#if BEAT_SABER_1_29_0 || BEAT_SABER_1_37_1
            return tableData.tableView;
#else
            return tableData.TableView;
#endif
        }
    }
}
