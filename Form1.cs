using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KangarooPunchWithInterface
{
    public partial class KangarooPunchInteface : Form
    {
        private KangarooPunch KP;
        private bool muteLeft;
        private bool muteRight;

        public KangarooPunchInteface()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            muteLeft = true;
            muteRight = true;
            KP = new KangarooPunch();
            StatusText.Text = KP.getStatus();
            fillDropDowns();
            fillTextBox();
            setLeftPicture();
            setRightPicture();
            muteLeft = false;
            muteRight = false;
        }

        public void fillDropDowns()
        {
            int i = 0;
            List<currency> currencyList = KP.getCurrencyList();
            currency C = null;
            String option = "";

            //no currencies - quit early
            if (currencyList.Count == 0)
            { return; }

            for (i = 0; i < currencyList.Count; i++)
            {
                C = currencyList[i];
                option = C.getISO() + " - " + C.getName();

                LeftDropdown.Items.Add(option);
                RightDropdown.Items.Add(option);
            }

            //attempt to set default to USD - United States Dollar
            try
            {
                LeftDropdown.SelectedIndex = 17;
                RightDropdown.SelectedIndex = 17;
            }
            catch (Exception E) { }

            return;
        }

        public void fillTextBox()
        {
            LeftTextBox.Text = "1.00";
            RightTextBox.Text = "1.00";
            return;
        }

        public void setLeftPicture()
        {
            int index = LeftDropdown.SelectedIndex;
            LeftPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            String file = "images/" + index + ".png";

            Bitmap img = new Bitmap(file);
            LeftPicture.Image = (Image)img;

            return;
        }

        public void setRightPicture()
        {
            int index = RightDropdown.SelectedIndex;
            RightPicture.SizeMode = PictureBoxSizeMode.StretchImage;
            String file = "images/" + index + ".png";

            Bitmap img = new Bitmap(file);
            RightPicture.Image = (Image)img;

            return;
        }

        public double Convert(String from, String to, double amount)
        {
            double result = 0;
            result = KP.convert(from, to, amount);
            return result;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //left change
        private void LeftDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (muteLeft == true)
            { return; }

            muteRight = true;
            leftToRightConvert();
            setLeftPicture();
            muteRight = false;
        }

        //left text box changed
        private void LeftTextBox_TextChanged(object sender, EventArgs e)
        {
            if (muteLeft == true)
            { return; }

            muteRight = true;
            leftToRightConvert();
            muteRight = false;
        }

        //right change
        private void RightDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (muteRight == true)
            { return; }

            muteLeft = true;
            rightToLeftConvert();
            setRightPicture();
            muteLeft = false;
        }

        //right text box changed
        private void RightTextBox_TextChanged(object sender, EventArgs e)
        {
            if (muteRight == true)
            { return; }

            muteLeft = true;
            rightToLeftConvert();
            muteLeft = false;
        }

        public void leftToRightConvert()
        {
            double val = 0;
            double result = 0;
            List<currency> currencyList = KP.getCurrencyList();
            int leftIndex = 0;
            int rightIndex = 0;
            String from = "";
            String to = "";

            //attempt to parse value
            try
            {
                val = double.Parse(LeftTextBox.Text);
            }
            catch (Exception E)
            {
                RightTextBox.Text = "-";
                return;
            }

            leftIndex = LeftDropdown.SelectedIndex;
            rightIndex = RightDropdown.SelectedIndex;

            //grab LEFT's ISO
            from = currencyList[leftIndex].getISO();
            //grab RIGHT's ISO
            to = currencyList[rightIndex].getISO();
            //conversion call
            result = KP.convert(from, to, val);
            //set result to LEFT text box
            RightTextBox.Text = result.ToString();

            return;
        }

        public void rightToLeftConvert()
        {
            double val = 0;
            double result = 0;
            List<currency> currencyList = KP.getCurrencyList();
            int leftIndex = 0;
            int rightIndex = 0;
            String from = "";
            String to = "";

            //attempt to parse value
            try
            {
                val = double.Parse(RightTextBox.Text);
            }
            catch(Exception E)
            {
                LeftTextBox.Text = "-";
                return;
            }

            leftIndex = LeftDropdown.SelectedIndex;
            rightIndex = RightDropdown.SelectedIndex;

            //grab RIGHT's ISO
            from = currencyList[rightIndex].getISO();
            //grab LEFT's ISO
            to = currencyList[leftIndex].getISO();
            //conversion call
            result = KP.convert(from, to, val);
            //set result to LEFT text box
            LeftTextBox.Text = result.ToString();

            return;
        }


    }
}
