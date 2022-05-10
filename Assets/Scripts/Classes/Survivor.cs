namespace Classes
{
    public class Survivor
    {
        public string name;
        public int wounds;
        public bool isAlive;
        public int actions;

        public Survivor(string survivorName)
        {
            name = survivorName;
            isAlive = true;
            actions = 3;
            wounds = 0;
        }

        public void ReceiveWound()
        {
            if (isAlive)
            {
                if (wounds < 2)
                {
                    wounds += 1;
                    isAlive = false;
                }
            }
        }
    }
}