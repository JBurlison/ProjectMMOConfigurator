namespace ProjectMMOConfigurator.Models
{
    public class Blocks
    {
        public bool _override { get; set; }
        public string[] isTagFor { get; set; } = [];
        public Dictionary<string, Dictionary<string, int>> xp_values { get; set; } = [];
        public Dictionary<string, List<string>> nbt_xp_values { get; set; } = [];
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
        public Dictionary<string, List<string>> nbt_requirements { get; set; } = [];
        public Vein_Data vein_data { get; set; } = new Vein_Data();
    }
}
