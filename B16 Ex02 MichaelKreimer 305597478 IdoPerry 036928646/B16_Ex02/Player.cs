using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex02
{
    internal class Player
    {
        private string m_name;
        private byte m_score;

        public Player(int playerNumber)
        {
            m_name = "Player " + (playerNumber + 1);
            m_score = 0;
        }

        public string Name
        {
            get
            {
                return m_name;
            }

            set
            {
                m_name = value;
            }
        }

        public int Score
        {
            get
            {
                return (int)m_score;
            }

            set
            {
                m_score = (byte)value;
            }
        }
    }
}
