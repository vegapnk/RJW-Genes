# 1.2

Changes:

- Cocoon Weaver Gene
- Spawn Spelopede Gene (Can re changed to spawn Megascarabs or other insects)

Internal:

- Renamed Abilities to have _ability_ in their name, to not exactly match the gene-defnames.

Fixes: 

- Cockeater Ability has now Icon of Cockeater Gene
- Cockeater now leaves a bite wound!
- Pythokin-Patch checks for Licentialabs (#30)

ToDo: 

- Icons: Cocoon, Spelopede Spawn
- Sound: Spelopede Spawn

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
