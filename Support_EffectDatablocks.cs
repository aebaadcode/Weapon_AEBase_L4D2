//shell stuff

//9mm
datablock ParticleData(Doom_PistolShellParticle) {
	dragCoefficient      = 1;
	gravityCoefficient   = 2;
	inheritedVelFactor   = 1;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -100.0;
	spinRandomMax		= 100.0;
	//Relevant stuff:
	textureName		= "./Shells/pistolshell1"; //This automatically gets replaced with animTexName[0]
	animTexName[0]	= "./Shells/pistolshell1";
	animTexName[1]	= "./Shells/pistolshell2";
	animTexName[2]	= "./Shells/pistolshell3";
	animTexName[3]	= "./Shells/pistolshell4";
	animTexName[4]	= "./Shells/pistolshell5";
	animTexName[5]	= "./Shells/pistolshell6";
	animTexName[6]	= "./Shells/pistolshell7";
	animTexName[7]	= "./Shells/pistolshell8";
	animateTexture	= true;
	framesPerSec	= 60;
	colors[0]     = "1 1 1 1";
	colors[1]     = "1 1 1 1";
	sizes[0]      = 0.125;
	sizes[1]      = 0.125;

	useInvAlpha = true;
};

//BUUUULLLSHIEEET downloader stuff dont laugh at me i just want based animated shells ===((((((
datablock ParticleData(Doom_PistolShell2TextureDownloader : Doom_PistolShellParticle)
{
    textureName = "./Shells/pistolshell2";
};
datablock ParticleData(Doom_PistolShell3TextureDownloader : Doom_PistolShellParticle)
{
    textureName = "./Shells/pistolshell3";
};
datablock ParticleData(Doom_PistolShell4TextureDownloader : Doom_PistolShellParticle)
{
    textureName = "./Shells/pistolshell4";
};
datablock ParticleData(Doom_PistolShell5TextureDownloader : Doom_PistolShellParticle)
{
    textureName = "./Shells/pistolshell5";
};
datablock ParticleData(Doom_PistolShell6TextureDownloader : Doom_PistolShellParticle)
{
    textureName = "./Shells/pistolshell6";
};
datablock ParticleData(Doom_PistolShell7TextureDownloader : Doom_PistolShellParticle)
{
    textureName = "./Shells/pistolshell7";
};
datablock ParticleData(Doom_PistolShell8TextureDownloader : Doom_PistolShellParticle)
{
    textureName = "./Shells/pistolshell8";
};
// end of sowhjilgeurtawljgerknhaerhsgljknflsahdnkjef (torture) 

datablock ParticleEmitterData(Doom_PistolShellEmitter) {
   lifetimeMS = 1;
   ejectionPeriodMS = 25;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 0.1;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 15;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "Doom_PistolShellParticle";

   uiName = "";
};

datablock DebrisData(Doom_PistolShellDebris : AEPistolShellDebris)
{
	emitters = Doom_PistolShellEmitter;
	shapeFile = "base/data/shapes/empty.dts";
	staticOnMaxBounce = false;
};

// END OF SHELLS


// MAG shiet

datablock ParticleData(Doom_MagParticle) {
	dragCoefficient      = 1;
	gravityCoefficient   = 2;
	inheritedVelFactor   = 1;
	constantAcceleration = 0.0;
	lifetimeMS           = 1000;
	lifetimeVarianceMS   = 0;
	spinSpeed		= 10.0;
	spinRandomMin		= -100.0;
	spinRandomMax		= 100.0;
	//Relevant stuff:
	textureName		= "./Shells/Mag1"; //This automatically gets replaced with animTexName[0]
	animTexName[0]	= "./Shells/Mag1";
	animTexName[1]	= "./Shells/Mag2";
	animTexName[2]	= "./Shells/Mag3";
	animTexName[3]	= "./Shells/Mag4";
	animTexName[4]	= "./Shells/Mag5";
	animTexName[5]	= "./Shells/Mag6";
	animTexName[6]	= "./Shells/Mag7";
	animTexName[7]	= "./Shells/Mag8";
	animateTexture	= true;
	framesPerSec	= 30;
	colors[0]     = "1 1 1 1";
	colors[1]     = "1 1 1 1";
	sizes[0]      = 0.75;
	sizes[1]      = 0.75;

	useInvAlpha = true;
};

//BUUUULLLSHIEEET downloader stuff dont laugh at me i just want based animated mags ===((((((
datablock ParticleData(Doom_Mag2TextureDownloader : Doom_MagParticle)
{
    textureName = "./Shells/Mag2";
};
datablock ParticleData(Doom_Mag3TextureDownloader : Doom_MagParticle)
{
    textureName = "./Shells/Mag3";
};
datablock ParticleData(Doom_Mag4TextureDownloader : Doom_MagParticle)
{
    textureName = "./Shells/Mag4";
};
datablock ParticleData(Doom_Mag5TextureDownloader : Doom_MagParticle)
{
    textureName = "./Shells/Mag5";
};
datablock ParticleData(Doom_Mag6TextureDownloader : Doom_MagParticle)
{
    textureName = "./Shells/Mag6";
};
datablock ParticleData(Doom_Mag7TextureDownloader : Doom_MagParticle)
{
    textureName = "./Shells/Mag7";
};
datablock ParticleData(Doom_Mag8TextureDownloader : Doom_MagParticle)
{
    textureName = "./Shells/Mag8";
};
// end of sowhjilgeurtawljgerknhaerhsgljknflsahdnkjef (torture) 

datablock ParticleEmitterData(Doom_MagEmitter) {
   lifetimeMS = 1;
   ejectionPeriodMS = 25;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   velocityVariance = 0.1;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 15;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance = false;
   particles = "Doom_MagParticle";

   uiName = "";
};

datablock DebrisData(Doom_MagDebris : AEPistolShellDebris)
{
	emitters = Doom_MagEmitter;
	shapeFile = "base/data/shapes/empty.dts";
	staticOnMaxBounce = false;
};

// END OF MAG stuff

// ITEM SPRITES

datablock ParticleData(Doom_PistolSpriteParticle) {
	dragCoefficient      = 0;
	gravityCoefficient   = 0;
	inheritedVelFactor   = 0;
	constantAcceleration = 0.0;
	lifetimeMS           = 12;
	lifetimeVarianceMS   = 0;
	spinSpeed		= 0;
	spinRandomMin		= 0;
	spinRandomMax		= 0;
	//Relevant stuff:
	textureName		= "./ItemSprites/pistol"; 
	colors[0]     = "1 1 1 1";
	colors[1]     = "1 1 1 1";
	sizes[0]      = 1.25;
	sizes[1]      = 1.25;

	useInvAlpha = true;
};

datablock ParticleEmitterData(Doom_PistolSpriteEmitter) {
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 0;
   velocityVariance = 0;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 0;
   phiReferenceVel  = 0;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "Doom_PistolSpriteParticle";

   uiName = "doom pistol";
};