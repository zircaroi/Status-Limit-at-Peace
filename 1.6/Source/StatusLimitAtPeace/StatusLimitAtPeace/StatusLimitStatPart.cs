using RimWorld;
using System;
using Verse;

namespace StatusLimitAtPeace
{
    public class StatusLimitStatPart : StatPart
    {
        public string TargetValue;

        private bool VerifyRequest(StatRequest req)
        {
            if (!req.HasThing || !req.Thing.Spawned || req.Thing.Map == null)
            {
                return false;
            }

            if (req.Thing.def.race == null && !req.Thing.def.IsBuildingArtificial)
            {
                return false;
            }

            if (req.Thing.def.IsBuildingArtificial)
            {
                return true;
            }

            Pawn pawn = req.Thing as Pawn;
            if (req.Thing.Faction == null || pawn == null)
            {
                return false;
            }

            if (!StatusLimitAtPeace.instance.SettingsObject.ApplyOnDraftedPawns && pawn.Drafted)
            {
                return false;
            }

            if (pawn.Faction == Faction.OfPlayer)
            {
                if (!StatusLimitAtPeace.instance.SettingsObject.ApplyOnPlayerColonists &&
                    !StatusLimitAtPeace.instance.SettingsObject.ApplyOnPlayerNotColonists)
                {
                    return false;
                }

                if (!StatusLimitAtPeace.instance.SettingsObject.ApplyOnPlayerNotColonists &&
                    !pawn.def.race.Humanlike)
                {
                    return false;
                }
            }
            else
            {
                if (!StatusLimitAtPeace.instance.SettingsObject.ApplyOnEnemyFactionPawns &&
                    pawn.Faction.PlayerRelationKind == FactionRelationKind.Hostile)
                {
                    return false;
                }

                if (!StatusLimitAtPeace.instance.SettingsObject.ApplyOnNotEnemyFactionPawns &&
                    pawn.Faction.PlayerRelationKind != FactionRelationKind.Hostile)
                {
                    return false;
                }
            }

            return true;
        }

        private StoryDanger GetDangerRating(StatRequest req)
        {
            var map = req.Thing?.Map;

            if (StatusLimitAtPeace.instance.SettingsObject.TreatNotPlayerColonyMapAsHighDanger && map != null && !map.IsPlayerHome)
            {
                return StoryDanger.High;
            }

            Pawn pawn = req.Thing as Pawn;
            if (pawn == null || pawn.Faction == null)
            {
                return map?.dangerWatcher.DangerRating ?? StoryDanger.High;
            }
            if (StatusLimitAtPeace.instance.SettingsObject.TreatDraftedPawnsAsHighDanger && pawn.Drafted)
            {
                return StoryDanger.High;
            }
            return map.dangerWatcher.DangerRating;
        }

        public override string ExplanationPart(StatRequest req)
        {
            var text = " (Status Limit at Peace)";
            var defaultValue = new TaggedString(string.Empty);
            if (!VerifyRequest(req))
            {
                return defaultValue;
            }

            //var dangerRating = req.Thing.Map.dangerWatcher.DangerRating;
            var dangerRating = GetDangerRating(req);

            string valueExplanation;
            if (req.Thing.def.IsBuildingArtificial)
            {
                var dangerType = "";
                var explanation = "";
                var activityValue = "";
                valueExplanation = "";
                if (TargetValue == "BedRestEffectiveness")
                {
                    switch (dangerRating)
                    {
                        case StoryDanger.None:
                            dangerType = "Peace".Translate();
                            activityValue = StatusLimitAtPeace.instance.SettingsObject.Bed_P.ToString();
                            break;
                        case StoryDanger.Low:
                            dangerType = "LowDanger".Translate();
                            activityValue = StatusLimitAtPeace.instance.SettingsObject.Bed_LD.ToString();
                            break;
                        case StoryDanger.High:
                            dangerType = "HighDanger".Translate();
                            activityValue = StatusLimitAtPeace.instance.SettingsObject.Bed_HD.ToString();
                            break;
                    }

                    explanation = "StatusLimitAtPeaceExplanationPart.Explanation".Translate(dangerType);
                    valueExplanation = "StatusLimitAtPeaceExplanationPart.MaxBedRestEffectiveness".Translate(activityValue);
                }

                if (TargetValue != "JoyGainFactor")
                {
                    return explanation + valueExplanation + text;
                }

                switch (dangerRating)
                {
                    case StoryDanger.None:
                        dangerType = "Peace".Translate();
                        activityValue = StatusLimitAtPeace.instance.SettingsObject.Joy_P.ToString();
                        break;
                    case StoryDanger.Low:
                        dangerType = "LowDanger".Translate();
                        activityValue = StatusLimitAtPeace.instance.SettingsObject.Joy_LD.ToString();
                        break;
                    case StoryDanger.High:
                        dangerType = "HighDanger".Translate();
                        activityValue = StatusLimitAtPeace.instance.SettingsObject.Joy_HD.ToString();
                        break;
                }

                explanation = "StatusLimitAtPeaceExplanationPart.Explanation".Translate(dangerType);
                valueExplanation = "StatusLimitAtPeaceExplanationPart.MaxJoyGainFactor".Translate(activityValue);

                return explanation + valueExplanation + text;
            }

            valueExplanation = "";
            var valueInformation = "";
            switch (dangerRating)
            {
                case StoryDanger.None:
                    {
                        valueExplanation =
                            "StatusLimitAtPeaceExplanationPart.Explanation".Translate((string)"Peace".Translate());
                        switch (TargetValue)
                        {
                            case "MoveSpeed":
                                {
                                    valueInformation =
                                        "StatusLimitAtPeaceExplanationPart.MaxMoveSpeed".Translate(
                                            StatusLimitAtPeace.instance.SettingsObject.Move_P.ToString("F2"));
                                    break;
                                }
                            case "EatingSpeed":
                                {
                                    valueInformation =
                                        "StatusLimitAtPeaceExplanationPart.MaxEatingSpeed".Translate(StatusLimitAtPeace.instance
                                            .SettingsObject.Eating_P.ToString());
                                    break;
                                }
                            case "GeneralLaborSpeed":
                                {
                                    valueInformation =
                                        "StatusLimitAtPeaceExplanationPart.MaxGeneralLaborSpeed".Translate(StatusLimitAtPeace
                                            .instance.SettingsObject.GeneralLabor_P.ToString());
                                    break;
                                }
                            case "ConstructionSpeed":
                                {
                                    valueInformation =
                                        "StatusLimitAtPeaceExplanationPart.MaxConstructionSpeed".Translate(StatusLimitAtPeace
                                            .instance.SettingsObject.Construction_P.ToString());
                                    break;
                                }
                            case "PlantWorkSpeed":
                                {
                                    valueInformation =
                                        "StatusLimitAtPeaceExplanationPart.MaxPlantWorkSpeed".Translate(StatusLimitAtPeace.instance
                                            .SettingsObject.PlantWork_P.ToString());
                                    break;
                                }
                            case "MiningSpeed":
                                {
                                    valueInformation =
                                        "StatusLimitAtPeaceExplanationPart.MaxMiningSpeed".Translate(StatusLimitAtPeace.instance
                                            .SettingsObject.Mining_P.ToString());
                                    break;
                                }
                            case "RestRateMultiplier":
                                {
                                    valueInformation =
                                        "StatusLimitAtPeaceExplanationPart.MaxRestRateMultiplier".Translate(StatusLimitAtPeace
                                            .instance.SettingsObject.Rest_P.ToString());
                                    break;
                                }
                        }

                        break;
                    }
                case StoryDanger.Low:
                    {
                        valueExplanation =
                            "StatusLimitAtPeaceExplanationPart.Explanation".Translate((string)"LowDanger".Translate());
                        switch (TargetValue)
                        {
                            case "MoveSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxMoveSpeed".Translate(
                                        StatusLimitAtPeace.instance.SettingsObject.Move_LD.ToString("F2"));
                                break;
                            case "EatingSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxEatingSpeed".Translate(StatusLimitAtPeace.instance
                                        .SettingsObject.Eating_LD.ToString());
                                break;
                            case "GeneralLaborSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxGeneralLaborSpeed".Translate(StatusLimitAtPeace
                                        .instance.SettingsObject.GeneralLabor_LD.ToString());
                                break;
                            case "ConstructionSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxConstructionSpeed".Translate(StatusLimitAtPeace
                                        .instance.SettingsObject.Construction_LD.ToString());
                                break;
                            case "PlantWorkSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxPlantWorkSpeed".Translate(StatusLimitAtPeace.instance
                                        .SettingsObject.PlantWork_LD.ToString());
                                break;
                            case "MiningSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxMiningSpeed".Translate(StatusLimitAtPeace.instance
                                        .SettingsObject.Mining_LD.ToString());
                                break;
                            case "RestRateMultiplier":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxRestRateMultiplier".Translate(StatusLimitAtPeace
                                        .instance.SettingsObject.Rest_LD.ToString());
                                break;
                        }

                        break;
                    }
                case StoryDanger.High:
                    {
                        valueExplanation =
                            "StatusLimitAtPeaceExplanationPart.Explanation".Translate((string)"HighDanger".Translate());
                        switch (TargetValue)
                        {
                            case "MoveSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxMoveSpeed".Translate(
                                        StatusLimitAtPeace.instance.SettingsObject.Move_HD.ToString("F2"));
                                break;
                            case "EatingSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxEatingSpeed".Translate(StatusLimitAtPeace.instance
                                        .SettingsObject.Eating_HD.ToString());
                                break;
                            case "GeneralLaborSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxGeneralLaborSpeed".Translate(StatusLimitAtPeace
                                        .instance.SettingsObject.GeneralLabor_HD.ToString());
                                break;
                            case "ConstructionSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxConstructionSpeed".Translate(StatusLimitAtPeace
                                        .instance.SettingsObject.Construction_HD.ToString());
                                break;
                            case "PlantWorkSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxPlantWorkSpeed".Translate(StatusLimitAtPeace.instance
                                        .SettingsObject.PlantWork_HD.ToString());
                                break;
                            case "MiningSpeed":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxMiningSpeed".Translate(StatusLimitAtPeace.instance
                                        .SettingsObject.Mining_HD.ToString());
                                break;
                            case "RestRateMultiplier":
                                valueInformation =
                                    "StatusLimitAtPeaceExplanationPart.MaxRestRateMultiplier".Translate(StatusLimitAtPeace
                                        .instance.SettingsObject.Rest_HD.ToString());
                                break;
                        }

                        break;
                    }
            }

            return valueExplanation + valueInformation + text;
        }

        public override void TransformValue(StatRequest req, ref float val)
        {
            if (!VerifyRequest(req))
            {
                return;
            }

            //var dangerRating = req.Thing.Map.dangerWatcher.DangerRating;
            var dangerRating = GetDangerRating(req);

            if (req.Thing.def.IsBuildingArtificial)
            {
                switch (TargetValue)
                {
                    case "BedRestEffectiveness":
                        {
                            switch (dangerRating)
                            {
                                case StoryDanger.None:
                                    val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Bed_P / 100f);
                                    break;
                                case StoryDanger.Low:
                                    val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Bed_LD / 100f);
                                    break;
                                case StoryDanger.High:
                                    val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Bed_HD / 100f);
                                    break;
                            }

                            break;
                        }
                    case "JoyGainFactor":
                        {
                            switch (dangerRating)
                            {
                                case StoryDanger.None:
                                    val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Joy_P / 100f);
                                    break;
                                case StoryDanger.Low:
                                    val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Joy_LD / 100f);
                                    break;
                                case StoryDanger.High:
                                    val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Joy_HD / 100f);
                                    break;
                            }

                            break;
                        }
                }

                return;
            }


            switch (dangerRating)
            {
                case StoryDanger.None:
                    {
                        switch (TargetValue)
                        {
                            case "MoveSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Move_P);
                                break;
                            case "EatingSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Eating_P / 100f);
                                break;
                            case "GeneralLaborSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.GeneralLabor_P / 100f);
                                break;
                            case "ConstructionSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Construction_P / 100f);
                                break;
                            case "PlantWorkSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.PlantWork_P / 100f);
                                break;
                            case "MiningSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Mining_P / 100f);
                                break;
                            case "RestRateMultiplier":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Rest_P / 100f);
                                break;
                        }

                        break;
                    }
                case StoryDanger.Low:
                    {
                        switch (TargetValue)
                        {
                            case "MoveSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Move_LD);
                                break;
                            case "EatingSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Eating_LD / 100f);
                                break;
                            case "GeneralLaborSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.GeneralLabor_LD / 100f);
                                break;
                            case "ConstructionSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Construction_LD / 100f);
                                break;
                            case "PlantWorkSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.PlantWork_LD / 100f);
                                break;
                            case "MiningSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Mining_LD / 100f);
                                break;
                            case "RestRateMultiplier":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Rest_LD / 100f);
                                break;
                        }

                        break;
                    }
                case StoryDanger.High:
                    {
                        switch (TargetValue)
                        {
                            case "MoveSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Move_HD);
                                break;
                            case "EatingSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Eating_HD / 100f);
                                break;
                            case "GeneralLaborSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.GeneralLabor_HD / 100f);
                                break;
                            case "ConstructionSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Construction_HD / 100f);
                                break;
                            case "PlantWorkSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.PlantWork_HD / 100f);
                                break;
                            case "MiningSpeed":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Mining_HD / 100f);
                                break;
                            case "RestRateMultiplier":
                                val = Math.Min(val, StatusLimitAtPeace.instance.SettingsObject.Rest_HD / 100f);
                                break;
                        }

                        break;
                    }
            }
        }

        /*
        public override string ExplanationPart(StatRequest req)
        {
            string ModNameDisplay = " (Status Limit at Peace)";

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
                        if (TargetValue == "RestRateMultiplier")
                        {
                            StringValue = LoadedSetting.Rest_P.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxRestRateMultiplier", StringValue);
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
                        if (TargetValue == "RestRateMultiplier")
                        {
                            StringValue = LoadedSetting.Rest_LD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxRestRateMultiplier", StringValue);
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
                        if (TargetValue == "RestRateMultiplier")
                        {
                            StringValue = LoadedSetting.Rest_HD.ToString();
                            StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxRestRateMultiplier", StringValue);
                        }

                    }

                    return Explanation + StringValueWithLabel + ModNameDisplay;

                }
            }

            bool flagb1 = req.HasThing && req.Thing.Spawned && req.Thing.def.IsBuildingArtificial && req.Thing.Map != null;

            if (flagb1)
            {
                Settings LoadedSetting = LoadedModManager.GetMod<StatusLimitAtPeace>().GetSettings<Settings>();

                StoryDanger DangerRating = req.Thing.Map.dangerWatcher.DangerRating;
                string DangerExplanation = "";
                string Explanation = "";
                string StringValue = "";
                string StringValueWithLabel = "";

                if (TargetValue == "BedRestEffectiveness")
                {
                    if (DangerRating == StoryDanger.None)
                    {
                        DangerExplanation = Translator.Translate("Peace");
                        StringValue = LoadedSetting.Bed_P.ToString();
                    }
                    if (DangerRating == StoryDanger.Low)
                    {
                        DangerExplanation = Translator.Translate("LowDanger");
                        StringValue = LoadedSetting.Bed_LD.ToString();
                    }
                    if (DangerRating == StoryDanger.High)
                    {
                        DangerExplanation = Translator.Translate("HighDanger");
                        StringValue = LoadedSetting.Bed_HD.ToString();
                    }
                    Explanation = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.Explanation", DangerExplanation);
                    StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxBedRestEffectiveness", StringValue);
                }

                if (TargetValue == "JoyGainFactor")
                {
                    if (DangerRating == StoryDanger.None)
                    {
                        DangerExplanation = Translator.Translate("Peace");
                        StringValue = LoadedSetting.Joy_P.ToString();
                    }
                    if (DangerRating == StoryDanger.Low)
                    {
                        DangerExplanation = Translator.Translate("LowDanger");
                        StringValue = LoadedSetting.Joy_LD.ToString();
                    }
                    if (DangerRating == StoryDanger.High)
                    {
                        DangerExplanation = Translator.Translate("HighDanger");
                        StringValue = LoadedSetting.Joy_HD.ToString();
                    }
                    Explanation = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.Explanation", DangerExplanation);
                    StringValueWithLabel = TranslatorFormattedStringExtensions.Translate("StatusLimitAtPeaceExplanationPart.MaxJoyGainFactor", StringValue);
                }

                return Explanation + StringValueWithLabel + ModNameDisplay;
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
                        if (TargetValue == "RestRateMultiplier")
                        {
                            val = Math.Min(val, LoadedSetting.Rest_P / 100f);
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
                        if (TargetValue == "RestRateMultiplier")
                        {
                            val = Math.Min(val, LoadedSetting.Rest_LD / 100f);
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
                        if (TargetValue == "RestRateMultiplier")
                        {
                            val = Math.Min(val, LoadedSetting.Rest_HD / 100f);
                        }

                    }
                }
            }

            bool flagb1 = req.HasThing && req.Thing.Spawned && req.Thing.def.IsBuildingArtificial && req.Thing.Map != null;

            if (flagb1)
            {
                //LoadSavedSetting
                Settings LoadedSetting = LoadedModManager.GetMod<StatusLimitAtPeace>().GetSettings<Settings>();
                StoryDanger DangerRating = req.Thing.Map.dangerWatcher.DangerRating;

                if (TargetValue == "BedRestEffectiveness")
                {
                    if (DangerRating == StoryDanger.None)
                    {
                        val = Math.Min(val, LoadedSetting.Bed_P / 100f);
                    }
                    if (DangerRating == StoryDanger.Low)
                    {
                        val = Math.Min(val, LoadedSetting.Bed_LD / 100f);
                    }
                    if (DangerRating == StoryDanger.High)
                    {
                        val = Math.Min(val, LoadedSetting.Bed_HD / 100f);
                    }
                }

                if (TargetValue == "JoyGainFactor")
                {
                    if (DangerRating == StoryDanger.None)
                    {
                        val = Math.Min(val, LoadedSetting.Joy_P / 100f);
                    }
                    if (DangerRating == StoryDanger.Low)
                    {
                        val = Math.Min(val, LoadedSetting.Joy_LD / 100f);
                    }
                    if (DangerRating == StoryDanger.High)
                    {
                        val = Math.Min(val, LoadedSetting.Joy_HD / 100f);
                    }
                }

            }

        }
        */
    }
}
