using Windows.Management.Deployment;

bool useSingularMethod = false;
string packageFullName;

// Parse command line arguments
if (args.Length == 1)
{
    packageFullName = args[0];
}
else if (args.Length == 2 && (args[0] == "--singular" || args[0] == "-s"))
{
    useSingularMethod = true;
    packageFullName = args[1];
}
else
{
    Console.WriteLine("Usage: AppRegister.exe [--singular|-s] <PackageFullName>");
    Console.WriteLine("  --singular, -s: Use RegisterPackageByFullNameAsync (singular) instead of RegisterPackagesByFullNameAsync (plural)");
    Console.WriteLine("Example: AppRegister.exe \"MyApp_1.0.0.0_x64__abc123def456\"");
    Console.WriteLine("Example: AppRegister.exe --singular \"MyApp_1.0.0.0_x64__abc123def456\"");
    return 1;
}

try
{
    Console.WriteLine($"Attempting to register package: {packageFullName}");
    Console.WriteLine($"Using method: {(useSingularMethod ? "RegisterPackageByFullNameAsync (singular)" : "RegisterPackagesByFullNameAsync (plural)")}");

    var packageManager = new Windows.Management.Deployment.PackageManager();
    var options = new RegisterPackageOptions();
    
    DeploymentResult deploymentResult;
    
    if (useSingularMethod)
    {
        // Use the singular method
        deploymentResult = await packageManager.RegisterPackageByFullNameAsync(
            packageFullName, 
            null, // dependencyPackageFullNames
            DeploymentOptions.None);
    }
    else
    {
        // Use the plural method (default)
        deploymentResult = await packageManager.RegisterPackagesByFullNameAsync(
            [packageFullName], 
            options);
    }

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
