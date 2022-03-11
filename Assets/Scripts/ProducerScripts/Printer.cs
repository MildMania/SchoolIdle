using System;

public class Printer : Producer
{
	protected override void AwakeCustomActions()
	{
		_produceDelay = GameConfigManager.Instance.GameConfig.PrinterDelay;
		_produceLimit = GameConfigManager.Instance.GameConfig.PrinterLimit;
	}
}