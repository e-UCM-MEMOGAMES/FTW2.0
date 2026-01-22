public static class Defs
{
    /// <summary>
    /// Nombre de la escena del menu principal
    /// </summary>
    public static string
    MENU_SCENE_NAME = "Start",
    /// <summary>
    /// Nombre de la escena de configuracion
    /// </summary>
    SETTINGS_SCENE_NAME = "Settings",
    /// <summary>
    /// Nombre de la escena de creditos
    /// </summary>
    CREDITS_SCENE_NAME = "Credits",
    /// <summary>
    /// Nombre de la escena de opciones del nivel
    /// </summary>
    LEVEL_SETTINGS_SCENE_NAME = "LevelSelector",

    /// <summary>
    /// Nombre de la escena del tutorial
    /// </summary>
    TUTORIAL_SCENE_NAME = "Tutorial",


    /// Id de la configuracion para el volumen de la musica de fondo en las preferencias
    /// </summary>
    BGM_VOLUME_PREFS_KEY = "bgmVolume",
    /// <summary>
    /// Id de la configuracion para el volumen de los efectos de sonido en las preferencias
    /// </summary>
    SFX_VOLUME_PREFS_KEY = "sfxVolume",
    /// <summary>
    /// Id de la configuracion para el idioma en las preferencias
    /// </summary>
    LANGUAGE_PREFS_KEY = "language",
    /// <summary>
    /// Id de la configuracion para la informacion de cada nivel 
    /// (la id empieza con este string y cambia dependiendo del nivel o el mapa)
    /// </summary>
    LEVEL_NAME_PREFS_KEY = "Level",
    /// <summary>
    /// Id de la configuracion para la informacion de cada mapa de cada nivel 
    /// </summary>
    MAP_NAME_PREFS_KEY = "Map";

    /// <summary>
    /// Devuelve la Id de la configuracion para el nivel indicado en el clima indicado
    /// </summary>
    public static string GetLevelKey(int levelNumber, int mapNumber)
    {
        return $"{LEVEL_NAME_PREFS_KEY}_{levelNumber.ToString()}_{MAP_NAME_PREFS_KEY}_{mapNumber.ToString()}";
    }
}