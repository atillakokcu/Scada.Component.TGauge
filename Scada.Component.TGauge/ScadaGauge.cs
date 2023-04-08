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

namespace Scada.Component.TGauge
{
    public partial class ScadaGauge : UserControl
    {
        int Count = 0;
        private int FHeat;

        public int Heat
        {
            get { return GetHeat(); }
            set { SetHeat(value); }
        }

        private void SetHeat(int value)
        {
            FHeat = value;
            trackBar1.Value = FHeat;

            if (FHeat >= FmaxHeat)
            {
                if (FAlarmAktive)
                {
                    timer1.Enabled= true;
                }
            }
        }

        private int GetHeat()
        {
            return FHeat;
        }


        private string FDeviceName;

        public string DeviceName
        {
            get { return GetDeviceName(); }
            set { SetDeviceName(value); }
        }

        private void SetDeviceName(string value)
        {
            FDeviceName = value;
            LblName.Text = value;
        }

        private string GetDeviceName()
        {
            return DeviceName;
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Count > 20)
            {
                timer1.Enabled = false;
            }

            if (LblName.BackColor == Color.Silver)
            {
                LblName.BackColor = Color.Red;
                
            }
            else
            {
                LblName.BackColor = Color.Silver;
            }
            Count++;
            
        }
    }
}
