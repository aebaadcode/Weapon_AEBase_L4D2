//audio
datablock AudioProfile(DeagleFireSound)
{
   filename    = "./Deagle_fire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(DeagleUseSound)
{
   filename    = "./Deagle_Equip.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(DeagleClipInSound)
{
   filename    = "./P200_ClipIn.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(DeagleClipOutSound)
{
   filename    = "./P200_ClipOut.wav";
   description = AudioClose3d;
   preload = true;
};

AddDamageType("Deagle",   '<bitmap:add-ons/Weapon_P200/CI_Pistol> %1',    '%2 <bitmap:add-ons/Weapon_P200/CI_Pistol> %1',0.75,1);
datablock ProjectileData(DeagleProjectile)
{
   projectileShapeName = "add-ons/Weapon_Gun/bullet.dts";
   directDamage        = 40;
   directDamageType    = $DamageType::Deagle;
   radiusDamageType    = $DamageType::Deagle;

   brickExplosionRadius = 0;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 14;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 22;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 190;
   verticalImpulse	  = 80;
   explosion           = gunExplosion;

   muzzleVelocity      = 125;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 1000;
   fadeDelay           = 700;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.10;
   isBallistic         = true;
   gravityMod = 0.2;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

//////////
// item //
//////////
datablock ItemData(DeagleItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./DeaglePU.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Deagle";
	iconName = "./UI_Deagle";
	doColorShift = true;
	colorShiftColor = "0.7 0.7 0.72 1.000";

	 // Dynamic properties defined by the scripts
	image = DeagleImage;
	canDrop = true;
	
	//Ammo Guns Parameters
	maxAmmo = 8;
	canReload = 1;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(DeagleImage)
{

   // Basic Item properties
   shapeFile = "./Deagle.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = 0; //"0.7 1.2 -0.5";
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
   item = DeagleItem;
   ammo = " ";
   projectile = DeagleProjectile;
   projectileType = Projectile;

   casing = GunShellDebris;
   shellExitDir        = "1.0 0.1 1.0";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 10.0;	
   shellVelocity       = 5.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;


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
	stateSound[0]				= DeagleUseSound;

	stateName[1]                    	= "Ready";
	stateTransitionOnNoAmmo[1]		= "ReloadStart";
	stateTransitionOnTriggerDown[1] 	= "Fire";
	stateAllowImageChange[1]        	= true;
	stateSequence[1]			= "ready";

	stateName[2]                    	= "Fire";
	stateTransitionOnTimeout[2]     	= "Smoke";
	stateTimeoutValue[2]            	= 0.05;
	stateFire[2]                    	= true;
	stateAllowImageChange[2]        	= false;
	stateScript[2]                  	= "onFire";
	stateSequence[2]			= "Fire";
	stateWaitForTimeout[2]			= true;
	stateEmitter[2]				= gunFlashEmitter;
	stateEmitterTime[2]			= 0.05;
	stateEmitterNode[2]			= "muzzleNode";
	stateSound[2]				= DeaglefireSound;

	stateName[3] 				= "Smoke";
	stateEmitter[3]				= gunSmokeEmitter;
	stateEmitterTime[3]			= 0.1;
	stateEmitterNode[3]			= "muzzleNode";
	stateTimeoutValue[3]            	= 0.01;
	stateTransitionOnTimeout[3]     	= "Wait";

	stateName[4]				= "Wait";
	stateTimeoutValue[4]			= 0.375;
	stateTransitionOnTimeout[4]		= "LoadCheckA";
	
	//Torque switches states instantly if there is an ammo/noammo state, regardless of stateWaitForTimeout
	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "onLoadCheck";
	stateTimeoutValue[5]			= 0.01;
	stateTransitionOnTimeout[5]		= "LoadCheckB";
	
	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNoAmmo[6]		= "ReloadWait";
	
	stateName[7]				= "ReloadWait";
	stateTimeoutValue[7]			= 0.3;
	stateScript[7]				= "";
	stateTransitionOnTimeout[7]		= "ReloadStart";
	stateWaitForTimeout[7]			= true;
	
	stateName[8]				= "ReloadStart";
	stateTimeoutValue[8]			= 0.9;
	stateScript[8]				= "onReloadStart";
	stateTransitionOnTimeout[8]		= "Reloaded";
	stateWaitForTimeout[8]			= true;
	stateSound[8]				= DeagleClipOutSound;
	stateSequence[8]			= "reload";
	
	stateName[9]				= "Reloaded";
	stateTimeoutValue[9]			= 0.9;
	stateScript[9]				= "onReloaded";
	stateTransitionOnTimeout[9]		= "Ready";
	stateSound[9]				= DeagleClipInSound;
	stateSequence[9]			= "reload2";
};

function DeagleImage::onFire(%this,%obj,%slot)
{
	
	%obj.playThread(2, shiftAway);

	%obj.toolAmmo[%obj.currTool]--;

	%projectile = %this.projectile;
	if(vectorLen(%obj.getVelocity()) > 0.1)
	{
		%spread = 0.002;
	}
	else
	{
		%spread = 0;
	}

	%vector = %obj.getMuzzleVector(%slot);
	%objectVelocity = %obj.getVelocity();
	%vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
	%vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
	%velocity = VectorAdd(%vector1,%vector2);
	%x = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
	%y = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
	%z = (getRandom() - 0.5) * 10 * 3.1415926 * %spread;
	%mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
	%velocity = MatrixMulVector(%mat, %velocity);
	
	%p = new (%this.projectileType)()
	{
		dataBlock = %projectile;
		initialVelocity = %velocity;
		initialPosition = %obj.getMuzzlePoint(%slot);
		sourceObject = %obj;
		sourceSlot = %slot;
		client = %obj.client;
	};
}

function DeagleImage::onReloadStart(%this,%obj,%slot)
{
	%obj.toolAmmo[%obj.currTool] = 0;
}

function DeagleImage::onReloaded(%this,%obj,%slot)
{
	
	%obj.toolAmmo[%obj.currTool] = %this.item.maxAmmo;
	%obj.setImageAmmo(%slot,1);
}

function DeagleImage::onMount(%this, %obj, %slot)
{	
	%obj.hidenode("RHand");
        %obj.hidenode("LHand");
}


function DeagleImage::onUnMount(%this, %obj, %slot)
{	
	%obj.unhidenode("RHand");
        %obj.unhidenode("LHand");
}
