namespace ProjectMMOConfigurator.Models
{
    public class Blocks
    {
        public bool _override { get; set; } = true;
        public string[] isTagFor { get; set; } = [];

        /// <summary>
        /// See <see cref="ItemEventTypes"/> for keys of the first dictionary. Second dictionary is the skill and value is xp gain from pacing the block.
        /// </summary>
        public Dictionary<string, Dictionary<string, int>> xp_values { get; set; } = [];

        /// <summary>
        /// See <see cref="ItemEventTypes"/> for the keys of the first dictionary. the list is the nbt rules.
        /// </summary>
        public Dictionary<string, List<string>> nbt_xp_values { get; set; } = [];
        /// <summary>
        /// See <see cref="ItemRequirement"/> for the keys of the first dictionary. Dictionary of each Item requirement and the requied skill & level
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
        public Vein_Data vein_data { get; set; } = new Vein_Data();
    }
}
