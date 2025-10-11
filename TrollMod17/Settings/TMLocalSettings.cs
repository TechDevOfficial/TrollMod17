using MiraAPI.LocalSettings.Attributes;

namespace TrollMod17.Settings;

public enum ButtonPos
{
    Left,
    Right
}

public static class TMLocalSettings
{
    [LocalEnumSetting("Buttons Position")]
    public static ButtonPos ButtonsPosition { get; set; } = ButtonPos.Right;
}
