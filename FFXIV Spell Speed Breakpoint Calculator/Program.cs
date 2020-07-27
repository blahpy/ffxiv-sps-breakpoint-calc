using System;

namespace FFXIV_Spell_Speed_Breakpoint_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the calculator!");
            //These are unused variables that may be added later to expand the calculator for other uses
            /* int weaponDamage = 0;
            int attackMagicPower = 0;
            int det = 0;
            int autoAttackDelay = 0;
            int jobMod = 0;
            int baseDet = 0;
            int attackPowerDiv = 0;
            int detDiv = 0; 
            int spellSpeed = 0; */

            Console.WriteLine("Please enter your character level:");
            int level = 80;
            try
            {
                level = Convert.ToInt32(Console.ReadLine());
            }
            catch (Exception e) {
                Console.WriteLine("Error: " + e.Message);
                Console.WriteLine("Outputting default Level 80 information.");
            }

            double lowestGCD = 3.00;
            double gCD = 2.50;
            Console.WriteLine("GCD    Sps");
            LevelModifiers lvlMods = GetLevelModifiers(level);
            for (int i=lvlMods.lvlModSUB;i<4500 && gCD>1.50;i++) {
                gCD = GetGCD(i, lvlMods);
                if (gCD < lowestGCD) {
                    Console.WriteLine(String.Format("{0:0.00}", gCD) + "   " + i);
                    lowestGCD = gCD;
                }
            }
        }

        static LevelModifiers GetLevelModifiers(int level) {
            int[] lvlModSUBTable = new int[80]{ 56, 57, 60, 62, 65, 68, 70, 73, 76, 78, 82, 85, 89, 93, 96, 100, 104, 109, 113, 116, 122, 127, 133, 138, 144, 150, 155, 162, 168, 173, 181, 188, 194, 202, 209, 215, 223, 229, 236, 244, 253, 263, 272, 283, 292, 302, 311, 322, 331, 341, 342, 344, 345, 346, 347, 349, 350, 351, 352, 354, 355, 356, 357, 358, 359, 360, 361, 362, 363, 364, 365, 366, 367, 368, 370, 372, 374, 376, 378, 380 };
            int[] lvlModDIVTable = new int[80]{ 56, 57, 60, 62, 65, 68, 70, 73, 76, 78, 82, 85, 89, 93, 96, 100, 104, 109, 113, 116, 122, 127, 133, 138, 144, 150, 155, 162, 168, 173, 181, 188, 194, 202, 209, 215, 223, 229, 236, 244, 253, 263, 272, 283, 292, 302, 311, 322, 331, 341, 393, 444, 496, 548, 600, 651, 703, 755, 806, 858, 941, 1032, 1133, 1243, 1364, 1497, 1643, 1802, 1978, 2170, 2263, 2360, 2461, 2566, 2676, 2790, 2910, 3034, 3164, 3300 };
            LevelModifiers result = new LevelModifiers();
            if (level > 0 && level < 81)
            {
                result.lvlModSUB = lvlModSUBTable[level - 1];
                result.lvlModDIV = lvlModDIVTable[level - 1];
                return result;
            }
            else {
                result.lvlModSUB = 380; //Level 80 value
                result.lvlModDIV = 3300; //Level 80 value
                return result;
            };
        }

        static double GetGCD(int spellSpeed, LevelModifiers lvlMods) {
            int lvlModSUB = lvlMods.lvlModSUB;
            int lvlModDIV = lvlMods.lvlModDIV;
            int actionDelay = 2500;
            double gCDm = Math.Floor((1000 - Math.Floor((double)(130 * (spellSpeed - lvlModSUB) / lvlModDIV))) * actionDelay / 1000);
            double gCDc = Math.Floor(gCDm / 10);
            return gCDc / 100;
        }
    }
}
