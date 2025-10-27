using Windows.Management.Deployment;

if (args.Length != 1)
{
    Console.WriteLine("Usage: AppRegister.exe <PackageFullName>");
    Console.WriteLine("Example: AppRegister.exe \"MyApp_1.0.0.0_x64__abc123def456\"");
    return 1;
}

string packageFullName = args[0];

try
{
    Console.WriteLine($"Attempting to register package: {packageFullName}");

    var packageManager = new Windows.Management.Deployment.PackageManager();

    // Register the package by full name
    var options = new RegisterPackageOptions();
    var deploymentResult = await packageManager.RegisterPackagesByFullNameAsync(
        [packageFullName], 
        options);

    if (deploymentResult.IsRegistered)
    {
        Console.WriteLine("✅ Package registered successfully!");
        return 0;
    }
    else
    {
        Console.WriteLine($"❌ Registration failed:");
        Console.WriteLine($"Error: {deploymentResult.ErrorText}");
        Console.WriteLine($"Extended Error Code: {deploymentResult.ExtendedErrorCode}");
        return 1;
    }
}
catch (Exception ex)
{
    Console.WriteLine($"❌ Exception occurred: {ex.Message}");
    return 1;
}
