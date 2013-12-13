using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoPR
{
    public class Player
    {
        public List<Unit> unitList;
        public int resources { get; private set; }
        public bool isPlayer1 { get; private set; }
        public SimpleController controller;

        public Player(bool p1)
        {
            init(p1, 0, null);
        }
        public Player(bool p1, SimpleController controller)
        {
            init(p1, 0, controller);
        }

        public void init(bool p1, int resources, SimpleController controller)
        {
            unitList = new List<Unit>();
            this.resources = resources;
            isPlayer1 = p1;
            this.controller = controller;
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
