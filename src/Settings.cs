using ModSettings;
using System.Reflection;


namespace KeroseneLampTweaks
{
    public enum LampColor
    {
        Default, Red, Yellow, Blue, Cyan, Green, Purple, White, Custom
    }

    internal class KeroseneLampTweaksSettings : JsonModSettings
    {
        // DECAY SECTION
        [Section("Decay")]
        [Name("Decay when turning it on")]
        [Description("Decay when you turn on lanterns. \nVanilla is 0")]
        [Slider(0f, 2f, 21, NumberFormat = "{0:0.00}%")]
        public float turnOnDecay = 0f;

        [Name("Decay over time while on (per hour)")]
        [Description("Decay over time when lanterns are ON. \nVanilla is 0")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:0.00}%")]
        public float overTimeDecay = 0f;

        // BURN RATE SECTION
        [Section("Global Burn Rate")]
        [Name("Rate of burn for placed lamps")]
        [Description("At what rate the fuel of a lamp will be consumed when placed. 1 is default (4 hours), 0 makes lamps not consume fuel when placed, and 2 doubles the consumption.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00}")]
        public float placed_burn_multiplier = 1f;

        [Name("Rate of burn for held lamps")]
        [Description("At what rate the fuel of a lamp will be consumed when held. 1 is default (4 hours), 0 makes lamps not consume fuel when equipped, and 2 doubles the consumption.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00}")]
        public float held_burn_multiplier = 1f;

        [Section("Burn Rate penalty")]
        [Name("Condition threshold")]
        [Description("There won't be any fuel consumption penalty for a lantern above this condition threshold. " +
            "\n 0% : Penalty will never be applied" +
            "\n 50% : No fuel consumption penalty will be applied with lanterns between 50% and 100% condition" +
            "\n 80% : No fuel consumption penalty will be applied with lanterns between 80% and 100% condition")]
        [Slider(0, 100, 101, NumberFormat = "{0:0}%")]
        public int conditionThreshold = 80;

        [Name("Max penalty")]
        [Description("Maximum fuel consumption penalty when lantern condition is (close to) 0%." +
            "\n The penalty increases linearly between the condition threshold and 0% condition." +
            "\n 20% : consumption penalty will be 0 close to threshold, 20% higher close to 0% condition and proportional in between" +
            "\n 100% : consumption penalty will be 0 close to threshold, doubled close to 0% condition and proportional in between")]
        [Slider(0, 100, 101, NumberFormat = "{0:0}%")]
        public int maxPenalty = 100;

        // LIGHT SETTINGS SECTION
        [Section("Light Settings")]
        [Name("Lamp Light Range Modifier")]
        [Description("How far the light from lamps is cast. e.g. 1 is default (20m outside, 25m inside), 0 makes lamps not cast light, and 2 doubles the distance.")]
        [Slider(0f, 2f, 201, NumberFormat = "{0:0.00}")]
        public float lamp_range = 1f;

        [Name("Lamp Light Color")]
        [Description("Color for the lamp light.")]
        [Choice("Default (Orange)", "Red", "Yellow", "Blue", "Cyan", "Green", "Purple", "White", "Custom")]
        public LampColor lampColor = LampColor.Default;

        [Name("Lamp Color Red")]
        [Slider(0, 255)]
        public int lampColorR = 0;

        [Name("Lamp Color Green")]
        [Slider(0, 255)]
        public int lampColorG = 0;

        [Name("Lamp Color Blue")]
        [Slider(0, 255)]
        public int lampColorB = 0;

        [Name("Different Color for Spelunkers Lamp")]
        [Description("Turn this on to color spelunker's lamp a different color.")]
        public bool spelunkerColor = false;

        [Name("Spelunkers Lamp Light Color")]
        [Description("Color for the Spelunkers lamp light.")]
        [Choice("Default (Orange)", "Red", "Yellow", "Blue", "Cyan", "Green", "Purple", "White", "Custom")]
        public LampColor spelunkersLampColor = LampColor.Default;

        [Name("Spelunkers Lamp Color Red")]
        [Slider(0, 255)]
        public int spelunkersLampColorR = 0;

        [Name("Spelunkers Lamp Color Green")]
        [Slider(0, 255)]
        public int spelunkersLampColorG = 0;

        [Name("Spelunkers Lamp Color Blue")]
        [Slider(0, 255)]
        public int spelunkersLampColorB = 0;

        // MISC SECTION
        [Section("Misc")]
        [Name("Mute lamps audio")]
        [Description("This enables lamps to be silent when turned on and placed.")]
        public bool muteLamps = false;

        protected override void OnChange(FieldInfo field, object oldVal, object newVal)
        {
            RefreshFields();
        }

        internal void RefreshFields()
        {
            if (lampColor == LampColor.Custom)
            {
                SetFieldVisible(nameof(lampColorR), true);
                SetFieldVisible(nameof(lampColorG), true);
                SetFieldVisible(nameof(lampColorB), true);
            }
            else
            {
                SetFieldVisible(nameof(lampColorR), false);
                SetFieldVisible(nameof(lampColorG), false);
                SetFieldVisible(nameof(lampColorB), false);
            }

            SetFieldVisible(nameof(spelunkersLampColor), spelunkerColor);

            if (spelunkersLampColor == LampColor.Custom)
            {
                SetFieldVisible(nameof(spelunkersLampColorR), true);
                SetFieldVisible(nameof(spelunkersLampColorG), true);
                SetFieldVisible(nameof(spelunkersLampColorB), true);
            }
            else
            {
                SetFieldVisible(nameof(spelunkersLampColorR), false);
                SetFieldVisible(nameof(spelunkersLampColorG), false);
                SetFieldVisible(nameof(spelunkersLampColorB), false);
            }
        }
    }

    internal static class Settings
    {
        public static KeroseneLampTweaksSettings settings = new();

        public static void OnLoad()
        {
            settings.RefreshFields();
            settings.AddToModSettings("Kerosene Lamps Tweaks");
        }
    }
}
