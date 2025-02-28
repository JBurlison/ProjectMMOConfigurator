I am working on a UI to edit json files for the minecraft mod project MMO.

The application is going to be responsible for editing 10's of thousands of json files.

https://moddedmc.wiki/en/project/pmmo/docs/configuration/config_home

I have included a demo_data folder with some examples and the Models folder has models for the major types that we are starting with (Biome, Blocks, Dimension, Entity & Item). the parent folder name of the json determines what the model is. so if the Parent folder is blocks, then the model for each json is Blocks. If no model exists, ignore for now. 

A lot of these require skills to be leveled up or gain xp. So there is a skill level requirement, Those skills are kept in a parent folder called config. The SkillsConfig model in skill.cs is the model for the skills json file.

there are some overall goals with this UI.

The ability to edit any individual file and save it.
select a folder and traverse it and load all json from it.
provide a way of finding all models that have a skill.
Mass editing specifc models to add skill requirements to say xp_values.
view all the files loaded by their model type.
anything in the config directory should be considered special case as they are config files. for now we are only supporting Skills.json The application should have the ability to edit this file also. Its location may change based on where the user keeps their json data but it will always be in a folder named config.

