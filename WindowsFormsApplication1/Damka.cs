using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CheckersLogic;

namespace Checkers
{
    public partial class Damka : Form
    {
        private Game_Settings m_GameSettingsForm = new Game_Settings();
        private Board m_Board;
        private int m_Player1Score = 0;
        private int m_Player2Score = 0;
        private Label LabelPlayer1 = new Label();
        private Label LabelPlayer2 = new Label();
        private CheckersButton[,] m_ButtonBoard;

        public Damka()
        {
            InitializeComponent();
        }

        private void Damka_Load(object sender, EventArgs e)
        {
            m_GameSettingsForm.ShowDialog();
            this.Font = new Font(this.Font, FontStyle.Bold);
            this.Size = new Size(m_GameSettingsForm.BoardSize * 50 + 100, m_GameSettingsForm.BoardSize * 50 + 100);
            LabelPlayer1.Text = "Player2: " + m_Player1Score;
            LabelPlayer1.Location = new Point(50, 20);
            LabelPlayer2.Text = "Player2: " + m_Player2Score;
            LabelPlayer2.Location = new Point(LabelPlayer1.Location.X + 50 * (m_GameSettingsForm.BoardSize / 2), LabelPlayer1.Location.Y);
            Controls.Add(LabelPlayer1);
            Controls.Add(LabelPlayer2);
            m_Board = new Board(m_GameSettingsForm.BoardSize);
            initializeButtons();
            setButtonBoardToStartGame();
        }

        private void setButtonBoardToStartGame()
        {
            int boardSize = m_GameSettingsForm.BoardSize;

            for (int i = 0; i < (boardSize / 2) - 1; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (m_ButtonBoard[i, j].BackColor != Color.Gray)
                    {
                        m_ButtonBoard[i, j].Text = "O";
                    }
                }
            }

            for (int i = (boardSize / 2) + 1; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if (m_ButtonBoard[i, j].BackColor != Color.Gray)
                    {
                        m_ButtonBoard[i, j].Text = "X";
                    }
                }
            }
        }

        private void initializeButtons()
        {
            m_ButtonBoard = new CheckersButton[m_GameSettingsForm.BoardSize, m_GameSettingsForm.BoardSize];
            CheckersButton checkersButton;
            bool aLeagalSquare;
            for (int i = 0; i < m_GameSettingsForm.BoardSize; i++)
            {
                for (int j = 0; j < m_GameSettingsForm.BoardSize; j++)
                {
                    checkersButton = new CheckersButton(i, j);
                    checkersButton.Size = new Size(50, 50);
                    checkersButton.Location = new Point(50 + j * 50, 50 + i * 50);
                    aLeagalSquare = (checkersButton.RowInBoard + checkersButton.ColInBoard) % 2 == 1;
                    if (!aLeagalSquare)
                    {
                        checkersButton.BackColor = Color.Gray;
                    }

                    m_ButtonBoard[i, j] = checkersButton;
                    Controls.Add(checkersButton);

                }
            }
        }
    }
}
