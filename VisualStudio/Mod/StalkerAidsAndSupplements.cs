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
            MelonLogger.Msg(System.ConsoleColor.Yellow, "Welcome to the zone, stalker...");

            assetBundle = StalkerAidsAndSupplementsUtils.LoadFromStream("StalkerAidsAndSupplements.Assets.moremedsassets");

            Settings.instance.AddToModSettings("StalkerAidsAndSupplements");
        }
    }
}
 