using AfflictionComponent.Components;
using Moment;
using Newtonsoft.Json;

namespace StalkerAidsAndSupplementsMod.Afflictions
{
    internal class AfflictionUtils
    {
        internal static void TrackCigaretteUsage()
        {
            SmokingDataManager.Instance.OnCigaretteSmoked();
        }
    }
    internal class SmokingDataManager : IScheduledEventExecutor
    {
        public string ScheduledEventExecutorId => "StalkerAids.SmokingManager";
        internal static SmokingDataManager Instance { get; private set; }

        private ModDataManager dataManager = new ModDataManager("StalkerAidsAndSupplements", false);

        // Cigarette counters
        internal static int Cigarettes_Recent = 0;
        internal static int Cigarettes_LifeTime = 0;
        internal static float Cigarette_Time_Last = 0f;
        internal static int Cigarettes_Risk_Active = 0;
        internal static int Cigarettes_Addiction_Active = 0;
        internal static float First_Cigarette_Time = 0f;

        private const float TIME_WINDOW = 720f;        
        private const int MINOR_LUNG_THRESHOLD = 150;

        private bool addictionPending = false; // tracks the 24 hour delay

        internal SmokingDataManager()
        {
            Instance = this;
            Moment.Moment.OnHourChanged += OnHourChanged;
        }
        internal class SmokingData
        {
            public int RecentCigarettes { get; set; }
            public int LifetimeCigarettes { get; set; }
            public float LastCigaretteTime { get; set; }
            public int CigarettesWhileAtRisk { get; set; }
            public int CigarettesWhileAddicted { get; set; }
            public bool AddictionPending { get; set; }
            public float FirstCigaretteTime { get; set; }
        }
        internal void Save()
        {
            SmokingData data = new SmokingData
            {
                RecentCigarettes = Cigarettes_Recent,
                LifetimeCigarettes = Cigarettes_LifeTime,
                LastCigaretteTime = Cigarette_Time_Last,
                CigarettesWhileAtRisk = Cigarettes_Risk_Active,
                CigarettesWhileAddicted = Cigarettes_Addiction_Active,
                AddictionPending = addictionPending,
                FirstCigaretteTime = First_Cigarette_Time,
            };
            string json = JsonConvert.SerializeObject(data);
            dataManager.Save(json);
        }
        internal void Load()
        {
            string? json = dataManager.Load();
            if (json == null)
            {
                return;
            }

            SmokingData? data = JsonConvert.DeserializeObject<SmokingData>(json);
            if (data == null) return;

            Cigarettes_Recent = data.RecentCigarettes;
            Cigarettes_LifeTime = data.LifetimeCigarettes;
            Cigarette_Time_Last = data.LastCigaretteTime;
            Cigarettes_Risk_Active = data.CigarettesWhileAtRisk;
            Cigarettes_Addiction_Active = data.CigarettesWhileAddicted;
            addictionPending = data.AddictionPending;
            First_Cigarette_Time = data.FirstCigaretteTime;
        }
        internal void OnCigaretteSmoked()
        {
            float coughRoll = UnityEngine.Random.Range(0f, 100f);
            if (coughRoll < 33f)
            {
                TriggerCough();
            }
            float currentTime = GameManager.GetTimeOfDayComponent().GetHoursPlayedNotPaused();

            if (currentTime - Cigarette_Time_Last > TIME_WINDOW) // Reset recent cigarettes if outside the time window
            {
                Cigarettes_Recent = 0;
                First_Cigarette_Time = currentTime;
            }
            if (Cigarettes_Recent == 0)
            {
                First_Cigarette_Time = currentTime;
            }
            // Update all counters
            Cigarettes_Recent++;
            Cigarettes_LifeTime++;
            Cigarette_Time_Last = currentTime;

            AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
            if (manager == null) return;

            // If already addicted, just track cigarettes while addicted
            if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
            {
                Cigarettes_Addiction_Active++;
                CheckLungDamage(manager);
                CheckMinorLungDamage(manager);
                ResetCureTimer("NicotineAddiction");
                Save();
                return;
            }
            // If at risk, notify the risk and roll for addiction
            if (manager.HasAfflictionOfType(typeof(AddictionRisk)))
            {
                Cigarettes_Risk_Active++;
                ResetCureTimer("AddictionRisk");
                CheckMinorLungDamage(manager);

                if (Cigarettes_Risk_Active >= 20) // 20 cigarettes = 100% (20 * 5 = 100)
                {
                    var risk = manager.GetAfflictionsOfType(typeof(AddictionRisk)).FirstOrDefault() as AddictionRisk;
                    risk?.Cure(false); 
                    addictionPending = true;
                    Moment.Moment.ScheduleRelative(this, new EventRequest((1, 0, 0), "startAddiction"));
                    Save();
                    return;
                }
                Save();
                return;
            }
            float daysElapsed = (currentTime - First_Cigarette_Time) / 24f;
            float cigarettesPerDay = Cigarettes_Recent / Mathf.Max(daysElapsed, 1f);

            if (cigarettesPerDay >= 6f)
            {
                TriggerAddictionRisk(manager);
            }
            else if (cigarettesPerDay >= 4f && Cigarettes_Recent >= 16)
            {
                TriggerAddictionRisk(manager);
            }
            else if (cigarettesPerDay >= 3f && Cigarettes_Recent >= 24)
            {
                TriggerAddictionRisk(manager);
            }
            CheckMinorLungDamage(manager);
            Save();
        }
        private void CheckLungDamage(AfflictionManager manager)
        {
            if (manager.HasAfflictionOfType(typeof(LungDamage))) return;

            float chance = Mathf.Clamp(Cigarettes_Addiction_Active * 2f, 0f, 25f);
            float roll = UnityEngine.Random.Range(0f, 100f);

            if (roll < chance)
            {
                LungDamage damage = new LungDamage();
                damage.Start();
            }
        }
        private void CheckMinorLungDamage(AfflictionManager manager)
        {
            // Fixed:
            if (Cigarettes_LifeTime >= MINOR_LUNG_THRESHOLD && !manager.HasAfflictionOfType(typeof(MinorLungDamage)) && !manager.HasAfflictionOfType(typeof(LungDamage)))
            {
                MinorLungDamage damage = new MinorLungDamage();
                damage.Start();
            }
        }
        public void Execute(TLDDateTime time, string eventType, string? eventId, string? eventData)
        {
            AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
            if (manager == null) return;

            switch (eventType)
            {
                case "cureAddictionRisk":
                    if (addictionPending) break;

                    if (manager.HasAfflictionOfType(typeof(AddictionRisk)))
                    {
                        var risk = manager.GetAfflictionsOfType(typeof(AddictionRisk)).FirstOrDefault() as AddictionRisk;
                        risk?.Cure();
                    }
                    break;
                case "cureNicotineAddiction":
                    if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                    {
                        var addiction = manager.GetAfflictionsOfType(typeof(NicotineAddiction)).FirstOrDefault() as NicotineAddiction;
                        addiction?.Cure();
                        Cigarettes_Addiction_Active = 0;
                    }
                    break;
                case "insomniaCheck":
                    if (manager.HasAfflictionOfType(typeof(NicotineAddiction)))
                    {
                        if (GameManager.GetInsomniaComponent().HasInsomniaAffliction())
                        {
                            return;
                        }

                        float roll = UnityEngine.Random.Range(0f, 100f);

                        if (roll < 25f)
                        {
                            GameManager.GetInsomniaComponent().ApplyInsomniaAffliction(Il2CppTLD.Gameplay.Condition.InsomniaCause.ElectrostaticFog);
                        }
                        Moment.Moment.ScheduleRelative(this, new EventRequest((0, 48, 0), "insomniaCheck"));
                    }
                    break;
                case "dailyAddictionRoll":
                    if (manager == null) break;
                    if (addictionPending) break;

                    if (!manager.HasAfflictionOfType(typeof(AddictionRisk))) break;
                    {
                        float rollChance = Mathf.Clamp(Cigarettes_Risk_Active * 2f, 0f, 35f);
                        float roll = UnityEngine.Random.Range(0f, 100f);

                        if (roll < rollChance)
                        {
                            var risk = manager.GetAfflictionsOfType(typeof(AddictionRisk)).FirstOrDefault() as AddictionRisk;
                            risk?.Cure(false);
                            addictionPending = true;
                            Moment.Moment.ScheduleRelative(this, new EventRequest((1, 0, 0), "startAddiction"));
                        }
                        else
                        {
                            Moment.Moment.ScheduleRelative(this, new EventRequest((1, 0, 0), "dailyAddictionRoll"));
                        }
                    }
                    break;
                case "startAddiction":
                    if (addictionPending)
                    {
                        NicotineAddiction addiction = new NicotineAddiction();
                        addiction.Start();
                        addictionPending = false;
                    }
                    break;
            }
            Save();
        }
        internal void ScheduleCureEvents(string afflictionType)
        {
            switch (afflictionType)
            {
                case "AddictionRisk":
                    if (!Moment.Moment.IsScheduled(ScheduledEventExecutorId, "cureAddictionRisk"))
                    {
                        Moment.Moment.ScheduleRelative(this, new EventRequest((7, 0, 0), "cureAddictionRisk"));
                    }
                    if (!Moment.Moment.IsScheduled(ScheduledEventExecutorId, "dailyAddictionRoll"))
                    {
                        Moment.Moment.ScheduleRelative(this, new EventRequest((1, 0, 0), "dailyAddictionRoll"));
                    }
                    break;
                case "NicotineAddiction":
                    if (!Moment.Moment.IsScheduled(ScheduledEventExecutorId, "cureNicotineAddiction"))
                    {
                        Moment.Moment.ScheduleRelative(this, new EventRequest((14, 0, 0), "cureNicotineAddiction"));
                    }
                    if (!Moment.Moment.IsScheduled(ScheduledEventExecutorId, "insomniaCheck"))
                    {
                        Moment.Moment.ScheduleRelative(this, new EventRequest((0, 48, 0), "insomniaCheck"));
                    }
                    break;
            }
        }
        internal void ResetCureTimer(string afflictionType)
        {
            switch (afflictionType)
            {
                case "AddictionRisk":
                    Moment.Moment.ScheduleRelative(this, new EventRequest((7, 0, 0), "cureAddictionRisk"));
                    break;
                case "NicotineAddiction":
                    Moment.Moment.ScheduleRelative(this, new EventRequest((14, 0, 0), "cureNicotineAddiction"));
                    break;
            }
        }
        private void OnHourChanged(TLDDateTime time)
        {
            AfflictionManager manager = AfflictionManager.GetAfflictionManagerInstance();
            if (manager == null) return;

            if (time.Hour == 0)
            {
                if (manager.HasAfflictionOfType(typeof(AddictionRisk)))
                {
                    if (Cigarettes_Risk_Active > 0)
                    {
                        Cigarettes_Risk_Active--;
                        if (Cigarettes_Risk_Active == 0)
                        {
                            var risk = manager.GetAfflictionsOfType(typeof(AddictionRisk)).FirstOrDefault() as AddictionRisk;
                            risk?.Cure();
                        }
                        Save();
                    }
                }
            }
            if (manager.HasAfflictionOfType(typeof(LungDamage)))
            {
                float severeCoughRoll = UnityEngine.Random.Range(0f, 100f);
                if (severeCoughRoll < 18f)
                {
                    TriggerCough();
                }
            }
            else if (manager.HasAfflictionOfType(typeof(MinorLungDamage)))
            {
                float minorCoughRoll = UnityEngine.Random.Range(0f, 100f);
                if (minorCoughRoll < 9f) 
                {
                    TriggerCough();
                }
            }
        }
        private void TriggerAddictionRisk(AfflictionManager manager)
        {
            AddictionRisk newRisk = new AddictionRisk();
            newRisk.Start();
            Cigarettes_Recent = 0;
            First_Cigarette_Time = 0f;
        }
        private void TriggerCough()
        {
            PlayerCough cough = GameManager.GetPlayerCough();
            if (cough == null || !cough.IsActive()) return;

            cough.MaybeStart("Play_SuffocationCough");
            MelonCoroutines.Start(StopCoughAfterDelay(3f)); // Coughing stops after 3 seconds
        }
        private System.Collections.IEnumerator StopCoughAfterDelay(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            PlayerCough cough = GameManager.GetPlayerCough();
            if (cough != null && cough.IsActive())
            {
                cough.Stop();
            }
        }
    }
}