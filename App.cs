using StereoKit;

// Initialize StereoKit
SKSettings settings = new SKSettings
{
    appName = "SK_NET9_UWP",
    assetsFolder = "Assets",
};
if (args.Contains("--sim")) settings.mode = AppMode.Simulator;
if (!SK.Initialize(settings))
    return;

// Create assets used by the app
Pose cubePose = new Pose(0, 0, -0.5f);
Model cube = Model.FromMesh(
    Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
    Material.UI);

Matrix floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
Material floorMaterial = new Material("floor.hlsl");
floorMaterial.Transparency = Transparency.Blend;
string message = $"Hello from .NET {Environment.Version}";

// Core application loop
SK.Run(() =>
{
    if (Device.DisplayBlend == DisplayBlend.Opaque)
        Mesh.Cube.Draw(floorMaterial, floorTransform);

    UI.Handle("Cube", ref cubePose, cube.Bounds);
    cube.Draw(cubePose.ToMatrix());

    Vec3 textPos = cubePose.position + Vec3.UnitY * 0.1f;
    Text.Add(message, Matrix.TR(textPos, Quat.LookAt(textPos, Input.Head.position)));
});
