namespace B16_Ex02
{
    internal class Player
    {
        private string m_Name;
        private byte m_Score;

        public Player(int playerNumber)
        {
            m_Name = "Player " + (playerNumber + 1);
            m_Score = 0;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }

            set
            {
                m_Name = value;
            }
        }

        public int Score
        {
            get
            {
                return (int)m_Score;
            }

            set
            {
                m_Score = (byte)value;
            }
        }
    }
}
