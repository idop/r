using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    internal class Player
    {
        private string m_name;
        private byte m_score;
        private bool m_isHuman;

        public Player(bool isHuman,int playerNumber)
        {
            m_name = isHuman ? "Human" : "Computer";
            m_name = m_name + playerNumber;
            m_score = 0;
            m_isHuman = isHuman;
        }

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }
        public int Score
        {
            get { return (int)m_score; }
            set { m_score = (byte)value; }
        }
        public bool IsHuman
        {
            get { return m_isHuman; }
        }

    }
}
