#  JovDK
---

## About the project

JovDK is Unity **D**evelopment **K**it that groups tools and implementations that are not single-project-specifics. All Code on this kit need to have the intent of being used on any project with litle or none dependencies

---

### Getting Started:

To use this development kit, clone this repo as a git submodule of your main git project

##### Adding repo as git-submodule:

``` sh
# adding the submodule:
git submodule add https://github.com/joveem/JovDK/ Assets/_JovDK
#
```

##### If the project already has the submodule:

``` sh
# Install dependencies:
git submodule update --recursive --init
#
```

##### Dependencies:

[:link: .NET Framework 4.7.1](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net471) (Edit > Project Settings > Player > Other Settings > Configuration > API Compatibility Level)

[:link: Newtonsoft Json Unity Package ^3.0.0](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.0) (Unity Package Manager)