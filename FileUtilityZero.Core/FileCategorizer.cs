namespace FileUtilityZero.Core;

// Pure, stateless extension -> category lookup used to tag scan results with
// a simple, human-readable bucket for a file inventory. This is deliberately
// NOT a MIME type lookup - it's a coarse classification for eyeballing or
// filtering scan results, not content-type negotiation. GetCategory is a
// plain dictionary lookup with no filesystem access, so it needs no
// IFileSystem dependency and is static rather than instance-based.
public static class FileCategorizer
{
    // Fallback bucket for any extension not present in the map below
    // (including files with no extension at all).
    public const string DefaultCategory = "Other";

    // Keyed by extension including the leading dot (matching what
    // Path.GetExtension returns), case-insensitive so ".CPP" and ".cpp"
    // both resolve.
    //
    // Beyond the eight buckets requested (Code, Image, Document, Audio,
    // Video, Archive, Executable, Other), I added a ninth: "Font". Font
    // files (.ttf/.otf/.woff/...) don't fit naturally into any of the
    // other seven non-Other buckets, and they're common enough in a
    // general file inventory that dumping them into "Other" alongside
    // truly-unclassified files felt like it would lose useful signal.
    private static readonly Dictionary<string, string> ExtensionToCategory = new(StringComparer.OrdinalIgnoreCase)
    {
        // Code: source, script, markup/stylesheet, and query files.
        [".c"] = "Code",
        [".h"] = "Code",
        [".cpp"] = "Code",
        [".cc"] = "Code",
        [".cxx"] = "Code",
        [".hpp"] = "Code",
        [".hh"] = "Code",
        [".cs"] = "Code",
        [".java"] = "Code",
        [".py"] = "Code",
        [".pyw"] = "Code",
        [".js"] = "Code",
        [".mjs"] = "Code",
        [".cjs"] = "Code",
        [".ts"] = "Code",
        [".tsx"] = "Code",
        [".jsx"] = "Code",
        [".go"] = "Code",
        [".rb"] = "Code",
        [".php"] = "Code",
        [".swift"] = "Code",
        [".kt"] = "Code",
        [".kts"] = "Code",
        [".rs"] = "Code",
        [".scala"] = "Code",
        [".pl"] = "Code",
        [".pm"] = "Code",
        [".sh"] = "Code",
        [".bash"] = "Code",
        [".zsh"] = "Code",
        [".ps1"] = "Code",
        [".psm1"] = "Code",
        [".psd1"] = "Code",
        [".vb"] = "Code",
        [".vbs"] = "Code",
        [".sql"] = "Code",
        [".html"] = "Code",
        [".htm"] = "Code",
        [".css"] = "Code",
        [".scss"] = "Code",
        [".sass"] = "Code",
        [".less"] = "Code",
        [".lua"] = "Code",
        [".r"] = "Code",
        [".m"] = "Code",
        [".asm"] = "Code",
        [".s"] = "Code",
        [".dart"] = "Code",
        [".groovy"] = "Code",
        [".clj"] = "Code",
        [".ex"] = "Code",
        [".exs"] = "Code",
        [".erl"] = "Code",
        [".hs"] = "Code",
        [".jl"] = "Code",

        // Image
        [".jpg"] = "Image",
        [".jpeg"] = "Image",
        [".png"] = "Image",
        [".gif"] = "Image",
        [".bmp"] = "Image",
        [".tiff"] = "Image",
        [".tif"] = "Image",
        [".ico"] = "Image",
        [".svg"] = "Image",
        [".webp"] = "Image",
        [".heic"] = "Image",
        [".heif"] = "Image",
        [".raw"] = "Image",
        [".cr2"] = "Image",
        [".nef"] = "Image",
        [".psd"] = "Image",
        [".ai"] = "Image",
        [".eps"] = "Image",

        // Document: office documents, plain/structured text, and
        // config/data-as-text files (json/xml/yaml/csv/etc.) - none of the
        // requested buckets fit those better than "Document" does.
        [".doc"] = "Document",
        [".docx"] = "Document",
        [".pdf"] = "Document",
        [".txt"] = "Document",
        [".rtf"] = "Document",
        [".odt"] = "Document",
        [".xls"] = "Document",
        [".xlsx"] = "Document",
        [".ods"] = "Document",
        [".ppt"] = "Document",
        [".pptx"] = "Document",
        [".odp"] = "Document",
        [".csv"] = "Document",
        [".tsv"] = "Document",
        [".md"] = "Document",
        [".markdown"] = "Document",
        [".xml"] = "Document",
        [".json"] = "Document",
        [".yaml"] = "Document",
        [".yml"] = "Document",
        [".ini"] = "Document",
        [".cfg"] = "Document",
        [".conf"] = "Document",
        [".toml"] = "Document",
        [".log"] = "Document",
        [".epub"] = "Document",

        // Audio
        [".mp3"] = "Audio",
        [".wav"] = "Audio",
        [".flac"] = "Audio",
        [".aac"] = "Audio",
        [".ogg"] = "Audio",
        [".wma"] = "Audio",
        [".m4a"] = "Audio",
        [".opus"] = "Audio",
        [".aiff"] = "Audio",
        [".mid"] = "Audio",
        [".midi"] = "Audio",

        // Video
        [".mp4"] = "Video",
        [".avi"] = "Video",
        [".mov"] = "Video",
        [".wmv"] = "Video",
        [".flv"] = "Video",
        [".mkv"] = "Video",
        [".webm"] = "Video",
        [".m4v"] = "Video",
        [".mpg"] = "Video",
        [".mpeg"] = "Video",
        [".3gp"] = "Video",

        // Archive (includes disk images, which are casually "an archive" to
        // most users of a file inventory tool like this one)
        [".zip"] = "Archive",
        [".rar"] = "Archive",
        [".7z"] = "Archive",
        [".tar"] = "Archive",
        [".gz"] = "Archive",
        [".tgz"] = "Archive",
        [".bz2"] = "Archive",
        [".xz"] = "Archive",
        [".cab"] = "Archive",
        [".lz"] = "Archive",
        [".zst"] = "Archive",
        [".iso"] = "Archive",

        // Executable
        [".exe"] = "Executable",
        [".msi"] = "Executable",
        [".bat"] = "Executable",
        [".cmd"] = "Executable",
        [".com"] = "Executable",
        [".app"] = "Executable",
        [".dll"] = "Executable",
        [".so"] = "Executable",
        [".dylib"] = "Executable",
        [".deb"] = "Executable",
        [".rpm"] = "Executable",
        [".apk"] = "Executable",
        [".jar"] = "Executable",

        // Font
        [".ttf"] = "Font",
        [".otf"] = "Font",
        [".woff"] = "Font",
        [".woff2"] = "Font",
        [".eot"] = "Font",
    };

    // Looks up the human-readable category for a file extension (as returned
    // by Path.GetExtension, e.g. ".cpp" - leading dot, any case). Unknown or
    // missing extensions fall back to DefaultCategory ("Other").
    public static string GetCategory(string extension)
    {
        if (string.IsNullOrEmpty(extension))
        {
            return DefaultCategory;
        }

        return ExtensionToCategory.TryGetValue(extension, out string? category) ? category : DefaultCategory;
    }
}
