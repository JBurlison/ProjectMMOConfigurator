namespace ProjectMMOConfigurator.Models
{
    public class Entity
    {
        public bool _override { get; set; } = true;
        public string[] isTagFor { get; set; } = [];

        /// <summary>
        /// See <see cref="EntityEventTypes"/> for keys of the first dictionary. Second dictionary is the skill and value is xp gain from pacing the block.
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> xp_values { get; set; } = [];

        /// <summary>
        /// See <see cref="EntityRequirement"/> for the keys of the first dictionary. the list is the nbt rules.
        /// </summary>
        public Dictionary<string, List<string>> nbt_xp_values { get; set; } = [];

        /// <summary>
        /// The first dictionary only has 2 valid keys DEAL_DAMAGE & RECEIVE_DAMAGE. the second dictionary is the damage type, the third is the skill &  XP gain
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
        /// See <see cref="EntityRequirement"/> for keys of the first dictionary. Dictionary of each Item requirement and the requied skill & level
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

        /// <summary>
        /// /// See <see cref="EntityRequirement"/> for the keys of the first dictionary. the list is the nbt rules.
        /// </summary>
        public Dictionary<string, List<string>> nbt_requirements { get; set; } = [];
    }


    public static class EntityEventTypes
    {
        public const string BREED = "BREED";
        public const string DEATH = "DEATH";
        public const string ENTITY = "ENTITY";
        public const string RIDING = "RIDING";
        public const string SHIELD_BLOCK = "SHIELD_BLOCK";
        public const string TAMING = "TAMING";
    }

    public static class EntityRequirement
    {
        public const string BREED = "BREED";
        public const string DEATH = "DEATH";
        public const string KILL = "KILL";
        public const string RIDE = "RIDE";
        public const string ENTITY_INTERACT = "ENTITY_INTERACT";
        public const string TAME = "TAME";
    }
}
