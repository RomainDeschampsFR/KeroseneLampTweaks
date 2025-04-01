using UnityEngine;
using MelonLoader;


namespace KeroseneLampTweaks
{
    class Main : MelonMod
    {
        
        public override void OnInitializeMelon()
        {
            Settings.OnLoad();
        }

        public static void ColorLamps(GameObject lamp)
        {
            
            if (Settings.settings.lampColor != LampColor.Default)
            {
                Color newColor;

                if (lamp.name.Contains("Spelunkers") && Settings.settings.spelunkerColor)
                {
                    newColor = GetNewColor(Settings.settings.spelunkersLampColor, true);
                }
                else
                {
                    newColor = GetNewColor(Settings.settings.lampColor);
                }

                foreach (Light light in lamp.GetComponentsInChildren<Light>())
                {
                    light.color = newColor;
                }

                foreach (Light light in lamp.GetComponents<Light>())
                {
                    light.color = newColor;
                }
            }
        }

        public static Color GetNewColor(LampColor lampColor, bool isSpelunkers = false)
        {
            Color newColor = new Color(0.993f, 0.670f, 0.369f, 1.000f);

            switch (lampColor)
            {
                case LampColor.Red:
                    newColor = new Color32(255, 105, 92, 255);
                    break;

                case LampColor.Yellow:
                    newColor = new Color32(255, 228, 92, 255);
                    break;

                case LampColor.Blue:
                    newColor = new Color32(92, 105, 255, 255);
                    break;

                case LampColor.Cyan:
                    newColor = new Color32(92, 225, 255, 255);
                    break;

                case LampColor.Green:
                    newColor = new Color32(91, 216, 95, 255);
                    break;

                case LampColor.Purple:
                    newColor = new Color32(208, 91, 216, 255);
                    break;

                case LampColor.White:
                    newColor = new Color32(255, 255, 255, 255);
                    break;

                case LampColor.Custom:
                    if (!isSpelunkers)
                    {
                        newColor = new Color32((byte)Settings.settings.lampColorR, (byte)Settings.settings.lampColorG, (byte)Settings.settings.lampColorB, 255);
                    }
                    else
                    {
                        newColor = new Color32((byte)Settings.settings.spelunkersLampColorR, (byte)Settings.settings.spelunkersLampColorG, (byte)Settings.settings.spelunkersLampColorB, 255);
                    }
                    break;
            }

            return newColor;
        }
    }
}
