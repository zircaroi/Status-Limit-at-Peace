using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace StatusLimitAtPeace
{
    public class Settings : ModSettings
    {
        //Toggles
        public bool ApplyOnPlayerColonists = true;
        public bool ApplyOnPlayerNotColonists = true;
        public bool ApplyOnNotEnemyFactionPawns = false;
        public bool ApplyOnEnemyFactionPawns = true;

        //1.5Add
        public bool ApplyOnDraftedPawns = true;

        //1.6Add
        public bool TreatDraftedPawnsAsHighDanger = true;
        public bool TreatNotPlayerColonyMapAsHighDanger = true;

        //Variables for Max Stats
        //P: Peace, LD: Low Danger, HD: High Danger
        public float Move_P = 15f;
        public float Eating_P = 150f;
        public float GeneralLabor_P = 400f;
        public float Construction_P = 400f;
        public float PlantWork_P = 400f;
        public float Mining_P = 400f;
        public float Rest_P = 150f;
        public float Bed_P = 150f;
        public float Joy_P = 200f;

        public float Move_LD = 30f;
        public float Eating_LD = 150f;
        public float GeneralLabor_LD = 400f;
        public float Construction_LD = 400f;
        public float PlantWork_LD = 400f;
        public float Mining_LD = 400f;
        public float Rest_LD = 400f;
        public float Bed_LD = 400f;
        public float Joy_LD = 400f;

        public float Move_HD = 30f;
        public float Eating_HD = 99999f;
        public float GeneralLabor_HD = 99999f;
        public float Construction_HD = 99999f;
        public float PlantWork_HD = 99999f;
        public float Mining_HD = 99999f;
        public float Rest_HD = 99999f;
        public float Bed_HD = 99999f;
        public float Joy_HD = 99999f;

        //Buffers

        private string Move_B_P;
        private string Eating_B_P;
        private string GeneralLabor_B_P;
        private string Construction_B_P;
        private string PlantWork_B_P;
        private string Mining_B_P;
        private string Rest_B_P;
        private string Bed_B_P;
        private string Joy_B_P;

        private string Move_B_LD;
        private string Eating_B_LD;
        private string GeneralLabor_B_LD;
        private string Construction_B_LD;
        private string PlantWork_B_LD;
        private string Mining_B_LD;
        private string Rest_B_LD;
        private string Bed_B_LD;
        private string Joy_B_LD;

        private string Move_B_HD;
        private string Eating_B_HD;
        private string GeneralLabor_B_HD;
        private string Construction_B_HD;
        private string PlantWork_B_HD;
        private string Mining_B_HD;
        private string Rest_B_HD;
        private string Bed_B_HD;
        private string Joy_B_HD;

        private Vector2 scrollPosition = Vector2.zero;

        //1.5Add
        public void Reset()
        {
            ApplyOnPlayerColonists = true;
            ApplyOnPlayerNotColonists = true;
            ApplyOnNotEnemyFactionPawns = false;
            ApplyOnEnemyFactionPawns = true;
            ApplyOnDraftedPawns = true;
            TreatDraftedPawnsAsHighDanger = true;
            TreatNotPlayerColonyMapAsHighDanger = true;
            Move_P = 15f;
            Move_B_P = "15";
            Eating_P = 150f;
            Eating_B_P = "150";
            GeneralLabor_P = 400f;
            GeneralLabor_B_P = "400";
            Construction_P = 400f;
            Construction_B_P = "400";
            PlantWork_P = 400f;
            PlantWork_B_P = "400";
            Mining_P = 400f;
            Mining_B_P = "400";
            Rest_P = 150f;
            Rest_B_P = "150";
            Bed_P = 150f;
            Bed_B_P = "150";
            Joy_P = 200f;
            Joy_B_P = "200";
            Move_LD = 30f;
            Move_B_LD = "30";
            Eating_LD = 150f;
            Eating_B_LD = "150";
            GeneralLabor_LD = 400f;
            GeneralLabor_B_LD = "400";
            Construction_LD = 400f;
            Construction_B_LD = "400";
            PlantWork_LD = 400f;
            PlantWork_B_LD = "400";
            Mining_LD = 400f;
            Mining_B_LD = "400";
            Rest_LD = 400f;
            Rest_B_LD = "400";
            Bed_LD = 400f;
            Bed_B_LD = "400";
            Joy_LD = 400f;
            Joy_B_LD = "400";
            Move_HD = 30f;
            Move_B_HD = "30";
            Eating_HD = 99999f;
            Eating_B_HD = "99999";
            GeneralLabor_HD = 99999f;
            GeneralLabor_B_HD = "99999";
            Construction_HD = 99999f;
            Construction_B_HD = "99999";
            PlantWork_HD = 99999f;
            PlantWork_B_HD = "99999";
            Mining_HD = 99999f;
            Mining_B_HD = "99999";
            Rest_HD = 99999f;
            Rest_B_HD = "99999";
            Bed_HD = 99999f;
            Bed_B_HD = "99999";
            Joy_HD = 99999f;
            Joy_B_HD = "99999";
        }

        public override void ExposeData()
        {
            Scribe_Values.Look<bool>(ref this.ApplyOnPlayerColonists, "ApplyOnPlayerColonists", true);
            Scribe_Values.Look<bool>(ref this.ApplyOnPlayerNotColonists, "ApplyOnPlayerNotColonists", true);
            Scribe_Values.Look<bool>(ref this.ApplyOnNotEnemyFactionPawns, "ApplyOnNotEnemyFactionPawns", false);
            Scribe_Values.Look<bool>(ref this.ApplyOnEnemyFactionPawns, "ApplyOnEnemyFactionPawns", true);

            //1.5Add
            Scribe_Values.Look(ref ApplyOnDraftedPawns, "ApplyOnDraftedPawns", true);

            //1.6Add
            Scribe_Values.Look(ref TreatDraftedPawnsAsHighDanger, "TreatDraftedPawnsAsHighDanger", true);
            Scribe_Values.Look(ref TreatNotPlayerColonyMapAsHighDanger, "TreatNotPlayerColonyMapAsHighDanger", true);

            Scribe_Values.Look(ref Move_P, "MaxMoveSpeedPeace", 15f);
            Scribe_Values.Look(ref Eating_P, "MaxEatingSpeedPeace", 150f);
            Scribe_Values.Look(ref GeneralLabor_P, "MaxGeneralLaborSpeedPeace", 400f);
            Scribe_Values.Look(ref Construction_P, "MaxConstructionSpeedPeace", 400f);
            Scribe_Values.Look(ref PlantWork_P, "MaxPlantWorkSpeedPeace", 400f);
            Scribe_Values.Look(ref Mining_P, "MaxMiningSpeedPeace", 400f);
            Scribe_Values.Look(ref Rest_P, "MaxRestRateMultiplierPeace", 150f);
            Scribe_Values.Look(ref Bed_P, "MaxBedRestEffectivenessPeace", 150f);
            Scribe_Values.Look(ref Joy_P, "MaxJoyGainFactorPeace", 200f);

            Scribe_Values.Look(ref Move_LD, "MaxMoveSpeedLowDanger", 30f);
            Scribe_Values.Look(ref Eating_LD, "MaxEatingSpeedLowDanger", 150f);
            Scribe_Values.Look(ref GeneralLabor_LD, "MaxGeneralLaborSpeedLowDanger", 400f);
            Scribe_Values.Look(ref Construction_LD, "MaxConstructionSpeedLowDanger", 400f);
            Scribe_Values.Look(ref PlantWork_LD, "MaxPlantWorkSpeedLowDanger", 400f);
            Scribe_Values.Look(ref Mining_LD, "MaxMiningSpeedLowDanger", 400f);
            Scribe_Values.Look(ref Rest_LD, "MaxRestRateMultiplierLowDanger", 400f);
            Scribe_Values.Look(ref Bed_LD, "MaxBedRestEffectivenessLowDanger", 400f);
            Scribe_Values.Look(ref Joy_LD, "MaxJoyGainFactorLowDanger", 400f);

            Scribe_Values.Look(ref Move_HD, "MaxMoveSpeedHighDanger", 30f);
            Scribe_Values.Look(ref Eating_HD, "MaxEatingSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref GeneralLabor_HD, "MaxGeneralLaborSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref Construction_HD, "MaxConstructionSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref PlantWork_HD, "MaxPlantWorkSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref Mining_HD, "MaxMiningSpeedHighDanger", 99999f);
            Scribe_Values.Look(ref Rest_HD, "MaxRestRateMultiplierHighDanger", 99999f);
            Scribe_Values.Look(ref Bed_HD, "MaxBedRestEffectivenessHighDanger", 99999f);
            Scribe_Values.Look(ref Joy_HD, "MaxJoyGainFactorHighDanger", 99999f);

            base.ExposeData();
        }

        public void DoWindowContents(Rect inRect)
        {

            // スクロール可能な外枠
            Rect outRect = inRect;
            // コンテンツの実際の高さ（十分大きめに取るか、動的計算してもOK）
            float contentHeight = 800f;
            Rect viewRect = new Rect(0f, 0f, inRect.width - 16f, contentHeight);

            Widgets.BeginScrollView(outRect, ref scrollPosition, viewRect);

            var gapHeight = 220f;   //from 150f
            var controlDistance = 8f;

            var listing_Standard = new Listing_Standard();
            var listing_Standard2 = new Listing_Standard();

            var list = new List<string>
            {
            "",
            "MaxMoveSpeed".Translate(),
            "MaxEatingSpeed".Translate(),
            "MaxGeneralLaborSpeed".Translate(),
            "MaxConstructionSpeed".Translate(),
            "MaxPlantWorkSpeed".Translate(),
            "MaxMiningSpeed".Translate(),
            "MaxRestRateMultiplier".Translate(),
            "MaxBedRestEffectiveness".Translate(),
            "MaxJoyGainFactor".Translate()
            };

            //Old code
            /*
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
                Translator.Translate("MaxMiningSpeed"),
                Translator.Translate("MaxRestRateMultiplier"),
                Translator.Translate("MaxBedRestEffectiveness"),
                Translator.Translate("MaxJoyGainFactor")
            };

            */

            var list2 = new List<string>
            {
                "",
                "[cell/s]",
                "[%]",
                "[%]",
                "[%]",
                "[%]",
                "[%]",
                "[%]",
                "[%]",
                "[%]"
            };

            listing_Standard.Begin(viewRect); //inRect
            listing_Standard.CheckboxLabeled("ApplyOnPlayerColonists".Translate(), ref ApplyOnPlayerColonists);
            listing_Standard.Gap(controlDistance);
            listing_Standard.CheckboxLabeled("ApplyOnPlayerNotColonists".Translate(), ref ApplyOnPlayerNotColonists);
            listing_Standard.Gap(controlDistance);
            listing_Standard.CheckboxLabeled("ApplyOnNotEnemyFactionPawns".Translate(), ref ApplyOnNotEnemyFactionPawns);
            listing_Standard.Gap(controlDistance);
            listing_Standard.CheckboxLabeled("ApplyOnEnemyFactionPawns".Translate(), ref ApplyOnEnemyFactionPawns);
            listing_Standard.Gap(controlDistance);
            listing_Standard.CheckboxLabeled("ApplyOnDraftedPawns".Translate(), ref ApplyOnDraftedPawns);
            listing_Standard.Gap(controlDistance);
            listing_Standard.CheckboxLabeled("TreatDraftedPawnsAsHighDanger".Translate(), ref TreatDraftedPawnsAsHighDanger);
            listing_Standard.Gap(controlDistance);
            listing_Standard.CheckboxLabeled("TreatNotPlayerColonyMapAsHighDanger".Translate(), ref TreatNotPlayerColonyMapAsHighDanger);
            listing_Standard.Gap(controlDistance);

            listing_Standard.Gap(350f);
            listing_Standard.Label("DisableNote".Translate());
            if (listing_Standard.ButtonTextLabeledPct("SLAP.ResetValues".Translate(), "Reset".Translate(), 0.5f))
            {
                //Reset();
                Find.WindowStack.Add(new Dialog_MessageBox(
                /* text                 */ "Are you sure you want to reset the target defName list to defaults?",
                /* buttonAText          */ "Reset".Translate(),
                /* buttonAAction        */ () => { Reset(); },
                /* buttonBText          */ "Cancel".Translate(),
                /* buttonBAction        */ null,  // Cancel 時の Action（不要なら null）
                /* title                */ null,
                /* buttonADestructive   */ true,  // Reset ボタンを赤文字に
                /* acceptAction         */ null,  // 追加の acceptAction（不要なら null）
                /* cancelAction         */ null,  // 追加の cancelAction（不要なら null）
                /* layer                */ WindowLayer.Dialog
                ));
            }

            //Old code
            /*
            listingStandard.Begin(inRect);
            listingStandard.CheckboxLabeled(Translator.Translate("ApplyOnPlayerColonists"), ref this.ApplyOnPlayerColonists);
            listingStandard.Gap(GapForLines);
            listingStandard.CheckboxLabeled(Translator.Translate("ApplyOnPlayerNotColonists"), ref this.ApplyOnPlayerNotColonists);
            listingStandard.Gap(GapForLines);
            listingStandard.CheckboxLabeled(Translator.Translate("ApplyOnNotEnemyFactionPawns"), ref this.ApplyOnNotEnemyFactionPawns);
            listingStandard.Gap(GapForLines);
            listingStandard.CheckboxLabeled(Translator.Translate("ApplyOnEnemyFactionPawns"), ref this.ApplyOnEnemyFactionPawns);
            listingStandard.Gap(GapForLines);

            listingStandard.Gap(350f);
            listingStandard.Label(Translator.Translate("DisableNote"), -1);
            listingStandard.End();

            listingSection1.Begin(inRect);
            */

            /* First Column = Labels */
            listing_Standard.End();
            listing_Standard2.Begin(viewRect); //inRect
            listing_Standard2.ColumnWidth = 180f;
            listing_Standard2.Gap(gapHeight);
            foreach (var item in list)
            {
                listing_Standard2.Label(item);
                listing_Standard2.Gap(controlDistance);
            }

            /*
            listingSection1.ColumnWidth = 180f;
            listingSection1.Gap(GapForSection1);
            foreach (string T in maxLabels)
            {
                listingSection1.Label(T);
                listingSection1.Gap(GapForLines);

            }
            */

            /* Second Column = Units */
            listing_Standard2.NewColumn();
            listing_Standard2.ColumnWidth = 60f;
            listing_Standard2.Gap(gapHeight);
            foreach (var item2 in list2)
            {
                listing_Standard2.Label(item2);
                listing_Standard2.Gap(controlDistance);
            }

            /*
            listingSection1.NewColumn();
            listingSection1.ColumnWidth = 60f;
            listingSection1.Gap(GapForSection1);
            foreach (string T in unitLabels)
            {
                listingSection1.Label(T);
                listingSection1.Gap(GapForLines);

            }
            */

            listing_Standard2.NewColumn();
            listing_Standard2.ColumnWidth = 180f;
            listing_Standard2.Gap(gapHeight);
            listing_Standard2.Label("Peace".Translate());
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Move_P, ref Move_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Eating_P, ref Eating_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref GeneralLabor_P, ref GeneralLabor_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Construction_P, ref Construction_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref PlantWork_P, ref PlantWork_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Mining_P, ref Mining_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Rest_P, ref Rest_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Bed_P, ref Bed_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Joy_P, ref Joy_B_P, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.NewColumn();
            listing_Standard2.Gap(gapHeight);
            listing_Standard2.Label("LowDanger".Translate());
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Move_LD, ref Move_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Eating_LD, ref Eating_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref GeneralLabor_LD, ref GeneralLabor_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Construction_LD, ref Construction_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref PlantWork_LD, ref PlantWork_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Mining_LD, ref Mining_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Rest_LD, ref Rest_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Bed_LD, ref Bed_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Joy_LD, ref Joy_B_LD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.NewColumn();
            listing_Standard2.Gap(gapHeight);
            listing_Standard2.Label("HighDanger".Translate());
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Move_HD, ref Move_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Eating_HD, ref Eating_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref GeneralLabor_HD, ref GeneralLabor_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Construction_HD, ref Construction_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref PlantWork_HD, ref PlantWork_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Mining_HD, ref Mining_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Rest_HD, ref Rest_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Bed_HD, ref Bed_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.TextFieldNumeric(ref Joy_HD, ref Joy_B_HD, 0.1f, 99999f);
            listing_Standard2.Gap(controlDistance);
            listing_Standard2.End();

            Widgets.EndScrollView();

            /*
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
            listingSection1.TextFieldNumeric<float>(ref this.Rest_P, ref this.Rest_B_P, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Bed_P, ref this.Bed_B_P, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Joy_P, ref this.Joy_B_P, 0.1f, 99999f);
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
            listingSection1.TextFieldNumeric<float>(ref this.Rest_LD, ref this.Rest_B_LD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Bed_LD, ref this.Bed_B_LD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Joy_LD, ref this.Joy_B_LD, 0.1f, 99999f);
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
            listingSection1.TextFieldNumeric<float>(ref this.Rest_HD, ref this.Rest_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Bed_HD, ref this.Bed_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);
            listingSection1.TextFieldNumeric<float>(ref this.Joy_HD, ref this.Joy_B_HD, 0.1f, 99999f);
            listingSection1.Gap(GapForLines);

            listingSection1.End();

            */
        }
    }
}
