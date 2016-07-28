namespace Common.Helpers.Interfaces
{
	public interface ISerializeHelper<T>
	{
		T DeserializeXml(string filePath);
		void Serialize(string fileNameToGenerate, T objectToSerialize);
	}
}
