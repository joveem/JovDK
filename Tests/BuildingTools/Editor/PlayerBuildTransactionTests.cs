using System;
using NUnit.Framework;
using JovDK.Unity.Editor.Build;
public sealed class PlayerBuildTransactionTests
{
    [Test]
    public void Failure_ReleasesContextAndAllowsNextBuild()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            using var transaction = new PlayerBuildTransaction("C:/Synthetic Project/test.apk");
            Assert.That(PlayerBuildingTool.IsBuilding, Is.True);
            throw new InvalidOperationException("Synthetic build failure");
        });
        Assert.That(PlayerBuildingTool.IsBuilding, Is.False);
        Assert.That(PlayerBuildingTool.AuthorizedOutput, Is.Null);
        using var next = new PlayerBuildTransaction("C:/Synthetic Project/next.apk");
        Assert.That(PlayerBuildingTool.IsBuilding, Is.True);
    }
    [Test]
    public void ReentrantBuild_IsRejectedWithoutRevokingOwner()
    {
        using var transaction = new PlayerBuildTransaction("C:/Synthetic Project/owner.apk");
        Assert.Throws<InvalidOperationException>(() => new PlayerBuildTransaction("C:/Synthetic Project/other.apk"));
        Assert.That(PlayerBuildingTool.AuthorizedOutput.Replace('\\','/'), Does.EndWith("/owner.apk"));
    }
}
