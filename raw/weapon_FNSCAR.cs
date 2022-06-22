datablock AudioProfile(FNSCAR1Sound)
{
   filename    = "./FNSCAR_Fire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(FNSCARClipInSound)
{
   filename    = "./FNSCAR_ClipIn.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(FNSCARClipOutSound)
{
   filename    = "./FNSCAR_ClipOut.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(FNSCARUseSound)
{
   filename    = "./FNSCAR_Equip.wav";
   description = AudioClose3d;
   preload = true;
};

AddDamageType("FNSCAR",   '<bitmap:add-ons/Weapon_FNSCAR/CI_SCAR> %1',    '%2 <bitmap:add-ons/Weapon_FNSCAR/CI_SCAR> %1',0.2,1);
datablock ProjectileData(FNSCARProjectile)
{
   projectileShapeName = "add-ons/Weapon_Gun/bullet.dts";
   directDamage        = 10;
   directDamageType    = $DamageType::FNSCAR;
   radiusDamageType    = $DamageType::FNSCAR;

   brickExplosionRadius = 0;
   brickExplosionImpact = false;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 0;
   verticalImpulse	  = 0;
   explosion           = gunExplosion;

   muzzleVelocity      = 75;
   velInheritFactor    = 1;

   armingDelay         = 00;
   lifetime            = 4000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = true;
   gravityMod = 0.2;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";

   uiName = "Combat Rifle Bullet";
};

//////////
// item //
//////////
datablock ItemData(FNSCARItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./FNSCARPU.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "FN-Scar";
	iconName = "./UI_SCAR";
	doColorShift = true;
	colorShiftColor = "0.5 0.5 0.5 1.000";

	 // Dynamic properties defined by the scripts
	image = FNSCARImage;
	canDrop = true;

	maxAmmo = 50;
	canReload = 1;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(FNSCARImage)
{
   // Basic Item properties
   shapeFile = "./FNSCAR.dts";
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
   item = FNSCARItem;
   ammo = " ";
   projectile = FNSCARProjectile;
   projectileType = Projectile;

	casing = gunShellDebris;
	shellExitDir        = "2.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   doColorShift = true;
   colorShiftColor = FNSCARItem.colorShiftColor;//"0.400 0.196 0 1.000";

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
	stateSound[0]				= FNSCARUseSound;
	stateSequence[0]			= "use";

	stateName[1]                     	= "Ready";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "Fire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                    	= "Fire";
	stateTransitionOnTimeout[2]     	= "Fire2";
	stateTimeoutValue[2]            	= 0.06;
	stateFire[2]                    	= true;
	stateAllowImageChange[2]        	= false;
	stateSequence[2]                	= "Fire";
	stateScript[2]                  	= "onFire";
	stateEjectShell[2]       	  	= true;
	stateEmitter[2]				= gunFlashEmitter;
	stateEmitterTime[2]			= 0.05;
	stateEmitterNode[2]			= "muzzleNode";
	stateWaitForTimeout[2]			= true;
	stateSound[2]				= FNSCAR1Sound;

	stateName[3]                    	= "Fire2";
	stateTransitionOnTimeout[3]     	= "Fire3";
	stateTimeoutValue[3]            	= 0.06;
	stateFire[3]                    	= true;
	stateAllowImageChange[3]        	= false;
	stateSequence[3]                	= "Fire";
	stateScript[3]                  	= "onFire";
	stateEjectShell[3]       	  	= true;
	stateEmitter[3]				= gunFlashEmitter;
	stateEmitterTime[3]			= 0.05;
	stateEmitterNode[3]			= "muzzleNode";
	stateWaitForTimeout[3]			= true;
	stateSound[3]				= FNSCAR1Sound;

	stateName[4]                    	= "Fire3";
	stateTransitionOnTimeout[4]     	= "Fire4";
	stateTimeoutValue[4]            	= 0.06;
	stateFire[4]                    	= true;
	stateAllowImageChange[4]        	= false;
	stateSequence[4]                	= "Fire";
	stateScript[4]                  	= "onFire";
	stateEjectShell[4]       	  	= true;
	stateEmitter[4]				= gunFlashEmitter;
	stateEmitterTime[4]			= 0.05;
	stateEmitterNode[4]			= "muzzleNode";
	stateWaitForTimeout[4]			= true;
	stateSound[4]				= FNSCAR1Sound;

	stateName[5]                    	= "Fire4";
	stateTransitionOnTimeout[5]     	= "Fire5";
	stateTimeoutValue[5]            	= 0.06;
	stateFire[5]                    	= true;
	stateAllowImageChange[5]        	= false;
	stateSequence[5]                	= "Fire";
	stateScript[5]                  	= "onFire";
	stateEjectShell[5]       	  	= true;
	stateEmitter[5]				= gunFlashEmitter;
	stateEmitterTime[5]			= 0.05;
	stateEmitterNode[5]			= "muzzleNode";
	stateWaitForTimeout[5]			= true;
	stateSound[5]				= FNSCAR1Sound;

	stateName[6]                    	= "Fire5";
	stateTransitionOnTimeout[6]     	= "Delay";
	stateTimeoutValue[6]            	= 0.06;
	stateFire[6]                    	= true;
	stateAllowImageChange[6]        	= false;
	stateSequence[6]                	= "Fire";
	stateScript[6]                  	= "onFire";
	stateEjectShell[6]       	  	= true;
	stateEmitter[6]				= gunFlashEmitter;
	stateEmitterTime[6]			= 0.05;
	stateEmitterNode[6]			= "muzzleNode";
	stateWaitForTimeout[6]			= true;
	stateSound[6]				= FNSCAR1Sound;

	stateName[7]				= "Delay";
	stateTransitionOnTimeout[7]     	= "FireLoadCheckA";
	stateTimeoutValue[7]            	= 0.08;
	stateEmitter[7]				= gunSmokeEmitter;
	stateEmitterTime[7]			= 0.07;
	stateEmitterNode[7]			= "muzzleNode";
	
	stateName[8]				= "LoadCheckA";
	stateScript[8]				= "onLoadCheck";
	stateTimeoutValue[8]			= 0.01;
	stateTransitionOnTimeout[8]		= "LoadCheckB";
	
	stateName[9]				= "LoadCheckB";
	stateTransitionOnAmmo[9]		= "Ready";
	stateTransitionOnNoAmmo[9]		= "Reload";

	stateName[10]				= "Reload";
	stateTimeoutValue[10]			= 0.96;
	stateScript[10]				= "onReloadStart";
	stateTransitionOnTimeout[10]		= "Wait";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "reload1";
	stateSound[10]				= FNSCARClipOutSound;
	
	stateName[11]				= "Wait";
	stateTimeoutValue[11]			= 1.31;
	stateSound[11]				= FNSCARClipInSound;
	stateScript[11]				= "onReloadWait";
	stateSequence[11]			= "reload2";
	stateTransitionOnTimeout[11]		= "Reloaded";
	
	stateName[12]				= "FireLoadCheckA";
	stateScript[12]				= "onLoadCheck";
	stateTimeoutValue[12]			= 0.01;
	stateTransitionOnTimeout[12]		= "FireLoadCheckB";
	
	stateName[13]				= "FireLoadCheckB";
	stateTransitionOnAmmo[13]		= "Smoke";
	stateTransitionOnNoAmmo[13]		= "ReloadSmoke";
	
	stateName[14] 				= "Smoke";
	stateEmitter[14]			= gunSmokeEmitter;
	stateEmitterTime[14]			= 0.3;
	stateEmitterNode[14]			= "muzzleNode";
	stateTimeoutValue[14]			= 0.4;
	stateTransitionOnTimeout[14]		= "Ready";
	stateTransitionOnTriggerDown[14]	= "Fire";
	
	stateName[15] 				= "ReloadSmoke";
	stateEmitter[15]			= gunSmokeEmitter;
	stateEmitterTime[15]			= 0.3;
	stateEmitterNode[15]			= "muzzleNode";
	stateTimeoutValue[15]			= 0.2;
	stateTransitionOnTimeout[15]		= "Reload";
	
	stateName[16]				= "Reloaded";
	stateTimeoutValue[16]			= 0.01;
	stateScript[16]				= "onReloaded";
	stateTransitionOnTimeout[16]		= "Ready";
};

function FNSCARImage::onFire(%this,%obj,%slot)
{
	%projectile = FNSCARProjectile;

	if(vectorLen(%obj.getVelocity()) < 0.1)
	{
		%spread = 0.00025;
	}
	else
	{
		%spread = 0.001;
	}
	
	%obj.lastShotTime = getSimTime();
	%shellcount = 1;

	%obj.playThread(2, plant);
	%shellcount = 1;
	%obj.toolAmmo[%obj.currTool]--;

	for(%shell=0; %shell<%shellcount; %shell++)
	{
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
		MissionCleanup.add(%p);
	}
	return %p;
}


function FNSCARImage::onMount(%this, %obj, %slot)
{	
	%obj.hidenode("RHand");
        %obj.hidenode("LHand");
}


function FNSCARImage::onUnMount(%this, %obj, %slot)
{	
	%obj.unhidenode("RHand");
        %obj.unhidenode("LHand");
}

function FNSCARImage::onLoadCheck(%this,%obj,%slot)
{
	if(%obj.toolAmmo[%obj.currTool] <= 0)
		%obj.setImageAmmo(%slot,0);
	else
		%obj.setImageAmmo(%slot,1);
}

function FNSCARImage::onReloadStart(%this,%obj,%slot)
{
	%obj.toolAmmo[%obj.currTool] = 0;
}

function FNSCARImage::onReloaded(%this,%obj,%slot)
{
	%obj.toolAmmo[%obj.currTool] = 50;
	%obj.setImageAmmo(%slot,1);
}