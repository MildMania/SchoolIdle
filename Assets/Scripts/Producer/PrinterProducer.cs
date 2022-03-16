using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterProducer : ProducerBase
{
	protected override void OnStartCustomActions()
	{
		base.OnStartCustomActions();
		StartProduce();
	}
}
