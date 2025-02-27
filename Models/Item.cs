namespace ProjectMMOConfigurator.Models
{
    public class Item
    {
        public bool _override { get; set; } = true;
        public string[] isTagFor { get; set; } = [];
        /// <summary>
        /// See <see cref="ItemEventTypes"/> for keys of the first dictionary. Second dictionary is the skill and value is xp gain
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> xp_values { get; set; } = [];

        /// <summary>
        /// See <see cref="ItemEventTypes"/> for the keys of the first dictionary. the list is the nbt rules.
        /// </summary>
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
        /// See <see cref="ItemRequirement"/> for first dictionary. Dictionary of each Item requirement and the requied skill & level
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
        /// /// See <see cref="ItemRequirement"/> for the keys of the first dictionary. the list is the nbt rules.
        /// </summary>
        public Dictionary<string, List<string>> nbt_requirements { get; set; } = [];

        /// <summary>
        /// The first dictionary only has 2 valid keys HEALD & WORN. the second dictionary is the skill & level
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> bonuses { get; set; } = [];

        /// <summary>
        /// The first dictionary only has 2 valid keys HEALD & WORN. the list is the nbt rules.
        /// </summary>
        public Dictionary<string, List<string>> nbt_bonuses { get; set; } = [];

        /// <summary>
        /// key is a effect such as minecraft:slowness or minecraft:mining_fatigue, value is the level of the effect.
        /// </summary>
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

    public static class ItemEventTypes
    {
        public const string ANVIL_REPAIR = "ANVIL_REPAIR";
        public const string BLOCK_BREAK = "BLOCK_BREAK";
        public const string BLOCK_PLACE = "BLOCK_PLACE";
        public const string BREW = "BREW";
        public const string CONSUME = "CONSUME";
        public const string CRAFT = "CRAFT";
        public const string TOOL_BREAKING = "TOOL_BREAKING";
        public const string ENCHANT = "ENCHANT";
        public const string FISH = "FISH";
        public const string SMELT = "SMELT";
        public const string GIVEN_AS_TRADE = "GIVEN_AS_TRADE";
        public const string ACTIVATE_ITEM = "ACTIVATE_ITEM";
        public const string RECEIVED_AS_TRADE = "RECEIVED_AS_TRADE";
    }

    public static class ItemRequirement
    {
        public const string WEAR = "WEAR";
        public const string TOOL = "TOOL";
        public const string WEAPON = "WEAPON";
        public const string USE = "USE";
        public const string PLACE = "PLACE";
        public const string BREAK = "BREAK";
        public const string INTERACT = "INTERACT";
    }
}
