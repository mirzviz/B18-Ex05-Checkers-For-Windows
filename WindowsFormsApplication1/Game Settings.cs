using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Game_Settings : Form
    {
        private int m_BoardSize;
        public int BoardSize
        {
            get
            {
                return m_BoardSize;
            }
            set
            {
                m_BoardSize = value;
            }
        }

        public Game_Settings()
        {
            InitializeComponent();
        }

        private void Game_Settings_Load(object sender, EventArgs e)
        {

        }

        private void CheckBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxPlayer2.Checked == true)
            {
                TextBoxPlayer2.Enabled = true;
            }
            else
            {
                TextBoxPlayer2.Enabled = false;
            }
        }
        
        private void buttonDone_Click(object sender, EventArgs e)
        {
            if(radioButtonSize6.Checked == true)
            {
                m_BoardSize = 6;
            }
            else if(radioButtonSize8.Checked == true)
            {
                m_BoardSize = 8;
            }
            else if (radioButtonSize10.Checked == true)
            {
                m_BoardSize = 10;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
