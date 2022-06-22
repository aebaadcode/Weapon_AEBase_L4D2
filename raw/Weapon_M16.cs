//audio
datablock AudioProfile(M16FireSound)
{
   filename    = "./M16_fire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(M16EquipSound)
{
   filename    = "./M16_Use.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(M16ClipInSound)
{
   filename    = "./M16_ClipInFULL.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(M16ClipOutSound)
{
   filename    = "./M16_ClipOut.wav";
   description = AudioClose3d;
   preload = true;
};

//OtherData
AddDamageType("M16",   '<bitmap:add-ons/Weapon_M16/ci_M16> %1',    '%2 <bitmap:add-ons/Weapon_M16/ci_M16> %1',0.75,1);
datablock ProjectileData(M16Projectile)
{
   projectileShapeName = "add-ons/Weapon_Gun/bullet.dts";
   directDamage        = 10;
   directDamageType    = $DamageType::M16;
   radiusDamageType    = $DamageType::M16;

   brickExplosionRadius = 0;
   brickExplosionImpact = true;          //destroy a brick if we hit it directly?
   brickExplosionForce  = 10;
   brickExplosionMaxVolume = 1;          //max volume of bricks that we can destroy
   brickExplosionMaxVolumeFloating = 2;  //max volume of bricks that we can destroy if they aren't connected to the ground

   impactImpulse	     = 80;
   verticalImpulse     = 40;
   explosion           = gunExplosion;

   muzzleVelocity      = 75;
   velInheritFactor    = 1;

   armingDelay         = 0;
   lifetime            = 4000;
   fadeDelay           = 3500;
   bounceElasticity    = 0.5;
   bounceFriction      = 0.20;
   isBallistic         = true;
   gravityMod = 0.2;

   hasLight    = false;
   lightRadius = 3.0;
   lightColor  = "0 0 0.5";
};

//////////
// item //
//////////
datablock ItemData(M16Item)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./M16PU.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "M16";
	iconName = "./UI_M16";
	doColorShift = true;
	colorShiftColor = "0.4 0.4 0.41 1.000";

	 // Dynamic properties defined by the scripts
	image = M16Image;
	canDrop = true;
	
	maxAmmo = 50;
	canReload = 1;
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(M16Image)
{
   // Basic Item properties
   shapeFile = "./M16.dts";
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
   item = M16Item;
   ammo = " ";
   projectile = M16Projectile;
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

   doColorShift = false;

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
   // Initial start up state

	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.7;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	StateSequence[0]			= "use";
	StateSound[0]				= "M16EquipSound";

	stateName[1]                     	= "Ready";
	stateTransitionOnNoAmmo[1]       	= "Reload";
	stateTransitionOnTriggerDown[1]  	= "Fire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                    	= "Fire";
	stateTransitionOnTimeout[2]     	= "Delay";
	stateTimeoutValue[2]            	= 0.00;
	stateFire[2]                    	= true;
	stateAllowImageChange[2]        	= false;
	stateSequence[2]                	= "Fire";
	stateScript[2]                  	= "onFire";
	stateEjectShell[2]       	  	= true;
	stateEmitter[2]				= gunFlashEmitter;
	stateEmitterTime[2]			= 0.05;
	stateEmitterNode[2]			= "muzzleNode";
	stateWaitForTimeout[2]			= true;
	StateSequence[2]			= "fire";
	stateSound[2]				= M16FireSound;

	stateName[3]				= "Delay";
	stateTransitionOnTimeout[3]     	= "FireLoadCheckA";
	stateTimeoutValue[3]            	= 0.06;
	stateEmitter[3]				= gunSmokeEmitter;
	stateEmitterTime[3]			= 0.06;
	stateEmitterNode[3]			= "muzzleNode";
	
	//Torque switches states instantly if there is an ammo/noammo state, regardless of stateWaitForTimeout

	stateName[4]				= "LoadCheckA";
	stateScript[4]				= "onLoadCheck";
	stateTimeoutValue[4]			= 0.01;
	stateTransitionOnTimeout[4]		= "LoadCheckB";
	
	stateName[5]				= "LoadCheckB";
	stateTransitionOnAmmo[5]		= "Ready";
	stateTransitionOnNoAmmo[5]		= "ReloadWait";
	
	stateName[6]				= "ReloadWait";
	stateTimeoutValue[6]			= 0.3;
	stateTransitionOnTimeout[6]		= "Reload";
	stateWaitForTimeout[6]			= true;
	
	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.4;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadB";
	stateWaitForTimeout[7]			= true;
	StateSequence[7]			= "reload1";
	stateSound[7]				= M16ClipOutSound;

	stateName[8]				= "ReloadB";
	stateTimeoutValue[8]			= 1.53;
	stateTransitionOnTimeout[8]		= "Reloaded";
	stateWaitForTimeout[8]			= true;
	StateSequence[8]			= "reload2";
	stateSound[8]				= M16ClipInSound;
	
	stateName[9]				= "Reloaded";
	stateTimeoutValue[9]			= 0.5;
	stateScript[9]				= "onReloaded";
	stateTransitionOnTimeout[9]		= "Ready";

	stateName[10] 				= "Smoke";
	stateEmitter[10]			= gunSmokeEmitter;
	stateEmitterTime[10]			= 0.3;
	stateEmitterNode[10]			= "muzzleNode";
	stateTimeoutValue[10]			= 0.2;
	stateTransitionOnTimeout[10]		= "Ready";
	stateTransitionOnTriggerDown[10]	= "Fire";
	
	stateName[11] 				= "ReloadSmoke";
	stateEmitter[11]			= gunSmokeEmitter;
	stateEmitterTime[11]			= 0.3;
	stateEmitterNode[11]			= "muzzleNode";
	stateTimeoutValue[11]			= 0.2;
	stateTransitionOnTimeout[11]		= "Reload";

	stateName[12]				= "FireLoadCheckA";
	stateScript[12]				= "onLoadCheck";
	stateTimeoutValue[12]			= 0.01;
	stateTransitionOnTimeout[12]		= "FireLoadCheckB";
	
	stateName[13]				= "FireLoadCheckB";
	stateTransitionOnAmmo[13]		= "Smoke";
	stateTransitionOnNoAmmo[13]		= "ReloadSmoke";
};

function M16Image::onFire(%this,%obj,%slot)
{
	if(vectorLen(%obj.getVelocity()) < 0.1 && (getSimTime() - %obj.lastShotTime) > 50)
	{
		%projectile = M16Projectile;
		%spread = 0.001;
	}
	else
	{
		%projectile = M16Projectile;
		%spread = 0.005;
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
function M16Image::onMount(%this, %obj, %slot)
{	
	%obj.hidenode("RHand");
        %obj.hidenode("LHand");
}


function M16Image::onUnMount(%this, %obj, %slot)
{	
	%obj.unhidenode("RHand");
        %obj.unhidenode("LHand");
}

function M16Image::onLoadCheck(%this,%obj,%slot)
{
	if(%obj.toolAmmo[%obj.currTool] <= 0)
		%obj.setImageAmmo(%slot,0);
	else
		%obj.setImageAmmo(%slot,1);
}

function M16Image::onReloaded(%this,%obj,%slot)
{
	%obj.toolAmmo[%obj.currTool] = 50;
	%obj.setImageAmmo(%slot,1);
}