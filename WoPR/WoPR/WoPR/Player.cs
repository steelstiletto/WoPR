using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoPR
{
    class Player
    {
        public List<Unit> unitList;
        private int resources;
        public bool isPlayer1 { get; private set; }

        public Player(bool p1)
        {
            unitList = new List<Unit>();
            resources = 0;
            isPlayer1 = p1;
        }

        //input: amount spent
        //output: boolean expressing whether the transaction was possible, if true the resources were modified
        public bool spendResources(int amount)
        {
            if ((resources - amount) < 0)
            {
                return false;
            }
            else
            {
                resources -= amount;
                return true;
            }
            
        }

        //input amount to increase resources by
        public void grantIncome(int amount)
        {
            resources += amount;
        }
    }
}
