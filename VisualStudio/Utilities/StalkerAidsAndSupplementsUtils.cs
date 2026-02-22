using StalkerAidsAndSupplementsMod;

namespace StalkerAidsAndSupplementsMod
{
    internal static class StalkerAidsAndSupplementsUtils
    {
        //Vitamin C setup 
        public static Il2CppSystem.Collections.Generic.List<FoodItem.Nutrient> VitC(int amount = 10)
        {
            FoodItem.Nutrient vitc = new FoodItem.Nutrient();
            vitc.m_Amount = amount;
            vitc.m_Nutrient = new Il2CppTLD.Gameplay.AssetReferenceNutrientDefinition("13a8bda1e12982e428b7551cc01b01df");
            Il2CppSystem.Collections.Generic.List<FoodItem.Nutrient> list = new Il2CppSystem.Collections.Generic.List<FoodItem.Nutrient>();
            list.Add(vitc);
            return list;
        }

        // Loading liquid materials from embedded asset bundle
        public static Material JamLiquidMaterial()
        {
            Texture2D LiquidTexture = StalkerAidAndSupplements.JamTexturesBundle.LoadAsset<Texture2D>("T_RosehipJam_Cooking");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }
        public static Material HerbalTeaLiquidMaterial()
        {
            Texture2D LiquidTexture = StalkerAidAndSupplements.JamTexturesBundle.LoadAsset<Texture2D>("T_HerbalTea_Cooking");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }
        public static Material AcornCoffeeLiquidMaterial()
        {
            Texture2D LiquidTexture = StalkerAidAndSupplements.JamTexturesBundle.LoadAsset<Texture2D>("T_AcornCoffee_Cooking");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }
        public static Material CoffeeLiquidMaterial()
        {
            Texture2D LiquidTexture = StalkerAidAndSupplements.JamTexturesBundle.LoadAsset<Texture2D>("T_Coffee_Cooking");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }
        public static Material ReishiTeaLiquidMaterial()
        {
            Texture2D LiquidTexture = StalkerAidAndSupplements.JamTexturesBundle.LoadAsset<Texture2D>("T_ReishiTea_Cooking");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }
        public static Material RoseHipTeaLiquidMaterial()
        {
            Texture2D LiquidTexture = StalkerAidAndSupplements.JamTexturesBundle.LoadAsset<Texture2D>("T_RoseHipTea_Cooking");
            Material LiquidMaterial = new Material(GearItem.LoadGearItemPrefab("GEAR_CoffeeCup").gameObject.GetComponent<Cookable>().m_CookingPotMaterialsList[0]);
            LiquidMaterial.SetTexture("_Main_texture2", LiquidTexture);
            return LiquidMaterial;
        }

        // Asset Bundle Loader
        public static AssetBundle LoadFromStream(string name)
        {
            using (Stream? stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                MemoryStream? memory = new((int)stream.Length);
                stream!.CopyTo(memory);

                Il2CppSystem.IO.MemoryStream memoryStream = new Il2CppSystem.IO.MemoryStream(memory.ToArray());

                AssetBundle loadFromMemoryInternal = AssetBundle.LoadFromStream(memoryStream);
                return loadFromMemoryInternal;
            };
        }
    }
}



