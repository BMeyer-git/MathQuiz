using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        // initialize the randomizer and all of the integer variables to be used in the quiz
        Random randomizer = new Random();

        int addend1;
        int addend2;

        int minuend;
        int subtrahend;

        int multiplicand;
        int multiplier;

        int dividend;
        int divisor;

        int timeLeft;


        public Form1()
        {
            InitializeComponent();
        }

        public void StartTheQuiz()
        {
            // Initalize random values for addition and convert to string for labels
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);

            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            sum.Value = 0;

            // Initialize random values for subraction, the subrahend cannot go above the
            // minuend, so the difference will always be positive
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Initialize random values and labels for multiplication
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 11);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Initalize random values and labels for divison
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // The user is given 30 seconds to do the quiz, and the timer is started.
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            // When the quiz is started, the start button is disabled, and the timer's
            // background is set to white, noticable if a quiz has already been done
            StartTheQuiz();
            startButton.Enabled = false;
            timeLabel.BackColor = Color.White;
        }

        private bool CheckTheAnswer()
        {
            // If all of the math question components equal the value given by the user,
            // then they have succeeded.
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Change the color of the timer to warn/stress out the user if time is short
            if (timeLeft <= 5)
            {
                timeLabel.BackColor = Color.Red;
            }

            // Check for the correct answers every tick, notify user when they're done
            if (CheckTheAnswer())
            {
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                               "Congratulations!");
                startButton.Enabled = true;
            }
            // update the timer so long as they have time left
            else if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " seconds";
            }
            // If they have no time left, stop the timer and let the user know of their failure,
            // give them the correct answers so they can learn from it
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }

        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // The sender is treated as a NumericUpDown labeled answerbox
            NumericUpDown answerBox = sender as NumericUpDown;

            // If the answerbox in question has data inside, automatically select the text for
            // easier overwriting in the quiz
            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // As the form loads, put the current date on the window
            DateTime currentDay = DateTime.Today;
            dateLabel.Text = currentDay.ToString("dd MMMM yyyy");
        }
    }
}
