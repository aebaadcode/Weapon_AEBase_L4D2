//Why disable something nessisary?
%error = ForceRequiredAddOn("Weapon_Gun");

if(%error == $Error::AddOn_Disabled)	
{
	//Oh jesus this persons an idiot.
   	error("ERROR: Weapon_MilSniper - required add-on Weapon_Gun not found");
}
else
{
   	exec("./Support_AmmoGuns.cs");
   	exec("./Weapon_MilSniper.cs");  
}