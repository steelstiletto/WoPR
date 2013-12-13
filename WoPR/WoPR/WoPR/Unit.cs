using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoPR
{
    public class Unit
    {
        public const int MAXHP = 100;

        public enum armorType { infantry, armor, air, demon };
        public enum moveType { foot, tread, air };
        public enum unitType { trooper, demolitionSquad, samTrooper, halftrack, flameTank, heavyTank, flakCannon, artillery, helicopter, imp, harpy, behemoth, hellCannon };

        private int hp;
        private int cost;
        private int movementSpeed;
        private int movementPoints;
        private double armor;
        public bool moved;
        public bool acted;

        private armorType armorT;
        private moveType moveT;

        private Attack primaryAttack;
        private Attack secondaryAttack;
        private Player owner;

        public unitType t {get; private set;}

        //In: type of unit to be created, owner of created unit
        //Out: unit of specified details
        public Unit(unitType type, Player p)
        {
            hp = 100;
            owner = p;
            t = type;
            moved = true;
            acted = true;

            //switch to set stats based on given type
            switch (type)
            {
                //infantry
                case unitType.trooper:
                    cost = 100;
                    movementSpeed = 2;
                    armor = 0.3;
                    armorT = armorType.infantry;
                    moveT = moveType.foot;
                    primaryAttack = new Attack(Attack.Name.rifle); //assault rifle
                    secondaryAttack = new Attack(Attack.Name.flameThrower); //flame thrower
                    break;

                case unitType.demolitionSquad:
                    cost = 200;
                    movementSpeed = 1;
                    armor = 0.35;
                    armorT = armorType.infantry;
                    moveT = moveType.foot;
                    primaryAttack = new Attack(Attack.Name.mortar); //mortar
                    secondaryAttack = new Attack(Attack.Name.bazooka); //bazooka
                    break;

                case unitType.samTrooper:
                    cost = 200;
                    movementSpeed = 1;
                    armor = 0.3;
                    armorT = armorType.infantry;
                    moveT = moveType.foot;
                    primaryAttack = new Attack(Attack.Name.samLauncher); //sam launcher
                    secondaryAttack = new Attack(Attack.Name.rifle); //rifle
                    break;

                //Vehicles
                case unitType.halftrack:
                    cost = 400;
                    movementSpeed = 9;
                    armor = 0.4;
                    armorT = armorType.armor;
                    moveT = moveType.tread;
                    primaryAttack = new Attack(Attack.Name.hmg); //Heavy Machine Gun
                    secondaryAttack = new Attack(Attack.Name.hitNRun); //Hit and Run - no counter attack
                    break;

                case unitType.flameTank:
                    cost = 500;
                    movementSpeed = 7;
                    armor = 0.45;
                    armorT = armorType.armor;
                    moveT = moveType.tread;
                    primaryAttack = new Attack(Attack.Name.flameThrower); //flame launcher
                    secondaryAttack = new Attack(Attack.Name.lightCannon); //light cannon
                    break;

                case unitType.heavyTank:
                    cost = 700;
                    movementSpeed = 6;
                    armor = 0.5;
                    armorT = armorType.armor;
                    moveT = moveType.tread;
                    primaryAttack = new Attack(Attack.Name.heavyCannon); //heavy cannon
                    secondaryAttack = new Attack(Attack.Name.lmg); //lmg
                    break;

                case unitType.flakCannon:
                    cost = 600;
                    movementSpeed = 7;
                    armor = 0.4;
                    armorT = armorType.armor;
                    moveT = moveType.tread;
                    primaryAttack = new Attack(Attack.Name.flakCannon); //flak cannon
                    secondaryAttack = new Attack(Attack.Name.lmg); //lmg
                    break;

                case unitType.artillery:
                    cost = 700;
                    movementSpeed = 6;
                    armor = 0.4;
                    armorT = armorType.armor;
                    moveT = moveType.tread;
                    primaryAttack = new Attack(Attack.Name.bombardment); //bombardment - AoE
                    secondaryAttack = new Attack(Attack.Name.lmg); //lmg
                    break;

                //aircraft
                case unitType.helicopter:
                    cost = 700;
                    movementSpeed = 8;
                    armor = 0.4;
                    armorT = armorType.air;
                    moveT = moveType.air;
                    primaryAttack = new Attack(Attack.Name.RocketPods); //Rocket Pods
                    secondaryAttack = new Attack(Attack.Name.Minigun); //minigun
                    break;

                //demons
                case unitType.imp:
                    cost = 100;
                    movementSpeed = 7;
                    armor = 0.3;
                    armorT = armorType.demon;
                    moveT = moveType.foot;
                    primaryAttack = new Attack(Attack.Name.fireball); //fireball
                    secondaryAttack = new Attack(Attack.Name.claws); //claws
                    break;

                case unitType.harpy:
                    cost = 100;
                    movementSpeed = 7;
                    armor = 0.3;
                    armorT = armorType.air;
                    moveT = moveType.air;
                    primaryAttack = new Attack(Attack.Name.claws); //claws
                    secondaryAttack = new Attack(Attack.Name.skyDive); //sky dive - no counter attack
                    break;

                case unitType.behemoth:
                    cost = 100;
                    movementSpeed = 6;
                    armor = 0.6;
                    armorT = armorType.demon;
                    moveT = moveType.foot;
                    primaryAttack = new Attack(Attack.Name.demonBlade); //Demonic Blade
                    secondaryAttack = new Attack(Attack.Name.darkPulse); //Dark Pulse - AoE centered on unit
                    break;

                case unitType.hellCannon:
                    cost = 100;
                    movementSpeed = 5;
                    armor = 0.4;
                    armorT = armorType.demon;
                    moveT = moveType.foot;
                    primaryAttack = new Attack(Attack.Name.siegeCannon); //siege cannon - AoE
                    secondaryAttack = new Attack(Attack.Name.exhaustVent); //exhaust vent
                    break;

            }

            movementPoints = movementSpeed;
        }

        //input: attack to be tested
        //output: bool representing whether or not the attack can target this unit
        public bool attackable(Attack a)
        {
            bool valid = true;
            switch (a.getType())
            {
                case Attack.Type.shell:
                    if (armorT == armorType.air || armorT == armorType.infantry)
                    {
                        valid = false;
                    }
                    break;

                case Attack.Type.aa:
                    if (armorT != armorType.air)
                    {
                        valid = false;
                    }
                    break;

                case Attack.Type.flame:
                    if (armorT == armorType.armor || armorT == armorType.air)
                    {
                        valid = false;
                    }
                    break;

                case Attack.Type.blade:
                    if (armorT == armorType.air)
                    {
                        valid = false;
                    }
                    break;
            }
            return valid;
        }

        //input: attack to be tested
        //output: bool representing whether or not the attack deals double damage
        public bool superEffective(Attack a)
        {
            bool valid = false;
            switch (a.getType())
            {
                case Attack.Type.shell:
                    if (armorT == armorType.armor)
                    {
                        valid = true;
                    }
                    break;

                case Attack.Type.aa:
                    if (armorT == armorType.air)
                    {
                        valid = true;
                    }
                    break;

                case Attack.Type.flame:
                    if (armorT == armorType.infantry || armorT == armorType.demon)
                    {
                        valid = true;
                    }
                    break;

                case Attack.Type.bullet:
                    if (armorT == armorType.infantry)
                    {
                        valid = true;
                    }
                    break;
            }
            return valid;
        }

        //input: attack to be tested
        //output: bool representing whether or not the attack deals half damage
        public bool notEffective(Attack a)
        {
            bool valid = false;
            switch (a.getType())
            {
                case Attack.Type.aa:
                    if (armorT == armorType.air)
                    {
                        valid = true;
                    }
                    break;

                case Attack.Type.flame:
                    if (armorT == armorType.infantry || armorT == armorType.demon)
                    {
                        valid = true;
                    }
                    break;

                case Attack.Type.bullet:
                    if (armorT == armorType.infantry)
                    {
                        valid = true;
                    }
                    break;
            }
            return !valid;
        }
        //returns if the unit is dead
        public bool isDead()
        {
            return (hp > 0);
        }

        //input: attack targeting this unit, Tile of unit being attacked
        //output: amount of damage the unit suffered from the attack
        public int damage(Attack a, Tile t)
        {
            int dealt = 0;
            Random rand = new Random();

            //formula:   (damage roll) X (Armor) X (Effective damage modifier) X (Terrain bonus)
            dealt = (int)(((double)rand.Next(a.getMinDamage(), a.getMaxDamage())) * (1 - armor) * (1 - t.getDBonus())); //roll damage and multiply against armor
            if (superEffective(a)) //double damage for attack effective against armor
            {
                dealt *= 2;
            }
            else if (notEffective(a)) //half damage for attack weak against armor
            {
                dealt = (int)((double)dealt / (double)2);
            }

            //adjust hp and return
            hp -= dealt;
            if (hp <= 0)
            {
                owner.unitList.Remove(this);
                t.unit = null;
            }
            return dealt;
        }

        public void heal(int amount)
        {
            hp += amount;
            if (hp > MAXHP)
            {
                hp = MAXHP;
            }
        }
        //output: resets the unit to maximum move points
        public void resetMovepoints()
        {
            movementPoints = movementSpeed;
        }

        public bool move(int pointCost)
        {
            if (pointCost > movementPoints)
            {
                return false;
            }
            else
            {
                movementPoints -= pointCost;
                return true;
            }
        }

        //Get Methods
        public int getHp()
        {
            return hp;
        }
        public int getMoveSpeed()
        {
            return movementSpeed;
        }
        public int getMovePoints()
        {
            return movementPoints;
        }
        public double getArmor()
        {
            return armor;
        }
        public armorType getArmorType()
        {
            return armorT;
        }
        public moveType getMoveType()
        {
            return moveT;
        }
        public Attack getPrimaryAttack()
        {
            return primaryAttack;
        }
        public Attack getSecondaryAttack()
        {
            return secondaryAttack;
        }
        public Player getOwner()
        {
            return owner;
        }
    }
}
