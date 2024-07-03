# RJW-Genes [NSFW]

This mod adds genes related and based on RJW to Rimworld. 

## Current Features 

- Different Genitalia Types 
- Genitalia Size Scaling 
- Extra Genitalia, Male Pregnancy, Futas, Femboys
- Most RJW Traits
- Cum-Amount Changes, Transfer Nutrition Boosts
- Sexual Vampires that need Cum, Cocks or some other sources.
- Mech Breeding Additions & Orgasmic Mytosis
- Human-Animal Gene Inheritance merged from [Shabakur](https://github.com/Shabakur/RJW_Animal_Gene_Inheritance)
- Genetic Diseases that spread on Intercourse
- Patches for some popular / common Xenotypes from other Mods.

*You might not see all of them. Many genes just show up if other mods are loaded*.

**Conflicts:**
1. Should not be used with the original RJW_Animal_Gene_Inheritance anymore. 
2. There was an issue with other "Male-Only / Female-Only" Mods --- for which we provide our own Genes now. 
3. CAI5000 will not crash, but will make *Seduce*-Ability fail. I think same goes for Combat Extended.
4. Alpha Genes "Female / Male Only" Genes might overwrite later Genitalia-Changes and should be avoided in combination with RJW-Genes features.
5. rjw.sexperience.cumgenes removes fertilin-gain from Cum item - I hope I addressed this by adding a load order but keep me posted (Issue #41)
6. [Consistent Gene Inheritance](https://steamcommunity.com/sharedfiles/filedetails/?id=2881479142&searchtext=Consistent+Gene+Inheritance) alters inheritance - it messes a bit with the Insect-Caste Logic. Your game will not crash, but the insect xenotypes will be a bit messed up. 

## Structure

- Toplevel: By Function (i.E. "Genes", "Animal Inheritance", "Utility")
- Then: By Domain, following the Gene-Categories ("Cosmetic","Special", "Damage",...)
- Last: By Type of Action (Def, Patch, etc.)

So if you want to change / add a gene about shrinking cocks you were to go: `Genes -> GenitaliaSize -> Defs`. 