/* -------------------------------------------------------
 * WARNING:
 *
 * This code was generated by the Excel Importer
 * Any changes will be lost, when it gets regenerated.
 * -----------------------------------------------------*/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Data.Imports
{
    public class TestThingsImport_TEMPLATE : ScriptableObject
    {
        [Serializable]
        public class Row
        {
            public int Id;
            public string Name;
            public bool IsActive;
            public float Value;
            public double Cost;
            public long LongValue;

        }

        public List<Row> Rows = new List<Row>();
    }
}
