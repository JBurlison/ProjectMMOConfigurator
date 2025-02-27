namespace ProjectMMOConfigurator.Models
{
    public class Biome
    {
        public List<string> isTagFor { get; set; } = [];
        public required Global_Mob_Modifier[] global_mob_modifier { get; set; }

        /// <summary>
        /// Dictionary of mob modifiers for each mob type
        /// Key is the mob type, value is a dictionary of attribute modifiers
        /// Attrbuts can be found in attributes.cs. NOTE that the min and max is for information purposes only, the UI does not enforce it.
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

        /// <summary>
        /// a list of ore ids to ignore when vein mining
        /// </summary>
        public List<string> vein_blacklist { get; set; } = [];
        public bool _override { get; set; } = true;
        public BiomeBonus bonus { get; set; } = new BiomeBonus();
    }

    public class BiomeBonus
    {
        /// <summary>
        /// dictionary of skill and bonus experience gain from being in the biome
        /// </summary>
        public Dictionary<string, float> Biome { get; set; } = [];
    }
}
