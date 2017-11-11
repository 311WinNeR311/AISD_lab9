using System;
using System.Windows.Forms;

namespace AISD_lab8
{
    public partial class Form1 : Form
    {
        private int[] iArray = null;
        private uint uiSize = 0;

        public Form1()
        {
            InitializeComponent();
        }
        // Quick sort
        static public void Swap<T>(ref T l, ref T r)
        {
            T temp = l;
            l = r;
            r = temp;
        }
        void QuickSort(int[] iArray)
        {
            QuickSort(iArray, 0, iArray.Length - 1);
        }
        void QuickSort(int[] iArray, int left, int right)
        {
            int i = left, j = right;
            int middle = iArray[(i + j) / 2];
            while (i <= j)
            {
                while (iArray[i] < middle)
                {
                    i++;
                }
                while (iArray[j] > middle)
                {
                    j--;
                }
                if (i <= j)
                {
                    Swap(ref iArray[i], ref iArray[j]);
                    i++;
                    j--;
                }
            }
            if (left < j)
            {
                QuickSort(iArray, left, j);
            }
            if (i < right)
            {
                QuickSort(iArray, i, right);
            }
        }

        // Binary search
        private bool BinarySearch(int [] iArray, int iElement, ref int iPos, ref int iCountOfComparsions)
        {
            iCountOfComparsions = 0;
            bool isFounded = false;
            int iMin = 0;
            int iMax = iArray.Length - 1;
            int iPosP = iPos;
            while (iMin < iMax)
            {
                iPos = (iMin + iMax) / 2;
                ++iCountOfComparsions;
                if (iArray[iPos] < iElement)
                    iMin = iPos + 1;
                else
                {
                    iPosP = iPos;
                    iMax = iPos;
                    isFounded = true;
                }
            }
            return isFounded;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 6 || !(uint.TryParse(textBox1.Text, out uint _uiSize)))
            {
                MessageBox.Show("Incorrect count of elements");
                return;
            }

            uiSize = _uiSize;
            
            dataGridView1.Rows.Clear();
            richTextBox1.Clear();

            iArray = new int[uiSize];

            Random rnd = new Random();
            for (uint i = 0; i < uiSize; ++i)
            {
                iArray[i] = rnd.Next(0, 100000);
            }

            QuickSort(iArray);

            for (uint i = 0; i < uiSize; ++i)
            {
                dataGridView1.Rows.Add(Convert.ToString(iArray[i]));
            }
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (uiSize == 0 || iArray == null)
            {
                MessageBox.Show("First of all, generate random elements");
                return;
            }

            //Searching min element(iMin)
            int iMin = iArray[0];
            for (uint i = 0; i < uiSize; i++)
            {
                if (iMin > iArray[i])
                {
                    iMin = iArray[i];
                }
            }

            //Finding in an array larger or equal element for a doubled product of minimum element(iMin)
            int iMinDoubleProduct = 2 * iMin * iMin;
            int iPos = 0;
            int iCountOfComparsions = 0;
            bool isFounded = BinarySearch(iArray, iMinDoubleProduct, ref iPos, ref iCountOfComparsions);

            richTextBox1.Clear();

            // Output results
            if (isFounded)
            {
                richTextBox1.Text += "Position of founded element: " + (iPos + 1).ToString() + "\nCount of comparsions: " + iCountOfComparsions.ToString();
            }
            else
            {
                richTextBox1.Text += "Element hasn't been founded\nCount of comparsions: " + iCountOfComparsions.ToString();
            }
            button2.Enabled = false;
        }
    }
}
