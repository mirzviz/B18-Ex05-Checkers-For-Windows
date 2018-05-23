using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public class CheckersButton : Button
    {
        private int m_RowInBoard;
        public int RowInBoard
        {
            get
            {
                return m_RowInBoard;
            }
            set
            {
                m_RowInBoard = value;
            }
        }

        private int m_ColInBoard;
        public int ColInBoard
        {
            get
            {
                return m_ColInBoard;
            }
            set
            {
                m_ColInBoard = value;
            }
        }

        public CheckersButton(int i_RowInBoard, int i_ColInBoard)
        {
            m_RowInBoard = i_RowInBoard;
            m_ColInBoard = i_ColInBoard;
        }
        
    }
}
