//audio
datablock AudioProfile(GlockFireSound)
{
   filename    = "./Glock_Fire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(GlockUseSound)
{
   filename    = "./Glock_Deploy.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(GlockClipOutSound)
{
   filename    = "./Glock_ClipOut.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(GlockClipInSound)
{
   filename    = "./Glock_ClipIn.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(P200FireSound)
{
   filename    = "./P200_fire.wav";
   description = AudioClose3d;
   preload = true;
};

//////////
// item //
//////////
datablock ItemData(AkimboHandgunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "Add-Ons/Weapon_P200/Glock.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "Dual Handguns";
	iconName = "./UI_DualPistol";
	doColorShift = true;
	colorShiftColor = "0.7 0.7 0.74 1.000";

	 // Dynamic properties defined by the scripts
	image = AkimboHandgunImage;
	canDrop = true;
	
	//Ammo Guns Parameters
	maxAmmo = 30;
	canReload = 0;
};

////////////////
//weapon image//
////////////////
AddDamageType("AkimboHandgun",   '<bitmap:add-ons/Weapon_P200/CI_DualPistol> %1',    '%2 <bitmap:add-ons/Weapon_P200/CI_DualPistol> %1',0.75,1);
datablock ShapeBaseImageData(AkimboHandgunImage)
{
   // Basic Item properties
	shapeFile = "Add-Ons/Weapon_P200/P200.dts";
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
   item = AkimboHandgunItem;
   ammo = " ";
   projectile = p200Projectile;
   projectileType = Projectile;

	casing = gunShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = true;

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.15;
	stateSequence[0]			= "use";
	stateTransitionOnTimeout[0]     	= "LoadCheckA";
	stateSound[0]			  	= GlockUseSound;

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
	stateEjectShell[2]       		= true;
	stateSequence[2]			= "Fire";
	stateWaitForTimeout[2]		  	= true;
	stateEmitter[2]			  	= gunFlashEmitter;
	stateEmitterTime[2]		  	= 0.05;
	stateEmitterNode[2]		  	= "muzzleNode";
	stateSound[2]			  	= P200fireSound;

	stateName[3] 			  	= "Smoke";
	stateEmitter[3]			  	= gunSmokeEmitter;
	stateEmitterTime[3]		  	= 0.1;
	stateEmitterNode[3]		  	= "muzzleNode";
	stateTimeoutValue[3]            	= 0.01;
	stateTransitionOnTimeout[3]     	= "Wait";
	
	stateName[4]			  	= "Wait";
	stateTimeoutValue[4]		  	= 0.045;
	stateTransitionOnTimeout[4]	  	= "AkimboLoadCheckA";

	stateName[5]			  	= "HandgunsAkimbo";
	stateTimeoutValue[5]		  	= 0.10;
	stateScript[5] 				= "onFireAkimbo";
	stateTransitionOnTimeout[5]	  	= "LoadCheckA";
	
	stateName[6]				= "LoadCheckA";
	stateScript[6]				= "onLoadCheck";
	stateTimeoutValue[6]			= 0.01;
	stateTransitionOnTimeout[6]		= "LoadCheckB";
	
	stateName[7]				= "LoadCheckB";
	stateTransitionOnAmmo[7]		= "Ready";
	stateTransitionOnNoAmmo[7]		= "ReloadWait";
	
	stateName[8]				= "AkimboLoadCheckA";
	stateScript[8]				= "onLoadCheck";
	stateTimeoutValue[8]			= 0.01;
	stateTransitionOnTimeout[8]		= "AkimboLoadCheckB";
	
	stateName[9]				= "AkimboLoadCheckB";
	stateTransitionOnAmmo[9]		= "HandgunsAkimbo";
	stateTransitionOnNoAmmo[9]		= "ReloadWait";
	
	stateName[10]				= "ReloadWait";
	stateTimeoutValue[10]			= 0.3;
	stateScript[10]				= "";
	stateTransitionOnTimeout[10]		= "ReloadStart";
	stateWaitForTimeout[10]			= true;
	
	stateName[11]				= "ReloadStart";
	stateTimeoutValue[11]			= 0.6;
	stateScript[11]				= "onReloadStart";
	stateTransitionOnTimeout[11]		= "ReloadNext";
	stateWaitForTimeout[11]			= true;
	stateSound[11]				= GlockClipOutSound;
	stateSequence[11]			= "reload1";
	
	stateName[13]				= "ReloadNext";
	stateTimeoutValue[13]			= 0.9;
	stateScript[13]				= "onReloadNext";
	stateTransitionOnTimeout[13]		= "Reloaded";
	stateSound[13]				= GlockClipInSound;
	stateSequence[13]			= "reload2";
	
	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.9;
	stateScript[14]				= "onReloaded";
	stateTransitionOnTimeout[14]		= "Ready";
};

datablock ShapeBaseImageData(LeftHandedHandgunImage)
{
   // Basic Item properties
shapeFile = "Add-Ons/Weapon_P200/Glock.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 1;
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
   item = AkimboHandgunItem;
   ammo = " ";
   projectile = gunProjectile;
   projectileType = Projectile;

	casing = gunShellDebris;
	shellExitDir        = "1.0 -1.3 1.0";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 15.0;	
	shellVelocity       = 7.0;

   //melee particles shoot from eye node for consistancy
   melee = false;
   //raise your arm up or not
   armReady = false;

   doColorShift = true;
   colorShiftColor = AkimboHandgunItem.colorShiftColor;//"0.400 0.196 0 1.000";

   //casing = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.

   // Initial start up state
	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.15;
	stateSequence[0]			= "use";
	stateTransitionOnTimeout[0]     	= "LoadCheckA";
	stateSound[0]			  	= GlockUseSound;

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
	stateEjectShell[2]       		= true;
	stateSequence[2]			= "Fire";
	stateWaitForTimeout[2]		  	= true;
	stateEmitter[2]			  	= gunFlashEmitter;
	stateEmitterTime[2]		  	= 0.05;
	stateEmitterNode[2]		  	= "muzzleNode";
	stateSound[2]			  	= P200fireSound;

	stateName[3] 			  	= "Smoke";
	stateEmitter[3]			  	= gunSmokeEmitter;
	stateEmitterTime[3]		  	= 0.1;
	stateEmitterNode[3]		  	= "muzzleNode";
	stateTimeoutValue[3]            	= 0.01;
	stateTransitionOnTimeout[3]     	= "Wait";
	
	stateName[4]			  	= "Wait";
	stateTimeoutValue[4]		  	= 0.045;
	stateTransitionOnTimeout[4]	  	= "AkimboLoadCheckA";

	stateName[5]			  	= "HandgunsAkimbo";
	stateTimeoutValue[5]		  	= 0.10;
	stateScript[5] 				= "onFireAkimbo";
	stateTransitionOnTimeout[5]	  	= "LoadCheckA";
	
	stateName[6]				= "LoadCheckA";
	stateScript[6]				= "onLoadCheck";
	stateTimeoutValue[6]			= 0.01;
	stateTransitionOnTimeout[6]		= "LoadCheckB";
	
	stateName[7]				= "LoadCheckB";
	stateTransitionOnAmmo[7]		= "Ready";
	stateTransitionOnNoAmmo[7]		= "ReloadWait";
	
	stateName[8]				= "AkimboLoadCheckA";
	stateScript[8]				= "onLoadCheck";
	stateTimeoutValue[8]			= 0.01;
	stateTransitionOnTimeout[8]		= "AkimboLoadCheckB";
	
	stateName[9]				= "AkimboLoadCheckB";
	stateTransitionOnAmmo[9]		= "HandgunsAkimbo";
	stateTransitionOnNoAmmo[9]		= "ReloadWait";
	
	stateName[10]				= "ReloadWait";
	stateTimeoutValue[10]			= 0.3;
	stateScript[10]				= "";
	stateTransitionOnTimeout[10]		= "ReloadStart";
	stateWaitForTimeout[10]			= true;
	
	stateName[11]				= "ReloadStart";
	stateTimeoutValue[11]			= 0.6;
	stateScript[11]				= "onReloadStart";
	stateTransitionOnTimeout[11]		= "ReloadNext";
	stateWaitForTimeout[11]			= true;
	stateSound[11]				= GlockClipOutSound;
	stateSequence[11]			= "reload1";
	
	stateName[13]				= "ReloadNext";
	stateTimeoutValue[13]			= 0.9;
	stateScript[13]				= "onReloadNext";
	stateTransitionOnTimeout[13]		= "Reloaded";
	
	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.9;
	stateScript[14]				= "onReloaded";
	stateTransitionOnTimeout[14]		= "Ready";
	stateSound[14]				= GlockClipInSound;
	stateSequence[14]			= "reload2";
};

function AkimboHandgunImage::onMount(%this, %obj, %slot)
{
	Parent::onMount(%this, %obj, %slot);
	%obj.mountImage(LeftHandedHandgunImage, 1);
}

function AkimboHandgunImage::onUnMount(%this, %obj, %slot)
{
	Parent::onUnMount(%this, %obj, %slot);
	%obj.unMountImage(1);
}

function AkimboHandgunImage::onFire(%this,%obj,%slot)
{
	if(vectorLen(%obj.getVelocity()) < 0.1 && (getSimTime() - %obj.lastShotTime) > 1200)
	{
		%projectile = P200Projectile;
		%spread = 0.00001;
		%obj.playThread(2, plant);
	}
	else
	{
		%projectile = P200Projectile;
		%spread = 0.001;
		%obj.playThread(2, plant);
	}
	
	%obj.lastShotTime = getSimTime();

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

function AkimboHandgunImage::onFireAkimbo(%this,%obj,%slot)
{
	%obj.setImageTrigger(1,1);
}

function AkimboHandgunImage::onLoadCheck(%this,%obj,%slot)
{
	Parent::onLoadCheck(%this,%obj,%slot);
	%obj.setImageAmmo(1,%obj.getImageAmmo(0));
}

function LeftHandgunImage::onLoadCheck(%this,%obj,%slot)
{
	Parent::onLoadCheck(%this,%obj,%slot);
	%obj.setImageAmmo(1,%obj.getImageAmmo(0));
}

function AkimboHandgunImage::onReloadStart(%this,%obj,%slot)
{
	%obj.toolAmmo[%obj.currTool] = 0;
}

function LeftHandgunImage::onReloadStart(%this,%obj,%slot)
{
	%obj.toolAmmo[%obj.currTool] = 0;
}

//function AkimboHandgunImage::onReloadLeft(%this,%obj,%slot)
//{
//	%obj.playThread(3, leftRecoil);
//}

function AkimboHandgunImage::onReloaded(%this,%obj,%slot)
{
	
	%obj.toolAmmo[%obj.currTool] = %this.item.maxAmmo;
	%obj.setImageAmmo(%slot,1);
}

function LeftHandedHandgunImage::onReloaded(%this,%obj,%slot)
{
	
	%obj.toolAmmo[%obj.currTool] = %this.item.maxAmmo;
	%obj.setImageAmmo(%slot,1);
}

function LeftHandedHandgunImage::onMount(%this, %obj, %slot)
{
	Parent::onMount(%this, %obj, %slot);
	%obj.playThread(1, armreadyboth);
}

function LeftHandedHandgunImage::onUnMount(%this, %obj, %slot)
{
	Parent::onUnMount(%this, %obj, %slot);
}

function LeftHandedHandgunImage::onFire(%this, %obj, %slot)
{
	if(vectorLen(%obj.getVelocity()) < 0.1 && (getSimTime() - %obj.lastShotTime) > 1200)
	{
		%projectile = P200Projectile;
		%spread = 0.00001;
		%obj.playThread(2, plant);
	}
	else
	{
		%projectile = P200Projectile;
		%spread = 0.001;
		%obj.playThread(2, plant);
	}
	
	%obj.lastShotTime = getSimTime();

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