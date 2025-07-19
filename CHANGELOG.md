# 2.5.2 (19-07-2025)
(Commit by @Telanda-DDS)

**Fixes**
- Fixed "Big and Small - Heaven and Hell" XML patch being applied incorrectly, again.
- Fixed issue that was causing Extra genitals,Anus,breasts being added to pawns when a gene was applied.
- Removed some code duplication in the Gender specific Genes, I cannot see any situations where this code was required, but but there are some notes in other genes relating to character editor, I'm leaving other genes As they are for now, and will wait to see if it breaks some strange edge case i didn't test for.


# 2.5.1 (17-07-2025)
(Commit by @Telanda-DDS)
  
**Changes**
- RJW-Genes no longer requires Cumpilation, it is now an optional mod.
  
  
**Fixes**
- Fixed Big & Small Genes XML Patch Error.
- Removed references to Obsolete RJW Methods.
  
  
**Known Issues**
- Succubus Tail interactions disabled pending rewrite due to updates to RJW Interactions system.
- Quirks disabled due to migration of RJW quirks to it's own sub-mod that has not yet been updated to 1.6.

---

# 2.5 (16-07-2025)
(Commit by @Telanda-DDS)
**Fixes**
- Added Rimworld 1.6 Branch and support - Initial Dev Test.
  
  
**Known Issues**
- Succubus Tail interactions disabled pending rewrite due to updates to RJW Interactions system.
- Quirks disabled due to migration of RJW quirks to it's own sub-mod that has not yet been updated to 1.6
  
  

---

# 2.4.2 (25-11-2024)

**Fixes**:

- Removed Mention of Hugslib Dependency (Thanks @DukeEltoro)
- Re-Added Pig & Orc Genitalia, to Fix WorldGen with SpawnRJW-Genes (Thanks @DukeElToro)
- Change to MechBirth Transpiler (Thanks @DukeElToro & @Jikulopo)
- Changes to be compatible with RJW 5.6 (Thanks @DukeElToro)

**Non-Fix**:

- EverGrowth will still only max out. This would be a good first issue, for all you naughty wannabe modders!

# 2.4.1 (22-10-2024)

**Additions**:

- Added standard patches for Obsidia Arachnas, Axotl, Ceraton, Mothoids, Tortle, Slimes Xenotypes (#188, Thanks @casualArtifice)
- Added standard patches for Rimsenal Harana, Urred & Askbarn Xenotypes (#188, Thanks @casualArtifice)
- Re-Added standard patches for Roos Faun & Minotaur (#188, Thanks @casualArtifice)
- Issue and Feature Request Templates for Github

**Fixes**:

- Put Living Cumbucket code properly behind a dependency check (#190)
- Fixed some Xenotype Patches to not need Licentia for Generous Donor (#188, Thanks @casualArtifice)
- Fixed GeneDef Typo in Heaven-And-Hell LiL Glutton elasticity (#122)

**Other**:

- Nazeeeem made a gene [here](https://gitgud.io/nazeeeem/nutritious-cum) that increases nutrition from cum, and reduces nutritions from anything else (effectifely closes #101)

# 2.4.0 (17-10-2024)

**Changes**:

- Made Littered Birth Letters Translatable (#178)
- Likes Cumflation now works for all Cumflations and Stuffings from [Cumpilation](https://github.com/vegapnk/Cumpilation). It only removes the "physical" parts of the debuffs (pain, moving), but not e.g. food need reduction. It is now called `rjw_genes_inflatable`.  
- Likes Cumflation might not work for Licentia anymore. Maybe it does, tried my best. 
- Blocked Cumflation moved from Licentia Labs to Cumflation Hediffs. It's now called Un-Inflatable (`rjw_genes_un_inflatable`). 
- Generous Donor now does not need Licentia anymore. 
- Orgasmic Mytosis does not copy pregnancies anymore (#184). This should make things a bit more stable and protect against errors. 
- Eating 10 Cumpilation Cum should give 2 Fertilin for CumEater Pawns.

**Fixes**: 

- Fixed two patches using the outdated LitteredBirth DefNames (#181)
- Fertilin is looking for the `Cumpilation_Cum` instead of Sexperiences `GatheredCum` (#176,#185)
- Hardened Orgasmic Mytosis against LoveThrall (#130)
- Added some more checks for Aphrodisiac Pheromones (#183)
- Changed description of LifeForce Empath (#175 thanks @elatedCentipede)
- Ovary Agitator, Limbyc Stimulator and Bioscaffold can be removed with surgery (#186). Scramblers cannot be removed. 

**Other**:

- I decided to (for now) not lock Fertilin Gain behind Cum as Fluid Type. So you will get some from InsectSpunk, etc. I hope that it does not get out of hand, but I don't want to introduce a punishing mechanic. 
- Evergrowing Genitalia now uses the BodySizeOverride of Comps. But: Still not fully correctly displayed :/ (#161)

**To Test**: 

- Since the RJW 5.5 Update, the fluid amount has changed. With Cumpilation, I also changed how much fluid becomes one item etc. so right now, Succubi might be a bit too easy or a bit too hard. To fix or change it, I need some feedback that use these in their "normal" playthroughs. 

# 2.3.2 (19-09-2024)

**Changes**:

- Lots of wording-changes provided by @myphicbowser (Thanks!)

**Fixes**:

- Fixed Miss-Firing of GatheredCum Patch after `RJW-Sexperience 1.5.1.5` update (#172, thanks @Ryufas)

**Additions**:

- Simple patch for `Cumpilation_Cum` to give Fertilin too (#172)

# 2.3.1 (13-09-2024)

**Fixes**:

- Asexual Gene (`rjw_genes_no_sex_need`) now gives the `asexual` trait instead of removing Sex Need (#164). Every 3k ticks (every half hour) it sets SexNeed to 60%. 
- Same changes as Asexual Gene for the Android version `rjw_genes_SexDisabled` (#137)
- Fixed some references to genes that changed names in patches. 
- Some hardening against Error on WorldGen (#159)

**Removal**:

- Removed Xenotype Patches for Roos Xenotypes - they were not ported to 1.5 (by Roos, atleast.)

**Known Issues**: 

- There can be an issue with World-Gen, if you use Spawn-RJW-Genes. I hope they address it soon. (#159)

# 2.3.0 (07-09-2024)

**Summary**:

- Migration to RJW 5.5.0, big thanks to @prototype99 (#146)

**Additions**:

- Genes that change Fluid-Types: Milk-Penis, Cum-Breasts & Insect-Spunk. These only overwrite existing Fluid, so if you have a Ovipositor pawn it will make InsectSpunk anyway.
- Gene: Electric Satisfaction. Charges nearby powernets on orgasm. 
- Gene: Genetic Disease Carrier. Allows to spread diseases for a week while being immune against them. Chances follow normal infection. (#135)

**Changes**:

- Migration to RJW 5.5.0, big thanks to @prototype99 (#146)
- Removed some Genitalia and Genes that substituted Race-Support. Golem, Pig, Ghost, Marine and Orc Genitalia are gone (for now), but an enthusiast can surely bring it back.
- `rjw_genes_evergrowth` is not exclusive with normal genitalia-sizes anymore. 
- "Very much Cum" Gene -> "Very much Fluid" Gene. Works for all primary genitalia of both genders now.  
- Introduced a max-severity of 75.0 for Living Cumbucket (#153)
- Genitalia-Size Genes are now XML-Configurable
- Genitalia-Size genes are now slightly more pronounced - e.g. large male genitalia go from (0.6 -> 1.01) instead of (0.5 -> 1.0). People were a bit unhappy with seeing "average" penises on their large male genitalia pawns.
- Due to the pending migration, `rjw_genes_evergrowth` and `rjw_genes_hormonal_saliva` do not produce *oversized* genitalia anymore - they just max out at the moment. I will work on getting this back. 

**Fixes**:

- Reverted the `Missing Body Part` from (#133). Causes issues at World Generation with some other Mods (#157,#159).

**Internal**:

- `GenitaliaFluidChangeExtension` and Extensible Genes that change FluidTypes.
- Renamed GeneDef from `LitteredBirth` to `rjw_genes_littered_birth` for consistency
- `BoundedExtension` to provide Upper and Lower Bounds for GenitaliaSizes. 
- Fixed a tiny yellow startup-warning by deleting a duplicate file. 

*Beta-1* was put up on 03-09-2024.

Since then the Disease Carrier was added and a typo was fixed. 

# 2.2.1 (23-08-2024)

**Changes**:

- Support for `1.5`, `1.4`, ... Folder-Structure
- Salvaged the `1.4` Folder from `RJW-Genes 1.3.3`. This will be a *frozen* version. 
- Biotechs Highmates do not get patched Hypersexual anymore. This was wished because Hypersexual is rather heavy. 
- "No-XXX"-Genes add a `Missing Body Part` Hediff. This was wanted to work better with OTY sized apparel (#133)
- VGE Hybridization now has a chance to be hybridized. If not met, it will continue with normal bestiality behaviour. 
- VGE Hybridization can not produce Humans from `DogMan + Dog` anymore, unfortunately. 

**Fixes**:

- Fixed the usage of "Disabled" VGE Hybridization (#116)
- Changed Version number in manifest.xml (#132)
- Reduced Log-Level for Likes-Cumflation Debug Messages (#131)
- Fixed the Localization String for Animal Hybrids (#144) ("Baseliner Bug" still persists, thats all RW not me)
- Added another null-check for "Offset Lifeforce" (#120,#143)
- Added a check for LifeForce-Empath having an active life-force gene (#143), which likely threw errors being active despite children not having life-force yet.
- Added a map-check for LifeForce Empaths. Won't work off-map. (#120)
- Added another Patch for `Blocked Masturbation` that disables the ThinkNode like Sexperience does (#127,#147). Thanks @TinyMechanoid333 !
- Fixed males spawning with small-female breasts (#138) by adding a detailed check `should have breasts`
- Added a `MayRequire="rjw.sexperience"` to the Genies Sex-Curiosity, which was a hidden dependency on sexperience (#136)
- Updated some Labels and References in patches (#149), thanks @flock-of-birds
- Removed the Filth-Production for Living-Cum-Bucket (#129). Hope to re-introduce it later but to remove the nullpointers some people get for now. 

**Internal**:

- Way more debug-logs for Animal Pregnancies.
- Removed seperate debug-logging for Animal_BGS - Just uses the normal setting from RJW-Genes.
- Merged the behaviour of Twinkifier and Feminizer into one `Patch_Aftersex_ApplyProgressingGeneticTransformations`. This allows for a XML-Only approach to defining more genes like this. Thanks @ArcherSaiter (#145,#150)
- Moved all DefOfs into it's own source-folder. 

**See Also**: 

- [Wiki Page on Hybridization](https://github.com/vegapnk/RJW-Genes/wiki/Vanilla-Genetics-Expanded-Hybridization)

# 2.2.0 (23-07-2024)

## Explanations

**Genetic Diseases**:

This update introduces genetic diseases that are shared on sex. 
Infection is handled when sex finishes, so a coitus-interruptus will not result in infections. 

Infections can be 

- Turned off entirely 
- Spread only on penetrative sex
- Chances are adjustable per XML per Gene

By turning their spread off, you effectively have a set of normal negative genes. 
Dead pawns can spread diseases, but cannot receive them. For all you necros out there. 

*Why???* 

Most of the genes so far were positive or neutral, 
so I got some fair requests to introduce negative genes to keep xenotypes balanced. 
I know that this is some overlap with the STD mod, but well ... you are free to turn things off? 

In theory, you can specify and gene of any kind to be spreadable by sex, not only ones written by this mod. 

**Genetic Infectors**:

These Genes can apply a genetic disease, but are not genetic diseases themselves. 
A single infector gene can have multiple resulting diseases, see this extension example: 

```xml
<li Class="RJW_Genes.GeneticInfectorExtension">
    <infectionChance>0.05</infectionChance>
    <infectionGenes>
        <li>rjw_genes_size_blinded</li>
        <li>rjw_genes_infectious_bisexuality</li>
    </infectionGenes>
</li>
```

The infection-chance is applied per gene - for the example above there would be a 5% chance to apply `size_blinded` and 5% chance to apply `infectious_bisexuality`.
Multiple infections can happen on a single sex. 
The `infectionGenes` can be any gene, this is not limited to genetic diseases (e.g. ugly or something). 

*Infectors* are always applied even if the genetic disease spread is turned off. 
The created genetic diseases will follow the logic outlined above. 

**Disease Immunity**: 

Pawns can be immune to genetic diseases. 

This is either done with a specialised gene (`rjw_genes_genetic_disease_immunity`)
or by genes giving specific immunities. 

Any gene can give immunity against any genetic disease using an extension: 

```xml
<li Class="RJW_Genes.ImmunityAgainstGenesExtension">
    <givesImmunityAgainst>
        <li>rjw_genes_size_blinded</li>
    </givesImmunityAgainst>
</li>
```

These extensions can be slapped on any gene, 
but they are meant mostly to have infectors immune against their own diseases. 

**Twinkification / Feminization**:

Both approaches follow the same general logic. 

- Pawn `A` has Twinkifier Gene and fucks Pawn `B`
- `B` receives a `twinkification progress` hediff with some effects
- Upon having ANY sex, `B` can gain genes from a relevant genepool. 
- Genes can be minor or major, depending on the progress of twinkification. 

Pawn `B` might be immune against twinkifier as normal immunity logic against diseases. 
But once the hediff is there, the twinkification can happen unless you wait for it to cool down.
Gene additions are subject to an application chance (25% for minor, 10% for major)
and might try to add a gene that already exists - then nothing happens. 

*Twink Genepool*: 
- (Minor) Body_Thin
- (Minor) Homosexual
- (Minor) Beard_NoBeardOnly
- (Minor) Small male genitalia
- (Major) Minor Vulnerability
- (Major) Infectious Homosexuality
- (Major) Delicate
- (Major) Beauty Pretty
- (Major) Fertile Anus

*Feminization Genepool*:
- (Minor) Long Hair
- (Minor) No-Beard
- (Minor) Small Male Genitals
- (Minor) No Cum
- (Minor) Big Breasts (will only show later)
- (Major) Female Only
- (Major) No Penis
- (Major) Minor Vulnerability

You can configure all genes, as well as their application chance, in the Genes` XML. 

*Why are these changes Genetic?* 
Because this is the genes mod, and I find things here quite robust.

## Changelog

**Additions:** 

- Gene: Genetic Disease Immunity. cannot get infected by any genetic diseases, and won't be affected by some other genes (see relevant genes)
- Disease Gene: Vulnerability. Pawn is likelier to be raped 
- Disease Gene: Infectious Hypersexuality
- Disease Gene: Infectious Homosexuality & Bisexuality
- Disease Gene: Infectious lower fertility
- Disease Gene: Infectious higher sex need
- Disease Gene: Fluctual Sexual Need. (Configurable) Chance to reset sex-need to near-zero and gain a bit of rest-need.
- Disease Gene: Size Blinded. Pawns have a higher chance for hooking up with pawns with a big cock, lower chance for small cocks.
- Infector Gene: Genetic Stretcher. Pawns can infect other pawns with *Size Blinded*
- Gene: Hardwired Progenity. Pawns with this get a malus on having no-children, and bonus on having a lot. 
- Gene: Sexual Genetic Swap. Pawns have a chance to switch a random gene with their sexpartner. 
- (Archite) Gene: Sexual Genetic Thief. Pawns have a chance to steal a gene from their sexpartner. Genetic Disease Immunity shields against this. 
- Gene: Sperm Displacement. Pawns might overwrite an existing pregnancy, becoming the new father. The pregnancy will stay in its gestation progress.
- Gene: Twinkification. Pawns turn their (male) sexual partners into breedable twinks.
- Gene: Feminization. Pawns turn their (male) sexual partners into women.
- Gene: Blocked Masturbation. Pawns cannot masturbate. 
- {Sexperience} Gene: Living Cumbucket. Pawns with this Gene get "filled" upon sex, and slowly disperse usable gathered cum. 
- Disease Gene: Infectious Blocked Masturbation
- Gene: Rut. Pawns have a chance to go into heat and need more sex for a day. (Default: 5% chance per day, to go 1 day in heat).
- Disease Gene: Infectious Rut.
- Pawns will have negative thoughts about pawns with more genetic diseases than themselves. 
- Faction Penalties for spreading diseases, stealing genes and aging pawns with age transfer
- Patch  for [Imphilee Xeno](https://steamcommunity.com/sharedfiles/filedetails/?id=2990674516) by @Bunuffin

**Changes**::

- Cum-Amount-Changing genes now are XML Adjustable and share a single `.cs`-class
- Incubi are now Bisexual too, as they should be. 

**Fixes:**

- Fixed an Issue where pawns would always get the Pheromone social boost, unless they had the pheromone (#113)
- Fixed two hidden dependencies on Ideology and Royalty (#115)
- Fixed some more hidden dependencies on Ideology Icons (#118)
- Fixed a hidden dependency on Licentialabs (#119)

**Internal:**

- GenderFluid-Gene now uses a generalized `TickBasedChanceExtension` over its unique special `GenderFluidExtension`
- Introduced a `ModLog.Debug` Function that checks for the settings before printing - trying to spread it over the whole project. 
- Removed TODO File. I have enough to do. 

**Notes:**

*One Time Error Load*

The changes to the cum-gene will give a 1-time warning on loading the save. The warning looks like this: 
```
Could not find class RJW_Genes.Gene_MuchCum while resolving node li. Trying to use Verse.Gene instead. Full node: <li Class="RJW_Genes.Gene_MuchCum"><def>rjw_genes_much_cum</def><pawn>Thing_Human697</pawn><overriddenByGene>null</overriddenByGene><loadID>82</loadID></li>
UnityEngine.StackTraceUtility:ExtractStackTrace ()
Verse.Log:Error (string)
...
```

This is not dangerous.

*Blocked Masturbation* 

Might not be fully working - for testing, I run things in DevMode, and I can just order people to masturbate. 
Please playtest this a bit for me, as I want to make it work nicely. 

*Supporting*

You can now support me with [buying me a coffee](https://buymeacoffee.com/vegapnk). 
The mod will remain free, open source and I will not hide or lock any features. 
Its just meant if you want to drop me a tip. 

**Since Beta-1** (11-07-2024) 

- Made the Feminizer and Twinkifier configurable with XML.
- Typos in the Hediff Defs, tweaking of some values.
- Living Cum-Bucket & Rut Genes
- Great icons by @Alpenglow

**Since Beta-2** (17-07-2024)

- Changed behaviour of living cumbucket. Now, once "really full" the output happens more rarely but is much more at once.
- More Icons by @Alpenglow <3 this time attributed correct. 
- Adjusted some of the metabolic values - likes cumflation, generous donor and living cumbucket have small costs.

# 2.1.0 (27-06-2024)

**Additions**:

- New Ability Gene *Mating-Call*: Get bred by all nearby animals. 
- New Ability Gene *Pheromone Spit*: Mark a target to be bred by nearby animals
- New Passive Gene *Sexual Tamer*: Chance to tame or train animals on bestiality.
- Human + Animal Pregnancy can (if enabled in settings) produce Vanilla Expanded Genetics Hybrids. Thanks to @Jaaldabaoth (#88)
- Xenogenes for "Big and Small" Xenotypes thanks to @Flock-of-birds (#83)
- Xenogenes for "Biotech Expansion Mammalia" and "Biotech Expansion - Mythic" thanks to @Ohreallyow (#86)
- Xenogenes update for (many?) "Vanilla Races Expanded (VRE)" Mods - Thanks to @Jaaldabaoth (#84 / #88 )
- Genes can now fulfill RJW Quirks, configurable in XML. Thanks to @Jaaldabaoth (#84 / #88)
- "Genes" to support VRE Androids having Mechanical Genitalia. Thanks to @Jaaldabaoth (#88)
- "Curiosity Genes" alongside other Curiosities from VRE. Thanks to @Jaaldabaoth (#88)
- Support for many VE-Genetics Animals into the Animal-Framework. Thanks to @Jaaldabaoth (#88)
- "Likes Cumflation"-Gene now also makes pawns immune against the effects of cumflations. They are still cumflated. 
- Gasmasks make immune against Aphrodisiac Pheromones (#108)

**Fixes**:

- Licentia Genes are back in and should work again. I used the [updated for by Jaaldabaoth](https://gitgud.io/Jaaldabaoth/licentia-labs) for my testing.
- Lower-casing most labels to fit rimworld standards, thanks to @Flock-of-birds (#83)
- X-Gender-Only Genes leave / re-add artificial genitalia. Thanks to @Jaaldabaoth (#84 / #88)
- Issues with Elasticity References (#87)
- Null Pointer for LoveFeeding when finding Mechs. Thanks to @Jaaldabaoth (Fixes #90)
- Changes to the Implants introduced in 2.0.0 - they imposed a hidden dependency on Royalty (Fixes #89)
- Issues when generating relations (#98, fixed in #106 thanks @Jaaldabaoth)
- Hardening of Licentia Dependencies (#105)
- Littered Birth and Chest-Burst Pregnancy are now mutually exclusive (#96)
- Youth Fountain cannot youth on masturbation anymore (#99)
- Documented the HasActiveGene Error Message (#104)
- Pawns with "rjw_genes_no_sexneed" wont go raping (or atleast way less, #100)
- Extra Nullcheck for Genes in Orgasmic Mytosis (#95)
- Orgasmic Mytosis Pawns *should* inherit the Xenotypename and favorite colour now 
- Babies should not have "SexChangeThoughts" anymore when they had a (fe)male-only gene (#103)

**Changes**:

- Minified some Race-Patches, thanks to @Flock-of-birds (#83)
- XML-Genitalia-Genes can now also provide Breasts. Thanks to @Jaaldabaoth (#84 / #88)
- Many patches and files went into a `Mods`Folder and use an `LoadFolders.xml`. Thanks to @Jaaldabaoth (#84 / #88)
- Translate-Keys for Settings 
- Translate-Key for Animal Hybrid Race-Names
- "Tick-Speed" for Evergrowth moved from Mod-Settings to XML
- Some new Icons thanks to Kira-Bad-Artist
- Some new Icons thanks to Archer 

# 2.0.0 (30-05-2024)

**Summary**:

- 1.5 Support 
- Femboys, MPReg
- Couple Implants around Fertility / Sex / Pregnancy
- Hive Removal (to be reintroduced somewhere else)
- Licentia Genes became placeholders, no functions until Licentia is on 1.5

**Additions**: 

- initial Rimworld 1.5 thanks to @jaaldabaoth
- Fertile Anus Gene, thanks to @jaaldabaoth
- Femboy Genes, for male pawns with Vaginas @jaaldabaoth
- Udder Gene @jaaldabaoth , Fixes #57
- Some other Genitalia Sets (Crocodilian, Racoon, ...) thanks to @jaaldabaoth, Closes #57
- LitteredBirthsGene thanks to @jaaldabaoth
- OvaryAgitator that allows for a cyclic fertility. Thanks @jaaldabaoth. See [its description](./Common/Defs/ThingDefs/OvaryAgitator.xml)
- LimbicStimulator / Scrambler thanks to @jaaldabaoth. See [Scrambler Description](Common/Defs/ThingDefs/Scrambler.xml) and [Limbic Stimulator](./Common/Defs/ThingDefs/LimbicStimulator.xml)
- Bioscaffold that allows for faster pregnancies. thanks to @jaaldabaoth. See [its description](./Common/Defs/ThingDefs/Bioscaffold.xml)
- Multi-Pregnancy Gene thanks to @jaaldabaoth
- Many genes have settings configurable in their xml - tick speeds and distances.

**Changes**: 

- Modularised many "change gentialia sets"-genes into a XML configurations. Thanks to @flock-of-birds
- **Removed** Hive Logic for now! This might break some of your save-games. 
- Highmates "Initiate Loving" uses Succubus "Seduce" ability, thanks to @jaaldabaoth
- Slider for Evergrowth Tick-Speed, thanks to @jaaldabaoth
- Resizing Age for resizing Genes can be set in Settings @jaaldabaoth
- **Licentia related genes are only placeholders**, as Licentia is not 1.5 yet
- InsectIncubator only does self-fertilization now, but does not increase storage capacities for eggs anymore
- Some Genes (GenitaliaTypes, Cum, some others) cannot be in quest-rewards anymore. Special, Size, Breeding and Lifeforce genes still can be quest rewards.

**Fixes:** 

- Small changes to drawing Succubus Wings
- Minor Format changes to lots of XML files
- Pawns with ONLY an Archotech penis will not give Fertilin, and will not receive debuffs (Fixes #71)

**Hidden Changes:** 

- Patches for "Same Mother" / "Same Father" to account for Male-Pregs. This should only extend behaviour but be warned a bit. 
- Patches to RJW-Pregnancy Helper to account for Male Pregs. 
- Some replacements from `hasGene` to `hasActiveGene` 

# 1.3.3 (02-11-2023)

**Fixes:** 

- Added another check to the AG Malachai Xenotype (Fixes #68)
- Fixed the `GatheredCum`Fertilin Ingestion Patch throwing an Error for people without Sexperience (Fixes #69 (nice))

# 1.3.2 (24-10-2023)

**Fixes:**

- Removed patch for Malachai, needs a different patching operator but I don't want to have broken fixes for now (Tracked in a new Issue)

# 1.3.1 (22-10-2023)

**Changes**

- Added patches for Alpha-Genes Animusen, Drakonori and Malachai (Closes #65, thanks @Stars22223)
- Little Social Boost for Pawns affected by pheromones (Closes #50)

**Fixes:**

- XML Missmatch for Succubus Letter (Fixed in #64, thanks @mwcrow)
- Made constructor for Empathetic Lifeforce explicitly `public` to not hang up in character creation (Fixes #66)

# 1.3.0 (19-09-2023)

**Changes:**

- New Gene for Evergrowing Cocks. Be careful. 
- New Gene for Genderfluid Pawns - daily chance to change sex. Futas just change "display" and keep genitalia, other pawns switch genitalia. XML configurable times & chances. 
- New Drawings for the Succubi Wings & Tail, thanks to @Monti (donated via Discord)
- Simple Gene that removes Sex need (called asexual, `rjw_genes_no_sex_need`)
- New Gene that grows Penisses on Oral sex. Configurable in XML. 
- New Gene Lifeforce Empath: Gain Lifeforce for sexually satisfied pawns, loose for frustrated pawns.
- More genes are configurable with XML, e.g. tick speed, distances or multipliers. I am getting the hang of it. 
- Halfed Succubus Fertilin-Need, increased cost of abilities.
- Translation Keys for some letters

**Fixes:**

- New attempt at fixing condom consumption for Fertilin, adressing #41 & #48 provided by Infi
- Copy of Infis patch for eating cum from sexperience, #41 and #48
- Updated some Icons to have better backgrounds (thanks @WasmachenDennSachenSo #53)
- Custom Queen- and Drone-Xenotypes should work now. 
- Some more checks if Queen is on Map or not (fixing #60)

*Notes*: 
The pawns that are gender fluid can get pregnant. 
However, with RJW 5.3.7 these pregnancies disappear. 
This is a change needed upstream, but I will have a look. 

# 1.2.1 (18-06-2023)

**Fixes**: 

- Issue with RJW Changes for Orgasms, #52. Methods were renamed. 
- Notes on the Gene Inheritance #51

# 1.2 (11-06-2023)

**Changes:**

- Cocoon Weaver Gene
- Spawn Spelopede Gene (Can be changed to spawn megascarabs or other insects via xml)
- Queens & Caste logic (see below)
- Addition to InsectIncubator: Now fertilizes eggs once placed inside a host, and breeds out eggs roughly twice as fast.
- Many new icons 
- Custom background icons when Vanilla-Expanded-Framework is loaded
- Sexual Age Drainer & Youth Fountain now change age as configured in XML
- Draft for a Hive-Start Scenario
- Added Orgasmic Mytosis Gene: On Multiple Orgasms, spawn an identical copy of a pawn. Items and Implants are not copied.
- Patches for Alpha Genes Xenotypes and LTS Xenotech
- New Simple Genitalia Patches for other popular Xenotypes (Thanks @Pali42K)

**Internal:**

- Renamed abilities to have _ability_ in their name, to not exactly match the gene-defnames.
- Moved Insect-Incubator & Insect-Breeder to hive category and folders (from breeding)
- Some exclusion-tags for Alpha Genes
- Removed Patches for conditional Genes, and moved them to `mayRequire` in the XenotypeDefs

**Fixes:**

- Cockeater Ability has now Icon of Cockeater Gene
- Cockeater now leaves a bite wound!
- Pythokin-Patch checks for Licentialabs (#30)
- Removed Sex-Change thoughts for pawns born or spawned with a gender-altering gene (Issue #32, PR #33 by @callavico)
- More consistent behavior for genitalia resizing over multiple game-starts (Issue #34)

**Queen & Caste Logic**

There are 3 genes revolting around a new, hopefully flexible insect-caste system. Queens, Drones and Workers. These reproduce either through normal sex, or can utilize the insect birth once [this PR](https://gitgud.io/Ed86/rjw/-/merge_requests/266) has been merged in. 
Update: It has been merged into [RJW 5.3.5](https://gitgud.io/Ed86/rjw/-/tags/5.3.5), make sure you update!

In general, the logic is the following: 

- A queen can have sex with anyone. If the partner was a drone, there is chance for the baby to become a queen, drone or worker. 
- If the partner of the queen was not a drone, the baby will be a worker.
- If the drone didn't mate with a queen but someone else, normal inheritance happens
- The assignment is done by xenotypes for queen and drones. The baby will get all xenogenes of their parents chosen xenotypes. 
- For workers, every queen can have a set of genes for their workers defined in [a special def](./Common/Defs/QueenWorkerMappingDefs/QueenWorkerMappingDefs_base.xml). These will be added as endogenes, so that pawns can still become xenotypes.
- There is a default gene-set for workers, making dumb, sterile and servile pawns. 
- Chances for Offsprings (Drone, Queen, Worker) is defined in an [XML-Def](./Common/Defs/HiveOffspringChanceDef/HiveOffspringChanceDefs.xml). They are set per Queen. 
- Birthlogic should apply for normal pregnancies, and for RJW-Insect Eggs. Other Pregnancies (from mods) are not supported.

I am not sure if I want to have a specific mapping defining that queen can only mate with certain drones, let me know how you feel about it. 
*Queens can be male*. I just used the female-term, but implementation is gender-neutral. 

**On Alpha Genes** 

Alpha genes might have colliding features, but I need some reports to find out about it. 
I disabled the specific gender and the oviparious reproduction (when you have rjw-ovipos). 
I recommend using alpha genes for the Hive-Playthroughs, as otherwise the Halamyr look a bit ... boring? 
But i don't want to add a bunch of cosmetic genes on top of things. 

*And what the fuck is a halamyr?* Well I had to name my little ants somehow. But I didn't want to call them `myr` as I maybe want to make some [TiTs](https://www.fenoxo.com/play-games/) xenotypes separately.
And I am aware that the TiTs-Myr work different than the things I made now. 


**Changes Since beta-1**:

- Fix of icon-names (#36)
- Changes to the scenario (more building items, throne for start). Wealth is now at 12k, which is the same as crashlanded and lost tribe.
- Added the Orgasmic Mytosis Draft
- Many touches on the Halamyr Hive Logic and Fertilizitation (#37,#38)

**Changes Since beta-2**: 

- Mostly Patches and Changes to the Halamyr Defs
- Some re-arranging and mayRequires for other mods

# 1.1.4 (06-04-2023)

**Fixes:**

- Youth Fountain and Age Drainer **really** "stop" at 18 (#26, #28) and never age pawns
- Drastically reduced vomiting time due to an missunderstanding (#29). `0.4` instead of `0.01`

Sometimes life is like that, and you have to fix the fixes. 
It was never really broken, life is just very long. 

# 1.1.3 (28-03-2023)

**Changes:**

- Youth Fountain and Age Drainer "stop" at 18 (#26)
- Youth Fountain and Age Drainer activate only for pawns at 18 (#26)
- Drained Pawns vomit less (from mtb 0.05 to 0.01)(#29)

**Fixes:**

- InsectBreeder would mess with normal Pawn-Animal pregancy for egg laying animals (#23)

# 1.1.2 (19-03-2023)

**Changes:**

- Added more cool images from WasMachenDennSachen (#22)

**Fixes:** 

- Aphrodisiac Pheromones checks for children and other conditions (#25)

# 1.1.1 (10-03-2023)

**Changes:** 

- Drastically increased mood-penalty for Fertilin-Loss (if the pawn is still too happy, there will never be a breakdown for missing fertilin)
- No-Breast Genes add Nipples
- Featureless Chest Gene (No Nipples at all, adds the RJW Featureless Chest as requested per some Kobold fetishists)

**Fixes:** 

- Small and Big Male Genitalia had images wrong way round 
- Fertilin should activate at a MinAge of 18 

**Important**: The Fertilin Changes could throw errors! I tested a bit, but not a lot. 
So please reach out if you get something and I will try to fix it ASAP. 

# 1.1.0 (2023-03-04)

1.1.0-beta-1 was released on 05.02.2023. 

Changes since beta: Typos, Icons & Merge of Anal & Vaginal Absorber.

## Features: 

- Succubus, other Fertilin Xenotypes (see below)
- Orgasm Rush got a Hediff
- Unbreakable Gene
- Age-Transfer and Youth-Fountain per Sex Gene 
- Bisexual and Homosexual Genes with Placeholder Icon
- Balancing some Genes by changing metabolism and complexity
- Pheromone Gene (#13, Shabakur)
- RJW Race-Support Compatibility (#12,#13, Shabakur)
- Animal Gene Inheritance Gene-Chance Multiplier per Setting (#13, Shabakur)
- Lots of Debug-Only Logging for Animal Gene Inheritance
- Patches for some popular Xenotype Mods (Nyaron, Kijin3, Roos Minotaurs, VRE Phytokin)
- Generous Donor Cheatmode 
- Mod-Settings 

## Fertilin:

Big news ! We got a system similar to hemogen running, labelled *Fertilin*. 

Pawns gain Fertilin through various sources, and can spend them on various abilities and loose it over time. 

Fertilin-Sources: 

- Vaginal & Anal Absorption (Through sex in respective types)
- Cum-Eater (through oral sex, eating cum from Sexperience, or cunnilinguing (?) cumflated pawns)
- Sex Drain, after Sex apply a debuff to the fucked pawn 
- Cock-Eater, bite off the wiener of downed enemies, own colonists, animals, slaves, not sure about visitors but let me know what you think. 

Animals give less Fertilin as a source, configurable in the Mod Settings. 
The Vaginal, Anal and CumEater interactions *empty* the pawns balls and the pawn needs to *recharge*. Gain and recharge are based on cum-production. 
The drainer is a flag - undrained pawns can be drained, drained pawns will not get any debuff and will not give any fertilin.

Fertilin-Abilities: 

- Heal Pussy; Rape someone to tend their wounds.
- Seduce; Target Pawn needs to follow the caster, engaging in sex on collision.
- Paralysing Kiss; Stun someone in meele range.
- Naked Prowess; When the pawn is naked, get a 3h buff on meele stats and armor. 

Other than that, the Xenotypes tend to have other buffs from base-game to make up for their insatiable hunger. 

Xenotypes: 

**Succubi** absorb Fertilin through sex and can use it for paralyzing kiss and seduce. 

**Incubi** are their male parallels, but gaining fertilin through draining. 

**Cumazones** are female only martial fighters, that can only gain fertilin through biting cocks. Be sure to have enough supply before you hire one of these bad bitches. 

Incubi and Succubi can spawn in a special event at night when pawns have low sex need. Cumazones can just appear randomly. 

**_On Inheritance:_** 

We realized that it's not nice if the Succubi Babies do not have all Succubi Genes, and have Fertilin with no use for it or look like wookies instead. Hence, we changed the Fertilin Xenotypes to be Xenogenes (non-inheritable) *BUT* we recommend using the [dominant-gene](https://steamcommunity.com/sharedfiles/filedetails/?id=2884110898) mod. 
If the dominant-gene mod is loaded, our xenotypes will spawn with it, making Succubus Babies Succubi. 
In case you do not want that behaviour, look for the Patch `Patch_Dominant_Gene.xml` and delete it. 

**_On Making your own Succubi_:**

Currently Succubi and Incubi only spawn through a special event determined by their xenotype def name. So if you make your own, they will not spawn with this event. Make sure that you have a way to get your custom xenotypes appear. You might want to "just alter" the xenotype xml provided by us, then they will spawn with the event. 

## Fixes:

- Issue with Breast-Size (#8) fixed by lowercasing breast-match (Shabakur) 
- Error on Game Load when Licentia Genes are tried to be added to Xenotypes for players without Licentia (#5,#17)
- Futa Gene only triggers if Pawn is not a futa already (#2)
- Genitalia Resizing triggers on 20th Birthday (#11)
- RJW-Gene-Inheritance Settings now do things (#13, Shabakur)
- Check for Animals in Orgasm Rush, no Orgasm Rush for and from Animals (#15)
- Using Character Editor, it can happen that the Genes fired twice. I hoped to harden this issue by checking better (#19) 

# 1.0.1 (2022-12-20)

- Fix issue with Orgasm Rush throwing an Error on Animal Orgasm (Thanks Shabakur)
- (Internal) Use of RJW methods to clean up racegroupdefs

# 1.0.0 (2022-12-19)

Initial Release ! 

Content: 

- Genes for Base-RJW-Genitalia Types 
- Genes for Small and Big Genitalia per Genitalia (Breast, Anus, Penis, Vagina)
- Elasticity Gene if Licentia is loaded
- Genes to modulate Cum amount (None, much, very much)
- Male and Female only Genes
- Genes for Genitalia-Amount (2 Penis, 0 Penis, etc.)
- Futa-Gene that adds the other Genitalia, without touching Gender
- Orgasm Rush, restoring sleep for Pawns on Orgasm
- Breeding Genes: Immunity to Mech Birthing Damage, Fertilizing All Eggs in Pawn and more egg space 
- Genes that add RJW Traits (Zoophile, Necrophile, Hypersexual, Rapist)
- Animal Gene Inheritance, utilizing Base-RJW Racegroups and a Dictionary for which Genes are how likely to appear in Crossbreeding
