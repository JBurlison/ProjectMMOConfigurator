namespace ProjectMMOConfigurator.Models
{
    public class Skill
    {
        public int maxLevel { get; set; } = 2147483647;
        public required string icon { get; set; }
        public int iconSize { get; set; } = 32;
        public bool useTotalLevels { get; set; }
        public int color { get; set; }
        public bool noAfkPenalty { get; set; }
        public bool displayGroupName { get; set; }
        public bool showInList { get; set; } = true;
        public Dictionary<string, float> groupfor { get; set; } = [];

    }

    public class SkillsConfig
    {
        public string type { get; set; } = "SKILLS";
        public Dictionary<string, Skill> skills { get; set; } = [];
    }
}
