datablock AudioProfile(L4D2_DeagleFireSound)
{
   filename    = "./Sounds/Fire/Deagle_fire.wav";
   description = HeavyClose3D;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(L4D2_DeagleItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./Deagle/DeaglePU.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "L4D2: Deagle";
	iconName = "./Icons/UI_Deagle";
	doColorShift = true;
	colorShiftColor = "0.4 0.4 0.41 1.000";

	 // Dynamic properties defined by the scripts
	image = L4D2_DeagleImage;
	canDrop = true;

	AEAmmo = 8;
	AEType = AE_HeavyPAmmoItem.getID();
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
datablock ShapeBaseImageData(L4D2_DeagleImage)
{
   // Basic Item properties
   shapeFile = "./Deagle/Deagle.dts";
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
   item = L4D2_DeagleItem;
   ammo = " ";
   projectile = AETrailedProjectile;
   projectileType = Projectile;

   casing = AEPistolShellDebris;
   shellExitDir        = "1.0 0.1 1.0";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 10.0;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = true;
	safetyImage = L4D2_DeagleSafetyImage;
//    scopingImage = L4D2_DeagleIronsightImage;
	doColorShift = true;
	colorShiftColor = L4D2_DeagleItem.colorShiftColor;//"0.400 0.196 0 1.000";

	shellSound = AEShellSMG;
	shellSoundMin = 450; //min delay for when the shell sound plays
	shellSoundMax = 550; //max delay for when the shell sound plays

   muzzleFlashScale = "1.5 1.5 1.5";
   bulletScale = "1 1 1";

   projectileDamage = 40;
   projectileCount = 1;
   projectileHeadshotMult = 2;
   projectileVelocity = 200;
   projectileTagStrength = 0.35;  // tagging strength
   projectileTagRecovery = 0.03; // tagging decay rate

   recoilHeight = 1;
   recoilWidth = 0;
   recoilWidthMax = 0;
   recoilHeightMax = 20;

   spreadBurst = 3; // how much shots it takes to trigger spread i think
   spreadBase = 1;
   spreadReset = 500; // m
   spreadMin = 1;
   spreadMax = 200;

   screenshakeMin = "0.25 0.25 0.25"; 
   screenshakeMax = "0.5 0.5 0.5"; 
   farShotSound = RevolverDistantSound;
   farShotDistance = 40;

		sonicWhizz = true;
		whizzSupersonic = true;
		whizzThrough = false;
		whizzDistance = 12;
		whizzChance = 100;
		whizzAngle = 80;
	staticTotalRange = 100;			

	projectileFalloffStart = $ae_falloffPistolLongStart;
	projectileFalloffEnd = $ae_falloffPistolLongEnd;
	projectileFalloffDamage = $ae_falloffLongPistol;
	
		flashlightDistance = 8;
		laserOffStates = "";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.
   // Initial start up state
	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.99;
	stateSequence[0]			= "Use";
	stateTransitionOnTimeout[0]     	= "LoadCheckA";
	stateSound[0]				= L4D2_DeagleUseSound;

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "ReloadWait";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateScript[2]                     = "AEOnFire";
	stateEmitter[2]					= AEBaseShotgunFlashEmitter;
	stateEmitterTime[2]				= 0.05;
	stateEmitterNode[2]				= "muzzlePoint";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateTimeoutValue[3]			= 0.001;
	stateEmitter[3]					= AEBaseSmokeBigEmitter;
	stateEmitterTime[3]				= 0.05;
	stateEmitterNode[3]				= "muzzlePoint";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[4]				= "LoadCheckA";
	stateScript[4]				= "AEMagLoadCheck";
	stateTimeoutValue[4]			= 0.1;
	stateTransitionOnTriggerUp[4]		= "LoadCheckB";
	
	stateName[5]				= "LoadCheckB";
	stateTransitionOnAmmo[5]		= "Ready";
	stateTransitionOnNotLoaded[5] = "Empty";
	stateTransitionOnNoAmmo[5]		= "ReloadWait";

	stateName[6]				= "Reload";
	stateTimeoutValue[6]			= 0.9;
	stateScript[6]				= "onReloadStart";
	stateTransitionOnTimeout[6]		= "ReloadB";
	stateWaitForTimeout[6]			= true;
	stateSequence[6]			= "reload";
	stateSound[6]				= L4D2_DeagleClipOutSound;
	
	stateName[7]				= "ReloadB";
	stateTimeoutValue[7]			= 0.9;
	stateTransitionOnTimeout[7]		= "Reloaded";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "reload2";
	stateSound[7]				= L4D2_DeagleClipInSound;
	
	stateName[8]				= "FireLoadCheckA";
	stateScript[8]				= "AEMagLoadCheck";
	stateTimeoutValue[8]			= 0.375;
	stateTransitionOnTimeout[8]		= "FireLoadCheckB";
	
	stateName[9]				= "FireLoadCheckB";
	stateTransitionOnAmmo[9]		= "Ready";
	stateTransitionOnNoAmmo[9]		= "ReloadWait";	
	stateTransitionOnNotLoaded[9]  = "Ready";
		
	stateName[10]				= "Reloaded";
	stateTimeoutValue[10]			= 0.1;
	stateScript[10]				= "AEMagReloadAll";
	stateTransitionOnTimeout[10]		= "Ready";
	
	stateName[11]				= "ReadyLoop";
	stateTransitionOnTimeout[11]		= "Ready";

	stateName[12]          = "Empty";
	stateTransitionOnTriggerDown[12]  = "Dryfire";
	stateTransitionOnLoaded[12] = "ReloadWait";
	stateScript[12]        = "AEOnEmpty";

	stateName[13]           = "Dryfire";
	stateTransitionOnTriggerUp[13] = "Empty";
	stateWaitForTimeout[13]    = false;
	stateScript[13]      = "onDryFire";
	
	stateName[14]				= "ReloadWait";
	stateTimeoutValue[14]			= 0.3;
	stateTransitionOnTimeout[14]		= "Reload";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function L4D2_DeagleImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(3); 
    %obj.playAudio(3, L4D2_DeagleFireSound);
	
	%obj.blockImageDismount = true;
	%obj.schedule(200, unBlockImageDismount);

	Parent::AEOnFire(%this, %obj, %slot);
}

function L4D2_DeagleImage::AEOnLowClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(0, shiftaway);
}

function L4D2_DeagleImage::AEOnMedClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(0, shiftaway);
   %obj.aeplayThread(3, jump);
}

function L4D2_DeagleImage::AEOnHighClimb(%this, %obj, %slot) 
{
   %obj.aeplayThread(0, shiftaway);
}

function L4D2_DeagleImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function L4D2_DeagleImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function L4D2_DeagleImage::onMount(%this,%obj,%slot)
{
	%obj.aeplayThread(2, plant);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function L4D2_DeagleImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(L4D2_DeagleSafetyImage)
{
   shapeFile = "./Deagle/Deagle.dts";
   emap = true;
   mountPoint = 0;
   offset = "-0.25 -0.325 -0.35";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "-15 0 85" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = L4D2_DeagleItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = true;
   scopingImage = L4D2_DeagleImage;
   safetyImage = L4D2_DeagleImage;
   doColorShift = true;
   colorShiftColor = L4D2_DeagleItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function L4D2_DeagleSafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function L4D2_DeagleSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function L4D2_DeagleSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	parent::onUnMount(%this,%obj,%slot);
}