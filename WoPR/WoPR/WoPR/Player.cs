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

        public Player()
        {
            unitList = new List<Unit>();
            resources = 0;
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
