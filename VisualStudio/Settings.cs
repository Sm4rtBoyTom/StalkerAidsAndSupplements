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

        [Name("Vitamin C Amount")]
        [Description("Adjust the amount of Vitamin C Vitamin Pills give per One Dose. - Default 20 [Requires scene reload.]")]
        [Slider(10, 40, 20)]
        public int VitaminCSmall = 20;

        [Name("Vitamin C Calories")]
        [Description("Adjust the amount of Calories Vitamin Pills give per One Dose. - Default 2 [Requires scene reload.]")]
        [Slider(1, 25, 24)]
        public int VitaminCalories = 2;

        [Section("Sleeping Settings")]

        [Name("Fatigue Increase Amount")]
        [Description("Adjust the amount of fatigue Sleeping Pills Increase per One Dose. - Default 25% [Requires scene reload.]")]
        [Slider(-15, -50, -25)]
        public int SleepingIncrease = -25;

        [Name("Sleeping Pills Calories")]
        [Description("Adjust the amount of Calories Sleeping Pills give per One Dose. - Default 2 [Requires scene reload.]")]
        [Slider(1, 25, 24)]
        public int SleepingCalories = 2;

        [Name("Condition Rest Bonus")]
        [Description("Adjust the amount of Additional Condition Received per Hour Rested. - Default +1HP [Requires scene reload.]")]
        [Slider(0, 3, 1)]
        public int SleepingHP = 1;

        [Name("Condition Rest Bonus Duration")]
        [Description("Adjust the Duration of Condition Rest Bonus per One Dose. - Default 3 Hours [Requires scene reload.]")]
        [Slider(0, 3, 1)]
        public int SleepingBonusDuration = 3;

        [Section("Caffeine Settings")]

        [Name("Fatigue Decrease Amount")]
        [Description("Adjust the amount of fatigue Caffeine Pills Decrease per One Dose. - Default 25% [Requires scene reload.]")]
        [Slider(15, 50 ,25)]
        public int CaffeineDecrease = 25;

        [Name("Caffeine Pills Calories")]
        [Description("Adjust the amount of Calories Caffeine Pills give per One Dose. - Default 2 [Requires scene reload.]")]
        [Slider(1, 25, 24)]
        public int CaffeineCalories = 2;

        [Name("Caffeine Pills Duration")]
        [Description("Adjust the amount of Time Fatigue Reduced Buff ĺasts per One Dose. - Default 1 Hours [Requires scene reload.]")]
        [Slider(0, 3, 1)]
        public int CaffeineTime = 1;

        [Name("Remove HeadacheDebuff")]
        [Description("Removes the headache debuff from caffeine pills. [Requires scene reload.]")]
        public bool Headache = false;

        [Section("Jam Settings Settings")]

        [Name("Jam Calories")]
        [Description("Adjust the amount of calories that Rosehip Jam has - Default 1500 [Requires scene reload.]")]
        [Slider(500f, 2500, 1500)]
        public int JamCalories = 1500;

        [Name("Jam Vitamin")]
        [Description("Adjust the Vitamin C Amount of Rosehip Jam - Default 240 [Requires scene reload.]")]
        [Slider(120, 480, 240)]
        public int VitaminCJam = 240;

        [Section("First Aid Kit Settings")]

        [Name("Instant HP Increase")]
        [Description("Adjust the amount of HP that First Aid Kit Replenishes Instantly - Default 10% [Requires scene reload.]")]
        [Slider(10, 25, 10)]
        public int FirstAidKit = 10;

        [Name("HP Increase Over Time")]
        [Description("Adjust the amount of HP First Aid Kit Replenishes Over Time - Default 3% Per Hour [Requires scene reload.]")]
        [Slider(1, 5, 1)]
        public int FirstAidAmount = 3;

        [Name("Condition Over Time Duration")]
        [Description("Adjust the amount of Hours that Condition over time Buff lasts - Default 4  Hours [Requires scene reload.]")]
        [Slider(2, 8, 1)]
        public int FirstAidTime = 4;

        [Section("Burdock Dressing Settings")]

        [Name("HP Increase Over Time")]
        [Description("Adjust the amount of HP that Burdock Dressing Replenishes Over Time - Default 1.5% Per Hour [Requires scene reload.]")]
        [Slider(1.5f, 3f, 1)]
        public float BandageAmount = 1.5f;

        [Name("Condition Over Time Duration")]
        [Description("Adjust the amount of Hours that Condition over time Buff lasts - Default 2  Hours [Requires scene reload.]")]
        [Slider(1, 4, 1)]
        public int BandageTime = 2;

        [Section("Ibuprofen Settings")]

        [Name("HP Increase Over Time")]
        [Description("Adjust the amount of HP that Ibuprofen Replenishes Over Time - Default 1.5% Per Hour [Requires scene reload.]")]
        [Slider(1f, 2.5f, 1)]
        public float IbuprofenAmount = 2f;

        [Name("Condition Over Time Duration")]
        [Description("Adjust the amount of Hours that Condition over time Buff lasts - Default 2  Hours [Requires scene reload.]")]
        [Slider(1f, 2.5f, 1)]
        public float IbuprofenTime = 1f;



        [Section("Reset Settings")]

        [Name("Reset to Default Settings")]
        [Description("\"Resets all settings to Default. (Confirm and scene reload/transition required.)")]
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
                instance.VitaminCalories = 2;
                instance.SleepingIncrease = -25;
                instance.SleepingCalories = 2;
                instance.SleepingBonusDuration = 3;
                instance.SleepingHP = 1;
                instance.CaffeineCalories = 2;
                instance.CaffeineDecrease = 25;
                instance.CaffeineTime = 1;
                instance.JamCalories = 1500;
                instance.VitaminCJam = 240;
                instance.FirstAidKit = 10;
                instance.Headache = true;
                instance.FirstAidTime = 4;
                instance.FirstAidAmount = 3;
                instance.BandageTime = 2;
                instance.BandageAmount = 1.5f;
                instance.IbuprofenAmount = 2f;
                instance.IbuprofenTime = 1;
                instance.RefreshGUI();
            }
        }
    }
    
}