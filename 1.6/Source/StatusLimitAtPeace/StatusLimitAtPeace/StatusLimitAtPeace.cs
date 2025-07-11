using Verse;
using UnityEngine;

namespace StatusLimitAtPeace
{


    public class StatusLimitAtPeace : Mod
    {
        public static StatusLimitAtPeace instance;
        public readonly Settings SettingsObject;

        public StatusLimitAtPeace(ModContentPack content) : base(content)
        {
            instance = this;
            SettingsObject = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            GetSettings<Settings>().DoWindowContents(inRect);
        }

        public override string SettingsCategory()
        {
            return "Status Limit at Peace";
        }


    }


}


