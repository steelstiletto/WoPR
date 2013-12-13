using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WoPR
{
    public class Attack
    {
        public enum Type {bullet, shell, aa, flame, blade};
        public enum Name {rifle, flameThrower, mortar, bazooka, samLauncher, hmg, hitNRun, flameLauncher, lightCannon, heavyCannon, lmg, flakCannon, bombardment, RocketPods, Minigun, fireball, claws, skyDive, demonBlade, darkPulse, siegeCannon, exhaustVent};


        private int minDamage;
        private int maxDamage;
        private int range;
        private int radius;
        private String name;

        private Type t;

        public Attack(Name n)
        {
            //switch to set stats based on given type
            switch (n)
            {
                case Name.bazooka:
                    name = "Bazooka";
                    t = Type.shell;
                    maxDamage = 60;
                    minDamage = 50;
                    range = 0;
                    radius = 0;
                    break;

                case Name.bombardment:
                    name = "Artillery Bombardment";                    
                    t = Type.shell;
                    maxDamage = 80;
                    minDamage = 30;
                    range = 4;
                    radius = 2;
                    break;

                case Name.claws:
                    name = "Demon Claws";
                    t = Type.blade;
                    maxDamage = 40;
                    minDamage = 30;
                    range = 0;
                    radius = 0;
                    break;

                case Name.darkPulse:
                    name = "Dark Pulse";                    
                    t = Type.flame;
                    maxDamage = 60;
                    minDamage = 30;
                    range = 0;
                    radius = 1;
                    break;

                case Name.demonBlade:
                    name = "Demon Blade";
                    t = Type.blade;
                    maxDamage = 60;
                    minDamage = 50;
                    range = 0;
                    radius = 0;
                    break;

                case Name.exhaustVent:
                    name = "Vent Exhaust";
                    t = Type.flame;
                    maxDamage = 40;
                    minDamage = 30;
                    range = 0;
                    radius = 1;
                    break;

                case Name.fireball:
                    name = "Fireball";
                    t = Type.flame;
                    maxDamage = 50;
                    minDamage = 40;
                    range = 0;
                    radius = 0;
                    break;

                case Name.flakCannon:
                    name = "Flak Cannon";
                    t = Type.aa;
                    maxDamage = 80;
                    minDamage = 50;
                    range = 1;
                    radius = 1;
                    break;

                case Name.flameLauncher:
                    name = "Flame Launcher";
                    t = Type.flame;
                    maxDamage = 60;
                    minDamage = 50;
                    range = 0;
                    radius = 0;
                    break;

                case Name.flameThrower:
                    name = "Flame Thrower";
                    t = Type.flame;
                    maxDamage = 40;
                    minDamage = 30;
                    range = 0;
                    radius = 0;
                    break;

                case Name.heavyCannon:
                    name = "Heavy Cannon";
                    t = Type.shell;
                    maxDamage = 60;
                    minDamage = 50;
                    range = 0;
                    radius = 0;
                    break;

                case Name.hitNRun:
                    name = "Hit and Run";
                    t = Type.bullet;
                    maxDamage = 40;
                    minDamage = 30;
                    range = 0;
                    radius = 0;
                    break;

                case Name.hmg:
                    name = "Heavy Machine Gun";
                    t = Type.bullet;
                    maxDamage = 60;
                    minDamage = 50;
                    range = 0;
                    radius = 0;
                    break;

                case Name.lightCannon:
                    name = "Light Cannon";
                    t = Type.shell;
                    maxDamage = 40;
                    minDamage = 30;
                    range = 0;
                    radius = 0;
                    break;

                case Name.lmg:
                    name = "Light Machine Gun";
                    t = Type.bullet;
                    maxDamage = 40;
                    minDamage = 30;
                    range = 0;
                    radius = 0;
                    break;

                case Name.Minigun:
                    name = "Minigun";
                    t = Type.bullet;
                    maxDamage = 60;
                    minDamage = 40;
                    range = 0;
                    radius = 0;
                    break;

                case Name.mortar:
                    name = "Mortar";
                    t = Type.shell;
                    maxDamage = 60;
                    minDamage = 30;
                    range = 2;
                    radius = 1;
                    break;

                case Name.rifle:
                    name = "Assault Rifle";
                    t = Type.bullet;
                    maxDamage = 40;
                    minDamage = 30;
                    range = 0;
                    radius = 0;
                    break;

                case Name.RocketPods:
                    name = "Rocket Pods";
                    t = Type.shell;
                    maxDamage = 70;
                    minDamage = 40;
                    range = 0;
                    radius = 0;
                    break;

                case Name.samLauncher:
                    name = "Sam Launcher";
                    t = Type.aa;
                    maxDamage = 70;
                    minDamage = 40;
                    range = 0;
                    radius = 0;
                    break;

                case Name.siegeCannon:
                    name = "Siege Cannon";
                    t = Type.flame;
                    maxDamage = 60;
                    minDamage = 30;
                    range = 4;
                    radius = 2;
                    break;

                case Name.skyDive:
                    name = "Sky Dive";
                    t = Type.blade;
                    maxDamage = 40;
                    minDamage = 30;
                    range = 0;
                    radius = 0;
                    break;
            }
        }

        //GET METHODS
        public int getMinDamage()
        {
            return minDamage;
        }
        public int getMaxDamage()
        {
            return maxDamage;
        }
        public int getRange()
        {
            return range;
        }
        public int getRadius()
        {
            return radius;
        }
        public String getName()
        {
            return name;
        }
        public Type getType()
        {
            return t;
        }

    }
}
