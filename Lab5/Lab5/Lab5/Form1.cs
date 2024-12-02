using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Name: Cameron Shephard
         * Date: November 2024
         * This program rolls one dice or calculates mark stats.
         * Link to your repo in GitHub: https://github.com/shephardcam/ProgLab5
         * */

        //class-level random object
        Random rand = new Random();
        string NAME = "Cameron Shephard";

        private void Form1_Load(object sender, EventArgs e)
        {
            //select one roll radiobutton
            radOneRoll.Checked = true;
            //add your name to end of form title
            this.Text += Name; 
        } // end form load

        private void btnClear_Click(object sender, EventArgs e)
        {
            //call the function
            ClearOneRoll();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            //call the function
            ClearStats();
        }

        private void btnRollDice_Click(object sender, EventArgs e)
        {
            int dice1, dice2, total;
            //call ftn RollDice, placing returned number into integers

            //place integers into labels

            // call ftn GetName sending total and returning name

            //display name in label
            dice1 = RollDice();
            dice2 = RollDice();
            total = dice1 + dice2;

            lblDice1.Text = dice1.ToString();
            lblDice2.Text = dice2.ToString();
            
            string name = GetName(total);

            lblRollName.Text = name;
        }

        /* Name: ClearOneRoll
        *  Sent: nothing
        *  Return: nothing
        *  Clear the labels */
            private void ClearOneRoll()
            {
                lblDice1.Text = string.Empty;
                lblDice2.Text = string.Empty;
                lblRollName.Text = string.Empty;
            }

        /* Name: ClearStats
        *  Sent: nothing
        *  Return: nothing
        *  Reset nud to minimum value, chkbox unselected, 
        *  clear labels and listbox */
           private void ClearStats()
           {
                nudNumber.Value = nudNumber.Minimum;
                chkSeed.Checked = false;
                lblAverage.Text = string.Empty;
                lblPass.Text = string.Empty;
                lblFail.Text = string.Empty;
                lstMarks.Items.Clear();
           }

        /* Name: RollDice
        * Sent: nothing
        * Return: integer (1-6)
        * Simulates rolling one dice */
        private int RollDice()
        {
            return rand.Next(1, 7);
        }

        /* Name: GetName
        * Sent: 1 integer (total of dice1 and dice2) 
        * Return: string (name associated with total) 
        * Finds the name of dice roll based on total.
        * Use a switch statement with one return only
        * Names: 2 = Snake Eyes
        *        3 = Litle Joe
        *        5 = Fever
        *        7 = Most Common
        *        9 = Center Field
        *        11 = Yo-leven
        *        12 = Boxcars
        * Anything else = No special name*/
        private string GetName(int total) {
            switch (total) { 
            case 2: 
                return "Snake eyes";

            case 3:
                return "Litle Joe";

            case 5:
                return "Fever";

            case 7:
                return "Most Common";

            case 9:
                return "Center Feild";

            case 11:
                return "Yo-leven";

            case 12:
                return "Boxcars";

            default:
                return "No special name";
            }
        }

        private void btnSwapNumbers_Click(object sender, EventArgs e)
        {
            //call ftn DataPresent twice sending string returning boolean

            //if data present in both labels, call SwapData sending both strings

            //put data back into labels

            //if data not present in either label display error msg
            if (DataPresent(lblDice1.Text) && DataPresent(lblDice2.Text))
            {
                string data1 = lblDice1.Text;
                string data2 = lblDice2.Text;

                SwapData(ref data1, ref data2); // Call SwapData to swap the values

                lblDice1.Text = data1; // Update labels with swapped data
                lblDice2.Text = data2;
            }
            else
            {
                MessageBox.Show("Both labels must have data", NAME);
            }

        }

        /* Name: DataPresent
        * Sent: string
        * Return: bool (true if data, false if not) 
        * See if string is empty or not*/
        private bool DataPresent(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                return true;
            }
            else { 
                return false;
            }
        }

        /* Name: SwapData
        * Sent: 2 strings
        * Return: none 
        * Swaps the memory locations of two strings*/
        private void SwapData(ref string str1, ref string str2)
        {
            string temp = str1;
            str1 = str2;
            str2 = temp;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            //declare variables and array

            //check if seed value

            //fill array using random number

            //call CalcStats sending and returning data

            //display data sent back in labels - average, pass and fail
            // Format average always showing 2 decimal places 
            int marksAmount = (int)nudNumber.Value;
            int[] marks = new int[marksAmount];
            int pass, fail;

            if (chkSeed.Checked)
            {
                rand = new Random(1000); 
            }
            else
            {
                rand = new Random();
            }

            for (int i = 0; i < marks.Length; i++)
            {
                marks[i] = rand.Next(40, 101);
            }


            double average = CalcStats(marks, out pass, out fail);

            lblAverage.Text = average.ToString("F2");
            lblPass.Text = pass.ToString();
            lblFail.Text = fail.ToString();

            lstMarks.Items.Clear();
            foreach (int mark in marks)
            {
                lstMarks.Items.Add(mark);
            }
        } // end Generate click

        /* Name: CalcStats
        * Sent: array and 2 integers
        * Return: average (double) 
        * Run a foreach loop through the array.
        * Passmark is 60%
        * Calculate average and count how many marks pass and fail
        * The pass and fail values must also get returned for display*/
        private double CalcStats(int[] marks, out int pass, out int fail)
        {
            int total = 0;
            double average;
            pass = 0;
            fail = 0;
            

            foreach (int mark in marks)
            {
                total += mark;
                if (mark >= 60)
                    pass++;
                else
                    fail++;
            }

            average = (double)total / marks.Length;
            
            return average;
        }

        private void radOneRoll_CheckedChanged(object sender, EventArgs e)
        {
            if (radOneRoll.Checked)
            { 
                grpOneRoll.Visible = true;
                grpMarkStats.Visible = false;
                ClearOneRoll(); 
            }
            else if (radRollStats.Checked)
            {
                grpOneRoll.Visible = false; 
                grpMarkStats.Visible = true; 
                ClearStats();
            }
        }

        private void chkSeed_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeed.Checked)
            {
                DialogResult result = MessageBox.Show("Are you sure you want a seed value?",NAME , MessageBoxButtons.YesNo);

                if (result == DialogResult.No)
                {
                    chkSeed.Checked = false;
                }
            }
        }
    }
}
