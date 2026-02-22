

namespace StalkerAidsAndSupplementsMod
{
    internal class SweetButtons
    {
        private static GameObject sweetenButton;

        internal static GearItem currentFoodItem;

        private static int sugarUnitsToUse = 2; 
        private static int jamPortionsToUse = 1;

        private static int GetSugarAmount(string itemName)
        {
            string lower = itemName.ToLowerInvariant();

            if (lower.Contains("bannock") || lower.Contains("porridge")) //if Gear_name contains bannock or porridge consume 5 tsp of sugar
            {
                return 5;  
            }

            return 2;  //else consume 2 tsp
        }
        //List of items with button function
        private static readonly string[] AllowedFoodItems =
        {
            "GEAR_CookedPorridge",
            "GEAR_CookedBannock",
            "GEAR_CookedBannockAcorn",

            //Drinks

            "GEAR_GreenTeaCup",
            "GEAR_BirchbarkTea",
            "GEAR_BurdockTea",
            "GEAR_ReishiTea",
            "GEAR_RoseHipTea",

            // PineNeedleTeaMod

   //         "GEAR_PineNeedleTea"
        };

        private static readonly string[] sugarOnlyItems =
        {
            "GEAR_CoffeeCup",
            "GEAR_AcornCoffeeCup"
        };
        private static readonly string[] JamOnlyItems =
        {
            //FoodPack by TKG

   //         "GEAR_Bread",
   //         "GEAR_CookedApplePie",
   //         "GEAR_Cookies"
        };

        internal static void InitializeItemDescriptionPage(ItemDescriptionPage itemDescriptionPage)
        {

            GameObject equipButton = itemDescriptionPage.m_MouseButtonEquip;
            sweetenButton = UnityEngine.Object.Instantiate<GameObject>(equipButton, equipButton.transform.parent, true);
            sweetenButton.transform.Translate(0f, -0.1f, 0);
            Utils.GetComponentInChildren<UILabel>(sweetenButton).text = Localization.Get("GAMEPLAY_ButtonSweeten");

            AddAction(sweetenButton, new System.Action(OnSweeten));

            SetSweetenActive(false);
        }

        private static void AddAction(GameObject button, System.Action action)
        {
            Il2CppSystem.Collections.Generic.List<EventDelegate> placeHolderList = new Il2CppSystem.Collections.Generic.List<EventDelegate>();
            placeHolderList.Add(new EventDelegate(action));
            Utils.GetComponentInChildren<UIButton>(button).onClick = placeHolderList;
        }

        internal static void SetSweetenActive(bool active)
        {
            NGUITools.SetActive(sweetenButton, active);
        }

        internal static bool AllowButton(string gearName)
        {
            if (gearName.Contains("Jam") || gearName.Contains("Sugar") || gearName.Contains("Fruit")) //if Gear_name contains Jam, sugar or fruit then don't show button
            {
                return false;
            }

            foreach (string item in AllowedFoodItems)
            {
                if (gearName.Contains(item))
                {
                    return true;
                }
            }

            foreach (string item in sugarOnlyItems)
            {
                if (gearName.Contains(item))
                {
                    return true;
                }
            }

            foreach (string item in JamOnlyItems)
            {
                if (gearName.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }
        private static bool IsSugarOnly(string gearName)
        {
            foreach (string item in sugarOnlyItems)
            {
                if (gearName.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
        private static bool IsJamOnly(string gearName)
        {
            foreach (string item in JamOnlyItems)
            {
                if (gearName.Contains(item))
                {
                    return true;
                }
            }
            return false;
        }
        private static void OnSweeten()
        {
            var foodItem = SweetButtons.currentFoodItem;

            if (foodItem == null) return;

            if (foodItem.GetNormalizedCondition() <= 0.2f) //Checks  if FoodItem condition is more than 20%
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CannotSweetenRuinedItem"));
                GameAudioManager.PlayGUIError();
                return;
            }

            FoodItem food = foodItem.GetComponent<FoodItem>();
            if (food != null)
            {
                float caloriePercent = food.m_CaloriesRemaining / food.m_CaloriesTotal;

                if (caloriePercent < 0.5f) //Checks if items remaining calories are 50% or more
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NotEnoughCaloriesOnItem"));
                    GameAudioManager.PlayGUIError();
                    return;
                }
            }

            int sugarNeeded = GetSugarAmount(foodItem.name);

            bool hasSugar = GameManager.GetInventoryComponent().GearInInventory("GEAR_SugarA", sugarNeeded);
            bool hasJam = GameManager.GetInventoryComponent().GearInInventory("GEAR_RosehipJam", jamPortionsToUse);
            bool isSugarOnly = IsSugarOnly(foodItem.name);
            bool isJamOnly = IsJamOnly(foodItem.name);

            // Store food data
            string originalName = foodItem.name;
            float condition = foodItem.CurrentHP;

            if (isSugarOnly)
            {
                if (hasSugar)
                {
                    UseSugar(originalName, condition, sugarNeeded);
                }
                else
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NoSugar"));
                    GameAudioManager.PlayGUIError();
                }
                return;
            }

            if (isJamOnly)
            {
                if (hasJam)
                {
                    UseJam(originalName, condition);
                }
                else
                {
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NoJam"));
                    GameAudioManager.PlayGUIError();
                }
                return;
            }

            if (hasJam)
            {
                UseJam(originalName, condition);
            }
            else if (hasSugar)
            {
                UseSugar(originalName, condition, sugarNeeded);  
            }
            else
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NotEnoughSugarOrJam"));
                GameAudioManager.PlayGUIError();
            }
        }

        private static string tempOriginalName;
        private static float tempCondition;
        private static int tempStackCount;

        private static void UseSugar(string originalName, float condition, int sugarAmount)
        {
            if (!GameManager.GetInventoryComponent().GearInInventory("GEAR_SugarA", sugarAmount))
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NotEnoughSugar"));
                GameAudioManager.PlayGUIError();
                return;
            }

            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_ProgressBarSweeten"), 2f, 0f, 0f,
                "", null, false, true, new System.Action<bool, bool, float>(OnSweetenWithSugarFinished));

            GameManager.GetInventoryComponent().RemoveGearFromInventory("GEAR_SugarA", sugarAmount);

            int stackCount = currentFoodItem.m_StackableItem != null ? currentFoodItem.m_StackableItem.m_Units : 1;

            GearItem.Destroy(currentFoodItem);

            tempOriginalName = originalName;
            tempCondition = condition;
            tempStackCount = stackCount;
        }
        private static void UseJam(string originalName, float condition)
        {
            if (!GameManager.GetInventoryComponent().GearInInventory("GEAR_RosehipJam", jamPortionsToUse))
            {
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_NotEnoughJam"));
                GameAudioManager.PlayGUIError();
                return;
            }

            GameAudioManager.PlayGuiConfirm();
            InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_ProgressBarSweeten"), 2f, 0f, 0f,
                "", null, false, true, new System.Action<bool, bool, float>(OnSweetenWithJamFinished));

            GameManager.GetInventoryComponent().RemoveGearFromInventory("GEAR_RosehipJam", jamPortionsToUse);

            int stackCount = currentFoodItem.m_StackableItem != null ? currentFoodItem.m_StackableItem.m_Units : 1;

            GearItem.Destroy(currentFoodItem);

            tempOriginalName = originalName;
            tempCondition = condition;
            tempStackCount = stackCount;  
        }
        private static void OnSweetenWithSugarFinished(bool success, bool playerCancel, float progress)
        {
            if (!success || playerCancel) return;

            string sweetenedName = tempOriginalName + "Sugar";

            GearItem sweetenedItem = GearItem.LoadGearItemPrefab(sweetenedName);

            if (sweetenedItem != null)
            {
                GearItem instantiateditem = sweetenedItem.GetComponent<GearItem>();
                if (instantiateditem != null)
                {
                    instantiateditem.m_CurrentHP = tempCondition;
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(instantiateditem, tempStackCount);
                }
            }
        }

        private static void OnSweetenWithJamFinished(bool success, bool playerCancel, float progress)
        {
            if (!success || playerCancel) return;

            string sweetenedName = tempOriginalName + "Jam";

            GearItem sweetenedItem = GearItem.LoadGearItemPrefab(sweetenedName);

            if (sweetenedItem != null)
            {
                GearItem instantiateditem = sweetenedItem.GetComponent<GearItem>();
                if (instantiateditem != null)
                {
                    instantiateditem.m_CurrentHP = tempCondition;
                    GameManager.GetPlayerManagerComponent().InstantiateItemInPlayerInventory(instantiateditem, tempStackCount);
                }
            }
        }
    }
    [HarmonyPatch(typeof(Panel_Inventory), nameof(Panel_Inventory.Initialize))]
    internal class SweetenInitialization
    {
        private static void Postfix(Panel_Inventory __instance)
        {
            SweetButtons.InitializeItemDescriptionPage(__instance.m_ItemDescriptionPage);
        }
    }

    [HarmonyPatch(typeof(ItemDescriptionPage), nameof(ItemDescriptionPage.UpdateGearItemDescription))]
    internal class UpdateSweetenButton
    {
        private static void Postfix(ItemDescriptionPage __instance, GearItem gi)
        {
            if (__instance != InterfaceManager.GetPanel<Panel_Inventory>()?.m_ItemDescriptionPage) return;

            SweetButtons.currentFoodItem = gi?.GetComponent<GearItem>();

            if (gi != null && SweetButtons.AllowButton(gi.name))
            {
                SweetButtons.SetSweetenActive(true);
            }
            else
            {
                SweetButtons.SetSweetenActive(false);
            }
        }
    }
}