using AfflictionComponent.Components;
using ExpandedTradingFramework.Framework;
using StalkerAidsAndSupplementsMod.Afflictions;
using StalkerAidsAndSupplementsMod.Afflictions.Buffs;
using StalkerAidsAndSupplementsMod.Trades;

namespace StalkerAidsAndSupplementsMod
{
    internal class StalkerAidAndSupplements : MelonMod
    {
        private static AssetBundle? assetBundle;
        internal static AssetBundle JamTexturesBundle
        {
            get => assetBundle ?? throw new System.NullReferenceException(nameof(assetBundle));
        }
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg(System.ConsoleColor.DarkGreen, "Welcome to the Great Bear, S.T.A.L.K.E.R...");
            assetBundle = StalkerAidsAndSupplementsUtils.LoadFromStream("StalkerAidsAndSupplements.Resources.Assets.moremedsassets");
            Settings.instance.AddToModSettings("StalkerAidsAndSupplements");
            new SmokingDataManager();
            ConsoleCommands.Register();

            CustomTradeDefinition[] enabledTrades = CustomTrades.GetEnabledTrades();
            CustomTradeRegistry.RegisterExternalTrades(enabledTrades);
        }
    }
    internal static class ConsoleCommands
    {
        internal static void Register() //affliction commands
        {
            uConsole.RegisterCommand("NicotineAddiction", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null) return;
                foreach (var aff in mgr.m_Afflictions)
                    if (aff is NicotineAddiction) return;
                var affliction = new NicotineAddiction();
                affliction.Start();
            }));
            uConsole.RegisterCommand("NicotineAddiction_Risk", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null) return;
                foreach (var aff in mgr.m_Afflictions)
                    if (aff is AddictionRisk) return;
                var affliction = new AddictionRisk();
                affliction.Start();
            }));
            uConsole.RegisterCommand("LungDamage", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null) return;
                foreach (var aff in mgr.m_Afflictions)
                    if (aff is LungDamage) return;
                var affliction = new LungDamage();
                affliction.Start();
            }));
            uConsole.RegisterCommand("LungDamage_Minor", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null) return;
                foreach (var aff in mgr.m_Afflictions)
                    if (aff is LungDamage) return;
                var affliction = new MinorLungDamage();
                affliction.Start();
            }));
            uConsole.RegisterCommand("CigBuff", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null) return;
                foreach (var aff in mgr.m_Afflictions)
                    if (aff is LungDamage) return;
                var affliction = new CigaretteBuff();
                affliction.Start();
            }));
            //Cure Commands
            uConsole.RegisterCommand("NicotineAddiction_cure", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null || mgr.m_Afflictions == null) return;
                for (int i = mgr.m_Afflictions.Count - 1; i >= 0; i--)
                {
                    if (mgr.m_Afflictions[i] is NicotineAddiction)
                        mgr.m_Afflictions[i].Cure();
                }
            }));
            uConsole.RegisterCommand("NicotineAddiction_Risk_cure", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null || mgr.m_Afflictions == null) return;
                for (int i = mgr.m_Afflictions.Count - 1; i >= 0; i--)
                {
                    if (mgr.m_Afflictions[i] is AddictionRisk)
                        mgr.m_Afflictions[i].Cure();
                }
            }));
            uConsole.RegisterCommand("LungDamage_cure", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null || mgr.m_Afflictions == null) return;
                for (int i = mgr.m_Afflictions.Count - 1; i >= 0; i--)
                {
                    if (mgr.m_Afflictions[i] is LungDamage)
                        mgr.m_Afflictions[i].Cure();
                }
            }));
            uConsole.RegisterCommand("LungDamage_Minor_cure", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null || mgr.m_Afflictions == null) return;
                for (int i = mgr.m_Afflictions.Count - 1; i >= 0; i--)
                {
                    if (mgr.m_Afflictions[i] is MinorLungDamage)
                        mgr.m_Afflictions[i].Cure();
                }
            }));
            uConsole.RegisterCommand("CigBuff_cure", new Action(() =>
            {
                var mgr = AfflictionManager.GetAfflictionManagerInstance();
                if (mgr == null || mgr.m_Afflictions == null) return;
                for (int i = mgr.m_Afflictions.Count - 1; i >= 0; i--)
                {
                    if (mgr.m_Afflictions[i] is CigaretteBuff)
                        mgr.m_Afflictions[i].Cure();
                }
            }));
            uConsole.RegisterCommand("Aurora", new Action(() =>
            {
                var mgr = GameManager.GetAuroraManager();
                if (mgr == null) return;

                mgr.BoostAurora(true);
            }));
            uConsole.RegisterCommand("Aurora_Stop", new Action(() =>
            {
                var mgr = GameManager.GetAuroraManager();
                if (mgr == null) return;

                mgr.BoostAurora(false);
            }));
        }
    }
}
