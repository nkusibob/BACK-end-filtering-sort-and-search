﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.Helper
{
    public static class ExportToExcel
    {
        public static int mycount = 0;


        public static DataTable ToDataTable<T>(IList<T> data, DataTable mytable)
        {
            List<string> propertEntity = new List<string>();
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();
            table = mytable;



            for (int i = 0; i < props.Count; i++)
            {

                PropertyDescriptor prop = props[i];

            }
            object[] values = new object[props.Count];

            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);

                }
                table.Rows.Add(values);
            }
            
            return table;
        }
        public static DataTable ExportGeneric<T>(List<T> listToExport)
        {
            

            List<string> propertEntity = new List<string>();
            DataTable dt = new DataTable("DataToExcel");
            foreach (var item in listToExport)
            {


                foreach (var prop in item.GetType().GetProperties())
                {


                    var propType = "";




                    propType = prop.PropertyType.Name;


                    if (!dt.Columns.Contains(prop.Name))
                    {
                        dt.Columns.Add(new DataColumn(prop.Name));
                        propertEntity.Add(prop.Name);

                    }





                }

            }
            List<object> propValue = new List<object>();

            var dataToExportToExcel = listToExport;

            return ToDataTable(dataToExportToExcel, dt);



        }
    }
}
