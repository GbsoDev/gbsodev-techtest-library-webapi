namespace GbsoDev.TechTest.Library.MSTest
{
	public static class Utils
	{
		public static void AreEquals<T>(object expected, T actual)
		{
			foreach (var expectedProperty in expected.GetType().GetProperties())
			{
				var expectedValue = expectedProperty.GetValue(expected);
				var actualProperty = typeof(T).GetProperty(expectedProperty.Name) ?? throw new ApplicationException($"La propiedad esperada \"{expectedProperty.Name}\" se encuentra entra las propiedades de la salida");
				var actualValue = actualProperty.GetValue(actual);
				Assert.AreEqual(expectedValue, actualValue, $"El valor la propiedad de entrada \"{expectedProperty.Name}\", no puede ser distinto en la salida");
			}
		}

		public static void IsNotNulls<T>(object expected, T actual)
		{
			foreach (var expectedProperty in expected.GetType().GetProperties())
			{
				var actualProperty = typeof(T).GetProperty(expectedProperty.Name) ?? throw new ApplicationException($"La propiedad esperada \"{expectedProperty.Name}\" se encuentra entra las propiedades de la salida");
				var actualValue = actualProperty.GetValue(actual);
				Assert.IsNotNull(actualValue, $"El valor la propiedad de salida \"{expectedProperty.Name}\", no puede ser nulo");
			}
		}
	}
}
