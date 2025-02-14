namespace UI.WPF.Services.Abstracts;

public interface IChangelogService
{
	/// <summary>
	/// Gets the all changelog from file.
	/// </summary>
	/// <returns><see cref="Dictionary2{TKey, TValue}{String, String}"/> where Key is version number, Value is <see cref="List{String}"/> of changes.</returns>
	Dictionary<string, List<string>> GetAllChangelog();
}
