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
        bool Game_Lost = false, Game_Started = false;
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

            for (int i = 0; i < RiskLevel; i++) 
            {
                num[i] = rnd.Next(2, 27);
            }
            foreach (Button button in tableLayoutPanel1.Controls.OfType<Button>())
            {
                button.Click += Button_Click;
            }
            BallanceValueLabel.Text = BallanceValue.ToString();

            OneXmultiplier.FlatAppearance.BorderColor = Color.Black;
            TwoXmultiplier.FlatAppearance.BorderColor = Color.Black;
            ThreeXmultiplier.FlatAppearance.BorderColor = Color.Black;
            
        }



        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            // Check if the clicked button is a "mine" button
            if(Game_Started == false && BetAmmountValue > 0)
            {
                Game_Started = true;
                BallanceValue -= BetAmmountValue;
                BallanceValueLabel.Text = BallanceValue.ToString();
                BetAmmountBox.Enabled = false;
            }

            if (button.Text=="?" && BetAmmountValue<=0)
            {
                MessageBox.Show("Please select a bet value first");
            }
           

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
            Game_Started = false;
            CurrentValueLabel.Text = CurrentValue.ToString();

            if (BetAmmountValue > BallanceValue)
            {
                BetAmmountValue = 0;
                BetAmmountBox.Text = "";
            }
           
        }

        private void OneXmultiplier_Click(object sender, EventArgs e)
        {
            if (Game_Started==false)
            {
                RiskLevel = 1;
                OneXmultiplier.FlatAppearance.BorderColor = Color.Red;
                TwoXmultiplier.FlatAppearance.BorderColor = Color.Black;
                ThreeXmultiplier.FlatAppearance.BorderColor = Color.Black;
                num = new int[RiskLevel];
                Random rnd = new Random();

                for (int i = 0; i < RiskLevel; i++)
                {
                    num[i] = rnd.Next(2, 27);
                }
            }
        }

        private void TwoXmultiplier_Click(object sender, EventArgs e)
        {
            if (Game_Started == false)
            {
                RiskLevel = 2;
                TwoXmultiplier.FlatAppearance.BorderColor = Color.Red;
                OneXmultiplier.FlatAppearance.BorderColor = Color.Black;
                ThreeXmultiplier.FlatAppearance.BorderColor = Color.Black;
                num = new int[RiskLevel];
                Random rnd = new Random();

                for (int i = 0; i < RiskLevel; i++)
                {
                    num[i] = rnd.Next(2, 27);
                }

            }
        }

        private void ThreeXmultiplier_Click(object sender, EventArgs e)
        {
            if (Game_Started == false)
            {
                RiskLevel = 3;
                ThreeXmultiplier.FlatAppearance.BorderColor = Color.Red;
                OneXmultiplier.FlatAppearance.BorderColor = Color.Black;
                TwoXmultiplier.FlatAppearance.BorderColor = Color.Black;
                num = new int[RiskLevel];
                Random rnd = new Random();

                for (int i = 0; i < RiskLevel; i++)
                {
                    num[i] = rnd.Next(2, 27);
                }
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            BoardReset();
            ResetButton.Visible = false;
        }

    }
}