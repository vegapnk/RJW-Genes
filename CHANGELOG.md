# 1.3.0

**Changes:**

- New Gene for Evergrowing Cocks. Be careful. 
- New Gene for Genderfluid Pawns - daily chance to change sex. Futas just change "display" and keep genitalia, other pawns switch genitalia.
- New Drawings for the Succubi Wings & Tail, thanks to @Monti (donated via Discord)

**Fixes:**

- New attempt at fixing condom consumption for Fertilin, adressing #41 & #48 provided by Infi
- Copy of Infis patch for eating cum from sexperience, #41 and #48
- Updated some Icons to have better backgrounds (thanks @WasmachenDennSachenSo #53)

*Notes*: 
The pawns that are gender fluid can get pregnant. 
However, with RJW 5.3.7 these pregnancies disappear. 
This is a change needed upstream, but I will have a look. 

# 1.2.1 

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

# 1.1.4

Fixes: 

- Youth Fountain and Age Drainer **really** "stop" at 18 (#26, #28) and never age pawns
- Drastically reduced vomiting time due to an missunderstanding (#29). `0.4` instead of `0.01`

Sometimes life is like that, and you have to fix the fixes. 
It was never really broken, life is just very long. 

# 1.1.3

Changes:

- Youth Fountain and Age Drainer "stop" at 18 (#26)
- Youth Fountain and Age Drainer activate only for pawns at 18 (#26)
- Drained Pawns vomit less (from mtb 0.05 to 0.01)(#29)

Fixes: 

- InsectBreeder would mess with normal Pawn-Animal pregancy for egg laying animals (#23)

# 1.1.2

Changes:

- Added more cool images from WasMachenDennSachen (#22)

Fixes: 

- Aphrodisiac Pheromones checks for children and other conditions (#25)

# 1.1.1 

Changes: 

- Drastically increased mood-penalty for Fertilin-Loss (if the pawn is still too happy, there will never be a breakdown for missing fertilin)
- No-Breast Genes add Nipples
- Featureless Chest Gene (No Nipples at all, adds the RJW Featureless Chest as requested per some Kobold fetishists)

Fixes: 

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
