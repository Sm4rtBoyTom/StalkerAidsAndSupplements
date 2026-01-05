using MelonLoader;
using StalkerAidsAndSupplementsMod;

namespace StalkerAidsAndSupplementsMod
{
    public class StalkerAidAndSupplements : MelonMod
    {

        private static AssetBundle? assetBundle;

        internal static AssetBundle JamTexturesBundle
        {
            get => assetBundle ?? throw new System.NullReferenceException(nameof(assetBundle));
        }

        public override void OnInitializeMelon()
        {
            MelonLoader.MelonLogger.Msg(System.ConsoleColor.Yellow, "Welcome to the zone, stalker...");
            assetBundle = StalkerAidsAndSupplementsMod.StalkerAidsAndSupplementsUtils.LoadFromStream("StalkerAidsAndSupplements.Assets.moremedsassets");
            Settings.instance.AddToModSettings("Stalker Aids And Supplements");
        }
    }
}