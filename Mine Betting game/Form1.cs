using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Mine_Betting_game
{
    public partial class Form1 : Form
    {
        int RiskLevel = 1;
        private int[] num;
        int BetAmmountValue = 0;
        double CurrentValue = 0;
        bool Game_Lost = false;
        double BallanceValue = 10;
        public Form1()
        {
            InitializeComponent();
            num = new int[RiskLevel];

        }

        public void Form1_Load(object sender, EventArgs e)
        {
            ResetButton.Visible = false;
            Random rnd = new Random();

            for (int i = 0; i < RiskLevel; i++) // corrected loop initialization
            {
                num[i] = rnd.Next(2, 27);
            }
            foreach (Button button in tableLayoutPanel1.Controls.OfType<Button>())
            {
                button.Click += Button_Click;
            }
            BallanceValueLabel.Text = BallanceValue.ToString();
        }

        

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // Check if the clicked button is a "mine" button
            for (int i = 0; i < RiskLevel; i++)
            {
                if (button.Name == "button" + num[i])
                {
                    button.Text = "💣"; // Set text to indicate it's a mine
                    CurrentValue = 0;
                    CurrentValueLabel.Text = CurrentValue.ToString();
                    Game_Lost = true;
                    ResetButton.Visible = true;
                    break;
                }
                else if (button.Text != "$" && Game_Lost == false && BetAmmountValue>0)
                {
                    button.Text = "$"; 
                    CurrentValue = CurrentValue + ( BetAmmountValue * 0.15 * RiskLevel);
                    CurrentValueLabel.Text = CurrentValue.ToString();
                } 

            }
        }

        private void BetAmmountBox_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(BetAmmountBox.Text, out int betAmount))
            {
                if (betAmount <= BallanceValue)
                {
                    BetAmmountValue = int.Parse(BetAmmountBox.Text);
                    BallanceValue -= betAmount;
                    BallanceValueLabel.Text = BallanceValue.ToString();
                    BetAmmountBox.Enabled = false;
                }
                else
                {
                    BetAmmountBox.Text = "";
                    MessageBox.Show("Insufficient funds");
                }
            }
            else
            {
                
                BetAmmountBox.Text = "";
            }
        }

        private void CashoutButton_Click(object sender, EventArgs e)
        {
            BallanceValue = BallanceValue + CurrentValue;
            BallanceValueLabel.Text = BallanceValue.ToString();
            BoardReset();
        }

        private void BoardReset()
        {
            BetAmmountBox.Enabled = true;
            foreach (Button button in tableLayoutPanel1.Controls.OfType<Button>())
            {
                button.Text = "?";
            }
            CurrentValue = 0;
            Game_Lost = false;
            CurrentValueLabel.Text = CurrentValue.ToString();

            if (BetAmmountValue > BallanceValue)
            {
                BetAmmountValue = 0;
                BetAmmountBox.Text = "";
            }
            else
            {
                BallanceValue -= BetAmmountValue;
                BallanceValueLabel.Text = BallanceValue.ToString();
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            BoardReset();
            ResetButton.Visible = false;
            
        }
    }
}