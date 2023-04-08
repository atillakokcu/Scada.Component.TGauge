using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Scada.Data;

namespace Scada.Component.TGauge
{
    public partial class ScadaGauge : UserControl
    {
       
        
        private int FHeat;

        public int Heat
        {
            get { return GetHeat(); }
            set { SetHeat(value); }
        }

        private void SetHeat(int value)
        {
            FHeat = value;
            LblHeat.Text = FHeat.ToString();
            

            if (FHeat >= FmaxHeat)
            {
                if (FAlarmAktive)
                {
                    Blink();
                }
            }
        }

        private int GetHeat()
        {
            return FHeat;
        }


        private TDevice FDevice;

        public TDevice Device
        {
            get { return GetDevice(); }
            set { SetDevice (value); }
        }

        private void SetDevice(TDevice value)
        {
            FDevice = value;
            LblName.Text = FDevice.DeviceName;
        }

        private TDevice GetDevice()
        {
            return FDevice;
        }

        private int FmaxHeat;

        public int MaxHeat
        {
            get { return GetmaxHeat(); }
            set { SetmaxHeat(value); }
        }

        private void SetmaxHeat(int value)
        {
            FmaxHeat = value;
        }

        private int GetmaxHeat()
        {
            return FmaxHeat;
        }


        private bool FAlarmAktive;

        public bool AlarmAktive
        {
            get { return GetAlarmAktive(); }
            set { SetAlarmAktive(value); }
        }

        private void SetAlarmAktive(bool value)
        {
            FAlarmAktive = value;
        }

        private bool GetAlarmAktive()
        {
            return FAlarmAktive;
        }

        public ScadaGauge()
        {
            InitializeComponent();
            
                CheckForIllegalCrossThreadCalls = false;

            
        }

       

        private void Blink()
        {

            Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    if (LblName.BackColor == Color.Silver)
                    {
                        LblName.BackColor = Color.Red;

                    }
                    else
                    {
                        LblName.BackColor = Color.Silver;
                    }
                    Thread.Sleep(50);
                }


            });
        }
    }
}
