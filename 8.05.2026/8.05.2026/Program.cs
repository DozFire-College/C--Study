namespace slovo
{
	class Program
	{
		static void Main(string[] args)
		{
			string fullSlovo = slovo1.GetPart() +
			                   slovo2.GetPart() +
			                   slovo3.GetPart();
			Console.WriteLine(fullSlovo);
		}
	}
}