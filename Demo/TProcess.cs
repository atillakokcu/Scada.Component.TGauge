using Scada.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Scada.Data.DataList;
using System.Windows.Forms.Design;

namespace Demo
{
    public delegate void RxOnGenerateDevice(List<TDevice> Devices);
    public delegate void RxOnGetDeviceData(List<TData> DataList);
    public class TProcess
    {
        Thread ThrSmulation;
        private List<TDevice> devices;
        TDevice device;
        public int DeviceCount;
        

        public event RxOnGenerateDevice OnGenerateDevice;
        public event RxOnGetDeviceData OnGetDeviceData;

        public void CreateDevice(int Count)
        {
            DeviceCount = Count;
            devices = new List<TDevice>();
            for (int i = 0; i < Count; i++)
            {
                 device = new TDevice();
                device.DeviceId= new Random().Next(10,99);
                Thread.Sleep(20);
                device.DeviceName= device.DeviceId.ToString()+" nolu Cihaz";
                devices.Add(device);

            }

            if (OnGenerateDevice!=null)
            {
                OnGenerateDevice(devices);
            }

        }


        public void StartGenerateDate()
        {
            if (ThrSmulation == null)
            {
                ThrSmulation = new Thread( new ThreadStart(DoStartGenerateDate) ); 
            }
            ThrSmulation.Start();
        }


        public void DoStartGenerateDate()
        {
            foreach (var device in devices)
            {
                TDeviceProcess deviceProcess = new TDeviceProcess(device);
                deviceProcess.Start();
            }

        }

        public void GetDeviceDatas()
        {
            foreach (var device in devices)
            {
                TDeviceDataProcess deviceProcess = new TDeviceDataProcess(device);
                deviceProcess.OnGetDeviceData += DeviceProcess_OnGetDeviceData;
                deviceProcess.Start();
            }

        }

        private void DeviceProcess_OnGetDeviceData(List<TData> DataList)
        {
            if (OnGetDeviceData !=null)
            {
                OnGetDeviceData(DataList);
            }
        }
    }


    public class TDeviceProcess
    {
        TScadaList Scadadatalist = new TScadaList();
        private TDevice device;
        Thread MyThreard;

        public TDeviceProcess(TDevice Device)
        {
            device = Device;

        }


        public void Start() //bu threat içinde farklı bir threat üretmeye yarıyor
        {
            if (MyThreard==null)
            {
                MyThreard = new Thread(new ThreadStart(DoStart));
            }
            MyThreard.Start();
        }

        public void DoStart()
        {
            while (true)
            {
                lock (Scadadatalist)
                {
                    TData data = new TData();
                    data.DateDate = DateTime.Now;
                    data.Heat = new Random().Next(1, 100);
                    Thread.Sleep(100);
                    data.DeviceId = device.DeviceId;
                    Scadadatalist.AddData(data);
                }

               
                int SleepTime = new Random().Next(3, 9);
                Thread.Sleep(SleepTime * 1000);

            }

        }


       


    }

    public class TDeviceDataProcess
    {
        public event RxOnGetDeviceData OnGetDeviceData;
        public TDevice device;
        Thread MyThreard;

        public TDeviceDataProcess(TDevice Device)
        {
            device = Device;

        }


        public void Start() //bu threat içinde farklı bir threat üretmeye yarıyor
        {
            if (MyThreard == null)
            {
                MyThreard = new Thread(new ThreadStart(DoStart));
            }
            MyThreard.Start();
        }

        public void DoStart()
        {
            while (true)
            {
               List< TData> dataList = new List< TData>();
                lock (TScadaList.DataList)
                {
                    dataList = (from devicedata in TScadaList.DataList
                                where devicedata.DeviceId == device.DeviceId
                                orderby devicedata.DateDate
                                select devicedata).ToList();
                    foreach (var data in dataList)
                    {
                        TScadaList.DataList.Remove(data);
                    }
                }

                
                if (OnGetDeviceData!=null)
                {
                    OnGetDeviceData(dataList);
                }
                Thread.Sleep(500);

            }

        }





    }

}
