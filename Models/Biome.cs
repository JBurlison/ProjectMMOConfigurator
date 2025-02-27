namespace ProjectMMOConfigurator.Models
{
    public class Biome
    {
        public List<string> isTagFor { get; set; } = [];
        public required Global_Mob_Modifier[] global_mob_modifier { get; set; }

        /// <summary>
        /// Dictionary of mob modifiers for each mob type
        /// Key is the mob type, value is a dictionary of attribute modifiers
        /// Note: ge
        /// https://minecraft.wiki/w/Attribute for a list of attributes
        /// https://github.com/Shadows-of-Fire/Apothic-Attributes/blob/1.20/src/main/java/dev/shadowsoffire/attributeslib/api/AttributeHelper.java for a list of attributes
        /// <code>
        ///           "minecraft:zombie": {
        ///            "minecraft:generic.max_health": 0.5, //half health
        ///            "minecraft:generic.movement_speed": 2.0, //double speed
        ///            "minecraft:generic.attack_damage": 1.1 // 10% increase in damage
        ///         },
        ///          "minecraft:skeleton": {
        ///            "minecraft:generic.attack_damage": 1.15  //not all attributes need to have values, only what you want to modify.
        ///          }
        /// </code>
        /// </summary>
        public Dictionary<string, Dictionary<string, float>> mob_modifier { get; set; } = [];
        public List<string> vein_blacklist { get; set; } = [];
        public bool Override { get; set; }
        public BiomeBonus bonus { get; set; } = new BiomeBonus();
    }

    public class BiomeBonus
    {
        public Dictionary<string, float> Biome { get; set; } = [];
    }
}
