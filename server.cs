//we need the base add-on for this, so force it to load
%error = ForceRequiredAddOn("Weapon_AEBase");

if(%error == $Error::AddOn_NotFound)
{
	//we don't have the base, so we're screwed =(
	error("ERROR: AEBase_L4D2 - required add-on Weapon_AEBase not found");
}
else
{
//   exec("./Support_EffectDatablocks.cs");
   exec("./Sounds/Sounds.cs");
   exec("./Weapon_AK47.cs");
   exec("./Weapon_M16.cs");
   exec("./Weapon_FNScar.cs");
   exec("./Weapon_HuntRifle.cs");
   exec("./Weapon_MilSniper.cs");
   exec("./Weapon_Mac10.cs");
   exec("./Weapon_Uzi.cs");
   exec("./Weapon_Deagle.cs");
   exec("./Weapon_HuntShotgun.cs");
   exec("./Weapon_ChromeShotgun.cs");
   exec("./Weapon_Spas12.cs");
   exec("./Weapon_AutoShotgun.cs");
}