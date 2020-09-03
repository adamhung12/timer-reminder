using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace TimerReminder
{
	public partial class Form1 : Form
	{
		public Form1(String s)
		{
			InitializeComponent();
            if (s != null && s != "")
            {
                this.textBox1.Text = s;
                triggerAction();
            }
        }

        int h, m, s = 0;

        bool updated = false;

        void triggerAction()
        {
            String t = textBox1.Text;
            updateTitle(t);
            updateHour(t);
            updateSec(t);
            updateMin(t);

            sec = s + m * 60 + h * 60 * 60;
            i = 0;
            textBox1.Visible = false;
            label1.Visible = true;

            updateLabelValue(sec, 0);
            timer = new System.Timers.Timer();
            timer.Interval = 1000; // In milliseconds
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Elapsed += TimerElapsed;
            timer.Start();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyData == Keys.Enter)
            {
                triggerAction();
            }
        }

        System.Timers.Timer timer = new System.Timers.Timer();
        int i = 0;
        int sec;
        void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            
            label1.Invoke((MethodInvoker)delegate
            {

                updateLabelValue(sec, ++i);
                if (sec == i)
                {
                    this.Focus();
                    this.BringToFront();
                    this.Activate();
                    label1.Text = "Done: " + h + " hours " + m + " mins " + s + " seconds";

                    this.Location = new System.Drawing.Point(
                        System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width/2 - this.Width/2,
                        System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height/2 - this.Height/2
                        );
                    timer.Stop();
                    timer.Dispose();
                }
            });
            
        }


        void updateLabelValue(int sec, int i)
        {
            int n = sec - i++;
            int h = n / 3600;
            n = n % 3600;
            int m = n / 60;
            n = n % 60;
            int s = n;
            label1.Text = h + " hours " + m + " mins " + s + " seconds";
        }

        private void updateHour(String s)
        {
            Regex hourReg = new Regex(@"(\d+)[h|H]");
            Match mm = hourReg.Match(s);
            if (mm.Success)
            {
                string s2 = mm.Groups[1].Value.Trim();
                h = Int16.Parse(s2);
            }
        }
        private void updateMin(String s)
        {
            Regex hourReg = new Regex(@"(\d+)[m|M]");
            Match mm = hourReg.Match(s);
            if (mm.Success)
            {
                string s2 = mm.Groups[1].Value.Trim();
                m = Int16.Parse(s2);
            }

        }
        private void updateSec(String s)
        {
            Regex secReg = new Regex(@"(\d+)[s|S]");
            Match mm = secReg.Match(s);
            if (mm.Success)
            {
                string s2 = mm.Groups[1].Value.Trim();
                this.s = Int16.Parse(s2);
            }

        }


        private void updateTitle(String s)
        {
            Regex titleReg = new Regex(@"\*\*(.*)");
            Match m = titleReg.Match(s);
            if (m.Success)
            {
                string ss = m.Value;
                string s2 = m.Groups[1].Value.Trim();

                Text = s2;
                
                Console.Out.WriteLine("");
            }


        }
    }
}
