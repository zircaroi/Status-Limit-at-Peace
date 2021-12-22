using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;
using UnityEngine;

namespace StatusLimitAtPeace
{
    public class Settings : ModSettings
    {
        //Toggles
        public bool ApplyOnPlayerColonists = true;
        public bool ApplyOnPlayerNotColonists = true;
        public bool ApplyOnNotEnemyFactionPawns = false;
        public bool ApplyOnEnemyFactionPawns = false;

        //public bool RaidBeaconIsHighDanger = true;

        //public bool UseNormalizedMoveSpeed = false;
        //or
        //public bool CompensateWeatherEffectsOnMoveSpeed = false;
        //public bool CompensateTerrainEffectsOnMoveSpeed = false;

        public bool GiddyUpMountPawnIgnoreMoveSpeed = true;


        //Variables for Max Stats
        //P: Peace, LD: Low Danger, HD: High Danger
        public float Move_P = 15f;
        public float Eating_P = 150f;
        public float GeneralLabor_P = 400f;
        public float Construction_P = 400f;
        public float PlantWork_P = 400f;
        public float Mining_P = 400f;

        public float Move_LD = 30f;
        public float Eating_LD = 150f;
        public float GeneralLabor_LD = 400f;
        public float Construction_LD = 400f;
        public float PlantWork_LD = 400f;
        public float Mining_LD= 400f;

        public float Move_HD = 30f;
        public float Eating_HD = 99999f;
        public float GeneralLabor_HD = 99999f;
        public float Construction_HD = 99999f;
        public float PlantWork_HD = 99999f;
        public float Mining_HD = 99999f;

        //Buffers

        private string Move_B_P;
        private string Eating_B_P;
        private string GeneralLabor_B_P;
        private string Construction_B_P;
        private string PlantWork_B_P;
        private string Mining_B_P;

        private string Move_B_LD;
        private string Eating_B_LD;
        private string GeneralLabor_B_LD;
        private string Construction_B_LD;
        private string PlantWork_B_LD;
        private string Mining_B_LD;

        private string Move_B_HD;
        private string Eating_B_HD;
        private string GeneralLabor_B_HD;
        private string Construction_B_HD;
        private string PlantWork_B_HD;
        private string Mining_B_HD;
        public override void ExposeData()
        {
            Scribe_Values.Look<bool>(ref this.ApplyOnPlayerColonists, "ApplyOnPlayerColonists", true);
            Scribe_Values.Look<bool>(ref this.ApplyOnPlayerNotColonists, "ApplyOnPlayerNotColonists", true);
            Scribe_Values.Look<bool>(ref this.ApplyOnNotEnemyFactionPawns, "ApplyOnNotEnemyFactionPawns", false);
            //Scribe_Values.Look<bool>(ref this.ApplyOnEnemyFactionPawns, "ApplyOnEnemyFactionPawns", true);

            Scribe_Values.Look(ref Move_P, "MaxMoveSpeedPeace", 15f);
            Scribe_Values.Look(ref Eating_P, "MaxEatingSpeedPeace", 150f);
            Scribe_Values.Look(ref GeneralLabor_P, "MaxGeneralLaborSpeedPeace", 400f);
            Scribe_Values.Look(ref Construction_P, "MaxConstructionSpeedPeace", 400f);
            Scribe_Values.Look(ref PlantWork_P, "MaxPlantWorkSpeedPeace", 400f);
            Scribe_Values.Look(ref Mining_P, "MaxMiningSpeedPeace", 400f);

            Scribe_Values.Look(ref Move_LD, "MaxMoveSpeedLowDanger", 30f);
            Scribe_Values.Look(ref Eating_LD, "MaxEatingSpeedLowDanger", 150f);
            Scribe_Values.Look(ref GeneralLabor_LD, "MaxGeneralLaborSpeedLowDanger", 400f);
            Scribe_Values.Look(ref Construction_LD, "MaxConstructionSpeedLowDanger", 400f);
            Scribe_Values.Look(ref PlantWork_LD, "MaxPlantWorkSpeedLowDanger", 400f);
            Scribe_Values.Look(ref Mining_LD, "MaxMiningSpeedLowDanger", 400f);

            Scribe_Values.Look(ref Move_HD, "MaxMoveSpeedHighDanger", 30f);
            Scribe_Values.Look(ref Eating_HD, "MaxEatingSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref GeneralLabor_HD, "MaxGeneralLaborSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref Construction_HD, "MaxConstructionSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref PlantWork_HD, "MaxPlantWorkSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref Mining_HD, "MaxMiningSpeedHighDanger", 99999f);

            base.ExposeData();
        }

        public void DoWindowContents(Rect inRect)
        {
            float GapForSection1 = 150f;
            float GapForLines = 8f;

            Listing_Standard listingStandard = new Listing_Standard();
            Listing_Standard listingSection1 = new Listing_Standard();

            List<String> maxLabels = new List<string>()
            {
                "",
                Translator.Translate("MaxMoveSpeed"),
                Translator.Translate("MaxEatingSpeed"),
                Translator.Translate("MaxGeneralLaborSpeed"),
                Translator.Translate("MaxConstructionSpeed"),
                Translator.Translate("MaxPlantWorkSpeed"),
                Translator.Translate("MaxMiningSpeed")
            };

            List<String> unitLabels = new List<string>()
            {
                "",
                "[cell/s]",
                "[%]",
                "[%]",
                "[%]",
                "[%]",
                "[%]"
            };

            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled(Translator.Translate("ApplyOnPlayerColonists"), ref this.ApplyOnPlayerColonists);
            listingStandard.Gap(GapForLines);
            listingStandard.CheckboxLabeled(Translator.Translate("ApplyOnPlayerNotColonists"), ref this.ApplyOnPlayerNotColonists);
            listingStandard.Gap(GapForLines);
            listingStandard.CheckboxLabeled(Translator.Translate("ApplyOnNotEnemyFactionPawns"), ref this.ApplyOnNotEnemyFactionPawns);
            listingStandard.Gap(GapForLines);
            listingStandard.CheckboxLabeled(Translator.Translate("ApplyOnEnemyFactionPawns"), ref this.ApplyOnEnemyFactionPawns);
            listingStandard.Gap(GapForLines);

            listingStandard.Gap(250f);
            listingStandard.Label(Translator.Translate("DisableNote"), -1);
            listingStandard.End();

            listingSection1.Begin(inRect);

            /* First Column = Labels */
            listingSection1.ColumnWidth = 180f;
            listingSection1.Gap(GapForSection1);
            foreach (string T in maxLabels)
            {
                listingSection1.Label(T);
                listingSection1.Gap(GapForLines);

            }

            /* Second Column = Units */
            listingSection1.NewColumn();
            listingSection1.ColumnWidth = 60f;
            listingSection1.Gap(GapForSection1);
            foreach (string T in unitLabels)
            {
                listingSection1.Label(T);
                listingSection1.Gap(GapForLines);

            }

            listingSection1.NewColumn();
            listingSection1.ColumnWidth = 180f;
            listingSection1.Gap(GapForSection1);
            listingSection1.Label(Translator.Translate("Peace"), -1);
            listingSection1.Gap(GapForLines);

            listingSection1.TextFieldNumeric<float>(ref this.Move_P, ref this.Move_B_P, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Eating_P, ref this.Eating_B_P, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.GeneralLabor_P, ref this.GeneralLabor_B_P, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Construction_P, ref this.Construction_B_P, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.PlantWork_P, ref this.PlantWork_B_P, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Mining_P, ref this.Mining_B_P, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);

            listingSection1.NewColumn();
            //listingSection1.ColumnWidth = 180f;
            listingSection1.Gap(GapForSection1);
            listingSection1.Label(Translator.Translate("LowDanger"), -1);
            listingSection1.Gap(GapForLines);

            listingSection1.TextFieldNumeric<float>(ref this.Move_LD, ref this.Move_B_LD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Eating_LD, ref this.Eating_B_LD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.GeneralLabor_LD, ref this.GeneralLabor_B_LD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Construction_LD, ref this.Construction_B_LD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.PlantWork_LD, ref this.PlantWork_B_LD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Mining_LD, ref this.Mining_B_LD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);

            listingSection1.NewColumn();
            //listingSection1.ColumnWidth = 180f;
            listingSection1.Gap(GapForSection1);
            listingSection1.Label(Translator.Translate("HighDanger"), -1);
            listingSection1.Gap(GapForLines);

            listingSection1.TextFieldNumeric<float>(ref this.Move_HD, ref this.Move_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Eating_HD, ref this.Eating_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.GeneralLabor_HD, ref this.GeneralLabor_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Construction_HD, ref this.Construction_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.PlantWork_HD, ref this.PlantWork_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Mining_HD, ref this.Mining_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);

            listingSection1.End();
        }
    }

    public class StatusLimitAtPeace : Mod
    {
        Settings settings;

        public StatusLimitAtPeace(ModContentPack content) : base(content)
        {
            this.GetSettings<Settings>();
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            base.GetSettings<Settings>().DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Status Limit at Peace";
        }


    }

    public class StatusLimitStatPart : StatPart
    {
        public string TargetValue;

        public override string ExplanationPart(StatRequest req)
        {
            string ModNameDisplay = " (Status Limit at Peace)";


            //Is req Pawn?
            bool flag1 = req.HasThing && req.Thing.Spawned && req.Thing.def.race != null && req.Thing.Map != null;


            //Check Ignore GiddyUpPawns
            /*
            bool flag6 = false;

            List<ModMetaData> LoadedMods = ModsConfig.ActiveModsInLoadOrder.ToList<ModMetaData>();
            bool giddyuploaded = false;
            for (int j = 0; j < LoadedMods.Count; j++)
            {
                if (LoadedMods[j].PackageId.ToLower().Contains("roolo.giddyupcore"))
                {
                    giddyuploaded = true;
                }
            }

            if (giddyuploaded)
            {
                Console.WriteLine("GiddyUpLoaded");
                ExtendedDataStorage extendedDataStorage = GiddyUpCore.Base.Instance.GetExtendedDataStorage();
                Pawn pawn = req.Pawn;
                flag6 = extendedDataStorage.GetExtendedDataFor(pawn).mount != null && LoadedSetting.GiddyUpMountPawnIgnoreMoveSpeed;
            }
            else
            {
                Console.WriteLine("GiddyUpNotLoaded");
            }
            */
            
            

            //Check RaidBeacon is HighDaner
            //bool flag5 = req.Thing.Map.ra

            if (flag1)
            {
                //LoadSavedSetting
                Settings LoadedSetting = LoadedModManager.GetMod<StatusLimitAtPeace>().GetSettings<Settings>();



                //Check Apply PlayerColonist
                bool flag2 = req.Thing.Faction != null && req.Thing.Faction == Faction.OfPlayer && req.Thing.def.race.Humanlike && LoadedSetting.ApplyOnPlayerColonists;

                //Check Apply PlayerAnimals
                bool flag3 = req.Thing.Faction != null && req.Thing.Faction == Faction.OfPlayer && !req.Thing.def.race.Humanlike && LoadedSetting.ApplyOnPlayerNotColonists;

                //Check Apply NotEnemyFactionPawns
                bool flag4 = req.Thing.Faction != null && req.Thing.Faction != Faction.OfPlayer && req.Thing.Faction.PlayerRelationKind != FactionRelationKind.Hostile && LoadedSetting.ApplyOnNotEnemyFactionPawns;

                //Check Apply EnemyFactionPawns
                bool flag5 = req.Thing.Faction != null && req.Thing.Faction != Faction.OfPlayer && req.Thing.Faction.PlayerRelationKind == FactionRelationKind.Hostile && LoadedSetting.ApplyOnEnemyFactionPawns;

                if (flag2 || flag3 || flag4 || flag5)
                {
                    StoryDanger DangerRating = req.Thing.Map.dangerWatcher.DangerRating;
                    string DangerExplanation;
                    string Explanation = "";
                    string StringValue;
                    string StringValueWithLabel = "";

                    if (DangerRating == StoryDanger.None)
                    {
                        DangerExplanation = Translator.Translate("Peace");
                        Explanation = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.Explanation", DangerExplanation);
                        if (TargetValue == "MoveSpeed")
                        {
                            StringValue = LoadedSetting.Move_P.ToString("F2");
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxMoveSpeed", StringValue);
                        }
                        if (TargetValue == "EatingSpeed")
                        {
                            StringValue = LoadedSetting.Eating_P.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxEatingSpeed", StringValue);
                        }
                        if (TargetValue == "GeneralLaborSpeed")
                        {
                            StringValue = LoadedSetting.GeneralLabor_P.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxGeneralLaborSpeed", StringValue);
                        }
                        if (TargetValue == "ConstructionSpeed")
                        {
                            StringValue = LoadedSetting.Construction_P.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxConstructionSpeed", StringValue);
                        }
                        if (TargetValue == "PlantWorkSpeed")
                        {
                            StringValue = LoadedSetting.PlantWork_P.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxPlantWorkSpeed", StringValue);
                        }
                        if (TargetValue == "MiningSpeed")
                        {
                            StringValue = LoadedSetting.Mining_P.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxMiningSpeed", StringValue);
                        }

                    }
                    if (DangerRating == StoryDanger.Low)
                    {
                        DangerExplanation = Translator.Translate("LowDanger");
                        Explanation = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.Explanation", DangerExplanation);
                        if (TargetValue == "MoveSpeed")
                        {
                            StringValue = LoadedSetting.Move_LD.ToString("F2");
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxMoveSpeed", StringValue);
                        }
                        if (TargetValue == "EatingSpeed")
                        {
                            StringValue = LoadedSetting.Eating_LD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxEatingSpeed", StringValue);
                        }
                        if (TargetValue == "GeneralLaborSpeed")
                        {
                            StringValue = LoadedSetting.GeneralLabor_LD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxGeneralLaborSpeed", StringValue);
                        }
                        if (TargetValue == "ConstructionSpeed")
                        {
                            StringValue = LoadedSetting.Construction_LD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxConstructionSpeed", StringValue);
                        }
                        if (TargetValue == "PlantWorkSpeed")
                        {
                            StringValue = LoadedSetting.PlantWork_LD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxPlantWorkSpeed", StringValue);
                        }
                        if (TargetValue == "MiningSpeed")
                        {
                            StringValue = LoadedSetting.Mining_LD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxMiningSpeed", StringValue);
                        }
                    }
                    if (DangerRating == StoryDanger.High)
                    {
                        DangerExplanation = Translator.Translate("HighDanger");
                        Explanation = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.Explanation", DangerExplanation);
                        if (TargetValue == "MoveSpeed")
                        {
                            StringValue = LoadedSetting.Move_HD.ToString("F2");
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxMoveSpeed", StringValue);
                        }
                        if (TargetValue == "EatingSpeed")
                        {
                            StringValue = LoadedSetting.Eating_HD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxEatingSpeed", StringValue);
                        }
                        if (TargetValue == "GeneralLaborSpeed")
                        {
                            StringValue = LoadedSetting.GeneralLabor_HD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxGeneralLaborSpeed", StringValue);
                        }
                        if (TargetValue == "ConstructionSpeed")
                        {
                            StringValue = LoadedSetting.Construction_HD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxConstructionSpeed", StringValue);
                        }
                        if (TargetValue == "PlantWorkSpeed")
                        {
                            StringValue = LoadedSetting.PlantWork_HD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxPlantWorkSpeed", StringValue);
                        }
                        if (TargetValue == "MiningSpeed")
                        {
                            StringValue = LoadedSetting.Mining_HD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxMiningSpeed", StringValue);
                        }

                    }

                    return Explanation + StringValueWithLabel + ModNameDisplay;

                }
            }


            return new TaggedString(string.Empty);
        }


        public override void TransformValue(StatRequest req, ref float val)
        {

            //Is req Pawn?
            bool flag1 = req.HasThing && req.Thing.Spawned && req.Thing.def.race != null && req.Thing.Map != null;


            if (flag1)
            {
                //LoadSavedSetting
                Settings LoadedSetting = LoadedModManager.GetMod<StatusLimitAtPeace>().GetSettings<Settings>();

                //Check Apply PlayerColonist
                bool flag2 = req.Thing.Faction != null && req.Thing.Faction == Faction.OfPlayer && req.Thing.def.race.Humanlike && LoadedSetting.ApplyOnPlayerColonists;

                //Check Apply PlayerAnimals
                bool flag3 = req.Thing.Faction != null && req.Thing.Faction == Faction.OfPlayer && !req.Thing.def.race.Humanlike && LoadedSetting.ApplyOnPlayerNotColonists;

                //Check Apply NotEnemyFactionPawns
                bool flag4 = req.Thing.Faction != null && req.Thing.Faction != Faction.OfPlayer && req.Thing.Faction.PlayerRelationKind != FactionRelationKind.Hostile && LoadedSetting.ApplyOnNotEnemyFactionPawns;

                //Check Apply EnemyFactionPawns
                bool flag5 = req.Thing.Faction != null && req.Thing.Faction != Faction.OfPlayer && req.Thing.Faction.PlayerRelationKind == FactionRelationKind.Hostile && LoadedSetting.ApplyOnEnemyFactionPawns;



                if (flag2 || flag3 || flag4 || flag5)
                {
                    StoryDanger DangerRating = req.Thing.Map.dangerWatcher.DangerRating;

                    if (DangerRating == StoryDanger.None)
                    {
                        if (TargetValue == "MoveSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Move_P);
                        }
                        if (TargetValue == "EatingSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Eating_P / 100f);
                        }
                        if (TargetValue == "GeneralLaborSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.GeneralLabor_P / 100f);
                        }
                        if (TargetValue == "ConstructionSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Construction_P / 100f);
                        }
                        if (TargetValue == "PlantWorkSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.PlantWork_P / 100f);
                        }
                        if (TargetValue == "MiningSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Mining_P / 100f);
                        }
                    }
                    if (DangerRating == StoryDanger.Low)
                    {
                        if (TargetValue == "MoveSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Move_LD);
                        }
                        if (TargetValue == "EatingSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Eating_LD / 100f);
                        }
                        if (TargetValue == "GeneralLaborSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.GeneralLabor_LD / 100f);
                        }
                        if (TargetValue == "ConstructionSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Construction_LD / 100f);
                        }
                        if (TargetValue == "PlantWorkSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.PlantWork_LD / 100f);
                        }
                        if (TargetValue == "MiningSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Mining_LD / 100f);
                        }
                    }
                    if (DangerRating == StoryDanger.High)
                    {
                        if (TargetValue == "MoveSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Move_HD);
                        }
                        if (TargetValue == "EatingSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Eating_HD / 100f);
                        }
                        if (TargetValue == "GeneralLaborSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.GeneralLabor_HD / 100f);
                        }
                        if (TargetValue == "ConstructionSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Construction_HD / 100f);
                        }
                        if (TargetValue == "PlantWorkSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.PlantWork_HD / 100f);
                        }
                        if (TargetValue == "MiningSpeed")
                        {
                            val = Math.Min(val, LoadedSetting.Mining_HD / 100f);
                        }

                    }
                }
            }
        }

    }

}


