namespace ProjectMMOConfigurator.Models
{
    public class Entity
    {
        public bool _override { get; set; }
        public string[] isTagFor { get; set; } = [];
        public Dictionary<string, List<string>> xp_values { get; set; } = [];
        public Dictionary<string, List<string>> nbt_xp_values { get; set; } = [];

        /// <summary>
        /// <code>
        /// "damage_type_xp":{
        ///  "DEAL_DAMAGE": {
        ///    "minecraft:player_attack": { //gives the below XP for every point of damage dealt with melee attacks
        ///      "combat": 10  
        ///    },
        ///    "#minecraft:is_projectile": { //using a tag, we can give archery XP for every point of damage dealt by projectiles
        ///      "archery": 10
        ///    },
        ///    "magic_mod:spell_damage": { //if a mod adds their own damage type, you can use those here too.
        ///      "magic": 100,
        ///      "spellcasting": 50
        ///    }
        ///  },
        ///  "RECEIVE_DAMAGE": {} 
        /// }
        /// </code>
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, int>>> damage_type_xp { get; set; } = [];

        /// <summary>
        /// Dictionary of each Item requirement and the requied skill & level
        /// 
        /// <code>
        /// "requirements": {
        ///  "TOOL": {
        ///    "mining": 10,
        ///    "excavation": 5
        ///  },
        ///  "WEAPON": {
        ///    "combat": 10
        ///  }
        /// }
        /// </code>
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> requirements { get; set; } = [];
        public Dictionary<string, Dictionary<string, int>> nbt_requirements { get; set; } = [];
    }
}
