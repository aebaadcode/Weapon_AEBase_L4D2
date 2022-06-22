datablock AudioProfile(L4D2_FNSCARFireSound)
{
   filename    = "./Sounds/Fire/FNSCAR_fire.wav";
   description = MediumClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(L4D2_FNSCARItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./FNSCAR/FNSCARPU.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "L4D2: FN-Scar";
	iconName = "./Icons/UI_SCAR";
	doColorShift = true;
	colorShiftColor = "0.4 0.4 0.41 1.000";

	 // Dynamic properties defined by the scripts
	image = L4D2_FNSCARImage;
	canDrop = true;

	AEAmmo = 50;
	AEType = AE_LightRAmmoItem.getID();
	AEBase = 1;

	RPM = 800;
	recoil = "Heavy";
	uiColor = "1 1 1";
	description = "Powerful and reliable, the AK-47 is one of the most popular assault rifles in the world.";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(L4D2_FNSCARImage)
{
   // Basic Item properties
   shapeFile = "./FNSCAR/FNSCAR.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = L4D2_FNSCARItem;
   ammo = " ";
   projectile = AEProjectile;
   projectileType = Projectile;

   casing = AERifleShellDebris;
	shellExitDir        = "2.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = true;
	safetyImage = L4D2_FNSCARSafetyImage;
 //   scopingImage = L4D2_FNSCARIronsightImage;
	doColorShift = true;
	colorShiftColor = L4D2_FNSCARItem.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellRifle;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

	muzzleFlashScale = "1 1 1";
	bulletScale = "1 1 1";

	projectileDamage = 10;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 400;
	projectileTagStrength = 0.51;  // tagging strength
	projectileTagRecovery = 0.03; // tagging decay rate

	recoilHeight = 0.25;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 5; // how much shots it takes to trigger spread i think
	spreadReset = 400; // m
	spreadBase = 100;
	spreadMin = 100;
	spreadMax = 500;

	screenshakeMin = "0.1 0.1 0.1";
	screenshakeMax = "0.15 0.15 0.15";

	farShotSound = RifleGDistantSound;
	farShotDistance = 40;

	sonicWhizz = true;
	whizzSupersonic = true;
	whizzThrough = false;
	whizzDistance = 14;
	whizzChance = 100;
	whizzAngle = 80;

	staticHitscan = true;
	staticEffectiveRange = 110;
	staticTotalRange = 2000;
	staticGravityScale = 1.5;
	staticSwayMod = 2;
	staticEffectiveSpeedBonus = 0;
	staticSpawnFakeProjectiles = true;
	staticTracerEffect = ""; // defaults to AEBulletStaticShape
	staticScaleCalibre = 0.25;
	staticScaleLength = 0.25;
	staticUnitsPerSecond = 400;

		flashlightDistance = 8;
		laserOffStates = "";

	projectileFalloffStart = $ae_falloffRifleStart;
	projectileFalloffEnd = $ae_falloffRifleEnd;
	projectileFalloffDamage = $ae_falloffRifle;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.
   // Initial start up state
	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.73;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSound[0]				= L4D2_FNSCARUseSound;
	stateSequence[0]			= "use";

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "ReloadWait";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

// please pay no attention to the state spam =3
	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateTimeoutValue[3]			= 0.001;
	stateEmitter[3]					= AEBaseSmokeEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[4]				= "FireLoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.01;
	stateTransitionOnTimeout[4]		= "FireLoadCheckB";
	
	stateName[5]				= "FireLoadCheckB";
	stateTransitionOnAmmo[5]		= "preFire2";
	stateTransitionOnNoAmmo[5]		= "ReloadWait";
	stateTransitionOnNotLoaded[5]  = "Ready";
// fire 2
	stateName[6]                       = "preFire2";
	stateTransitionOnTimeout[6]        = "Fire2";
	stateScript[6]                     = "AEOnFire";
	stateEmitter[6]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[6]				= 0.05;
	stateEmitterNode[6]				= "muzzlePoint";
	stateFire[6]                       = true;
	stateEjectShell[6]                       = true;

	stateName[7]                    = "Fire2";
	stateTransitionOnTimeout[7]     = "FireLoadCheckA2";
	stateTimeoutValue[7]			= 0.001;
	stateEmitter[7]					= AEBaseSmokeEmitter;
	stateEmitterTime[7]				= 0.05;
	stateEmitterNode[7]				= "muzzlePoint";
	stateAllowImageChange[7]        = false;
	stateSequence[7]                = "Fire";
	stateWaitForTimeout[7]			= true;
	
	stateName[8]				= "FireLoadCheckA2";
	stateScript[8]				= "AEMagLoadCheck";
	stateTimeoutValue[8]			= 0.01;
	stateTransitionOnTimeout[8]		= "FireLoadCheckB2";
	
	stateName[9]				= "FireLoadCheckB2";
	stateTransitionOnAmmo[9]		= "preFire3";
	stateTransitionOnNoAmmo[9]		= "ReloadWait";
	stateTransitionOnNotLoaded[9]  = "Ready";
// fire 3
	stateName[10]                       = "preFire3";
	stateTransitionOnTimeout[10]        = "Fire3";
	stateScript[10]                     = "AEOnFire";
	stateEmitter[10]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[10]				= 0.05;
	stateEmitterNode[10]				= "muzzlePoint";
	stateFire[10]                       = true;
	stateEjectShell[10]                       = true;

	stateName[11]                    = "Fire3";
	stateTransitionOnTimeout[11]     = "FireLoadCheckA3";
	stateTimeoutValue[11]			= 0.001;
	stateEmitter[11]					= AEBaseSmokeEmitter;
	stateEmitterTime[11]				= 0.05;
	stateEmitterNode[11]				= "muzzlePoint";
	stateAllowImageChange[11]        = false;
	stateSequence[11]                = "Fire";
	stateWaitForTimeout[11]			= true;
	
	stateName[12]				= "FireLoadCheckA3";
	stateScript[12]				= "AEMagLoadCheck";
	stateTimeoutValue[12]			= 0.01;
	stateTransitionOnTimeout[12]		= "FireLoadCheckB3";
	
	stateName[13]				= "FireLoadCheckB3";
	stateTransitionOnAmmo[13]		= "preFire4";
	stateTransitionOnNoAmmo[13]		= "ReloadWait";
	stateTransitionOnNotLoaded[13]  = "Ready";
// fire 4
	stateName[14]                       = "preFire4";
	stateTransitionOnTimeout[14]        = "Fire4";
	stateScript[14]                     = "AEOnFire";
	stateEmitter[14]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[14]				= 0.05;
	stateEmitterNode[14]				= "muzzlePoint";
	stateFire[14]                       = true;
	stateEjectShell[14]                       = true;

	stateName[15]                    = "Fire4";
	stateTransitionOnTimeout[15]     = "FireLoadCheckA4";
	stateTimeoutValue[15]			= 0.001;
	stateEmitter[15]					= AEBaseSmokeEmitter;
	stateEmitterTime[15]				= 0.05;
	stateEmitterNode[15]				= "muzzlePoint";
	stateAllowImageChange[15]        = false;
	stateSequence[15]                = "Fire";
	stateWaitForTimeout[15]			= true;
	
	stateName[16]				= "FireLoadCheckA4";
	stateScript[16]				= "AEMagLoadCheck";
	stateTimeoutValue[16]			= 0.01;
	stateTransitionOnTimeout[16]		= "FireLoadCheckB4";
	
	stateName[17]				= "FireLoadCheckB4";
	stateTransitionOnAmmo[17]		= "preFire5";
	stateTransitionOnNoAmmo[17]		= "ReloadWait";
	stateTransitionOnNotLoaded[17]  = "Ready";
// fire 5
	stateName[18]                       = "preFire5";
	stateTransitionOnTimeout[18]        = "Fire5";
	stateScript[18]                     = "AEOnFire";
	stateEmitter[18]					= AEBaseRifleFlashEmitter;
	stateEmitterTime[18]				= 0.05;
	stateEmitterNode[18]				= "muzzlePoint";
	stateFire[18]                       = true;
	stateEjectShell[18]                       = true;

	stateName[19]                    = "Fire5";
	stateTransitionOnTimeout[19]     = "FireLoadCheckA5";
	stateTimeoutValue[19]			= 0.001;
	stateEmitter[19]					= AEBaseSmokeEmitter;
	stateEmitterTime[19]				= 0.05;
	stateEmitterNode[19]				= "muzzlePoint";
	stateAllowImageChange[19]        = false;
	stateSequence[19]                = "Fire";
	stateWaitForTimeout[19]			= true;
	
	stateName[20]				= "FireLoadCheckA5";
	stateScript[20]				= "AEMagLoadCheck";
	stateTimeoutValue[20]			= 0.25;
	stateTransitionOnTimeout[20]		= "FireLoadCheckB5";
	
	stateName[21]				= "FireLoadCheckB5";
	stateTransitionOnAmmo[21]		= "Ready";
	stateTransitionOnNoAmmo[21]		= "Reload";	
	stateTransitionOnNotLoaded[21]  = "Ready";
	// end of spam :P

	stateName[22]				= "LoadCheckA";
	stateScript[22]				= "AEMagLoadCheck";
	stateTimeoutValue[22]			= 0.1;
	stateTransitionOnTimeout[22]		= "LoadCheckB";
	
	stateName[23]				= "LoadCheckB";
	stateTransitionOnAmmo[23]		= "Ready";
	stateTransitionOnNotLoaded[23] = "Empty";
	stateTransitionOnNoAmmo[23]		= "ReloadWait";

	stateName[24]				= "Reload";
	stateTimeoutValue[24]			= 0.96;
	stateScript[24]				= "onReloadStart";
	stateTransitionOnTimeout[24]		= "ReloadB";
	stateWaitForTimeout[24]			= true;
	stateSequence[24]			= "reload1";
	stateSound[24]				= L4D2_FNSCARClipOutSound;
	
	stateName[25]				= "ReloadB";
	stateTimeoutValue[25]			= 1.31;
	stateTransitionOnTimeout[25]		= "Reloaded";
	stateWaitForTimeout[25]			= true;
	stateSequence[25]			= "reload2";
	stateSound[25]				= L4D2_FNSCARClipInSound;
		
	stateName[26]				= "Reloaded";
	stateTimeoutValue[26]			= 0.1;
	stateScript[26]				= "AEMagReloadAll";
	stateTransitionOnTimeout[26]		= "Ready";
	
	stateName[27]				= "ReadyLoop";
	stateTransitionOnTimeout[27]		= "Ready";

	stateName[28]          = "Empty";
	stateTransitionOnTriggerDown[28]  = "Dryfire";
	stateTransitionOnLoaded[28] = "ReloadWait";
	stateScript[28]        = "AEOnEmpty";

	stateName[29]           = "Dryfire";
	stateTransitionOnTriggerUp[29] = "Empty";
	stateWaitForTimeout[29]    = false;
	stateScript[29]      = "onDryFire";
	
	stateName[30]				= "ReloadWait";
	stateTimeoutValue[30]			= 0.3;
	stateTransitionOnTimeout[30]		= "Reload";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function L4D2_FNSCARImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(3); 
    %obj.playAudio(3, L4D2_FNSCARFireSound);
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function L4D2_FNSCARImage::AEOnLowClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(0, plant);
}

function L4D2_FNSCARImage::AEOnMedClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(3, jump);
   %obj.aeplayThread(0, plant);
}

function L4D2_FNSCARImage::AEOnHighClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(3, jump);
}

function L4D2_FNSCARImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function L4D2_FNSCARImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function L4D2_FNSCARImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function L4D2_FNSCARImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(L4D2_FNSCARSafetyImage)
{
   shapeFile = "./FNSCAR/FNSCAR.dts";
   emap = true;
   mountPoint = 0;
   offset = "-0.2 -0.325 -0.3";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "-15 0 85" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = L4D2_FNSCARItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = true;
   scopingImage = L4D2_FNSCARImage;
   safetyImage = L4D2_FNSCARImage;
   doColorShift = true;
   colorShiftColor = L4D2_FNSCARItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function L4D2_FNSCARSafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function L4D2_FNSCARSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function L4D2_FNSCARSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}

// EQUIP IMAGE THING

datablock ShapeBaseImageData(L4D2_FNSCAREquipImage)
{
   shapeFile = "./FNSCAR/FNSCAR.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = L4D2_FNSCARItem;
   sourceImage = L4D2_FNSCARImage;
   ammo = " ";
   melee = false;
   armReady = true;
   hideHands = true;
   doColorShift = true;
   colorShiftColor = L4D2_FNSCARItem.colorShiftColor;
   
	isSafetyImage = true;

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.7;
	stateTransitionOnTimeout[0]       	= "Done";
	stateSound[0]				= L4D2_FNSCAREquipSound;
	stateSequence[0]			= "use";
	
	stateName[1]                     	= "Done";
	stateScript[1]				= "onDone";

};

function L4D2_FNSCAREquipImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function L4D2_FNSCAREquipImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);	
}

function L4D2_FNSCAREquipImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.sourceImage, 0);
}
