namespace ProjectMMOConfigurator.Models
{
    public class Item
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
        public Dictionary<string, Dictionary<string, int>> bonuses { get; set; } = [];
        public Dictionary<string, Dictionary<string, int>> nbt_bonuses { get; set; } = [];
        public Dictionary<string, int> negative_effect { get; set; } = [];

        /// <summary>
        /// Key is a minecraft item.
        /// </summary>
        public Dictionary<string, Salvage> salvage { get; set; } = [];
        public Vein_Data vein_data { get; set; } = new Vein_Data();
    }

    public class Salvage
    {
        public int salvageMax { get; set; }
        public float baseChance { get; set; }
        public float maxChance { get; set; }

        /// <summary>
        ///     Key is a skill.
        ///<code>
        ///"crafting": 0.005,
        ///"smithing": 0.005
        ///</code>
        /// </summary>
        public Dictionary<string, float> chancePerLevel { get; set; } = [];

        /// <summary>
        ///    Key is a skill.
        /// </summary>
        public Dictionary<string, int> levelReq { get; set; } = [];

        /// <summary>
        ///   Key is a skill.
        /// </summary>
        public Dictionary<string, int> xpPerItem { get; set; } = [];
    }

    public class Xpperitem
    {
        public int crafting { get; set; }
        public int smithing { get; set; }
    }

    public class Vein_Data
    {
        public int chargeCap { get; set; }
        public float chargeRate { get; set; }
    }
}
