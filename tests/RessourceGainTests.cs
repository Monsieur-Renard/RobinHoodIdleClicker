using Godot;
using System;

public class RessourceGainTests : WAT.Test
{
	Building building;

	public override String Title()
	{
		return "Ressource gain tests";
	}

    public override void Pre()
    {
        base.Pre();
		building = new Building();
		building.RessourceType = "Wood";
		building.BaseValue = 1;
		
	}

    [Test]
	public void ClickBasicAmount()
	{
		building.Level = 1;
		GlobalVariables.AxeLevel = 1;

		double expectedAmount = 1;

		Assert.IsEqual(expectedAmount, building.RessourceGained(), "The correct amount of ressources was gained for one click");
	}

	[Test]
	public void ClickBuildingLevel4()
	{
		building.Level = 4;
		GlobalVariables.AxeLevel = 1;

		double expectedAmount = 4;

		Assert.IsEqual(expectedAmount, building.RessourceGained(), "The correct amount of ressources was gained for one click on a level 4 building");
	}

	[Test]
	public void ClickToolLevel2()
	{
		building.Level = 1;
		GlobalVariables.AxeLevel = 2;

		double expectedAmount = 2;

		Assert.IsEqual(expectedAmount, building.RessourceGained(), "The correct amount of ressources was gained for one click with a level 2 tool");
	}

	[Test]
	public void ClickBuildingLevel4ToolLevel2()
	{
		building.Level = 4;
		GlobalVariables.AxeLevel = 2;

		double expectedAmount = 8;

		Assert.IsEqual(expectedAmount, building.RessourceGained(), "The correct amount of ressources was gained for one click on a level 4 building and a level 2 tool");
	}
}
