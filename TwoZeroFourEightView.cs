using System;
using System.Drawing;
using System.Windows.Forms;

namespace twozerofoureight
{
    public partial class TwoZeroFourEightView : Form, View
    {
        Model model;
        Controller controller;

        public TwoZeroFourEightView()
        {
            InitializeComponent();
            model = new TwoZeroFourEightModel();
            model.AttachObserver(this);
            controller = new TwoZeroFourEightController();
            controller.AddModel(model);
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
        }

        public void Notify(Model m)
        {
            var _m = (TwoZeroFourEightModel)m;
            UpdateBoard(_m.GetBoard());
            UpdateScore(_m.GetScore());
            UpdateStatus(_m.GetStatus());
        }

        private void UpdateTile(Label l, int i)
        {
            if (i != 0)
            {
                l.Text = Convert.ToString(i);
            }
            else
            {
                l.Text = "";
            }
            switch (i)
            {
                case 0:
                    l.BackColor = Color.Gray;
                    break;
                case 2:
                    l.BackColor = Color.DarkGray;
                    break;
                case 4:
                    l.BackColor = Color.Orange;
                    break;
                case 8:
                    l.BackColor = Color.Red;
                    break;
                default:
                    l.BackColor = Color.Green;
                    break;
            }
        }
        private void UpdateBoard(int[,] board)
        {
            UpdateTile(lbl00, board[0, 0]);
            UpdateTile(lbl01, board[0, 1]);
            UpdateTile(lbl02, board[0, 2]);
            UpdateTile(lbl03, board[0, 3]);
            UpdateTile(lbl10, board[1, 0]);
            UpdateTile(lbl11, board[1, 1]);
            UpdateTile(lbl12, board[1, 2]);
            UpdateTile(lbl13, board[1, 3]);
            UpdateTile(lbl20, board[2, 0]);
            UpdateTile(lbl21, board[2, 1]);
            UpdateTile(lbl22, board[2, 2]);
            UpdateTile(lbl23, board[2, 3]);
            UpdateTile(lbl30, board[3, 0]);
            UpdateTile(lbl31, board[3, 1]);
            UpdateTile(lbl32, board[3, 2]);
            UpdateTile(lbl33, board[3, 3]);
        }

        private void UpdateScore(int s)
        {
            lbScore.Text = string.Format("Score: {0}", s.ToString());
        }

        private void UpdateStatus(string s)
        {
            //Skip
            if (s.Equals("Alive"))
                return;

            //If Over

            btnDown.Enabled = false;
            btnLeft.Enabled = false;
            btnRight.Enabled = false;
            btnUp.Enabled = false;

            //Simple Notice
            MessageBox.Show( 
                                string.Format("You {0}!", s.Equals("Over") ? "Lose" : "Won"),
                                s.Equals("Over") ? "Game Over" : "Congratulations", 
                                MessageBoxButtons.OK
                           );
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.LEFT);
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.UP);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            controller.ActionPerformed(TwoZeroFourEightController.DOWN);
        }

        private void TwoZeroFourEightView_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    e.IsInputKey = true;    this.btnUp.Focus();                                         //Force focus
                    controller.ActionPerformed(TwoZeroFourEightController.UP);
                    break;

                case Keys.Down:
                case Keys.S:
                    e.IsInputKey = true;    this.btnDown.Focus();                                        //Force focus
                    controller.ActionPerformed(TwoZeroFourEightController.DOWN);
                    break;

                case Keys.Left:
                case Keys.A:
                    e.IsInputKey = true;    this.btnLeft.Focus();                                        //Force focus
                    controller.ActionPerformed(TwoZeroFourEightController.LEFT);
                    break;

                case Keys.Right:
                case Keys.D:
                    e.IsInputKey = true;    this.btnRight.Focus();                                        //Force focus
                    controller.ActionPerformed(TwoZeroFourEightController.RIGHT);
                    break;
            }

        }

    }
}
