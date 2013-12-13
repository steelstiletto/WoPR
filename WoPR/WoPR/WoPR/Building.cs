using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoPR
{
    public class Building
    {
        public enum buildType {headquarters, barracks, garage, supplyDepot};

        private int incomeValue;
        private Unit.unitType[] buildable;

        public Building(buildType type)
        {
            switch (type)
            {
                case buildType.headquarters:
                    buildable = null;
                    incomeValue = 100;
                    break;

                case buildType.barracks:
                    buildable = new Unit.unitType[] {Unit.unitType.trooper, Unit.unitType.demolitionSquad,  Unit.unitType.samTrooper};
                    incomeValue = 100;
                    break;

                case buildType.garage:
                    buildable = new Unit.unitType[] {Unit.unitType.halftrack, Unit.unitType.flameTank, Unit.unitType.heavyTank, Unit.unitType.flakCannon, Unit.unitType.artillery, Unit.unitType.helicopter};
                    incomeValue = 100;
                    break;

                case buildType.supplyDepot:
                    buildable = null;
                    incomeValue = 100;
                    break;
            }
        }
    }
}
