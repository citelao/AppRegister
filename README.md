# AppRegister

This sample app exercises the `PackageManager` APIs to register packages:

* `RegisterPackageByFullNameAsync`
* `RegisterPackagesByFullNameAsync`

## Usage

Default, plural API (`RegisterPackageByFullNameAsync`):

```pwsh
> dotnet run "MyApp_10.24.0.2000_arm64__cw5n1h2txyewy"
Attempting to register package: MyApp_10.24.0.2000_arm64__cw5n1h2txyewy
Using method: RegisterPackagesByFullNameAsync (plural)
❌ Registration failed:
Error:
Extended Error Code:
```

Singular API (`RegisterPackageByFullNameAsync`):

```pwsh
> dotnet run -s "MyApp_10.24.0.2000_arm64__cw5n1h2txyewy"
Attempting to register package: MyApp_10.24.0.2000_arm64__cw5n1h2txyewy
Using method: RegisterPackageByFullNameAsync (singular)
✅ Package registered successfully!
```

The hope is that this can reproduce issues with the registration APIs.