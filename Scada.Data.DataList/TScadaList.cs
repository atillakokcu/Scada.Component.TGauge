using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scada.Data.DataList
{
    public class TScadaList
    {
        public static List<TData> DataList = new List<TData>();

        public void AddData(TData Data)
        {
            DataList.Add(Data);
        }
    }
}
