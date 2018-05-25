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
        private bool m_AButttonIsPressed;
        private CheckersButton m_ButtonPressed;
        
        public Damka()
        {
            InitializeComponent();
        }

        private void Damka_Load(object sender, EventArgs e)
        {
            m_GameSettingsForm.ShowDialog();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font(this.Font, FontStyle.Bold);
            this.Size = new Size(m_GameSettingsForm.BoardSize * 50 + 100, m_GameSettingsForm.BoardSize * 50 + 100);
            LabelPlayer1.Text = "Player1: " + m_Player1Score;
            LabelPlayer1.Location = new Point(50, 20);
            LabelPlayer2.Text = "Player2: " + m_Player2Score;
            LabelPlayer2.Location = new Point(LabelPlayer1.Location.X + 50 * (m_GameSettingsForm.BoardSize / 2), LabelPlayer1.Location.Y);
            Controls.Add(LabelPlayer1);
            Controls.Add(LabelPlayer2);
            m_Board = new Board(m_GameSettingsForm.BoardSize);
            initializeButtons();
            syncButtonBoardWithLogicalBoard();
        }

        private void syncButtonBoardWithLogicalBoard()
        {
            for(int i = 0; i < m_GameSettingsForm.BoardSize; i++)
            {
                for(int j = 0; j < m_GameSettingsForm.BoardSize; j++)
                {
                    Square currentSquareInLogicalBoard = m_Board.BoardMatrix[i, j];
                    if (currentSquareInLogicalBoard == Square.EMPTY)
                    {
                        m_ButtonBoard[i, j].Text = " ";
                    }
                    else if (currentSquareInLogicalBoard == Square.K)
                    {
                        m_ButtonBoard[i, j].Text = "K";
                    }
                    else if (currentSquareInLogicalBoard == Square.O)
                    {
                        m_ButtonBoard[i, j].Text = "O";
                    }
                    else if (currentSquareInLogicalBoard == Square.U)
                    {
                        m_ButtonBoard[i, j].Text = "U";
                    }
                    else if (currentSquareInLogicalBoard == Square.X)
                    {
                        m_ButtonBoard[i, j].Text = "X";
                    }
                }
            }
        }

        private void buttonFromBoard_Click(object sender, EventArgs e)
        {
            CheckersButton clickedButton = sender as CheckersButton;
            if (clickedButton != null)
            {

                if (clickedButton.BackColor == Color.Gray)
                {
                    goto End;
                }

                if (!m_AButttonIsPressed)
                {                    
                    m_AButttonIsPressed = true;
                    clickedButton.BackColor = Color.SkyBlue;
                    m_ButtonPressed = clickedButton;
                    goto End;
                }

                if(m_ButtonPressed == clickedButton)
                {
                    clickedButton.BackColor = Color.Empty;
                    m_AButttonIsPressed = false;
                    goto End;
                }

                Move move = twoButtonsToAMove(clickedButton);
                if (!m_Board.Play(move))
                {
                    MessageBox.Show("Illegal Move!");
                }

                handleGameOverAndRematch();

                m_AButttonIsPressed = false;
                m_ButtonPressed.BackColor = Color.Empty;
                if (!m_GameSettingsForm.MultiplayerMode)
                {
                    if (m_Board.Turn == Square.O)
                    {
                        m_Board.ComputerPlay();
                    }
                }

                handleGameOverAndRematch();
                syncButtonBoardWithLogicalBoard();
            }

            End:;
        }

        private void handleGameOverAndRematch()
        {
            if (!m_Board.GameIsOnKeepPlaying)
            {
                StringBuilder messege = new StringBuilder();
                messege.AppendFormat("{0} Won!", findWinnersNameAndGivePointsToWinner());
                messege.AppendLine();
                messege.AppendFormat("Another Round?");
                DialogResult result = MessageBox.Show(messege.ToString(), "Damka", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    this.Close();
                }
                else
                {
                    m_Board.initializeBoard();
                    LabelPlayer1.Text = "Player1: " + m_Player1Score;
                    LabelPlayer2.Text = "Player2: " + m_Player2Score;
                }
            }
        }
        private string findWinnersNameAndGivePointsToWinner()
        {
            int difference = ((m_Board.NumberOfK * 4) + m_Board.NumberOfX) - ((m_Board.NumberOfU * 4) + m_Board.NumberOfO);
            string winner;
            if (difference > 0)
            {                                       // X won = player1 won
                winner = "Player 1";
                m_Player1Score += difference;
            }
            else
            {
                winner = "Plaer 2";
                m_Player2Score -= difference;
            }

            return winner;
        }

        private Move twoButtonsToAMove(CheckersButton i_Button)
        {
            return new Move(m_ButtonPressed.RowInBoard, m_ButtonPressed.ColInBoard, i_Button.RowInBoard, i_Button.ColInBoard);
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
                    else
                    {
                        checkersButton.BackColor = Color.Empty;
                    }

                    checkersButton.Click += new EventHandler(buttonFromBoard_Click);
                    m_ButtonBoard[i, j] = checkersButton;
                    Controls.Add(checkersButton);

                }
            }
        }
    }
}
