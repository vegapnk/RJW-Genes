# 1.1.0 (2023-xx-xx)

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

## Fixes:

- Issue with Breast-Size (#8) fixed by lowercasing breast-match (Shabakur) 
- Error on Game Load when Licentia Genes are tried to be added to Xenotypes for players without Licentia (#5,#17)
- Futa Gene only triggers if Pawn is not a futa already (#2)
- Genitalia Resizing triggers on 20th Birthday (#11)
- RJW-Gene-Inheritance Settings now do things (#13, Shabakur)
- Check for Animals in Orgasm Rush, no Orgasm Rush for and from Animals (#15)

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
