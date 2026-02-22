using Il2CppRewired.ComponentControls.Data;
using ModSettings;
using MelonLoader;
using UnityEngine;

namespace StalkerAidsAndSupplementsMod
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();

        [Section("Vitamin C Settings")]

        [Name("Vitamin C per Dose")]
        [Description("Adjust how much Vitamin C a single dose of Vitamin C Pills provides. Default: 20. [Requires scene reload.]")]
        [Slider(10, 40, 7)]
        public int VitaminCSmall = 20;

        [Name("Calories per Dose")]
        [Description("Adjust how many calories a single dose of Vitamin C Pills provides. Default: 5. [Requires scene reload.]")]
        [Slider(5, 25, 5)]
        public int VitaminCalories = 5;

        [Section("Sleeping Settings")]

        [Name("Fatigue Change per Dose")]
        [Description("Adjust the fatigue change from one dose of Sleeping Pills. Default: -25%. [Requires scene reload.]")]
        [Slider(-25, -50, 6)]
        public int SleepingIncrease = -25;

        [Name("Calories per Dose")]
        [Description("Adjust how many calories a single dose of Sleeping Pills provides. Default: 5. [Requires scene reload.]")]
        [Slider(5, 25, 5)]
        public int SleepingCalories = 5;

        [Name("Condition Bonus per Hour Slept")]
        [Description("Adjust the additional condition gained per hour slept while the bonus is active. Default: +1 condition/hour. [Requires scene reload.]")]
        [Slider(0, 3, 7)]
        public float SleepingHP = 1;

        [Name("Condition Bonus Duration")]
        [Description("Adjust how long the condition rest bonus lasts per dose. Default: 3 hours. [Requires scene reload.]")]
        [Slider(0, 6, 13)]
        public float SleepingBonusDuration = 3;

        [Section("Caffeine Settings")]

        [Name("Fatigue Reduction per Dose")]
        [Description("Adjust the fatigue reduction from one dose of Caffeine Pills. Default: 25%. [Requires scene reload.]")]
        [Slider(25, 50 ,6)]
        public int CaffeineDecrease = 25;

        [Name("Calories per Dose")]
        [Description("Adjust how many calories a single dose of Caffeine Pills provides. Default: 5. [Requires scene reload.]")]
        [Slider(5, 25, 5)]
        public int CaffeineCalories = 5;

        [Name("Fatigue Reduction Duration")]
        [Description("Adjust how long the fatigue reduction lasts per dose. Default: 1 hour. [Requires scene reload.]")]
        [Slider(0, 3, 7)]
        public float CaffeineTime = 1;

        [Name("Carry Weight Bonus")]
        [Description("Adjust the carry weight bonus from one dose of Caffeine Pills. Default: 1.5 kilograms. [Requires scene reload.]")]
        [Slider(1, 3, 5)]
        public float CaffeineCarry = 1.5f;

        [Name("Carry Weight Bonus Duration")]
        [Description("Adjust how long the carry weight bonus lasts per dose. Default: 1.5 hours. [Requires scene reload.]")]
        [Slider(1, 2.5f, 4)]
        public float CaffeineCarryTime = 1.5f;

        /*   [Name("Remove HeadacheDebuff")]
           [Description("Removes the headache debuff from caffeine pills. [Requires scene reload.]")]
           public bool Headache = true;*/

        [Section("Jam Settings Settings")]
        

        [Name("Calories per Serving")]
        [Description("Adjust how many calories Rosehip Jam provides per serving. Default: 150. [Requires scene reload.]")]
        [Slider(50, 250, 9)]
        public int JamCalories = 150;

        [Name("Vitamin C per Serving")]
        [Description("Adjust how much Vitamin C Rosehip Jam provides per serving. Default: 24. [Requires scene reload.]")]
        [Slider(10, 50, 9)]
        public int VitaminCJam = 25;

        [Section("First Aid Kit Settings")]

        [Name("Condition Restoration per Hour")]
        [Description("Adjust how much condition the First Aid Kit restores per hour. Default: 5% per hour. [Requires scene reload.]")]
        [Slider(1, 7.5f, 14)]
        public float FirstAidAmount = 5;

        [Name("Condition Restoration Duration")]
        [Description("Adjust how long the condition-over-time effect lasts. Default: 6 hours. [Requires scene reload.]")]
        [Slider(2, 8, 13)]
        public float FirstAidTime = 6;

        [Section("Burdock Dressing Settings")]

        [Name("Condition Restoration per Hour")]
        [Description("Adjust how much condition Burdock Dressing restores per hour. Default: 1.5% per hour. [Requires scene reload.]")]
        [Slider(1.5f, 3f, 4)]
        public float BandageAmount = 1.5f;

        [Name("Condition Restoration Duration")]
        [Description("Adjust how long the condition-over-time effect lasts. Default: 2 hours. [Requires scene reload.]")]
        [Slider(1, 4, 7)]
        public float BandageTime = 2;

        [Section("Anabiotics Settings")]

        [Name("Condition Restoration per Hour Slept")]
        [Description("Adjust how much condition Anabiotics restore per hour slept. Default: 6% per hour. [Requires scene reload.]")]
        [Slider(3, 8, 11)]
        public float SleepHP = 6;

        [Name("Cabin Fever Reduction (Hours)")]
        [Description("Adjust how many cabin fever hours Anabiotics reduce. Default: 12 hours. [Requires scene reload.]")]
        [Slider(2, 16, 15)]
        public int CabinFeverHours = 12;

        [Section("Reset Settings")]

        [Name("Reset to Default Settings")]
        [Description("Resets all settings to default. (Confirmation and a scene reload/transition required.)")]
        public bool ResetSettings = false;

        protected override void OnConfirm()
        {
            ApplyReset();
            instance.ResetSettings = false;
            base.OnConfirm();
            instance.RefreshGUI();
        }
        public static void ApplyReset()
        {
            if(instance.ResetSettings==true) 
            {
                instance.VitaminCSmall = 20;
                instance.VitaminCalories = 5;
                instance.SleepingIncrease = -25;
                instance.SleepingCalories = 5;
                instance.SleepingBonusDuration = 3;
                instance.SleepingHP = 1;
                instance.CaffeineCalories = 5;
                instance.CaffeineDecrease = 25;
                instance.CaffeineTime = 1;
                instance.JamCalories = 150;
                instance.VitaminCJam = 24;
         //     instance.Headache = true;
                instance.FirstAidTime = 6;
                instance.FirstAidAmount = 5;
                instance.BandageTime = 2;
                instance.BandageAmount = 1.5f;
                instance.SleepHP = 6;
                instance.CaffeineCarry = 1.5f;
                instance.CaffeineCarryTime = 1.5f;
                instance.CabinFeverHours = 12;
                instance.RefreshGUI();
            }
        }
    }
    
}