namespace Producer.Old
{
	public class PrinterProducer : ProducerBase
	{
		protected override void OnStartCustomActions()
		{
			base.OnStartCustomActions();
			StartProduce();
		}
	}
}
