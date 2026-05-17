namespace StalkerAidsAndSupplementsMod.Afflictions
{
    internal class AfflictionUtils
    {
        internal static void TrackCigaretteUsage()
        {
            SmokingDataManager.Instance.OnCigaretteSmoked();
        }
    }
}