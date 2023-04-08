using Scada.Component.TGauge;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        private TProcess Process;
        private List<Scada.Data.TDevice> Devices;
        public Form1()
        {
            InitializeComponent();
            Process = new TProcess();
            Process.OnGenerateDevice += Process_OnGenerateDevice;
            Process.OnGetDeviceData += Process_OnGetDeviceData;
        }

        private void Process_OnGetDeviceData(List<Scada.Data.TData> DataList)
        {
            foreach (var item in DataList)
            {
               var cntrl= PnlDevices.Controls.Find("gauge_" + item.DeviceId.ToString(), true).FirstOrDefault();
                if (cntrl != null)
                {
                    ((ScadaGauge)cntrl).Heat = item.Heat;
                }
                
            }

            
        }

        private void Process_OnGenerateDevice(List<Scada.Data.TDevice> Devices)
        {
            this.Devices = Devices;
            GenerateScadaGauge();
            Process.StartGenerateDate();
            Thread.Sleep(2000);
            Process.GetDeviceDatas();
        }

        public void GenerateScadaGauge()
        {
            foreach (var Device in Devices)
            {
                ScadaGauge gauge = new ScadaGauge();
                gauge.Dock= DockStyle.Left;
                gauge.Name = "gauge_"+Device.DeviceId.ToString();
                gauge.AlarmAktive = true;
                gauge.Device = Device;
                gauge.MaxHeat = 70;
                gauge.Parent = PnlDevices;
            }

        }


        private void TxtTime_ValueChanged(object sender, EventArgs e)
        {

        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            int Count = (int)TxtCount.Value;
            Process.CreateDevice(Count);
        }
    }
}
