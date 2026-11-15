using System.Text.Json;

try
{
    if (args.Length != 1) throw new ArgumentException("Pass one JSON input file");
    using var data = JsonDocument.Parse(File.ReadAllText(args[0]));
    Console.WriteLine(JsonSerializer.Serialize(Analyze(data.RootElement)));
    return 0;
}
catch (Exception error) when (error is IOException or JsonException or FormatException
                               or ArgumentException or InvalidOperationException
                               or KeyNotFoundException or OverflowException)
{
    Console.Error.WriteLine(error.Message);
    return 2;
}

static object Analyze(JsonElement data)
{
    static string[] Markers(JsonElement value)
    {
        var items=value.EnumerateArray().Select(x=>x.GetString()??"").ToArray();
        if(items.Length<1||items.Length>1000||items.Any(string.IsNullOrWhiteSpace))
            throw new ArgumentException("Expected 1 to 1000 markers");
        return items;
    }
    var reference=Markers(data.GetProperty("reference"));
    var observed=Markers(data.GetProperty("observed"));
    var offsets=new List<int>();
    if(reference.Length==observed.Length)
        for(var offset=0;offset<reference.Length;offset++)
            if(observed.Select((value,index)=>value==reference[(offset+index)%reference.Length]).All(x=>x))
                offsets.Add(offset);
    return new {match=offsets.Count>0,offset=offsets.Count>0?(int?)offsets[0]:null,allOffsets=offsets};
}
