namespace UI.WPF.Services.Abstracts;

public interface IChangelogService
{
	/// <summary>
	/// Gets the all changelog from file.
	/// </summary>
	/// <returns><see cref="Dictionary2{TKey, TValue}{String, String}"/> where Key is version number, Value is <see cref="List{String}"/> of changes.</returns>
	IEnumerable<SingleVersionChangelogNote> GetAllChangelog();
}


public record class SingleVersionChangelogNote(string VersionName, IEnumerable<string> Notes);