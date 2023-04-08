using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scada.Data;

namespace Scada.Data.DataList
{
    public class TScadaList
    {
        public static List<TData> DataList = new List<TData>();
        public static Queue<List<TData>> Datas = new Queue<List<TData>>();

        public void AddData(TData Data)
        {
            DataList.Add(Data);
        }

        public static void AddHeatData(TData Data)
        {
            Datas.Enqueue(Data);
        }


        public static TData GetHeatDatas()
        {
            return Datas.Dequeue();

        }

        public void dd()
        {
            Datas.

        }


    }
}
