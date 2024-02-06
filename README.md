#  JovDK
---

## About the project

JovDK is Unity **D**evelopment **K**it that groups tools and implementations that are not single-project-specifics. All Code on this kit need to have the intent of being used on any project with litle or none dependencies

---

### Getting Started:

To use this development kit, first check the [Dependencies](#dependencies) section. After that, clone this repo as a git submodule of your main git project

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

Make sure to have all that before installation

[:link: .NET Framework 4.7.1](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net471) (Edit > Project Settings > Player > Other Settings > Configuration > API Compatibility Level > .NET Framework)
[:link: Newtonsoft Json Unity Package ^3.2.0](https://docs.unity3d.com/Packages/com.unity.nuget.newtonsoft-json@3.2) (Unity Package Manager > "+" > Add package by name > insert "com.unity.nuget.newtonsoft-json" > Add)
[:link: DOTween ^1.2.765](https://dotween.demigiant.com/download)
[:link: TextMeshPro ^3.0.7](https://docs.unity3d.com/Packages/com.unity.textmeshpro@3.2)
[:link: Unity-Logs-Viewer ^1.8](https://github.com/aliessmael/Unity-Logs-Viewer)
[:link: [Graphy] - Ultimate FPS Counter - Stats Monitor & Debugger ^3.0.5](https://assetstore.unity.com/packages/tools/gui/graphy-ultimate-fps-counter-stats-monitor-debugger-105778)
