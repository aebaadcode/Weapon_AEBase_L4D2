//audio
datablock AudioProfile(ChromeShotgunFireSound)
{
   filename    = "./Chrome_shotgun_fire.wav";
   description = AudioClose3d;
   preload = true;
};

AddDamageType("ChromeShotgun",   '<bitmap:add-ons/Weapon_Shotguns/CI_CShotgun> %1',    '%2 <bitmap:add-ons/Weapon_Shotguns/CI_CShotgun> %1',0.75,1);
datablock ProjectileData(ChromeShotgunProjectile)
{
   projectileShapeName = "add-ons/Weapon_Gun/bullet.dts";
   directDamage        = 15;
   directDamageType    = $DamageType::ChromeShotgun;
   radiusDamageType    = $DamageType::ChromeShotgun;

   brickExplosionRadius = 0.2;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 15;
   brickExplosionMaxVolume = 20;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 30;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 200;
   verticalImpulse     = 100;
   explosion           = gunExplosion;

   muzzleVelocity      = 75;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 4000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = true;
   gravityMod = 0.4;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

datablock ProjectileData(ShotgunBlastProjectile : ChromeShotgunProjectile)
{
   projectileShapeName = "add-ons/Vehicle_Tank/tankbullet.dts";
   directDamage        = 30;
   directDamageType    = $DamageType::ChromeShotgun;
   radiusDamageType    = $DamageType::ChromeShotgun;

   brickExplosionRadius = 0.4;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 30;
   brickExplosionMaxVolume = 25;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 35;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 300;
   verticalImpulse     = 100;
   explosion           = gunExplosion;

   muzzleVelocity      = 100;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 70;
   fadeDelay           = 0;
   isBallistic         = true;
   gravityMod = 0.0;
};


//////////
// item //
//////////
datablock ItemData(ChromeShotgunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./CShotgunPU.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Chrome Shotgun";
	iconName = "./UI_CShotgun";
	doColorShift = true;
	colorShiftColor = "0.3 0.3 0.31 1.000";

	 // Dynamic properties defined by the scripts
	image = ChromeShotgunImage;
	canDrop = true;
	
	maxAmmo = 8;
	canReload = 1;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(ChromeShotgunImage)
{
   // Basic Item properties
   shapeFile = "./Chrome_Shotgun.dts";
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
   item = ChromeShotgunItem;
   ammo = " ";
   projectile = ChromeShotgunProjectile;
   projectileType = Projectile;

   casing = ShotgunShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;
   minShotTime = 1000;

   doColorShift = false;
   colorShiftColor = ChromeShotgunItem.colorShiftColor;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.67;
	stateSequence[0]			 = "Use";
	stateTransitionOnTimeout[0]     	= "LoadCheckA";
	stateSound[0]			  	= HuntingShotgunUseSound;

	stateName[1]                    	= "Ready";
	stateTransitionOnTriggerDown[1] 	= "Fire";
	stateTransitionOnNoAmmo[1]		= "ReloadCheckA";
	stateAllowImageChange[1]		= true;
	stateSequence[1]			= "ready";

	stateName[2]                    	= "Fire";
	stateTransitionOnTimeout[2]     	= "Smoke";
	stateTimeoutValue[2]            	= 0.1;
	stateFire[2]                    	= true;
	stateAllowImageChange[2]        	= false;
	stateSequence[2]			= "Fire";
	stateScript[2]                  	= "onFire";
	stateWaitForTimeout[2]		  	= true;
	stateEmitter[2]			  	= gunFlashEmitter;
	stateEmitterTime[2]		  	= 0.05;
	stateEmitterNode[2]		  	= "muzzleNode";
	stateSound[2]			  	= ChromeShotgunfireSound;

	stateName[3] 			  	= "Smoke";
	stateEmitter[3]			  	= gunSmokeEmitter;
	stateEmitterTime[3]		  	= 0.1;
	stateEmitterNode[3]		  	= "muzzleNode";
	stateTimeoutValue[3]            	= 0.2;
	stateTransitionOnTimeout[3]     	= "Eject";

	stateName[4]			  	= "Eject";
	stateTimeoutValue[4]		  	= 0.7;
	stateTransitionOnTimeout[4]	  	= "LoadCheckA";
	stateWaitForTimeout[4]		  	= true;
	stateEjectShell[4]       	  	= true;
	stateSequence[4]			= "Pump";
	stateSound[4]			  	= HuntingShotgunReloadSound;
	stateScript[4]                  	= "onEject";
	
	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "onLoadCheck";
	stateTimeoutValue[5]			= 0.01;
	stateTransitionOnTimeout[5]		= "LoadCheckB";
						
	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNoAmmo[6]		= "ForceReload";
	
	stateName[7]				= "ReloadCheckA";
	stateScript[7]				= "onReloadCheck";
	stateTimeoutValue[7]			= 0.01;
	stateTransitionOnTimeout[7]		= "ReloadCheckB";
						
	stateName[8]				= "ReloadCheckB";
	stateTransitionOnAmmo[8]		= "CompleteReload";
	stateTransitionOnNoAmmo[8]		= "Reload";
						
	stateName[9]				= "ForceReload";
	stateTransitionOnTimeout[9]     	= "ForceReloaded";
	stateTimeoutValue[9]			= 0.53;
	stateSequence[9]			= "Reload";
	stateSound[9]				= HuntingShotgunShellInSound;
	stateScript[9]				= "onReloadStart";
	
	stateName[10]				= "ForceReloaded";
	stateTransitionOnTimeout[10]     	= "ReloadCheckA";
	stateTimeoutValue[10]			= 0.2;
	stateScript[10]				= "onReloaded";
	
	stateName[11]				= "Reload";
	stateTransitionOnTimeout[11]     	= "Reloaded";
	stateTransitionOnTriggerDown[11]	= "Fire";
	stateWaitForTimeout[11]			= false;
	stateTimeoutValue[11]			= 0.53;
	stateSequence[11]			= "Reload";
	stateSound[11]				= HuntingShotgunShellInSound;
	stateScript[11]				= "onReloadStart";
	
	stateName[12]				= "Reloaded";
	stateTransitionOnTimeout[12]     	= "ReloadCheckA";
	stateTransitionOnTriggerDown[12]	= "Fire";
	stateWaitForTimeout[12]			= false;
	stateTimeoutValue[12]			= 0.2;
	stateScript[12]				= "onReloaded";

	stateName[13]			  	= "CompleteReload";
	stateTimeoutValue[13]		  	= 0.7;
	stateTransitionOnTimeout[13]	  	= "Ready";
	stateWaitForTimeout[13]		  	= true;
	stateSequence[13]			= "Pump";
	stateSound[13]			  	= HuntingShotgunReloadSound;
	stateScript[13]                  	= "onEject";
};

function ChromeShotgunImage::onFire(%this,%obj,%slot)
{
  %fvec = %obj.getForwardVector();
  %fX = getWord(%fvec,0);
  %fY = getWord(%fvec,1);
  
  %evec = %obj.getEyeVector();
  %eX = getWord(%evec,0);
  %eY = getWord(%evec,1);
  %eZ = getWord(%evec,2);
  
  %eXY = mSqrt(%eX*%eX+%eY*%eY);
  
  %aimVec = %fX*%eXY SPC %fY*%eXY SPC %eZ;

	%obj.playThread(2, activate);
	%obj.toolAmmo[%obj.currTool]--;

	%projectile = %this.projectile;
	%spread = 0.0008;
	%shellcount = 6;

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
	
	//shotgun blast projectile: only effective at point blank, sends targets flying off into the distance
	//
	//more or less represents the concussion blast. i can only assume such a thing exists because
	// i've never stood infront of a fucking shotgun before
	///////////////////////////////////////////////////////////

	%projectile = "shotgunBlastProjectile";
	
	%vector = %obj.getMuzzleVector(%slot);
	%objectVelocity = %obj.getVelocity();
	%vector1 = VectorScale(%vector, %projectile.muzzleVelocity);
	%vector2 = VectorScale(%objectVelocity, %projectile.velInheritFactor);
	%velocity = VectorAdd(%vector1,%vector2);
	
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
	return %p;
}
function ChromeShotgunImage::onMount(%this, %obj, %slot)
{	
	%obj.hidenode("RHand");
        %obj.hidenode("LHand");
}


function ChromeShotgunImage::onUnMount(%this, %obj, %slot)
{	
	%obj.unhidenode("RHand");
        %obj.unhidenode("LHand");
}


function ChromeShotgunImage::onReloadCheck(%this,%obj,%slot)
{
	if(%obj.toolAmmo[%obj.currTool] < %this.item.maxAmmo && %this.item.maxAmmo > 0 && %obj.getState() !$= "Dead")
		%obj.setImageAmmo(%slot,0);
	else
		%obj.setImageAmmo(%slot,1);
}

function ChromeShotgunImage::onReloaded(%this,%obj,%slot)
{
	%obj.toolAmmo[%obj.currTool]++;
}